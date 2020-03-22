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
	public partial class Frm_business07 : BaseDialog
	{
		DataView dv_lc;
		FireBusiness_ds business_ds = null;
		string AC001 = string.Empty;
		string SALESTYPE = string.Empty;

		public Frm_business07()
		{
			InitializeComponent();
		}

		private void Frm_business07_Load(object sender, EventArgs e)
		{
			business_ds = this.swapdata["dataset"] as FireBusiness_ds;
			
			if(this.swapdata.ContainsKey("AC001"))
				AC001 = this.swapdata["AC001"].ToString();

			SALESTYPE = this.swapdata["SALESTYPE"].ToString();

			dv_lc = new DataView(business_ds.AllItem);
			dv_lc.RowFilter = "item_type='07' and status = '1' ";

			//为下拉列表赋数据源
			glookup_lc.Properties.DataSource = dv_lc;
			glookup_lc.Properties.DisplayMember = "ITEM_TEXT";
			glookup_lc.Properties.ValueMember = "ITEM_ID";
		}

		private void b_ok_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(glookup_lc.EditValue.ToString()))
			{
				glookup_lc.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				glookup_lc.ErrorText = "请先选择灵车!";
				return;
			}

			string s_si001 = glookup_lc.EditValue.ToString();     //灵车编号
 
			if (SALESTYPE == "0")			//火化业务
			{
				int result = FireAction.FireSales_07(AC001,
												  s_si001,
												  Envior.cur_userId
				);
				if (result > 0)
				{
					DialogResult = DialogResult.OK;
					this.Close();
				}
			}else if (SALESTYPE == "1")		//临时性销售
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