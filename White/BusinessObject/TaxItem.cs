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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;

namespace White.BusinessObject
{
	public partial class TaxItem : BaseBusiness
	{
		DataTable dt_ti01 = new DataTable("TI01");
		DataColumn col_ti001 = new DataColumn("TI001", typeof(string));   // 税票项目ID
		DataColumn col_ti002 = new DataColumn("TI002", typeof(string));   // 税票项目分类编码 
		DataColumn col_ti003 = new DataColumn("TI003", typeof(string));   // 税票项目名称
		DataColumn col_ti004 = new DataColumn("TI004", typeof(decimal));  // 税率
		DataColumn col_status = new DataColumn("STATUS", typeof(string)); // 状态
		 
		OracleDataAdapter ti01Adapter = new OracleDataAdapter("select * from ti01 where status = '1' ", SqlAssist.conn);
		OracleCommandBuilder builder = null; 

		public TaxItem()
		{
			InitializeComponent();

			dt_ti01.Columns.AddRange(new DataColumn[] { col_ti001,col_ti002,col_ti003,col_ti004,col_status});
			dt_ti01.PrimaryKey = new DataColumn[] { col_ti001 };                //设置主键

			gridControl1.DataSource = dt_ti01;
			builder = new OracleCommandBuilder(ti01Adapter);
		}

		private void TaxItem_Load(object sender, EventArgs e)
		{
			ti01Adapter.Fill(dt_ti01);
			//设置自动过滤(过滤掉删除行:此操作应该在数据集装入数据后)
			gridView1.ActiveFilter.Clear();
			gridView1.ActiveFilterString = "STATUS <> '0'";
		}

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			gridView1.AddNewRow();
			int rowno = gridView1.FocusedRowHandle;
			/////// 设置焦点 开始编辑 !!!
			gridView1.FocusedColumn = gridView1.Columns["TI002"];
			gridView1.ShowEditor();
		}

		/// <summary>
		/// 初始化新行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
		{
			//// 初始化新行时触发(当在新行中)
			GridView view = sender as GridView;
			string ti001 = Tools.GetEntityPK("TI01");
			gridView1.SetRowCellValue(e.RowHandle, "TI001", ti001);
			gridView1.SetRowCellValue(e.RowHandle, "STATUS", "1");
		 
		}

		/// <summary>
		/// 绘制行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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
		/// 删除	
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (gridView1.FocusedRowHandle >= 0)
			{
				if (MessageBox.Show("确认要删除当前的记录吗", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
				{
					return;
				}

			}
			gridView1.SetFocusedRowCellValue("STATUS", "0");
			gridView1.UpdateCurrentRow();
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (!gridView1.PostEditor()) return;
			if (!gridView1.UpdateCurrentRow()) return;
			if (!saveCheck()) return;
			try
			{
				ti01Adapter.Update(dt_ti01);
				MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			catch (Exception ee)
			{
				MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		private bool saveCheck()
		{
			foreach(DataRow dr in dt_ti01.Rows)
			{
				if (dr["STATUS"].ToString() == "0") continue;
				if(dr["TI002"] is DBNull || dr["TI003"] is DBNull || dr["TI004"] is DBNull)
				{
					XtraMessageBox.Show("数据输入不完整!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					return false;
				} 
			}
			return true;
		}

		/// <summary>
		/// 刷新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			gridView1.BeginUpdate();
			dt_ti01.Rows.Clear();
			ti01Adapter.Fill(dt_ti01);
			gridView1.EndUpdate();
		}

		/// <summary>
		/// 行校验
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			//string value = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TI002").ToString();
			if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TI002") == null)
			{
				e.Valid = false;
				(sender as ColumnView).SetColumnError(gridView1.Columns["TI002"], "税收分类编码不能为空!");
				return;
			}

			//value = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TI003").ToString();
			if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TI003") == null)
			{
				e.Valid = false;
				(sender as ColumnView).SetColumnError(gridView1.Columns["TI003"], "项目名称不能为空!");
				return;
			}
		}

		/// <summary>
		/// 编辑校验
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
		{
			string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
			if (colName.Equals("TI002"))         //税收分类编码
			{
				if (String.IsNullOrEmpty(e.Value.ToString()))
				{
					e.Valid = false;
					e.ErrorText = "税收分类编码不能为空!";
				}
				else
				{
					//for (int i = 0; i < gridView1.RowCount - 1; i++)
					//{
					//	if (i == (sender as ColumnView).FocusedRowHandle) continue;
					//	if (gridView1.GetRowCellValue(i, "TI002") == null) continue;

					//	//如果名字相同,则校验不通过!                        
					//	if (String.Equals(gridView1.GetRowCellValue(i, "TI002").ToString(), e.Value.ToString()))
					//	{
					//		e.Valid = false;
					//		e.ErrorText = "值已经存在!";
					//		break;
					//	}
					//}
				}
			}
			else if (colName.Equals("TI003"))  //项目名称
			{
				if (String.IsNullOrEmpty(e.Value.ToString()))
				{
					e.Valid = false;
					e.ErrorText = "税收项目名称不能为空!";
				}
				else
				{
					//for (int i = 0; i < gridView1.RowCount - 1; i++)
					//{
					//	if (i == (sender as ColumnView).FocusedRowHandle) continue;
					//	if (gridView1.GetRowCellValue(i, "TI003") == null) continue;

					//	//如果名字相同,则校验不通过!                        
					//	if (String.Equals(gridView1.GetRowCellValue(i, "TI003").ToString(), e.Value.ToString()))
					//	{
					//		e.Valid = false;
					//		e.ErrorText = "值已经存在!";
					//		break;
					//	}
					//}
				}
			}
			else if (colName.Equals("TI004"))  //税率
			{
				if(e.Value == null || e.Value is DBNull)
				{
					e.Value = false;
					e.ErrorText = "税率不能为空!";
				}
				else
				{
					if(Convert.ToDecimal(e.Value) >1 || Convert.ToDecimal(e.Value) < 0)
					{
						e.Value = false;
						e.ErrorText = "税率只能在0~1之间!";
					}
				}
			}  
		}
	}
}
