using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using White.BaseObject;
using White.DataSet;
using White.BusinessObject;
using DevExpress.XtraGrid.Views.Base;
using White.Action;

namespace White.Forms
{
	public partial class Frm_businessMisc : BaseDialog
	{
		//BaseBusiness bo = null;
		FireBusiness_ds business_ds = null;
		string SALESTYPE = string.Empty;

		public Frm_businessMisc()
		{
			InitializeComponent();
		}

		private void Frm_businessMisc_Load(object sender, EventArgs e)
		{
			//if (this.swapdata["businessObject"] is FireBusiness)
			//	bo = this.swapdata["businessObject"] as FireBusiness;
			//else if (this.swapdata["businessObject"] is TempSales)
			//	bo = this.swapdata["businessObject"] as TempSales;

			business_ds = this.swapdata["dataset"] as FireBusiness_ds;

			gridControl1.DataSource = business_ds.AllItem;
			gridControl2.DataSource = business_ds.AllItem;
			gridControl3.DataSource = business_ds.AllItem;
			gridControl4.DataSource = business_ds.AllItem;

			gridView1.ActiveFilterString = "item_type = '05' and status = '1' ";
			gridView2.ActiveFilterString = "item_type = '10' and status = '1' ";
			gridView3.ActiveFilterString = "item_type = '11' and status = '1' ";
			gridView4.ActiveFilterString = "(item_type = '12' or item_type = '13' ) and status = '1' ";
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
		/// 绘制行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
		/// 绘制行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView4_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
		/// 设置数量（非绑定列）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
		{
			if (e.Column.FieldName.ToUpper() == "NUMS" && e.IsGetData)
			{
				e.Value = 1;
			}
		}

		/// <summary>
		/// 数量校验
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
		{
			string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
			if (colName == "NUMS")
			{
				if (e.Value == null || e.Value is System.DBNull)
				{
					e.Valid = false;
					e.ErrorText = "请输入数量!";
					return;
				}
				else if (int.Parse(e.Value.ToString()) <= 0)
				{
					e.Valid = false;
					e.ErrorText = "数量必须大于0！";
					return;
				}
			}else if(colName == "PRICE")
			{
				if (decimal.Parse(e.Value.ToString()) <= 0)
				{
					e.Valid = false;
					e.ErrorText = "价格必须大于0！";
					return;
				}
			}
		}

		/// <summary>
		/// 谷类只能选择一个(放开，可以多选)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView2_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
		{
			//if (e.Action == CollectionChangeAction.Add && gridView2.SelectedRowsCount > 1)
			//{
			//	int row = e.ControllerRow;
			//	gridView2.BeginUpdate();
			//	gridView2.ClearSelection();
			//	gridView2.SelectRow(row);
			//	gridView2.EndUpdate();
			//}
		}

		/// <summary>
		/// 纸类只能选择一个
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView3_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Add && gridView3.SelectedRowsCount > 1)
			{
				int row = e.ControllerRow;
				gridView3.BeginUpdate();
				gridView3.ClearSelection();
				gridView3.SelectRow(row);
				gridView3.EndUpdate();
			}
		}

		private void gridView4_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
		{
			string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
			if (colName == "NUMS")
			{
				if (e.Value == null || e.Value is System.DBNull)
				{
					e.Valid = false;
					e.ErrorText = "请输入数量!";
					return;
				}
				else if (int.Parse(e.Value.ToString()) <= 0)
				{
					e.Valid = false;
					e.ErrorText = "数量必须大于0！";
					return;
				}
			}
		}

