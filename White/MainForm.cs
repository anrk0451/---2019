using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTab;
using White.BaseObject;
using White.Misc;
using DevExpress.XtraSplashScreen;
using System.Threading;
using White.Forms;
using Oracle.ManagedDataAccess.Client;
using White.Action;
using DevExpress.XtraEditors;
using DevExpress.XtraTab.ViewInfo;
using White.DataSet;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Diagnostics;
//using PrtServ;

namespace White
{
	public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
	{
		[DllImport("user32.dll", EntryPoint = "FindWindow")]
		private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
		[DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
		private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

		//打印服务进程
		Process printprocess = new Process();


		/// <summary>
		/// 业务对象
		/// </summary>
		class Bo01
		{
			public string bo001 { get; set; }   //业务编号

			public string bo003 { get; set; }   //业务名称
			public string bo004 { get; set; }   //业务对象类型 w-窗口 x-xtratabpage对象
		}



		public Dictionary<string, Object> swapdata { get; set; }                                          //交换数据对象

		//追踪已经打开的Tab页
		private Dictionary<string, Bo01> businessTab = new Dictionary<string, Bo01>();
		private Dictionary<string, XtraTabPage> openedTabPage = new Dictionary<string, XtraTabPage>();
		private DataTable dt_bo01 = new DataTable("BO01");											      //存放业务对象表
 
		public MainForm()
		{
			SplashScreenManager.ShowForm(typeof(SplashScreen1));
			Thread.Sleep(2000);
			SplashScreenManager.CloseForm();
			InitializeComponent();
 
			Envior.mform = this;

			swapdata = new Dictionary<string, object>();

			//启动打印服务进程
			printprocess.StartInfo.FileName = "pbnative.exe";
			printprocess.Start();

		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			dt_bo01.Dispose();

			//断开数据库
			SqlAssist.DisConnect();

			//如果连接博思则断开
			if (Envior.FIN_READY) FinInvoice.DisConnect();

			//关闭关联的打印进程
			if (!printprocess.HasExited) printprocess.Kill();

			//关闭打印服务对象
			//if (Envior.prtserv != null)	Envior.prtserv.Dispose();
		}


		private void MainForm_Load(object sender, EventArgs e)
		{
			OracleDataAdapter bo01Adapter = SqlAssist.getSingleTableAdapter("select * from bo01");
			bo01Adapter.Fill(dt_bo01);

			List<Bo01> bo01_rows = ModelHelper.TableToEntity<Bo01>(dt_bo01);
			businessTab = bo01_rows.ToDictionary(key => key.bo001, value => value);

			Frm_login f_login = new Frm_login();
			f_login.ShowDialog();

			if (f_login.DialogResult == DialogResult.OK)  //登录成功处理..........
			{
				

				barStaticItem2.Caption = Envior.cur_userName;
				bs_version.Caption = AppInfo.AppVersion;
				f_login.Dispose();

				//读取发票基础信息
				this.ReadInvoiceBaseInfo();
			}
 
			//连接打印进程
			this.ConnectPrtServ();

			//自动连接博思服务器
			string autoConnect = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath).AppSettings.Settings["ConnectFinInvoice"].Value.ToString();
			if (autoConnect == "1") FinInvoice.AutoConnectBosi();

			//创建打印服务对象
			//Envior.prtserv = new n_prtserv();

			//读取 税务开票 APPID
			Envior.TAX_APPID = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath).AppSettings.Settings["APPID"].Value.ToString();
		}

