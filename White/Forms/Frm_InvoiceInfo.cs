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
    /// <summary>
    /// 显示发票信息
    /// </summary>
    public partial class Frm_InvoiceInfo : BaseDialog
    {
        DataTable dt_invoice = new DataTable();
        OracleDataAdapter adapter = new OracleDataAdapter("select * from v_validInvoiceInfo where fa001 = :fa001", SqlAssist.conn);
        OracleParameter op_fa001 = new OracleParameter("fa001", OracleDbType.Varchar2, 10);

        string fa001 = string.Empty;

        public Frm_InvoiceInfo()
        {
            InitializeComponent();
        }

        private void Frm_InvoiceInfo_Load(object sender, EventArgs e)
        {
            ///设置检索参数
            fa001 = this.swapdata["FA001"].ToString();
            op_fa001.Direction = ParameterDirection.Input;
            op_fa001.Value = fa001;
            this.Text = "发票信息【流水号" + fa001 + "】";

            ////检索结算信息
            OracleDataReader reader = SqlAssist.ExecuteReader("select * from v_financeday where fa001='" + fa001 + "'");
            reader.Read();
            if (reader.HasRows)
            {
                te_fa001.EditValue = reader["FA001"];
                te_fa002.EditValue = reader["FA002_TEXT"];
                te_fa003.EditValue = reader["GUYNAME"];
                te_fa100.EditValue = reader["HANDLER"];
            }

            adapter.SelectCommand.Parameters.Add(op_fa001);
            gridControl2.DataSource = dt_invoice;
            adapter.Fill(dt_invoice);
        }

        /// <summary>
        /// 转换票别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if(e.Column.FieldName.ToUpper() == "BILLTYPE")
            {
                if (e.Value.ToString() == "T")
                    e.DisplayText = "税票";
                else if (e.Value.ToString() == "F")
                    e.DisplayText = "财政票";
            }
        }
    }
}