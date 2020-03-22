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
using Oracle.ManagedDataAccess.Client;
using White.Action;
using White.Misc;

namespace White.Forms
{
    public partial class Frm_registerOut : BaseDialog
    {
        private string rc001 = string.Empty;
        private decimal price = decimal.Zero;  //寄存单价
		private decimal regfee = decimal.Zero; //补退费金额
        private bool isrefund = false;         //是否退费

		private int compare = 0;

        public Frm_registerOut()
        {
            InitializeComponent();
        }

		private void Frm_registerOut_Load(object sender, EventArgs e)
		{
			rc001 = this.swapdata["RC001"].ToString();
			OracleDataReader reader = SqlAssist.ExecuteReader("select * from rc01 where rc001='" + rc001 + "'");
			while (reader.Read())
			{
				txtEdit_rc001.Text = rc001;
				txtEdit_rc109.EditValue = reader["RC109"];		//寄存证号
				txtEdit_rc003.EditValue = reader["RC003"];		//逝者姓名
				txtEdit_rc303.EditValue = reader["RC303"];		//逝者姓名2
				rg_rc002.EditValue = reader["RC002"];			//性别
				rg_rc202.EditValue = reader["RC202"];			//性别2
				txtEdit_rc004.EditValue = reader["RC004"];		//年龄
				txtEdit_rc404.EditValue = reader["RC404"];
				txtEdit_rc150.EditValue = reader["RC150"];		//寄存到期日期
				be_position.EditValue = RegisterAction.GetRegPathName(rc001);

				//price = Math.Round(  RegisterAction.GetBitPrice(reader["RC130"].ToString()) / 12, 0);
				price = RegisterAction.GetBitPrice(reader["RC130"].ToString());
				txtEdit_price.EditValue = price;

				//比较到期日期 （判断是否应该补或退）
				compare = string.Compare(Convert.ToDateTime(reader["RC150"]).ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"));

				//设置补退信息
				this.SetBTInfo();
			}


			//TODO 5. 根据权限设置 是否允许补退费
			checkEdit1.Enabled = true;

		}

		/// <summary>
		/// 设置补退费信息
		/// </summary>
		private void SetBTInfo()
		{
			int diff = RegisterAction.CalcOutDiffDays(rc001);
			
			if (compare == 0)
			{
				checkEdit1.Enabled = false;
				txtEdit_nums.Enabled = false;
			}
			else if (compare > 0 && checkEdit1.Checked)  //退费
			{
				lc_1.Text = "剩余天数";
				lc_2.Text = "应退费月数";
				lc_3.Text = "退费金额";
				isrefund = true;

				txtEdit_nums.EditValue = Math.Ceiling((diff * 1.0f) / 30);
				this.Calc_je(Convert.ToInt32(Math.Ceiling((diff * 1.0f) / 30)));
			}
			else if(compare < 0 && checkEdit1.Checked)
			{
				lc_1.Text = "过期天数";
				lc_2.Text = "应补费月数";
				lc_3.Text = "补费金额";
				isrefund = false;

				txtEdit_nums.EditValue = Math.Ceiling((diff * 1.0f) / 30);
				this.Calc_je(Convert.ToInt32(Math.Ceiling((diff * 1.0f) / 30)));
			}
			else
			{
				lc_1.Text = "过期天数";
				lc_2.Text = "应补费月数";
				lc_3.Text = "补费金额";

				regfee = 0;
				txtEdit_fee.EditValue = regfee;
			}

			txtEdit_diff.EditValue = diff;

		}


		/// <summary>
		/// 是否补退费 开关
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkEdit1_CheckedChanged(object sender, EventArgs e)
		{
			txtEdit_nums.Enabled = checkEdit1.Checked;
			if (!checkEdit1.Checked)
			{
				txtEdit_nums.Text = "0.00";
			}

			this.SetBTInfo();
		}

		/// <summary>
		/// 补退费年限校验
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtEdit_nums_Validating(object sender, CancelEventArgs e)
		{
			if (!string.IsNullOrEmpty(txtEdit_nums.Text))
			{
				if (Convert.ToDecimal(txtEdit_nums.Text) < 0)
				{
					txtEdit_nums.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
					txtEdit_nums.ErrorText = "应为正值!";
					e.Cancel = true;
				}
			}
		}

		/// <summary>
		/// 补退费年限变更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtEdit_nums_EditValueChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtEdit_nums.Text))
			{
				int nums = Convert.ToInt32(txtEdit_nums.Text);
				//txtEdit_fee.EditValue = Math.Round(price * nums);
				this.Calc_je(nums);
			}
		}

		private void Calc_je(int nums)
		{
			if (nums % 12 == 0)
				regfee = (nums / 12) * price;
			else if (nums % 6 == 0)
				regfee = Math.Round((nums / 6) * (price / 2), 0);
			else
				regfee = Math.Round(nums * (price / 12), 0);

			txtEdit_fee.EditValue = regfee;
		}


