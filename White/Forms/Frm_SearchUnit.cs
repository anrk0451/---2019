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
using White.Domain;

namespace White.Forms
{
    public partial class Frm_SearchUnit : BaseDialog
    {
        DataTable dt_tu01 = new DataTable();   //临时性销售单位列表
        OracleDataAdapter tu01Adapter = new OracleDataAdapter("select * from tu01", SqlAssist.conn);


        public Frm_SearchUnit()
        {
            InitializeComponent();
        }

        private void Frm_SearchUnit_Load(object sender, EventArgs e)
        {
            tu01Adapter.Fill(dt_tu01);
            gridControl1.DataSource = dt_tu01;
        
        }

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            this.SelectRow(rowHandle);
        }

        private void SelectRow(int rowHandle)
        {
            if (rowHandle >= 0)
            {
                Tu01 tu01 = new Tu01();
                tu01.tu003 = gridView1.GetRowCellValue(rowHandle, "TU003").ToString();
                tu01.tu001 = gridView1.GetRowCellValue(rowHandle, "TU001").ToString();
                tu01.tu005 = gridView1.GetRowCellValue(rowHandle, "TU005").ToString();
                tu01.tu006 = gridView1.GetRowCellValue(rowHandle, "TU006").ToString();
                tu01.tu007 = gridView1.GetRowCellValue(rowHandle, "TU007").ToString();
                this.swapdata["TU01"] = tu01;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            this.SelectRow(gridView1.FocusedRowHandle);
        }
    }
}