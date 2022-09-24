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
    public partial class Frm_KayitSil : Form
    {

        //SqlConnection baglanti = new SqlConnection("Data Source = 185.81.154.166, 1433; Network Library =DBMSSOCN; Initial Catalog = kayitlar; User ID = hasan; Password=H.12345");
         SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=Kayitlar;Integrated Security=True");


        public Frm_KayitSil()
        {
            InitializeComponent();
        }

        SqlDataAdapter adaptor;
        SqlCommand komut;

        public DataGridViewRow Satir { get; private set; }

        void griddoldur()
        {
            baglanti.Open();
            adaptor = new SqlDataAdapter("Select *From kayitlar", baglanti);
            DataTable tablo = new DataTable();
            adaptor.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            griddoldur();
            dataGridView1.Columns["id"].Width = 30;
            dataGridView1.Columns["id"].HeaderText = "ID";

            dataGridView1.Columns["ad"].Width = 50;
            dataGridView1.Columns["ad"].HeaderText = "Ad";

            dataGridView1.Columns["soyad"].Width = 50;
            dataGridView1.Columns["soyad"].HeaderText = "Soyad";

            dataGridView1.Columns["cepno"].Width = 80;
            dataGridView1.Columns["cepno"].HeaderText = "Cep Telefon";

            dataGridView1.Columns["evtel"].Width = 80;
            dataGridView1.Columns["evtel"].HeaderText = "Ev Telefon";

            dataGridView1.Columns["eposta"].Width = 80;
            dataGridView1.Columns["eposta"].HeaderText = "E Posta";

            dataGridView1.Columns["tcno"].Width = 75;
            dataGridView1.Columns["tcno"].HeaderText = "Tc";

            dataGridView1.Columns["il"].Width = 65;
            dataGridView1.Columns["il"].HeaderText = "İl";

            dataGridView1.Columns["ilce"].Width = 70;
            dataGridView1.Columns["ilce"].HeaderText = "İlçe";

            dataGridView1.Columns["adres"].Width = 80;
            dataGridView1.Columns["adres"].HeaderText = "Adres";

            dataGridView1.Columns["cinsiyet"].Width = 50;
            dataGridView1.Columns["cinsiyet"].HeaderText = "Cinsiyet";

            dataGridView1.Columns["ciltno"].Width = 35;
            dataGridView1.Columns["ciltno"].HeaderText = "Cilt No";

            dataGridView1.Columns["ailesirano"].Width = 40;
            dataGridView1.Columns["ailesirano"].HeaderText = "Aile Sıra No";

            dataGridView1.Columns["dogumtarihi"].Width = 65;
            dataGridView1.Columns["dogumtarihi"].HeaderText = "Doğum Tarihi";

            dataGridView1.Columns["resim"].Width = 70;
            dataGridView1.Columns["resim"].HeaderText = "Resim";

            dataGridView1.Columns["egitimtipi"].Width = 45;
            dataGridView1.Columns["egitimtipi"].HeaderText = "Eğitim Tipi";

            dataGridView1.Columns["kurssecimi"].Width = 50;
            dataGridView1.Columns["kurssecimi"].HeaderText = "Kurs Seçimi";

            dataGridView1.Columns["haftaicison"].Width = 60;
            dataGridView1.Columns["haftaicison"].HeaderText = "Haftaiçi/Sonu";

            dataGridView1.Columns["saat"].Width = 40;
            dataGridView1.Columns["saat"].HeaderText = "Saat";

            dataGridView1.Columns["Sinif"].Width = 30;
            dataGridView1.Columns["Sinif"].HeaderText = "Sınıf";

            dataGridView1.Columns["Ogretmen"].Width = 60;
            dataGridView1.Columns["Ogretmen"].HeaderText = "Öğretmen";

            dataGridView1.Columns["DeleteID"].Width = 30;
            dataGridView1.Columns["DeleteID"].HeaderText = "Sil";

            dataGridView1.Columns["Mezun"].Width = 60;
            dataGridView1.Columns["Mezun"].HeaderText = "Mezun Durumu";

            dataGridView1.Columns["Dondurma"].Width = 60;
            dataGridView1.Columns["Dondurma"].HeaderText = "Dondurma";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TxtBx_TCSil.Clear();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (TxtBx_TCSil.Text != string.Empty)
            {
                DialogResult Secim = new DialogResult();

                Secim = MessageBox.Show("İşlemi onaylıyor musunuz?", "ONAYLAYIN", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (Secim == DialogResult.Yes)
                {
                    komut = new SqlCommand();
                    baglanti.Open();
                    komut.Connection = baglanti;
                    komut.CommandText = "update kayitlar set deleteID='1' where id = '" + id + "'";
                    if (komut.ExecuteNonQuery() > 0)
                    {
                        baglanti.Close();
                        griddoldur();
                        MessageBox.Show("Silme Talebi Gönderildi", "Bilgilendirme", MessageBoxButtons.OK);
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
            }
            else { MessageBox.Show("Lütfen Seçim Yapınız"); }
            baglanti.Close();
        }

        int id;

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            TxtBx_TCSil.Text = dataGridView1.CurrentRow.Cells["tcno"].Value.ToString();
            id = int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
        }

        private void TxtBx_SoyadFilt_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter kmtsoyadfilt = new SqlDataAdapter("select *from kayitlar where soyad like '%" + TxtBx_SoyadFilt.Text + "%'", baglanti);
            DataTable tblosoyad = new DataTable();
            kmtsoyadfilt.Fill(tblosoyad);
            dataGridView1.DataSource = tblosoyad;
            baglanti.Close();
        }
    }
}
