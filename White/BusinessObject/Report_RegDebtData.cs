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

namespace White.BusinessObject
{
	public partial class Report_RegDebtData : BaseBusiness
	{
		private DataTable dt_source = new DataTable();
		private OracleDataAdapter dtAdapter = new OracleDataAdapter("select * from v_reg_debt where diff between :begin and :end", SqlAssist.conn);
		private OracleParameter op_begin = null;
		private OracleParameter op_end = null;

		public Report_RegDebtData()
		{
			InitializeComponent();
		}

		private void Report_RegDebtData_Load(object sender, EventArgs e)
		{
			op_begin = new OracleParameter("begin", OracleDbType.Int32);
			op_begin.Direction = ParameterDirection.Input;

			op_end = new OracleParameter("end", OracleDbType.Int32);
			op_end.Direction = ParameterDirection.Input;
			dtAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end });

			gridControl1.DataSource = dt_source;
		}
		/// <summary>
		/// 查询条件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_report_regdebt frm_1 = new Frm_report_regdebt();
			if (frm_1.ShowDialog() == DialogResult.OK)
			{
				switch (frm_1.swapdata["type"].ToString())
				{
					case "全部":
						op_begin.Value = 0;
						op_end.Value = 9999;
						break;
					case "一年之内":
						op_begin.Value = 0;
						op_end.Value = 12;
						break;
					case "三年之内":
						op_begin.Value = 0;
						op_end.Value = 36;
						break;
					case "三年以上":
						op_begin.Value = 36;
						op_end.Value = 9999;
						break;
				}

				this.RefreshData();
			}
			frm_1.Dispose();
		}

		/// <summary>
		/// 刷新数据
		/// </summary>
		private void RefreshData()
		{
			this.Cursor = Cursors.WaitCursor;
			gridView1.BeginUpdate();
			dt_source.Rows.Clear();
			dtAdapter.Fill(dt_source);
			gridView1.EndUpdate();
			this.Cursor = Cursors.Arrow;
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
	}
}
