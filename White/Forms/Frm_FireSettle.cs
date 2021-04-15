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
using White.BusinessObject;
using White.DataSet;
using White.Action;
using White.Misc;
using White.Domain;

namespace White.Forms
{
    public partial class Frm_FireSettle : BaseDialog
    {
        FireBusiness_ds business_ds = null;
        string AC001 = string.Empty;
        List<int> rowList;
        DataTable dt_source;


        public Frm_FireSettle()
        {
            InitializeComponent();
        }

        private void Frm_FireSettle_Load(object sender, EventArgs e)
        {
            AC001 = this.swapdata["AC001"].ToString();
            business_ds = this.swapdata["dataset"] as FireBusiness_ds;
            rowList = this.swapdata["rowList"] as List<int>;


            ///拷贝要结算的记录!!!
            dt_source = business_ds.Sa01.Clone();
            foreach (int i in rowList)
            {
                dt_source.Rows.Add(business_ds.Sa01.Rows[i].ItemArray);
            }

            gridControl1.DataSource = dt_source;
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

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            decimal dec_fin = new decimal(0);
            decimal dec_tax = new decimal(0);
            string s_tip = string.Empty;

            foreach(DataRow dr in dt_source.Rows)
            {
                if (dr["SA020"].ToString() == "F")
                    dec_fin += Convert.ToDecimal(dr["SA007"]);
                else if (dr["SA020"].ToString() == "T")
                    dec_tax += Convert.ToDecimal(dr["SA007"]);
            }

            if (dec_fin > 0 && dec_tax > 0)
                s_tip = "本次结算共需要一张财政发票和一张税务发票,是否继续?";
            else if (dec_fin > 0)
                s_tip = "本次结算共需要一张财政发票,是否继续?";
            else if (dec_tax > 0)
                s_tip = "本次结算共需要一张税务发票,是否继续?";
            else
                return;

            if (XtraMessageBox.Show(s_tip, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            if(dec_tax > 0 && !TaxInvoice.ClientIsOnline())
            {
                if(XtraMessageBox.Show("【税神通】客户端不在线!是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            }

            string s_fa001 = Tools.GetEntityPK("FA01");
            string s_cuname = FireAction.GetGuyNameById(AC001);
            List<string> sa001_list = new List<string>();
            foreach (DataRow r in dt_source.Rows)
            {
                sa001_list.Add(r["SA001"].ToString());
            }

            int result = FireAction.FireBusinessSettle( s_fa001,
                                                           AC001,
                                                           s_cuname,
                                                           sa001_list.ToArray(),
                                                           Envior.cur_userId 
            );

            if (result > 0)
            {
                b_ok.Enabled = false;

                int fire_row = gridView1.LocateByValue("SA002", "06");
                //如果有火化,打印火化证明
                if (fire_row >= 0)
                {   //打印火化证明
                    if (XtraMessageBox.Show("现在打印火化证明?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        PrtServAction.Print_HHZM(AC001,0);
                }

                XtraMessageBox.Show("结算成功!现在开始打印发票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////开财政票!
                if (dec_fin > 0)
                {
                    if (FinInvoice.GetCurrentPh() > 0)
                    {
                        if (XtraMessageBox.Show("下一张财政发票号码:" + Envior.FIN_NEXT_BILL_NO + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            FinInvoice.Invoice(s_fa001);
                        }
                    }
                }

                //// 开税票
                if (dec_tax > 0)
                {
                    //获取税务客户信息
                    string s_ac003 = SqlAssist.ExecuteScalar("select ac003 from ac01 where ac001='" + AC001 + "'").ToString();
                    Frm_TaxClientInfo frm_taxClient = new Frm_TaxClientInfo(s_ac003);
                    if (frm_taxClient.ShowDialog() != DialogResult.OK) return;
                    TaxClientInfo clientInfo = frm_taxClient.swapdata["taxclientinfo"] as TaxClientInfo;

                    if (TaxInvoice.GetNextInvoiceNo(s_fa001) > 0) 
                    {
                        if (XtraMessageBox.Show("下一张税票代码:" + Envior.NEXT_BILL_CODE + "\r\n" + "票号:" + Envior.NEXT_BILL_NUM + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            TaxInvoice.Invoice(s_fa001, clientInfo);
                        }
                    }
                    
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
                
            }
        }
    }
}