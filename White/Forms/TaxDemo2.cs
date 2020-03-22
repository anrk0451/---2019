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
using White.Action;
using White.Misc;
using System.Web;
using RestSharp;
using Newtonsoft.Json;

namespace White.Forms
{
    public partial class TaxDemo2 :BaseDialog
    {
        public TaxDemo2()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(TaxInvoice.GetNextInvoiceNo("0000000022") > 0)
            {
                memoEdit1.Text = Envior.NEXT_BILL_CODE + "    " + Envior.NEXT_BILL_NUM;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
			Dictionary<string, Object> bdata = new Dictionary<string, object>();
			bdata.Add("fplxdm", "007");             //发票类型代码
			bdata.Add("kplx", "0");                 //开票类型 0-正数发票 1-负数
			bdata.Add("tspz", "00");                //特殊票种 00-不是 01-农产品销售 02-农产品收购
			bdata.Add("xhdwdzdh", "省农机公司");    //销货单位地址电话
			bdata.Add("xhdwyhzh", "农业银行");      //销货单位银行账号
			bdata.Add("ghdwsbh", "");               //购货单位纳税识别号
			bdata.Add("ghdwmc", "黑龙江电视台");  //购货单位名称
			bdata.Add("ghdwdzdh", "汉水路");           //购货单位地址电话
			bdata.Add("ghdwyhzh", "");              //购货单位银行账号

			List<C_detail> detaildata = new List<C_detail>();
			C_detail c_detail = new C_detail();
			c_detail.xh = "1";
			c_detail.fphxz = "0";                     //发票行性质	
			c_detail.ggxh = "";                       //规格型号
			c_detail.dw = "";                         //单位
			c_detail.spmc = "卫生棺A";               //商品名称
			c_detail.spsl = "1";
			c_detail.dj = "12.18";
			c_detail.je = "12.18";
			c_detail.sl = "0.03";
			c_detail.se = "0.37";                      //税额						
			c_detail.hsbz = "0";
			//c_detail.spbm = "3070401000000000000";    //商品编码
			c_detail.spbm = "1050104050000000000";
			c_detail.zxbm = "";
			c_detail.yhzcbs = "0";
			c_detail.slbs = "";
			c_detail.zzstsgl = "";
			detaildata.Add(c_detail);
			bdata.Add("mx", detaildata);
			bdata.Add("hjje", "12.18");             //合计金额
			bdata.Add("hjse", "0.37");              //合计税额
			bdata.Add("jshj", "12.55");             //价税合计
			bdata.Add("bz", "");                   //备注
			bdata.Add("skr", "张三");               //收款人
			bdata.Add("fhr", "复核人");            //复核人
			bdata.Add("kpr", "开票人");            //开票人
			bdata.Add("tzdbh", "");                 //通知单编号 专票红字必填
			bdata.Add("yfphm", "");                //原发票号码 负数发票必填
			bdata.Add("yfpdm", "");                //原发票代码 负数发票必填
			bdata.Add("gmf_dzyx", "");             //购买方电子邮箱 推送使用，电子发票，购买方电子邮箱和手机号码微信id三个必填一
			bdata.Add("gmf_sjhm", "13333335678");  //购买方手机号码
			bdata.Add("gmf_openid", "");           //购买方微信id

			string s_business_json = Tools.ConvertObjectToJson(bdata);
			

			string s_inputmi = Tools.AesEncrypt(s_business_json, Envior.TAX_PRIVATE_KEY);


			//完整请求数据
			Dictionary<string, object> fulldata = new Dictionary<string, object>();
			fulldata.Add("async", "true");
			fulldata.Add("input", s_inputmi);
			fulldata.Add("nsrsbh", "12231000414356488Q");
			fulldata.Add("appid", "207547b9-e5c8-414a-af47-df726867cbaa");
			fulldata.Add("serviceid", "FPKJ");
			fulldata.Add("sid", Tools.GetEntityPK("TAXREQ"));


			string s_full_json = Tools.ConvertObjectToJson(fulldata);
			string s_fullmi = Tools.AesEncrypt(s_full_json, Envior.TAX_PUBLIC_KEY);
			
			/////准备要发送的报文 urlencode编码
			string s_urlencode = HttpUtility.UrlEncode(s_fullmi);
		

			var client = new RestClient("https://taxsapi.holytax.com/v1/api/s");
			client.Timeout = -1;
			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			request.AddParameter("json", s_urlencode);
			IRestResponse response = client.Execute(request);
			string retstr = response.Content;


			////处理返回信息
			Object obj = JsonConvert.DeserializeObject(retstr);
			Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;
			string data = js["data"].ToString();
			string result = Tools.AesDecrypt(data,Envior.TAX_PRIVATE_KEY);
			memoEdit1.Text  = result;
		}

