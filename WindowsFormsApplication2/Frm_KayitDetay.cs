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
    public partial class Frm_KayitDetay : Form
    {
        public Frm_KayitDetay()
        {
            InitializeComponent();
        }

        public string adi { get; set; }
        public string soyadi { get; set; }
        public string ceptel { get; set; }
        public string evtel { get; set; }
        public string eposta { get; set; }
        public string tcno { get; set; }
        public string il { get; set; }
        public string ilce { get; set; }
        public string adres { get; set; }
        public string cinsiyet { get; set; }
        public string ciltno { get; set; }
        public string ailesirano { get; set; }
        public DateTime dogumtarihi { get; set; }
        public string fotoyolu { get; set; } 
        public string egitimtipi { get; set; }
        public string kurs { get; set; }
        public string haftaicson { get; set; }
        public string sbh_ogl_aks { get; set; }


        private void Form3_Load(object sender, EventArgs e)
        {
            Lbl_Ad.Text = adi;
            Lbl_Soyad.Text = soyadi;
            Lbl_Tc.Text = tcno;
            Lbl_Cep.Text = ceptel;
            Lbl_EvTel.Text = evtel;
            Lbl_EPosta.Text = eposta;
            Lbl_il.Text = il;
            Lbl_ilce.Text = ilce;
            Lbl_Adres.Text = adres;
            Lbl_Cinsiyet.Text = cinsiyet;
            Lbl_CiltNo.Text = ciltno;
            Lbl_AileSira.Text = ailesirano;
            Lbl_DogTar.Text = dogumtarihi.ToShortDateString();
            Lbl_EgitimTipi.Text = egitimtipi;
            Lbl_Kurs.Text = kurs;
            Lbl_HaftaicSon.Text = haftaicson;
            Lbl_SbhOgAks.Text = sbh_ogl_aks;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Frm_AdminPanel frm_adm = (Frm_AdminPanel)Application.OpenForms["Frm_AdminPanel"];
            byte[] resim;
            if (frm_adm.openFileDialog1.FileName == "openFileDialog1")
            {
                fotoyolu = Application.StartupPath + "\\Resimler\\NoFoto.png";
            }

            FileStream fs = new FileStream(fotoyolu, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            resim = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();

            frm_adm.baglanti.Open();
            SqlCommand kmtogrekle= new SqlCommand("insert into kayitlar(ad, soyad, cepno, evtel, eposta, tcno, il, ilce, adres, cinsiyet, ciltno, ailesirano, dogumtarihi, resim, egitimtipi, kurssecimi, haftaicison, saat) values('" +adi+ "', '" + soyadi + "', '" + ceptel+ "', '" + evtel + "', '" + eposta + "', '" + tcno+ "', '" + il + "', '" + ilce + "', '" + adres + "', '" + cinsiyet + "', '" + ciltno + "', '" + ailesirano + "', '" + dogumtarihi + "',@p1, '" + egitimtipi + "', '" + kurs + "', '" + haftaicson + "', '" + sbh_ogl_aks + "')", frm_adm.baglanti);

            kmtogrekle.Parameters.Add("@p1", SqlDbType.Image, resim.Length).Value = resim;
            if (kmtogrekle.ExecuteNonQuery() > 0)
            {
                frm_adm.baglanti.Close();
                ogrbildirim();
                frm_adm.kytbildirimleri();
                MessageBox.Show("Kayıt işlemi Başarılı...");
                Close();
            }
            else { MessageBox.Show("Kayıt İşlemi Başarısız");}
            frm_adm.baglanti.Close();
        }

        public void ogrbildirim()
        {
            Frm_AdminPanel frm_adm = (Frm_AdminPanel)Application.OpenForms["Frm_AdminPanel"];
            frm_adm.Lst_VwYeniKytveYeniTlbBildirimleri.Items.Clear();
            frm_adm.baglanti.Open();
            SqlCommand kmtkytbildirim = new SqlCommand("insert into kayitbildirimleri (bildirimler) values (@p1)", frm_adm.baglanti);
            kmtkytbildirim.Parameters.AddWithValue("@p1", tcno + " TC Numaralı Öğrenci Kayıt Edilmiştir.");
            kmtkytbildirim.ExecuteNonQuery();
            frm_adm.baglanti.Close();
        }

        private void Btn_Duzenle_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
