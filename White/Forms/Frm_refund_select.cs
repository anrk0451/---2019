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
using White.Misc;
using White.Domain;

namespace White.Forms
{
    public partial class Frm_refund_select : BaseDialog
    {
        DataTable dt_sales = new DataTable();
        OracleParameter op_sa010 = new OracleParameter("sa010", OracleDbType.Varchar2, 10);
        OracleParameter op_sa020 = new OracleParameter("sa020", OracleDbType.Varchar2, 3);
        string s_sa010 = string.Empty;
        string s_sa020 = string.Empty;
        OracleDataAdapter salesAdapter =
            new OracleDataAdapter("select * from v_refund_select where sa010 = :fa001 and sa020 like :sa020", SqlAssist.conn);

        public Frm_refund_select()
        {            
            InitializeComponent(); 
        }

        private void Frm_refund_select_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dt_sales;
            s_sa010 = this.swapdata["SA010"].ToString();
            s_sa020 = this.swapdata["SA020"].ToString();
            op_sa010.Direction = ParameterDirection.Input;
            op_sa020.Direction = ParameterDirection.Input;

            op_sa010.Value = s_sa010;
            op_sa020.Value = s_sa020;

            salesAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_sa010, op_sa020 });
            salesAdapter.Fill(dt_sales);

            gridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridColumn4.SummaryItem.DisplayFormat = "合计 = {0:N2}";

        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 验证 退费数量合法性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            decimal dec_refundFee = decimal.Zero;
            int rowHandle = gridView1.FocusedRowHandle;
            if(decimal.TryParse(e.Value.ToString(), out dec_refundFee))
            {
                if(dec_refundFee > Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle,"SA007")))
                {
                    e.Valid = false;
                    e.ErrorText = "退费金额不能大于原收费金额!";
                    return;
                }
            }

        }

        /// <summary>
        /// 退费办理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!gridView1.PostEditor()) return;
            if (!gridView1.UpdateCurrentRow()) return;

            List<string> itemIdList = new List<string>();
            List<decimal> numsList = new List<decimal>();
            List<decimal> priceList = new List<decimal>();
            List<string> itemTypeList = new List<string>();
 
            decimal dec_fin_sum = decimal.Zero;
            decimal dec_temp = decimal.Zero;
 
            for(int i =0; i< gridView1.RowCount; i++)
            {
                if (decimal.TryParse(gridView1.GetRowCellValue(i,"REFUNDFEE").ToString(), out dec_temp))
                {   if (dec_temp <= 0) continue;
                    itemIdList.Add(gridView1.GetRowCellValue(i, "SA004").ToString());
                    priceList.Add(Convert.ToDecimal(gridView1.GetRowCellValue(i, "REFUNDFEE")));
                    numsList.Add(-1);
                    itemTypeList.Add(gridView1.GetRowCellValue(i,"SA002").ToString());
                    dec_fin_sum +=  Convert.ToDecimal(gridView1.GetRowCellValue(i, "REFUNDFEE"));   
                }
            }
            if(numsList.Count <= 0)
            {
                XtraMessageBox.Show("还未选择退费的项目!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

            string s_fa001 = Tools.GetEntityPK("FA01");
            string s_memo = te_memo.Text;


            try
            {
                int re = MiscAction.FinRefundSettle(s_fa001, itemIdList.ToArray(), itemTypeList.ToArray(), priceList.ToArray(), numsList.ToArray(), Envior.cur_userId, s_memo, s_sa010);

                if (re > 0)
                {
                    XtraMessageBox.Show("退费结算完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string s_fa190 = SqlAssist.ExecuteScalar("select fa190 from fa01 where fa001='" + s_sa010 + "'").ToString();
                    if( s_fa190.Substring(0,1) == "1")  //原收费记录  财政发票已开
                    {
                        string s_pjlx = string.Empty;
                        string s_pjh = string.Empty;
                        string s_zch = string.Empty;
                        string s_invcode = string.Empty;
                        OracleDataReader reader_fin = SqlAssist.ExecuteReader("select * from fin_log where settleId='" + s_sa010 + "'");
                        {
                            reader_fin.Read();
                            s_pjlx = reader_fin["INVOICEKIND"].ToString();
                            s_pjh = reader_fin["INVOICENO"].ToString();
                            s_zch = reader_fin["INVOICEZCH"].ToString();
                            if (string.IsNullOrEmpty(s_zch))
                            {
                                Frm_Zch_input frm_zch = new Frm_Zch_input();
                                if(frm_zch.ShowDialog() == DialogResult.OK)
                                {
                                    s_zch = frm_zch.swapdata["zch"].ToString();
                                }
                                frm_zch.Dispose();
                            }
                        }
                        reader_fin.Dispose();
                            
                        StringBuilder sb_content = new StringBuilder();
                        decimal dec_fee = decimal.Zero;
                        ///获取原票据号、类型成功!!!
                        if (!string.IsNullOrEmpty(s_zch) && !string.IsNullOrEmpty(s_pjlx) && !string.IsNullOrEmpty(s_pjh))
                        {
                            for(int i = 0 ; i < itemIdList.Count;i++ )
                            {
                                //if (MiscAction.GetItemInvoiceType(itemIdList[i]) != "F") continue;
                                s_invcode = MiscAction.GetItemInvoiceCode(itemTypeList[i], itemIdList[i]);
                                dec_fee = Math.Abs(priceList[i] * numsList[i]);
                                sb_content.Append(s_invcode + "	" + dec_fee.ToString() + "	");
                            }

                            //if (!Envior.FIN_READY)
                            //    XtraMessageBox.Show("未连接到博思开票服务器!请稍后补开!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //else
                            //{
                            //    string s_newpjh = FinInvoice.GetCurrentPh(Envior.FIN_INVOICE_TYPE);
                            //    if (String.IsNullOrEmpty(s_newpjh))
                            //        XtraMessageBox.Show("未获取到下一张财政发票号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    else
                            //    {
                            //        if (XtraMessageBox.Show("下一张财政发票号码:" + s_newpjh + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            //        {
                            //            FinInvoice.Refund(s_pjlx, s_pjh, s_zch, sb_content.ToString(), "F_Qt1 = xxx | F_Qt2 = xxx | F_Qt3 = xxx", s_fa001, s_newpjh, dec_fin_sum);
                            //        }
                            //    }
                            //}
                        }
                    }
                    //else if (Math.Abs(dec_tax_sum) > 0 && s_fa190.Substring(1, 1) == "1")  //退费项目含税票项目 并且税务发票已开
                    //{
                    //    //获取税务客户信息
                    //    Frm_TaxClientInfo frm_taxClient = new Frm_TaxClientInfo();
                    //    if (frm_taxClient.ShowDialog() == DialogResult.OK)
                    //    {
                    //        TaxClientInfo clientInfo = frm_taxClient.swapdata["taxclientinfo"] as TaxClientInfo;
                    //        if (TaxInvoice.GetNextInvoiceNo() > 0)
                    //        {
                    //            if (XtraMessageBox.Show("下一张税票代码:" + Envior.NEXT_BILL_CODE + "\r\n" + "票号:" + Envior.NEXT_BILL_NUM + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //            {
                    //                TaxInvoice.Invoice(s_fa001, clientInfo);
                    //            }
                    //        }
                    //    }
                    //}
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.ToString(),"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if(e.Column.FieldName.ToUpper() == "SA020")
            {
                if (e.Value.ToString() == "T")
                    e.DisplayText = "税票";
                else
                    e.DisplayText = "财政票";
            }
        }
    }
}