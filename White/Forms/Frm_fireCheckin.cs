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
using White.Domain;
using White.Misc;
using White.Dao;

namespace White.Forms
{
	public partial class Frm_fireCheckin : BaseDialog
	{
		Checkin_ds checkin_ds = null;
		Ac01_dao ac01_dao = new Ac01_dao();
		Ac01 ac01 = null;

		string action = string.Empty;
		string AC001 = string.Empty;

		BaseBusiness businessObject = null;


		public Frm_fireCheckin()
		{
			InitializeComponent();
		}


		/// <summary>
		/// 窗口装入事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Frm_fireCheckin_Load(object sender, EventArgs e)
		{
			//获取传入 数据集
			checkin_ds = this.swapdata["dataset"] as Checkin_ds;        
			action = this.swapdata["action"].ToString();

			if (this.swapdata.ContainsKey("businessObject"))
				businessObject = this.swapdata["businessObject"] as BaseBusiness;

			lookUp_ac005.Properties.DataSource = checkin_ds.St01_reason;
			lookUp_ac005.Properties.ValueMember = "ST003";
			lookUp_ac005.Properties.DisplayMember = "ST003";
			checkin_ds.St01_reason.Sort = "SORTID ASC";

			lookUp_ac052.Properties.DataSource = checkin_ds.St01_relation;
			lookUp_ac052.Properties.ValueMember = "ST003";
			lookUp_ac052.Properties.DisplayMember = "ST003";
			checkin_ds.St01_relation.Sort = "SORTID ASC";

			lookUp_ac060.Properties.DataSource = checkin_ds.St01_driver;
			lookUp_ac060.Properties.ValueMember = "ST001";
			lookUp_ac060.Properties.DisplayMember = "ST003";
			checkin_ds.St01_driver.Sort = "SORTID ASC";

			lookUp_ac007.Properties.DataSource = checkin_ds.St01_district;
			lookUp_ac007.Properties.ValueMember = "ST003";
			lookUp_ac007.Properties.DisplayMember = "ST003";
			checkin_ds.St01_district.Sort = "SORTID ASC";

			lookup_gtype.Properties.DataSource = checkin_ds.St01_gtype;
			lookup_gtype.Properties.ValueMember = "ST001";
			lookup_gtype.Properties.DisplayMember = "ST003";
			checkin_ds.St01_gtype.Sort = "SORTID ASC";

			if (string.Equals(action, "edit"))
			{
				this.Text = "登记修改";
				AC001 = this.swapdata["AC001"].ToString();

				ac01 = ac01_dao.GetSingle(s => s.ac001 == AC001);
				if (ac01 == null)
				{
					b_ok.Enabled = false;
					MessageBox.Show("查找数据失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					b_ok.Enabled = false;
					return;
				}

				txtEdit_ac003.EditValue = ac01.ac003;
				rg_ac002.EditValue = ac01.ac002;
				txtEdit_ac004.EditValue = ac01.ac004;
				txtedit_ac014.EditValue = ac01.ac014;
				txtEdit_ac009.EditValue = ac01.ac009;
				dateEdit_ac010.EditValue = ac01.ac010;
				lookUp_ac005.EditValue = ac01.ac005;
				lookUp_ac060.EditValue = ac01.ac060;
				lookUp_ac007.EditValue = ac01.ac007;
				lookup_gtype.EditValue = ac01.ac300;    //逝者类型
				txtEdit_ac008.EditValue = ac01.ac008;

				txtEdit_ac050.EditValue = ac01.ac050;
				txtEdit_ac051.EditValue = ac01.ac051;
				lookUp_ac052.EditValue = ac01.ac052;

				txtEdit_ac150.EditValue = ac01.ac150;
				txtEdit_ac151.EditValue = ac01.ac151;
				lookUp_ac152.EditValue = ac01.ac152;
 
				txtEdit_ac055.EditValue = ac01.ac055;
				mem_ac099.EditValue = ac01.ac099;
			}
			else
			{
				ac01 = new Ac01();
				if(checkin_ds.St01_gtype.Count > 0)
					lookup_gtype.EditValue = checkin_ds.St01_gtype.ToTable().Rows[0]["ST001"];
			}

		}

		/// <summary>
		/// 身份证号校验
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtedit_ac014_Validating(object sender, CancelEventArgs e)
		{
			string s_idcard = txtedit_ac014.Text.Trim();
			if (string.IsNullOrWhiteSpace(s_idcard)) return;

			if (s_idcard.Length != 15 && s_idcard.Length != 18)
			{
				txtedit_ac014.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtedit_ac014.ErrorText = "身份证号位数错误!";
				e.Cancel = true;
			}
			else if (s_idcard.Length == 15)
			{
				if (!Tools.CheckIDCard15(s_idcard))
				{
					txtedit_ac014.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
					txtedit_ac014.ErrorText = "身份证号错误!";
					e.Cancel = true;
				}
			}
			else if (s_idcard.Length == 18)
			{
				if (!Tools.CheckIDCard18(s_idcard))
				{
					txtedit_ac014.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
					txtedit_ac014.ErrorText = "身份证号错误!";
					e.Cancel = true;
				}
			}
		}

		/// <summary>
		/// 年龄校验
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtEdit_ac004_Validating(object sender, CancelEventArgs e)
		{
			string s_ac004 = txtEdit_ac004.Text.Trim();
			if (string.IsNullOrWhiteSpace(s_ac004)) return;

			int i;
			if (int.TryParse(s_ac004, out i))
			{
				if (i < 0)
				{
					txtEdit_ac004.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
					txtEdit_ac004.ErrorText = "年龄不能小于0!";
					e.Cancel = true;
				}
			}
		}

		/// <summary>
		/// 死亡时间校验
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dateEdit_ac010_Validating(object sender, CancelEventArgs e)
		{
			if (dateEdit_ac010.EditValue == null) return;
			if (DateTime.Compare((DateTime)dateEdit_ac010.EditValue, System.DateTime.Now) > 0)
			{
				dateEdit_ac010.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				dateEdit_ac010.ErrorText = "死亡时间不能大于系统当前时间!";
				e.Cancel = true;
			}
		}

		/// <summary>
		/// 保存前检查
		/// </summary>
		/// <returns></returns>
		private bool SaveCheck()
		{
			//逝者姓名
			if (string.IsNullOrWhiteSpace(txtEdit_ac003.Text.Trim()))
			{
				txtEdit_ac003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_ac003.ErrorText = "逝者姓名必须输入!";
				txtEdit_ac003.Focus();
				return false;
			}
			//年龄
			if (string.IsNullOrWhiteSpace(txtEdit_ac004.Text.Trim()))
			{
				txtEdit_ac004.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_ac004.ErrorText = "年龄必须输入!";
				txtEdit_ac004.Focus();
				return false;
			}

			//身份证号
			//if (string.IsNullOrWhiteSpace(txtedit_ac014.Text.Trim()))
			//{
			//	txtedit_ac014.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
			//	txtedit_ac014.ErrorText = "身份证号必须输入!";
			//	txtedit_ac014.Focus();
			//	return false;
			//}

			//死亡原因
			if (lookUp_ac005.EditValue == null)
			{
				lookUp_ac005.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				lookUp_ac005.ErrorText = "死亡原因必须输入!";
				lookUp_ac005.Focus();
				return false;
			}
			//逝者户籍
			if (lookUp_ac007.EditValue == null)
			{
				lookUp_ac007.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				lookUp_ac007.ErrorText = "逝者户籍必须输入!";
				lookUp_ac007.Focus();
				return false;
			}
			//联系人
			if (string.IsNullOrWhiteSpace(txtEdit_ac050.Text))
			{
				txtEdit_ac050.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_ac050.ErrorText = "联系人必须输入!";
				txtEdit_ac050.Focus();
				return false;
			}
			//与逝者关系
			if (string.IsNullOrWhiteSpace(lookUp_ac052.Text))
			{
				lookUp_ac052.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				lookUp_ac052.ErrorText = "与逝者关系必须输入!";
				lookUp_ac052.Focus();
				return false;
			}
			//联系电话
			if (string.IsNullOrWhiteSpace(txtEdit_ac051.Text))
			{
				txtEdit_ac051.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_ac051.ErrorText = "联系人必须输入!";
				txtEdit_ac051.Focus();
				return false;
			}
			if (string.IsNullOrWhiteSpace(lookup_gtype.Text))
			{
				lookup_gtype.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				lookup_gtype.ErrorText = "逝者类型必须输入!";
				lookup_gtype.Focus();
				return false;
			}
			return true;
		}

		/// <summary>
		/// 确定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void b_ok_Click(object sender, EventArgs e)
		{
			if (!SaveCheck()) return;  //数据合法性校验!!!

			if (action.Equals("add"))
			{
				ac01.ac001 = Tools.GetEntityPK("AC01");
			}


			ac01.ac002 = rg_ac002.EditValue.ToString();     //性别
			ac01.ac003 = txtEdit_ac003.Text;                //逝者姓名
			ac01.ac004 = int.Parse(txtEdit_ac004.Text);     //年龄
			ac01.ac005 = lookUp_ac005.EditValue.ToString(); //死亡原因
			ac01.ac014 = txtedit_ac014.Text;                //身份证号
			ac01.ac007 = lookUp_ac007.EditValue.ToString(); //籍贯-所属区县
			ac01.ac008 = txtEdit_ac008.Text;                //籍贯-详细地址


			if (dateEdit_ac010.EditValue != null)
				ac01.ac010 = DateTime.Parse(dateEdit_ac010.EditValue.ToString());  //死亡时间


			ac01.ac009 = txtEdit_ac009.Text;                //接灵地址
			ac01.ac050 = txtEdit_ac050.Text;                //联系人
			ac01.ac051 = txtEdit_ac051.Text;                //联系电话

			if (!(lookUp_ac052.EditValue == null || lookUp_ac052.EditValue is System.DBNull))
			{
				ac01.ac052 = lookUp_ac052.EditValue.ToString(); //与逝者关系
			}

			ac01.ac150 = txtEdit_ac150.Text;                //联系人
			ac01.ac151 = txtEdit_ac151.Text;                //联系电话

			if (!(lookUp_ac152.EditValue == null || lookUp_ac152.EditValue is System.DBNull))
			{
				ac01.ac152 = lookUp_ac152.EditValue.ToString(); //与逝者关系
			}


			ac01.ac055 = txtEdit_ac055.Text;                //联系地址

			if (lookUp_ac060.EditValue != null)
				ac01.ac060 = lookUp_ac060.EditValue.ToString(); //灵车司机


			if (action.Equals("add"))
			{
				ac01.ac100 = Envior.cur_userId;                 //经办人
				ac01.ac200 = DateTime.Now;                      //经办日期
			}
				 
			ac01.ac110 = Envior.cur_userId;                 //最后经办人
			ac01.ac220 = DateTime.Now;                      //最后经办日期
			ac01.ac099 = mem_ac099.Text;                    //备注
			ac01.status = "1";                              //当前状态 
			ac01.ac300 = lookup_gtype.EditValue.ToString();
			try
			{
				string s_tip = "保存成功!";
				if (action.Equals("add"))
				{
					ac01_dao.Insert(ac01);
				}
				else
				{
					ac01_dao.Update(ac01);
				}

				XtraMessageBox.Show(s_tip, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

				//TODO 1.登记完成立即办理业务！

				if (businessObject != null)
					businessObject.swapdata["AC001"] = ac01.ac001;

				this.DialogResult = DialogResult.OK;
				this.Close();

				if (action.Equals("add") && XtraMessageBox.Show("现在办理业务吗?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
				{
					Envior.mform.openBusinessObject("FireBusiness", ac01.ac001);
				}
			}
			catch (Exception ee)
			{
				XtraMessageBox.Show("保存数据失败!\n" + ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void lookUp_ac007_TextChanged(object sender, EventArgs e)
		{
			//XtraMessageBox.Show(lookUp_ac007.Text);
		}

		private void buttonEdit1_EditValueChanged(object sender, EventArgs e)
		{
			XtraMessageBox.Show("changed");
		}
	}
}