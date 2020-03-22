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
	public partial class Frm_business02 : BaseDialog
	{
		DataView dv_lcg;
		FireBusiness_ds business_ds = null;
		string AC001 = string.Empty;
		string SALESTYPE = string.Empty;

		public Frm_business02()
		{
			InitializeComponent();
		}

		private void Frm_business02_Load(object sender, EventArgs e)
		{
			business_ds = this.swapdata["dataset"] as FireBusiness_ds;

			if(this.swapdata.ContainsKey("AC001"))
				AC001 = this.swapdata["AC001"].ToString();

			SALESTYPE = this.swapdata["SALESTYPE"].ToString();

			dv_lcg = new DataView(business_ds.AllItem);
			dv_lcg.RowFilter = "item_type='02' and status ='1' ";

			//为下拉列表赋数据源
			glookup_lcg.Properties.DataSource = dv_lcg;
			glookup_lcg.Properties.DisplayMember = "ITEM_TEXT";
			glookup_lcg.Properties.ValueMember = "ITEM_ID";

			dateEdit_so005.EditValue = DateTime.Now;
		}

		private void b_ok_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(glookup_lcg.EditValue.ToString()))
			{
				glookup_lcg.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				glookup_lcg.ErrorText = "请先选择一个冷藏柜!";
				return;
			}
			if (dateEdit_so005.EditValue == null || string.IsNullOrEmpty(dateEdit_so005.EditValue.ToString()))
			{
				dateEdit_so005.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				dateEdit_so005.ErrorText = "请输入开始存放时间!";
				return;
			}
			if (string.IsNullOrEmpty(txtedit_nums.Text))
			{
				txtedit_nums.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtedit_nums.ErrorText = "请输入存放天数!";
				return;
			}
			decimal nums = decimal.Parse(txtedit_nums.Text);

			if ((nums - Math.Floor(nums)) > 0 && (nums - Math.Floor(nums)) != new decimal(0.5))
			{
				txtedit_nums.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtedit_nums.ErrorText = "存放天数只能为整数或者半日!";
				return;
			}

			string s_si001 = glookup_lcg.EditValue.ToString();     //冷餐柜编号
			DateTime so005 = (DateTime)dateEdit_so005.EditValue;   //开始存放日期

			if(SALESTYPE == "0")		//火化业务
			{
				int result = FireAction.FireSales_02(AC001,
												  s_si001,
												  nums,
												  so005,
												  Envior.cur_userId
				);
				if (result > 0)
				{
					DialogResult = DialogResult.OK;
					this.Close();
				}
			}else if(SALESTYPE == "1")  //临时销售
			{
				DialogResult = DialogResult.OK;
				this.swapdata["ITEMID"] = s_si001;
				this.swapdata["NUMS"] = nums;
				this.Close();
			}
			
		}

		private void b_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}