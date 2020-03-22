using DevExpress.XtraEditors;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using White.Misc;

namespace White.Action
{
    /// <summary>
    /// 财政发票类
    /// </summary>
    class FinInvoice
    {
        [DllImport("kpjk.dll", EntryPoint = "PConnect")]
        private extern static int ConnectKp();

        [DllImport("kpjk.dll", EntryPoint = "PDisConnect")]
        private extern static int DisconnectKp();

        [DllImport("kpjk.dll", EntryPoint = "PAdvConnect")]
        private extern static int AdvConnectKp(string User,string pwd,string zt);


        [DllImport("kpjk.dll", EntryPoint = "PLoginSuccess")]
        private extern static int PLoginSuccess();
 

        [DllImport("kpjk.dll", EntryPoint = "PGetCurPh", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private extern static int GetCurPh(string ptype, [MarshalAs(UnmanagedType.LPStr)]StringBuilder res);
         


        [DllImport("kpjk.dll", EntryPoint = "PZrPj", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private extern static int PZrPj(string invdata,int ifprt,string pjlx,string bz, [MarshalAs(UnmanagedType.LPStr)]StringBuilder res);

        [DllImport("kpjk.dll", EntryPoint = "PDelPj", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private extern static int PDelPj(string pjh, [MarshalAs(UnmanagedType.LPStr)]StringBuilder res);


        [DllImport("kpjk.dll", EntryPoint = "PZrTkkp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private extern static int PZrTkkp(string oldph,string oldpjlx,string oldzch,string newpjlx,string tkitem,string aqt, [MarshalAs(UnmanagedType.LPStr)]StringBuilder res);


        /// <summary>
        /// 连接开票服务器
        /// </summary>
        /// <returns></returns>
        public static int Connect()
        {
            int result = ConnectKp();
            return result;
        }

        /// <summary>
        /// 连接开票服务器
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public static int AdvConnect(string user,string pwd,string zt)
        {
            return AdvConnectKp(user, pwd, zt);
        }


        /// <summary>
        /// 断开开票服务器
        /// </summary>
        /// <returns></returns>
        public static int DisConnect()
        {
            return DisconnectKp();
        }

        /// <summary>
        /// 判断是否连接博思开票服务器
        /// </summary>
        /// <returns></returns>
        public static bool IsConnect()
        {
            if (PLoginSuccess() == 1)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 返回指定票据类型的可用票号
        /// </summary>
        /// <param name="pjlx"></param>
        /// <returns></returns>
        public static string GetCurrentPh(string pjlx)
        {
            StringBuilder sb_pjh = new StringBuilder();
            if (GetCurPh(pjlx, sb_pjh) > 0)
            {
                return sb_pjh.ToString().Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 发票开具
        /// </summary>
        /// <param name="invdata"></param>
        /// <param name="ifprt"></param>
        /// <param name="pjlx"></param>
        /// <param name="bz"></param>
        /// <returns></returns>
        public static string Invoice(string invdata,int ifprt,string pjlx,string bz)
        {
            StringBuilder sb_res = new StringBuilder();
            try
            {
                PZrPj(invdata, ifprt, pjlx, bz, sb_res);
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.ToString(),"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
             
            return sb_res.ToString();
        }

        /// <summary>
        /// 作废财政发票
        /// </summary>
        /// <param name="pjh"></param>
        /// <returns></returns>
        public static string Remove(string pjh)
        {
            StringBuilder sb_res = new StringBuilder();
            PDelPj(pjh, sb_res);
            return sb_res.ToString();
        } 

        /// <summary>
        /// 作废财政发票
        /// </summary>
        /// <param name="zch"></param>
        /// <param name="pjlx"></param>
        /// <param name="pjh"></param>
        /// <returns></returns>
        public static int Remove(string zch,string pjlx,string pjh)
        {
            string s_content = "票据类型=" + pjlx + "|票据号=" + pjh + "|注册号=" + zch;
            MessageBox.Show(s_content);
            string retstr = Remove(s_content);
            if (retstr.IndexOf("成功") >= 0)
            {
                XtraMessageBox.Show("作废财政发票成功!\r\n" + "票据类型:" + pjlx + ",票据号:" + pjh + ",注册号:" + zch, "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return 1;
            }
            else
            {
                XtraMessageBox.Show("作废财政发票失败!请与管理员联系!\r\n" + retstr,"提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return -1;
            }
        }

        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="OldPjlx"></param>
        /// <param name="OldPjh"></param>
        /// <param name="OldZch"></param>
        /// <param name="NewPjlx"></param>
        /// <param name="tkitem"></param>
        /// <param name="aQt"></param>
        /// <param name="res"></param>
        /// <returns></returns>
        public static int Refund(string OldPjlx,string OldPjh,string OldZch,string tkitem,string aQt,string fa001,string NewPjh,decimal hjje)
        {
            StringBuilder sb_res = new StringBuilder();
            PZrTkkp(OldPjh,OldPjlx, OldZch, Envior.FIN_INVOICE_TYPE, tkitem, aQt, sb_res);
            if (sb_res.ToString().IndexOf("成功") >= 0)  //退费发票开具成功
            {
                if (FinInvoiceLog(fa001, Envior.FIN_INVOICE_TYPE, NewPjh, "",0 - Math.Abs(hjje),Envior.cur_userName) > 0)
                {
                    XtraMessageBox.Show("发票开具成功!\r\n" + "发票类型:" + Envior.FIN_INVOICE_TYPE + "\r\n" + "发票号:" + NewPjh, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 1;
                }
                else
                {
                    XtraMessageBox.Show("发票开具成功!但记录日志失败，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return 1;
                }
            }
            else
            {
                XtraMessageBox.Show("发票开具失败!\r\n" + sb_res.ToString(),"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return -1;
            }
        }


        /// <summary>
        /// 根据结算流水号开具发票
        /// </summary>
        /// <param name="fa001"></param>
        /// <returns></returns>
        public static int Invoice(string fa001)
        {
            OracleParameter op_fa001 = new OracleParameter("fa001", OracleDbType.Varchar2, 10);
            op_fa001.Direction = ParameterDirection.Input;
            op_fa001.Value = fa001;
            OracleDataReader reader_fa01 = SqlAssist.ExecuteReader("select fa003,fa180 from fa01 where fa001 = :fa001",new OracleParameter[] { op_fa001});
            
            string s_head = string.Empty;
            string s_memo = string.Empty;  //备注
            decimal dec_hjje = decimal.Zero;
            while (reader_fa01.Read())
            {
                //读取交款人
                s_head = Envior.FIN_INVOICE_TITLE + "=" + reader_fa01["FA003"].ToString() + "	";
                s_memo = reader_fa01["FA180"].ToString();
            }
            reader_fa01.Dispose();

            string s_sql = @"select sa002,
                                    sa004,
                                    sa003,
                                    price,
                                    decode(sa002,'08',1,nums) nums,
                                    sa020,
                                    sa007,
                                    pkg_business.fun_GetInvoiceCode(sa002,sa004) invcode
                             from v_sa01
                            where sa010 = :fa001 order by sa001";
            OracleDataReader reader_sa01 = SqlAssist.ExecuteReader(s_sql,new OracleParameter[] { op_fa001});

            StringBuilder sb_detail = new StringBuilder(100);
            while (reader_sa01.Read())
            {
                if (reader_sa01["SA020"].ToString() != "F") continue;  //如果不是财政发票,忽略
                sb_detail.Append("收费项目=" + reader_sa01["INVCODE"].ToString() + "	" + "计费数量=" + reader_sa01["NUMS"].ToString() + "	" + "收费标准=" + reader_sa01["PRICE"].ToString() + "	" + "金额=" + reader_sa01["SA007"].ToString() + "	");
                dec_hjje += Convert.ToDecimal(reader_sa01["SA007"]);
            }
            reader_sa01.Dispose();

            string vContent = "<&票据><&票据头>" + s_head + "	" + "</&票据头>" + "<&收费项目>" + sb_detail.ToString() + "</&收费项目></&票据>";


            //XtraMessageBox.Show(vContent,"debug 2");
            string retstr = Invoice(vContent, 1,Envior.FIN_INVOICE_TYPE, s_memo);
            if(retstr.IndexOf("成功") >= 0)
            {
                //TODO 4. 记录财政发票日志
                string s_info = retstr.Substring(3);
                string[] s_arry = s_info.Split(',');
                if(s_arry.Length >= 4)
                {
                    string s_pjlx = s_arry[0];    //票据类型
                    string s_pjh = s_arry[1];     //发票号
                    string s_zch = s_arry[3];     //注册号
                    if(FinInvoiceLog(fa001,s_pjlx,s_pjh,s_zch,dec_hjje,Envior.cur_userName) > 0)
                    {
                        XtraMessageBox.Show("发票开具成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 1;
                    }
                    else
                    {
                        XtraMessageBox.Show("发票开具成功!但记录日志失败，请与管理员联系！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return 1;
                    }
                }
                else
                {
                    XtraMessageBox.Show("发票开具成功!但记录日志失败，请与管理员联系!！\r\n" + retstr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return 1;
                }
            }
            else
            {
                XtraMessageBox.Show("发票开具失败!\r\n" + retstr,"提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return -1;
            }
        }

        /// <summary>
        /// 财政发票开票日志
        /// </summary>
        /// <param name="fa001">结算流水号</param>
        /// <param name="fplx">发票类型</param>
        /// <param name="fph">发票号</param>
        /// <param name="zch">注册号</param>
        /// <returns></returns>
        public static int FinInvoiceLog(string fa001,string fplx,string fph,string zch,decimal hjje,string kpr)
        {
            //逝者编号
            OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
            op_fa001.Direction = ParameterDirection.Input;
            op_fa001.Value = fa001;

            //票据类型
            OracleParameter op_pjlx = new OracleParameter("ic_billCode", OracleDbType.Varchar2,10);
            op_pjlx.Direction = ParameterDirection.Input;
            op_pjlx.Value = fplx;

            //票号
            OracleParameter op_fph = new OracleParameter("ic_billNo", OracleDbType.Varchar2,10);
            op_fph.Direction = ParameterDirection.Input;
            op_fph.Value = fph;

            //注册号
            OracleParameter op_zch = new OracleParameter("ic_zch", OracleDbType.Varchar2, 10);
            op_zch.Direction = ParameterDirection.Input;
            op_zch.Value = zch;

            //合计金额
            OracleParameter op_hjje = new OracleParameter("in_hjje", OracleDbType.Decimal, 10);
            op_hjje.Direction = ParameterDirection.Input;
            op_hjje.Value = hjje;
            //开票人
            OracleParameter op_kpr = new OracleParameter("ic_kpr", OracleDbType.Varchar2, 10);
            op_kpr.Direction = ParameterDirection.Input;
            op_kpr.Value = kpr;


            return SqlAssist.ExecuteProcedure("pkg_business.prc_FinInvoiceLog",
                new OracleParameter[] {op_fa001,op_pjlx,op_fph,op_zch,op_hjje,op_kpr});
        }

        /// <summary>
        /// 自动连接到博思服务器
        /// </summary>
        /// <returns></returns>
        public static void AutoConnectBosi()
        {
            int result = 0;
            if (String.IsNullOrEmpty(Envior.cur_userBosi))
            {
                result = Connect();
            }
            else
            {
                result = AdvConnect(Envior.cur_userBosi, Envior.cur_pwdBosi, "");
            }
            if (result == 1)
                Envior.FIN_READY = true;
            else
            {
                XtraMessageBox.Show("连接财政开票服务器失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Envior.FIN_READY = false;
            }
            // PrtServAction.ConnectBosi(Envior.cur_userBosi, Envior.cur_pwdBosi, Envior.mform.Handle.ToInt32());
        }

    }
}
