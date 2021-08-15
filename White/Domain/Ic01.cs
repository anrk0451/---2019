using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White.Domain
{
	class Ic01
	{
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public string ic001 { get; set; } //身份编号
		public string ac001 { get; set; } //逝者编号
		public string ic000 { get; set; } //类型 0-逝者 1-家属
		public string ic003 { get; set; } //姓名
		public string ic002 { get; set; } //性别

		public DateTime ic004 { get; set; }  //出生日期
		public string ic014 { get; set; }     //身份证号
		public string ic016 { get; set; }     //地址
		public string ic017 { get; set; }     //签发机关
		public string ic018 { get; set; }     //有效期限
		public string ic019 { get; set; }     //安全模块号

	}

}
