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
	public partial class Frm_business04 : BaseDialog
	{
		DataView dv_gbt;
		FireBusiness_ds business_ds = null;
		string AC001 = string.Empty;
		string SALESTYPE = string.Empty;


		public Frm_business04()
		{
			InitializeComponent();
		}


		private void Frm_business04_Load(object sender, EventArgs e)
		{
			business_ds = this.swapdata["dataset"] as FireBusiness_ds;

			if(this.swapdata.ContainsKey("AC001"))
				AC001 = this.swapdata["AC001"].ToString();

			SALESTYPE = this.swapdata["SALESTYPE"].ToString();

			dv_gbt = new DataView(business_ds.AllItem);
			dv_gbt.RowFilter = "item_type='04' and status = '1' ";

			//为下拉列表赋数据源
			glookup_slt.Properties.DataSource = dv_gbt;
			glookup_slt.Properties.DisplayMember = "ITEM_TEXT";
			glookup_slt.Properties.ValueMember = "ITEM_ID";

			dateEdit_so005.EditValue = DateTime.Now;
		}

		private void b_ok_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(glookup_slt.EditValue.ToString()))
			{
				glookup_slt.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				glookup_slt.ErrorText = "请先选择一个告别厅!";
				return;
			}
			if (dateEdit_so005.EditValue == null || string.IsNullOrEmpty(dateEdit_so005.EditValue.ToString()))
			{
				dateEdit_so005.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				dateEdit_so005.ErrorText = "请输入告别时间!";
				return;
			}

			string s_si001 = glookup_slt.EditValue.ToString();     //告别厅编号
			DateTime so005 = (DateTime)dateEdit_so005.EditValue;   //告别日期

			if (String.IsNullOrEmpty(MiscAction.GetItemInvoiceType(s_si001)))
			{
				XtraMessageBox.Show("选择的行尚未设置发票类别!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (SALESTYPE == "0")		//火化业务
			{
				int result = FireAction.FireSales_04(AC001,
												  s_si001,
												  so005,
												  Envior.cur_userId
				);
				if (result > 0)
				{
					DialogResult = DialogResult.OK;
					this.Close();
				}
			}else if (SALESTYPE == "1") //临时性销售
			{
				DialogResult = DialogResult.OK;
				this.swapdata["ITEMID"] = s_si001;
				this.Close();
			}
			
		}

		private void b_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}