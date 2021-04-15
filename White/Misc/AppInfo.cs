using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White
{
    /// <summary>
    /// 应用信息
    /// </summary>
    class AppInfo
    {
        private static string _AppTitle = "殡仪馆管理信息系统";     //应用标题
        private static string _AppVersion = "20.0131001";           //应用版本号
        private static string _UnitName = "牡丹江市第二殡仪馆";     //使用单位    
        private static string _ROOTID = "0000000000";               //root用户Id
        private static int _GRID_HEIGHT = 48;
        private static int _GRID_WIDTH = 55;


        private static string _BOSI_API_ADDR = @"http://192.168.101.37:18026/standard-web/api/standard/";   //博思API服务地址
  
        private static int _TAXITEMCOUNT = 8;                       //打印发票清单阈值
		private static string _REG_TAX_NAME = "寄存费";				//寄存费税务名称

        //private static string _FIN_TRAFFIC = "90001";               //接运
        //private static string _FIN_FIRE = "90003";                  //火化
        //private static string _FIN_STORE = "90002";                 //存放
        //private static string _FIN_REGISTER = "90004";              //骨灰寄存


        private static string _FIN_TRAFFIC = "10304490801";               //接运
        private static string _FIN_FIRE = "10304490802";                  //火化
        private static string _FIN_STORE = "10304490804";                 //存放
        private static string _FIN_REGISTER = "10304490803";              //骨灰寄存

        public static string UnitName
        {
            get { return AppInfo._UnitName; }
        }

        public static string AppTitle
        {
            get { return _AppTitle; }
        }

        public static string AppVersion
        {
            get { return _AppVersion; }
        }

        public static string ROOTID
        {
            get { return _ROOTID; }
        }
 
		public static int TAXITEMCOUNT
		{
			get { return _TAXITEMCOUNT; }
		}

		public static string REG_TAX_NAME
		{
			get { return _REG_TAX_NAME; }
		}

        public static string BOSI_API_ADDR
        {
            get { return _BOSI_API_ADDR; }
        }
        public static int GRID_HEIGHT
        {
            get { return _GRID_HEIGHT; }
        }

        public static int GRID_WIDTH
        {
            get { return _GRID_WIDTH; }
        }

        public static string FIN_TRAFFIC
        {
            get { return _FIN_TRAFFIC; }
        }

        public static string FIN_FIRE
        {
            get { return _FIN_FIRE; }
        }

        public static string FIN_STORE
        {
            get { return _FIN_STORE; }
        }

        public static string FIN_REGISTER
        {
            get { return _FIN_REGISTER; }
        }
    }
}
