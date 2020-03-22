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
using Oracle.ManagedDataAccess.Client;

namespace White.Forms
{
    public partial class Frm_Report_Checkout : BaseDialog
    {
        private DataTable dt_ac007_source = new DataTable();   //所属区县
        private OracleDataAdapter ac007Adapter =
            new OracleDataAdapter("select st001,st003 from st01 where st002 = 'DISTRICT' and status = '1' order by sortId", SqlAssist.conn);

        public Frm_Report_Checkout()
        {
            InitializeComponent();
        }

        private void Frm_Report_Checkout_Load(object sender, EventArgs e)
        {
            //bo = this.swapdata["BusinessObject"] as BaseBusiness;

            dateEdit2.EditValue = DateTime.Today;
            dateEdit1.EditValue = DateTime.Today.AddMonths(-1);

            ac007Adapter.Fill(dt_ac007_source);
            DataRow newrow = dt_ac007_source.NewRow();
            newrow["ST001"] = "%";
            newrow["ST003"] = "全部";
            dt_ac007_source.Rows.Add(newrow);

            lookUp_ac007.Properties.DataSource = dt_ac007_source;
            lookUp_ac007.Properties.DisplayMember = "ST003";
            lookUp_ac007.Properties.ValueMember = "ST003";
            lookUp_ac007.EditValue = "全部";
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            this.swapdata["dbegin"] = dateEdit1.EditValue.ToString();
            this.swapdata["dend"] = dateEdit2.EditValue.ToString();
            this.swapdata["AC003"] = textEdit1.Text;
            this.swapdata["AC007"] = lookUp_ac007.EditValue;

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}