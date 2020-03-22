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
	public partial class FinItem : BaseBusiness
	{
		DataTable dt_in01 = new DataTable("IN01");
		DataColumn col_in001 = new DataColumn("IN001", typeof(string));   // 财政发票项目ID
		DataColumn col_in002 = new DataColumn("IN002", typeof(string));   // 项目代码 
		DataColumn col_in003 = new DataColumn("IN003", typeof(string));   // 财政发票项目名
		DataColumn col_status = new DataColumn("STATUS", typeof(string)); // 状态

		OracleDataAdapter in01Adapter = new OracleDataAdapter("select * from in01 where status = '1' ", SqlAssist.conn);
		OracleCommandBuilder builder = null;


		public FinItem()
		{
			InitializeComponent();
			dt_in01.Columns.AddRange(new DataColumn[] { col_in001, col_in002, col_in003,col_status });
			dt_in01.PrimaryKey = new DataColumn[] { col_in001 };                //设置主键

			gridControl1.DataSource = dt_in01;
			builder = new OracleCommandBuilder(in01Adapter);
		}

		private void FinItem_Load(object sender, EventArgs e)
		{
			in01Adapter.Fill(dt_in01);
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
			gridView1.FocusedColumn = gridView1.Columns["IN002"];
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
			string in001 = Tools.GetEntityPK("IN01");
			gridView1.SetRowCellValue(e.RowHandle, "IN001", in001);
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
			if (gridView1.FocusedRowHandle < 4 && gridView1.FocusedRowHandle >=0)
			{
				XtraMessageBox.Show("内置项目,不能删除!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}
			else if (gridView1.FocusedRowHandle >= 0)
			{
				if (XtraMessageBox.Show("确认要删除当前的记录吗", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
				{
					return;
				}

			}
			gridView1.SetFocusedRowCellValue("STATUS", "0");
			gridView1.UpdateCurrentRow();
		}

		/// <summary>
		/// 保存前检查
		/// </summary>
		/// <returns></returns>
		private bool saveCheck()
		{
			foreach (DataRow dr in dt_in01.Rows)
			{
				if (dr["STATUS"].ToString() == "0") continue;
				if (dr["IN002"] is DBNull || dr["IN003"] is DBNull )
				{
					XtraMessageBox.Show("数据输入不完整!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
			}
			return true;
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
				in01Adapter.Update(dt_in01);
				MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			catch (Exception ee)
			{
				MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		/// <summary>
		/// 刷新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			gridView1.BeginUpdate();
			dt_in01.Rows.Clear();
			in01Adapter.Fill(dt_in01);
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
			if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IN002") == null)
			{
				e.Valid = false;
				(sender as ColumnView).SetColumnError(gridView1.Columns["IN002"], "财政发票代码不能为空!");
				return;
			}

			//value = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TI003").ToString();
			if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IN003") == null)
			{
				e.Valid = false;
				(sender as ColumnView).SetColumnError(gridView1.Columns["IN003"], "项目名称不能为空!");
				return;
			}
		}

		private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
		{
			string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
			if (colName.Equals("IN002"))													//财政发票编码
			{
				if (String.IsNullOrEmpty(e.Value.ToString()))
				{
					e.Valid = false;
					e.ErrorText = "财政项目编码不能为空!";
				}
				else
				{
					for (int i = 0; i < gridView1.RowCount - 1; i++)
					{
						if (i == (sender as ColumnView).FocusedRowHandle) continue;
						if (gridView1.GetRowCellValue(i, "IN002") == null) continue;

						//如果名字相同,则校验不通过!                        
						if (String.Equals(gridView1.GetRowCellValue(i, "IN002").ToString(), e.Value.ToString()))
						{
							e.Valid = false;
							e.ErrorText = "值已经存在!";
							break;
						}
					}
				}
			}
			else if (colName.Equals("IN003"))												//项目名称
			{
				if (String.IsNullOrEmpty(e.Value.ToString()))
				{
					e.Valid = false;
					e.ErrorText = "税收项目名称不能为空!";
				}
				else
				{
					for (int i = 0; i < gridView1.RowCount - 1; i++)
					{
						if (i == (sender as ColumnView).FocusedRowHandle) continue;
						if (gridView1.GetRowCellValue(i, "IN003") == null) continue;

						//如果名字相同,则校验不通过!                        
						if (String.Equals(gridView1.GetRowCellValue(i, "IN003").ToString(), e.Value.ToString()))
						{
							e.Valid = false;
							e.ErrorText = "值已经存在!";
							break;
						}
					}
				}
			}			 
		}

		/// <summary>
		/// 控制是否允许编辑
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
		{
			if (gridView1.FocusedRowHandle < 4 && gridView1.FocusedRowHandle >=0)
				e.Cancel = true;
		}
	}
}
