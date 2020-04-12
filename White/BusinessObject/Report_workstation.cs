using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using White.BaseObject;
using Oracle.ManagedDataAccess.Client;
using White.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;
using White.Action;

namespace White.BusinessObject
{
    public partial class Report_workstation : BaseBusiness
    {
        private DataTable dt_wsStat = new DataTable();
        private OracleDataAdapter statAdapter = new OracleDataAdapter("select * from rep_workStation order by ws001", SqlAssist.conn);

        private DataTable dt_finInvoice = new DataTable();
        private OracleDataAdapter finAdapter = new OracleDataAdapter("select * from v_financeDay_invoices where billType = 'F' and ws001 = :ws001 and (to_char(kprq,'yyyy-mm-dd') between :begin and :end) ", SqlAssist.conn);

        private DataTable dt_taxInvoice = new DataTable();
        private OracleDataAdapter taxAdapter = new OracleDataAdapter("select * from v_financeDay_invoices where billType = 'T' and ws001 = :ws001 and (to_char(kprq,'yyyy-mm-dd') between :begin and :end) ", SqlAssist.conn);


        private OracleParameter op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
        private OracleParameter op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
        private OracleParameter op_ws001 = new OracleParameter("ws001", OracleDbType.Varchar2, 10);

        private string s_begin = string.Empty;
        private string s_end = string.Empty;
        private string s_ws001 = string.Empty;

        public Report_workstation()
        {
            InitializeComponent();
        }

        private void Report_workstation_Load(object sender, EventArgs e)
        {
            op_end.Direction = ParameterDirection.Input;
            op_begin.Direction = ParameterDirection.Input;
            op_ws001.Direction = ParameterDirection.Input;

            statAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_ws001, op_begin, op_end });
            finAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_ws001, op_begin, op_end });
            taxAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_ws001, op_begin, op_end });

            gridControl_center.DataSource = dt_wsStat;
            gridControl_fin.DataSource = dt_finInvoice;
            gridControl_tax.DataSource = dt_taxInvoice;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void RefreshData()
        {
            if (MiscAction.WorkStationStat(s_ws001, s_begin, s_end) > 0)
            {
                this.Cursor = Cursors.WaitCursor;

                dt_wsStat.Rows.Clear();
                statAdapter.Fill(dt_wsStat);

                //财政笔数
                gridColumn3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridColumn3.SummaryItem.DisplayFormat = "{0:N0}";
                //财政金额
                gridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridColumn4.SummaryItem.DisplayFormat = "{0:N2}";
                //税务笔数
                gridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridColumn5.SummaryItem.DisplayFormat = "{0:N0}";
                //税务金额
                gridColumn6.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridColumn6.SummaryItem.DisplayFormat = "{0:N2}";

                this.Cursor = Cursors.Arrow;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Frm_wsStat frm_1 = new Frm_wsStat();
            if (frm_1.ShowDialog() == DialogResult.OK)
            {
                if (frm_1.swapdata["dbegin"] == null || frm_1.swapdata["dbegin"] is System.DBNull)
                {
                    s_begin = "1900-01-01";
                }
                else
                {
                    s_begin = Convert.ToDateTime(frm_1.swapdata["dbegin"]).ToString("yyyy-MM-dd");
                }

                if (frm_1.swapdata["dend"] == null || frm_1.swapdata["dend"] is System.DBNull)
                {
                    s_end = "9999-12-31";
                }
                else
                {
                    s_end = Convert.ToDateTime(frm_1.swapdata["dend"]).ToString("yyyy-MM-dd");
                }

                if (frm_1.swapdata["ws001"] == null)
                    s_ws001 = "%";
                else
                    s_ws001 = frm_1.swapdata["ws001"].ToString();

                op_begin.Value = s_begin;
                op_end.Value = s_end;
                op_ws001.Value = s_ws001;

                this.RefreshData();
            }
            frm_1.Dispose();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridControl grid = gridControl_center;
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出Excel";
            fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";

            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.TextExportMode = TextExportMode.Text;//设置导出模式为文本
                grid.ExportToXlsx(fileDialog.FileName, options);
                XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridView_center_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int rowHandle = e.FocusedRowHandle;
            if (rowHandle >= 0)
            {
                string s_ws001 = gridView_center.GetRowCellValue(rowHandle, "WS001").ToString();
                op_ws001.Value = s_ws001;

                //1.财政发票
                dt_finInvoice.Rows.Clear();
                finAdapter.Fill(dt_finInvoice);
                //2.税务发票
                dt_taxInvoice.Rows.Clear();
                taxAdapter.Fill(dt_taxInvoice);
            }
        }

        private void gridView_fin_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle.ToString();
                }
            }
        }

        private void gridView_tax_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle.ToString();
                }
            }
        }
    }
}
