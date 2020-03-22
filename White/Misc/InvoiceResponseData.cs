using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White
{
    /// <summary>
    /// 封装 博思返回数据
    /// </summary>
    class InvoiceResponseData
    {
        //public Dictionary<string,string> data { get; set; }  //返回结果参数

		public string data { get; set; }
        public string noise { get; set; }                      //请求随机标识
        public string sign { get; set; }                       //签名
       
    }
}
