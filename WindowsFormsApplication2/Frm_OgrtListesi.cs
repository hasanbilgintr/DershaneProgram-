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
    public partial class Frm_OgrtListesi : Form
    {
        public Frm_OgrtListesi()
        {
            InitializeComponent();
        }

        private void Frm_OgrtListesi_Load(object sender, EventArgs e)
        {
            Frm_AdminPanel frm_adm = new Frm_AdminPanel();
            frm_adm.baglanti.Open();
            SqlDataAdapter kmt = new SqlDataAdapter("Select *from Vw_Ogretmenkayitlari", frm_adm.baglanti);
            DataTable tbl = new DataTable();
            kmt.Fill(tbl);
            GrdVw_OgrtList.DataSource = tbl;
            frm_adm.baglanti.Close();
        }

        private void GrdVw_OgrtList_DoubleClick(object sender, EventArgs e)
        {
            Frm_AdminPanel frm_admin = (Frm_AdminPanel)Application.OpenForms["Frm_AdminPanel"];
            frm_admin.ChckBx_OgretmenEPosta.Checked = true;
            frm_admin.Btn_OgretmenEkleKaydet.Enabled = false;
            frm_admin.Btn_OgretmenKaydiniGuncelle.Enabled = true;
            frm_admin.Btn_OgretmenKaydiniSil.Enabled = true;
            frm_admin.ogrtID = Convert.ToInt32(GrdVw_OgrtList.CurrentRow.Cells[0].Value.ToString());
            frm_admin.TxtBx_OgretmenTC.Text = GrdVw_OgrtList.CurrentRow.Cells[1].Value.ToString();
            frm_admin.TxtBx_OgretmenAdSoyAd.Text = GrdVw_OgrtList.CurrentRow.Cells[2].Value.ToString();
            frm_admin.TxtBx_OgretmenCep.Text = GrdVw_OgrtList.CurrentRow.Cells[3].Value.ToString();
            frm_admin.TxtBx_OgretmenEvTel.Text = GrdVw_OgrtList.CurrentRow.Cells[4].Value.ToString();
            frm_admin.TxtBx_OgretmenEPosta.Text = GrdVw_OgrtList.CurrentRow.Cells[5].Value.ToString();
            frm_admin.CmbBx_Ogretmenil.Text = GrdVw_OgrtList.CurrentRow.Cells[6].Value.ToString();
            frm_admin.CmbBx_Ogretmenilce.Text = GrdVw_OgrtList.CurrentRow.Cells[7].Value.ToString();
            frm_admin.TxtBx_OgretmenAdres.Text = GrdVw_OgrtList.CurrentRow.Cells[8].Value.ToString();
            frm_admin.CmbBx_OgretmenCinsiyet.Text = GrdVw_OgrtList.CurrentRow.Cells[9].Value.ToString();
            frm_admin.TxtBx_OgretmenCiltNo.Text = GrdVw_OgrtList.CurrentRow.Cells[10].Value.ToString();
            frm_admin.TxtBx_OgretmenAileSiraNO.Text = GrdVw_OgrtList.CurrentRow.Cells[11].Value.ToString();
            frm_admin.DtTmPckr_OgretmenDT.Text = GrdVw_OgrtList.CurrentRow.Cells[12].Value.ToString();
            frm_admin.CmbBx_OgretmenDepartman.Text = GrdVw_OgrtList.CurrentRow.Cells[13].Value.ToString();
            frm_admin.TxtBx_OgretmenPozisyon.Text = GrdVw_OgrtList.CurrentRow.Cells[14].Value.ToString();
            frm_admin.TxtBx_OgretmenUni.Text = GrdVw_OgrtList.CurrentRow.Cells[15].Value.ToString();
            frm_admin.TxtBx_OgretmenOkuduguBolum.Text = GrdVw_OgrtList.CurrentRow.Cells[16].Value.ToString();

            if (GrdVw_OgrtList.CurrentRow.Cells[17].Value.ToString().Trim() != "")
            {
                byte[] Resim = (byte[])GrdVw_OgrtList.CurrentRow.Cells[17].Value;
                using (MemoryStream ms = new MemoryStream(Resim))
                {
                    frm_admin.PctrBx_OgretmenResim.Image = Image.FromStream(ms);
                }
            }
            else
            {
                frm_admin.PctrBx_OgretmenResim.SizeMode = PictureBoxSizeMode.StretchImage;
                frm_admin.PctrBx_OgretmenResim.ImageLocation = Application.StartupPath + "\\Resimler\\NoFoto.png";
            }


            this.Close();
        }
    }
}
