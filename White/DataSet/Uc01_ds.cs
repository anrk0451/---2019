using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White.DataSet
{
    class Uc01_ds : System.Data.DataSet
    {
        public DataTable Uc01 { get; }
        public OracleDataAdapter uc01Adapter { get; }
        OracleCommandBuilder builder = null;

        public Uc01_ds()
        {
            DataColumn col_uc001 = new DataColumn("UC001", typeof(string));   // 操作员编号      
            DataColumn col_uc002 = new DataColumn("UC002", typeof(string));   // 操作员代码            
            DataColumn col_uc003 = new DataColumn("UC003", typeof(string));   // 操作员姓名
            DataColumn col_uc007 = new DataColumn("UC007", typeof(string));   // 博思登录账号
            DataColumn col_uc008 = new DataColumn("UC008", typeof(string));   // 博思登录密码
            DataColumn col_uc004 = new DataColumn("UC004", typeof(string));   // 密码

            DataColumn col_status = new DataColumn("STATUS", typeof(string)); // 状态
            DataColumn col_roleslist = new DataColumn("ROLESLIST", typeof(string)); // 所属角色列表

            Uc01 = new DataTable("Uc01");
            Uc01.Columns.AddRange(new DataColumn[]
                {col_uc001,col_uc002,col_uc003,col_uc004,col_uc008,col_uc007,col_status,col_roleslist});
            Uc01.PrimaryKey = new DataColumn[] { col_uc001 };                //设置主键

            this.Tables.Add(Uc01);

            uc01Adapter = new OracleDataAdapter("select * from v_uc01 where status = '1' order by uc001", SqlAssist.conn);

            builder = new OracleCommandBuilder(uc01Adapter);
        }
    }
}
