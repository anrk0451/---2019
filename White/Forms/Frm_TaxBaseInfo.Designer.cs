namespace White.Forms
{
    partial class Frm_TaxBaseInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.te_id = new DevExpress.XtraEditors.TextEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.te_addr_tele = new DevExpress.XtraEditors.TextEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.te_bank_account = new DevExpress.XtraEditors.TextEdit();
			this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
			this.combo_type = new DevExpress.XtraEditors.ComboBoxEdit();
			this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
			this.te_publickey = new DevExpress.XtraEditors.TextEdit();
			this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
			this.te_privatekey = new DevExpress.XtraEditors.TextEdit();
			this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
			this.te_url = new DevExpress.XtraEditors.TextEdit();
			this.te_checker = new DevExpress.XtraEditors.TextEdit();
			this.te_cashier = new DevExpress.XtraEditors.TextEdit();
			this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.te_id.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_addr_tele.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_bank_account.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.combo_type.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_publickey.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_privatekey.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_url.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_checker.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_cashier.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.Image = null;
			this.labelControl1.AppearanceDisabled.Image = null;
			this.labelControl1.AppearanceHovered.Image = null;
			this.labelControl1.AppearancePressed.Image = null;
			this.labelControl1.Location = new System.Drawing.Point(89, 92);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(114, 22);
			this.labelControl1.TabIndex = 0;
			this.labelControl1.Text = "纳税人识别号:";
			// 
			// te_id
			// 
			this.te_id.Location = new System.Drawing.Point(248, 87);
			this.te_id.Name = "te_id";
			this.te_id.Size = new System.Drawing.Size(377, 28);
			this.te_id.TabIndex = 1;
			// 
			// labelControl2
			// 
			this.labelControl2.Appearance.Image = null;
			this.labelControl2.AppearanceDisabled.Image = null;
			this.labelControl2.AppearanceHovered.Image = null;
			this.labelControl2.AppearancePressed.Image = null;
			this.labelControl2.Location = new System.Drawing.Point(89, 149);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(114, 22);
			this.labelControl2.TabIndex = 2;
			this.labelControl2.Text = "单位地址电话:";
			// 
			// te_addr_tele
			// 
			this.te_addr_tele.Location = new System.Drawing.Point(248, 146);
			this.te_addr_tele.Name = "te_addr_tele";
			this.te_addr_tele.Size = new System.Drawing.Size(377, 28);
			this.te_addr_tele.TabIndex = 3;
			// 
			// labelControl3
			// 
			this.labelControl3.Appearance.Image = null;
			this.labelControl3.AppearanceDisabled.Image = null;
			this.labelControl3.AppearanceHovered.Image = null;
			this.labelControl3.AppearancePressed.Image = null;
			this.labelControl3.Location = new System.Drawing.Point(89, 206);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(114, 22);
			this.labelControl3.TabIndex = 4;
			this.labelControl3.Text = "单位银行账号:";
			// 
			// te_bank_account
			// 
			this.te_bank_account.Location = new System.Drawing.Point(248, 202);
			this.te_bank_account.Name = "te_bank_account";
			this.te_bank_account.Size = new System.Drawing.Size(377, 28);
			this.te_bank_account.TabIndex = 5;
			// 
			// labelControl5
			// 
			this.labelControl5.Appearance.Image = null;
			this.labelControl5.AppearanceDisabled.Image = null;
			this.labelControl5.AppearanceHovered.Image = null;
			this.labelControl5.AppearancePressed.Image = null;
			this.labelControl5.Location = new System.Drawing.Point(89, 264);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(78, 22);
			this.labelControl5.TabIndex = 8;
			this.labelControl5.Text = "发票类型:";
			// 
			// combo_type
			// 
			this.combo_type.Location = new System.Drawing.Point(248, 260);
			this.combo_type.Name = "combo_type";
			this.combo_type.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.combo_type.Properties.Items.AddRange(new object[] {
            "普票",
            "专票",
            "电子票"});
			this.combo_type.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.combo_type.Size = new System.Drawing.Size(377, 28);
			this.combo_type.TabIndex = 9;
			// 
			// simpleButton2
			// 
			this.simpleButton2.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.simpleButton2.Appearance.ForeColor = System.Drawing.Color.White;
			this.simpleButton2.Appearance.Options.UseBackColor = true;
			this.simpleButton2.Appearance.Options.UseForeColor = true;
			this.simpleButton2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.simpleButton2.Location = new System.Drawing.Point(657, 89);
			this.simpleButton2.LookAndFeel.UseDefaultLookAndFeel = false;
			this.simpleButton2.Name = "simpleButton2";
			this.simpleButton2.Size = new System.Drawing.Size(162, 34);
			this.simpleButton2.TabIndex = 12;
			this.simpleButton2.Text = "取消";
			this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
			// 
			// simpleButton1
			// 
			this.simpleButton1.Appearance.BackColor = System.Drawing.Color.Lime;
			this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButton1.Appearance.Options.UseBackColor = true;
			this.simpleButton1.Appearance.Options.UseForeColor = true;
			this.simpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.simpleButton1.Location = new System.Drawing.Point(657, 37);
			this.simpleButton1.LookAndFeel.UseDefaultLookAndFeel = false;
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(162, 34);
			this.simpleButton1.TabIndex = 11;
			this.simpleButton1.Text = "确定";
			this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
			// 
			// labelControl6
			// 
			this.labelControl6.Appearance.Image = null;
			this.labelControl6.AppearanceDisabled.Image = null;
			this.labelControl6.AppearanceHovered.Image = null;
			this.labelControl6.AppearancePressed.Image = null;
			this.labelControl6.Location = new System.Drawing.Point(89, 323);
			this.labelControl6.Name = "labelControl6";
			this.labelControl6.Size = new System.Drawing.Size(42, 22);
			this.labelControl6.TabIndex = 13;
			this.labelControl6.Text = "公钥:";
			// 
			// te_publickey
			// 
			this.te_publickey.Location = new System.Drawing.Point(248, 317);
			this.te_publickey.Name = "te_publickey";
			this.te_publickey.Size = new System.Drawing.Size(377, 28);
			this.te_publickey.TabIndex = 14;
			// 
			// labelControl7
			// 
			this.labelControl7.Appearance.Image = null;
			this.labelControl7.AppearanceDisabled.Image = null;
			this.labelControl7.AppearanceHovered.Image = null;
			this.labelControl7.AppearancePressed.Image = null;
			this.labelControl7.Location = new System.Drawing.Point(89, 375);
			this.labelControl7.Name = "labelControl7";
			this.labelControl7.Size = new System.Drawing.Size(42, 22);
			this.labelControl7.TabIndex = 15;
			this.labelControl7.Text = "私钥:";
			// 
			// te_privatekey
			// 
			this.te_privatekey.Location = new System.Drawing.Point(248, 371);
			this.te_privatekey.Name = "te_privatekey";
			this.te_privatekey.Size = new System.Drawing.Size(377, 28);
			this.te_privatekey.TabIndex = 16;
			// 
			// labelControl8
			// 
			this.labelControl8.Appearance.Image = null;
			this.labelControl8.AppearanceDisabled.Image = null;
			this.labelControl8.AppearanceHovered.Image = null;
			this.labelControl8.AppearancePressed.Image = null;
			this.labelControl8.Location = new System.Drawing.Point(89, 35);
			this.labelControl8.Name = "labelControl8";
			this.labelControl8.Size = new System.Drawing.Size(146, 22);
			this.labelControl8.TabIndex = 17;
			this.labelControl8.Text = "税务发票服务URL:";
			// 
			// te_url
			// 
			this.te_url.Location = new System.Drawing.Point(248, 31);
			this.te_url.Name = "te_url";
			this.te_url.Size = new System.Drawing.Size(377, 28);
			this.te_url.TabIndex = 18;
			// 
			// te_checker
			// 
			this.te_checker.Location = new System.Drawing.Point(248, 480);
			this.te_checker.Name = "te_checker";
			this.te_checker.Size = new System.Drawing.Size(377, 28);
			this.te_checker.TabIndex = 26;
			// 
			// te_cashier
			// 
			this.te_cashier.Location = new System.Drawing.Point(248, 425);
			this.te_cashier.Name = "te_cashier";
			this.te_cashier.Size = new System.Drawing.Size(377, 28);
			this.te_cashier.TabIndex = 25;
			// 
			// labelControl9
			// 
			this.labelControl9.Appearance.Image = null;
			this.labelControl9.AppearanceDisabled.Image = null;
			this.labelControl9.AppearanceHovered.Image = null;
			this.labelControl9.AppearancePressed.Image = null;
			this.labelControl9.Location = new System.Drawing.Point(89, 483);
			this.labelControl9.Name = "labelControl9";
			this.labelControl9.Size = new System.Drawing.Size(60, 22);
			this.labelControl9.TabIndex = 24;
			this.labelControl9.Text = "审核人:";
			// 
			// labelControl4
			// 
			this.labelControl4.Appearance.Image = null;
			this.labelControl4.AppearanceDisabled.Image = null;
			this.labelControl4.AppearanceHovered.Image = null;
			this.labelControl4.AppearancePressed.Image = null;
			this.labelControl4.Location = new System.Drawing.Point(89, 428);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(60, 22);
			this.labelControl4.TabIndex = 23;
			this.labelControl4.Text = "收款人:";
			// 
			// Frm_TaxBaseInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(847, 547);
			this.Controls.Add(this.te_checker);
			this.Controls.Add(this.te_cashier);
			this.Controls.Add(this.labelControl9);
			this.Controls.Add(this.labelControl4);
			this.Controls.Add(this.te_url);
			this.Controls.Add(this.labelControl8);
			this.Controls.Add(this.te_privatekey);
			this.Controls.Add(this.labelControl7);
			this.Controls.Add(this.te_publickey);
			this.Controls.Add(this.labelControl6);
			this.Controls.Add(this.simpleButton2);
			this.Controls.Add(this.simpleButton1);
			this.Controls.Add(this.combo_type);
			this.Controls.Add(this.labelControl5);
			this.Controls.Add(this.te_bank_account);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.te_addr_tele);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.te_id);
			this.Controls.Add(this.labelControl1);
			this.Name = "Frm_TaxBaseInfo";
			this.Text = "税务发票基础信息";
			this.Load += new System.EventHandler(this.Frm_TaxBaseInfo_Load);
			((System.ComponentModel.ISupportInitialize)(this.te_id.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_addr_tele.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_bank_account.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.combo_type.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_publickey.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_privatekey.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_url.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_checker.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_cashier.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit te_id;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit te_addr_tele;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit te_bank_account;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ComboBoxEdit combo_type;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit te_publickey;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit te_privatekey;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit te_url;
		private DevExpress.XtraEditors.TextEdit te_checker;
		private DevExpress.XtraEditors.TextEdit te_cashier;
		private DevExpress.XtraEditors.LabelControl labelControl9;
		private DevExpress.XtraEditors.LabelControl labelControl4;
	}
}