namespace White.Forms
{
    partial class Frm_AdjustRegisterDate
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
            this.b_exit = new DevExpress.XtraEditors.SimpleButton();
            this.b_ok = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.de_end = new DevExpress.XtraEditors.DateEdit();
            this.de_begin = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtedit_pos = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtEdit_rc003 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.de_end.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_end.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_begin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_begin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_pos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc003.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // b_exit
            // 
            this.b_exit.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.b_exit.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.b_exit.Appearance.ForeColor = System.Drawing.Color.White;
            this.b_exit.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.b_exit.Appearance.Options.UseBackColor = true;
            this.b_exit.Appearance.Options.UseFont = true;
            this.b_exit.Appearance.Options.UseForeColor = true;
            this.b_exit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.b_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_exit.Location = new System.Drawing.Point(203, 198);
            this.b_exit.Name = "b_exit";
            this.b_exit.Size = new System.Drawing.Size(63, 31);
            this.b_exit.TabIndex = 84;
            this.b_exit.Text = "退出";
            this.b_exit.Click += new System.EventHandler(this.b_exit_Click);
            // 
            // b_ok
            // 
            this.b_ok.Appearance.BackColor = System.Drawing.Color.Lime;
            this.b_ok.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.b_ok.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.b_ok.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.b_ok.Appearance.Options.UseBackColor = true;
            this.b_ok.Appearance.Options.UseFont = true;
            this.b_ok.Appearance.Options.UseForeColor = true;
            this.b_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.b_ok.Location = new System.Drawing.Point(27, 198);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(170, 31);
            this.b_ok.TabIndex = 83;
            this.b_ok.Text = "确定";
            this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl4.Appearance.Image = null;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.AppearanceDisabled.Image = null;
            this.labelControl4.AppearanceHovered.Image = null;
            this.labelControl4.AppearancePressed.Image = null;
            this.labelControl4.Location = new System.Drawing.Point(261, 137);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(15, 15);
            this.labelControl4.TabIndex = 82;
            this.labelControl4.Text = "至";
            // 
            // de_end
            // 
            this.de_end.EditValue = null;
            this.de_end.Location = new System.Drawing.Point(287, 133);
            this.de_end.Name = "de_end";
            this.de_end.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.de_end.Properties.Appearance.Options.UseFont = true;
            this.de_end.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_end.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_end.Size = new System.Drawing.Size(125, 22);
            this.de_end.TabIndex = 81;
            // 
            // de_begin
            // 
            this.de_begin.EditValue = null;
            this.de_begin.Location = new System.Drawing.Point(128, 133);
            this.de_begin.Name = "de_begin";
            this.de_begin.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.de_begin.Properties.Appearance.Options.UseFont = true;
            this.de_begin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_begin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_begin.Size = new System.Drawing.Size(125, 22);
            this.de_begin.TabIndex = 80;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl3.Appearance.Image = null;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.AppearanceDisabled.Image = null;
            this.labelControl3.AppearanceHovered.Image = null;
            this.labelControl3.AppearancePressed.Image = null;
            this.labelControl3.Location = new System.Drawing.Point(27, 136);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(68, 15);
            this.labelControl3.TabIndex = 79;
            this.labelControl3.Text = "寄存日期:";
            // 
            // txtedit_pos
            // 
            this.txtedit_pos.Location = new System.Drawing.Point(128, 78);
            this.txtedit_pos.Name = "txtedit_pos";
            this.txtedit_pos.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtedit_pos.Properties.Appearance.Options.UseFont = true;
            this.txtedit_pos.Properties.ReadOnly = true;
            this.txtedit_pos.Size = new System.Drawing.Size(364, 22);
            this.txtedit_pos.TabIndex = 78;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Appearance.Image = null;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.AppearanceDisabled.Image = null;
            this.labelControl2.AppearanceHovered.Image = null;
            this.labelControl2.AppearancePressed.Image = null;
            this.labelControl2.Location = new System.Drawing.Point(27, 82);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(68, 15);
            this.labelControl2.TabIndex = 77;
            this.labelControl2.Text = "寄存位置:";
            // 
            // txtEdit_rc003
            // 
            this.txtEdit_rc003.Location = new System.Drawing.Point(128, 27);
            this.txtEdit_rc003.Name = "txtEdit_rc003";
            this.txtEdit_rc003.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEdit_rc003.Properties.Appearance.Options.UseFont = true;
            this.txtEdit_rc003.Properties.ReadOnly = true;
            this.txtEdit_rc003.Size = new System.Drawing.Size(125, 22);
            this.txtEdit_rc003.TabIndex = 76;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Appearance.Image = null;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.AppearanceDisabled.Image = null;
            this.labelControl1.AppearanceHovered.Image = null;
            this.labelControl1.AppearancePressed.Image = null;
            this.labelControl1.Location = new System.Drawing.Point(27, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 15);
            this.labelControl1.TabIndex = 75;
            this.labelControl1.Text = "逝者姓名:";
            // 
            // Frm_AdjustRegisterDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 257);
            this.Controls.Add(this.b_exit);
            this.Controls.Add(this.b_ok);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.de_end);
            this.Controls.Add(this.de_begin);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtedit_pos);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtEdit_rc003);
            this.Controls.Add(this.labelControl1);
            this.Name = "Frm_AdjustRegisterDate";
            this.Text = "调整寄存日期";
            this.Load += new System.EventHandler(this.Frm_AdjustRegisterDate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.de_end.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_end.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_begin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_begin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_pos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc003.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton b_exit;
        private DevExpress.XtraEditors.SimpleButton b_ok;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit de_end;
        private DevExpress.XtraEditors.DateEdit de_begin;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtedit_pos;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtEdit_rc003;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}