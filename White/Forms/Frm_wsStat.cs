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

namespace White.Forms
{
    public partial class Frm_wsStat : BaseDialog
    {
        public Frm_wsStat()
        {
            InitializeComponent();
        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_wsStat_Load(object sender, EventArgs e)
        {
            dateEdit2.EditValue = DateTime.Today;
            dateEdit1.EditValue = DateTime.Today;
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            this.swapdata["dbegin"] = dateEdit1.EditValue;
            this.swapdata["dend"] = dateEdit2.EditValue;

            if (radioButton1.Checked)
                this.swapdata["ws001"] = Envior.WORKSTATIONID;
            else
                this.swapdata["ws001"] = "%";

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}