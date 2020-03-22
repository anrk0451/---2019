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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;

namespace White.Forms
{
	public partial class Frm_layerPrice : BaseDialog
	{
		DataTable mytable;
		public Frm_layerPrice()
		{
			InitializeComponent();
		}

		private void Frm_layerPrice_Load(object sender, EventArgs e)
		{
			mytable = this.swapdata["table"] as DataTable;
			string regionId = this.swapdata["regionId"].ToString();
			gridControl1.DataSource = mytable;
			gridView1.ActiveFilter.Add(gridView1.Columns["RG001"], new ColumnFilterInfo("[RG001] = '" + regionId + "'", ""));

			for (int i = 1; i <= gridView1.Columns.Count; i++)
			{
				if (gridView1.Columns[i - 1].FieldName != "LY002" && gridView1.Columns[i - 1].FieldName != "PRICE")
				{
					gridView1.Columns[i - 1].Visible = false;
				}
				else if (gridView1.Columns[i - 1].FieldName == "LY002")
				{
					gridView1.Columns[i - 1].Caption = "层号";
					gridView1.Columns[i - 1].Width = 45;
					gridView1.Columns[i - 1].OptionsColumn.AllowFocus = false;
					gridView1.Columns[i - 1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
					gridView1.Columns[i - 1].OptionsColumn.AllowEdit = false;  //不可编辑
					gridView1.Columns[i - 1].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
				}
				else if (gridView1.Columns[i - 1].FieldName == "PRICE")
				{
					gridView1.Columns[i - 1].Caption = "定价";
					gridView1.Columns[i - 1].Width = 55;
					RepositoryItemTextEdit priceEditor = new RepositoryItemTextEdit();
					priceEditor.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
					priceEditor.Mask.EditMask = "N2";
					priceEditor.Mask.UseMaskAsDisplayFormat = true;

					gridView1.Columns[i - 1].ColumnEdit = priceEditor;
				}
			}
		}

		private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
		{
			GridColumn col = gridView1.FocusedColumn;
			int row = gridView1.FocusedRowHandle;
			if (col.FieldName == "PRICE" && e.Value != null)
			{
				if (decimal.Parse(e.Value.ToString()) < 0)
				{
					e.Valid = false;
					e.ErrorText = "价格不能小于0!";
				}
			}
		}

		/// <summary>
		/// 复制价格
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void simpleButton3_Click(object sender, EventArgs e)
		{
			int row = gridView1.FocusedRowHandle;
			if (string.IsNullOrEmpty(gridView1.GetRowCellValue(row, "PRICE").ToString())) return;

			decimal price = decimal.Parse(gridView1.GetRowCellValue(row, "PRICE").ToString());
			for (int i = 1; i <= gridView1.RowCount; i++)
			{
				gridView1.SetRowCellValue(i - 1, "PRICE", price);
			}
		}

		/// <summary>
		/// 确定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void simpleButton1_Click(object sender, EventArgs e)
		{
			if (!gridView1.PostEditor()) return;
			if (!gridView1.UpdateCurrentRow()) return;
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void simpleButton2_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}