		private void sb_ok_Click(object sender, EventArgs e)
		{
			List<string> itemId_list = new List<string>();
			List<string> itemType_list = new List<string>();
			List<string> itemInvoiceType_list = new List<string>();
			List<decimal> price_list = new List<decimal>();
			List<decimal> nums_list = new List<decimal>();

			if (!gridView1.PostEditor()) return;
			if (!gridView1.UpdateCurrentRow()) return;
			if (!gridView4.PostEditor()) return;
			if (!gridView4.UpdateCurrentRow()) return;

			string s_itemId = string.Empty;
			string s_invoiceType = string.Empty;

			int selectedRowHandle = 0;

			Int32[] selectedRowHandles = gridView1.GetSelectedRows();
			for (int i = 0; i < selectedRowHandles.Length; i++)
			{
				selectedRowHandle = selectedRowHandles[i];
				
				if (selectedRowHandle >= 0)
				{
					s_itemId = gridView1.GetRowCellValue(selectedRowHandle, "ITEM_ID").ToString();
					s_invoiceType = MiscAction.GetItemInvoiceType(s_itemId);
					if (String.IsNullOrEmpty(s_invoiceType))
					{
						XtraMessageBox.Show("第" + selectedRowHandle.ToString()+ "行尚未设置发票类别!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						tabPane1.SelectedPageIndex = 0;
						gridView1.FocusedRowHandle = selectedRowHandle;
						return;
					}

					itemInvoiceType_list.Add(s_invoiceType);
					itemId_list.Add(gridView1.GetRowCellValue(selectedRowHandle, "ITEM_ID").ToString());
					itemType_list.Add(gridView1.GetRowCellValue(selectedRowHandle, "ITEM_TYPE").ToString());

					if (gridView1.GetRowCellValue(selectedRowHandle, "PRICE") is System.DBNull)
						price_list.Add(0);
					else
						price_list.Add(decimal.Parse(gridView1.GetRowCellValue(selectedRowHandle, "PRICE").ToString()));

					nums_list.Add(decimal.Parse(gridView1.GetRowCellValue(selectedRowHandle, "NUMS").ToString()));
				}
			}

			selectedRowHandles = gridView2.GetSelectedRows();
			for (int i = 0; i < selectedRowHandles.Length; i++)
			{
				selectedRowHandle = selectedRowHandles[i];
				if (selectedRowHandle >= 0)
				{
					s_itemId = gridView2.GetRowCellValue(selectedRowHandle, "ITEM_ID").ToString();
					s_invoiceType = MiscAction.GetItemInvoiceType(s_itemId);
					if (String.IsNullOrEmpty(s_invoiceType))
					{
						XtraMessageBox.Show("第" + selectedRowHandle.ToString() + "行尚未设置发票类别!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						tabPane1.SelectedPageIndex = 1;
						gridView2.FocusedRowHandle = selectedRowHandle;
						return;
					}

					itemInvoiceType_list.Add(s_invoiceType);
					itemId_list.Add(gridView2.GetRowCellValue(selectedRowHandle, "ITEM_ID").ToString());
					itemType_list.Add(gridView2.GetRowCellValue(selectedRowHandle, "ITEM_TYPE").ToString());
					price_list.Add(decimal.Parse(gridView2.GetRowCellValue(selectedRowHandle, "PRICE").ToString()));
					nums_list.Add(decimal.Parse(gridView2.GetRowCellValue(selectedRowHandle, "NUMS").ToString()));
				}
			}

			selectedRowHandles = gridView3.GetSelectedRows();
			for (int i = 0; i < selectedRowHandles.Length; i++)
			{
				selectedRowHandle = selectedRowHandles[i];
				if (selectedRowHandle >= 0)
				{
					s_itemId = gridView3.GetRowCellValue(selectedRowHandle, "ITEM_ID").ToString();
					s_invoiceType = MiscAction.GetItemInvoiceType(s_itemId);
					if (String.IsNullOrEmpty(s_invoiceType))
					{
						XtraMessageBox.Show("第" + selectedRowHandle.ToString() + "行尚未设置发票类别!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						tabPane1.SelectedPageIndex = 2;
						gridView3.FocusedRowHandle = selectedRowHandle;
						return;
					}

					itemInvoiceType_list.Add(s_invoiceType);
					itemId_list.Add(gridView3.GetRowCellValue(selectedRowHandle, "ITEM_ID").ToString());
					itemType_list.Add(gridView3.GetRowCellValue(selectedRowHandle, "ITEM_TYPE").ToString());
					price_list.Add(decimal.Parse(gridView3.GetRowCellValue(selectedRowHandle, "PRICE").ToString()));
					nums_list.Add(decimal.Parse(gridView3.GetRowCellValue(selectedRowHandle, "NUMS").ToString()));
				}
			}

			selectedRowHandles = gridView4.GetSelectedRows();
			for (int i = 0; i < selectedRowHandles.Length; i++)
			{
				selectedRowHandle = selectedRowHandles[i];
				if (selectedRowHandle >= 0)
				{
					s_itemId = gridView4.GetRowCellValue(selectedRowHandle, "ITEM_ID").ToString();
					s_invoiceType = MiscAction.GetItemInvoiceType(s_itemId);

					if (String.IsNullOrEmpty(s_invoiceType))
					{
						XtraMessageBox.Show("第" + selectedRowHandle.ToString() + "行尚未设置发票类别!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						tabPane1.SelectedPageIndex = 3;
						gridView4.FocusedRowHandle = selectedRowHandle;
						return;
					}
					itemInvoiceType_list.Add(s_invoiceType);
					itemId_list.Add(gridView4.GetRowCellValue(selectedRowHandle, "ITEM_ID").ToString());
					itemType_list.Add(gridView4.GetRowCellValue(selectedRowHandle, "ITEM_TYPE").ToString());
					price_list.Add(decimal.Parse(gridView4.GetRowCellValue(selectedRowHandle, "PRICE").ToString()));
					nums_list.Add(decimal.Parse(gridView4.GetRowCellValue(selectedRowHandle, "NUMS").ToString()));
				}
			}
			if (itemId_list.Count == 0)
			{
				MessageBox.Show("请选择记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			this.swapdata["itemIdList"] = itemId_list;
			this.swapdata["priceList"] = price_list;
			this.swapdata["numsList"] = nums_list;
			this.swapdata["itemTypeList"] = itemType_list;
			this.swapdata["itemInvoiceTypeList"] = itemInvoiceType_list;
			DialogResult = DialogResult.OK;
			this.Dispose();
		}
	}
}