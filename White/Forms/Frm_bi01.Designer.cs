namespace White.Forms
{
	partial class Frm_bi01
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
			this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.te_bi003 = new DevExpress.XtraEditors.TextEdit();
			this.te_price = new DevExpress.XtraEditors.TextEdit();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.te_bi003.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_price.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// simpleButton2
			// 
			this.simpleButton2.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.simpleButton2.Appearance.ForeColor = System.Drawing.Color.White;
			this.simpleButton2.Appearance.Options.UseBackColor = true;
			this.simpleButton2.Appearance.Options.UseForeColor = true;
			this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.simpleButton2.Location = new System.Drawing.Point(303, 165);
			this.simpleButton2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
			this.simpleButton2.LookAndFeel.UseDefaultLookAndFeel = false;
			this.simpleButton2.Name = "simpleButton2";
			this.simpleButton2.Size = new System.Drawing.Size(80, 30);
			this.simpleButton2.TabIndex = 20;
			this.simpleButton2.Text = "取消";
			this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
			// 
			// simpleButton1
			// 
			this.simpleButton1.Appearance.BackColor = System.Drawing.Color.Lime;
			this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.White;
			this.simpleButton1.Appearance.Options.UseBackColor = true;
			this.simpleButton1.Appearance.Options.UseForeColor = true;
			this.simpleButton1.Location = new System.Drawing.Point(175, 165);
			this.simpleButton1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
			this.simpleButton1.LookAndFeel.UseDefaultLookAndFeel = false;
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(121, 30);
			this.simpleButton1.TabIndex = 19;
			this.simpleButton1.Text = "确定";
			this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Location = new System.Drawing.Point(51, 124);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(74, 22);
			this.radioButton3.TabIndex = 18;
			this.radioButton3.Text = "使无效";
			this.radioButton3.UseVisualStyleBackColor = true;
			this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
			// 
			// te_bi003
			// 
			this.te_bi003.Enabled = false;
			this.te_bi003.Location = new System.Drawing.Point(196, 72);
			this.te_bi003.Name = "te_bi003";
			this.te_bi003.Size = new System.Drawing.Size(191, 24);
			this.te_bi003.TabIndex = 17;
			this.te_bi003.Validating += new System.ComponentModel.CancelEventHandler(this.te_bi003_Validating);
			// 
			// te_price
			// 
			this.te_price.Location = new System.Drawing.Point(196, 28);
			this.te_price.Name = "te_price";
			this.te_price.Properties.Appearance.Options.UseTextOptions = true;
			this.te_price.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.te_price.Properties.Mask.EditMask = "N2";
			this.te_price.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
			this.te_price.Properties.Mask.UseMaskAsDisplayFormat = true;
			this.te_price.Size = new System.Drawing.Size(191, 24);
			this.te_price.TabIndex = 16;
			this.te_price.Validating += new System.ComponentModel.CancelEventHandler(this.te_price_Validating);
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(51, 75);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(89, 22);
			this.radioButton2.TabIndex = 15;
			this.radioButton2.Text = "修改号位";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(51, 29);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(89, 22);
			this.radioButton1.TabIndex = 14;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "修改价格";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// Frm_bi01
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(439, 223);
			this.Controls.Add(this.simpleButton2);
			this.Controls.Add(this.simpleButton1);
			this.Controls.Add(this.radioButton3);
			this.Controls.Add(this.te_bi003);
			this.Controls.Add(this.te_price);
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.radioButton1);
			this.Name = "Frm_bi01";
			this.Text = "寄存号位编辑";
			this.Load += new System.EventHandler(this.Frm_bi01_Load);
			((System.ComponentModel.ISupportInitialize)(this.te_bi003.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_price.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton simpleButton2;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
		private System.Windows.Forms.RadioButton radioButton3;
		private DevExpress.XtraEditors.TextEdit te_bi003;
		private DevExpress.XtraEditors.TextEdit te_price;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton1;
	}
}