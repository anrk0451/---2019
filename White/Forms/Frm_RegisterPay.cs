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
using White.Domain;

namespace White.Forms
{
    public partial class Frm_RegisterPay : BaseDialog
    {
        private string rc001 = string.Empty;                //逝者编号
        private decimal bitprice = decimal.Zero;            //号位单价 

		private decimal oldprice = decimal.Zero;            //上次寄存费价格

		private decimal old_regfee = decimal.Zero;
		private decimal new_regfee = decimal.Zero;

		private int oldnums = 0;
		private int newnums = 0;
		private decimal fpfee = decimal.Zero;

		private DataTable dt_rc04 = new DataTable("RC04");  //缴费记录
		private DataTable dt_fpsource = new DataTable();    //寄存附品
		private DataTable dt_sa01 = new DataTable();


        private OracleDataAdapter rc04Adapter = new OracleDataAdapter("", SqlAssist.conn);
		private OracleDataAdapter fpAdapter = new OracleDataAdapter("select * from V_ALLVALIDITEM where item_type in ('12','13') ", SqlAssist.conn);
		private OracleDataAdapter sa01Adapter = new OracleDataAdapter("select * from sa01 where 1<0", SqlAssist.conn);
        public Frm_RegisterPay()
        {
            InitializeComponent();
        }

        private void Frm_RegisterPay_Load(object sender, EventArgs e)
        {
			string s_rc130 = string.Empty;

			rc001 = this.swapdata["RC001"].ToString();

			OracleDataReader reader = SqlAssist.ExecuteReader("select * from rc01 where rc001='" + rc001 + "'");
			while (reader.Read())
			{
				txtEdit_rc001.Text = rc001;
				txtEdit_rc109.EditValue = reader["RC109"];
				txtEdit_rc003.EditValue = reader["RC003"];
				txtEdit_rc303.EditValue = reader["RC303"];
				txtEdit_rc004.EditValue = reader["RC004"];
				txtEdit_rc404.EditValue = reader["RC404"];
				rg_rc002.EditValue = reader["RC002"];
				rg_rc202.EditValue = reader["RC202"];
				be_position.Text = RegisterAction.GetRegPathName(rc001);

				s_rc130 = reader["RC130"].ToString();
				bitprice = Convert.ToDecimal(SqlAssist.ExecuteScalar("select bi009 from bi01 where bi001='" + s_rc130 + "'", null));
				txtedit_price.EditValue = bitprice;

				//如果最后缴费日期 大于 2018-01-01 则无需混合价格
				if(string.Compare(Convert.ToDateTime(reader["RC150"]).ToString("yyyyMMdd"), "20180101") >0 )
				{
					checkEdit1.Enabled = false;
				}
				else
				{
					oldprice = RegisterAction.GetLastRegPrice(rc001);
				}

			}
			reader.Dispose();

			rc04Adapter.SelectCommand.CommandText = "select * from v_rc04 where rc001='" + rc001 + "' order by rc020";
			rc04Adapter.Fill(dt_rc04);
			gridControl1.DataSource = dt_rc04;

			fpAdapter.Fill(dt_fpsource);			
			lookup_sa004.DataSource = dt_fpsource;
			lookup_sa004.DisplayMember = "ITEM_TEXT";
			lookup_sa004.ValueMember = "ITEM_ID";

			sa01Adapter.Fill(dt_sa01);
			gridControl2.DataSource = dt_sa01;

			//comboBox1.Text = "";
			this.Calc_Hj(12);
 
		}

		private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if (e.Column.FieldName == "RC031")  //缴费类型
			{
				if (e.Value.ToString() == "1")
					e.DisplayText = "正常";
				else if (e.Value.ToString() == "0")
				{
					e.DisplayText = "原始登记";
				}
			}
			else if(e.Column.FieldName == "LAMP") //送灯
			{
				if (e.Value.ToString() == "1")
					e.DisplayText = "有";
				else
					e.DisplayText = "无";
			}
		}

