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
            te_pjlx.Text = Envior.FIN_INVOICE_TYPE;
            te_title.Text = Envior.FIN_INVOICE_TITLE;
        }

        
        private void b_ok_Click(object sender, EventArgs e)
        {
            string s_pjlx = te_pjlx.Text;
            string s_title = te_title.Text;
            if (string.IsNullOrEmpty(s_pjlx))
            {
                te_pjlx.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_pjlx.ErrorText = "票据类型不能为空!";
                return;
            }else if (string.IsNullOrEmpty(s_title))
            {
                te_title.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_title.ErrorText = "题头名称不能为空!";
                return;
            }

            if (AppAction.SaveFinanceInfo(s_pjlx,s_title)> 0)
            {
                XtraMessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Envior.FIN_INVOICE_TYPE = s_pjlx;
                Envior.FIN_INVOICE_TITLE = s_title;
                
                this.Close();
            }


        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}