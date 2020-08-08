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
using White.DataSet;
using White.Forms;
using White.Action;
using DevExpress.XtraGrid.Views.Base;
using White.Misc;
using White.Domain;
using Oracle.ManagedDataAccess.Client;
using White.Dao;

namespace White.BusinessObject
{
	public partial class TempSales : BaseBusiness
	{
		FireBusiness_ds business_ds = new FireBusiness_ds();
		decimal dec_fin = new decimal(0);
		decimal dec_tax = new decimal(0);
		Tu01 tu01 = null;
		

		public TempSales()
		{
			InitializeComponent();	
		}

		private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_business01 frm_1 = new Frm_business01();
			frm_1.swapdata["dataset"] = business_ds;
			frm_1.swapdata["SALESTYPE"] = "1";

			DialogResult result = frm_1.ShowDialog();
			if (result == DialogResult.OK)
			{
				string s_itemId = frm_1.swapdata["ITEMID"].ToString();
				DataRow dr = business_ds.Sa01.Rows.Add();
				dr["SA003"] = MiscAction.GetItemFullName(s_itemId);
				dr["SA002"] = "01";																	 //类型：守灵厅
				dr["SA004"] = s_itemId;
				dr["PRICE"] = MiscAction.GetItemFixPrice(s_itemId);									 //单价
				dr["SA005"] = "1";																	 //临时性销售
				dr["NUMS"] = Convert.ToDecimal(frm_1.swapdata["NUMS"]);								 //数量
				dr["SA007"] = Convert.ToDecimal(dr["PRICE"]) * Convert.ToDecimal(dr["NUMS"]);        //金额
				dr["SA020"] = "F";

				dr.EndEdit();
			}
			frm_1.Dispose();
			this.CalcSum();
		}

		private void TempSales_Load(object sender, EventArgs e)
		{
			gridControl1.DataSource = business_ds.Sa01;
		}

