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
using White.Action;

namespace White.Forms
{
    public partial class Frm_AdjustRegisterDate : BaseDialog
    {
        string s_ac001 = string.Empty;

        public Frm_AdjustRegisterDate()
        {
            InitializeComponent();
        }

        private void Frm_AdjustRegisterDate_Load(object sender, EventArgs e)
        {
            s_ac001 = this.swapdata["AC001"].ToString();
            OracleDataReader reader = SqlAssist.ExecuteReader("select * from rc01 where rc001='" + s_ac001 + "'");
            while (reader.Read())
            {

                txtEdit_rc003.EditValue = reader["RC003"];
                txtedit_pos.Text = RegisterAction.GetRegPathName(s_ac001);
                de_begin.EditValue = reader["RC140"];
                de_end.EditValue = reader["RC150"];

            }
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(Convert.ToDateTime(de_begin.EditValue), Convert.ToDateTime(de_end.EditValue)) >= 0)
            {
                MessageBox.Show("寄存开始日期必须小于结束日期!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (RegisterAction.AdjustRegisterDate(s_ac001, Convert.ToDateTime(de_begin.EditValue), Convert.ToDateTime(de_end.EditValue)) > 0)
            {
                MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}