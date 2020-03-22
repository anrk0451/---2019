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
    public partial class Frm_Zch_input : BaseDialog
    {
        public Frm_Zch_input()
        {
            InitializeComponent();
        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_Zch_input_Load(object sender, EventArgs e)
        {
            textEdit1.Focus();
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            string s_zch = textEdit1.Text;
            if (string.IsNullOrEmpty(s_zch))
            {
                textEdit1.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                textEdit1.ErrorText = "请输入发票注册号!";
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.swapdata["zch"] = s_zch;
            this.Close();
        }
    }
}