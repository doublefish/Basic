namespace Basic.Model.Config
{
	/// <summary>
	/// 充值
	/// </summary>
	public class Recharge : Adai.Standard.Models.Config
	{
		/// <summary>
		/// 转账支付
		/// </summary>
		public const int Transfer = 11;
		/// <summary>
		/// 网关支付
		/// </summary>
		public const int Gateway = 12;
		/// <summary>
		/// 快捷支付
		/// </summary>
		public const int Quick = 13;
		/// <summary>
		/// 认证支付
		/// </summary>
		public const int Auth = 14;
		/// <summary>
		/// 银联支付
		/// </summary>
		public const int Union = 15;
		/// <summary>
		/// 支付宝
		/// </summary>
		public const int Alipay = 16;
		/// <summary>
		/// 微信支付
		/// </summary>
		public const int Wechat = 17;

		/// <summary>
		/// 构造函数
		/// </summary>
		public Recharge()
		{
			Add(Transfer, "转账支付");
			Add(Gateway, "网关支付");
			Add(Quick, "快捷支付");
			Add(Auth, "认证支付");
			Add(Union, "银联支付");
			Add(Alipay, "支付宝");
			Add(Wechat, "微信支付");
		}
	}
}
