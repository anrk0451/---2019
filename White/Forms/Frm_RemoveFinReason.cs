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
    public partial class Frm_RemoveFinReason : BaseDialog
    {
        public Frm_RemoveFinReason()
        {
            InitializeComponent();
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            string s_reason = memoEdit1.Text;
            this.swapdata["reason"] = s_reason;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Frm_RemoveFinReason_Load(object sender, EventArgs e)
        {
            memoEdit1.Focus();
        }
    }
}