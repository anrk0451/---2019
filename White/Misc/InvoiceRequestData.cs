using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White
{
	/// <summary>
	/// 封装发送 博思数据
	/// </summary>
	class InvoiceRequestData
	{
		public string region { get; set; }                 //区划，由平台提供
		public string deptcode { get; set; }               //单位标识，由平台提供
		public string appid { get; set; }                  //调用方应用帐号，由平台提供
		public string data { get; set; }                   //请求业务参数 json 再经过base64编码
		public string noise { get; set; }                  //请求随机标识
		public string version { get; set; }                //版本号
		public string sign { get; set; }                   //签名 MD5摘要结果转换成大写

	}
}

	