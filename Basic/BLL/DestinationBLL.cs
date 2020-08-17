using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// DestinationBLL
	/// </summary>
	public class DestinationBLL : BLL<Destination>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly DestinationDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public DestinationBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new DestinationDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(Destination data)
		{
			if (data.RegionId < 1)
			{
				return "地区Id无效。";
			}
			//data.Region = new RegionBLL().Get(data.RegionId, true);
			//if (data.Region == null)
			//{
			//	return "地区Id无效。";
			//}
			if (ExistByRegionId(data.Id, data.RegionId, true))
			{
				return "相同地区Id已存在。";
			}
			if (!ValidateStatus(data.Status))
			{
				return "状态标识无效。";
			}
			data.Cover = CommonHelper.ReplaceFilePath(data.Cover);
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
				data.CreateTime = data.UpdateTime;
			}
			return base.Validate(data);
		}

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="data"></param>
		public override void Add(Destination data)
		{
			data.Region = new RegionBLL().Get(data.RegionId, true);
			var parents = new List<Destination>();
			foreach (var parentId in data.Region.ParentIds)
			{
				if (parentId == 0)
				{
					continue;
				}
				var parent = Dal.GetByRegionId(parentId, true);
				if (parent == null)
				{
					parent = new Destination()
					{
						RegionId = parentId,
						UpdateTime = DateTime.Now,
						Sequence = 99,
						Status = Model.Config.Status.Enabled
					};
					parent.CreateTime = parent.UpdateTime;
				}
				parents.Add(parent);
			}
			var datas = parents.Where(o => o.Id == 0).ToList();
			if (datas.Count > 0)
			{
				datas.Add(data);
				base.Add(datas);
			}
			else
			{
				base.Add(data);
			}
		}

		/// <summary>
		/// 修改状态
		/// </summary>
		/// <param name="id"></param>
		/// <param name="status"></param>
		public override void UpdateStatus(int id, int status)
		{
			if (!ValidateStatus(status))
			{
				throw new CustomException("状态标识无效。");
			}
			var data = new Destination()
			{
				Id = id,
				Status = status
			};
			Dal.Update(data, new string[] { "Status" });
		}

		/// <summary>
		/// 关联数据
		/// </summary>
		/// <param name="list"></param>
		public override void List(ICollection<Destination> list)
		{
			var regionIds = list.Select(o => o.RegionId).Distinct().ToArray();
			var regions = new RegionBLL().ListByPks(regionIds, true);
			foreach (var data in list)
			{
				data.Region = regions.Where(o => o.Id == data.RegionId).FirstOrDefault();
				data.Cover = CommonHelper.RestoreFilePath(data.Cover);
			}
		}

		#region 扩展

		/// <summary>
		/// 修改封面
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cover"></param>
		public void UpdateCover(int id, string cover)
		{
			var data = new Destination()
			{
				Id = id,
				Cover = CommonHelper.ReplaceFilePath(cover)
			};
			Dal.Update(data, new string[] { "Cover" });
		}

		/// <summary>
		/// 是否存在相同地区Id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="regionId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public bool ExistByRegionId(int id, int regionId, bool useCache = false)
		{
			var result = Dal.GetByRegionId(regionId, useCache);
			return result != null && result.Id != id;
		}

		/// <summary>
		/// 查询 - 根据热门指数倒序
		/// </summary>
		/// <param name="take"></param>
		/// <returns></returns>
		public ICollection<Destination> ListByPopularIndex(int take = 10)
		{
			return List(take, "PopularIndex", OrderByType.Desc);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="take"></param>
		/// <param name="orderByField"></param>
		/// <param name="orderByType"></param>
		/// <returns></returns>
		public ICollection<Destination> List(int take = 10, string orderByField = "Sequence", OrderByType orderByType = OrderByType.Asc)
		{
			var results = Dal.List(take, orderByField, orderByType);
			if (results.Count > 0)
			{
				List(results);
			}
			return results.ToArray();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual ICollection<Destination> ListByParentId(int parentId, bool useCache = false)
		{
			var results = Dal.ListByParentId(parentId, useCache);
			if (results.Count > 0)
			{
				List(results);
			}
			return results.ToArray();
		}

		#endregion
	}
}
