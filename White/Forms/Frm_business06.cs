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
using White.Action;
using White.Misc;

namespace White.Forms
{
	public partial class Frm_business06 : BaseDialog
	{
		DataView dv_hh;
		FireBusiness_ds business_ds = null;
		string AC001 = string.Empty;


		public Frm_business06()
		{
			InitializeComponent();
		}

		private void Frm_business06_Load(object sender, EventArgs e)
		{
			business_ds = this.swapdata["dataset"] as FireBusiness_ds;
			AC001 = this.swapdata["AC001"].ToString();

			dv_hh = new DataView(business_ds.AllItem);
			dv_hh.RowFilter = "item_type='06' and status = '1'";

			//为下拉列表赋数据源
			glookup_hh.Properties.DataSource = dv_hh;
			glookup_hh.Properties.DisplayMember = "ITEM_TEXT";
			glookup_hh.Properties.ValueMember = "ITEM_ID";

			dateEdit_so005.EditValue = DateTime.Now;
		}

		private void b_ok_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(glookup_hh.EditValue.ToString()))
			{
				glookup_hh.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				glookup_hh.ErrorText = "请先选择火化标准!";
				return;
			}
			if (dateEdit_so005.EditValue == null || string.IsNullOrEmpty(dateEdit_so005.EditValue.ToString()))
			{
				dateEdit_so005.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				dateEdit_so005.ErrorText = "请输入火化时间!";
				return;
			}

			string s_si001 = glookup_hh.EditValue.ToString();      //火化标准编号
			DateTime so005 = (DateTime)dateEdit_so005.EditValue;   //火化时间

			int result = FireAction.FireSales_06(AC001,
												  s_si001,
												  so005,
												  Envior.cur_userId
				);
			if (result > 0)
			{
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void b_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}