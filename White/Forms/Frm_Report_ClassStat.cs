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
    public partial class Frm_Report_ClassStat : BaseDialog
    {
        private DataTable dt_cast = new DataTable();
        private OracleDataAdapter castAdapter =
            new OracleDataAdapter("select * from castinfo", SqlAssist.conn);

        public Frm_Report_ClassStat()
        {
            InitializeComponent();
        }

        private void Frm_Report_ClassStat_Load(object sender, EventArgs e)
        {
            checkedListBoxControl1.DataSource = dt_cast;
            checkedListBoxControl1.ValueMember = "SERVICESALESTYPE";
            checkedListBoxControl1.DisplayMember = "TYPEDESC";
            castAdapter.Fill(dt_cast);
 

            dateEdit2.EditValue = DateTime.Today;
            dateEdit1.EditValue = DateTime.Today.AddMonths(-1);

            checkedListBoxControl1.CheckAll();
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            StringBuilder sb_class_string = new StringBuilder();  //所选类别字符串

            this.swapdata["dbegin"] = dateEdit1.EditValue;
            this.swapdata["dend"] = dateEdit2.EditValue;
            List<string> classList = new List<string>();


            foreach (DataRowView item in checkedListBoxControl1.CheckedItems)
            {
                classList.Add(item["SERVICESALESTYPE"].ToString());
                sb_class_string.Append(item["TYPEDESC"].ToString() + ",");
            }

            if (classList.Count <= 0)
            {
                MessageBox.Show("请至少选择一个类别!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.swapdata["class"] = classList.ToArray();
            this.swapdata["class-string"] = sb_class_string.ToString().Substring(0, sb_class_string.ToString().Length - 1);

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxControl1.ItemCount; i++)
            {
                checkedListBoxControl1.SetItemCheckState(i, checkEdit1.CheckState);
            }
        }
    }
}