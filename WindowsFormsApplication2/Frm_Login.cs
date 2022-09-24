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
    public partial class Frm_Login : Form
    {
        public Frm_Login()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source = 185.81.154.166, 1433; Network Library = DBMSSOCN; Initial Catalog = kayitlar; User ID = hasan; Password=H.12345");
        SqlCommand komut;
        SqlDataReader okuyucu;
        SqlDataAdapter adaptor;

        public void giris()
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
            {
                MessageBox.Show("Eksik Bilgi Girdiniz!!", "Giriş Kontrol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                baglanti.Open();
                komut = new SqlCommand("SELECT * FROM kullanicilar WHERE kullaniciadi= '" + textBox1.Text + "' AND sifre= '" + textBox2.Text + "'", baglanti);
                komut.Parameters.AddWithValue("@kullaniciadi", textBox1.Text);
                komut.Parameters.AddWithValue("@sifre", textBox2.Text);
                adaptor = new SqlDataAdapter(komut);
                okuyucu = komut.ExecuteReader();
                if (okuyucu.Read())
                {
                    okuyucu.Close();
                    komut = new SqlCommand("SELECT * FROM kullanicilar WHERE kullaniciadi= '" + textBox1.Text + "' AND sifre= '" + textBox2.Text + "' AND yetki = 'Admin'", baglanti);
                    adaptor = new SqlDataAdapter(komut);
                    okuyucu = komut.ExecuteReader();
                    if (okuyucu.Read())
                    {
                        this.Hide();
                        Frm_AdminPanel frm = new Frm_AdminPanel();
                        frm.ShowDialog();
                        okuyucu.Close();
                    }
                    else
                    {
                        okuyucu.Close();
                        komut = new SqlCommand("SELECT * FROM kullanicilar WHERE kullaniciadi= '" + textBox1.Text + "' AND sifre= '" + textBox2.Text + "' AND yetki = 'Kullanıcı'", baglanti);
                        adaptor = new SqlDataAdapter(komut);
                        okuyucu = komut.ExecuteReader();
                        if (okuyucu.Read())
                        {
                            this.Hide();
                            Frm_AnaMenu frm = new Frm_AnaMenu();
                            frm.ShowDialog();
                            okuyucu.Close();
                        }
                        else
                        {
                            okuyucu.Close();
                            komut = new SqlCommand("SELECT * FROM kullanicilar WHERE kullaniciadi= '" + textBox1.Text + "' AND sifre= '" + textBox2.Text + "' AND yetki = 'Bilgiislem'", baglanti);
                            adaptor = new SqlDataAdapter(komut);
                            okuyucu = komut.ExecuteReader();
                            if (okuyucu.Read())
                            {
                                this.Hide();
                                Frm_Bilgİslem frm = new Frm_Bilgİslem();
                                frm.ShowDialog();
                                okuyucu.Close();
                            }
                        }
                    }
                }
                else { MessageBox.Show("Kullanıcı veya Şifre hatalıdır.");}
                baglanti.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            giris();
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {

        }
    }
}
