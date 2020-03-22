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
    public partial class Frm_FireTime : BaseDialog
    {
        public Frm_FireTime()
        {
            InitializeComponent();
        }

        private void Frm_FireTime_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = DateTime.Now ;
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            this.swapdata["AC015"] = dateEdit1.EditValue;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}