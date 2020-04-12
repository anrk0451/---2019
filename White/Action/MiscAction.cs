using DevExpress.XtraEditors;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace White.Action
{
    class MiscAction
    {
		/// <summary>
		/// 返回服务或商品名
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemFullName(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_getItemFullName(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString();
		}

		/// <summary>
		/// 返回服务或商品单价
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static decimal GetItemFixPrice(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return decimal.Parse(SqlAssist.ExecuteScalar("select pkg_business.fun_getFixPrice(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString());
		}

		/// <summary>
		/// 获取项目票别
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemInvoiceType(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_getItemInvoiceType(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString();
		}

		/// <summary>
		/// 返回商品税率
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static decimal GetItemTaxRate(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return decimal.Parse(SqlAssist.ExecuteScalar("select pkg_business.fun_getItemTaxRate(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString());
		}

		/// <summary>
		/// 获取项目的 发票编码 (含财政、税务)
		/// </summary>
		/// <param name="serviceSalesType"></param>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemInvoiceCode(string serviceSalesType,string itemId)
		{
			//项目类别
			OracleParameter op_type = new OracleParameter("ic_serviceSalesType", OracleDbType.Varchar2, 3);
			op_type.Direction = ParameterDirection.Input;
			op_type.Value = serviceSalesType;

			//项目ID
			OracleParameter op_itemId = new OracleParameter("ic_salesItemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_GetInvoiceCode(:type,:itemId) from dual", new OracleParameter[] { op_type,op_itemId }).ToString();
		}

		/// <summary>
		/// 操作员姓名映射
		/// </summary>
		/// <param name="uc001"></param>
		/// <returns></returns>
		public static string Mapper_operator(string uc001)
		{
			OracleParameter op_uc001 = new OracleParameter("uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Value = uc001;
			Object re = SqlAssist.ExecuteScalar("select uc003 from uc01 where uc001 = :uc001", new OracleParameter[] { op_uc001 });
			return re.ToString();
		}

		/// <summary>
		/// 财务收款作废
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int FinanceRemove(string fa001, string reason, string handler)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//作废原因
			OracleParameter op_fa003 = new OracleParameter("ic_fa003", OracleDbType.Varchar2, 50);
			op_fa003.Direction = ParameterDirection.Input;
			op_fa003.Value = reason;

			//作废人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FinanceRemove",
				new OracleParameter[] { op_fa001, op_fa003, op_handler });
		}

		/// <summary>
		/// 财政发票作废日志
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="zfr"></param>
		/// <param name="reason"></param>
		/// <returns></returns>
		public static int FinRemove_log(string fa001,string zfr,string reason)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//作废人
			OracleParameter op_zfr = new OracleParameter("ic_zfr", OracleDbType.Varchar2, 50);
			op_zfr.Direction = ParameterDirection.Input;
			op_zfr.Value = zfr;

			//作废原因
			OracleParameter op_reason = new OracleParameter("ic_reason", OracleDbType.Varchar2, 100);
			op_reason.Direction = ParameterDirection.Input;
			op_reason.Value = reason;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FinRemove_log",
				new OracleParameter[] { op_fa001, op_zfr,op_reason});
		}

		/// <summary>
		/// 税务发票作废日志
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="zfr"></param>
		/// <param name="reason"></param>
		/// <returns></returns>
		public static int TaxRemove_log(string fa001, string zfr, string reason)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//作废人
			OracleParameter op_zfr = new OracleParameter("ic_zfr", OracleDbType.Varchar2, 50);
			op_zfr.Direction = ParameterDirection.Input;
			op_zfr.Value = zfr;

			//作废原因
			OracleParameter op_reason = new OracleParameter("ic_reason", OracleDbType.Varchar2, 100);
			op_reason.Direction = ParameterDirection.Input;
			op_reason.Value = reason;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_TaxRemove_log",
				new OracleParameter[] { op_fa001, op_zfr, op_reason });
		}
		/// <summary>
		/// 修改密码
		/// </summary>
		/// <param name="uc001"></param>
		/// <param name="newpwd"></param>
		/// <returns></returns>
		public static int Modify_Pwd(string uc001, string newpwd)
		{
			//用户编号
			OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Value = uc001;

			//新密码
			OracleParameter op_newpwd = new OracleParameter("ic_newPwd", OracleDbType.Varchar2, 50);
			op_newpwd.Direction = ParameterDirection.Input;
			op_newpwd.Value = newpwd;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_Modify_Pwd",
				new OracleParameter[] { op_uc001, op_newpwd });
		}


		/// <summary>
		/// 判断指定收费记录 是否有退费记录
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static bool HaveRefund(string fa001)
		{
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			int result = int.Parse(SqlAssist.ExecuteScalar("select pkg_business.fun_HaveRefund(:ic_fa001) from dual", new OracleParameter[] { op_fa001 }).ToString());
			return result >0 ? true:false;
		}


		/// <summary>
		/// 退费结算
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="itemId_arry"></param>
		/// <param name="itemType_arry"></param>
		/// <param name="price_arry"></param>
		/// <param name="nums_arry"></param>
		/// <param name="handler"></param>
		/// <param name="memo"></param>
		/// <param name="ofa001"></param>
		/// <returns></returns>
		public static int FinRefundSettle(string fa001,  string[] itemId_arry, string[] itemType_arry, decimal[] price_arry, decimal[] nums_arry,
			string handler, string memo,string ofa001)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
 
			//销售项目编号数组
			OracleParameter op_itemId_arry = new OracleParameter("ic_itemId_arry", OracleDbType.Varchar2);
			op_itemId_arry.Direction = ParameterDirection.Input;
			op_itemId_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_itemId_arry.Value = itemId_arry;

			//销售项目类型数组
			OracleParameter op_itemType_arry = new OracleParameter("ic_itemType_arry", OracleDbType.Varchar2);
			op_itemType_arry.Direction = ParameterDirection.Input;
			op_itemType_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_itemType_arry.Value = itemType_arry;

			//销售项目单价数组
			OracleParameter op_price_arry = new OracleParameter("in_price_arry", OracleDbType.Decimal);
			op_price_arry.Direction = ParameterDirection.Input;
			op_price_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_price_arry.Value = price_arry;

			//销售项目数量数组
			OracleParameter op_nums_arry = new OracleParameter("in_nums_arry", OracleDbType.Decimal);
			op_nums_arry.Direction = ParameterDirection.Input;
			op_nums_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_nums_arry.Value = nums_arry;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			//备注
			OracleParameter op_memo = new OracleParameter("ic_memo", OracleDbType.Varchar2, 80);
			op_memo.Direction = ParameterDirection.Input;
			op_memo.Value = memo;

			//原结算流水号
			OracleParameter op_ofa001= new OracleParameter("ic_ofa001", OracleDbType.Varchar2, 10);
			op_ofa001.Direction = ParameterDirection.Input;
			op_ofa001.Value = ofa001;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_RefundSettle_Fin",
				new OracleParameter[] { op_fa001, op_itemId_arry, op_itemType_arry, op_price_arry, op_nums_arry, op_handler, op_memo,op_ofa001 });
		}

		/// <summary>
		/// 税务项目退费结算
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="itemId_arry"></param>
		/// <param name="itemType_arry"></param>
		/// <param name="price_arry"></param>
		/// <param name="nums_arry"></param>
		/// <param name="handler"></param>
		/// <param name="memo"></param>
		/// <param name="ofa001"></param>
		/// <returns></returns>
		public static int TaxRefundSettle(string fa001, string[] itemId_arry, string[] itemType_arry, decimal[] price_arry, decimal[] nums_arry,
			string handler, string memo, string ofa001)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//销售项目编号数组
			OracleParameter op_itemId_arry = new OracleParameter("ic_itemId_arry", OracleDbType.Varchar2);
			op_itemId_arry.Direction = ParameterDirection.Input;
			op_itemId_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_itemId_arry.Value = itemId_arry;

			//销售项目类型数组
			OracleParameter op_itemType_arry = new OracleParameter("ic_itemType_arry", OracleDbType.Varchar2);
			op_itemType_arry.Direction = ParameterDirection.Input;
			op_itemType_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_itemType_arry.Value = itemType_arry;

			//销售项目单价数组
			OracleParameter op_price_arry = new OracleParameter("in_price_arry", OracleDbType.Decimal);
			op_price_arry.Direction = ParameterDirection.Input;
			op_price_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_price_arry.Value = price_arry;

			//销售项目数量数组
			OracleParameter op_nums_arry = new OracleParameter("in_nums_arry", OracleDbType.Decimal);
			op_nums_arry.Direction = ParameterDirection.Input;
			op_nums_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_nums_arry.Value = nums_arry;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			//备注
			OracleParameter op_memo = new OracleParameter("ic_memo", OracleDbType.Varchar2, 80);
			op_memo.Direction = ParameterDirection.Input;
			op_memo.Value = memo;

			//原结算流水号
			OracleParameter op_ofa001 = new OracleParameter("ic_ofa001", OracleDbType.Varchar2, 10);
			op_ofa001.Direction = ParameterDirection.Input;
			op_ofa001.Value = ofa001;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_RefundSettle_Tax",
				new OracleParameter[] { op_fa001, op_itemId_arry, op_itemType_arry, op_price_arry, op_nums_arry, op_handler, op_memo, op_ofa001 });
		}

		/// <summary>
		/// 财务类别统计
		/// </summary>
		/// <param name="dbegin"></param>
		/// <param name="dend"></param>
		/// <param name="class_arry"></param>
		/// <returns></returns>
		public static int ClassStat(string dbegin, string dend, string[] class_arry)
		{
			OracleCommand cmd = new OracleCommand("pkg_report.prc_ClassStat", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			OracleTransaction trans = null;

			//统计日期1
			OracleParameter op_begin = new OracleParameter("ic_begin", OracleDbType.Varchar2, 16);
			op_begin.Direction = ParameterDirection.Input;
			op_begin.Value = dbegin;
			//统计日期2
			OracleParameter op_end = new OracleParameter("ic_end", OracleDbType.Varchar2, 16);
			op_end.Direction = ParameterDirection.Input;
			op_end.Value = dend;

			//销售记录编号数组
			OracleParameter op_class_arry = new OracleParameter("ic_class", OracleDbType.Varchar2);
			op_class_arry.Direction = ParameterDirection.Input;
			op_class_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_class_arry.Value = class_arry;
 
			OracleParameter appcode = new OracleParameter("on_appcode", OracleDbType.Int16);
			appcode.Direction = ParameterDirection.Output;
			OracleParameter apperror = new OracleParameter("oc_error", OracleDbType.Varchar2, 100);
			apperror.Direction = ParameterDirection.Output;

			try
			{
				trans = SqlAssist.conn.BeginTransaction();
				cmd.Parameters.AddRange(new OracleParameter[] { op_begin, op_end, op_class_arry, appcode, apperror });
				cmd.ExecuteNonQuery();

				if (int.Parse(appcode.Value.ToString()) < 0)
				{
					trans.Rollback();
					XtraMessageBox.Show(apperror.Value.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}

				trans.Commit();
				return 1;
			}
			catch (InvalidOperationException e)
			{
				trans.Rollback();
				XtraMessageBox.Show("执行过程错误!\n" + e.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
			finally
			{
				cmd.Dispose();
			}
		}

		/// <summary>
		/// 返回 分类统计笔数
		/// </summary>
		/// <returns></returns>
		public static int GetClassStat_BS()
		{
			return Convert.ToInt32(SqlAssist.ExecuteScalar("select pkg_report.fun_GetClassStat_BS from dual"));
		}


		/// <summary>
		/// 收款员收款统计
		/// </summary>
		/// <param name="dbegin"></param>
		/// <param name="dend"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int CasherStat(string dbegin, string dend, string handler)
		{
			//开始日期
			OracleParameter op_dbegin = new OracleParameter("ic_begin", OracleDbType.Varchar2, 20);
			op_dbegin.Direction = ParameterDirection.Input;
			op_dbegin.Value = dbegin;

			//结束日期
			OracleParameter op_dend = new OracleParameter("ic_end", OracleDbType.Varchar2, 20);
			op_dend.Direction = ParameterDirection.Input;
			op_dend.Value = dend;

			//收款人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_report.prc_CasherStat",
				new OracleParameter[] { op_dbegin, op_dend, op_handler });
		}

		/// <summary>
		/// 工作站收款统计
		/// </summary>
		/// <param name="ws001"></param>
		/// <param name="dbegin"></param>
		/// <param name="dend"></param>
		/// <returns></returns>
		public static int WorkStationStat(string ws001, string dbegin, string dend)
		{
			//工作站编号
			OracleParameter op_ws001 = new OracleParameter("ic_ws001", OracleDbType.Varchar2, 10);
			op_ws001.Direction = ParameterDirection.Input;
			op_ws001.Value = ws001;

			//开始日期
			OracleParameter op_dbegin = new OracleParameter("ic_begin", OracleDbType.Varchar2, 20);
			op_dbegin.Direction = ParameterDirection.Input;
			op_dbegin.Value = dbegin;

			//结束日期
			OracleParameter op_dend = new OracleParameter("ic_end", OracleDbType.Varchar2, 20);
			op_dend.Direction = ParameterDirection.Input;
			op_dend.Value = dend;

			return SqlAssist.ExecuteProcedure("pkg_report.prc_workStationStat",
				new OracleParameter[] { op_ws001, op_dbegin, op_dend });
		}
	}
}
