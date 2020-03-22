using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using White.BaseObject;
using White.Forms;
using Oracle.ManagedDataAccess.Client;

namespace White.BusinessObject
{
    public partial class Report_FinRollBack : BaseBusiness
    {

		private DataTable dt_finance = new DataTable("FINANCE");
		private DataTable dt_invoice = new DataTable();

		private OracleDataAdapter finAdapter =
			new OracleDataAdapter("select * from v_financeRollback where (to_char(zfrq,'yyyy-mm-dd') between :begin and :end) ", SqlAssist.conn);

		private OracleDataAdapter invAdapter =
			new OracleDataAdapter("select * from v_finrollback_invoices where (to_char(fa200,'yyyy-mm-dd') between :begin and :end) ", SqlAssist.conn);


		private DataTable dt_detail = new DataTable("DETAIL");
		private OracleDataAdapter deAdapter =
			new OracleDataAdapter("select * from v_finremovedetail where sa010 = :sa010", SqlAssist.conn);

		private DataTable dt_detail2 = new DataTable("DETAIL2");
		private OracleDataAdapter deAdapter2 =
			new OracleDataAdapter("select * from v_finremovedetail where sa010 = :sa010 and sa020 = :sa020", SqlAssist.conn);


		OracleParameter op_begin = null;
		OracleParameter op_end = null;
		OracleParameter op_sa010 = null;
		OracleParameter op_sa020 = null;
		OracleParameter op_sa010_2 = null;



		public Report_FinRollBack()
        {
            InitializeComponent();
        }