		private void b_ok_Click(object sender, EventArgs e)
		{
			if (rc001 == null)
			{
				XtraMessageBox.Show("数据传递错误!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (txtEdit_oc003.EditValue == null || txtEdit_oc003.EditValue is System.DBNull)
			{
				txtEdit_oc003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_oc003.ErrorText = "请输入迁出办理人!";
				return;
			}
			if (mem_oc005.EditValue == null)
			{
				mem_oc005.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				mem_oc005.ErrorText = "请输入迁出原因!";
				return;
			}
			string s_oc003 = txtEdit_oc003.Text;   //迁出人
			string s_oc005 = mem_oc005.Text;       //迁出原因
			string s_oc004 = txtEdit_oc004.Text;   //迁出人身份证号

			int diff = int.Parse(txtEdit_diff.EditValue.ToString());
			decimal nums = decimal.Zero;
			string fa001 = Tools.GetEntityPK("FA01");
			string last_fa001 = RegisterAction.GetREGLastSettleId(rc001);     //获取最后一次缴费 结算流水号

			//补退情况
			if (checkEdit1.Checked)
			{
				nums = decimal.Parse(txtEdit_nums.Text);
			}
			else
			{
				nums = 0;
			}

			if (XtraMessageBox.Show("确认要继续办理迁出吗？本业务将不能回退!", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

			if ((!string.IsNullOrEmpty(txtEdit_fee.Text)) && Convert.ToDecimal(txtEdit_fee.Text) > 0 && Envior.cur_userId != AppInfo.ROOTID && !isrefund )
			{
				XtraMessageBox.Show("当前记录已经欠费,不能迁出!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}



			int re = RegisterAction.RegisterOut(rc001,
												 s_oc003,
												 s_oc004,
												 s_oc005,
												 diff,
												 fa001,
												 price,
												 isrefund ? 0 - nums : nums,
												 isrefund ? 0- Math.Abs(regfee) : Math.Abs(regfee),
												 Envior.cur_userId
				);
			if (re > 0)
			{
				XtraMessageBox.Show("迁出办理成功!现在打印【迁出通知单】", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				PrtServAction.PrtRegisterOutNotice(rc001,this.Handle.ToInt32());

				if (!isrefund && nums  > 0)
				{
					if (XtraMessageBox.Show("现在开具【发票】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{
						if (!Envior.FIN_READY)
							XtraMessageBox.Show("未连接到博思开票服务器!请稍后补开!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						else
						{
							string s_pjh = FinInvoice.GetCurrentPh(Envior.FIN_INVOICE_TYPE);
							if (String.IsNullOrEmpty(s_pjh))
								XtraMessageBox.Show("未获取到下一张财政发票号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							else
							{
								if (XtraMessageBox.Show("下一张财政发票号码:" + s_pjh + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
								{
									FinInvoice.Invoice(fa001);
								}
							}
						}
					}
				}
				else if(isrefund && Math.Abs(nums) > 0 )    //退费发票
				{
					string s_old_pjlx = string.Empty;
					string s_old_pjh = string.Empty;
					string s_old_zch = string.Empty;
					OracleDataReader reader_log = SqlAssist.ExecuteReader("select * from fin_log where settleId ='" + last_fa001 +"'");
					reader_log.Read();
					if (reader_log.HasRows)
					{
						s_old_pjlx = reader_log["INVOICEKIND"].ToString();    //票据类型
						s_old_pjh = reader_log["INVOICENO"].ToString();       //票据号
						s_old_zch = reader_log["INVOICEZCH"].ToString();      //注册号
					}
					else 
					{
						XtraMessageBox.Show("读取缴费发票信息出错!","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
					reader_log.Dispose();
					Frm_refundInfo frm_refund = new Frm_refundInfo(s_old_pjlx,s_old_pjh,s_old_zch);
					if(frm_refund.ShowDialog() == DialogResult.OK)
					{
						s_old_zch = frm_refund.swapdata["zch"].ToString(); //注册号
						if (!Envior.FIN_READY)
							XtraMessageBox.Show("未连接到博思开票服务器!请稍后补开!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						else
						{
							string s_newpjh = FinInvoice.GetCurrentPh(Envior.FIN_INVOICE_TYPE);
							if (String.IsNullOrEmpty(s_newpjh))
								XtraMessageBox.Show("未获取到下一张财政发票号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							else
							{
								if (XtraMessageBox.Show("下一张财政发票号码:" + s_newpjh + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
								{
									string s_tkitem = MiscAction.GetItemInvoiceCode("08", "") + "	" + Math.Abs(nums * price) + "	";
									FinInvoice.Refund(s_old_pjlx, s_old_pjh, s_old_zch, s_tkitem, "F_Qt1=xxx|F_Qt2=xxx|F_Qt3=xxx",fa001, s_newpjh,nums * price);
								}
							}
						}
					}
					frm_refund.Dispose();
				}
			}
			DialogResult = DialogResult.OK;
			this.Close();

		}

		private void sb_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}