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

namespace White.BusinessObject
{
    public partial class Report_FinItemStat : BaseBusiness
    {
        private DataTable dt_source1 = new DataTable();
        private DataTable dt_source2 = new DataTable();

        private OracleDataAdapter sumAdapter =
            new OracleDataAdapter("select sum(sa007) sa007,invcode from v_finitem_stat where (to_char(fa200,'yyyy-mm-dd') between :begin and :end) group by invcode ", SqlAssist.conn);
        private OracleDataAdapter detAdapter =
            new OracleDataAdapter("select sa003,price,invcode,sum(sa007) sa007,sum(nums) nums from v_finitem_stat where (to_char(fa200,'yyyy-mm-dd') between :begin and :end) group by sa003,invcode,price order by invcode,price", SqlAssist.conn);

        private OracleParameter op_begin = null;
        private OracleParameter op_end = null;


        public Report_FinItemStat()
        {
            InitializeComponent();
        }

        private void Report_FinItemStat_Load(object sender, EventArgs e)
        {
            op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
            op_begin.Direction = ParameterDirection.Input;

            op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
            op_end.Direction = ParameterDirection.Input;

            sumAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end });
            detAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end });

            gridControl1.DataSource = dt_source1;
            gridControl2.DataSource = dt_source2;
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
        /// 转换 财政编码->名字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.ToUpper() == "INVCODE")
            {
                if (e.Value.ToString() == AppInfo.FIN_FIRE)
                    e.DisplayText = "火化";
                else if (e.Value.ToString() == AppInfo.FIN_REGISTER)
                    e.DisplayText = "骨灰寄存";
                else if (e.Value.ToString() == AppInfo.FIN_STORE)
                    e.DisplayText = "存放";
                else if (e.Value.ToString() == AppInfo.FIN_TRAFFIC)
                    e.DisplayText = "接运";
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Frm_duration frm_1 = new Frm_duration();
            frm_1.swapdata["MODE"] = "2";

            if (frm_1.ShowDialog() == DialogResult.OK)
            {
                string s_begin = string.Empty;
                string s_end = string.Empty;

                if (frm_1.swapdata["begin"] == null)
                {
                    s_begin = "1900-01-01";
                }
                else
                {
                    s_begin = Convert.ToDateTime(frm_1.swapdata["begin"]).ToString("yyyy-MM-dd");
                }

                if (frm_1.swapdata["end"] == null)
                {
                    s_end = "9999-12-31";
                }
                else
                {
                    s_end = Convert.ToDateTime(frm_1.swapdata["end"]).ToString("yyyy-MM-dd");
                }


                op_begin.Value = s_begin;
                op_end.Value = s_end;

                this.Cursor = Cursors.WaitCursor;

                //////1.按汇总查
                gridView1.BeginUpdate();
                dt_source1.Rows.Clear();

                sumAdapter.Fill(dt_source1);

                gridColumn1.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridColumn1.SummaryItem.DisplayFormat = "合计 = {0:N2}";

                gridView1.EndUpdate();


                //////2. 按明细查询
                gridView2.BeginUpdate();
                dt_source2.Rows.Clear();

                detAdapter.Fill(dt_source2);

                gridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridColumn5.SummaryItem.DisplayFormat = "合计 = {0:N2}";

                gridView2.EndUpdate();

                this.Cursor = Cursors.Arrow;
            }
            frm_1.Dispose();
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

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.ToUpper() == "INVCODE")
            {
                if (e.Value.ToString() == AppInfo.FIN_FIRE)
                    e.DisplayText = "火化";
                else if (e.Value.ToString() == AppInfo.FIN_REGISTER)
                    e.DisplayText = "骨灰寄存";
                else if (e.Value.ToString() == AppInfo.FIN_STORE)
                    e.DisplayText = "存放";
                else if (e.Value.ToString() == AppInfo.FIN_TRAFFIC)
                    e.DisplayText = "接运";
            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void RefreshData()
        {
            this.Cursor = Cursors.WaitCursor;

            //////1.按汇总查
            gridView1.BeginUpdate();
            dt_source1.Rows.Clear();

            sumAdapter.Fill(dt_source1);

            gridColumn1.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridColumn1.SummaryItem.DisplayFormat = "合计 = {0:N2}";

            gridView1.EndUpdate();


            //////2. 按明细查询
            gridView2.BeginUpdate();
            dt_source2.Rows.Clear();

            detAdapter.Fill(dt_source2);

            gridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridColumn5.SummaryItem.DisplayFormat = "合计 = {0:N2}";

            gridView2.EndUpdate();

            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出Excel";
            fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";

            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.TextExportMode = TextExportMode.Text;//设置导出模式为文本

                if (xtraTabControl1.SelectedTabPageIndex == 0)
                {
                    gridControl1.ExportToXlsx(fileDialog.FileName, options);
                }
                else if (xtraTabControl1.SelectedTabPageIndex == 1)
                {
                    gridControl2.ExportToXlsx(fileDialog.FileName, options);
                }
                XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