		private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if(e.Column.FieldName.ToUpper() == "SA020")
			{
				if (e.Value.ToString() == "F")
					e.DisplayText = "财政票";
				else if (e.Value.ToString() == "T")
					e.DisplayText = "税票";
			}
		}

		
		/// <summary>
		/// 新增 冷藏
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_business02 frm_1 = new Frm_business02();
			frm_1.swapdata["dataset"] = business_ds;
			frm_1.swapdata["SALESTYPE"] = "1";

			DialogResult result = frm_1.ShowDialog();
			if (result == DialogResult.OK)
			{
				string s_itemId = frm_1.swapdata["ITEMID"].ToString();
				DataRow dr = business_ds.Sa01.Rows.Add();
				dr["SA003"] = MiscAction.GetItemFullName(s_itemId);
				dr["SA002"] = "02";                                                                  //类型：冷藏柜
				dr["SA004"] = s_itemId;
				dr["PRICE"] = MiscAction.GetItemFixPrice(s_itemId);                                  //单价
				dr["SA005"] = "1";                                                                   //临时性销售
				dr["NUMS"] = Convert.ToDecimal(frm_1.swapdata["NUMS"]);                              //数量
				dr["SA007"] = Convert.ToDecimal(dr["PRICE"]) * Convert.ToDecimal(dr["NUMS"]);        //金额
				dr["SA020"] = "F";

				dr.EndEdit();
			}
			frm_1.Dispose();
			this.CalcSum();
		}

		private void CalcSum()
		{
			dec_fin = 0;
			dec_tax = 0;
			foreach (DataRow dr in business_ds.Sa01.Rows)
			{
				if (dr["SA020"].ToString() == "F")
					dec_fin += Convert.ToDecimal(dr["SA007"]);
				else if (dr["SA020"].ToString() == "T")
					dec_tax += Convert.ToDecimal(dr["SA007"]);
			}

			te_fin_sum.Text = dec_fin.ToString("##,###.00");
			te_tax_sum.Text = dec_tax.ToString("##,###.00");
		}

		private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_business03 frm_1 = new Frm_business03();
			frm_1.swapdata["dataset"] = business_ds;
			frm_1.swapdata["SALESTYPE"] = "1";

			DialogResult result = frm_1.ShowDialog();
			if (result == DialogResult.OK)
			{
				List<string> itemId_list = frm_1.swapdata["xxs"] as List<string>;
				for (int i = 0; i < itemId_list.Count; i++)
				{
					DataRow dr = business_ds.Sa01.Rows.Add();
					dr["SA003"] = MiscAction.GetItemFullName(itemId_list[i]);
					dr["SA002"] = "03";
					dr["SA004"] = itemId_list[i];
					dr["PRICE"] = MiscAction.GetItemFixPrice(itemId_list[i]);
					dr["SA005"] = "1";
					dr["NUMS"] = 1;
					dr["SA007"] = dr["PRICE"];
					dr["SA020"] = MiscAction.GetItemInvoiceType(itemId_list[i]);
					dr.EndEdit();
				}
			}
			frm_1.Dispose();
			this.CalcSum();
		}

		/// <summary>
		/// 告别办理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_business04 frm_1 = new Frm_business04();
			frm_1.swapdata["dataset"] = business_ds;
			frm_1.swapdata["SALESTYPE"] = "1";

			DialogResult result = frm_1.ShowDialog();
			if (result == DialogResult.OK)
			{
				string s_itemId = frm_1.swapdata["ITEMID"].ToString();
				DataRow dr = business_ds.Sa01.Rows.Add();
				dr["SA003"] = MiscAction.GetItemFullName(s_itemId);
				dr["SA002"] = "04";                                                                  //类型：告别
				dr["SA004"] = s_itemId;
				dr["PRICE"] = MiscAction.GetItemFixPrice(s_itemId);                                  //单价
				dr["SA005"] = "1";                                                                   //临时性销售
				dr["NUMS"] = 1;																		 //数量
				dr["SA007"] = Convert.ToDecimal(dr["PRICE"]) ;										 //金额
				dr["SA020"] = MiscAction.GetItemInvoiceType(s_itemId);								 //票别

				dr.EndEdit();
			}
			frm_1.Dispose();
			this.CalcSum();
		}
		/// <summary>
		/// 灵车办理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_business07 frm_1 = new Frm_business07();
			frm_1.swapdata["dataset"] = business_ds;
			frm_1.swapdata["SALESTYPE"] = "1";

			DialogResult result = frm_1.ShowDialog();
			if (result == DialogResult.OK)
			{
				string s_itemId = frm_1.swapdata["ITEMID"].ToString();
				DataRow dr = business_ds.Sa01.Rows.Add();
				dr["SA003"] = MiscAction.GetItemFullName(s_itemId);
				dr["SA002"] = "07";                                                                  //类型：灵车
				dr["SA004"] = s_itemId;
				dr["PRICE"] = MiscAction.GetItemFixPrice(s_itemId);                                  //单价
				dr["SA005"] = "1";                                                                   //临时性销售
				dr["NUMS"] = 1;                                                                      //数量
				dr["SA007"] = Convert.ToDecimal(dr["PRICE"]);                                        //金额
				dr["SA020"] = "F";																	 //票别

				dr.EndEdit();
			}
			frm_1.Dispose();
			this.CalcSum();
		}

		

		/// <summary>
		/// 服务及商品
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_businessMisc frm_misc = new Frm_businessMisc();
			frm_misc.swapdata["SALESTYPE"] = "1";
			frm_misc.swapdata["dataset"] = business_ds;

			DialogResult result = frm_misc.ShowDialog();
			if (result == DialogResult.OK)
			{
				List<string> itemId_list = frm_misc.swapdata["itemIdList"] as List<string>;
				List<string> itemType_list = frm_misc.swapdata["itemTypeList"] as List<string>;
				List<decimal> price_list = frm_misc.swapdata["priceList"] as List<decimal>;
				List<decimal> nums_list = frm_misc.swapdata["numsList"] as List<decimal>;
				List<string> itemInvoiceType_list = frm_misc.swapdata["itemInvoiceTypeList"] as List<string>;
				int re = 0;

				for (int i = 0; i < itemId_list.Count; i++)
				{
					if (itemType_list[i] == "10" || itemType_list[i] == "11")
					{
						re = gridView1.LocateByValue("SA002", itemType_list[i]);
						if (re > 0)
						{

							if (itemType_list[i] == "10")
							{
								if (MessageBox.Show("已经选择【骨灰盒】,是否要继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) continue;
							}
							else if (itemId_list[i] == "11")
							{
								if (MessageBox.Show("已经选择【纸棺】,是否要替换?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) continue;
							}
							gridView1.DeleteRow(re);
						}
					}

					re = gridView1.LocateByValue("SA004", itemId_list[i]);
					if (re >= 0)
					{
						if (MessageBox.Show("【" + gridView1.GetRowCellValue(re, "SA003").ToString() + "】已经存在,要替换吗?",
							"提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) continue;
						gridView1.DeleteRow(re);
					}

					DataRow dr = business_ds.Sa01.Rows.Add();
					dr["SA003"] = MiscAction.GetItemFullName(itemId_list[i]);
					dr["SA002"] = itemType_list[i];
					dr["SA004"] = itemId_list[i];
					dr["PRICE"] = price_list[i];
					dr["SA005"] = "1";
					dr["NUMS"] = nums_list[i];
					dr["SA007"] = price_list[i] * nums_list[i];
					dr["SA020"] = itemInvoiceType_list[i];
					dr.EndEdit();
				}
				//RefreshSalesData();
			}
			frm_misc.Dispose();
			this.CalcSum();
		}


		/// <summary>
		/// 双击编辑项目
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_DoubleClick(object sender, EventArgs e)
		{
			int row = -1;
			if ((row = (sender as ColumnView).FocusedRowHandle) >= 0)
			{
				this.EditItem(row);
			}
		}

		/// <summary>
		/// 编辑项目
		/// </summary>
		/// <param name="rowHandle"></param>
		private void EditItem(int rowHandle)
		{
			Frm_salesEdit frm_modi = new Frm_salesEdit();
			frm_modi.swapdata["DATAROW"] = business_ds.Sa01.Rows[gridView1.GetDataSourceRowIndex(rowHandle)];
			frm_modi.ShowDialog();
			this.CalcSum();
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
		/// 结算
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (!SaveCheck()) return;
			string s_tip = string.Empty;
			if (dec_fin > 0 && dec_tax > 0)
				s_tip = "本次结算共需要一张财政发票和一张税务发票,是否继续?";
			else if (dec_fin > 0)
				s_tip = "本次结算共需要一张财政发票,是否继续?";
			else if (dec_tax > 0)
				s_tip = "本次结算共需要一张税务发票,是否继续?";
			else
				return;

			if (XtraMessageBox.Show(s_tip, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
			if (dec_tax > 0 && !TaxInvoice.ClientIsOnline())
			{
				if (XtraMessageBox.Show("【税神通】客户端不在线!是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
			}



			string s_cuname = be_cuname.EditValue.ToString();   //交款人(或单位)
			StringBuilder sb_memo = new StringBuilder(50);

			List<string> itemId_List = new List<string>();
			List<string> itemType_List = new List<string>();
			List<decimal> prict_List = new List<decimal>();
			List<decimal> nums_List = new List<decimal>();
			for (int i = 0; i < gridView1.RowCount; i++)
			{
				itemId_List.Add(gridView1.GetRowCellValue(i, "SA004").ToString());
				itemType_List.Add(gridView1.GetRowCellValue(i, "SA002").ToString());
				prict_List.Add(decimal.Parse(gridView1.GetRowCellValue(i, "PRICE").ToString()));
				nums_List.Add(decimal.Parse(gridView1.GetRowCellValue(i, "NUMS").ToString()));
			}
			string s_fa001 = Tools.GetEntityPK("FA01");
			int re = FireAction.TempSalesSettle(
						s_cuname, s_fa001, itemId_List.ToArray(), itemType_List.ToArray(), prict_List.ToArray(), nums_List.ToArray(), Envior.cur_userId,sb_memo.ToString());
			
			//如果保存失败,则退出处理
			if (re < 0) return;

			//清理数据
			business_ds.Sa01.Rows.Clear();
			be_cuname.Text = "";
			te_fin_sum.Text = "";
			te_tax_sum.Text = "";


			XtraMessageBox.Show("结算成功!现在开始开具发票!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);

			////开财政票!
			if(dec_fin > 0)
			{
				if (!Envior.FIN_READY) 
					XtraMessageBox.Show("未连接到博思开票服务器!请稍后补开!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				else
				{
					string s_pjh = FinInvoice.GetCurrentPh(Envior.FIN_INVOICE_TYPE);
					if (String.IsNullOrEmpty(s_pjh))
						XtraMessageBox.Show("未获取到下一张财政发票号!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
					else
					{
						if(XtraMessageBox.Show("下一张财政发票号码:" + s_pjh + ",是否继续?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
						{
							FinInvoice.Invoice(s_fa001);
						}
					}
				}
			}

			//// 开税票
			if (dec_tax > 0)
			{
				//获取税务客户信息
				Frm_TaxClientInfo frm_taxClient = null;
				if (tu01 == null)
					frm_taxClient = new Frm_TaxClientInfo(s_cuname);
				else
					frm_taxClient = new Frm_TaxClientInfo(tu01);

				if (frm_taxClient.ShowDialog() != DialogResult.OK)
				{
					tu01 = null;
					return;
				}
					
				TaxClientInfo clientInfo = frm_taxClient.swapdata["taxclientinfo"] as TaxClientInfo;

				if (TaxInvoice.GetNextInvoiceNo(s_fa001) < 0)
				{
					tu01 = null;
					return;  //获取票据号失败,则退出!!!
				}
					
				if (XtraMessageBox.Show("下一张税票代码:" + Envior.NEXT_BILL_CODE + "\r\n" + "票号:" + Envior.NEXT_BILL_NUM + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					TaxInvoice.Invoice(s_fa001, clientInfo);
				}

				//////////// 保存客户信息 ///////////////
				Tu01_dao tu01_dao = new Tu01_dao();
				if (tu01 != null)
				{
					tu01.tu003 = clientInfo.InfoClientName;
					tu01.tu005 = clientInfo.InfoClientTaxCode;
					tu01.tu006 = clientInfo.infoclientaddressphone;
					tu01.tu007 = clientInfo.infoclientbankaccount;
					tu01_dao.Update(tu01);
				}
				else if (clientInfo.InfoClientName.Length >= 5)
				{
					tu01 = new Tu01();
					tu01.tu001 = Tools.GetEntityPK("TU01");
					tu01.tu003 = clientInfo.InfoClientName;
					tu01.tu005 = clientInfo.InfoClientTaxCode;
					tu01.tu006 = clientInfo.infoclientaddressphone;
					tu01.tu007 = clientInfo.infoclientbankaccount;
					tu01_dao.Insert(tu01);
				}
				tu01 = null;
			}
		}

		private bool SaveCheck()
		{
			if(business_ds.Sa01.Rows.Count <= 0)
			{
				XtraMessageBox.Show("尚未选择项目!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return false;
			}
			foreach(DataRow dr in business_ds.Sa01.Rows)
			{
				if(dr["PRICE"] is DBNull || Convert.ToDecimal(dr["PRICE"]) == 0)
				{
					XtraMessageBox.Show(dr["SA003"].ToString() + "单价不能为0!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
					return false;
				}
				if (dr["NUMS"] is DBNull || Convert.ToDecimal(dr["NUMS"]) == 0)
				{
					XtraMessageBox.Show(dr["SA003"].ToString() + "数量不能为0!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
				if (dr["SA020"] is DBNull || String.IsNullOrEmpty(dr["SA020"].ToString()))
				{
					XtraMessageBox.Show(dr["SA003"].ToString() + "未设置发票类别!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
			}

			if (string.IsNullOrEmpty(be_cuname.Text))
			{
				be_cuname.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				be_cuname.ErrorText = "请输入交款人(单位)!";
				return false;
			}
			return true;
		}

		private void be_cuname_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			Frm_SearchUnit frm_search = new Frm_SearchUnit();
			DialogResult result = frm_search.ShowDialog();
			if(result == DialogResult.OK)
			{
				tu01 = frm_search.swapdata["TU01"] as Tu01;
				be_cuname.Text = tu01.tu003;
			}
			frm_search.Dispose();
		}

		/// <summary>
		/// 删除项目
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;

			if (rowHandle <= 0)
			{
				XtraMessageBox.Show("请先选择要删除的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			gridView1.DeleteRow(rowHandle);
		}
	}
}
