using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White.DataSet
{
	/// <summary>
	/// 火化登记 数据集
	/// </summary>
	class Checkin_ds : System.Data.DataSet
	{
		public DataTable Ac01 { get; }                     //登记信息表
		public DataTable St01 { get; }					   //系统数据字典表
		public DataTable Uc01 { get; }					   //操作员表	

		public DataTable Ct01 { get; }					   //系统代码表	 

		public DataView St01_reason { get; }			   //死亡原因
		public DataView St01_district { get; }			   //行政区县
		public DataView St01_driver { get; }			   //灵车司机	
		public DataView St01_relation { get; }             //与逝者关系
		public DataView St01_gtype { get; }				   //逝者类别	


		public OracleDataAdapter ac01Adapter { get; }
		public OracleDataAdapter st01Adapter { get; }
		public OracleDataAdapter uc01Adapter { get; }
		public OracleDataAdapter ct01Adapter { get; }

		/// <summary>
		/// 数据集构造函数（创建各个表、视图）
		/// </summary>
		public Checkin_ds()
		{
			//1.Ac01
			DataColumn col_ac001 = new DataColumn("AC001", typeof(string));   // 逝者编号
			col_ac001.Unique = true;
			col_ac001.AllowDBNull = false;
			DataColumn col_ac003 = new DataColumn("AC003", typeof(string));   // 逝者姓名
			DataColumn col_ac002 = new DataColumn("AC002", typeof(string));   // 性别 0-男 1-女 2-不详
			DataColumn col_ac004 = new DataColumn("AC004", typeof(int));      // 年龄
			DataColumn col_ac006 = new DataColumn("AC006", typeof(string));   // 骨灰处理方式

			DataColumn col_ac014 = new DataColumn("AC014", typeof(string));   // 身份证号
			DataColumn col_ac010 = new DataColumn("AC010", typeof(DateTime)); // 死亡时间
			DataColumn col_ac015 = new DataColumn("AC015", typeof(DateTime)); // 火化时间
			DataColumn col_ac005 = new DataColumn("AC005", typeof(string));   // 死亡原因
			DataColumn col_ac007 = new DataColumn("AC007", typeof(string));   // 所属区县
			DataColumn col_ac008 = new DataColumn("AC008", typeof(string));   // 详细地址
			DataColumn col_ac009 = new DataColumn("AC009", typeof(string));   // 接灵地址
			DataColumn col_ac020 = new DataColumn("AC020", typeof(DateTime)); // 到达中心时间
			DataColumn col_ac018 = new DataColumn("AC018", typeof(DateTime)); // 告别时间
			DataColumn col_ac019 = new DataColumn("AC019", typeof(DateTime)); // 开光时间
			DataColumn col_ac022 = new DataColumn("AC022", typeof(string));   // 主持人
			DataColumn col_ac050 = new DataColumn("AC050", typeof(string));   // 联系人
			DataColumn col_ac051 = new DataColumn("AC051", typeof(string));   // 联系电话
			DataColumn col_ac052 = new DataColumn("AC052", typeof(string));   // 与逝者关系
			DataColumn col_ac150 = new DataColumn("AC150", typeof(string));   // 联系人2
			DataColumn col_ac151 = new DataColumn("AC151", typeof(string));   // 联系电话2
			DataColumn col_ac152 = new DataColumn("AC152", typeof(string));   // 与逝者关系2

			DataColumn col_ac055 = new DataColumn("AC055", typeof(string));   // 联系地址
			DataColumn col_ac060 = new DataColumn("AC060", typeof(string));   // 灵车车号

			DataColumn col_ac100 = new DataColumn("AC100", typeof(string));   // 登记经办人
			DataColumn col_ac200 = new DataColumn("AC200", typeof(DateTime)); // 登记时间
			DataColumn col_ac110 = new DataColumn("AC110", typeof(string));   // 最后修改人
			DataColumn col_ac220 = new DataColumn("AC220", typeof(DateTime)); // 最后修改日期
			DataColumn col_ac099 = new DataColumn("AC099", typeof(string));   // 备注
			DataColumn col_ac300 = new DataColumn("AC300", typeof(string));   // 逝者类别
			DataColumn col_status = new DataColumn("STATUS", typeof(string)); // 当前状态  1-正常 0-删除

			Ac01 = new DataTable("Ac01");
			Ac01.Columns.AddRange(new DataColumn[] {col_ac001,col_ac003,col_ac002,col_ac004,col_ac006,col_ac014,col_ac010,col_ac015,col_ac005,col_ac007,col_ac008,col_ac009,col_ac020,
				col_ac018,col_ac019,col_ac022,col_ac050,col_ac051,col_ac052,col_ac055,col_ac060,col_ac150,col_ac151,col_ac152,col_ac100,col_ac200,col_ac110,col_ac220,col_ac099,col_ac300,col_status
			});
			Ac01.PrimaryKey = new DataColumn[] { col_ac001 };                 //设置主键
			this.Tables.Add(Ac01);
			ac01Adapter = new OracleDataAdapter("select * from ac01 where status <> '0' ", SqlAssist.conn);


			//2.St01 系统数据字典项
			St01 = new DataTable("St01");
			this.Tables.Add(St01);
			st01Adapter = new OracleDataAdapter("select * from st01 order by sortId", SqlAssist.conn);
			st01Adapter.Fill(St01);

			//3.Uc01  操作员
			Uc01 = new DataTable("Uc01");
			this.Tables.Add(Uc01);
			uc01Adapter = new OracleDataAdapter("select * from uc01", SqlAssist.conn);

			//4. Ct01 系统代码表
			Ct01 = new DataTable("Ct01");
			this.Tables.Add(Ct01);
			ct01Adapter = new OracleDataAdapter("select * from ct01", SqlAssist.conn);
			ct01Adapter.Fill(Ct01);


			//5.DataView 数据视图

			St01_reason = new DataView(St01);
			St01_reason.RowFilter = "ST002='DIEREASON' and status = '1'";

			St01_driver = new DataView(St01);
			St01_driver.RowFilter = "ST002='DRIVER'";

			St01_district = new DataView(St01);
			St01_district.RowFilter = "ST002='DISTRICT' and status = '1'";

			St01_relation = new DataView(St01);
			St01_relation.RowFilter = "ST002='RELATION' and status = '1' ";

			St01_gtype = new DataView(St01);
			St01_gtype.RowFilter = "ST002='GUYTYPE' and status = '1' ";
		}

		/// <summary>
		/// 检索 登记信息
		/// </summary>
		public void Fill_ac01()
		{
			Ac01.Rows.Clear();
			ac01Adapter.Fill(Ac01);
		}

	}
}