		/// <summary>
		/// /开具负数发票
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void simpleButton3_Click(object sender, EventArgs e)
		{
			TaxInvoice.PrintInvoice("023001800104", "52327257", "0");
		}

		private void simpleButton4_Click(object sender, EventArgs e)
		{
			Dictionary<string, Object> bdata = new Dictionary<string, object>();
			bdata.Add("fplxdm", "007");             //发票类型代码
			bdata.Add("kplx", "1");                 //开票类型 0-正数发票 1-负数
			bdata.Add("tspz", "00");                //特殊票种 00-不是 01-农产品销售 02-农产品收购
			bdata.Add("xhdwdzdh", "省农机公司");    //销货单位地址电话
			bdata.Add("xhdwyhzh", "农业银行");      //销货单位银行账号
			bdata.Add("ghdwsbh", "");               //购货单位纳税识别号
			bdata.Add("ghdwmc", "黑龙江电视台");  //购货单位名称
			bdata.Add("ghdwdzdh", "汉水路");           //购货单位地址电话
			bdata.Add("ghdwyhzh", "");              //购货单位银行账号

			List<C_detail> detaildata = new List<C_detail>();
			C_detail c_detail = new C_detail();
			c_detail.xh = "1";
			c_detail.fphxz = "0";                     //发票行性质	
			c_detail.ggxh = "";                       //规格型号
			c_detail.dw = "";                         //单位
			c_detail.spmc = "卫生棺A";               //商品名称
			c_detail.spsl = "-1";
			c_detail.dj = "12.18";
			c_detail.je = "-12.18";
			c_detail.sl = "0.03";
			c_detail.se = "-0.37";                      //税额						
			c_detail.hsbz = "0";
			//c_detail.spbm = "3070401000000000000";    //商品编码
			c_detail.spbm = "1050104050000000000";
			c_detail.zxbm = "";
			c_detail.yhzcbs = "0";
			c_detail.slbs = "";
			c_detail.zzstsgl = "";
			detaildata.Add(c_detail);
			bdata.Add("mx", detaildata);
			bdata.Add("hjje", "-12.18");             //合计金额
			bdata.Add("hjse", "-0.37");              //合计税额
			bdata.Add("jshj", "-12.55");             //价税合计
			bdata.Add("bz", "");                   //备注
			bdata.Add("skr", "张三");               //收款人
			bdata.Add("fhr", "复核人");            //复核人
			bdata.Add("kpr", "开票人");            //开票人
			bdata.Add("tzdbh", "");                 //通知单编号 专票红字必填
			bdata.Add("yfphm", "52327257");        //原发票号码 负数发票必填
			bdata.Add("yfpdm", "023001800104");    //原发票代码 负数发票必填
			bdata.Add("gmf_dzyx", "");             //购买方电子邮箱 推送使用，电子发票，购买方电子邮箱和手机号码微信id三个必填一
			bdata.Add("gmf_sjhm", "13333335678");  //购买方手机号码
			bdata.Add("gmf_openid", "");           //购买方微信id

			string s_business_json = Tools.ConvertObjectToJson(bdata);


			string s_inputmi = Tools.AesEncrypt(s_business_json, Envior.TAX_PRIVATE_KEY);


			//完整请求数据
			Dictionary<string, object> fulldata = new Dictionary<string, object>();
			fulldata.Add("async", "true");
			fulldata.Add("input", s_inputmi);
			fulldata.Add("nsrsbh", "12231000414356488Q");
			fulldata.Add("appid", "207547b9-e5c8-414a-af47-df726867cbaa");
			fulldata.Add("serviceid", "FPKJ");
			fulldata.Add("sid", Tools.GetEntityPK("TAXREQ"));


			string s_full_json = Tools.ConvertObjectToJson(fulldata);
			string s_fullmi = Tools.AesEncrypt(s_full_json, Envior.TAX_PUBLIC_KEY);

			/////准备要发送的报文 urlencode编码
			string s_urlencode = HttpUtility.UrlEncode(s_fullmi);


			var client = new RestClient("https://taxsapi.holytax.com/v1/api/s");
			client.Timeout = -1;
			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			request.AddParameter("json", s_urlencode);
			IRestResponse response = client.Execute(request);
			string retstr = response.Content;
			memoEdit1.Text = retstr;

			////处理返回信息
			Object obj = JsonConvert.DeserializeObject(retstr);
			Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;
			string data = js["data"].ToString();
			string result = Tools.AesDecrypt(data, Envior.TAX_PRIVATE_KEY);
			memoEdit2.Text = result;
		}
	}
}