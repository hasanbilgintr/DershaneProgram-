namespace WindowsFormsApplication2
{
    partial class Frm_OgrtAta
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
            this.label1 = new System.Windows.Forms.Label();
            this.CmbBx_Ogrtler = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_OgrtAta = new System.Windows.Forms.Button();
            this.CmbBx_Saat = new System.Windows.Forms.ComboBox();
            this.CmbBx_Haftaicison = new System.Windows.Forms.ComboBox();
            this.CmbBx_Kurs = new System.Windows.Forms.ComboBox();
            this.cmbBx_egitimtipi = new System.Windows.Forms.ComboBox();
            this.Lbl_Sinif = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(267, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sınıf :";
            // 
            // CmbBx_Ogrtler
            // 
            this.CmbBx_Ogrtler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBx_Ogrtler.FormattingEnabled = true;
            this.CmbBx_Ogrtler.Location = new System.Drawing.Point(306, 84);
            this.CmbBx_Ogrtler.Name = "CmbBx_Ogrtler";
            this.CmbBx_Ogrtler.Size = new System.Drawing.Size(100, 21);
            this.CmbBx_Ogrtler.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Btn_OgrtAta);
            this.groupBox1.Controls.Add(this.CmbBx_Saat);
            this.groupBox1.Controls.Add(this.CmbBx_Haftaicison);
            this.groupBox1.Controls.Add(this.CmbBx_Kurs);
            this.groupBox1.Controls.Add(this.cmbBx_egitimtipi);
            this.groupBox1.Controls.Add(this.Lbl_Sinif);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CmbBx_Ogrtler);
            this.groupBox1.Location = new System.Drawing.Point(19, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 234);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // Btn_OgrtAta
            // 
            this.Btn_OgrtAta.Location = new System.Drawing.Point(244, 175);
            this.Btn_OgrtAta.Name = "Btn_OgrtAta";
            this.Btn_OgrtAta.Size = new System.Drawing.Size(162, 23);
            this.Btn_OgrtAta.TabIndex = 8;
            this.Btn_OgrtAta.Text = "Öğretmen Ata";
            this.Btn_OgrtAta.UseVisualStyleBackColor = true;
            this.Btn_OgrtAta.Click += new System.EventHandler(this.Btn_OgrtAta_Click);
            // 
            // CmbBx_Saat
            // 
            this.CmbBx_Saat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBx_Saat.FormattingEnabled = true;
            this.CmbBx_Saat.Items.AddRange(new object[] {
            "Sabah",
            "Öğlen",
            "Akşam"});
            this.CmbBx_Saat.Location = new System.Drawing.Point(85, 170);
            this.CmbBx_Saat.Name = "CmbBx_Saat";
            this.CmbBx_Saat.Size = new System.Drawing.Size(111, 21);
            this.CmbBx_Saat.TabIndex = 7;
            // 
            // CmbBx_Haftaicison
            // 
            this.CmbBx_Haftaicison.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBx_Haftaicison.FormattingEnabled = true;
            this.CmbBx_Haftaicison.Items.AddRange(new object[] {
            "Haftaiçi",
            "Haftasonu"});
            this.CmbBx_Haftaicison.Location = new System.Drawing.Point(85, 127);
            this.CmbBx_Haftaicison.Name = "CmbBx_Haftaicison";
            this.CmbBx_Haftaicison.Size = new System.Drawing.Size(111, 21);
            this.CmbBx_Haftaicison.TabIndex = 6;
            // 
            // CmbBx_Kurs
            // 
            this.CmbBx_Kurs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBx_Kurs.FormattingEnabled = true;
            this.CmbBx_Kurs.Items.AddRange(new object[] {
            "Sistem",
            "Yazılım",
            "Network"});
            this.CmbBx_Kurs.Location = new System.Drawing.Point(85, 84);
            this.CmbBx_Kurs.Name = "CmbBx_Kurs";
            this.CmbBx_Kurs.Size = new System.Drawing.Size(111, 21);
            this.CmbBx_Kurs.TabIndex = 5;
            this.CmbBx_Kurs.SelectedIndexChanged += new System.EventHandler(this.CmbBx_Kurs_SelectedIndexChanged);
            // 
            // cmbBx_egitimtipi
            // 
            this.cmbBx_egitimtipi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBx_egitimtipi.FormattingEnabled = true;
            this.cmbBx_egitimtipi.Items.AddRange(new object[] {
            "İşkur",
            "Bireysel"});
            this.cmbBx_egitimtipi.Location = new System.Drawing.Point(84, 41);
            this.cmbBx_egitimtipi.Name = "cmbBx_egitimtipi";
            this.cmbBx_egitimtipi.Size = new System.Drawing.Size(112, 21);
            this.cmbBx_egitimtipi.TabIndex = 4;
            // 
            // Lbl_Sinif
            // 
            this.Lbl_Sinif.AutoSize = true;
            this.Lbl_Sinif.Location = new System.Drawing.Point(306, 44);
            this.Lbl_Sinif.Name = "Lbl_Sinif";
            this.Lbl_Sinif.Size = new System.Drawing.Size(13, 13);
            this.Lbl_Sinif.TabIndex = 0;
            this.Lbl_Sinif.Text = "--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Öğretmen :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Saat :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Haftaiçi/Sonu :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Kurs :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Eğitim Tipi :";
            // 
            // Frm_OgrtAta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 269);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_OgrtAta";
            this.Text = "Öğretmen Atama";
            this.Load += new System.EventHandler(this.Frm_OgrtAta_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CmbBx_Ogrtler;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox CmbBx_Saat;
        public System.Windows.Forms.ComboBox CmbBx_Haftaicison;
        public System.Windows.Forms.ComboBox CmbBx_Kurs;
        public System.Windows.Forms.ComboBox cmbBx_egitimtipi;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Btn_OgrtAta;
        public System.Windows.Forms.Label Lbl_Sinif;
    }
}