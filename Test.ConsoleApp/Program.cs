using System;
using System.Net.Sockets;
using System.Text;

namespace Test.ConsoleApp
{
	/// <summary>
	/// Program
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Main
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			Console.WriteLine(string.Join(',', args));



			Test(100);

			Console.WriteLine("Press Any Key to continue...");
			Console.ReadKey();
		}

		/// <summary>
		/// 接收信息
		/// </summary>
		/// <param name="obj"></param>
		public static void Recevice(object obj)
		{
			var client = (Socket)obj;
			while (true)
			{
				//接受从服务器返回的信息
				byte[] bytes = new byte[1024];
				//从服务器端接受返回信息
				var sieze = client.Receive(bytes, bytes.Length, 0);
				var message = Encoding.UTF8.GetString(bytes, 0, sieze);
				Console.WriteLine("客户端【{0}】接收服务器消息{1}", client.LocalEndPoint.ToString(), message);
			}
		}


		/// <summary>
		/// 测试
		/// </summary>
		/// <param name="runtimes">运行次数</param>
		static void Test(int runtimes = 100)
		{
			//var server = new SocketService("127.0.0.1", 8080, "S");

			//var client0 = new SocketClient(server.Host, server.Port, "CA");
			//var client1 = new SocketClient(server.Host, server.Port, "CB");



			var startTime = DateTime.Now;
			for (var i = 0; i < runtimes; i++)
			{
				//var s = server.Send("来自服务器的问好！", server.Remotes.ToArray());

				//try
				//{
				//	//向服务器发送信息
				//	client0.Send(string.Format("服务器你好啊{0} -- {1}", i, client0.Name));
				//	client1.Send(string.Format("服务器你好啊{0} -- {1}", i, client1.Name));
				//}
				//catch (Exception ex)
				//{
				//	//一定记着用完socket后要关闭
				//	client0.Close();
				//	client1.Close();
				//	Console.WriteLine("{0}", ex.Message);
				//}
			}

			var time = DateTime.Now.Subtract(startTime);
			var total_ms = time.TotalMilliseconds;
			var avg_ms = time.TotalMilliseconds / runtimes;
			Console.WriteLine("运行（次）：{0}，总耗时（毫秒）：{1}，平均耗时（毫秒）：{2}", runtimes, total_ms, avg_ms);
		}

	}
}
