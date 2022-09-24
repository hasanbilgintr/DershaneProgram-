using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Frm_CalisanlarListesi : Form
    {
        public Frm_CalisanlarListesi()
        {
            InitializeComponent();
        }

        private void CalisanListesi()
        {
            Frm_AdminPanel frm_adm = new Frm_AdminPanel();
            frm_adm.baglanti.Open();
            SqlDataAdapter kmt = new SqlDataAdapter("Select *from Vw_Calisanlar", frm_adm.baglanti);
            DataTable tbl = new DataTable();
            kmt.Fill(tbl);
            dataGridView1.DataSource = tbl;
            frm_adm.baglanti.Close();
        }

        private void Frm_CalisanlarListesi_Load(object sender, EventArgs e)
        {
            CalisanListesi();

            //dataGridView1.Columns["id"].Width = 30;
            //dataGridView1.Columns["id"].HeaderText = "ID"; uapılcak


        }


        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Frm_AdminPanel frm_admin = (Frm_AdminPanel)Application.OpenForms["Frm_AdminPanel"];
            frm_admin.ChckBx_CalManuel.Checked = true;
            frm_admin.Btn_CalEkle.Enabled = false;
            frm_admin.Btn_CalGun.Enabled = true;
            frm_admin.Btn_CalSil.Enabled = true;
            frm_admin.calid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            frm_admin.TxtBx_calTc.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            frm_admin.TxtBx_CalisanAdiSoyadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            frm_admin.TxtBx_calcep.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            frm_admin.TxtBx_calev.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            frm_admin.TxtBx_Caleposta.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            frm_admin.CmbBx_calil.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            frm_admin.CmbBx_calilce.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            frm_admin.TxtBx_caladres.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            frm_admin.CmbBx_calcinsiyet.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            frm_admin.TxtBx_calcilt.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            frm_admin.TxtBx_calailesirano.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            frm_admin.DtTmPckr_CalDogtar.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            frm_admin.CmbBx_CalisanDepartman.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
            frm_admin.TxtBx_calpozisyon.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString();
            frm_admin.TxtBx_caluni.Text = dataGridView1.CurrentRow.Cells[15].Value.ToString();
            frm_admin.TxtBx_calokubol.Text = dataGridView1.CurrentRow.Cells[16].Value.ToString();

            if (dataGridView1.CurrentRow.Cells[17].Value.ToString().Trim() != "")
            {
                byte[] Resim = (byte[])dataGridView1.CurrentRow.Cells[17].Value;
                using (MemoryStream ms = new MemoryStream(Resim))
                {
                    frm_admin.PctrBx_Calresim.Image = Image.FromStream(ms);
                }
            }
            else
            {
                frm_admin.PctrBx_Calresim.SizeMode = PictureBoxSizeMode.StretchImage;
                frm_admin.PctrBx_Calresim.ImageLocation = Application.StartupPath + "\\Resimler\\NoFoto.png";
            }


            this.Close();
        }
    }
}
