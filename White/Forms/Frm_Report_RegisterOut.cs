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
    public partial class Frm_Report_RegisterOut : BaseDialog
    {
        public Frm_Report_RegisterOut()
        {
            InitializeComponent();
        }

        private void Frm_Report_RegisterOut_Load(object sender, EventArgs e)
        {
            dateEdit2.EditValue = DateTime.Now;
            dateEdit1.EditValue = DateTime.Today.AddMonths(-1);
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            this.swapdata["dbegin"] = dateEdit1.EditValue;
            this.swapdata["dend"] = dateEdit2.EditValue;
            this.swapdata["RC003"] = textEdit1.EditValue;

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}