		/// <summary>
		/// 缴费年限变更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBox1_TextChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(comboBox1.Text)) return;
			int nums = int.Parse(comboBox1.Text);
			if (nums > 0 && bitprice > 0)
			{
				//txtedit_regfee.EditValue = nums * bitprice;
				this.Calc_Hj(nums);
			}
			else
				this.Calc_Hj(0);
		}

		/// <summary>
		/// 缴费年限校验
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBox1_Validating(object sender, CancelEventArgs e)
		{
			decimal nums;
			if (!decimal.TryParse(comboBox1.Text, out nums))
			{
				e.Cancel = true;
				XtraMessageBox.Show("请输入正确的缴费期限!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
		
			//if (nums - Math.Truncate(nums) > 0 && nums - Math.Truncate(nums) != new decimal(0.5))
			//{
			//	e.Cancel = true;
			//	XtraMessageBox.Show("缴费年限只能是整年或半年!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//	return;
			//}
		}

		/// <summary>
		/// 计算 寄存费
		/// </summary>
		private void Calc_Hj(int nums)
		{
			if(nums == 0)
			{
				new_regfee = 0;
				old_regfee = 0;
				newnums = 0;
				oldnums = 0;
			}					 
			else
			{
				if (!checkEdit1.Checked)  //使用新价格 
				{
					if (nums % 12 == 0)
						new_regfee = nums / 12 * bitprice;
					else if (nums % 6 == 0)
						new_regfee = Math.Round((nums / 6) * (bitprice / 2), 0);
					else
						new_regfee = Math.Round(bitprice / 12 * nums, 0);

					old_regfee = 0;
					oldnums = 0;
					newnums = nums;
				}
				else  
				{
					int testValue = Convert.ToInt32(SqlAssist.ExecuteScalar("select add_months(rc150," + nums.ToString() + ") - to_date('20171231','yyyymmdd') from rc01 where rc001 ='" + rc001 + "'"));
					if (testValue < 0)  //全部使用老价格!!!
					{
						if (nums % 12 == 0)
							old_regfee = nums / 12 * oldprice;
						else if (nums % 6 == 0)
							old_regfee = Math.Round((nums / 6) * (oldprice / 2), 0);
						else
							old_regfee = Math.Round(oldprice / 12 * nums, 0);

						new_regfee = 0;
						newnums = 0;
						oldnums = nums;
					}
					else               //使用混合价格   
					{
						oldnums = Convert.ToInt32(SqlAssist.ExecuteScalar("select ceil((to_date('20171231','yyyymmdd') - trunc(rc150))/30)  from rc01 where rc001='" + rc001 + "'"));
						newnums = nums - oldnums;

						if (oldnums % 12 == 0)
							old_regfee = oldnums / 12 * oldprice;
						else if (oldnums % 6 == 0)
							old_regfee = Math.Round((oldnums / 6) * (oldprice / 2), 0);
						else
							old_regfee = Math.Round(oldprice / 12 * oldnums, 0);

						if (newnums % 12 == 0)
							new_regfee = newnums / 12 * bitprice;
						else if (newnums % 6 == 0)
							new_regfee = Math.Round((newnums / 6) * (bitprice / 2), 0);
						else
							new_regfee = Math.Round(bitprice / 12 * newnums, 0);
					}
				} 
			} 
			txtedit_regfee.EditValue = new_regfee + old_regfee;
			lc_hj.Text = string.Format("{0:C2}", new_regfee + old_regfee + fpfee);
		}

		/// <summary>
		/// 缴费 过程
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void b_ok_Click(object sender, EventArgs e)
		{
			decimal nums;
			decimal dec_tax_sum = decimal.Zero;
			if (!decimal.TryParse(comboBox1.Text, out nums))
			{
				XtraMessageBox.Show("请输入正确的缴费期限!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!(bitprice > 0))
			{
				XtraMessageBox.Show("参数传递错误!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string cuname = txtEdit_rc003.Text;
			string fa001 = Tools.GetEntityPK("FA01");
			int re = 0;


			List<string> itemId_List = new List<string>();
			List<decimal> itemPrice_List = new List<decimal>();
			List<int> itemNums_List = new List<int>();

			if (fpfee > 0)
			{
				foreach (DataRow r in dt_sa01.Rows)
				{
					itemId_List.Add(r["SA004"].ToString());
					itemPrice_List.Add(Convert.ToDecimal(r["PRICE"]));
					itemNums_List.Add(Convert.ToInt32(r["NUMS"]));
					//计算税票项目金额
					if (MiscAction.GetItemInvoiceType(r["SA004"].ToString()) == "T")
					{
						dec_tax_sum += Convert.ToDecimal(r["SA007"]);
					}
				}
			}

			if(dec_tax_sum > 0 && !TaxInvoice.ClientIsOnline())
			{
				if (XtraMessageBox.Show("【税神通】客户端不在线!是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
			}


			if(fpfee>0)
				re = RegisterAction.RegisterPay(rc001, fa001, bitprice, nums, new_regfee + old_regfee,
									itemId_List.ToArray(),itemPrice_List.ToArray(),itemNums_List.ToArray(),Envior.cur_userId);
			else
				re = RegisterAction.RegisterPay(rc001, fa001, bitprice, nums, new_regfee + old_regfee, Envior.cur_userId);


			//if (!checkEdit1.Checked)
			//	re = RegisterAction.RegisterPay(rc001, fa001, bitprice, nums, new_regfee + old_regfee,Envior.cur_userId);
			//else
			//	re = RegisterAction.RegisterPay(rc001, fa001, oldprice, bitprice, oldnums, newnums, old_regfee, new_regfee, Envior.cur_userId);

			if (re > 0)
			{
				dt_rc04.Rows.Clear();
				rc04Adapter.Fill(dt_rc04);

				if (XtraMessageBox.Show("缴费成功!现在打印财政【发票】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					if (FinInvoice.GetCurrentPh() > 0)
					{
						if (XtraMessageBox.Show("下一张财政发票号码:" + Envior.FIN_NEXT_BILL_NO + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							FinInvoice.Invoice(fa001);
						}
					}
				}

				//// 开税票
				if (dec_tax_sum > 0)
				{
					//获取税务客户信息
					Frm_TaxClientInfo frm_taxClient = new Frm_TaxClientInfo(txtEdit_rc003.Text);
					if (frm_taxClient.ShowDialog() == DialogResult.OK)
					{
						TaxClientInfo clientInfo = frm_taxClient.swapdata["taxclientinfo"] as TaxClientInfo;
						if (TaxInvoice.GetNextInvoiceNo(fa001) > 0)
						{
							if (XtraMessageBox.Show("下一张税票代码:" + Envior.NEXT_BILL_CODE + "\r\n" + "票号:" + Envior.NEXT_BILL_NUM + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
							{
								TaxInvoice.Invoice(fa001, clientInfo);
							}
						}
					}
				}


				if (XtraMessageBox.Show("现在打印缴费记录吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					//打印缴费记录
					PrtServAction.PrtRegisterPayRecord(fa001,this.Handle.ToInt32());
				}
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void b_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void checkEdit1_Properties_CheckedChanged(object sender, EventArgs e)
		{
			int nums = 0;
			if(int.TryParse(comboBox1.Text, out nums))
			{
				this.Calc_Hj(nums);
			}
		}

		private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			int rowHandle = gridView2.FocusedRowHandle;
			if (e.Column.FieldName == "SA004" && e.Value != null && e.Value != System.DBNull.Value)
			{
				gridView2.SetRowCellValue(rowHandle, "PRICE", MiscAction.GetItemFixPrice(gridView2.GetRowCellValue(rowHandle, "SA004").ToString()));
				gridView2.SetRowCellValue(rowHandle, "NUMS", 1);
				calcFee(rowHandle);
			}
			else if (e.Column.FieldName == "PRICE" || e.Column.FieldName == "NUMS")
			{
				calcFee(rowHandle);
			}
			else if (e.Column.FieldName == "SA007")
			{
				fpfee = 0;
				for (int i = 0; i < gridView2.RowCount; i++)
				{
					if (i == rowHandle)
					{
						fpfee += Convert.ToDecimal(e.Value);
					}
					else
					{
						if (gridView2.GetRowCellValue(i, "SA007") != null && gridView2.GetRowCellValue(i, "SA007") != System.DBNull.Value)
							fpfee += Convert.ToDecimal(gridView2.GetRowCellValue(i, "SA007"));
					}

				}
				///// 如果是新行
				if (rowHandle < 0)
				{
					fpfee += Convert.ToDecimal(e.Value);
				}

				if(!string.IsNullOrEmpty(comboBox1.Text)) this.Calc_Hj(Convert.ToInt32(comboBox1.Text));
			}
		}


		/// <summary>
		/// 计算附品金额
		/// </summary>
		/// <param name="rowHandle"></param>
		private void calcFee(int rowHandle)
		{
			decimal price;
			if (!(gridView2.GetRowCellValue(rowHandle, "PRICE") is System.DBNull))
				price = Convert.ToDecimal(gridView2.GetRowCellValue(rowHandle, "PRICE"));
			else
				price = 0;

			int nums;
			if (!(gridView2.GetRowCellValue(rowHandle, "NUMS") is System.DBNull))
				nums = Convert.ToInt32(gridView2.GetRowCellValue(rowHandle, "NUMS"));
			else
				nums = 0;

			gridView2.SetRowCellValue(rowHandle, "SA007", price * nums);
		}

		private void gridControl2_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
		{

		}


		private void gridView2_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
		{
			fpfee = 0;
			for (int i = 0; i < gridView2.RowCount; i++)
			{
				if (gridView2.GetRowCellValue(i, "SA007") != null && gridView2.GetRowCellValue(i, "SA007") != System.DBNull.Value)
					fpfee += Convert.ToDecimal(gridView2.GetRowCellValue(i, "SA007"));
			}
			if (!string.IsNullOrEmpty(comboBox1.Text)) this.Calc_Hj(Convert.ToInt32(comboBox1.Text));
		}

		 
	}
}