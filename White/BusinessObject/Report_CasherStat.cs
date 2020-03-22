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
using White.Misc;
using White.Action;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;

namespace White.BusinessObject
{
    public partial class Report_CasherStat : BaseBusiness
    {
        DataTable dt_casherStat = new DataTable();
        OracleDataAdapter statAdapter = new OracleDataAdapter("select * from rep_casherStat order by uc001",SqlAssist.conn);

        DataTable dt_normal = new DataTable();
        OracleDataAdapter norAdapter = new OracleDataAdapter("select * from v_financeday where  fa100 = :fa100 and (to_char(fa200,'yyyy-mm-dd') between :begin and :end) and fa004 > 0"  , SqlAssist.conn);

        DataTable dt_refund = new DataTable();
        OracleDataAdapter refundAdapter = new OracleDataAdapter("select * from v_financeday where  fa100 = :fa100 and (to_char(fa200,'yyyy-mm-dd') between :begin and :end) and fa004 < 0", SqlAssist.conn);

        DataTable dt_finInvoice = new DataTable();
        OracleDataAdapter finAdapter = new OracleDataAdapter("select * from v_financeDay_invoices where billType = 'F' and fa100 = :fa100 and (to_char(fa200,'yyyy-mm-dd') between :begin and :end) ", SqlAssist.conn);

        DataTable dt_taxInvoice = new DataTable();
        OracleDataAdapter taxAdapter = new OracleDataAdapter("select * from v_financeDay_invoices where billType = 'T' and fa100 = :fa100 and (to_char(fa200,'yyyy-mm-dd') between :begin and :end) ", SqlAssist.conn);



        OracleParameter op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
        OracleParameter op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
        OracleParameter op_fa100 = new OracleParameter("fa100", OracleDbType.Varchar2, 10);
 
   
        string s_begin = string.Empty;
        string s_end = string.Empty;
        string s_fa100 = string.Empty;

        public Report_CasherStat()
        {
            InitializeComponent();
        }

        private void Report_CasherStat_Load(object sender, EventArgs e)
        {
            op_end.Direction = ParameterDirection.Input;
            op_begin.Direction = ParameterDirection.Input;
            op_fa100.Direction = ParameterDirection.Input;

            norAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_fa100,op_begin, op_end });
            refundAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_fa100, op_begin, op_end });
            finAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_fa100, op_begin, op_end });
            taxAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_fa100, op_begin, op_end });

            gridControl_center.DataSource = dt_casherStat;
            gridControl1.DataSource = dt_normal;
            gridControl2.DataSource = dt_refund;
            gridControl3.DataSource = dt_finInvoice;
            gridControl4.DataSource = dt_taxInvoice;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Frm_Report_Condition frm_1 = new Frm_Report_Condition();
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

                if (frm_1.swapdata["FA100"] == null)
                    s_fa100 = "%";
                else
                    s_fa100 = frm_1.swapdata["FA100"].ToString();

                op_begin.Value = s_begin;
                op_end.Value = s_end;
                op_fa100.Value = s_fa100;

                this.RefreshData();
            }
            frm_1.Dispose();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void RefreshData()
        {
            if(MiscAction.CasherStat(s_begin,s_end,s_fa100) > 0)
            {
                this.Cursor = Cursors.WaitCursor;

                dt_casherStat.Rows.Clear();
                statAdapter.Fill(dt_casherStat);

                gridColumn15.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridColumn15.SummaryItem.DisplayFormat = "{0:N0}";
                gridColumn16.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridColumn16.SummaryItem.DisplayFormat = "{0:N2}";

                groupControl1.Text = "统计日期 " + s_begin + "至" + s_end;

                this.Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// 行焦点改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_center_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int rowHandle = e.FocusedRowHandle;
            if(rowHandle >= 0)
            {
                string s_fa100 = gridView_center.GetRowCellValue(rowHandle, "UC001").ToString();
                op_fa100.Value = s_fa100;

                //1.正常缴费
                dt_normal.Rows.Clear();
                norAdapter.Fill(dt_normal);
                //2.退费
                dt_refund.Rows.Clear();
                refundAdapter.Fill(dt_refund);
                //3.财政发票
                dt_finInvoice.Rows.Clear();
                finAdapter.Fill(dt_finInvoice);
                //4.税务发票
                dt_taxInvoice.Rows.Clear();
                taxAdapter.Fill(dt_taxInvoice);
            }
        }

        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView3_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView4_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 0)
                gridView1.ShowFindPanel();
            else if (tabPane1.SelectedPageIndex == 1)
                gridView2.ShowFindPanel();
            else if (tabPane1.SelectedPageIndex == 2)
                gridView3.ShowFindPanel();
            else if (tabPane1.SelectedPageIndex == 3)
                gridView4.ShowFindPanel();
        }
    }
}
