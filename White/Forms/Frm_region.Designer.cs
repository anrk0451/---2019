namespace White.Forms
{
	partial class Frm_region
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
			this.combo_rg033 = new System.Windows.Forms.ComboBox();
			this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
			this.combo_rg030 = new System.Windows.Forms.ComboBox();
			this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
			this.txt_rg021 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
			this.txt_rg020 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.txt_rg011 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.txt_rg010 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.txt_rg003 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
			this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
			((System.ComponentModel.ISupportInitialize)(this.txt_rg021.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txt_rg020.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txt_rg011.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txt_rg010.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txt_rg003.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// combo_rg033
			// 
			this.combo_rg033.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combo_rg033.FormattingEnabled = true;
			this.combo_rg033.Items.AddRange(new object[] {
            "顺序",
            "蛇形"});
			this.combo_rg033.Location = new System.Drawing.Point(152, 317);
			this.combo_rg033.Name = "combo_rg033";
			this.combo_rg033.Size = new System.Drawing.Size(200, 26);
			this.combo_rg033.TabIndex = 47;
			// 
			// labelControl7
			// 
			this.labelControl7.Appearance.Image = null;
			this.labelControl7.AppearanceDisabled.Image = null;
			this.labelControl7.AppearanceHovered.Image = null;
			this.labelControl7.AppearancePressed.Image = null;
			this.labelControl7.Location = new System.Drawing.Point(35, 322);
			this.labelControl7.Name = "labelControl7";
			this.labelControl7.Size = new System.Drawing.Size(64, 18);
			this.labelControl7.TabIndex = 46;
			this.labelControl7.Text = "排列方向:";
			// 
			// combo_rg030
			// 
			this.combo_rg030.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combo_rg030.FormattingEnabled = true;
			this.combo_rg030.Items.AddRange(new object[] {
            "左上",
            "左下",
            "右上",
            "右下"});
			this.combo_rg030.Location = new System.Drawing.Point(152, 274);
			this.combo_rg030.Name = "combo_rg030";
			this.combo_rg030.Size = new System.Drawing.Size(200, 26);
			this.combo_rg030.TabIndex = 45;
			// 
			// labelControl6
			// 
			this.labelControl6.Appearance.Image = null;
			this.labelControl6.AppearanceDisabled.Image = null;
			this.labelControl6.AppearanceHovered.Image = null;
			this.labelControl6.AppearancePressed.Image = null;
			this.labelControl6.Location = new System.Drawing.Point(35, 280);
			this.labelControl6.Name = "labelControl6";
			this.labelControl6.Size = new System.Drawing.Size(64, 18);
			this.labelControl6.TabIndex = 44;
			this.labelControl6.Text = "起始位置:";
			// 
			// txt_rg021
			// 
			this.txt_rg021.Location = new System.Drawing.Point(152, 233);
			this.txt_rg021.Name = "txt_rg021";
			this.txt_rg021.Properties.Mask.EditMask = "f0";
			this.txt_rg021.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
			this.txt_rg021.Size = new System.Drawing.Size(200, 24);
			this.txt_rg021.TabIndex = 43;
			this.txt_rg021.Validating += new System.ComponentModel.CancelEventHandler(this.txt_rg021_Validating);
			// 
			// labelControl5
			// 
			this.labelControl5.Appearance.Image = null;
			this.labelControl5.AppearanceDisabled.Image = null;
			this.labelControl5.AppearanceHovered.Image = null;
			this.labelControl5.AppearancePressed.Image = null;
			this.labelControl5.Location = new System.Drawing.Point(35, 238);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(79, 18);
			this.labelControl5.TabIndex = 42;
			this.labelControl5.Text = "每层号位数:";
			// 
			// txt_rg020
			// 
			this.txt_rg020.Location = new System.Drawing.Point(152, 192);
			this.txt_rg020.Name = "txt_rg020";
			this.txt_rg020.Properties.Mask.EditMask = "f0";
			this.txt_rg020.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
			this.txt_rg020.Size = new System.Drawing.Size(200, 24);
			this.txt_rg020.TabIndex = 41;
			this.txt_rg020.Validating += new System.ComponentModel.CancelEventHandler(this.txt_rg020_Validating);
			// 
			// labelControl4
			// 
			this.labelControl4.Appearance.Image = null;
			this.labelControl4.AppearanceDisabled.Image = null;
			this.labelControl4.AppearanceHovered.Image = null;
			this.labelControl4.AppearancePressed.Image = null;
			this.labelControl4.Location = new System.Drawing.Point(35, 196);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(34, 18);
			this.labelControl4.TabIndex = 40;
			this.labelControl4.Text = "层数:";
			// 
			// txt_rg011
			// 
			this.txt_rg011.Location = new System.Drawing.Point(152, 151);
			this.txt_rg011.Name = "txt_rg011";
			this.txt_rg011.Properties.Mask.EditMask = "f0";
			this.txt_rg011.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
			this.txt_rg011.Size = new System.Drawing.Size(200, 24);
			this.txt_rg011.TabIndex = 39;
			this.txt_rg011.Validating += new System.ComponentModel.CancelEventHandler(this.txt_rg011_Validating);
			// 
			// labelControl3
			// 
			this.labelControl3.Appearance.Image = null;
			this.labelControl3.AppearanceDisabled.Image = null;
			this.labelControl3.AppearanceHovered.Image = null;
			this.labelControl3.AppearancePressed.Image = null;
			this.labelControl3.Location = new System.Drawing.Point(35, 154);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(64, 18);
			this.labelControl3.TabIndex = 38;
			this.labelControl3.Text = "终止号位:";
			// 
			// txt_rg010
			// 
			this.txt_rg010.Location = new System.Drawing.Point(152, 110);
			this.txt_rg010.Name = "txt_rg010";
			this.txt_rg010.Properties.DisplayFormat.FormatString = "N0";
			this.txt_rg010.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.txt_rg010.Properties.EditFormat.FormatString = "N0";
			this.txt_rg010.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.txt_rg010.Properties.Mask.EditMask = "n0";
			this.txt_rg010.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
			this.txt_rg010.Size = new System.Drawing.Size(200, 24);
			this.txt_rg010.TabIndex = 37;
			this.txt_rg010.Validating += new System.ComponentModel.CancelEventHandler(this.txt_rg010_Validating);
			// 
			// labelControl2
			// 
			this.labelControl2.Appearance.Image = null;
			this.labelControl2.AppearanceDisabled.Image = null;
			this.labelControl2.AppearanceHovered.Image = null;
			this.labelControl2.AppearancePressed.Image = null;
			this.labelControl2.Location = new System.Drawing.Point(35, 112);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(64, 18);
			this.labelControl2.TabIndex = 36;
			this.labelControl2.Text = "起始号位:";
			// 
			// simpleButton2
			// 
			this.simpleButton2.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.simpleButton2.Appearance.ForeColor = System.Drawing.Color.White;
			this.simpleButton2.Appearance.Options.UseBackColor = true;
			this.simpleButton2.Appearance.Options.UseForeColor = true;
			this.simpleButton2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.simpleButton2.Location = new System.Drawing.Point(375, 67);
			this.simpleButton2.LookAndFeel.UseDefaultLookAndFeel = false;
			this.simpleButton2.Name = "simpleButton2";
			this.simpleButton2.Size = new System.Drawing.Size(124, 31);
			this.simpleButton2.TabIndex = 35;
			this.simpleButton2.Text = "取消";
			this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
			// 
			// simpleButton1
			// 
			this.simpleButton1.Appearance.BackColor = System.Drawing.Color.Lime;
			this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.White;
			this.simpleButton1.Appearance.Options.UseBackColor = true;
			this.simpleButton1.Appearance.Options.UseForeColor = true;
			this.simpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.simpleButton1.Location = new System.Drawing.Point(375, 28);
			this.simpleButton1.LookAndFeel.UseDefaultLookAndFeel = false;
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(124, 31);
			this.simpleButton1.TabIndex = 34;
			this.simpleButton1.Text = "确定";
			this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.Image = null;
			this.labelControl1.AppearanceDisabled.Image = null;
			this.labelControl1.AppearanceHovered.Image = null;
			this.labelControl1.AppearancePressed.Image = null;
			this.labelControl1.Location = new System.Drawing.Point(35, 70);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(49, 18);
			this.labelControl1.TabIndex = 33;
			this.labelControl1.Text = "寄存排:";
			// 
			// txt_rg003
			// 
			this.txt_rg003.Location = new System.Drawing.Point(152, 69);
			this.txt_rg003.Name = "txt_rg003";
			this.txt_rg003.Size = new System.Drawing.Size(200, 24);
			this.txt_rg003.TabIndex = 32;
			// 
			// labelControl8
			// 
			this.labelControl8.Appearance.Image = null;
			this.labelControl8.AppearanceDisabled.Image = null;
			this.labelControl8.AppearanceHovered.Image = null;
			this.labelControl8.AppearancePressed.Image = null;
			this.labelControl8.Location = new System.Drawing.Point(35, 28);
			this.labelControl8.Name = "labelControl8";
			this.labelControl8.Size = new System.Drawing.Size(64, 18);
			this.labelControl8.TabIndex = 48;
			this.labelControl8.Text = "排列方案:";
			// 
			// radioGroup1
			// 
			this.radioGroup1.EditValue = "0";
			this.radioGroup1.Location = new System.Drawing.Point(152, 28);
			this.radioGroup1.Name = "radioGroup1";
			this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
			this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "常规"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "扩展")});
			this.radioGroup1.Size = new System.Drawing.Size(200, 24);
			this.radioGroup1.TabIndex = 49;
			this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
			// 
			// Frm_region
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(524, 367);
			this.Controls.Add(this.radioGroup1);
			this.Controls.Add(this.labelControl8);
			this.Controls.Add(this.combo_rg033);
			this.Controls.Add(this.labelControl7);
			this.Controls.Add(this.combo_rg030);
			this.Controls.Add(this.labelControl6);
			this.Controls.Add(this.txt_rg021);
			this.Controls.Add(this.labelControl5);
			this.Controls.Add(this.txt_rg020);
			this.Controls.Add(this.labelControl4);
			this.Controls.Add(this.txt_rg011);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.txt_rg010);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.simpleButton2);
			this.Controls.Add(this.simpleButton1);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.txt_rg003);
			this.Name = "Frm_region";
			this.Text = "新建寄存排";
			this.Load += new System.EventHandler(this.Frm_region_Load);
			((System.ComponentModel.ISupportInitialize)(this.txt_rg021.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txt_rg020.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txt_rg011.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txt_rg010.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txt_rg003.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox combo_rg033;
		private DevExpress.XtraEditors.LabelControl labelControl7;
		private System.Windows.Forms.ComboBox combo_rg030;
		private DevExpress.XtraEditors.LabelControl labelControl6;
		private DevExpress.XtraEditors.TextEdit txt_rg021;
		private DevExpress.XtraEditors.LabelControl labelControl5;
		private DevExpress.XtraEditors.TextEdit txt_rg020;
		private DevExpress.XtraEditors.LabelControl labelControl4;
		private DevExpress.XtraEditors.TextEdit txt_rg011;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.TextEdit txt_rg010;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.SimpleButton simpleButton2;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.TextEdit txt_rg003;
		private DevExpress.XtraEditors.LabelControl labelControl8;
		private DevExpress.XtraEditors.RadioGroup radioGroup1;
	}
}