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
using Oracle.ManagedDataAccess.Client;
using White.Forms;
using DevExpress.XtraPrinting;
using White.Misc;
using White.Action;
using White.DataSet;

namespace White.BusinessObject
{
    public partial class Report_Checkout : BaseBusiness
    {
        private DataTable dt_out = new DataTable();
        private OracleDataAdapter outAdapter =
            new OracleDataAdapter("select * from v_Checkout where (to_char(ac015,'yyyy-mm-dd') between :begin and :end) and ac003 like :ac003 and ac007_2 like :ac007", SqlAssist.conn);

        OracleParameter op_begin = null;
        OracleParameter op_end = null;
        OracleParameter op_ac003 = null;
        OracleParameter op_ac007 = null;

        public Report_Checkout()
        {
            InitializeComponent();
        }

        private void Report_Checkout_Load(object sender, EventArgs e)
        {
            //this.DisplayCondition();
            op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
            op_begin.Direction = ParameterDirection.Input;

            op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
            op_end.Direction = ParameterDirection.Input;

            op_ac003 = new OracleParameter("ac003", OracleDbType.Varchar2, 20);
            op_ac003.Direction = ParameterDirection.Input;

            op_ac007 = new OracleParameter("ac007", OracleDbType.Varchar2, 50);
            op_ac007.Direction = ParameterDirection.Input;

            outAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end, op_ac003, op_ac007 });
            gridControl1.DataSource = dt_out;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayCondition();
        }

		/// <summary>
		/// 显示查询条件窗口
		/// </summary>
		private void DisplayCondition()
		{
			Frm_Report_Checkout frm_out = new Frm_Report_Checkout();			 
			if (frm_out.ShowDialog() == DialogResult.OK)
			{
				string s_begin = string.Empty;
				string s_end = string.Empty;
				string s_ac003 = string.Empty;
				string s_ac007 = string.Empty;

				if (frm_out.swapdata["dbegin"] == null || frm_out.swapdata["dbegin"] is System.DBNull)
				{
					s_begin = "1900/01/01";
				}
				else
				{
					s_begin = Convert.ToDateTime(frm_out.swapdata["dbegin"]).ToString("yyyy/MM/dd");
				}

				if (frm_out.swapdata["dend"] == null || frm_out.swapdata["dend"] is System.DBNull)
				{
					s_end = "9999/12/31";
				}
				else
				{
					s_end = Convert.ToDateTime(frm_out.swapdata["dend"]).ToString("yyyy/MM/dd");
				}

				if (frm_out.swapdata["AC003"] == null || string.IsNullOrEmpty(frm_out.swapdata["AC003"].ToString()))
				{
					s_ac003 = "%";
				}
				else
				{
					s_ac003 = frm_out.swapdata["AC003"].ToString() + "%";
				}

				if (frm_out.swapdata["AC007"].ToString() == "全部")
					s_ac007 = "%";
				else
					s_ac007 = frm_out.swapdata["AC007"].ToString();

				op_begin.Value = s_begin;
				op_end.Value = s_end;
				op_ac003.Value = s_ac003;
				op_ac007.Value = s_ac007;

				this.Cursor = Cursors.WaitCursor;
				gridView1.BeginUpdate();
				dt_out.Rows.Clear();
				outAdapter.Fill(dt_out);
				gridView1.EndUpdate();
				this.Cursor = Cursors.Arrow;
			}
			frm_out.Dispose();
		}

		/// <summary>
		/// 刷新数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.RefreshData();
		}

		/// <summary>
		/// 刷新数据过程
		/// </summary>
		private void RefreshData()
		{
			this.Cursor = Cursors.WaitCursor;
			gridView1.BeginUpdate();
			dt_out.Rows.Clear();
			outAdapter.Fill(dt_out);
			gridView1.EndUpdate();
			this.Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// 导出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.Title = "导出Excel";
			fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";

			DialogResult dialogResult = fileDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
				options.TextExportMode = TextExportMode.Text;//设置导出模式为文本
				gridControl1.ExportToXlsx(fileDialog.FileName, options);
				XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// 查找
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) =>
			//显示查找对话框
			gridView1.ShowFindPanel();

		/// <summary>
		/// 已办业务查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle < 0) return;

			string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();

			Envior.mform.openBusinessObject("FireBusiness", s_ac001);
		}

		/// <summary>
		/// 补打火化证明
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle < 0) return;

			string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
			if (XtraMessageBox.Show("现在打印【火化证明】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
			{
				PrtServAction.Print_HHZM(s_ac001,Envior.mform.Handle.ToInt32());
			}
		}

		/// <summary>
		/// 绘制行号
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
		/// 修改
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle < 0) return;

			Checkin_ds checkin_ds = new Checkin_ds();

			Frm_fireCheckin frm_edit = new Frm_fireCheckin();
			frm_edit.swapdata["AC001"] = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
			//frm_edit.swapdata["BusinessObject"] = this;
			frm_edit.swapdata["dataset"] = checkin_ds;
			frm_edit.swapdata["action"] = "edit";
			

			if (frm_edit.ShowDialog() == DialogResult.OK)
			{
				this.RefreshData();
			}
			frm_edit.Dispose();
		}
	}
}
