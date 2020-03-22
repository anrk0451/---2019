using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White.Misc
{
	class Envior
	{
		public static MainForm mform { get; set; }          //当前主窗口
        public static string cur_user { get; set; }         //当前登录用户
        public static string cur_userId { get; set; }       //当前登录用户Id
        public static string cur_userName { get; set; }     //当前登录用户名
        public static string cur_userBosi  { get; set; }    //博思服务账号
        public static string cur_pwdBosi { get; set; }      //博思服务密码


        //关于财政发票
        public static bool FINANCE_INVOICE_HANDLE { get; set; }   //是否允许开具财政发票操作
        
        public static string FIN_INVOICE_TITLE { get; set; }      //财政发票交款人标题
        public static string FIN_INVOICE_TYPE { get; set; }       //财政发票票据类型



        public static string NEXT_BILL_CODE { get; set; }     //下张发票代码
        public static string NEXT_BILL_NUM { get; set; }      //下张发票票号
        public static string TAX_ID { get; set; }             //纳税识别号
        public static string TAX_ADDR_TELE { get; set; }      //税务-销方地址电话
        public static string TAX_BANK_ACCOUNT { get; set; }   //税务-销方银行&账号
        public static string TAX_APPID { get; set; }          //税务 appId
        public static string TAX_INVOICE_TYPE { get; set; }   //发票类型
        public static string TAX_PUBLIC_KEY { get; set; }     //公钥
        public static string TAX_PRIVATE_KEY { get; set; }    //私钥
        public static string TAX_SERVER_URL { get; set; }     //税务发票服务URL  
         

        public static string[] rolearry { get; set; }      //所属角色组
        public static char loginMode { get; set; }         //登陆模式

        //public static bool printable { get; set; }       //打印进程是否启动
		public static bool canInvoice { get; set; }		   //当前的用户允许开发票
        public static IntPtr prtservHandle { get; set; }   //打印服务窗口Handle
        public static int prtConnId { get; set; }          //打印会话连接Id 

		public static bool FIN_READY { get; set; }		   // 博思开票状态

		//public static n_prtserv prtserv { get; set; }      //打印服务对象
 
	}
}
