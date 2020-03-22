using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White.Domain
{
    /// <summary>
    /// 外卖客户对象
    /// </summary>
    public class Tu01
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string tu001 { get; set; }
        public string tu003 { get; set; }  //名称
        public string tu005 { get; set; }  //税号
        public string tu006 { get; set; }  //地址、电话
        public string tu007 { get; set; }  //银行、账号
    }
}