		private void Report_FinRollBack_Load(object sender, EventArgs e)
		{
			op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
			op_begin.Direction = ParameterDirection.Input;

			op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
			op_end.Direction = ParameterDirection.Input;

			op_sa010 = new OracleParameter("sa010", OracleDbType.Varchar2, 10);
			op_sa010.Direction = ParameterDirection.Input;

			op_sa020 = new OracleParameter("sa020", OracleDbType.Varchar2, 10);
			op_sa020.Direction = ParameterDirection.Input;

			op_sa010_2 = new OracleParameter("sa010", OracleDbType.Varchar2, 10);
			op_sa010_2.Direction = ParameterDirection.Input;


			finAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end });
			invAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end});
			deAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_sa010 });
			deAdapter2.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_sa010_2, op_sa020 });

			gridControl1.DataSource = dt_finance;
			gridControl2.DataSource = dt_detail;
			gridControl3.DataSource = dt_invoice;
			gridControl4.DataSource = dt_detail2;
		}


		/// <summary>
		/// 查询条件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			Frm_duration frm_1 = new Frm_duration();
			frm_1.swapdata["MODE"] = "2";

			if (frm_1.ShowDialog() == DialogResult.OK)
			{
				string s_begin = string.Empty;
				string s_end = string.Empty;

				if (frm_1.swapdata["begin"] == null)
				{
					s_begin = "1900-01-01";
				}
				else
				{
					s_begin = Convert.ToDateTime(frm_1.swapdata["begin"]).ToString("yyyy-MM-dd");
				}

				if (frm_1.swapdata["end"] == null)
				{
					s_end = "9999-12-31";
				}
				else
				{
					s_end = Convert.ToDateTime(frm_1.swapdata["end"]).ToString("yyyy-MM-dd");
				}

				 
				op_begin.Value = s_begin;
				op_end.Value = s_end;
  
				this.Cursor = Cursors.WaitCursor;

				//////1.按收费笔数检索
				gridView1.BeginUpdate();
				dt_finance.Rows.Clear();

				finAdapter.Fill(dt_finance);

				gridCol_Fa004.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridCol_Fa004.SummaryItem.DisplayFormat = "合计 = {0:N2}";

				gridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
				gridColumn5.SummaryItem.DisplayFormat = "共计 = {0:N0}笔";

				gridView1.EndUpdate();


				//////2. 按发票检索
				gridView3.BeginUpdate();
				dt_invoice.Rows.Clear();

				invAdapter.Fill(dt_invoice);

				gridColumn_fee.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn_fee.SummaryItem.DisplayFormat = "合计 = {0:N2}";

				gridColumn_ph.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
				gridColumn_ph.SummaryItem.DisplayFormat = "共计 = {0:N0}张发票";

				gridView3.EndUpdate();

				this.Cursor = Cursors.Arrow;
			}
			frm_1.Dispose();
		}

		/// <summary>
		/// 绘制行编号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{
			e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			if (e.Info.IsRowIndicator)
			{
				if (e.RowHandle >= 0)
				{
					e.Info.DisplayText = (e.RowHandle + 1).ToString();
				}
				else if (e.RowHandle < 0 && e.RowHandle > -1000)
				{
					e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
					e.Info.DisplayText = "G" + e.RowHandle.ToString();
				}
			}
		}

		/// <summary>
		/// 绘制行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView3_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{
			e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			if (e.Info.IsRowIndicator)
			{
				if (e.RowHandle >= 0)
				{
					e.Info.DisplayText = (e.RowHandle + 1).ToString();
				}
				else if (e.RowHandle < 0 && e.RowHandle > -1000)
				{
					e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
					e.Info.DisplayText = "G" + e.RowHandle.ToString();
				}
			}
		}

		/// <summary>
		/// 行焦点改变
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if (e.FocusedRowHandle >= 0)
			{
				this.RetrieveDetail(e.FocusedRowHandle);
			}
		}

		/// <summary>
		/// 检索明细
		/// </summary>
		/// <param name="rowHandle"></param>
		private void RetrieveDetail(int rowHandle)
		{
			if (rowHandle >= 0)
			{
				string s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
				op_sa010.Value = s_fa001;
				gridView2.BeginUpdate();
				dt_detail.Rows.Clear();
				deAdapter.Fill(dt_detail);
				gridView2.EndUpdate();
			}
		}

		/// <summary>
		/// 列文字转换
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if (e.Column.FieldName.ToUpper() == "FA190")
			{
				if (e.Value.ToString() == "00")
					e.DisplayText = "未开票";
				else if (e.Value.ToString() == "01")
					e.DisplayText = "税票";
				else if (e.Value.ToString() == "10")
					e.DisplayText = "财政票";
				else if (e.Value.ToString() == "11")
					e.DisplayText = "财政票+税票";
			}
			else if (e.Column.FieldName.ToUpper() == "FA195")
			{
				if (e.Value.ToString() == "00")
					e.DisplayText = "";
				else if (e.Value.ToString() == "01")
					e.DisplayText = "税票";
				else if (e.Value.ToString() == "10")
					e.DisplayText = "财政票";
				else if (e.Value.ToString() == "11")
					e.DisplayText = "财政票+税票";
			}
		}

		private void gridView3_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if(e.Column.FieldName.ToUpper() == "FLAG")
			{
				if (e.Value.ToString() == "1")
					e.DisplayText = "正常";
				else if (e.Value.ToString() == "0")
					e.DisplayText = "作废";
			}
		}

		private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if (e.FocusedRowHandle >= 0)
			{
				string s_fa001 = gridView3.GetRowCellValue(e.FocusedRowHandle, "FA001").ToString();
				string s_sa020 = gridView3.GetRowCellValue(e.FocusedRowHandle, "BILLTYPE").ToString();

				op_sa010_2.Value = s_fa001;
				op_sa020.Value = s_sa020;

				gridView3.BeginUpdate();
				dt_detail2.Rows.Clear();
				deAdapter2.Fill(dt_detail2);
				gridView3.EndUpdate();
			}
		}

		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (tabPane1.SelectedPageIndex == 0)
			{
				if (!gridView1.IsFindPanelVisible)
					gridView1.ShowFindPanel();
				else
					gridView1.HideFindPanel();
			}
			else if (tabPane1.SelectedPageIndex == 1)
			{
				if (!gridView3.IsFindPanelVisible)
					gridView3.ShowFindPanel();
				else
					gridView3.HideFindPanel();
			}
		}
	}
}
