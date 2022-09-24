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

    public partial class Frm_AnaMenu : Form
    {
        public Frm_AnaMenu()
        {
            InitializeComponent();
        }

        //public SqlConnection baglanti = new SqlConnection("Data Source = 185.81.154.166, 1433; Network Library = DBMSSOCN; Initial Catalog = kayitlar; User ID = hasan; Password=H.12345");
        public SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=Kayitlar;Integrated Security=True");

        public SqlDataAdapter adaptor;
        public SqlCommand komut;
        public SqlDataReader okuyucu;
        public DataTable tablo;

        int hata = 0;

        public void delete()
        {

        }
        public void kontrol()
        {

            if (TxtBx_Ad.Text == string.Empty)

                hata = 1;

            if (TxtBx_Soyad.Text == string.Empty)

                hata = 1;

            if (TxtBx_Cep.Text == string.Empty)

                hata = 1;

            if (TxtBx_EPosta.Text == string.Empty)

                hata = 1;

            if (TxtBx_Tc.Text == string.Empty)

                hata = 1;

            if (CmbBx_Cinsiyet.Text == string.Empty)

                hata = 1;

            if (CmbBx_il.Text == string.Empty)

                hata = 1;

            if (DtTmPckr_DogTar.Text == string.Empty)

                hata = 1;

            if (CmbBx_EğitimTipi.Text == string.Empty)

                hata = 1;

            if (CmbBx_KursSecimi.Text == string.Empty)

                hata = 1;

            if (CmbBx_HaftaicSon.Text == string.Empty)

                hata = 1;

            if (CmbBx_SbhOglAks.Text == string.Empty)

                hata = 1;

            if (hata == 1)
            {
                MessageBox.Show("Zorunlu Alanları Doldurun", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TxtBx_Tc.MaxLength = 11;
            TxtBx_TcAra.MaxLength = 11;

            LstVw_Siniflilar.FullRowSelect = true;
            LstVw_SnfAtama.FullRowSelect = true;

            tabPage1.Text = "Kayıt Bölümü";
            tabPage2.Text = "Ögrenci Arama";
            tabPage4.Text = "Sınıfa Yerleştirme ";

            listView2.View = View.Details;
            listView2.GridLines = true;
            listView2.Columns.Add("ID", 25);
            listView2.Columns.Add("Ad", 50);
            listView2.Columns.Add("Soyad", 50);
            listView2.Columns.Add("Cep No", 50);
            listView2.Columns.Add("Ev Tel", 50);
            listView2.Columns.Add("E Posta", 100);
            listView2.Columns.Add("Tc NO", 100);
            listView2.Columns.Add("İl", 100);
            listView2.Columns.Add("İlçe", 100);
            listView2.Columns.Add("adres", 100);
            listView2.Columns.Add("Cinsiyet", 50);
            listView2.Columns.Add("Cilt No", 50);
            listView2.Columns.Add("Aile Sıra No", 75);
            listView2.Columns.Add("Doğum Tarihi", 100);
            listView2.Columns.Add("Resim", 100);
            listView2.Columns.Add("Eğitim", 100);
            listView2.Columns.Add("Kurs", 100);
            listView2.Columns.Add("Gün", 100);
            listView2.Columns.Add("Saat", 100);

            baglanti.Open();
            SqlDataAdapter adaptor = new SqlDataAdapter("select * from iller ", baglanti);
            DataTable tablo2 = new DataTable();
            adaptor.Fill(tablo2);
            CmbBx_il.ValueMember = "id";
            CmbBx_il.DisplayMember = "il";
            CmbBx_il.DataSource = tablo2;
            baglanti.Close();

            CmbBx_EPosta.Items.Add("gmail.com");
            CmbBx_EPosta.Items.Add("hotmail.com");
            CmbBx_EPosta.Items.Add("outlook.com");
            CmbBx_EPosta.Items.Add("outlook.com.tr");
            CmbBx_EPosta.Items.Add("aol.com");
            CmbBx_EPosta.Items.Add("mail.com");
            CmbBx_EPosta.Items.Add("email.com");
            CmbBx_EPosta.Items.Add("mail.com.tr");
            CmbBx_EPosta.Items.Add("yahoo.com");
            CmbBx_EPosta.Items.Add("yandex.com");
            CmbBx_EPosta.Items.Add("mynet.com");

            baglanti.Open();//sınıfı olmayanların sınıf listesi
            SqlDataAdapter kmtsinif = new SqlDataAdapter("Select *from siniflar", baglanti);
            DataTable siniftablo = new DataTable();
            kmtsinif.Fill(siniftablo);
            CmbBx_SinifSec.DataSource = siniftablo;
            CmbBx_SinifSec.DisplayMember = "sinif";
            baglanti.Close();

            baglanti.Open();//sinifı olanların sınıf listesi
            SqlDataAdapter kmtsinifli = new SqlDataAdapter("Select *from siniflar", baglanti);
            DataTable siniflitablo = new DataTable();
            kmtsinifli.Fill(siniflitablo);
            CmbBx_Sinifli.DataSource = siniflitablo;
            CmbBx_Sinifli.DisplayMember = "sinif";
            baglanti.Close();

            baglanti.Open();//ogrtmenler sınıfları
            SqlDataAdapter kmtsiniflartablo = new SqlDataAdapter("Select *from siniflar", baglanti);
            DataTable siniflartablo = new DataTable();
            kmtsiniflartablo.Fill(siniflartablo);
            CmbBx_OgrtSinif.DataSource = siniflartablo;
            CmbBx_OgrtSinif.DisplayMember = "sinif";
            CmbBx_OgrtSinif.ValueMember = "id";
            baglanti.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) //kontrollü kapama
        {
            DialogResult dr = MessageBox.Show("Programı kapatmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else
                e.Cancel = true;
        }

        private void TxtBx_Tc_TextChanged(object sender, EventArgs e)
        {
            if (TxtBx_Tc.TextLength == 11)
            {
                MessageBox.Show("Maksimum sınıra ulaştınız!");
            }
        }

        private void button1_Click(object sender, EventArgs e) //Temizle
        {
            TxtBx_Ad.Clear();
            TxtBx_Soyad.Clear();
            TxtBx_Cep.Clear();
            TxtBx_Ev.Clear();
            TxtBx_EPosta.Clear();
            TxtBx_Tc.Clear();
            TxtBx_Cilt.Clear();
            TxtBx_AileSira.Clear();
            TxtBx_Adres.Clear();
            CmbBx_EğitimTipi.SelectedItem = null;
            CmbBx_KursSecimi.SelectedItem = null;
            CmbBx_HaftaicSon.SelectedItem = null;
            CmbBx_SbhOglAks.SelectedItem = null;
            CmbBx_ilce.SelectedItem = null;
            CmbBx_Cinsiyet.SelectedItem = null;
            CmbBx_il.SelectedItem = null;
            CmbBx_EPosta.SelectedItem = null;
            DtTmPckr_DogTar.Value = DateTime.Now;
            pictureBox1.ImageLocation = @"D:\İndirilenler\kurulProjesi\WF2_01.09.2019_final\WF2_01.09.2019\WF2_25.08.2019\WindowsFormsApplication2\WindowsFormsApplication2\WindowsFormsApplication2\Resimler\NoFoto.png";
        }

        private void button6_Click(object sender, EventArgs e)  //Veri tabanını gör
        {
            Frm_KayitSil frm = new Frm_KayitSil();
            frm.ShowDialog();
        }

        int hataarama = 0;

        private void button10_Click(object sender, EventArgs e)
        {
            if (TxtBx_AdAra.Text == string.Empty || TxtBx_SoyadAra.Text == string.Empty || TxtBx_TcAra.Text == string.Empty)
            {
                hataarama = 1;
            }
            else
            {
                hataarama = 0;
            }
            if (hataarama == 0)
            {
                listView2.Items.Clear();
                baglanti.Open();
                komut = new SqlCommand("Select * From kayitlar where ad = '" + TxtBx_AdAra.Text + "' and soyad = '" + TxtBx_SoyadAra.Text + "' and tcno = '" + TxtBx_TcAra.Text + "'", baglanti);
                komut.ExecuteNonQuery();
                okuyucu = komut.ExecuteReader();
                if (okuyucu.Read())
                {
                    ListViewItem item = new ListViewItem(okuyucu["id"].ToString());
                    item.SubItems.Add(okuyucu["ad"].ToString());
                    item.SubItems.Add(okuyucu["soyad"].ToString());
                    item.SubItems.Add(okuyucu["cepno"].ToString());
                    item.SubItems.Add(okuyucu["evtel"].ToString());
                    item.SubItems.Add(okuyucu["eposta"].ToString());
                    item.SubItems.Add(okuyucu["tcno"].ToString());
                    item.SubItems.Add(okuyucu["il"].ToString());
                    item.SubItems.Add(okuyucu["ilce"].ToString());
                    item.SubItems.Add(okuyucu["adres"].ToString());
                    item.SubItems.Add(okuyucu["cinsiyet"].ToString());
                    item.SubItems.Add(okuyucu["ciltno"].ToString());
                    item.SubItems.Add(okuyucu["ailesirano"].ToString());
                    item.SubItems.Add(okuyucu["dogumtarihi"].ToString());
                    item.SubItems.Add(okuyucu["resimyolu"].ToString());
                    item.SubItems.Add(okuyucu["egitimtipi"].ToString());
                    item.SubItems.Add(okuyucu["kurssecimi"].ToString());
                    item.SubItems.Add(okuyucu["haftaicison"].ToString());
                    item.SubItems.Add(okuyucu["saat"].ToString());
                    listView2.Items.Add(item);
                }
                else { MessageBox.Show("Eşleşme Bulunamadı"); }
            }
            else
            {
                MessageBox.Show("Lütfen Doldurun");
            }
            baglanti.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            TxtBx_AdAra.Clear();
            TxtBx_SoyadAra.Clear();
            TxtBx_TcAra.Clear();
            listView2.Items.Clear();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Sadece rakam girebilirsiniz...", "Uyarı");
            }
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Sadece rakam girebilirsiniz...", "Uyarı");
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Sadece rakam girebilirsiniz...", "Uyarı");
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                if ((int)e.KeyChar == 32) { e.Handled = false; }
                else
                {
                    e.Handled = true;
                    MessageBox.Show("Sadece harf girebilirsiniz...", "Uyarı");
                }
            }
        }

        private void button12_Click(object sender, EventArgs e) //Arka Planını değiştir
        {
            DialogResult tus;
            tus = colorDialog1.ShowDialog();
            if (tus == DialogResult.OK)
            {
                tabPage1.BackColor = colorDialog1.Color;
                tabPage2.BackColor = colorDialog1.Color;
                tabPage4.BackColor = colorDialog1.Color;
                this.BackColor = colorDialog1.Color;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbBx_il.SelectedIndex != -1)
            {
                SqlDataAdapter adaptor = new SqlDataAdapter("select * from ilceler where il = " + CmbBx_il.SelectedValue, baglanti);
                DataTable tablo = new DataTable();
                adaptor.Fill(tablo);
                CmbBx_ilce.ValueMember = "id";
                CmbBx_ilce.DisplayMember = "ilce";
                CmbBx_ilce.DataSource = tablo;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ChckBx_Manuel.Checked == true)
            {
                TxtBx_EPosta.Width = 224;
                label30.Visible = false;
                CmbBx_EPosta.Visible = false;
                CmbBx_EPosta.SelectedItem = null;
            }
            else
            {
                TxtBx_EPosta.Width = 90;
                label30.Visible = true;
                CmbBx_EPosta.Visible = true;
                CmbBx_EPosta.SelectedItem = null;
            }
        }

        public string Fotoyolu;

        private void Btn_ResimEkle_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == "openFileDialog1")
            {
                Fotoyolu = @"D:\İndirilenler\kurulProjesi\WF2_01.09.2019_final\WF2_01.09.2019\WF2_25.08.2019\WindowsFormsApplication2\WindowsFormsApplication2\WindowsFormsApplication2\Resimler\NoFoto.png";
                pictureBox1.ImageLocation = @"D:\İndirilenler\kurulProjesi\WF2_01.09.2019_final\WF2_01.09.2019\WF2_25.08.2019\WindowsFormsApplication2\WindowsFormsApplication2\WindowsFormsApplication2\Resimler\NoFoto.png";
            }
            else
            {
                Fotoyolu = openFileDialog1.FileName;
                pictureBox1.ImageLocation = openFileDialog1.FileName;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            hata = 0;
            Frm_KayitDetay frm = new Frm_KayitDetay();
            kontrol();
            if (hata == 1)
            {
                frm.Close();
            }
            else
            {
                frm.adi = TxtBx_Ad.Text;
                frm.soyadi = TxtBx_Soyad.Text;
                frm.ceptel = TxtBx_Cep.Text;
                frm.evtel = TxtBx_Ev.Text;
                if (ChckBx_Manuel.Checked == true)
                {
                    frm.eposta = TxtBx_EPosta.Text;
                }
                else
                {
                    frm.eposta = TxtBx_EPosta.Text + "@" + CmbBx_EPosta.Text;
                }
                frm.tcno = TxtBx_Tc.Text;
                frm.il = CmbBx_il.Text;
                frm.ilce = CmbBx_ilce.Text;
                frm.cinsiyet = CmbBx_Cinsiyet.Text;
                frm.ciltno = TxtBx_Cilt.Text;
                frm.ailesirano = TxtBx_AileSira.Text;
                frm.dogumtarihi = DtTmPckr_DogTar.Value;
                frm.fotoyolu = Fotoyolu;

                frm.adres = TxtBx_Adres.Text;
                frm.kurs = CmbBx_KursSecimi.Text;
                frm.egitimtipi = CmbBx_EğitimTipi.Text;
                frm.haftaicson = CmbBx_HaftaicSon.Text;
                frm.sbh_ogl_aks = CmbBx_SbhOglAks.Text;
                frm.ShowDialog();
            }
        }

        int id;

        private void Btn_KytGuncelle_Click(object sender, EventArgs e)
        {
            if (listView2.Items.Count != 0)
            {
                Frm_Guncelle frm_Gun = new Frm_Guncelle();
                id = int.Parse(listView2.Items[0].SubItems[0].Text);
                frm_Gun.id = id;
                frm_Gun.TxtBx_Ad.Text = listView2.Items[0].SubItems[1].Text;
                frm_Gun.TxtBx_Soyad.Text = listView2.Items[0].SubItems[2].Text;
                frm_Gun.TxtBx_Cep.Text = listView2.Items[0].SubItems[3].Text;
                frm_Gun.TxtBx_Ev.Text = listView2.Items[0].SubItems[4].Text;
                frm_Gun.TxtBx_EPosta.Text = listView2.Items[0].SubItems[5].Text;
                frm_Gun.TxtBx_Tc.Text = listView2.Items[0].SubItems[6].Text;
                //
                SqlDataAdapter komut = new SqlDataAdapter("Select *from iller", baglanti);
                DataTable tablo = new DataTable();
                komut.Fill(tablo);
                frm_Gun.CmbBx_il.ValueMember = "id";
                frm_Gun.CmbBx_il.DisplayMember = "il";
                frm_Gun.CmbBx_il.DataSource = tablo;
                frm_Gun.CmbBx_il.Text = listView2.Items[0].SubItems[7].Text;
                SqlDataAdapter komut4 = new SqlDataAdapter("select *from ilceler where il=@p1", baglanti);
                komut4.SelectCommand.Parameters.AddWithValue("@p1", frm_Gun.CmbBx_il.SelectedValue);
                DataTable tabloilce = new DataTable();
                komut4.Fill(tabloilce);
                frm_Gun.CmbBx_ilce.DataSource = tabloilce;
                frm_Gun.CmbBx_ilce.ValueMember = "id";
                frm_Gun.CmbBx_ilce.DisplayMember = "ilce";
                frm_Gun.CmbBx_ilce.Text = listView2.Items[0].SubItems[8].Text; //ilçe
                frm_Gun.TxtBx_Adres.Text = listView2.Items[0].SubItems[9].Text;
                frm_Gun.CmbBx_Cinsiyet.SelectedItem = listView2.Items[0].SubItems[10].Text;
                frm_Gun.TxtBx_Cilt.Text = listView2.Items[0].SubItems[11].Text;
                frm_Gun.TxtBx_AileSira.Text = listView2.Items[0].SubItems[12].Text;
                frm_Gun.DtTmPckr_DogTar.Text = listView2.Items[0].SubItems[13].Text;
                frm_Gun.PctrBx_OgrGuncelle.ImageLocation = listView2.Items[0].SubItems[14].Text;
                frm_Gun.CmbBx_EğitimTipi.SelectedItem = listView2.Items[0].SubItems[15].Text;
                frm_Gun.CmbBx_KursSecim.SelectedItem = listView2.Items[0].SubItems[16].Text;
                frm_Gun.CmbBx_HaftaicSon.SelectedItem = listView2.Items[0].SubItems[17].Text;
                frm_Gun.CmbBx_SbhOglAks.SelectedItem = listView2.Items[0].SubItems[18].Text;
                frm_Gun.ShowDialog();
            }
            else { MessageBox.Show("Lütfen Önce Arama Yapınız"); }
        }

        private void button2_Click(object sender, EventArgs e) //silme talep işlemi
        {
            if (listView2.Items.Count == 1)
            {
                id = int.Parse(listView2.Items[0].SubItems[0].Text);
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into kytsilmetalepleri (ogrid,silmetalebi) values (@p1,1) ", baglanti);
                komut.Parameters.AddWithValue("@p1", id); 
                if (komut.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Silme Talebi Gönderildi");
                    listView2.Items.Clear();
                    TxtBx_AdAra.Clear();
                    TxtBx_SoyadAra.Clear();
                    TxtBx_TcAra.Clear();
                }
                else { MessageBox.Show("Silme Talebi Gönderilemedi"); }
            }
            else { MessageBox.Show("Önce Arama Yapınız"); }
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e) //temizleme
        {
            CmbBx_Kurs_sinif.SelectedItem = null;
            CmbBx_Haftaicisonsinif.SelectedItem = null;
            CmbBx_Saatsinif.SelectedItem = null;
            cmbBx_egitimtipisinif.SelectedItem = null;
            LstVw_Siniflilar.Items.Clear();
            TxtBx_TcTikla.Clear();
        }

        public void Btn_OgrGetir_Click(object sender, EventArgs e)
        {
            LstVw_Siniflilar.Items.Clear();
            baglanti.Open();
            SqlCommand kmtOgrGetir = new SqlCommand("select *from kayitlar where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4  and Sinif=@p5", baglanti);
            kmtOgrGetir.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
            kmtOgrGetir.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
            kmtOgrGetir.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
            kmtOgrGetir.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
            kmtOgrGetir.Parameters.AddWithValue("@p5", CmbBx_Sinifli.Text);
            SqlDataReader oku = kmtOgrGetir.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem item = new ListViewItem(oku["id"].ToString());
                item.SubItems.Add(oku["ad"].ToString());
                item.SubItems.Add(oku["soyad"].ToString());
                item.SubItems.Add(oku["tcno"].ToString());
                item.SubItems.Add(oku["egitimtipi"].ToString());
                item.SubItems.Add(oku["kurssecimi"].ToString());
                item.SubItems.Add(oku["haftaicison"].ToString());
                item.SubItems.Add(oku["saat"].ToString());
                item.SubItems.Add(oku["Sinif"].ToString());
                LstVw_Siniflilar.Items.Add(item);
            }
            if (LstVw_Siniflilar.Items.Count == 0)
            {
                MessageBox.Show("Sınıfı Olan Öğrenci Bulunamadı");
            }
            else { }
            baglanti.Close();
            //////////////////////////////////////
            LstVw_SnfAtama.Items.Clear();
            baglanti.Open();
            SqlCommand kmtnosinif = new SqlCommand("select *from kayitlar where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and Sinif is null", baglanti);
            kmtnosinif.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
            kmtnosinif.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
            kmtnosinif.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
            kmtnosinif.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
            SqlDataReader okunosinif = kmtnosinif.ExecuteReader();
            while (okunosinif.Read())
            {
                ListViewItem item = new ListViewItem(okunosinif["id"].ToString());
                item.SubItems.Add(okunosinif["ad"].ToString());
                item.SubItems.Add(okunosinif["soyad"].ToString());
                item.SubItems.Add(okunosinif["tcno"].ToString());
                item.SubItems.Add(okunosinif["egitimtipi"].ToString());
                item.SubItems.Add(okunosinif["kurssecimi"].ToString());
                item.SubItems.Add(okunosinif["haftaicison"].ToString());
                item.SubItems.Add(okunosinif["saat"].ToString());
                item.SubItems.Add(okunosinif["sinif"].ToString());
                LstVw_SnfAtama.Items.Add(item);
            }
            if (LstVw_SnfAtama.Items.Count == 0)
            {
                MessageBox.Show("Sınıfı Olmayan Öğrenci Bulunamadı");
            }
            else { }
            baglanti.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstVw_Siniflilar.SelectedItems.Count > 0)
            {
                ListViewItem item = LstVw_Siniflilar.SelectedItems[0];
                TxtBx_TcTikla.Text = item.SubItems[3].Text;
                ID = item.SubItems[0].Text;
            }
        }

        public string ID;

        private void button4_Click(object sender, EventArgs e) //güncelleme
        {
            if (!string.IsNullOrWhiteSpace(ID))
            {
                Frm_SnfOgrtGuncelle frm_SnfOgrt = new Frm_SnfOgrtGuncelle();
                frm_SnfOgrt.id = ID;
                frm_SnfOgrt.ShowDialog();
            }
            else { MessageBox.Show("Önce Arama Yapıp Seçim Yapınız"); }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void TxtBx_TcAra_TextChanged(object sender, EventArgs e)
        {
            if (TxtBx_TcAra.Text.Length == 11)
            {
                MessageBox.Show("Maksimum sınıra ulaştınız!");
            }
        }


        public string IDsinifsiz;

        private void LstVw_SnfAtama_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstVw_SnfAtama.SelectedItems.Count > 0)
            {
                ListViewItem item = LstVw_SnfAtama.SelectedItems[0];
                TxtBx_TcSnfAta.Text = item.SubItems[3].Text;
                IDsinifsiz = item.SubItems[0].Text;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(IDsinifsiz) && !string.IsNullOrWhiteSpace(CmbBx_SinifSec.Text)) //Sınıf atama
            {
                //kontejyanı okuduk
                int kontejyan = 0;
                baglanti.Open();
                SqlCommand kmtkon = new SqlCommand("select *from siniflar where sinif=@p1", baglanti);
                kmtkon.Parameters.AddWithValue("@p1", CmbBx_SinifSec.Text);
                SqlDataReader kontoku = kmtkon.ExecuteReader();
                while (kontoku.Read())
                {
                    kontejyan = int.Parse(kontoku[2].ToString());
                }
                baglanti.Close();
                //////
                int snfkytsayisi = 0;
                baglanti.Open();
                SqlCommand kmtkyt = new SqlCommand("select count(id) from kayitlar where egitimtipi=@p1 and kurssecimi=@p2 and  haftaicison=@p3 and saat=@p4 and sinif=@p5", baglanti);
                kmtkyt.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtkyt.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtkyt.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtkyt.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                kmtkyt.Parameters.AddWithValue("@p5", CmbBx_SinifSec.Text);
                SqlDataReader kytoku = kmtkyt.ExecuteReader();
                while (kytoku.Read())
                {
                    snfkytsayisi = int.Parse(kytoku[0].ToString());
                }
                baglanti.Close();
                //
                if (kontejyan > snfkytsayisi)
                {
                    Frm_SinifAtama frm_ata = new Frm_SinifAtama();
                    baglanti.Open();
                    SqlCommand kmtsnfsiz = new SqlCommand("select *from kayitlar where id=@p1", baglanti);
                    kmtsnfsiz.Parameters.AddWithValue("@p1", IDsinifsiz);
                    SqlDataReader snfsizoku = kmtsnfsiz.ExecuteReader();
                    while (snfsizoku.Read())
                    { 
                        frm_ata.Lbl_Ad.Text = snfsizoku[1].ToString();
                        frm_ata.Lbl_Syd.Text = snfsizoku[2].ToString();
                        frm_ata.Lbl_tc.Text = snfsizoku[6].ToString();
                        frm_ata.Lbl_kont.Text = kontejyan.ToString();
                        frm_ata.Lbl_OgrSayisi.Text = snfkytsayisi.ToString();
                        frm_ata.Lbl_Snf.Text = CmbBx_SinifSec.Text;
                    }
                    baglanti.Close();
                    frm_ata.id = IDsinifsiz;
                    frm_ata.ShowDialog();
                }
                else { MessageBox.Show("Sınıf Doludur, Lütfen Başka Sınıf Seçiniz"); }
            }
            else { MessageBox.Show("Önce Arama Yapıp, Seçim Yapınız ve Sınıf Seçiniz"); }
        }

        private void button8_Click(object sender, EventArgs e)//Öğretmen ata
        {
            if (!string.IsNullOrEmpty(CmbBx_OgrtSinif.Text))
            {
                Frm_OgrtAta ogrtfrm = new Frm_OgrtAta();
                ogrtfrm.Lbl_Sinif.Text = CmbBx_OgrtSinif.Text;
                ogrtfrm.snfid = (int)CmbBx_OgrtSinif.SelectedValue;
                ogrtfrm.ShowDialog();
            } else
            {
                MessageBox.Show("Önce Sınıf Seçiniz");
            }
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void Btn_KytDondur_Click(object sender, EventArgs e)
        {
            if (listView2.Items.Count == 1)
            {
                id = int.Parse(listView2.Items[0].SubItems[0].Text);
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into kytdondurmatalebi (OgrID,OgrDondurmaTalebi) values (@p1,1)", baglanti);
                komut.Parameters.AddWithValue("@p1", id);
                if (komut.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Dondurma Talebi Gönderildi");
                    listView2.Items.Clear();
                    TxtBx_AdAra.Clear();
                    TxtBx_SoyadAra.Clear();
                    TxtBx_TcAra.Clear();
                }
                else { MessageBox.Show("Dondurma Talebi Gönderilemedi"); }
            }
            else { MessageBox.Show("Önce Arama Yapınız"); }
            baglanti.Close();
        }
    }
}