		/// <summary>
		/// 连接打印服务
		/// </summary>
		private void ConnectPrtServ()
		{
			IntPtr hwnd = FindWindow(null, "prtserv");
			if (hwnd != IntPtr.Zero)
			{
				Envior.prtservHandle = hwnd;
				int prtConnId = int.Parse(SqlAssist.ExecuteScalar("select seq_prtserv.nextval from dual", null).ToString());

				////建立连接
				PrtServAction.Connect(prtConnId, hwnd.ToInt32(), this.Handle.ToInt32());
				Envior.prtConnId = prtConnId;

				////给打印服务窗口发消息 建立连接
				SendMessage(hwnd, 0x2710, 0, prtConnId);
			}
			else
			{
				XtraMessageBox.Show("没有找到打印服务进程,不能打印!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}


		/// <summary>
		/// 读取发票信息
		/// </summary>
		private void ReadInvoiceBaseInfo()
		{
			OracleDataReader reader = SqlAssist.ExecuteReader("select * from sp01 where sp001  < '0000000100' ");
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					if (reader["SP002"].ToString() == "fin_invoice_title")             //发票交款人标题
						Envior.FIN_INVOICE_TITLE = reader["SP005"].ToString();
					else if (reader["SP002"].ToString() == "fin_invoice_type")         //发票类型	
						Envior.FIN_INVOICE_TYPE = reader["SP005"].ToString();
					//税务发票信息
					else if (reader["SP002"].ToString() == "tax_no")                    //纳税人识别号
						Envior.TAX_ID = reader["SP005"].ToString();
					else if (reader["SP002"].ToString() == "tax_addrtele")             //税票-销方地址电话
						Envior.TAX_ADDR_TELE = reader["SP005"].ToString();
					else if (reader["SP002"].ToString() == "tax_bankaccount")          //税票-销方银行账号
						Envior.TAX_BANK_ACCOUNT = reader["SP005"].ToString();
					else if (reader["SP002"].ToString() == "tax_appid")                //税票-应用账号
						Envior.TAX_APPID = reader["SP005"].ToString();
					else if (reader["SP002"].ToString() == "tax_invoicetype")          //税票-发票类型
						Envior.TAX_INVOICE_TYPE = reader["SP005"].ToString();
					else if (reader["SP002"].ToString() == "tax_server_url")           //税票-服务请求URL	
						Envior.TAX_SERVER_URL = reader["SP005"].ToString();
					else if (reader["SP002"].ToString() == "tax_publickey")            //税票-公钥
						Envior.TAX_PUBLIC_KEY = reader["SP005"].ToString();
					else if (reader["SP002"].ToString() == "tax_privatekey")           //税票-私钥
						Envior.TAX_PRIVATE_KEY = reader["SP005"].ToString();

				}
			}
			reader.Dispose();
		}

		 

		/// <summary>
		/// 打开业务对象(如果没有则创建)
		/// </summary>
		public void openBusinessObject(string bo001)
		{
			openBusinessObject(bo001, null);
		}

		/// <summary>
		/// 打开业务对象(如果没有则创建)
		/// </summary>
		public void openBusinessObject(string bo001, object parm)
		{
			if (openedTabPage.ContainsKey(bo001))
			{
				xtraTabControl1.SelectedTabPage = openedTabPage[bo001];
				if (parm != null)
				{
					foreach (Control control in openedTabPage[bo001].Controls)
					{
						if (control is BaseBusiness)
						{
							((BaseBusiness)control).swapdata["parm"] = parm;
							((BaseBusiness)control).Business_Init();
							return;
						}
					}
				}
			}
			else //如果尚未打开，则new
			{
				XtraTabPage newPage = new XtraTabPage();
				newPage.Text = businessTab[bo001].bo003;
				newPage.Tag = bo001;
				newPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;

				BaseBusiness bo = (BaseBusiness)Activator.CreateInstance(Type.GetType("White.BusinessObject." + bo001));

				Envior.mform = this;

				bo.Dock = DockStyle.Fill;
				bo.Parent = newPage;
				bo.swapdata.Add("parm", parm);

				newPage.Controls.Add(bo);

				xtraTabControl1.TabPages.Add(newPage);
				xtraTabControl1.SelectedTabPage = newPage;

				bo.Business_Init();

				////////登记已打开 Tabpage ////////
				openedTabPage.Add(bo001, newPage);

			}
		}

		/// <summary>
		/// 关闭标签页事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
		{
			ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;

			XtraTabPage curPage = (XtraTabPage)arg.Page;
			///////// 清除页面追踪 ////////
			openedTabPage.Remove(curPage.Tag.ToString());

			curPage.Dispose();
		}


		/// <summary>
		/// 数据项维护
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("数据项维护")) return;
			openBusinessObject("DataDict");
		}

		/// <summary>
		/// 升级文件上传
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem37_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("系统升级")) return;
			Frm_Upgrade frm_1 = new Frm_Upgrade();
			frm_1.ShowDialog();
			frm_1.Dispose();
		}

		/// <summary>
		/// 税务发票项目维护
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem35_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("税务发票项目维护")) return;
			openBusinessObject("TaxItem");
		}

		

		/// <summary>
		/// 财政发票项目维护
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem33_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("财政发票项目维护")) return;
			openBusinessObject("FinItem");
		}

		/// <summary>
		/// 寄存结构维护
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem36_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("寄存结构维护")) return;
			openBusinessObject("RegisterStru");
		}


		/// <summary>
		/// 商品服务定价维护
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("商品服务定价维护")) return;
			openBusinessObject("ServiceCommodity");
		}

		/// <summary>
		/// 进灵登记
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("进灵登记")) return;
			Frm_fireCheckin frm_checkin = new Frm_fireCheckin();
			frm_checkin.swapdata["action"] = "add";

			Checkin_ds checkin_ds = new Checkin_ds();
			frm_checkin.swapdata["dataset"] = checkin_ds;

			frm_checkin.ShowDialog();
			frm_checkin.Dispose();
		}

		/// <summary>
		/// 登记浏览
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("登记浏览")) return;
			openBusinessObject("FireCheckinBrow");
		}

		private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
		{
			////税务发票测试
			//TaxInvoice.WrapData();.
			TaxDemo frm_1 = new TaxDemo();
			frm_1.ShowDialog();
			frm_1.Dispose();
		}

		/// <summary>
		/// 临时销售
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("临时性销售")) return;
			openBusinessObject("TempSales");
		}

		/// <summary>
		/// 税务基础信息维护
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem34_ItemClick(object sender, ItemClickEventArgs e)
		{
			Frm_TaxBaseInfo frm_1 = new Frm_TaxBaseInfo();
			frm_1.ShowDialog();
			frm_1.Dispose();
		}

		private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("操作员管理")) return;
			openBusinessObject("Operators");
		}

		/// <summary>
		/// 日收费查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("业务收费日查询")) return;
			openBusinessObject("FinanceDaySearch");
		}

		/// <summary>
		/// 连接博思开票服务器
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem40_ItemClick(object sender, ItemClickEventArgs e)
		{
			if(XtraMessageBox.Show("要连接【财政发票服务器】吗?","确认",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
				FinInvoice.AutoConnectBosi();
		}

		/// <summary>
		/// 保存财政发票基础信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem32_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("财政发票基础信息")) return;
			Frm_FinBaseInfo frm_1 = new Frm_FinBaseInfo();
			frm_1.ShowDialog();
			frm_1.Dispose();
		}

		private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("角色管理")) return;
			openBusinessObject("Roles");
		}

		/// <summary>
		/// 寄存办理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
		{
			openBusinessObject("Register_brow"); 

		}

		/// <summary>
		/// 出灵数据查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("出灵数据查询")) return;
			openBusinessObject("Report_Checkout");
		}

		private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("寄存室数据")) return;
			openBusinessObject("RegisterRoomData");
		}

		/// <summary>
		/// 迁出数据查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("寄存迁出查询")) return;
			openBusinessObject("Report_RegisterOut");
			
		}


		/// <summary>
		/// 修改密码
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
		{
			Frm_ChgPwd frm_modify_pwd = new Frm_ChgPwd();
			frm_modify_pwd.ShowDialog();
		}

		private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("类别统计")) return;
			openBusinessObject("Report_Class_stat");
		}

		/// <summary>
		/// 套餐维护
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("套餐维护")) return;
			openBusinessObject("Combo");
		}

		/// <summary>
		/// 全部单项统计
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("全部单项统计")) return;
			openBusinessObject("Report_Item_Stat");
		}

		private void barButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("寄存量统计")) return;
			openBusinessObject("Report_regstat");
		}

		private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("收款员收款统计")) return;
			openBusinessObject("Report_CasherStat");
		}

		private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("收款作废查询")) return;
			openBusinessObject("Report_FinRollBack");
		}

		//财政项目发票统计
		private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
		{
			openBusinessObject("Report_FinItemStat");
		}

		/// <summary>
		/// 工作站收款统计
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
		{
			openBusinessObject("Report_workstation");
		}

		/// <summary>
		/// 处理窗口消息
		/// </summary>
		/// <param name="m"></param>
		//protected override void DefWndProc(ref Message m)
		//{
		//	switch (m.Msg)
		//	{
		//		case 10001:
		//			int commandNum = m.WParam.ToInt32();
		//			string responseText = PrtServAction.GetResponseText(commandNum);
		//			XtraMessageBox.Show(responseText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
		//			break;
		//		default:
		//			base.DefWndProc(ref m);///调用基类函数处理非自定义消息。
		//			break;
		//	}
		//}
	}
}