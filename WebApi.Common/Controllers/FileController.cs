using Adai.Standard;
using Adai.Standard.Ext;
using Adai.Standard.Models;
using Basic;
using Basic.BLL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Common.Controllers
{
	/// <summary>
	/// FileController
	/// </summary>
	[Route("api/File"), ApiExplorerSettings(GroupName = "public")]
	public class FileController : ApiController
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="factory"></param>
		/// <param name="webHostEnvironment"></param>
		public FileController(IStringLocalizerFactory factory, IWebHostEnvironment webHostEnvironment) : base(factory, webHostEnvironment)
		{
		}

		/// <summary>
		/// 上传
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		[ApiAuthorize(Frequency = 0D)]
		[Consumes("multipart/form-data")]
		[HttpPost("Upload")]
		public async Task<ReturnResult<FileData>> Upload(IFormFile file)
		{
			//非后台管理员限制请求频率
			if (LoginInfo == null || LoginInfo.Data.Type != "User")
			{
				Request.VerifyRequestFrequencyLimit(1D);
			}
			var fileDatas = await FileHelper.UploadAsync(WebHostEnvironment.WebRootPath, file).ConfigureAwait(false);
			var fileData = fileDatas.FirstOrDefault();
			//保存文件信息
			var data = new Basic.Model.File()
			{
				Code = fileData.Guid,
				Name = fileData.Name,
				Path = fileData.VirtualPath,
				Length = fileData.Length,
				Extension = fileData.Extension
			};
			new FileBLL().Add(data);
			return Json(fileData);
		}

		/// <summary>
		/// 上传（批量）
		/// </summary>
		/// <param name="files"></param>
		/// <returns></returns>
		[ApiAuthorize(Frequency = 1D)]
		[Consumes("multipart/form-data")]
		[HttpPost("Uploads")]
		public async Task<ReturnResult<ICollection<FileData>>> Uploads(IFormFileCollection files)
		{
			var fileDatas = await FileHelper.UploadAsync(WebHostEnvironment.WebRootPath, files.ToArray()).ConfigureAwait(false);
			//保存文件信息
			var datas = new HashSet<Basic.Model.File>();
			foreach (var fileData in fileDatas)
			{
				datas.Add(new Basic.Model.File()
				{
					Code = fileData.Guid,
					Name = fileData.Name,
					Path = fileData.VirtualPath,
					Length = fileData.Length,
					Extension = fileData.Extension
				});
			}
			new FileBLL().Add(datas);
			return Json(fileDatas);
		}

		/// <summary>
		/// 下载
		/// </summary>
		/// <param name="ids">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("Download/{ids}")]
		public FileResult Download(string ids)
		{
			var idArr = CommonHelper.StringToIds(ids);
			string path;
			if (idArr.Count > 1)
			{
				var datas = new FileBLL().ListByPks(idArr);
				var files = new Dictionary<string, string>();
				foreach (var data in datas)
				{
					var temp = FileHelper.ToPhysicalPath(data.Path);
					temp = Path.Combine(WebHostEnvironment.WebRootPath, temp);
					files.Add(data.Code, temp);
				}
				path = FileHelper.Compress(WebHostEnvironment.WebRootPath, files);
			}
			else
			{
				var data = new FileBLL().Get(idArr.FirstOrDefault());
				var temp = FileHelper.ToPhysicalPath(data.Path);
				path = Path.Combine(WebHostEnvironment.WebRootPath, temp);
			}

			if (!System.IO.File.Exists(path))
			{
				throw new Exception("对不起，您要下载的文件不存在。");
			}
			//创建文件流
			var stream = System.IO.File.OpenRead(path);
			var extension = Path.GetExtension(path);
			var contentType = new FileExtensionContentTypeProvider().Mappings[extension];
			var fileName = Path.GetFileName(path);
			return File(stream, contentType, fileName);
		}

		/// <summary>
		/// Manager
		/// </summary>
		/// <param name="direction"></param>
		/// <param name="path"></param>
		/// <param name="order"></param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("Manager")]
		public ReturnResult<Hashtable> Manager(string direction = "", string path = "", string order = "")
		{
			//根目录路径，绝对路径
			var rootPath = string.Format("{0}/{1}/", FileHelper.UploadDirectory, LoginInfo.Id);
			var serverPath = WebHostEnvironment.WebRootPath + "\\" + rootPath;
			if (!string.IsNullOrEmpty(direction))
			{
				var directions = new string[] { "image", "media", "file" };
				if (Array.IndexOf(directions, direction) == -1)
				{
					return Json(new Hashtable()
					{
						{ "error", 1 },
						{ "message", "目录名错误。" }
					});
				}

				rootPath += direction + "/";
				serverPath += direction + "/";
				if (!Directory.Exists(serverPath))
				{
					Directory.CreateDirectory(serverPath);
				}
			}

			string currentPath, currentUrl, currentDirPath, moveupDirPath;
			//根据path参数，设置各路径和URL
			if (string.IsNullOrEmpty(path))
			{
				currentPath = serverPath;
				currentUrl = rootPath;
				currentDirPath = "";
				moveupDirPath = "";
			}
			else
			{
				currentPath = serverPath + path;
				currentUrl = rootPath + path;
				currentDirPath = path;
				moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
			}

			//排序形式，name or size or type
			order = order.ToLower(CultureInfo.CurrentCulture);

			//不允许使用.移动到上一级目录
			if (Regex.IsMatch(path, @"\.\."))
			{
				return Json(new Hashtable()
				{
					{ "error", 1 },
					{ "message", "没有权限。" }
				});
			}

			//最后一个字符不是/
			if (!string.IsNullOrEmpty(path) && !path.EndsWith("/"))
			{
				return Json(new Hashtable()
				{
					{ "error", 1 },
					{ "message", "参数错误。" }
				});
			}

			//目录不存在或不是目录
			if (!Directory.Exists(currentPath))
			{
				return Json(new Hashtable()
				{
					{ "error", "1" },
					{ "message",  "目录不存在。" }
				});
			}

			//遍历目录取得文件信息
			var dirList = Directory.GetDirectories(currentPath);
			var fileList = Directory.GetFiles(currentPath);

			switch (order)
			{
				case "size":
					Array.Sort(dirList, new NameSorter());
					Array.Sort(fileList, new SizeSorter());
					break;
				case "type":
					Array.Sort(dirList, new NameSorter());
					Array.Sort(fileList, new TypeSorter());
					break;
				case "name":
				default:
					Array.Sort(dirList, new NameSorter());
					Array.Sort(fileList, new NameSorter());
					break;
			}

			var result = new Hashtable()
			{
				{ "moveup_dir_path", moveupDirPath },
				{ "current_dir_path", currentDirPath },
				{ "current_url", currentUrl },
				{ "total_count", dirList.Length + fileList.Length }
			};
			var file_list = new HashSet<Hashtable>();
			for (int i = 0; i < dirList.Length; i++)
			{
				var dirInfo = new DirectoryInfo(dirList[i]);
				var hash = new Hashtable()
				{
					{ "is_dir", true },
					{ "has_file", dirInfo.GetFileSystemInfos().Length > 0 },
					{ "filesize", 0 },
					{ "is_photo", false },
					{ "filetype", "" },
					{ "filename", dirInfo.Name },
					{ "datetime", dirInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss") }
				};
				file_list.Add(hash);
			}
			result["file_list"] = file_list;
			//图片扩展名
			var extensions = ConfigurationManager.AppSettings["Upload:Extensions-Image"].Split(',');
			for (int i = 0; i < fileList.Length; i++)
			{
				var file = new FileInfo(fileList[i]);
				var hash = new Hashtable()
				{
					{ "is_dir", false },
					{ "has_file", false },
					{ "filesize", file.Length },
					{ "is_photo", Array.IndexOf(extensions, file.Extension.Substring(1).ToLower(CultureInfo.CurrentCulture)) >= 0 },
					{ "filetype", file.Extension.Substring(1) },
					{ "filename", file.Name },
					{ "datetime", file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss") }
				};
				file_list.Add(hash);
			}
			return Json(result);
		}
	}
}