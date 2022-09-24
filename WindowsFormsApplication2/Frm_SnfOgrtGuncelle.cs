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

namespace WindowsFormsApplication2
{
    public partial class Frm_SnfOgrtGuncelle : Form
    {
        public Frm_SnfOgrtGuncelle()
        {
            InitializeComponent();
        }

        public string id { get; set; }

        private void Frm_SnfOgrtGuncelle_Load(object sender, EventArgs e)
        {
            Frm_AnaMenu frm_Ana = new Frm_AnaMenu();
            frm_Ana.baglanti.Open();
            SqlCommand kmt = new SqlCommand("select *from kayitlar where id=@p1", frm_Ana.baglanti);
            kmt.Parameters.AddWithValue("@p1", id);
            SqlDataReader oku = kmt.ExecuteReader();
            while (oku.Read())
            {
                Lbl_Tc.Text = oku[6].ToString();
                Lbl_Ad.Text = oku[1].ToString();
                Lbl_Soyad.Text = oku[2].ToString();
                Lbl_snf.Text = oku[19].ToString();
                Lbl_Ogrt.Text = oku[20].ToString();
                CmbBx_EgitimTipi.Text = oku[15].ToString();
                CmbBx_Kurs.Text = oku[16].ToString();
                CmbBx_Haftaicison.Text = oku[17].ToString();
                CmbBx_Saat.Text = oku[18].ToString();
            }
            frm_Ana.baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)   //kaydet
        {
            Frm_AnaMenu frm_ana = (Frm_AnaMenu)Application.OpenForms["Frm_AnaMenu"];
            frm_ana.baglanti.Open();
            SqlCommand kmtguncelle = new SqlCommand("update kayitlar set Sinif=@p1, Ogretmen=@p2 , egitimtipi=@p3,kurssecimi=@p4,haftaicison=@p5,saat=@p6 where id=@p7", frm_ana.baglanti);
            kmtguncelle.Parameters.AddWithValue("@p7", id);
            kmtguncelle.Parameters.AddWithValue("@p1", Lbl_snf.Text);
            kmtguncelle.Parameters.AddWithValue("@p2", Lbl_Ogrt.Text);
            kmtguncelle.Parameters.AddWithValue("@p3", CmbBx_EgitimTipi.Text);
            kmtguncelle.Parameters.AddWithValue("@p4", CmbBx_Kurs.Text);
            kmtguncelle.Parameters.AddWithValue("@p5", CmbBx_Haftaicison.Text);
            kmtguncelle.Parameters.AddWithValue("@p6", CmbBx_Saat.Text);
            if (kmtguncelle.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Güncelleme İşlemi Başarılı");
                frm_ana.ID = "";
                frm_ana.TxtBx_TcTikla.Clear();
                frm_ana.baglanti.Close();
                frm_ana.Btn_OgrGetir_Click(new object(), new EventArgs());//
                Close();
            }
            else { MessageBox.Show("Güncelleme İşlemi Başarısız"); }
            frm_ana.baglanti.Close();
        }

        private void Btn_SinifSil_Click(object sender, EventArgs e)
        {
            Frm_AnaMenu frm_ana = (Frm_AnaMenu)Application.OpenForms["Frm_AnaMenu"];
            frm_ana.baglanti.Open();
            SqlCommand kmtsnfsil = new SqlCommand("update  kayitlar set sinif=@p1 where id=@p2", frm_ana.baglanti);
            kmtsnfsil.Parameters.AddWithValue("@p2", id);
            kmtsnfsil.Parameters.AddWithValue("@p1", "");
            if (kmtsnfsil.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Öğrencinin Sınıfı Silindi");
                frm_ana.LstVw_Siniflilar.Items.Clear();
                frm_ana.cmbBx_egitimtipisinif.SelectedItem = null;
                frm_ana.CmbBx_Kurs_sinif.SelectedItem = null;
                frm_ana.CmbBx_Haftaicisonsinif.SelectedItem = null;
                frm_ana.CmbBx_Saatsinif.SelectedItem = null;
                frm_ana.ID = "";
                frm_ana.TxtBx_TcTikla.Clear();
                frm_ana.baglanti.Close();
                Close();
            }
            else { MessageBox.Show("işlem Başarısız!"); }
            frm_ana.baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Frm_AnaMenu frm_ana = (Frm_AnaMenu)Application.OpenForms["Frm_AnaMenu"];
            frm_ana.baglanti.Open();
            SqlCommand kmtogrtsil = new SqlCommand("update  kayitlar set Ogretmen=@p1 where id=@p2", frm_ana.baglanti);
            kmtogrtsil.Parameters.AddWithValue("@p2", id);
            kmtogrtsil.Parameters.AddWithValue("@p1", "");
            if (kmtogrtsil.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Öğrencinin Öğretmeni Silindi");
                frm_ana.LstVw_Siniflilar.Items.Clear();
                frm_ana.cmbBx_egitimtipisinif.SelectedItem = null;
                frm_ana.CmbBx_Kurs_sinif.SelectedItem = null;
                frm_ana.CmbBx_Haftaicisonsinif.SelectedItem = null;
                frm_ana.CmbBx_Saatsinif.SelectedItem = null;
                frm_ana.baglanti.Close();
                frm_ana.ID = "";
                frm_ana.TxtBx_TcTikla.Clear();
                Close();
            }
            else { MessageBox.Show("işlem Başarısız!"); }
            frm_ana.baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e) //sınıftan sil
        {
            Frm_AnaMenu frm_ana = (Frm_AnaMenu)Application.OpenForms["Frm_AnaMenu"];
            frm_ana.baglanti.Open();
            SqlCommand kmtsnfsiltalep = new SqlCommand("insert into sinifsilmetalebi (OgrID,SnfSilmeDurumu) values (@p1,1)", frm_ana.baglanti);
            kmtsnfsiltalep.Parameters.AddWithValue("@p1", id);
            if (kmtsnfsiltalep.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Sınıf Silme Talebi Yöneticiye Gönderildi");
                frm_ana.baglanti.Close();
                frm_ana.Btn_OgrGetir_Click(new object(), new EventArgs());
                this.Close();
            }
            else { MessageBox.Show("İşlem Başarısız"); }
            frm_ana.baglanti.Close();
        }
    }
}
