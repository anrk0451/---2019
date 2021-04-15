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
using White.Misc;
using White.Action;
using Oracle.ManagedDataAccess.Client;

namespace White.Forms
{
    public partial class Frm_FinBaseInfo : BaseDialog
    {
        public Frm_FinBaseInfo()
        {
            InitializeComponent();
        }

        private void Frm_FinBaseInfo_Load(object sender, EventArgs e)
        {
            te_agency_code.EditValue = Envior.FIN_AGENCY_CODE;
            te_agency_name.EditValue = Envior.FIN_AGENCY_NAME;
            te_appid.EditValue = Envior.FIN_APPID;
            te_appkey.EditValue = Envior.FIN_APPKEY;
            te_batch_code.EditValue = Envior.FIN_BATCH_CODE;
            te_bill_name.EditValue = Envior.FIN_BILLNAME;
            te_code.EditValue = Envior.FIN_CODE;
            te_region_code.EditValue = Envior.FIN_REGION_CODE;
            te_url.EditValue = Envior.FIN_URL;
            te_version.EditValue = Envior.FIN_VERSION;
        }

        
        private void b_ok_Click(object sender, EventArgs e)
        {
            string s_region_code = te_region_code.Text;
            string s_agency_code = te_agency_code.Text;
            string s_agency_name = te_agency_name.Text;
            string s_url = te_url.Text;
            string s_appid = te_appid.Text;
            string s_appkey = te_appkey.Text;
            string s_bill_code = te_code.Text;
            string s_batch_code = te_batch_code.Text;
            string s_bill_name = te_bill_name.Text;
            string s_version = te_version.Text;
            OracleTransaction trans = null;
            try
            {
                trans = SqlAssist.conn.BeginTransaction();
                SqlAssist.ExecuteNonQuery_NoTans("update sp01 set sp005 ='" + s_region_code + "' where sp001 = '0000000060' ");
                SqlAssist.ExecuteNonQuery_NoTans("update sp01 set sp005 ='" + s_agency_code + "' where sp001 = '0000000062' ");
                SqlAssist.ExecuteNonQuery_NoTans("update sp01 set sp005 ='" + s_agency_name + "' where sp001 = '0000000063' ");
                SqlAssist.ExecuteNonQuery_NoTans("update sp01 set sp005 ='" + s_url + "' where sp001 = '0000000064' ");
                SqlAssist.ExecuteNonQuery_NoTans("update sp01 set sp005 ='" + s_appid + "' where sp001 = '0000000065' ");
                SqlAssist.ExecuteNonQuery_NoTans("update sp01 set sp005 ='" + s_appkey + "' where sp001 = '0000000066' ");
                SqlAssist.ExecuteNonQuery_NoTans("update sp01 set sp005 ='" + s_bill_code + "' where sp001 = '0000000068' ");
                SqlAssist.ExecuteNonQuery_NoTans("update sp01 set sp005 ='" + s_batch_code + "' where sp001 = '0000000067' ");
                SqlAssist.ExecuteNonQuery_NoTans("update sp01 set sp005 ='" + s_bill_name + "' where sp001 = '0000000069' ");
                SqlAssist.ExecuteNonQuery_NoTans("update sp01 set sp005 ='" + s_version + "' where sp001 = '0000000061' ");

                Envior.FIN_REGION_CODE = s_region_code;
                Envior.FIN_AGENCY_CODE = s_agency_code;
                Envior.FIN_AGENCY_NAME = s_agency_name;
                Envior.FIN_URL = s_url;
                Envior.FIN_APPID = s_appid;
                Envior.FIN_APPKEY = s_appkey;
                Envior.FIN_CODE = s_bill_code;
                Envior.FIN_BATCH_CODE = s_batch_code;
                Envior.FIN_BILLNAME = s_bill_name;
                Envior.FIN_VERSION = s_version;

                trans.Commit();

                XtraMessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ee)
            {
                trans.Rollback();
                XtraMessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}