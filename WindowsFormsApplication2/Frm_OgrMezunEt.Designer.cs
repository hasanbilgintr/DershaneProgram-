namespace WindowsFormsApplication2
{
    partial class Frm_OgrMezunEt
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
            this.LstVw_OgrMezEtList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_MezEt = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LstVw_OgrMezEtList
            // 
            this.LstVw_OgrMezEtList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.LstVw_OgrMezEtList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstVw_OgrMezEtList.FullRowSelect = true;
            this.LstVw_OgrMezEtList.GridLines = true;
            this.LstVw_OgrMezEtList.HideSelection = false;
            this.LstVw_OgrMezEtList.Location = new System.Drawing.Point(3, 16);
            this.LstVw_OgrMezEtList.Name = "LstVw_OgrMezEtList";
            this.LstVw_OgrMezEtList.Size = new System.Drawing.Size(641, 414);
            this.LstVw_OgrMezEtList.TabIndex = 0;
            this.LstVw_OgrMezEtList.UseCompatibleStateImageBehavior = false;
            this.LstVw_OgrMezEtList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "id";
            this.columnHeader1.Width = 37;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "tc";
            this.columnHeader2.Width = 79;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Ad";
            this.columnHeader3.Width = 83;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Soyad";
            this.columnHeader4.Width = 84;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Sınıf";
            this.columnHeader5.Width = 77;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Kurs";
            this.columnHeader6.Width = 87;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Öğretmen";
            this.columnHeader7.Width = 82;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "EğitimTipi";
            this.columnHeader8.Width = 88;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LstVw_OgrMezEtList);
            this.groupBox1.Location = new System.Drawing.Point(13, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(647, 433);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // Btn_MezEt
            // 
            this.Btn_MezEt.Location = new System.Drawing.Point(515, 12);
            this.Btn_MezEt.Name = "Btn_MezEt";
            this.Btn_MezEt.Size = new System.Drawing.Size(142, 29);
            this.Btn_MezEt.TabIndex = 2;
            this.Btn_MezEt.Text = "Seçili Öğrencileri Mezun Et";
            this.Btn_MezEt.UseVisualStyleBackColor = true;
            this.Btn_MezEt.Click += new System.EventHandler(this.Btn_MezEt_Click);
            // 
            // Frm_OgrMezunEt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 492);
            this.Controls.Add(this.Btn_MezEt);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_OgrMezunEt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_OgrMezunEt";
            this.Load += new System.EventHandler(this.Frm_OgrMezunEt_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        public System.Windows.Forms.ListView LstVw_OgrMezEtList;
        private System.Windows.Forms.Button Btn_MezEt;
    }
}