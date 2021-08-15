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
using White.Action;
using System.Net;
using System.IO;
using Oracle.ManagedDataAccess.Client;

namespace White.Forms
{
	public partial class Frm_fireCheckin : BaseDialog
	{
		Checkin_ds checkin_ds = null;
		Ac01_dao ac01_dao = new Ac01_dao();
		Ic01_dao ic01_dao = new Ic01_dao();

		Ac01 ac01 = null;
		Ic01 ic01 = null;

		string action = string.Empty;
		string AC001 = string.Empty;

		BaseBusiness businessObject = null;
		private bool IDC_FLAG = false;

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
				be_url.Enabled = false;

				//读入照片		
				if (MiscAction.HasIDC(AC001))
				{
					OracleDataReader photo_reader = SqlAssist.ExecuteReader("select ic020 from ic01 where ic000 = '0' and ac001 ='" + AC001 + "'");
					if (photo_reader.HasRows && photo_reader.Read())
					{
						MemoryStream ms = new MemoryStream((byte[])photo_reader["IC020"]);//把照片读到MemoryStream里  
						Image imageBlob = Image.FromStream(ms, true);//用流创建Image  
						pictureEdit1.Image = imageBlob;//输出图片   
					}
					photo_reader.Dispose();
				}

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

			if (ic01 != null) ic01.ac001 = ac01.ac001;

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
					if (IDC_FLAG) ic01_dao.Insert(ic01);
				}
				else
				{
					ac01_dao.Update(ac01);
					if (IDC_FLAG) ic01_dao.Update(ic01);
				}

