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

namespace White.Forms
{
    public partial class Frm_refundInfo :BaseDialog
    {
        public Frm_refundInfo()
        {
            InitializeComponent();
        }
        public Frm_refundInfo(string pjlx,string pjh,string zch)
        {
            InitializeComponent();
            te_pjlx.Text = pjlx;
            te_pjh.Text = pjh;
            te_zch.Text = zch;
        }

        private void sb_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            string s_zch = te_zch.Text;
            if (String.IsNullOrEmpty(s_zch))
            {
                te_zch.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_zch.ErrorText = "注册号不能为空!";
                return;
            }

            this.swapdata["zch"] = s_zch;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}