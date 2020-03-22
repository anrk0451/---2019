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
    public partial class Frm_duration : BaseDialog
    {
        string s_mode = string.Empty;
        public Frm_duration()
        {
            InitializeComponent();
        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_duration_Load(object sender, EventArgs e)
        {
            s_mode = this.swapdata["MODE"].ToString();
            if(string.IsNullOrEmpty(s_mode) || s_mode == "1")
            {
                dateEdit2.EditValue = DateTime.Today;
                dateEdit1.EditValue = DateTime.Today;
            }
            else
            {
                dateEdit2.EditValue = DateTime.Today;
                dateEdit1.EditValue = DateTime.Today.AddMonths(-1);
            }
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            this.swapdata["begin"] = dateEdit1.EditValue;
            this.swapdata["end"] = dateEdit2.EditValue;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}