				///更新身份证照片
				if (IDC_FLAG && ic01 != null)
				{
					FileStream file = new FileStream("zp.bmp", FileMode.Open, FileAccess.Read);
					Byte[] imgByte = new Byte[file.Length];//把图片转成 Byte型 二进制流
					file.Read(imgByte, 0, imgByte.Length);//把二进制流读入缓冲区
					file.Close();
					MiscAction.Update_IDC_Photo(ic01.ic001, imgByte);
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
 
		/// <summary>
		/// 读取身份证
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtedit_ac014_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (!Envior.IDC_Reader_State)
			{
				XtraMessageBox.Show("身份证读卡器未连接!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}
			if (action == "edit" && MiscAction.HasIDC(AC001))
			{
				if (XtraMessageBox.Show("是否替换已有的身份证信息?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
			}
			try
			{
				int authenticate = CVRSDK.CVR_Authenticate();
				if (authenticate == 1)
				{
					int readContent = CVRSDK.CVR_Read_Content(4);
					if (readContent == 1)
					{
						FillData();
						dateEdit_ac010.Focus();
					}
					else
					{
						XtraMessageBox.Show("读卡失败!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				else
				{
					XtraMessageBox.Show("未放卡或卡片放置不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				XtraMessageBox.Show(ex.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void FillData()
		{
			try
			{
				int length;

				IDC_FLAG = true;

				// 照片保存在当前目录
				String szXPPath = "zp.bmp";
				System.Drawing.Image img = System.Drawing.Image.FromFile(szXPPath);
				System.Drawing.Image bmp = new System.Drawing.Bitmap(img);
				img.Dispose();
				pictureEdit1.Image = bmp;


				byte[] name = new byte[128];
				length = 128;
				CVRSDK.GetPeopleName(ref name[0], ref length);

				byte[] cnName = new byte[128];
				length = 128;
				CVRSDK.GetPeopleChineseName(ref cnName[0], ref length);

				byte[] number = new byte[128];
				length = 128;
				CVRSDK.GetPeopleIDCode(ref number[0], ref length);

				byte[] peopleNation = new byte[128];
				length = 128;
				CVRSDK.GetPeopleNation(ref peopleNation[0], ref length);

				byte[] peopleNationCode = new byte[128];
				length = 128;
				CVRSDK.GetNationCode(ref peopleNationCode[0], ref length);

				byte[] validtermOfStart = new byte[128];
				length = 128;
				CVRSDK.GetStartDate(ref validtermOfStart[0], ref length);

				byte[] birthday = new byte[128];
				length = 128;
				CVRSDK.GetPeopleBirthday(ref birthday[0], ref length);

				byte[] address = new byte[128];
				length = 128;
				CVRSDK.GetPeopleAddress(ref address[0], ref length);

				byte[] validtermOfEnd = new byte[128];
				length = 128;
				CVRSDK.GetEndDate(ref validtermOfEnd[0], ref length);

				byte[] signdate = new byte[128];
				length = 128;
				CVRSDK.GetDepartment(ref signdate[0], ref length);

				byte[] sex = new byte[128];
				length = 128;
				CVRSDK.GetPeopleSex(ref sex[0], ref length);

				byte[] Uid = new byte[128];
				length = 128;

				//CVRSDK.GetIDCardUID(ref Uid[0], 128);

				bool bCivic = true;
				byte[] certType = new byte[32];
				length = 32;
				CVRSDK.GetCertType(ref certType[0], ref length);

				string strType = System.Text.Encoding.ASCII.GetString(certType);
				int nStart = strType.IndexOf("I");
				if (nStart != -1) bCivic = false;

				if (ic01 == null)
				{
					ic01 = new Ic01();
					ic01.ic001 = Tools.GetEntityPK("IC01");
				}

				ic01.ic000 = "0";  //0-逝者 1-家属
				ic01.ic003 = System.Text.Encoding.GetEncoding("GB2312").GetString(name).Trim();    //姓名
				ic01.ic002 = System.Text.Encoding.GetEncoding("GB2312").GetString(sex).Replace("\0", "").Trim() == "男" ? "0" : "1";

				//出生日期
				string s_birth = System.Text.Encoding.GetEncoding("GB2312").GetString(birthday).Replace("\0", "").Trim();
				ic01.ic004 = Convert.ToDateTime(s_birth.Substring(0, 4) + "-" + s_birth.Substring(4, 2) + "-" + s_birth.Substring(6));

				//身份证号
				ic01.ic014 = System.Text.Encoding.GetEncoding("GB2312").GetString(number).Replace("\0", "").Trim();

				//地址
				ic01.ic016 = System.Text.Encoding.GetEncoding("GB2312").GetString(address).Replace("\0", "").Trim();

				//签发机关
				ic01.ic017 = System.Text.Encoding.GetEncoding("GB2312").GetString(signdate).Replace("\0", "").Trim();

				//有效期限
				ic01.ic018 = System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfStart).Replace("\0", "").Trim() + "-" + System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfEnd).Replace("\0", "").Trim();

				txtEdit_ac003.EditValue = ic01.ic003.Trim();
				rg_ac002.EditValue = ic01.ic002;
 
				txtEdit_ac004.EditValue = MiscAction.Calc_Age_Via_Birth(ic01.ic004.ToString("yyyy-MM-dd"));
				txtEdit_ac008.EditValue = ic01.ic016.Trim();
				txtedit_ac014.EditValue = ic01.ic014.Trim();

			}
			catch (Exception ex)
			{
				XtraMessageBox.Show(ex.ToString(), "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 读取死亡证明
		/// </summary>
		private void ReadDieCert()
		{
			string s_url = be_url.Text;
			if (string.IsNullOrEmpty(s_url)) return;

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(s_url);
			request.Method = "GET";
			request.ContentType = "application/x-www-form-urlencoded";

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream myResponseStream = response.GetResponseStream();
			StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
			string retString = myStreamReader.ReadToEnd();
			myStreamReader.Close();
			myResponseStream.Close();

			int p1, p2;
			p1 = retString.IndexOf("号：");
			p2 = retString.IndexOf("</td>", p1);

			string s_die_id = retString.Substring(p1 + 2, p2 - p1 - 2);  //死亡证明编号
			 
			//姓名
			p1 = retString.IndexOf("名：", p2);
			p2 = retString.IndexOf("</td>", p1);
			string s_ac003 = retString.Substring(p1 + 2, p2 - p1 - 2);

			//性别
			p1 = retString.IndexOf("别：", p2);
			p2 = retString.IndexOf("</td>", p1);
			string s_ac002 = retString.Substring(p1 + 2, p2 - p1 - 2) == "男" ? "0" : "1";

			//年龄
			p1 = retString.IndexOf("龄：", p2);
			p2 = retString.IndexOf("岁", p1);
			int i_ac004 = int.Parse(retString.Substring(p1 + 2, p2 - p1 - 2));

			//死亡原因
			p1 = retString.IndexOf("死亡原因：", p2);
			p2 = retString.IndexOf("</td>", p1);
			string s_reason = retString.Substring(p1 + 5, p2 - p1 - 5);

			//常驻地址
			p1 = retString.IndexOf("常住地址：", p2);
			p2 = retString.IndexOf("</td>", p1);
			string s_addr = retString.Substring(p1 + 5, p2 - p1 - 5);

			//死亡地点
			p1 = retString.IndexOf("死亡地点：", p2);
			p2 = retString.IndexOf("</td>", p1);
			string s_dieaddr = retString.Substring(p1 + 5, p2 - p1 - 5);

			//出生日期
			p1 = retString.IndexOf("出生日期：", p2);
			p2 = retString.IndexOf("</td>", p1);
			DateTime d_birth = Convert.ToDateTime(retString.Substring(p1 + 5, p2 - p1 - 5));

			//死亡时间
			p1 = retString.IndexOf("死亡日期：", p2);
			p2 = retString.IndexOf("</td>", p1);
			DateTime d_dietime = Convert.ToDateTime(retString.Substring(p1 + 5, p2 - p1 - 5));

			//身份证号码
			p1 = retString.IndexOf("身份证号码：", p2);
			p2 = retString.IndexOf("</td>", p1);
			string s_idc = retString.Substring(p1 + 6, p2 - p1 - 6);

			dateEdit_ac010.EditValue = d_dietime;  //死亡时间
			lookUp_ac005.EditValue = s_reason;     //死亡原因
			txtEdit_ac009.EditValue = s_dieaddr;   //死亡地点


			if (IDC_FLAG)
			{
				if (!string.IsNullOrEmpty(txtedit_ac014.Text))
				{
					if(txtedit_ac014.Text != s_idc)
					{
						XtraMessageBox.Show("【死亡证明】身份证号不一致!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
						return;
					}	 
				}
				else
				{
					txtedit_ac014.Text = s_idc;
				}

				if (!string.IsNullOrEmpty(txtEdit_ac003.Text))
				{
					if (txtEdit_ac003.Text != s_ac003)
					{
						XtraMessageBox.Show("【死亡证明】逝者姓名不一致!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
				else
				{
					txtEdit_ac003.Text = s_idc;
				}

				if(rg_ac002.EditValue.ToString() != s_ac002)
				{
					XtraMessageBox.Show("【死亡证明】逝者性别不一致!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				if (!string.IsNullOrEmpty(txtEdit_ac004.Text))
				{
					if ( int.Parse(txtEdit_ac004.Text) != i_ac004)
					{
						XtraMessageBox.Show("【死亡证明】逝者年龄不一致!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
				else
					txtEdit_ac004.EditValue = i_ac004;
				 
			}
			else
			{
				txtEdit_ac003.Text = s_ac003;          //逝者姓名
				rg_ac002.EditValue = s_ac002;          //性别
				txtEdit_ac004.EditValue = i_ac004;     //年龄
				txtedit_ac014.EditValue = s_idc;       //身份证号				
				txtEdit_ac008.EditValue = s_addr;	   //地址	
			}

		}

		private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			be_url.Focus();
		}

		private void be_url_Validating(object sender, CancelEventArgs e)
		{
			this.ReadDieCert();
		}
	}
}