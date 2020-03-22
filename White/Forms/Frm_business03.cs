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

namespace White.Forms
{
	/// <summary>
	/// 休息室办理
	/// </summary>
	public partial class Frm_business03 : BaseDialog
	{
		DataView dv_xxs;
		FireBusiness_ds business_ds = null;
		string SALESTYPE = string.Empty;

		public Frm_business03()
		{
			InitializeComponent();
		}

		private void Frm_business03_Load(object sender, EventArgs e)
		{
			business_ds = this.swapdata["dataset"] as FireBusiness_ds;
			SALESTYPE = this.swapdata["SALESTYPE"].ToString();
			 
			dv_xxs = new DataView(business_ds.AllItem);
			dv_xxs.RowFilter = "item_type='03' and status = '1' ";
			gridControl1.DataSource = dv_xxs;
		}

		private void b_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void b_ok_Click(object sender, EventArgs e)
		{
			int[] handle_arry = gridView1.GetSelectedRows();
			if (handle_arry.Length == 0)
			{
				MessageBox.Show("请先选择项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			List<string> itemIdList = new List<string>();
			foreach (int r in handle_arry)
			{
				if(gridView1.GetRowCellValue(r,"INVTYPE") is System.DBNull)
				{
					XtraMessageBox.Show("选择的行尚未设置发票类别!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
					return;
				}
				itemIdList.Add(gridView1.GetRowCellValue(r, "ITEM_ID").ToString());
			}

			this.swapdata["xxs"] = itemIdList;
			DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}