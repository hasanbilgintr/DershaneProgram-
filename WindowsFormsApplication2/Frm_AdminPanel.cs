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
    public partial class Frm_AdminPanel : Form
    {
        public Frm_AdminPanel()
        {
            InitializeComponent();
        }

        //public SqlConnection baglanti = new SqlConnection("Data Source = 185.81.154.166, 1433; Network Library = DBMSSOCN; Initial Catalog = kayitlar; User ID = hasan; Password=Hsn.12345");
        //public SqlConnection baglanti = new SqlConnection("Data Source = 185.81.155.74; Initial Catalog = otomosyon1; User ID = onur2"); //bölede yazılabilir
        public SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=Kayitlar;Integrated Security=True");

        public SqlDataAdapter adaptor;
        public SqlCommand komut;
        public SqlDataReader okuyucu;
        public DataTable tablo;

        int hata = 0;

        //public void firstdelete()
        //{
        //    if (TxtBx_TcAra.Text == string.Empty || TxtBx_TcAra.Text == "")
        //    {
        //        MessageBox.Show("Lütfen T.C Kimlik Numarasını Giriniz!", "Silme Kontrol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //    else
        //    {
        //        if (TxtBx_TcAra.TextLength < 11)
        //        {
        //            MessageBox.Show("T.C Kimlik Numarasını Eksik Girdiniz!!", "T.C Kimlik Kontrol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //        else
        //        {
        //            baglanti = new SqlConnection(@"Data Source = 185.81.154.166, 1433; Network Library = DBMSSOCN; Initial Catalog = kayitlar; User ID = hasan; Password=H.12345");
        //            baglanti.Open();
        //            komut = new SqlCommand("SELECT * FROM kayitlar WHERE tcno=@tcno", baglanti);
        //            komut.Parameters.AddWithValue("@tcno", TxtBx_TcAra.Text);
        //            adaptor = new SqlDataAdapter(komut);
        //            okuyucu = komut.ExecuteReader();
        //            if (okuyucu.Read())
        //            {
        //                baglanti = new SqlConnection(@"Data Source = 185.81.154.166, 1433; Network Library = DBMSSOCN; Initial Catalog = kayitlar; User ID = hasan; Password=H.12345");
        //                baglanti.Open();
        //                komut = new SqlCommand("SELECT * FROM kayitlar WHERE DeleteID = '1'", baglanti);
        //                komut.Parameters.AddWithValue("@DeleteID", 1);
        //                adaptor = new SqlDataAdapter(komut);
        //                okuyucu = komut.ExecuteReader();
        //                if (okuyucu.Read())
        //                {
        //                    MessageBox.Show(TxtBx_TcAra.Text + " T.C No'lu Kişi İçin Silme Talebinde Bulunulmuştur.Admin Onayı Beklenmektedir.", "Silme Talebi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    okuyucu.Close();
        //                    baglanti.Close();
        //                }
        //                else
        //                {
        //                    baglanti = new SqlConnection(@"Data Source = 185.81.154.166, 1433; Network Library = DBMSSOCN; Initial Catalog = kayitlar; User ID = hasan; Password=H.12345");
        //                    baglanti.Open();
        //                    komut = new SqlCommand("UPDATE kayitlar SET DeleteID= '1' WHERE TCNO= '" + TxtBx_TcAra.Text + "'", baglanti);
        //                    komut.ExecuteNonQuery();
        //                    MessageBox.Show("Silme Talebiniz İşleme Alınmıştır!", "Silme Talebi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    baglanti.Close();
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show(TxtBx_TcAra.Text + " TC No'lu Kullanıcı Bulunamamıştır.Lütfen T.C Kimlik Numarasını Kontrol Ediniz!", "Silme Kontrol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //            okuyucu.Close();
        //            baglanti.Close();
        //        }
        //    }
        //}

        public void adminlistfiil()
        {
            baglanti = new SqlConnection(@"Data Source = 185.81.154.166, 1433; Network Library = DBMSSOCN; Initial Catalog = kayitlar; User ID = hasan; Password=H.12345");
            komut = new SqlCommand("Select * FROM kayitlar WHERE DeleteID='1'", baglanti);
            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            okuyucu = komut.ExecuteReader();
            LstVw_SilmeTalepleri.Items.Clear();
            while (okuyucu.Read())
            {
                ListViewItem item = new ListViewItem(okuyucu["tcno"].ToString());
                item.SubItems.Add(okuyucu["ad"].ToString());
                item.SubItems.Add(okuyucu["soyad"].ToString());
                LstVw_SilmeTalepleri.Items.Add(item);
            }
            okuyucu.Close();
            baglanti.Close();
        }

        public void ogretmenekle()
        {

        }

        public void kullaniciekle()
        {
            if (CmbBx_Calisan.Text != string.Empty & TxtBx_KullaniciAdi.Text != string.Empty & TxtBx_KullaniciSifre.Text != string.Empty & CmbBx_Yetki.Text != string.Empty)
            {
                baglanti = new SqlConnection(@"Data Source = 185.81.154.166, 1433; Network Library = DBMSSOCN; Initial Catalog = kayitlar; User ID = hasan; Password=H.12345");
                komut = new SqlCommand("INSERT INTO kullanicilar(calisanID,kullaniciadi,sifre,yetki) VALUES (@calisan,@kullaniciadi,@sifre,@yetki)", baglanti);
                komut.Parameters.AddWithValue("@calisan", CmbBx_Calisan.SelectedValue);
                komut.Parameters.AddWithValue("@kullaniciadi", TxtBx_KullaniciAdi.Text);
                komut.Parameters.AddWithValue("@sifre", TxtBx_KullaniciSifre.Text);
                komut.Parameters.AddWithValue("@yetki", CmbBx_Yetki.Text);
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                if (komut.ExecuteNonQuery() > 0)
                {
                    baglanti.Close();
                    kullaniciListesi();
                    CmbBx_Calisan.SelectedIndex = -1;
                    TxtBx_KullaniciAdi.Clear();
                    TxtBx_KullaniciSifre.Clear();
                    CmbBx_Yetki.SelectedIndex = -1;
                    MessageBox.Show(TxtBx_KullaniciAdi.Text + " adlı kullanıcı eklenmiştir.", "Kullanıcı Ekle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else { MessageBox.Show("Ekleme Başarısız"); }
            }
            else { MessageBox.Show("Lütfen Alanları Giriniz"); }
            baglanti.Close();
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

        private void Frm_AdminPanel_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            tabPage1.Text = "Öğrenci Kayıt Bölümü";
            tabPage2.Text = "Ögrenci Arama Bölümü";
            tabPage4.Text = "Sınıfa Yerleştirme Bölümü";
            tabPage3.Text = "Kullanıcı ve Çalışan Ekleme Bölümü";
            tabPage5.Text = "Talepler Bölümü";
            tabPage6.Text = "Öğretmen Ekleme Bölümü";
            tabPage7.Text = "Muhasebe Bölümü";
            tabPage8.Text = "Bildirim Bölümü";
            tabPage13.Text = "Bilgi İşlem Bölümü";

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

            Cmb2.Enabled = false;
            Cmb3.Enabled = false;
            Cmb4.Enabled = false;
            Cmb5.Enabled = false;
            Cmb6.Enabled = false;
            Cmb7.Enabled = false;
            Cmb8.Enabled = false;
            Cmb9.Enabled = false;
            Cmb10.Enabled = false;
            Cmb11.Enabled = false;
            Cmb12.Enabled = false;
            Cmb13.Enabled = false;
            Cmb14.Enabled = false;
            Cmb15.Enabled = false;
            Cmb16.Enabled = false;
            Cmb17.Enabled = false;

            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlDataAdapter OSnfCbmDoldur = new SqlDataAdapter("select * from siniflar Order by sinif ", baglanti);
            DataTable OSnfCbmDoldurTablo = new DataTable();
            OSnfCbmDoldur.Fill(OSnfCbmDoldurTablo);
            Cmb_OSnf.ValueMember = "id";
            Cmb_OSnf.DisplayMember = "sinif";
            Cmb_OSnf.DataSource = OSnfCbmDoldurTablo;
            baglanti.Close();

            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
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

            string[] servisler = { "gmail.com", "hotmail.com", "outlook.com", "outlook.com.tr", "aol.com", "mail.com", "email.com", "mail.com.tr", "yahoo.com", "yandex.com", "mynet.com" };
            foreach (var item in servisler)
            {
                CmbBx_Caleposta.Items.Add(item);
            }

            string[] servislerogretmen = { "gmail.com", "hotmail.com", "outlook.com", "outlook.com.tr", "aol.com", "mail.com", "email.com", "mail.com.tr", "yahoo.com", "yandex.com", "mynet.com" };
            foreach (var item in servislerogretmen)
            {
                CmbBx_OgretmenEPosta.Items.Add(item);
            }

            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlDataAdapter kmt = new SqlDataAdapter("select *from calisanlar", baglanti);
            DataTable tblo = new DataTable();
            kmt.Fill(tblo);
            CmbBx_Calisan.DataSource = tblo;
            CmbBx_Calisan.DisplayMember = "adsoyad";
            CmbBx_Calisan.ValueMember = "id";
            baglanti.Close();

            //adminlistfiil();
            OgrtAtamaListesi();
            KytDondurmaListesi();
            OgrSilmeTalepleri();
            kullaniciListesi();
            kytbildirimleri();

            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); } //calışanların il listesi
            SqlDataAdapter adaptor1 = new SqlDataAdapter("select * from iller ", baglanti);
            DataTable tablo3 = new DataTable();
            adaptor1.Fill(tablo3);
            CmbBx_calil.ValueMember = "id";
            CmbBx_calil.DisplayMember = "il";
            CmbBx_calil.DataSource = tablo3;
            baglanti.Close();

            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); } //Öğretmenlerin il Listesi
            SqlDataAdapter ogretmeniladaptor = new SqlDataAdapter("select * from iller ", baglanti);
            DataTable ogretmeniltablo = new DataTable();
            ogretmeniladaptor.Fill(ogretmeniltablo);
            CmbBx_Ogretmenil.ValueMember = "id";
            CmbBx_Ogretmenil.DisplayMember = "il";
            CmbBx_Ogretmenil.DataSource = ogretmeniltablo;
            baglanti.Close();

            sinif();
            LstVw_Sinif.FullRowSelect = true;
            sinifdetay();
            LstVw_SinifDetay.FullRowSelect = true;

            DtTmPckr_OBasTar.MinDate = new System.DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);//sınırlama
            DtTmPckr_OBitTar.MinDate = new System.DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        public void sinif()
        {
            LstVw_Sinif.Items.Clear();
            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmtsnflistesi = new SqlCommand("select *from siniflar", baglanti);
            SqlDataReader snfoku = kmtsnflistesi.ExecuteReader();
            while (snfoku.Read())
            {
                ListViewItem snfekle = new ListViewItem();
                snfekle.Text = snfoku[0].ToString();
                snfekle.SubItems.Add(snfoku[1].ToString());
                snfekle.SubItems.Add(snfoku[2].ToString());
                LstVw_Sinif.Items.Add(snfekle);
            }
            baglanti.Close();
        }

        public void sinifdetay()
        {
            LstVw_SinifDetay.Items.Clear();
            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmtsnfdetaylist = new SqlCommand("select *from Vw_Siniflardetay where onay=1", baglanti);
            SqlDataReader snfoku = kmtsnfdetaylist.ExecuteReader();
            while (snfoku.Read())
            {
                ListViewItem snfdetayekle = new ListViewItem();
                snfdetayekle.Text = snfoku[0].ToString();
                snfdetayekle.SubItems.Add(snfoku[1].ToString());
                snfdetayekle.SubItems.Add(snfoku[2].ToString());
                snfdetayekle.SubItems.Add(snfoku[3].ToString());
                snfdetayekle.SubItems.Add(snfoku[4].ToString());
                snfdetayekle.SubItems.Add(snfoku[5].ToString());
                snfdetayekle.SubItems.Add(snfoku[6].ToString());
                snfdetayekle.SubItems.Add(snfoku[7].ToString());
                snfdetayekle.SubItems.Add(snfoku[8].ToString());
                snfdetayekle.SubItems.Add(snfoku[9].ToString());
                LstVw_SinifDetay.Items.Add(snfdetayekle);
            }
            baglanti.Close();
        }

        public int ogrtID;
        private void kullaniciListesi()
        {
            LstVw_KullaniciList.Items.Clear();

            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmt = new SqlCommand("select *from Vw_Kullanicilar", baglanti);
            SqlDataReader oku = kmt.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku[0].ToString();
                ekle.SubItems.Add(oku[1].ToString());
                ekle.SubItems.Add(oku[2].ToString());
                ekle.SubItems.Add(oku[3].ToString());
                ekle.SubItems.Add(oku[4].ToString());
                LstVw_KullaniciList.Items.Add(ekle);
            }
            baglanti.Close();
            LstVw_KullaniciList.FullRowSelect = true;
        }

        private void OgrSilmeTalepleri()
        {
            LstVw_SilmeTalepleri.Items.Clear();

            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmt = new SqlCommand("select *from Vw_SilmeTalepleri ", baglanti);
            SqlDataReader oku = kmt.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku[0].ToString();
                ekle.SubItems.Add(oku[1].ToString());
                ekle.SubItems.Add(oku[2].ToString());
                ekle.SubItems.Add(oku[3].ToString());
                LstVw_SilmeTalepleri.Items.Add(ekle);
            }
            baglanti.Close();
            LstVw_SilmeTalepleri.FullRowSelect = true;
        }

        public void kytbildirimleri()
        {
            Lst_VwYeniKytveYeniTlbBildirimleri.Items.Clear();
            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmtkytbildirim = new SqlCommand("select *from kayitbildirimleri ", baglanti);
            SqlDataReader okukytbildirim = kmtkytbildirim.ExecuteReader();
            while (okukytbildirim.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = okukytbildirim[1].ToString();
                ekle.SubItems.Add(okukytbildirim[2].ToString());
                Lst_VwYeniKytveYeniTlbBildirimleri.Items.Add(ekle);
            }
            baglanti.Close();
            LstVw_SilmeTalepleri.FullRowSelect = true;
        }

        private void OgrtAtamaListesi()
        {
            //    LstVw_OgrtAtama.Items.Clear();
            //    baglanti.Open();
            //    komut = new SqlCommand("select *from Vw_OgrtAtaList where OgrtAtamaTalebi=0", baglanti);
            //    okuyucu = komut.ExecuteReader();
            //    while (okuyucu.Read())
            //    {
            //        ListViewItem ekle = new ListViewItem();
            //        ekle.Text = okuyucu["SiniflarinOgrtmenleriID"].ToString();
            //        ekle.SubItems.Add(okuyucu["sinif"].ToString());
            //        ekle.SubItems.Add(okuyucu["Ogretmen"].ToString());
            //        ekle.SubItems.Add(okuyucu["EgitimTipi"].ToString());
            //        ekle.SubItems.Add(okuyucu["KursTipi"].ToString());
            //        ekle.SubItems.Add(okuyucu["Haftaicisonu"].ToString());
            //        ekle.SubItems.Add(okuyucu["Saat"].ToString());
            //        LstVw_OgrtAtama.Items.Add(ekle);
            //    }
            //    baglanti.Close();
            //    LstVw_OgrtAtama.FullRowSelect = true;
        }

        private void KytDondurmaListesi()
        {
            LstVw_KytDondurma.Items.Clear();

            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            komut = new SqlCommand("select *from Vw_kytdondurmaList where OgrDondurmaTalebi=1", baglanti);
            okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = okuyucu["ID"].ToString();
                ekle.SubItems.Add(okuyucu["Tcno"].ToString());
                ekle.SubItems.Add(okuyucu["ad"].ToString());
                ekle.SubItems.Add(okuyucu["soyad"].ToString());
                LstVw_KytDondurma.Items.Add(ekle);
            }
            baglanti.Close();
            LstVw_KytDondurma.FullRowSelect = true;
        }

        private void TxtBx_Tc_TextChanged(object sender, EventArgs e)
        {
            if (TxtBx_Tc.TextLength == 11)
            {
                MessageBox.Show("Maksimum sınıra ulaştınız!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
            PctrBx_OgrResim.SizeMode = PictureBoxSizeMode.StretchImage;
            PctrBx_OgrResim.ImageLocation = Application.StartupPath + "\\Resimler\\NoFoto.png";
        }

        private void button6_Click(object sender, EventArgs e)
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
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
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
                    item.SubItems.Add(okuyucu["resim"].ToString());
                    item.SubItems.Add(okuyucu["egitimtipi"].ToString());
                    item.SubItems.Add(okuyucu["kurssecimi"].ToString());
                    item.SubItems.Add(okuyucu["haftaicison"].ToString());
                    item.SubItems.Add(okuyucu["saat"].ToString());
                    item.SubItems.Add(okuyucu["sinif"].ToString());
                    item.SubItems.Add(okuyucu["Ogretmen"].ToString());
                    item.SubItems.Add(okuyucu["DeleteID"].ToString());
                    item.SubItems.Add(okuyucu["Mezun"].ToString());
                    item.SubItems.Add(okuyucu["Dondurma"].ToString());
                    listView2.Items.Add(item);
                    Btn_KytGuncelle.Enabled = true;
                    Btn_Kytsil.Enabled = true;
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

            Btn_Kytsil.Enabled = false;
            Btn_KytGuncelle.Enabled = false;
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

        private void button12_Click(object sender, EventArgs e)
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

        private void CmbBx_il_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbBx_il.SelectedIndex != -1)
            {
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlDataAdapter adaptor = new SqlDataAdapter("select * from ilceler where il = " + CmbBx_il.SelectedValue, baglanti);
                DataTable tablo = new DataTable();
                adaptor.Fill(tablo);
                CmbBx_ilce.ValueMember = "id";
                CmbBx_ilce.DisplayMember = "ilce";
                CmbBx_ilce.DataSource = tablo;
                baglanti.Close();
            }
        }

        private void ChckBx_Manuel_CheckedChanged(object sender, EventArgs e)
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
            openFileDialog1.Title = "Resim Aç";
            openFileDialog1.Filter = "Jpeg Dosyası (*.jpg)|*.jpg|Gif Dosyası (*.gif)|*.gif|Png Dosyası (*.png)|*.png|Tif Dosyası (*.tif)|*.tif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PctrBx_OgrResim.Image = Image.FromFile(openFileDialog1.FileName);
                Fotoyolu = openFileDialog1.FileName.ToString();
            }
            else
            {
                Fotoyolu = Application.StartupPath + "\\Resimler\\NoFoto.png";
                PctrBx_OgrResim.ImageLocation = Application.StartupPath + "\\Resimler\\NoFoto.png";
            }
            Bitmap bmpKucuk = new Bitmap(PctrBx_OgrResim.Image, 220, 150);
            PctrBx_OgrResim.SizeMode = PictureBoxSizeMode.CenterImage;
            PctrBx_OgrResim.SizeMode = PictureBoxSizeMode.StretchImage;
            PctrBx_OgrResim.Image = bmpKucuk;
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

                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand ogrguncellekmt = new SqlCommand("select * from kayitlar where id = @p1", baglanti);
                ogrguncellekmt.Parameters.AddWithValue("@p1", id);
                SqlDataReader ogrresimoku = ogrguncellekmt.ExecuteReader();
                while (ogrresimoku.Read())
                {
                    if (ogrresimoku["resim"].ToString() != "")
                    {
                        byte[] Resim = (byte[])ogrresimoku["resim"];

                        using (MemoryStream ms = new MemoryStream(Resim))
                        {
                            frm_Gun.PctrBx_OgrGuncelle.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        frm_Gun.PctrBx_OgrGuncelle.SizeMode = PictureBoxSizeMode.StretchImage;
                        frm_Gun.PctrBx_OgrGuncelle.ImageLocation = Application.StartupPath + "\\Resimler\\NoFoto.png";

                    }
                }

                baglanti.Close();

                frm_Gun.CmbBx_EğitimTipi.SelectedItem = listView2.Items[0].SubItems[15].Text;
                frm_Gun.CmbBx_KursSecim.SelectedItem = listView2.Items[0].SubItems[16].Text;
                frm_Gun.CmbBx_HaftaicSon.SelectedItem = listView2.Items[0].SubItems[17].Text;
                frm_Gun.CmbBx_SbhOglAks.SelectedItem = listView2.Items[0].SubItems[18].Text;
                frm_Gun.ShowDialog();
            }
            else { MessageBox.Show("Lütfen Önce Arama Yapınız"); }
        }

        private void button2_Click(object sender, EventArgs e) //kayıt sil düğme
        {
            DialogResult onaysil = MessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (onaysil == DialogResult.Yes)
            {
                //silme talebinden silme
                int kytsilid = int.Parse(listView2.Items[0].SubItems[0].Text);
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand kmttalepsil = new SqlCommand("delete from kytsilmetalepleri where ogrid=@p1", baglanti);
                kmttalepsil.Parameters.AddWithValue("@p1", kytsilid);
                kmttalepsil.ExecuteNonQuery();
                //sınıfsilme talebinden silme
                SqlCommand ogrsnftlbsil = new SqlCommand("delete from sinifsilmetalebi where ogrId=@p1", baglanti);
                ogrsnftlbsil.Parameters.AddWithValue("@p1", kytsilid);
                ogrsnftlbsil.ExecuteNonQuery();
                //ogrdondurmatalebi için
                SqlCommand kytdondurma = new SqlCommand("delete from kytdondurmatalebi where OgrId=@p1", baglanti);
                kytdondurma.Parameters.AddWithValue("@p1", kytsilid);
                kytdondurma.ExecuteNonQuery();
                SqlCommand kmtsil = new SqlCommand("delete from kayitlar where id=@p1", baglanti);
                kmtsil.Parameters.AddWithValue("@p1", kytsilid);
                if (kmtsil.ExecuteNonQuery() > 0)
                {
                    baglanti.Close();
                    kytsilmebilekle();
                    kytbildirimleri();
                    button11_Click(new object(), new EventArgs());
                    MessageBox.Show("Silme İşlemi Başarılı");
                }
                else { MessageBox.Show("Silme İşlemi Başarısız"); }
            }
            else { MessageBox.Show("İşlem İptal Edildi"); }
            baglanti.Close();
        }

        private void kytsilmebilekle()
        {
            Frm_AdminPanel frm_adm = (Frm_AdminPanel)Application.OpenForms["Frm_AdminPanel"];
            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmtkytbildirim = new SqlCommand("insert into kayitbildirimleri (bildirimler) values (@p1)", baglanti);
            kmtkytbildirim.Parameters.AddWithValue("@p1", listView2.Items[0].SubItems[6].Text + " TC Numaralı Öğrenci Kayıt  Silinmiştir.");
            if (kmtkytbildirim.ExecuteNonQuery() == 0)
            {
                MessageBox.Show("Bildirim Eklenemedi Sorun Var");
            }
            frm_adm.baglanti.Close();
        }

        public void button3_Click(object sender, EventArgs e) //sınıf temizlemeeee
        {
            CmbBx_Kurs_sinif.SelectedItem = null;
            CmbBx_Haftaicisonsinif.SelectedItem = null;
            CmbBx_Saatsinif.SelectedItem = null;
            cmbBx_egitimtipisinif.SelectedItem = null;
            LstVw_siniflar.Items.Clear();
            DtTmPckr_SnfBasTarhi.Value = DateTime.Now;
            DtTmPckr_SnfBitTarhi.Value = DateTime.Now;
            Cmb_SnfEgitimci.SelectedItem = null;
            Cmb_SnfEgitimci.Items.Clear();
            Cmb_SnfEgitimci.Enabled = false;
            Btn_OgrMezEt.Enabled = false;


            Cmb_SnfEgitimci.DataSource = null; //data ile çekilen combo item clearleme... ee:DD:D
            Btn_SinifKaydet.Enabled = false;


            PictureBox[] picturebosalt = { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16, pictureBox17 };
            foreach (var b in picturebosalt)
            {
                b.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            }

            ComboBox[] cmbbosalt = { Cmb1, Cmb2, Cmb3, Cmb4, Cmb5, Cmb6, Cmb7, Cmb8, Cmb9, Cmb10, Cmb11, Cmb12, Cmb13, Cmb14, Cmb15, Cmb16, Cmb17, cmb_ana, cmb_ana2 };
            for (int i = 0; i < cmbbosalt.Length; i++)
            {
                cmbbosalt[i].Items.Clear();
                cmbbosalt[i].SelectedItem = null;
                cmbbosalt[i].Enabled = false;
            }

            CheckBox[] chcbkapa = { chck1, chck2, chck3, chck4, chck5, chck6, chck7, chck8, chck9, chck10, chck11, chck12, chck13, chck14, chck15, chck16, chck17 };
            for (int i = 0; i < chcbkapa.Length; i++)
            {
                chcbkapa[i].Checked = false;
                chcbkapa[i].Enabled = false;
            }

        }

        public void Btn_OgrGetir_Click(object sender, EventArgs e)
        {
            //temizleme
            LstVw_siniflar.Items.Clear();
            DtTmPckr_SnfBasTarhi.Value = DateTime.Now;
            DtTmPckr_SnfBitTarhi.Value = DateTime.Now;
            Cmb_SnfEgitimci.SelectedItem = null;
            Cmb_SnfEgitimci.Items.Clear();
            Cmb_SnfEgitimci.Enabled = false;
            Btn_OgrMezEt.Enabled = false;


            Cmb_SnfEgitimci.DataSource = null; //data ile çekilen combo item clearleme... ee:DD:D
            Btn_SinifKaydet.Enabled = false;


            PictureBox[] picturebosalt = { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16, pictureBox17 };
            foreach (var b in picturebosalt)
            {
                b.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            }

            ComboBox[] cmbbosalt = { Cmb1, Cmb2, Cmb3, Cmb4, Cmb5, Cmb6, Cmb7, Cmb8, Cmb9, Cmb10, Cmb11, Cmb12, Cmb13, Cmb14, Cmb15, Cmb16, Cmb17, cmb_ana, cmb_ana2 };
            for (int i = 0; i < cmbbosalt.Length; i++)
            {
                cmbbosalt[i].Items.Clear();
                cmbbosalt[i].SelectedItem = null;
                cmbbosalt[i].Enabled = false;
            }

            CheckBox[] chcbkapa = { chck1, chck2, chck3, chck4, chck5, chck6, chck7, chck8, chck9, chck10, chck11, chck12, chck13, chck14, chck15, chck16, chck17 };
            for (int i = 0; i < chcbkapa.Length; i++)
            {
                chcbkapa[i].Checked = false;
                chcbkapa[i].Enabled = false;
            }
            //


            Btn_OgrMezEt.Enabled = false;
            LstVw_siniflar.Items.Clear();

            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmt = new SqlCommand("select *from Vw_siniflardetay where egitimtipi=@p1 and kurs=@p2 and hafta=@p3 and saat=@p4 and kayitliogrenci<=17", baglanti);
            kmt.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
            kmt.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
            kmt.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
            kmt.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
            SqlDataReader oku = kmt.ExecuteReader();
            bool varmi = false;
            while (oku.Read())
            {
                varmi = true;
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku[1].ToString();
                ekle.SubItems.Add(oku[2].ToString());
                ekle.SubItems.Add(oku[0].ToString());
                LstVw_siniflar.Items.Add(ekle);
            }
            LstVw_siniflar.FullRowSelect = true;
            if (varmi == false)
            {
                MessageBox.Show("Boş Sınıf Bulunamadı,Lütfen Sınıf Açınız");
            }
            baglanti.Close();
        }

        public string ID;

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ID))
            {
                Frm_SnfOgrtGuncelle frm_SnfOgrt = new Frm_SnfOgrtGuncelle();
                frm_SnfOgrt.id = ID;
                frm_SnfOgrt.ShowDialog();
            }
            else { MessageBox.Show("Önce Arama Yapıp Seçim Yapınız"); }
        }

        int ogrIDtalep = 0;

        private void button5_Click(object sender, EventArgs e) //Öğrenci Silme onay
        {
            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            if (seciliogrsilmeid > 0)
            {
                SqlCommand kmt = new SqlCommand("select * from kayitlar where tcno=@p1", baglanti);
                kmt.Parameters.AddWithValue("@p1", LstVw_SilmeTalepleri.SelectedItems[0].SubItems[1].Text);
                SqlDataReader oku = kmt.ExecuteReader();
                if (oku.Read())
                {
                    ogrIDtalep = int.Parse(oku[0].ToString());
                }
                oku.Close();
                //sınıfsilme talebinden silme
                SqlCommand ogrsnftlbsil = new SqlCommand("delete from sinifsilmetalebi where ogrId=@p1", baglanti);
                ogrsnftlbsil.Parameters.AddWithValue("@p1", ogrIDtalep);
                ogrsnftlbsil.ExecuteNonQuery();
                //ogrdondurmatalebi için
                SqlCommand kytdondurma = new SqlCommand("delete from kytdondurmatalebi where OgrId=@p1", baglanti);
                kytdondurma.Parameters.AddWithValue("@p1", ogrIDtalep);
                kytdondurma.ExecuteNonQuery();
                //sime talebinden silme
                SqlCommand kmttalepsil = new SqlCommand("delete from kytsilmetalepleri where id =@p1", baglanti);
                kmttalepsil.Parameters.AddWithValue("@p1", LstVw_SilmeTalepleri.SelectedItems[0].SubItems[0].Text);
                if (kmttalepsil.ExecuteNonQuery() > 0)
                {
                    SqlCommand kmt1 = new SqlCommand("delete from kayitlar where id=@p1", baglanti);
                    kmt1.Parameters.AddWithValue("@p1", ogrIDtalep);
                    if (kmt1.ExecuteNonQuery() > 0)
                    {
                        baglanti.Close();
                        kytbildirimleri();
                        seciliogrsilmeid = 0; //ki bidaha tıkladığında tekrar silmesin olmayacağında n hata vericektir
                        button5.Enabled = false;
                        button11.Enabled = false;
                        MessageBox.Show("Silme İşlemi Başarılı");
                    }
                    else { MessageBox.Show("Silme İşlemi Başarısız"); }
                }
                else { MessageBox.Show("Silme İşlemi Başarısız"); }
                baglanti.Close();
            }
            else { MessageBox.Show("Lütfen Seçi yapınız"); }
        }

        private void OgrSilmeTalebiOnayBil()
        {
            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmt = new SqlCommand("insert into kayitbildirimleri (bildirimler) values (@p1)", baglanti);
            kmt.Parameters.AddWithValue("@p1", LstVw_SilmeTalepleri.SelectedItems[0].SubItems[1].Text + " TC Numaralı Öğrenci Kaydı Silinmiştir.");
            if (kmt.ExecuteNonQuery() == 0)
            {
                MessageBox.Show("Bildirim eklenmedi hata vardır");
            }
            baglanti.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ogretmenekle();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            kullaniciekle();
        }

        private void Btn_OgrtAtaOnay_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmt = new SqlCommand("update siniflarinogretmenleri set OgrtAtamaTalebi = 1 where siniflarinogrtmenleriID = @p1", baglanti);
            kmt.Parameters.AddWithValue("@p1", LstVw_OgrtAtama.SelectedItems[0].Text);
            if (kmt.ExecuteNonQuery() > 0)
            {
                baglanti.Close();
                OgrtAtamaListesi();
                MessageBox.Show("İşlem Tamam!");
            }
            else { MessageBox.Show("İşlem başarısız!"); }
            baglanti.Close();
        }

        private void Btn_OgrtTalepred_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmtiptal = new SqlCommand("delete from siniflarinogretmenleri where siniflarinogrtmenleriID = @p1", baglanti);
            kmtiptal.Parameters.AddWithValue("@p1", LstVw_OgrtAtama.SelectedItems[0].Text);
            if (kmtiptal.ExecuteNonQuery() > 0)
            {
                baglanti.Close();
                OgrtAtamaListesi();
                MessageBox.Show("İşlem Tamam!");
            }
            else { MessageBox.Show("İşlem başarısız!"); }
            baglanti.Close();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        int DonOgrID = 0;

        private void Btn_KytOnay_Click(object sender, EventArgs e)//kyt dndurma talebi onay
        {
            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmt1 = new SqlCommand("select * from kayitlar where tcno=@p1", baglanti);
            kmt1.Parameters.AddWithValue("@p1", LstVw_KytDondurma.SelectedItems[0].SubItems[1].Text);
            SqlDataReader oku = kmt1.ExecuteReader();
            if (oku.Read())
            {
                DonOgrID = int.Parse(oku[0].ToString());
            }
            oku.Close();

            SqlCommand kmt = new SqlCommand("update kayitlar set dondurma=1 where id=@p1", baglanti);
            kmt.Parameters.AddWithValue("@p1", DonOgrID);
            if (kmt.ExecuteNonQuery() > 0)
            {
                SqlCommand kmtiptal = new SqlCommand("delete from kytdondurmatalebi where ID=@p1", baglanti);
                kmtiptal.Parameters.AddWithValue("@p1", seciliogrdonid);
                if (kmtiptal.ExecuteNonQuery() > 0)
                {
                    baglanti.Close();
                    KytDondurmaListesi();
                    Btn_KytDndrmiptal.Enabled = false;
                    Btn_KytOnay.Enabled = false;
                    MessageBox.Show("Kayıt Dondurma Başarılı!");
                }
                else { MessageBox.Show("Kayıt Dondurma  Başarısız!"); }
            }
            else { MessageBox.Show("Hata vardır "); }
            baglanti.Close();

        }

        private void Btn_KytDndrmiptal_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmtiptal = new SqlCommand("delete from kytdondurmatalebi where ID = @p1", baglanti);
            kmtiptal.Parameters.AddWithValue("@p1", LstVw_KytDondurma.SelectedItems[0].SubItems[0].Text);
            if (kmtiptal.ExecuteNonQuery() > 0)
            {
                baglanti.Close();
                KytDondurmaListesi();
                Btn_KytDndrmiptal.Enabled = false;
                Btn_KytOnay.Enabled = false;
                MessageBox.Show("Kayıt Dondurma İptali Başarılı");
            }
            else { MessageBox.Show("Kayıt Dondurma İptali Başarısız"); }
            baglanti.Close();
        }

        private void button14_Click(object sender, EventArgs e) //silme talebi red
        {
            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            if (seciliogrsilmeid > 0)
            {
                SqlCommand kmttalepsil = new SqlCommand("delete from kytsilmetalepleri where id =@p1", baglanti);
                kmttalepsil.Parameters.AddWithValue("@p1", LstVw_SilmeTalepleri.SelectedItems[0].SubItems[0].Text);
                if (kmttalepsil.ExecuteNonQuery() > 0)
                {
                    baglanti.Close();
                    OgrSilmeTalepleri();
                    kytbildirimleri();
                    seciliogrsilmeid = 0;
                    button5.Enabled = false;
                    button14.Enabled = false;
                    MessageBox.Show("Silme İşlemi Başarılı");
                }
                else { MessageBox.Show("Silme İşlemi Başarısız"); }
            }
            else { MessageBox.Show("Lütfen SEçim Yapınız"); }
            baglanti.Close();
        }

        int kulid;
        private void LstVw_KullaniciList_MouseClick(object sender, MouseEventArgs e)
        {
            Btn_Kulgun.Enabled = true;
            Btn_KulSil.Enabled = true;
            button13.Enabled = false;
            kulid = int.Parse(LstVw_KullaniciList.SelectedItems[0].SubItems[0].Text);
            CmbBx_Calisan.Text = LstVw_KullaniciList.SelectedItems[0].SubItems[1].Text;
            TxtBx_KullaniciAdi.Text = LstVw_KullaniciList.SelectedItems[0].SubItems[2].Text;
            TxtBx_KullaniciSifre.Text = LstVw_KullaniciList.SelectedItems[0].SubItems[3].Text;
            CmbBx_Yetki.Text = LstVw_KullaniciList.SelectedItems[0].SubItems[4].Text;
        }

        private void Btn_Kulgun_Click(object sender, EventArgs e)
        {
            DialogResult gunonay = MessageBox.Show("Kullanıcı Güncellensin mi?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (gunonay == DialogResult.Yes)
            {
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand kmt = new SqlCommand("update kullanicilar set calisanID=@p1,kullaniciadi=@p2,sifre=@p3,yetki=@p4 where id=@p5", baglanti);
                kmt.Parameters.AddWithValue("@p1", CmbBx_Calisan.SelectedValue);
                kmt.Parameters.AddWithValue("@p2", TxtBx_KullaniciAdi.Text);
                kmt.Parameters.AddWithValue("@p3", TxtBx_KullaniciSifre.Text);
                kmt.Parameters.AddWithValue("@p4", CmbBx_Yetki.Text);
                kmt.Parameters.AddWithValue("@p5", kulid);
                if (kmt.ExecuteNonQuery() > 0)
                {
                    baglanti.Close();
                    kullaniciListesi();
                    CmbBx_Calisan.SelectedIndex = -1;
                    TxtBx_KullaniciAdi.Clear();
                    TxtBx_KullaniciSifre.Clear();
                    CmbBx_Yetki.SelectedIndex = -1;
                    MessageBox.Show("Güncelleme Başarılı");
                }
                else { MessageBox.Show("Güncelleme Başarısız"); }
            }
            else { MessageBox.Show("İşlem İptal Edildi"); }
            baglanti.Close();
        }

        private void Btn_KulSil_Click(object sender, EventArgs e)
        {
            DialogResult silonay = MessageBox.Show("Kullanıcı Silinsin mi?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (silonay == DialogResult.Yes)
            {
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand kmt = new SqlCommand("delete from kullanicilar where id=@p1", baglanti);
                kmt.Parameters.AddWithValue("@p1", kulid);
                if (kmt.ExecuteNonQuery() > 0)
                {
                    baglanti.Close();
                    kullaniciListesi();
                    CmbBx_Calisan.SelectedIndex = -1;
                    TxtBx_KullaniciAdi.Clear();
                    TxtBx_KullaniciSifre.Clear();
                    CmbBx_Yetki.SelectedIndex = -1;
                    MessageBox.Show("Silme Başarılı");
                }
                else { MessageBox.Show("Silme Başarısız"); }
            }
            else { MessageBox.Show("İşlem İptal Edildi"); }
            baglanti.Close();
        }

        private void Btn_CalEkle_Click(object sender, EventArgs e)
        {
            byte[] resim;
            if (openFileDialog2.FileName == "openFileDialog2")
            {
                resimPath = Application.StartupPath + "\\Resimler\\NoFoto.png";
            }

            FileStream fs = new FileStream(resimPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            resim = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();

            if (!string.IsNullOrWhiteSpace(TxtBx_calTc.Text) && !string.IsNullOrWhiteSpace(TxtBx_CalisanAdiSoyadi.Text) && !string.IsNullOrWhiteSpace(TxtBx_calcep.Text) && !string.IsNullOrWhiteSpace(CmbBx_calil.Text) && !string.IsNullOrWhiteSpace(CmbBx_calcinsiyet.Text) && !string.IsNullOrWhiteSpace(TxtBx_calcilt.Text) && !string.IsNullOrWhiteSpace(TxtBx_calailesirano.Text) && !string.IsNullOrWhiteSpace(CmbBx_CalisanDepartman.Text) && !string.IsNullOrWhiteSpace(TxtBx_calpozisyon.Text) && !string.IsNullOrWhiteSpace(TxtBx_caluni.Text) && !string.IsNullOrWhiteSpace(TxtBx_calokubol.Text))
            {
                string mesaj = "Adı Soyadı : " + TxtBx_CalisanAdiSoyadi.Text + Environment.NewLine +
                "Tc No : " + TxtBx_calTc.Text + Environment.NewLine +
                "Cep No : " + TxtBx_calcep.Text + Environment.NewLine +
                "Ev No : " + TxtBx_calev.Text + Environment.NewLine +
                "E-Mail : " + eposta() + Environment.NewLine +
                "Şehir : " + CmbBx_calil.Text + Environment.NewLine +
                "İlçe : " + CmbBx_calilce.Text + Environment.NewLine +
                "Adres : " + TxtBx_caladres.Text + Environment.NewLine +
                "Aile Sıra No : " + TxtBx_calailesirano.Text + Environment.NewLine +
                "Cilt No : " + TxtBx_calcilt.Text + Environment.NewLine +
                "Cinsiyet : " + CmbBx_calcinsiyet.Text + Environment.NewLine +
                "Doğum Tarihi : " + DtTmPckr_CalDogtar.Text + Environment.NewLine +
                "Departman : " + CmbBx_CalisanDepartman.Text + Environment.NewLine +
                "Pozisyon : " + TxtBx_calpozisyon.Text + Environment.NewLine +
                "Üniversite : " + TxtBx_caluni.Text + Environment.NewLine +
                "Okuduğu Bölüm : " + TxtBx_calokubol.Text + Environment.NewLine +
                Environment.NewLine +

                "Kaydetmek İstediğinize Emin Misiniz ? ";

                DialogResult onay = MessageBox.Show(mesaj, "Çalışan Detay Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (onay == DialogResult.Yes)
                {
                    if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                    SqlCommand kmt = new SqlCommand("insert into calisanlar (tc,adsoyad,ceptel,evtel,eposta,ilID,ilceID,Adres,cinsiyet,ciltno,ailesirano,dogumtarihi,departman,pozisyon,universite,okudugubolum,resim) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", baglanti);
                    kmt.Parameters.AddWithValue("@p1", TxtBx_calTc.Text);
                    kmt.Parameters.AddWithValue("@p2", TxtBx_CalisanAdiSoyadi.Text);
                    kmt.Parameters.AddWithValue("@p3", TxtBx_calcep.Text);
                    kmt.Parameters.AddWithValue("@p4", TxtBx_calev.Text);
                    kmt.Parameters.AddWithValue("@p5", eposta());
                    kmt.Parameters.AddWithValue("@p6", CmbBx_calil.SelectedValue);
                    kmt.Parameters.AddWithValue("@p7", CmbBx_calilce.SelectedValue);
                    kmt.Parameters.AddWithValue("@p8", TxtBx_caladres.Text);
                    kmt.Parameters.AddWithValue("@p9", CmbBx_calcinsiyet.Text);
                    kmt.Parameters.AddWithValue("@p10", TxtBx_calcilt.Text);
                    kmt.Parameters.AddWithValue("@p11", TxtBx_calailesirano.Text);
                    kmt.Parameters.AddWithValue("@p12", DtTmPckr_CalDogtar.Value);
                    kmt.Parameters.AddWithValue("@p13", CmbBx_CalisanDepartman.Text);
                    kmt.Parameters.AddWithValue("@p14", TxtBx_calpozisyon.Text);
                    kmt.Parameters.AddWithValue("@p15", TxtBx_caluni.Text);
                    kmt.Parameters.AddWithValue("@p16", TxtBx_calokubol.Text);
                    kmt.Parameters.Add("@p17", SqlDbType.Image, resim.Length).Value = resim; //değişik :D
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Personel Başarıyla Eklendi");
                    }
                    else { MessageBox.Show("Ekleme Başarısız"); }
                }
                else { MessageBox.Show("İşlem İptal Edildi"); }
            }
            else { MessageBox.Show("Lütfen Zorunlu Alanları Doldurunuz"); }
            baglanti.Close();
        }

        public string eposta()
        {
            if (ChckBx_CalManuel.Checked == true)
            {
                return TxtBx_Caleposta.Text;
            }
            return TxtBx_Caleposta.Text + "@" + CmbBx_Caleposta.Text;
        }

        private void CmbBx_calil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbBx_il.SelectedIndex != -1)
            {
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlDataAdapter adaptor1 = new SqlDataAdapter("select * from ilceler where il=@p1", baglanti);
                adaptor1.SelectCommand.Parameters.AddWithValue("@p1", CmbBx_calil.SelectedValue);
                DataTable tablo3 = new DataTable();
                adaptor1.Fill(tablo3);
                CmbBx_calilce.ValueMember = "id";
                CmbBx_calilce.DisplayMember = "ilce";
                CmbBx_calilce.DataSource = tablo3;
                baglanti.Close();
            }
        }

        private void ChckBx_CalManuel_CheckedChanged(object sender, EventArgs e)
        {
            if (ChckBx_CalManuel.Checked == true)
            {
                TxtBx_Caleposta.Width = 171;
                label37.Visible = false;
                CmbBx_Caleposta.Visible = false;
                CmbBx_Caleposta.SelectedItem = null;
            }
            else
            {
                TxtBx_Caleposta.Width = 67;
                label37.Visible = true;
                CmbBx_Caleposta.Visible = true;
                CmbBx_Caleposta.SelectedItem = null;
            }
        }

        private void ChckBx_CalManuel_MouseHover(object sender, EventArgs e)
        {
            ToolTip aciklama = new ToolTip();
            aciklama.SetToolTip(ChckBx_CalManuel, "Manuel Giriş");
        }

        string resimPath = "";

        private void Btn_CalisanResimEkle_Click(object sender, EventArgs e)
        {
            openFileDialog2.Title = "Resim Aç";
            openFileDialog2.Filter = "Jpeg Dosyası (*.jpg)|*.jpg|Gif Dosyası (*.gif)|*.gif|Png Dosyası (*.png)|*.png|Tif Dosyası (*.tif)|*.tif";
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                PctrBx_Calresim.Image = Image.FromFile(openFileDialog2.FileName);
                resimPath = openFileDialog2.FileName.ToString();
            }
            else
            {
                resimPath = Application.StartupPath + "\\Resimler\\NoFoto.png";
                PctrBx_Calresim.ImageLocation = Application.StartupPath + "\\Resimler\\NoFoto.png";
            }

            Bitmap bmpKucuk = new Bitmap(PctrBx_Calresim.Image, 220, 150);
            PctrBx_Calresim.SizeMode = PictureBoxSizeMode.CenterImage;
            PctrBx_Calresim.SizeMode = PictureBoxSizeMode.StretchImage;
            PctrBx_Calresim.Image = bmpKucuk;
        }

        //string resimyok = Application.StartupPath + "\\Resimler\\NoFoto.png";

        private void Btn_CalisanBilgileriniTemizle_Click(object sender, EventArgs e)
        {
            PctrBx_Calresim.SizeMode = PictureBoxSizeMode.StretchImage;
            //PctrBx_Calresim.Image = Image.FromFile(resimyok);//objeliiii
            PctrBx_Calresim.ImageLocation = Application.StartupPath + "\\Resimler\\NoFoto.png";

            TxtBx_calTc.Clear();
            TxtBx_CalisanAdiSoyadi.Clear();
            TxtBx_calcep.Clear();
            TxtBx_calev.Clear();
            TxtBx_Caleposta.Clear();
            CmbBx_Caleposta.SelectedItem = null;
            CmbBx_calil.SelectedItem = null;
            CmbBx_calilce.SelectedItem = null;
            TxtBx_caladres.Clear();
            CmbBx_calcinsiyet.SelectedItem = null;
            TxtBx_calcilt.Clear();
            TxtBx_calailesirano.Clear();
            DtTmPckr_CalDogtar.Text = DateTime.Now.ToShortDateString();
            CmbBx_CalisanDepartman.SelectedItem = null;
            TxtBx_calpozisyon.Clear();
            TxtBx_caluni.Clear();
            TxtBx_calokubol.Clear();
            Btn_CalEkle.Enabled = true;
            Btn_CalGun.Enabled = false;
            Btn_CalSil.Enabled = false;
        }

        private void Btn_KullaniciBilgileriTemizle_Click(object sender, EventArgs e)
        {
            CmbBx_Calisan.SelectedItem = null;
            TxtBx_KullaniciAdi.Clear();
            TxtBx_KullaniciSifre.Clear();
            CmbBx_Yetki.SelectedItem = null;
            kulid = 0;
            Btn_Kulgun.Enabled = false;
            Btn_KulSil.Enabled = false;
            button13.Enabled = true;
        }
        public int calid;
        private void button8_Click(object sender, EventArgs e) //çalışanlar listesi
        {
            Frm_CalisanlarListesi frm_callis = new Frm_CalisanlarListesi();
            frm_callis.ShowDialog();
        }

        private void Btn_CalSil_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmt = new SqlCommand("delete from calisanlar where id=@p1", baglanti);
            kmt.Parameters.AddWithValue("@p1", calid);
            if (kmt.ExecuteNonQuery() > 0)
            {
                Btn_CalisanBilgileriniTemizle_Click(Btn_CalisanBilgileriniTemizle, new EventArgs());
                MessageBox.Show("Personel Silme İşlemi Başarılı");
            }
            else { MessageBox.Show("Silme İşlemi Başarısız"); }
            baglanti.Close();
        }

        private void Btn_CalGun_Click(object sender, EventArgs e)
        {
            byte[] resim;
            if (openFileDialog2.FileName == "openFileDialog2")
            {
                resimPath = Application.StartupPath + "\\Resimler\\NoFoto.png";
            }

            FileStream fs = new FileStream(resimPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            resim = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();

            if (!string.IsNullOrWhiteSpace(TxtBx_calTc.Text) && !string.IsNullOrWhiteSpace(TxtBx_CalisanAdiSoyadi.Text) && !string.IsNullOrWhiteSpace(TxtBx_calcep.Text) && !string.IsNullOrWhiteSpace(CmbBx_calil.Text) && !string.IsNullOrWhiteSpace(CmbBx_calcinsiyet.Text) && !string.IsNullOrWhiteSpace(TxtBx_calcilt.Text) && !string.IsNullOrWhiteSpace(TxtBx_calailesirano.Text) && !string.IsNullOrWhiteSpace(CmbBx_CalisanDepartman.Text) && !string.IsNullOrWhiteSpace(TxtBx_calpozisyon.Text) && !string.IsNullOrWhiteSpace(TxtBx_caluni.Text) && !string.IsNullOrWhiteSpace(TxtBx_calokubol.Text))
            {
                DialogResult onay = MessageBox.Show("Güncellemek İstediğinize Emin Misiniz?", "Güncelleme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (onay == DialogResult.Yes)
                {
                    if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                    SqlCommand kmt = new SqlCommand("update calisanlar set tc =@p1,adsoyad=@p2,ceptel=@p3,evtel=@p4,eposta=@p5,ilID=@p6,ilceID=@p7,Adres=@p8,cinsiyet=@p9,ciltno=@p10,ailesirano=@p11,dogumtarihi=@p12,departman=@p13,pozisyon=@p14,universite=@p15,okudugubolum=@p16,resim=@p17 where id=@p18", baglanti);
                    kmt.Parameters.AddWithValue("@p1", TxtBx_calTc.Text);
                    kmt.Parameters.AddWithValue("@p2", TxtBx_CalisanAdiSoyadi.Text);
                    kmt.Parameters.AddWithValue("@p3", TxtBx_calcep.Text);
                    kmt.Parameters.AddWithValue("@p4", TxtBx_calev.Text);
                    kmt.Parameters.AddWithValue("@p5", eposta());
                    kmt.Parameters.AddWithValue("@p6", CmbBx_calil.SelectedValue);
                    kmt.Parameters.AddWithValue("@p7", CmbBx_calilce.SelectedValue);
                    kmt.Parameters.AddWithValue("@p8", TxtBx_caladres.Text);
                    kmt.Parameters.AddWithValue("@p9", CmbBx_calcinsiyet.Text);
                    kmt.Parameters.AddWithValue("@p10", TxtBx_calcilt.Text);
                    kmt.Parameters.AddWithValue("@p11", TxtBx_calailesirano.Text);
                    kmt.Parameters.AddWithValue("@p12", DtTmPckr_CalDogtar.Value);
                    kmt.Parameters.AddWithValue("@p13", CmbBx_CalisanDepartman.Text);
                    kmt.Parameters.AddWithValue("@p14", TxtBx_calpozisyon.Text);
                    kmt.Parameters.AddWithValue("@p15", TxtBx_caluni.Text);
                    kmt.Parameters.AddWithValue("@p16", TxtBx_calokubol.Text);

                    SqlCommand rsmkmt = new SqlCommand("update calisanlar set resim = NULL where id=@p1", baglanti);
                    rsmkmt.Parameters.AddWithValue("@p1", calid);
                    if (rsmkmt.ExecuteNonQuery() > 0)
                    {
                        kmt.Parameters.Add("@p17", SqlDbType.Image, resim.Length).Value = resim;
                    }

                    kmt.Parameters.AddWithValue("@p18", calid);

                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Personel Güncellemesi Başarılı");
                    }
                    else { MessageBox.Show("Personel Güncellemesi Başarısız"); }
                }
                else { MessageBox.Show("İşlem İptal Edildi"); }
            }
            else { MessageBox.Show("Lütfen Zorunlu Alanları Doldurunuz"); }
            baglanti.Close();
        }

        private void Btn_OgretmenBilgileriniTemizle_Click(object sender, EventArgs e)
        {
            PctrBx_OgretmenResim.SizeMode = PictureBoxSizeMode.StretchImage;
            PctrBx_OgretmenResim.ImageLocation = Application.StartupPath + "\\Resimler\\NoFoto.png";

            TxtBx_OgretmenTC.Clear();
            TxtBx_OgretmenAdSoyAd.Clear();
            TxtBx_OgretmenCep.Clear();
            TxtBx_OgretmenEvTel.Clear();
            TxtBx_OgretmenEPosta.Clear();
            CmbBx_Ogretmenilce.SelectedItem = null;
            CmbBx_Ogretmenil.SelectedItem = null;
            CmbBx_OgretmenEPosta.SelectedItem = null;
            TxtBx_OgretmenAdres.Clear();
            CmbBx_OgretmenCinsiyet.SelectedItem = null;
            TxtBx_OgretmenCiltNo.Clear();
            TxtBx_OgretmenAileSiraNO.Clear();
            DtTmPckr_OgretmenDT.Text = DateTime.Now.ToShortDateString();
            CmbBx_OgretmenDepartman.SelectedItem = null;
            TxtBx_OgretmenPozisyon.Clear();
            TxtBx_OgretmenUni.Clear();
            TxtBx_OgretmenOkuduguBolum.Clear();
            Btn_OgretmenEkleKaydet.Enabled = true;
            Btn_OgretmenKaydiniGuncelle.Enabled = false;
            Btn_OgretmenKaydiniSil.Enabled = false;
        }

        string ogrtresim = "";
        private void Btn_OgretmenResimEkle_Click(object sender, EventArgs e)
        {
            openFileDialog3.Title = "Resim Aç";
            openFileDialog3.Filter = "Jpeg Dosyası (*.jpg)|*.jpg|Gif Dosyası (*.gif)|*.gif|Png Dosyası (*.png)|*.png|Tif Dosyası (*.tif)|*.tif";
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                PctrBx_OgretmenResim.Image = Image.FromFile(openFileDialog3.FileName);
                ogrtresim = openFileDialog3.FileName.ToString();
            }
            else
            {
                ogrtresim = Application.StartupPath + "\\Resimler\\NoFoto.png";
                PctrBx_OgretmenResim.ImageLocation = Application.StartupPath + "\\Resimler\\NoFoto.png";
            }

            Bitmap bmpKucuk = new Bitmap(PctrBx_OgretmenResim.Image, 220, 150);
            PctrBx_OgretmenResim.SizeMode = PictureBoxSizeMode.CenterImage;
            PctrBx_OgretmenResim.SizeMode = PictureBoxSizeMode.StretchImage;
            PctrBx_OgretmenResim.Image = bmpKucuk;
        }

        private void CmbBx_Ogretmenil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbBx_Ogretmenil.SelectedIndex != -1)
            {
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlDataAdapter ogretmeniladaptor = new SqlDataAdapter("select * from ilceler where il=@p1", baglanti);
                ogretmeniladaptor.SelectCommand.Parameters.AddWithValue("@p1", CmbBx_Ogretmenil.SelectedValue);
                DataTable ogretmenilcetablo = new DataTable();
                ogretmeniladaptor.Fill(ogretmenilcetablo);
                CmbBx_Ogretmenilce.ValueMember = "id";
                CmbBx_Ogretmenilce.DisplayMember = "ilce";
                CmbBx_Ogretmenilce.DataSource = ogretmenilcetablo;
                baglanti.Close();
            }
        }

        public string ogretmeneposta()
        {
            if (ChckBx_OgretmenEPosta.Checked == true)
            {
                return TxtBx_OgretmenEPosta.Text;
            }
            return TxtBx_OgretmenEPosta.Text + "@" + CmbBx_OgretmenEPosta.Text;
        }

        private void Btn_OgretmenEkleKaydet_Click(object sender, EventArgs e)
        {
            byte[] ogretmenresim;
            if (openFileDialog3.FileName == "openFileDialog3")
            {
                ogrtresim = Application.StartupPath + "\\Resimler\\NoFoto.png";
            }

            FileStream fsogretmen = new FileStream(ogrtresim, FileMode.Open, FileAccess.Read);
            BinaryReader brogretmen = new BinaryReader(fsogretmen);
            ogretmenresim = brogretmen.ReadBytes((int)fsogretmen.Length);
            brogretmen.Close();
            fsogretmen.Close();

            if (!string.IsNullOrWhiteSpace(TxtBx_OgretmenTC.Text) && !string.IsNullOrWhiteSpace(TxtBx_OgretmenAdSoyAd.Text) && !string.IsNullOrWhiteSpace(TxtBx_OgretmenCep.Text) && !string.IsNullOrWhiteSpace(CmbBx_Ogretmenil.Text) && !string.IsNullOrWhiteSpace(CmbBx_OgretmenCinsiyet.Text) && !string.IsNullOrWhiteSpace(TxtBx_OgretmenCiltNo.Text) && !string.IsNullOrWhiteSpace(TxtBx_OgretmenAileSiraNO.Text) && !string.IsNullOrWhiteSpace(CmbBx_OgretmenDepartman.Text) && !string.IsNullOrWhiteSpace(TxtBx_OgretmenPozisyon.Text) && !string.IsNullOrWhiteSpace(TxtBx_OgretmenUni.Text) && !string.IsNullOrWhiteSpace(TxtBx_OgretmenOkuduguBolum.Text))
            {
                string mesaj = "Adı Soyadı : " + TxtBx_OgretmenAdSoyAd.Text + Environment.NewLine +
                "Tc No : " + TxtBx_OgretmenTC.Text + Environment.NewLine +
                "Cep No : " + TxtBx_OgretmenCep.Text + Environment.NewLine +
                "Ev No : " + TxtBx_OgretmenEvTel.Text + Environment.NewLine +
                "E-Mail : " + ogretmeneposta() + Environment.NewLine +
                "Şehir : " + CmbBx_Ogretmenil.Text + Environment.NewLine +
                "İlçe : " + CmbBx_Ogretmenilce.Text + Environment.NewLine +
                "Adres : " + TxtBx_OgretmenAdres.Text + Environment.NewLine +
                "Aile Sıra No : " + TxtBx_OgretmenAileSiraNO.Text + Environment.NewLine +
                "Cilt No : " + TxtBx_OgretmenCiltNo.Text + Environment.NewLine +
                "Cinsiyet : " + CmbBx_OgretmenCinsiyet.Text + Environment.NewLine +
                "Doğum Tarihi : " + DtTmPckr_OgretmenDT.Text + Environment.NewLine +
                "Departman : " + CmbBx_OgretmenDepartman.Text + Environment.NewLine +
                "Pozisyon : " + TxtBx_OgretmenPozisyon.Text + Environment.NewLine +
                "Üniversite : " + TxtBx_OgretmenUni.Text + Environment.NewLine +
                "Okuduğu Bölüm : " + TxtBx_OgretmenOkuduguBolum.Text + Environment.NewLine +
                Environment.NewLine +

                "Kaydetmek İstediğinize Emin Misiniz ? ";

                DialogResult onay = MessageBox.Show(mesaj, "Öğretmen Detay Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (onay == DialogResult.Yes)
                {
                    if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                    SqlCommand kmt = new SqlCommand("insert into ogretmenkayitlari (tc,ogrtadsoyad,ceptel,evtel,eposta,ilID,ilceID,Adres,cinsiyet,ciltno,ailesirano,dogumtarihi,departman,pozisyon,universite,okudugubolum,resim) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", baglanti);
                    kmt.Parameters.AddWithValue("@p1", TxtBx_OgretmenTC.Text);
                    kmt.Parameters.AddWithValue("@p2", TxtBx_OgretmenAdSoyAd.Text);
                    kmt.Parameters.AddWithValue("@p3", TxtBx_OgretmenCep.Text);
                    kmt.Parameters.AddWithValue("@p4", TxtBx_OgretmenEvTel.Text);
                    kmt.Parameters.AddWithValue("@p5", ogretmeneposta());
                    kmt.Parameters.AddWithValue("@p6", CmbBx_Ogretmenil.SelectedValue);
                    kmt.Parameters.AddWithValue("@p7", CmbBx_Ogretmenilce.SelectedValue);
                    kmt.Parameters.AddWithValue("@p8", TxtBx_OgretmenAdres.Text);
                    kmt.Parameters.AddWithValue("@p9", CmbBx_OgretmenCinsiyet.Text);
                    kmt.Parameters.AddWithValue("@p10", TxtBx_OgretmenCiltNo.Text);
                    kmt.Parameters.AddWithValue("@p11", TxtBx_OgretmenAileSiraNO.Text);
                    kmt.Parameters.AddWithValue("@p12", DtTmPckr_OgretmenDT.Value);
                    kmt.Parameters.AddWithValue("@p13", CmbBx_OgretmenDepartman.Text);
                    kmt.Parameters.AddWithValue("@p14", TxtBx_OgretmenPozisyon.Text);
                    kmt.Parameters.AddWithValue("@p15", TxtBx_OgretmenUni.Text);
                    kmt.Parameters.AddWithValue("@p16", TxtBx_OgretmenOkuduguBolum.Text);
                    kmt.Parameters.Add("@p17", SqlDbType.Image, ogretmenresim.Length).Value = ogretmenresim;
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Btn_OgretmenBilgileriniTemizle_Click(Btn_OgretmenBilgileriniTemizle, new EventArgs());
                        MessageBox.Show("Öğretmen Başarıyla Eklendi!");
                        baglanti.Close();
                    }
                    else { MessageBox.Show("Öğretmen Ekleme işlemi Başarısız."); }
                }
                else { MessageBox.Show("İşlem İptal Edildi."); }
            }
            else { MessageBox.Show("Lütfen Zorunlu Alanları Doldurunuz."); }
            baglanti.Close();
        }

        private void ChckBx_OgretmenEPosta_CheckedChanged(object sender, EventArgs e)
        {
            if (ChckBx_OgretmenEPosta.Checked == true)
            {
                TxtBx_OgretmenEPosta.Width = 185;
                label59.Visible = false;
                CmbBx_OgretmenEPosta.Visible = false;
                CmbBx_OgretmenEPosta.SelectedItem = null;
            }
            else
            {
                TxtBx_OgretmenEPosta.Width = 67;
                label59.Visible = true;
                CmbBx_OgretmenEPosta.Visible = true;
                CmbBx_OgretmenEPosta.SelectedItem = null;
            }
        }

        private void Btn_Kullanmakilavuzu_Click(object sender, EventArgs e)
        {
            Frm_KullanmaKilavuzu frm_kk = new Frm_KullanmaKilavuzu();
            frm_kk.ShowDialog();
        }

        private void Btn_OgretmenlerListesi_Click(object sender, EventArgs e)
        {
            Frm_OgrtListesi frm_OgrtList = new Frm_OgrtListesi();
            frm_OgrtList.ShowDialog();
        }

        private void Btn_OgretmenKaydiniSil_Click(object sender, EventArgs e)
        {
            DialogResult onaysil = MessageBox.Show("Silmek İstediğinizden Emin Misiniz?", "Silme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onaysil == DialogResult.Yes)
            {
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand kmt = new SqlCommand("delete from ogretmenkayitlari where id=@p1", baglanti);
                kmt.Parameters.AddWithValue("@p1", ogrtID);
                if (kmt.ExecuteNonQuery() > 0)
                {
                    Btn_OgretmenBilgileriniTemizle_Click(Btn_OgretmenBilgileriniTemizle, new EventArgs());
                    MessageBox.Show("Silme İşlemi Başarılı");
                }
                else { MessageBox.Show("Silme İşlemi Başarısız"); }
            }
            else { MessageBox.Show("Silme İşlemi İptal Edildi"); }
            baglanti.Close();
        }



        private void Btn_OgretmenKaydiniGuncelle_Click(object sender, EventArgs e)
        {
            byte[] resim;
            if (openFileDialog3.FileName == "openFileDialog3")
            {
                ogrtresim = Application.StartupPath + "\\Resimler\\NoFoto.png";
            }

            FileStream fs = new FileStream(ogrtresim, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            resim = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();

            DialogResult onayguncelle = MessageBox.Show("Güncellemek İstediğinizden Emin Misiniz?", "Güncelleme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onayguncelle == DialogResult.Yes)
            {
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand kmt = new SqlCommand("update  ogretmenkayitlari set tc=@p2,ogrtadsoyad=@p3,ceptel=@p4,evtel=@p5,eposta=@p6,ilID=@p7,ilceID=@p8,adres=@p9,cinsiyet=@p10,ciltno=@p11,ailesirano=@p12,dogumtarihi=@p13,departman=@p14,pozisyon=@p15,universite=@p16,okudugubolum=@p17,resim=@p18  where id=@p1", baglanti);
                kmt.Parameters.AddWithValue("@p1", ogrtID);
                kmt.Parameters.AddWithValue("@p2", TxtBx_OgretmenTC.Text);
                kmt.Parameters.AddWithValue("@p3", TxtBx_OgretmenAdSoyAd.Text);
                kmt.Parameters.AddWithValue("@p4", TxtBx_OgretmenCep.Text);
                kmt.Parameters.AddWithValue("@p5", TxtBx_OgretmenEvTel.Text);
                kmt.Parameters.AddWithValue("@p6", TxtBx_OgretmenEvTel.Text);
                kmt.Parameters.AddWithValue("@p7", CmbBx_Ogretmenil.SelectedValue);
                kmt.Parameters.AddWithValue("@p8", CmbBx_Ogretmenilce.SelectedValue);
                kmt.Parameters.AddWithValue("@p9", TxtBx_OgretmenAdres.Text);
                kmt.Parameters.AddWithValue("@p10", CmbBx_OgretmenCinsiyet.Text);
                kmt.Parameters.AddWithValue("@p11", TxtBx_OgretmenCiltNo.Text);
                kmt.Parameters.AddWithValue("@p12", TxtBx_OgretmenAileSiraNO.Text);
                kmt.Parameters.AddWithValue("@p13", DtTmPckr_OgretmenDT.Value);
                kmt.Parameters.AddWithValue("@p14", CmbBx_OgretmenDepartman.Text);
                kmt.Parameters.AddWithValue("@p15", TxtBx_OgretmenPozisyon.Text);
                kmt.Parameters.AddWithValue("@p16", TxtBx_OgretmenUni.Text);
                kmt.Parameters.AddWithValue("@p17", TxtBx_OgretmenOkuduguBolum.Text);

                SqlCommand rsmkmt = new SqlCommand("update ogretmenkayitlari set resim = NULL where id=@p1", baglanti);
                rsmkmt.Parameters.AddWithValue("@p1", ogrtID);
                if (rsmkmt.ExecuteNonQuery() > 0)
                {
                    kmt.Parameters.Add("@p18", SqlDbType.Image, resim.Length).Value = resim;
                }
                if (kmt.ExecuteNonQuery() > 0)
                {
                    Btn_OgretmenBilgileriniTemizle_Click(Btn_OgretmenBilgileriniTemizle, new EventArgs());
                    MessageBox.Show("Güncelleme İşlemi Başarılı");
                }
                else { MessageBox.Show("Güncelleme İşlemi Başarısız"); }
            }
            else { MessageBox.Show("Güncelleme İşlemi İptal Edildi"); }
            baglanti.Close();
        }

        private void Btn_TumTalepleriYenile_Click(object sender, EventArgs e)
        {
            OgrtAtamaListesi();
            KytDondurmaListesi();
            OgrSilmeTalepleri();
            Btn_OgrtAtaOnay.Enabled = false;
            Btn_OgrtTalepred.Enabled = false;
            button14.Enabled = false;
            button5.Enabled = false;
            Btn_KytDndrmiptal.Enabled = false;
            Btn_KytOnay.Enabled = false;
        }

        int seciliogrsilmeid = 0;
        private void LstVw_SilmeTalepleri_MouseClick(object sender, MouseEventArgs e)
        {
            seciliogrsilmeid = int.Parse(LstVw_SilmeTalepleri.SelectedItems[0].SubItems[0].Text);
            if (seciliogrsilmeid > 0)
            {
                button5.Enabled = true;
                button14.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Btn_TumTalepleriYenile_Click(new object(), new EventArgs());
            kytbildirimleri();
        }

        int seciliogrdonid = 0;
        private void LstVw_KytDondurma_MouseClick(object sender, MouseEventArgs e)
        {
            seciliogrdonid = int.Parse(LstVw_KytDondurma.SelectedItems[0].SubItems[0].Text);
            if (seciliogrdonid > 0)
            {
                Btn_KytOnay.Enabled = true;
                Btn_KytDndrmiptal.Enabled = true;
            }
        }

        string ogretmen;

        public void LstVw_siniflar_MouseClick(object sender, MouseEventArgs e)
        {
            Cmb_SnfEgitimci.DataSource = null;
            Cmb_SnfEgitimci.SelectedItem = null;
            Cmb_SnfEgitimci.Items.Clear();
            Btn_SinifKaydet.Enabled = false;
            DtTmPckr_SnfBasTarhi.Value = DateTime.Now;
            DtTmPckr_SnfBitTarhi.Value = DateTime.Now;
            Btn_OgrMezEt.Enabled = true;
            ogretmen = "";

            chck1.Checked = false;
            chck2.Checked = false;
            chck3.Checked = false;
            chck4.Checked = false;
            chck5.Checked = false;
            chck6.Checked = false;
            chck7.Checked = false;
            chck8.Checked = false;
            chck9.Checked = false;
            chck10.Checked = false;
            chck11.Checked = false;
            chck12.Checked = false;
            chck13.Checked = false;
            chck14.Checked = false;
            chck15.Checked = false;
            chck16.Checked = false;
            chck17.Checked = false;

            chck1.Enabled = false;
            chck2.Enabled = false;
            chck3.Enabled = false;
            chck4.Enabled = false;
            chck5.Enabled = false;
            chck6.Enabled = false;
            chck7.Enabled = false;
            chck8.Enabled = false;
            chck9.Enabled = false;
            chck10.Enabled = false;
            chck11.Enabled = false;
            chck12.Enabled = false;
            chck13.Enabled = false;
            chck14.Enabled = false;
            chck15.Enabled = false;
            chck16.Enabled = false;
            chck17.Enabled = false;


            Cmb1.Items.Clear();
            Cmb2.Items.Clear();
            Cmb3.Items.Clear();
            Cmb4.Items.Clear();
            Cmb5.Items.Clear();
            Cmb6.Items.Clear();
            Cmb7.Items.Clear();
            Cmb8.Items.Clear();
            Cmb9.Items.Clear();
            Cmb10.Items.Clear();
            Cmb11.Items.Clear();
            Cmb12.Items.Clear();
            Cmb13.Items.Clear();
            Cmb14.Items.Clear();
            Cmb15.Items.Clear();
            Cmb16.Items.Clear();
            Cmb17.Items.Clear();

            Cmb1.Enabled = false;
            Cmb2.Enabled = false;
            Cmb3.Enabled = false;
            Cmb4.Enabled = false;
            Cmb5.Enabled = false;
            Cmb6.Enabled = false;
            Cmb7.Enabled = false;
            Cmb8.Enabled = false;
            Cmb9.Enabled = false;
            Cmb10.Enabled = false;
            Cmb11.Enabled = false;
            Cmb12.Enabled = false;
            Cmb13.Enabled = false;
            Cmb14.Enabled = false;
            Cmb15.Enabled = false;
            Cmb16.Enabled = false;
            Cmb17.Enabled = false;

            pictureBox1.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox2.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox3.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox4.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox5.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox6.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox7.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox8.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox9.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox10.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox11.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox12.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox13.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox14.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox15.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox16.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
            pictureBox17.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;

            cmb_ana.Items.Clear();
            cmb_ana2.Items.Clear();
            Cmb_ana3.Items.Clear();

            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmtogr = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and sinif=@p5 ", baglanti);
            kmtogr.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
            kmtogr.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
            kmtogr.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
            kmtogr.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
            kmtogr.Parameters.AddWithValue("@p5", LstVw_siniflar.SelectedItems[0].SubItems[0].Text);

            SqlDataReader oku = kmtogr.ExecuteReader();
            while (oku.Read())
            {
                cmb_ana.Items.Add(oku[1].ToString());
                ogretmen = oku["Ogretmen"].ToString();
            }
            oku.Close();

            SqlCommand kmttarihler = new SqlCommand("select *from Vw_SiniflarDetay where egitimtipi=@p1 and kurs=@p2 and hafta=@p3 and saat=@p4 and sinif=@p5", baglanti);
            kmttarihler.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
            kmttarihler.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
            kmttarihler.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
            kmttarihler.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
            kmttarihler.Parameters.AddWithValue("@p5", LstVw_siniflar.SelectedItems[0].SubItems[0].Text);

            SqlDataReader tarihoku = kmttarihler.ExecuteReader();
            while (tarihoku.Read())
            {
                //if (tarihoku["baslamatarihi"].ToString() != string.Empty && tarihoku["bitistarihi"].ToString() != string.Empty)
                //{
                DtTmPckr_SnfBasTarhi.Value = DateTime.Parse(tarihoku["baslamatarihi"].ToString());
                DtTmPckr_SnfBitTarhi.Value = DateTime.Parse(tarihoku["bitistarihi"].ToString());
                //}
            }

            baglanti.Close();


            if (ogretmen == string.Empty || ogretmen == null)

            {
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand kmt = new SqlCommand("select *from Vw_ogrtBranslari where bransadi=@p1", baglanti);
                kmt.Parameters.AddWithValue("@p1", CmbBx_Kurs_sinif.Text);
                SqlDataReader ogrtoku = kmt.ExecuteReader();
                while (ogrtoku.Read())
                {
                    Cmb_ana3.Items.Add(ogrtoku[2].ToString());
                }
                ogrtoku.Close();

                SqlCommand ogrtbosmu = new SqlCommand("select* from Vw_SiniflarDetay where  egitimtipi = @p1 and kurs = @p2 and hafta =@p3 and saat = @p4 and onay = 1 and (baslamatarihi between @p5 and @p6 or  bitistarihi between @p5 and @p6)", baglanti);
                ogrtbosmu.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                ogrtbosmu.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                ogrtbosmu.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                ogrtbosmu.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                ogrtbosmu.Parameters.AddWithValue("@p5", DtTmPckr_SnfBasTarhi.Value);
                ogrtbosmu.Parameters.AddWithValue("@p6", DtTmPckr_SnfBitTarhi.Value);
                SqlDataReader ogrtoku2 = ogrtbosmu.ExecuteReader();
                while (ogrtoku2.Read())
                {
                    Cmb_ana3.Items.Remove(ogrtoku2[7].ToString());
                }

                foreach (var c in Cmb_ana3.Items)
                {
                    Cmb_SnfEgitimci.Items.Add(c);
                }

                baglanti.Close();
                Cmb_SnfEgitimci.SelectedItem = null;
                Cmb_SnfEgitimci.Enabled = false;
            }
            else
            {
                Cmb_SnfEgitimci.SelectedItem = null;
                Cmb_SnfEgitimci.Items.Clear();
                Cmb_SnfEgitimci.Enabled = false;
                Cmb_SnfEgitimci.Items.Add(ogretmen);
                Cmb_SnfEgitimci.SelectedItem = ogretmen;
            }

            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
            kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
            kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
            kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
            kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
            SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
            while (oku2.Read())
            {
                cmb_ana2.Items.Add(oku2[1].ToString());
            }
            baglanti.Close();

            if (cmb_ana.Items.Count > 0)
            {
                cmb_ana.SelectedItem = cmb_ana.Items[0].ToString();
            }
            else if (cmb_ana.Items.Count == 0 & cmb_ana2.Items.Count >= 1)
            {
                Cmb1.Enabled = true;
                Cmb1.Items.Clear();

                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand kmtogrsayisi1 = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi1.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi1.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi1.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi1.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku3 = kmtogrsayisi1.ExecuteReader();
                while (oku3.Read())
                {
                    Cmb1.Items.Add(oku3[1].ToString());
                }
                baglanti.Close();
            }

            if (cmb_ana2.Items.Count > 0)
            {
                cmb_ana2.SelectedItem = cmb_ana2.Items[0].ToString();
            }
        }



        private void cmb_ana_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_ana.Items.Count >= 1)
            {
                pictureBox1.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb1.Enabled = false;
                Cmb2.Enabled = true;
                Cmb1.Items.Clear();
                Cmb1.Items.Add(cmb_ana.Items[0].ToString());
                Cmb1.Text = cmb_ana.Items[0].ToString();
            }
            else if (cmb_ana.Items.Count == 0 & cmb_ana2.Items.Count >= 1)
            {
                Cmb1.Enabled = true;
                Cmb1.Items.Clear();

                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb1.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
        }

        private void Cmb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb1.Text != string.Empty)  //cmb1 boşsa chck1 false olcak diyerek
            {
                chck1.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 2)
            {

                pictureBox2.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb2.Enabled = false;
                Cmb3.Enabled = true;
                Cmb2.Items.Clear();
                Cmb2.Items.Add(cmb_ana.Items[1].ToString());
                Cmb2.Text = cmb_ana.Items[1].ToString();
            }
            else if (cmb_ana.Items.Count == 1 && cmb_ana2.Items.Count >= 1)
            {
                Cmb1.Enabled = false;
                Cmb2.Enabled = true;
                Cmb2.Items.Clear();

                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb2.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb1.Text != string.Empty)
            {
                pictureBox1.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb1.SelectedItem);
                Cmb1.Enabled = false;

                if (Cmb2.Text == string.Empty)
                {
                    Cmb2.Enabled = true;
                }

                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }
        }

        private void Cmb2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb2.Text != string.Empty)
            {
                chck2.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 3 && Cmb3.Text == "")
            {
                pictureBox3.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb3.Enabled = false;
                Cmb4.Enabled = true;
                Cmb3.Items.Clear();
                Cmb3.Items.Add(cmb_ana.Items[2].ToString());
                Cmb3.Text = cmb_ana.Items[2].ToString();
            }
            else if (cmb_ana.Items.Count == 2 && cmb_ana2.Items.Count >= 1)
            {
                Cmb2.Enabled = false;
                Cmb3.Enabled = true;
                Cmb3.Items.Clear();

                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb3.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb2.Text != string.Empty)
            {
                pictureBox2.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb2.SelectedItem);
                Cmb2.Enabled = false;

                //buda eklencek...
                if (Cmb3.Text == string.Empty)
                {
                    Cmb3.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    //
                    //

                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                    ///////////////////////////////
                }
            }
        }

        private void Cmb3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb3.Text != string.Empty)
            {
                chck3.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 4 && Cmb4.Text == "")
            {
                pictureBox4.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb4.Enabled = false;
                Cmb5.Enabled = true;
                Cmb4.Items.Clear();
                Cmb4.Items.Add(cmb_ana.Items[3].ToString());
                Cmb4.Text = cmb_ana.Items[3].ToString();
            }
            else if (cmb_ana.Items.Count == 3 && cmb_ana2.Items.Count >= 1)
            {
                Cmb3.Enabled = false;
                Cmb4.Enabled = true;
                Cmb4.Items.Clear();
                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb4.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb3.Text != string.Empty)
            {
                pictureBox3.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb3.SelectedItem);
                Cmb3.Enabled = false;

                if (Cmb4.Text == string.Empty)
                {
                    Cmb4.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }
        }

        private void Cmb4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb4.Text != string.Empty)
            {
                chck4.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 5)
            {
                pictureBox5.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb5.Enabled = false;
                Cmb6.Enabled = true;
                Cmb5.Items.Clear();
                Cmb5.Items.Add(cmb_ana.Items[4].ToString());
                Cmb5.Text = cmb_ana.Items[4].ToString();
            }
            else if (cmb_ana.Items.Count == 4 && cmb_ana2.Items.Count >= 1)
            {
                Cmb4.Enabled = false;
                Cmb5.Enabled = true;
                Cmb5.Items.Clear();
                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb5.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb4.Text != string.Empty)
            {
                pictureBox4.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb4.SelectedItem);
                Cmb4.Enabled = false;

                if (Cmb5.Text == string.Empty)
                {
                    Cmb5.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }

        }

        private void Cmb5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb5.Text != string.Empty)
            {
                chck5.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 6)
            {
                pictureBox6.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb6.Enabled = false;
                Cmb7.Enabled = true;
                Cmb6.Items.Clear();
                Cmb6.Items.Add(cmb_ana.Items[5].ToString());
                Cmb6.Text = cmb_ana.Items[5].ToString();
            }
            else if (cmb_ana.Items.Count == 5 && cmb_ana2.Items.Count >= 1)
            {
                Cmb5.Enabled = false;
                Cmb6.Enabled = true;
                Cmb6.Items.Clear();
                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb6.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb5.Text != string.Empty)
            {
                pictureBox5.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb5.SelectedItem);
                Cmb5.Enabled = false;

                if (Cmb6.Text == string.Empty)
                {
                    Cmb6.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }
        }

        private void Cmb6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb6.Text != string.Empty)
            {
                chck6.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 7)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox7.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb7.Enabled = false;
                Cmb8.Enabled = true;
                Cmb7.Items.Clear();
                Cmb7.Items.Add(cmb_ana.Items[6].ToString());
                Cmb7.Text = cmb_ana.Items[6].ToString();
            }
            else if (cmb_ana.Items.Count == 6 && cmb_ana2.Items.Count >= 1)
            {
                Cmb6.Enabled = false;
                Cmb7.Enabled = true;
                Cmb7.Items.Clear();
                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb7.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb6.Text != string.Empty)
            {
                Cmb_SnfEgitimci.Enabled = true;
                Btn_SinifKaydet.Enabled = true;
                pictureBox6.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb6.SelectedItem);
                Cmb6.Enabled = false;

                if (Cmb7.Text == string.Empty)
                {
                    Cmb7.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }
        }

        private void Cmb7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb7.Text != string.Empty)
            {
                chck7.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 8)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox8.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb8.Enabled = false;
                Cmb9.Enabled = true;
                Cmb8.Items.Clear();
                Cmb8.Items.Add(cmb_ana.Items[7].ToString());
                Cmb8.Text = cmb_ana.Items[7].ToString();
            }
            else if (cmb_ana.Items.Count == 7 && cmb_ana2.Items.Count >= 1)
            {
                Cmb7.Enabled = false;
                Cmb8.Enabled = true;
                Cmb8.Items.Clear();

                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb8.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb7.Text != string.Empty)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox7.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb7.SelectedItem);
                Cmb7.Enabled = false;

                if (Cmb8.Text == string.Empty)
                {
                    Cmb8.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }
        }

        private void Cmb8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb8.Text != string.Empty)
            {
                chck8.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 9)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox9.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb9.Enabled = false;
                Cmb10.Enabled = true;
                Cmb9.Items.Clear();
                Cmb9.Items.Add(cmb_ana.Items[8].ToString());
                Cmb9.Text = cmb_ana.Items[8].ToString();
            }
            else if (cmb_ana.Items.Count == 8 && cmb_ana2.Items.Count >= 1)
            {
                Cmb8.Enabled = false;
                Cmb9.Enabled = true;
                Cmb9.Items.Clear();

                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb9.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb8.Text != string.Empty)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox8.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb8.SelectedItem);
                Cmb8.Enabled = false;

                if (Cmb9.Text == string.Empty)
                {
                    Cmb9.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }
        }

        private void Cmb9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb9.Text != string.Empty)
            {
                chck9.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 10)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox10.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb10.Enabled = false;
                Cmb11.Enabled = true;
                Cmb10.Items.Clear();
                Cmb10.Items.Add(cmb_ana.Items[9].ToString());
                Cmb10.Text = cmb_ana.Items[9].ToString();
            }
            else if (cmb_ana.Items.Count == 9 && cmb_ana2.Items.Count >= 1)
            {
                Cmb9.Enabled = false;
                Cmb10.Enabled = true;
                Cmb10.Items.Clear();

                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb10.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb9.Text != string.Empty)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox9.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb9.SelectedItem);
                Cmb9.Enabled = false;

                if (Cmb10.Text == string.Empty)
                {
                    Cmb10.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }
        }

        private void Cmb10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb10.Text != string.Empty)
            {
                chck10.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 11)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox11.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb11.Enabled = false;
                Cmb12.Enabled = true;
                Cmb11.Items.Clear();
                Cmb11.Items.Add(cmb_ana.Items[10].ToString());
                Cmb11.Text = cmb_ana.Items[10].ToString();
            }
            else if (cmb_ana.Items.Count == 10 && cmb_ana2.Items.Count >= 1)
            {
                Cmb10.Enabled = false;
                Cmb11.Enabled = true;
                Cmb11.Items.Clear();

                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb11.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb10.Text != string.Empty)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox10.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb10.SelectedItem);
                Cmb10.Enabled = false;

                if (Cmb11.Text == string.Empty)
                {
                    Cmb11.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }
        }

        private void Cmb11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb11.Text != string.Empty)
            {
                chck11.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 12)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox12.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb12.Enabled = false;
                Cmb13.Enabled = true;
                Cmb12.Items.Clear();
                Cmb12.Items.Add(cmb_ana.Items[11].ToString());
                Cmb12.Text = cmb_ana.Items[11].ToString();
            }
            else if (cmb_ana.Items.Count == 11 && cmb_ana2.Items.Count >= 1)
            {
                Cmb11.Enabled = false;
                Cmb12.Enabled = true;
                Cmb12.Items.Clear();

                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb12.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb11.Text != string.Empty)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox11.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb11.SelectedItem);
                Cmb11.Enabled = false;

                if (Cmb12.Text == string.Empty)
                {
                    Cmb12.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }
        }

        private void Cmb12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb12.Text != string.Empty)
            {
                chck12.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 13)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox13.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb13.Enabled = false;
                Cmb14.Enabled = true;
                Cmb13.Items.Clear();
                Cmb13.Items.Add(cmb_ana.Items[12].ToString());
                Cmb13.Text = cmb_ana.Items[12].ToString();
            }
            else if (cmb_ana.Items.Count == 12 && cmb_ana2.Items.Count >= 1)
            {
                Cmb12.Enabled = false;
                Cmb13.Enabled = true;
                Cmb13.Items.Clear();

                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb13.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb12.Text != string.Empty)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox12.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb12.SelectedItem);
                Cmb12.Enabled = false;

                if (Cmb13.Text == string.Empty)
                {
                    Cmb13.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }
        }

        private void Cmb13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb13.Text != string.Empty)
            {
                chck13.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 14)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox14.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb14.Enabled = false;
                Cmb15.Enabled = true;
                Cmb14.Items.Clear();
                Cmb14.Items.Add(cmb_ana.Items[13].ToString());
                Cmb14.Text = cmb_ana.Items[13].ToString();
            }
            else if (cmb_ana.Items.Count == 13 && cmb_ana2.Items.Count >= 1)
            {
                Cmb13.Enabled = false;
                Cmb14.Enabled = true;
                Cmb14.Items.Clear();

                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb14.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb13.Text != string.Empty)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox13.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb13.SelectedItem);
                Cmb13.Enabled = false;

                if (Cmb14.Text == string.Empty)
                {
                    Cmb14.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }
        }

        private void Cmb14_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb14.Text != string.Empty)
            {
                chck14.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 15)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox15.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb15.Enabled = false;
                Cmb16.Enabled = true;
                Cmb15.Items.Clear();
                Cmb15.Items.Add(cmb_ana.Items[14].ToString());
                Cmb15.Text = cmb_ana.Items[14].ToString();
            }
            else if (cmb_ana.Items.Count == 14 && cmb_ana2.Items.Count >= 1)
            {
                Cmb14.Enabled = false;
                Cmb15.Enabled = true;
                Cmb15.Items.Clear();

                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb15.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb14.Text != string.Empty)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox14.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb14.SelectedItem);
                Cmb14.Enabled = false;

                if (Cmb15.Text == string.Empty)
                {
                    Cmb15.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }
        }

        private void Cmb15_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb15.Text != string.Empty)
            {
                chck15.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 16)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox16.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb16.Enabled = false;
                Cmb17.Enabled = true;
                Cmb16.Items.Clear();
                Cmb16.Items.Add(cmb_ana.Items[15].ToString());
                Cmb16.Text = cmb_ana.Items[15].ToString();
            }
            else if (cmb_ana.Items.Count == 15 && cmb_ana2.Items.Count >= 1)
            {
                Cmb15.Enabled = false;
                Cmb16.Enabled = true;
                Cmb16.Items.Clear();

                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb16.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb15.Text != string.Empty)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox15.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb15.SelectedItem);
                Cmb15.Enabled = false;

                if (Cmb16.Text == string.Empty)
                {
                    Cmb16.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }
        }

        private void Cmb16_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb16.Text != string.Empty)
            {
                chck16.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 17)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox17.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                Cmb17.Enabled = false;
                Cmb17.Items.Clear();
                Cmb17.Items.Add(cmb_ana.Items[16].ToString());
                Cmb17.Text = cmb_ana.Items[16].ToString();
            }
            else if (cmb_ana.Items.Count == 16 && cmb_ana2.Items.Count >= 1)
            {
                Cmb16.Enabled = false;
                Cmb17.Enabled = true;
                Cmb17.Items.Clear();

                baglanti.Open();
                SqlCommand kmtogrsayisi = new SqlCommand("select *from Vw_cmbadsoyad where egitimtipi=@p1 and kurssecimi=@p2 and haftaicison=@p3 and saat=@p4 and (sinif is NULL or sinif='')", baglanti);
                kmtogrsayisi.Parameters.AddWithValue("@p1", cmbBx_egitimtipisinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p2", CmbBx_Kurs_sinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p3", CmbBx_Haftaicisonsinif.Text);
                kmtogrsayisi.Parameters.AddWithValue("@p4", CmbBx_Saatsinif.Text);
                SqlDataReader oku2 = kmtogrsayisi.ExecuteReader();
                while (oku2.Read())
                {
                    Cmb17.Items.Add(oku2[1].ToString());
                }
                baglanti.Close();
            }
            else if (Cmb16.Text != string.Empty)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox16.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb16.SelectedItem);
                Cmb16.Enabled = false;

                if (Cmb17.Text == string.Empty)
                {
                    Cmb17.Enabled = true;
                }

                if (Cmb1.SelectedItem == null)
                {
                    Cmb1.Items.Clear();
                }
                if (Cmb2.SelectedItem == null)
                {
                    Cmb2.Items.Clear();
                }
                if (Cmb3.SelectedItem == null)
                {
                    Cmb3.Items.Clear();
                }
                if (Cmb4.SelectedItem == null)
                {
                    Cmb4.Items.Clear();
                }
                if (Cmb5.SelectedItem == null)
                {
                    Cmb5.Items.Clear();
                }
                if (Cmb6.SelectedItem == null)
                {
                    Cmb6.Items.Clear();
                }
                if (Cmb7.SelectedItem == null)
                {
                    Cmb7.Items.Clear();
                }
                if (Cmb8.SelectedItem == null)
                {
                    Cmb8.Items.Clear();
                }
                if (Cmb9.SelectedItem == null)
                {
                    Cmb9.Items.Clear();
                }
                if (Cmb10.SelectedItem == null)
                {
                    Cmb10.Items.Clear();
                }
                if (Cmb11.SelectedItem == null)
                {
                    Cmb11.Items.Clear();
                }
                if (Cmb12.SelectedItem == null)
                {
                    Cmb12.Items.Clear();
                }
                if (Cmb13.SelectedItem == null)
                {
                    Cmb13.Items.Clear();
                }
                if (Cmb14.SelectedItem == null)
                {
                    Cmb14.Items.Clear();
                }
                if (Cmb15.SelectedItem == null)
                {
                    Cmb15.Items.Clear();
                }
                if (Cmb16.SelectedItem == null)
                {
                    Cmb16.Items.Clear();
                }
                if (Cmb17.SelectedItem == null)
                {
                    Cmb17.Items.Clear();
                }

                foreach (var a in cmb_ana2.Items)
                {
                    Cmb1.Items.Add(a);
                    Cmb2.Items.Add(a);
                    Cmb3.Items.Add(a);
                    Cmb4.Items.Add(a);
                    Cmb5.Items.Add(a);
                    Cmb6.Items.Add(a);
                    Cmb7.Items.Add(a);
                    Cmb8.Items.Add(a);
                    Cmb9.Items.Add(a);
                    Cmb10.Items.Add(a);
                    Cmb11.Items.Add(a);
                    Cmb12.Items.Add(a);
                    Cmb13.Items.Add(a);
                    Cmb14.Items.Add(a);
                    Cmb15.Items.Add(a);
                    Cmb16.Items.Add(a);
                    Cmb17.Items.Add(a);
                }
            }
        }

        private void Cmb17_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb17.Text != string.Empty)
            {
                chck17.Enabled = true;
            }

            if (cmb_ana.Items.Count >= 17)
            {
                Btn_SinifKaydet.Enabled = true;
                MessageBox.Show("Sınıf Doludur. Başka Sınıf Seçiniz yadaSınıf  Talep Ediniz");
            }
            else if (cmb_ana.Items.Count == 17 && cmb_ana2.Items.Count >= 1)
            {
                Cmb17.Enabled = false;
            }
            else if (Cmb17.Text != string.Empty)
            {
                Btn_SinifKaydet.Enabled = true;
                pictureBox17.Image = WindowsFormsApplication2.Properties.Resources.ogrdolu;
                cmb_ana2.Items.Remove(Cmb17.SelectedItem);
                Cmb17.Enabled = false;
                MessageBox.Show("Sınıf Dolmuştur Lütfen Kaydediniz");
            }
        }

        private void Btn_SinifKaydet_Click(object sender, EventArgs e)
        {
            //try
            //{
            if (!string.IsNullOrEmpty(Cmb_SnfEgitimci.Text))
            {
                DialogResult onaykaydet = MessageBox.Show("Kaydetmek İstediğinize Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (onaykaydet == DialogResult.Yes)
                {
                    ComboBox[] cmbler = { Cmb1, Cmb2, Cmb3, Cmb4, Cmb5, Cmb6, Cmb7, Cmb8, Cmb9, Cmb10, Cmb10, Cmb11, Cmb12, Cmb13, Cmb14, Cmb15, Cmb16, Cmb17 };
                    baglanti.Open();
                    for (int i = 0; i < cmbler.Count(); i++)
                    {
                        if (cmbler[i].SelectedIndex != -1)
                        {
                            SqlCommand kmt = new SqlCommand("update  kayitlar set Sinif=@p1 ,Ogretmen=@p2  where tcno=@p3 ", baglanti);
                            kmt.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[0].Text);
                            kmt.Parameters.AddWithValue("@p2", Cmb_SnfEgitimci.Text);
                            string[] tcal;
                            tcal = cmbler[i].Text.Split(' ');
                            kmt.Parameters.AddWithValue("@p3", tcal[2]);
                            kmt.ExecuteNonQuery();
                        }
                    }

                    if (Cmb_SnfEgitimci.SelectedIndex != -1)
                    {
                        int sinifid = 0;
                        SqlCommand kmt2 = new SqlCommand("select *from siniflar where sinif=@p1", baglanti);
                        kmt2.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[0].Text);
                        SqlDataReader kmt2oku = kmt2.ExecuteReader();
                        while (kmt2oku.Read())
                        {
                            sinifid = int.Parse(kmt2oku[0].ToString());
                        }
                        kmt2oku.Close();

                        // combolar dolu ise sayıyı 1 arttırıcakki sınıfta olan kişi sayısı belli olsun
                        int sayi = 0;
                        ComboBox[] cmbdolumu = { Cmb1, Cmb2, Cmb3, Cmb4, Cmb5, Cmb6, Cmb7, Cmb8, Cmb9, Cmb10, Cmb11, Cmb12, Cmb13, Cmb14, Cmb15, Cmb16, Cmb17 };
                        for (int i = 0; i < cmbdolumu.Count(); i++)
                        {
                            if (!string.IsNullOrEmpty(cmbdolumu[i].Text))
                            {
                                sayi++;
                            }
                        }
                        //
                        SqlCommand kmt3 = new SqlCommand("update siniflardetay set ogretmen = @p1 ,kayitliogrenci=@p9 where sinifid = @p2 and egitimtipi = @p3 and kurs = @p4 and hafta = @p5 and saat = @p6 and baslamatarihi=@p7 and bitistarihi=@p8", baglanti);
                        kmt3.Parameters.AddWithValue("@p1", Cmb_SnfEgitimci.Text);
                        kmt3.Parameters.AddWithValue("@p2", sinifid);
                        kmt3.Parameters.AddWithValue("@p3", cmbBx_egitimtipisinif.Text);
                        kmt3.Parameters.AddWithValue("@p4", CmbBx_Kurs_sinif.Text);
                        kmt3.Parameters.AddWithValue("@p5", CmbBx_Haftaicisonsinif.Text);
                        kmt3.Parameters.AddWithValue("@p6", CmbBx_Saatsinif.Text);
                        kmt3.Parameters.AddWithValue("@p7", DtTmPckr_SnfBasTarhi.Value);
                        kmt3.Parameters.AddWithValue("@p8", DtTmPckr_SnfBitTarhi.Value);
                        kmt3.Parameters.AddWithValue("@p9", sayi);
                        if (kmt3.ExecuteNonQuery() > 0)
                        {
                            button3_Click(new object(), new EventArgs());
                            MessageBox.Show("Sınıf Kaydetme Başarılı");
                        }
                        else { MessageBox.Show("İşlem Başarısız"); }
                    }
                    else { MessageBox.Show("Öğretmen seçiniz"); }
                }
                else { MessageBox.Show("İşlemi İptal Ettiniz"); }
                baglanti.Close();
            }
            else { MessageBox.Show("Lütfen Öğretmen Seçiniz"); }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Hata Oluştu, Yetkiliye Bildiriniz");
            //}
        }

        private void chck1_CheckedChanged(object sender, EventArgs e)
        {
            if (chck1.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb1.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb1.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt1 = new SqlCommand("update kayitlar set Sinif=NULL ,Ogretmen=NULL where tcno=@p1", baglanti);
                    kmt1.Parameters.AddWithValue("@p1", tcparcala[2]);
                    if (kmt1.ExecuteNonQuery() > 0)
                    {
                        Cmb1.Enabled = true;
                        cmb_ana2.Items.Add(Cmb1.Text);
                        cmb_ana.Items.Remove(Cmb1.Text);
                        Cmb1.Items.Clear();

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }

                        Cmb1.SelectedItem = null;
                        pictureBox1.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck1.Checked = false;
            }
        }

        private void chck2_CheckedChanged(object sender, EventArgs e)
        {
            if (chck2.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb2.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb2.Text.Split(' ');
                    baglanti.Open();

                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb2.Enabled = true;
                        cmb_ana2.Items.Add(Cmb2.Text);
                        cmb_ana.Items.Remove(Cmb2.Text);
                        Cmb2.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }

                        baglanti.Close();
                        Cmb2.SelectedItem = null;

                        pictureBox2.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck2.Checked = false;
            }
        }

        private void chck3_CheckedChanged(object sender, EventArgs e)
        {
            if (chck3.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb3.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb3.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL ,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb3.Enabled = true;
                        cmb_ana2.Items.Add(Cmb3.Text);
                        cmb_ana.Items.Remove(Cmb3.Text);
                        Cmb3.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }

                        baglanti.Close();
                        Cmb3.SelectedItem = null;

                        pictureBox3.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck3.Checked = false;
            }
        }

        private void chck4_CheckedChanged(object sender, EventArgs e)
        {
            if (chck4.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb4.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb4.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb4.Enabled = true;
                        cmb_ana2.Items.Add(Cmb4.Text);
                        cmb_ana.Items.Remove(Cmb4.Text);
                        Cmb4.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }

                        baglanti.Close();
                        Cmb4.SelectedItem = null;

                        pictureBox4.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());

                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck4.Checked = false;
            }
        }

        private void chck5_CheckedChanged(object sender, EventArgs e)
        {
            if (chck5.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb5.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb5.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb5.Enabled = true;
                        cmb_ana2.Items.Add(Cmb5.Text);
                        cmb_ana.Items.Remove(Cmb5.Text);
                        Cmb5.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }

                        baglanti.Close();
                        Cmb5.SelectedItem = null;

                        pictureBox5.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck5.Checked = false;
            }
        }

        private void chck6_CheckedChanged(object sender, EventArgs e)
        {
            if (chck6.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb6.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb6.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb6.Enabled = true;
                        cmb_ana2.Items.Add(Cmb6.Text);
                        cmb_ana.Items.Remove(Cmb6.Text);
                        Cmb6.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }

                        baglanti.Close();
                        Cmb6.SelectedItem = null;

                        pictureBox6.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck6.Checked = false;
            }
        }

        private void chck7_CheckedChanged(object sender, EventArgs e)
        {
            if (chck7.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb7.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb7.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb7.Enabled = true;
                        cmb_ana2.Items.Add(Cmb7.Text);
                        cmb_ana.Items.Remove(Cmb7.Text);
                        Cmb7.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }

                        baglanti.Close();
                        Cmb7.SelectedItem = null;

                        pictureBox7.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck7.Checked = false;
            }
        }

        private void chck8_CheckedChanged(object sender, EventArgs e)
        {
            if (chck8.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb8.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb8.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb8.Enabled = true;
                        cmb_ana2.Items.Add(Cmb8.Text);
                        cmb_ana.Items.Remove(Cmb8.Text);
                        Cmb8.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }

                        baglanti.Close();
                        Cmb8.SelectedItem = null;

                        pictureBox8.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck8.Checked = false;
            }
        }

        private void chck9_CheckedChanged(object sender, EventArgs e)
        {
            if (chck9.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb9.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb9.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb9.Enabled = true;
                        cmb_ana2.Items.Add(Cmb9.Text);
                        cmb_ana.Items.Remove(Cmb9.Text);
                        Cmb9.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }

                        baglanti.Close();
                        Cmb9.SelectedItem = null;

                        pictureBox9.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck9.Checked = false;
            }
        }

        private void chck10_CheckedChanged(object sender, EventArgs e)
        {
            if (chck10.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb10.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb10.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb10.Enabled = true;
                        cmb_ana2.Items.Add(Cmb10.Text);
                        cmb_ana.Items.Remove(Cmb10.Text);
                        Cmb10.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }
                        baglanti.Close();
                        Cmb10.SelectedItem = null;
                        pictureBox10.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck10.Checked = false;
            }
        }

        private void chck11_CheckedChanged(object sender, EventArgs e)
        {
            if (chck11.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb11.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb11.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb11.Enabled = true;
                        cmb_ana2.Items.Add(Cmb11.Text);
                        cmb_ana.Items.Remove(Cmb11.Text);
                        Cmb11.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }
                        baglanti.Close();
                        Cmb11.SelectedItem = null;
                        pictureBox11.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck11.Checked = false;

            }
        }

        private void chck12_CheckedChanged(object sender, EventArgs e)
        {
            if (chck12.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb12.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb12.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb12.Enabled = true;
                        cmb_ana2.Items.Add(Cmb12.Text);
                        cmb_ana.Items.Remove(Cmb12.Text);
                        Cmb12.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }
                        baglanti.Close();
                        Cmb12.SelectedItem = null;
                        pictureBox12.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck12.Checked = false;
            }
        }

        private void chck13_CheckedChanged(object sender, EventArgs e)
        {
            if (chck13.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb13.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb13.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb13.Enabled = true;
                        cmb_ana2.Items.Add(Cmb13.Text);
                        cmb_ana.Items.Remove(Cmb13.Text);
                        Cmb13.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }
                        baglanti.Close();
                        Cmb13.SelectedItem = null;
                        pictureBox13.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck13.Checked = false;
            }
        }

        private void chck14_CheckedChanged(object sender, EventArgs e)
        {
            if (chck14.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb14.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb14.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb14.Enabled = true;
                        cmb_ana2.Items.Add(Cmb14.Text);
                        cmb_ana.Items.Remove(Cmb14.Text);
                        Cmb14.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }
                        baglanti.Close();
                        Cmb14.SelectedItem = null;
                        pictureBox14.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck14.Checked = false;
            }
        }

        private void chck15_CheckedChanged(object sender, EventArgs e)
        {
            if (chck15.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb15.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb15.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb15.Enabled = true;
                        cmb_ana2.Items.Add(Cmb15.Text);
                        cmb_ana.Items.Remove(Cmb15.Text);
                        Cmb15.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }
                        baglanti.Close();
                        Cmb15.SelectedItem = null;
                        pictureBox15.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck15.Checked = false;
            }
        }

        private void chck16_CheckedChanged(object sender, EventArgs e)
        {
            if (chck16.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb16.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb16.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb16.Enabled = true;
                        cmb_ana2.Items.Add(Cmb16.Text);
                        cmb_ana.Items.Remove(Cmb16.Text);
                        Cmb16.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb17.Text == string.Empty)
                        {
                            Cmb17.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }
                        baglanti.Close();
                        Cmb16.SelectedItem = null;
                        pictureBox16.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck16.Checked = false;
            }
        }

        private void chck17_CheckedChanged(object sender, EventArgs e)
        {
            if (chck17.Checked == true)
            {
                DialogResult ogrsnfsil = MessageBox.Show(Cmb17.Text + " Bu Öğrenciyi Sınftan Çıkarmak İstediğinize Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ogrsnfsil == DialogResult.Yes)
                {
                    string[] tcparcala;
                    tcparcala = Cmb17.Text.Split(' ');
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL,Ogretmen=NULL where tcno=@p2", baglanti);
                    kmt.Parameters.AddWithValue("@p2", tcparcala[2]);
                    if (kmt.ExecuteNonQuery() > 0)
                    {
                        Cmb17.Enabled = true;
                        cmb_ana2.Items.Add(Cmb17.Text);
                        cmb_ana.Items.Remove(Cmb17.Text);
                        Cmb17.Items.Clear();

                        if (Cmb1.Text == string.Empty)
                        {
                            Cmb1.Items.Clear();
                        }

                        if (Cmb2.Text == string.Empty)
                        {
                            Cmb2.Items.Clear();
                        }

                        if (Cmb3.Text == string.Empty)
                        {
                            Cmb3.Items.Clear();
                        }

                        if (Cmb4.Text == string.Empty)
                        {
                            Cmb4.Items.Clear();
                        }

                        if (Cmb5.Text == string.Empty)
                        {
                            Cmb5.Items.Clear();
                        }

                        if (Cmb6.Text == string.Empty)
                        {
                            Cmb6.Items.Clear();
                        }

                        if (Cmb7.Text == string.Empty)
                        {
                            Cmb7.Items.Clear();
                        }

                        if (Cmb8.Text == string.Empty)
                        {
                            Cmb8.Items.Clear();
                        }

                        if (Cmb9.Text == string.Empty)
                        {
                            Cmb9.Items.Clear();
                        }

                        if (Cmb10.Text == string.Empty)
                        {
                            Cmb10.Items.Clear();
                        }

                        if (Cmb11.Text == string.Empty)
                        {
                            Cmb11.Items.Clear();
                        }

                        if (Cmb12.Text == string.Empty)
                        {
                            Cmb12.Items.Clear();
                        }

                        if (Cmb13.Text == string.Empty)
                        {
                            Cmb13.Items.Clear();
                        }

                        if (Cmb14.Text == string.Empty)
                        {
                            Cmb14.Items.Clear();
                        }

                        if (Cmb15.Text == string.Empty)
                        {
                            Cmb15.Items.Clear();
                        }

                        if (Cmb16.Text == string.Empty)
                        {
                            Cmb16.Items.Clear();
                        }

                        foreach (var f in cmb_ana2.Items)
                        {
                            Cmb1.Items.Add(f);
                            Cmb2.Items.Add(f);
                            Cmb3.Items.Add(f);
                            Cmb4.Items.Add(f);
                            Cmb5.Items.Add(f);
                            Cmb6.Items.Add(f);
                            Cmb7.Items.Add(f);
                            Cmb8.Items.Add(f);
                            Cmb9.Items.Add(f);
                            Cmb10.Items.Add(f);
                            Cmb11.Items.Add(f);
                            Cmb12.Items.Add(f);
                            Cmb13.Items.Add(f);
                            Cmb14.Items.Add(f);
                            Cmb15.Items.Add(f);
                            Cmb16.Items.Add(f);
                            Cmb17.Items.Add(f);
                            /// Ekleme
                        }
                        baglanti.Close();
                        Cmb17.SelectedItem = null;
                        pictureBox17.Image = WindowsFormsApplication2.Properties.Resources.ogrbos;
                        MessageBox.Show("Sınıftan Öğrenci Çıkarılmıştır.");

                        if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                        SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
                        ogrsayisi.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);

                        SqlDataReader oku = ogrsayisi.ExecuteReader();
                        int ogrsay = 0;
                        while (oku.Read())
                        {
                            ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
                        }
                        oku.Close();

                        ogrsay--;

                        SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
                        ogrdegis.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                        ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
                        ogrdegis.ExecuteNonQuery();
                        baglanti.Close();
                        Btn_OgrGetir_Click(new object(), new EventArgs());
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("İşleminiz İptal Edildi"); }
                baglanti.Close();
                chck17.Checked = false;
            }
        }

        private void Btn_SnfBosalt_Click(object sender, EventArgs e)
        {
            DialogResult snfbosaltonay = MessageBox.Show("Sınıfı Boşaltmak İstediğinize Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (snfbosaltonay == DialogResult.Yes)
            {
                ComboBox[] combolar = { Cmb1, Cmb2, Cmb3, Cmb4, Cmb5, Cmb6, Cmb7, Cmb8, Cmb9, Cmb10, Cmb11, Cmb12, Cmb13, Cmb14, Cmb15, Cmb16, Cmb17 };
                for (int i = 0; i < combolar.Length; i++)
                {
                    if (combolar[i].SelectedIndex != -1)
                    {
                        string[] tcparcala = combolar[i].Text.Split(' ');
                        baglanti.Open();
                        SqlCommand kmtsnfbosalt = new SqlCommand("update kayitlar set sinif = NULL, ogretmen = NULL where tcno = @p1", baglanti);
                        kmtsnfbosalt.Parameters.AddWithValue("@p1", tcparcala[2]);
                        kmtsnfbosalt.ExecuteNonQuery();
                        baglanti.Close();
                    }
                }
                //siniflardetay dakini e dersi sildik
                baglanti.Open();
                SqlCommand kmtsnfdetaybosalt = new SqlCommand("delete from siniflardetay where id = @p1  ", baglanti);
                kmtsnfdetaybosalt.Parameters.AddWithValue("@p1", LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                if (kmtsnfdetaybosalt.ExecuteNonQuery() > 0)
                {
                    button3_Click(new object(), new EventArgs());
                    MessageBox.Show("İşlem Başarılı!");
                }
                else { MessageBox.Show("İşlem Başarısız."); }
                baglanti.Close();
            }
        }

        private void button17_Click(object sender, EventArgs e)//sınıf oluştur
        {
            tabControl1.SelectedTab = tabPage13;
        }

        private void button12_Click_1(object sender, EventArgs e) //Öğrenci Mezun ET
        {
            if (LstVw_siniflar.SelectedItems[0].SubItems[0].Text != string.Empty)
            {
                baglanti.Open();
                Frm_OgrMezunEt mezun = new Frm_OgrMezunEt();
                ComboBox[] combolar = { Cmb1, Cmb2, Cmb3, Cmb4, Cmb5, Cmb6, Cmb7, Cmb8, Cmb9, Cmb10, Cmb11, Cmb12, Cmb13, Cmb14, Cmb15, Cmb16, Cmb17 };
                for (int i = 0; i < combolar.Length; i++)
                {
                    if (combolar[i].Text != string.Empty)
                    {
                        string[] tcparcala = combolar[i].Text.Split(' ');
                        SqlCommand kmt = new SqlCommand("select *from kayitlar where tcno=@p1", baglanti);
                        kmt.Parameters.AddWithValue("@p1", tcparcala[2]);
                        SqlDataReader oku = kmt.ExecuteReader();
                        while (oku.Read())
                        {
                            ListViewItem ekle = new ListViewItem();
                            ekle.Text = oku[0].ToString();
                            ekle.SubItems.Add(oku[6].ToString());
                            ekle.SubItems.Add(oku[1].ToString());
                            ekle.SubItems.Add(oku[2].ToString());
                            ekle.SubItems.Add(oku["Sinif"].ToString());
                            ekle.SubItems.Add(oku["kurssecimi"].ToString());
                            ekle.SubItems.Add(oku["Ogretmen"].ToString());
                            ekle.SubItems.Add(oku["egitimtipi"].ToString());
                            mezun.LstVw_OgrMezEtList.Items.Add(ekle);
                        }
                        oku.Close();
                    }
                }
                baglanti.Close();
                mezun.LstVw_OgrMezEtList.MultiSelect = true;
                mezun.LstVw_OgrMezEtList.CheckBoxes = true;
                mezun.snfdetayid = int.Parse(LstVw_siniflar.SelectedItems[0].SubItems[2].Text);
                mezun.ShowDialog();
            }
            else { MessageBox.Show("Lütfen Sınıf Seçiniz"); }
        }

        int snfid;

        private void LstVw_Sinif_MouseClick(object sender, MouseEventArgs e)
        {
            snfid = int.Parse(LstVw_Sinif.SelectedItems[0].SubItems[0].Text);
            TxtBx_Snf.Text = LstVw_Sinif.SelectedItems[0].SubItems[1].Text;
            TxtBx_Kont.Text = LstVw_Sinif.SelectedItems[0].SubItems[2].Text;
        }

        private void Btn_SnfSil_Click(object sender, EventArgs e)
        {
            DialogResult snfsilonay = MessageBox.Show("Sınıf silme işlemi; " + TxtBx_Snf.Text + " Numaralı sınıf ile alakalı tüm veri kayıtlarını (Derslikler, Öğrenciler, Öğretmen Dahil) silecektir. İşlemi onaylıyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (snfsilonay == DialogResult.Yes)
            {
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand kmtkyttemizle = new SqlCommand("update kayitlar set Sinif = NULL, Ogretmen = NULL where Sinif = @p1", baglanti);
                kmtkyttemizle.Parameters.AddWithValue("@p1", LstVw_Sinif.SelectedItems[0].SubItems[1].Text);
                if (kmtkyttemizle.ExecuteNonQuery() > -1)
                {
                    SqlCommand kmtsnfdtytemizle = new SqlCommand("delete from siniflardetay where sinifid = @p1", baglanti);
                    kmtsnfdtytemizle.Parameters.AddWithValue("@p1", LstVw_Sinif.SelectedItems[0].SubItems[0].Text);
                    if (kmtsnfdtytemizle.ExecuteNonQuery() > -1)
                    {
                        SqlCommand kmtsnfsil = new SqlCommand("delete from siniflar where id=@p1", baglanti);
                        kmtsnfsil.Parameters.AddWithValue("@p1", snfid);
                        if (kmtsnfsil.ExecuteNonQuery() > 0)
                        {
                            sinif();
                            MessageBox.Show("Sınıf Silme Başarılı");
                            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                            SqlDataAdapter OSnfCbmDoldur = new SqlDataAdapter("select * from siniflar", baglanti);
                            DataTable OSnfCbmDoldurTablo = new DataTable();
                            OSnfCbmDoldur.Fill(OSnfCbmDoldurTablo);
                            Cmb_OSnf.ValueMember = "id";
                            Cmb_OSnf.DisplayMember = "sinif";
                            Cmb_OSnf.DataSource = OSnfCbmDoldurTablo;
                            baglanti.Close();
                        }
                        else { MessageBox.Show("Sınıf Silme Başarısız"); }
                    }

                }
                baglanti.Close();
                sinifdetay();
                sinif();
            }
            else { MessageBox.Show("İşlem İptal Edildi"); }
        }

        private void Btn_SnfEkle_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand kmtsnfkntrl = new SqlCommand("select * from siniflar where sinif = @p1", baglanti);
            kmtsnfkntrl.Parameters.AddWithValue("@p1", TxtBx_Snf.Text);
            SqlDataReader okusnfkntrl = kmtsnfkntrl.ExecuteReader();
            bool ayni = false;
            while (okusnfkntrl.Read())
            {
                ayni = true;

            }
            okusnfkntrl.Close();
            if (ayni == false)
            {


                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand kmt = new SqlCommand("insert into siniflar (sinif,sinifkontejyan) values (@p1,@p2)", baglanti);
                kmt.Parameters.AddWithValue("@p1", TxtBx_Snf.Text);
                kmt.Parameters.AddWithValue("@p2", TxtBx_Kont.Text);
                if (kmt.ExecuteNonQuery() > 0)
                {
                    sinif();
                    if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                    SqlDataAdapter OSnfCbmDoldur = new SqlDataAdapter("select * from siniflar order by sinif", baglanti);
                    DataTable OSnfCbmDoldurTablo = new DataTable();
                    OSnfCbmDoldur.Fill(OSnfCbmDoldurTablo);
                    Cmb_OSnf.ValueMember = "id";
                    Cmb_OSnf.DisplayMember = "sinif";
                    Cmb_OSnf.DataSource = OSnfCbmDoldurTablo;
                    sinifdetay();
                    baglanti.Close();
                    MessageBox.Show("Sınıf Ekleme Başarılı");
                }
                else { MessageBox.Show("Sınıf Ekleme Başarısız"); }
            }
            else { MessageBox.Show("Bu sınıf numarası vardır, lütfen başka sınıf numarası yazınız."); }
            baglanti.Close();
        }

        private void button20_Click(object sender, EventArgs e) //sınıf temizleme
        {
            TxtBx_Snf.Clear();
            sinif();
        }

        int snfdtyid;

        private void LstVw_SinifDetay_MouseClick(object sender, MouseEventArgs e)
        {
            snfdtyid = int.Parse(LstVw_SinifDetay.SelectedItems[0].SubItems[0].Text);
            Cmb_OSnf.Text = LstVw_SinifDetay.SelectedItems[0].SubItems[1].Text;
            Cmb_OEgitimtipi.SelectedItem = LstVw_SinifDetay.SelectedItems[0].SubItems[3].Text;
            Cmb_OKurs.SelectedItem = LstVw_SinifDetay.SelectedItems[0].SubItems[4].Text;
            Cmb_OHafta.SelectedItem = LstVw_SinifDetay.SelectedItems[0].SubItems[5].Text;
            Cmb_OSaat.SelectedItem = LstVw_SinifDetay.SelectedItems[0].SubItems[6].Text;
            DtTmPckr_OBasTar.Text = LstVw_SinifDetay.SelectedItems[0].SubItems[8].Text;
            DtTmPckr_OBitTar.Text = LstVw_SinifDetay.SelectedItems[0].SubItems[9].Text;
        }

        private void Btn_BlgislmTmzle_Click(object sender, EventArgs e)
        {
            sinifdetay();
            Cmb_OSnf.SelectedItem = null;
            Cmb_OSaat.SelectedItem = null;
            Cmb_OHafta.SelectedItem = null;
            Cmb_OKurs.SelectedItem = null;
            Cmb_OEgitimtipi.SelectedItem = null;
            DtTmPckr_OBasTar.Value = DateTime.Now;
            DtTmPckr_OBitTar.Value = DateTime.Now;
        }

        private void Btn_BlgislmSil_Click(object sender, EventArgs e)
        {
            DialogResult snfsilonay = MessageBox.Show("Kayıtlı Dersiği ve Öğrencileri Silmek istediğinize Emin misiniz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (snfsilonay == DialogResult.Yes)
            {
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand kmtogrsnfdtysil = new SqlCommand("update kayitlar set Sinif = NULL, Ogretmen = NULL where sinif = @p1 and egitimtipi = @p2 and kurssecimi = @p3 and haftaicison = @p4 and saat = @p5", baglanti);
                kmtogrsnfdtysil.Parameters.AddWithValue("@p1", Cmb_OSnf.Text);
                kmtogrsnfdtysil.Parameters.AddWithValue("@p2", Cmb_OEgitimtipi.Text);
                kmtogrsnfdtysil.Parameters.AddWithValue("@p3", Cmb_OKurs.Text);
                kmtogrsnfdtysil.Parameters.AddWithValue("@p4", Cmb_OHafta.Text);
                kmtogrsnfdtysil.Parameters.AddWithValue("@p5", Cmb_OSaat.Text);
                kmtogrsnfdtysil.ExecuteNonQuery();

                snfdtyid = int.Parse(LstVw_SinifDetay.SelectedItems[0].SubItems[0].Text);
                SqlCommand kmtsnfdtysil = new SqlCommand("delete from siniflardetay where id = @p1 and baslamatarihi = @p2 and bitistarihi = @p3", baglanti);
                kmtsnfdtysil.Parameters.AddWithValue("@p1", snfdtyid);
                kmtsnfdtysil.Parameters.AddWithValue("@p2", DtTmPckr_OBasTar.Value);
                kmtsnfdtysil.Parameters.AddWithValue("@p3", DtTmPckr_OBitTar.Value);
                if (kmtsnfdtysil.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Derslik Silinmiştir, İşlem Başarılı.");
                }
                else { MessageBox.Show("Derslik Silinme Başarısız"); }
            }
            else { MessageBox.Show("İşlem İptal Edildi"); }
            baglanti.Close();
            sinifdetay();
        }

        private void Btn_BlgislmGuncelle_Click(object sender, EventArgs e)
        {

        }

        private void Btn_BlgislmSnfEkle_Click(object sender, EventArgs e)
        {
            if (Cmb_OSnf.Text != string.Empty && Cmb_OEgitimtipi.Text != string.Empty && Cmb_OKurs.Text != string.Empty && Cmb_OHafta.Text != string.Empty && Cmb_OSaat.Text != string.Empty && DtTmPckr_OBasTar.Text != DtTmPckr_OBitTar.Text)
            {
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand snfbosmu = new SqlCommand("select* from Vw_SiniflarDetay where  egitimtipi = @p1 and kurs = @p2 and hafta =@p3 and saat = @p4 and onay = 1 and (baslamatarihi between @p5 and @p6 or  bitistarihi between @p5 and @p6) and sinif=@p7", baglanti);
                //SqlCommand snfbosmu = new SqlCommand("select *from Vw_SiniflarDetay where  egitimtipi = @p1 and kurs = @p2 and hafta =@p3 and saat = @p4 and onay = 1 and ((baslamatarihi>=@p5 and baslamatarihi<=@p6) or ( bitistarihi>=@p5 and bitistarihi<=@p6)) and sinif=@p7", baglanti);
                snfbosmu.Parameters.AddWithValue("@p1", Cmb_OEgitimtipi.Text);
                snfbosmu.Parameters.AddWithValue("@p2", Cmb_OKurs.Text);
                snfbosmu.Parameters.AddWithValue("@p3", Cmb_OHafta.Text);
                snfbosmu.Parameters.AddWithValue("@p4", Cmb_OSaat.Text);
                snfbosmu.Parameters.AddWithValue("@p5", DtTmPckr_OBasTar.Value);
                snfbosmu.Parameters.AddWithValue("@p6", DtTmPckr_OBitTar.Value);
                snfbosmu.Parameters.AddWithValue("@p7", Cmb_OSnf.Text);
                bool bosmu = false;
                SqlDataReader oku = snfbosmu.ExecuteReader();
                if (oku.Read())
                {
                    bosmu = true;
                }
                oku.Close();
                if (bosmu == false)
                {
                    SqlCommand kmtsnfdtyekle = new SqlCommand("insert into siniflardetay (sinifid,egitimtipi,kurs,hafta,saat,baslamatarihi,bitistarihi,onay) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,1)", baglanti);
                    kmtsnfdtyekle.Parameters.AddWithValue("@p1", Cmb_OSnf.SelectedValue);
                    kmtsnfdtyekle.Parameters.AddWithValue("@p2", Cmb_OEgitimtipi.Text);
                    kmtsnfdtyekle.Parameters.AddWithValue("@p3", Cmb_OKurs.Text);
                    kmtsnfdtyekle.Parameters.AddWithValue("@p4", Cmb_OHafta.Text);
                    kmtsnfdtyekle.Parameters.AddWithValue("@p5", Cmb_OSaat.Text);
                    kmtsnfdtyekle.Parameters.AddWithValue("@p6", DtTmPckr_OBasTar.Value);
                    kmtsnfdtyekle.Parameters.AddWithValue("@p7", DtTmPckr_OBitTar.Value);
                    if (kmtsnfdtyekle.ExecuteNonQuery() > 0)
                    {
                        sinifdetay();
                        MessageBox.Show("Derslik Kayıt İşlemi Başarılı!");
                    }
                    else { MessageBox.Show("İşlem Başarısız!"); }
                }
                else
                {
                    MessageBox.Show("Girilen Bilgiler Dahilinde Boş Olamaz");
                }
                baglanti.Close();
            }
            else { MessageBox.Show("Lütfen Tüm Alanları Doldurunuz"); }
        }

        private void button2_Click_1(object sender, EventArgs e) //kayıt dondurma
        {
            DialogResult onaysil = MessageBox.Show("Öğrenci Kaydı Dondurmak İstediğinize Emin Misiniz?", "Dondurma Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (onaysil == DialogResult.Yes)
            {
                //silme talebinden silme
                int kytsilid = int.Parse(listView2.Items[0].SubItems[0].Text);
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand kmttalepsil = new SqlCommand("delete from kytsilmetalepleri where ogrid=@p1", baglanti);
                kmttalepsil.Parameters.AddWithValue("@p1", kytsilid);
                kmttalepsil.ExecuteNonQuery();




                //.....................................
                //sınıfsilme talebinden silme
                SqlCommand ogrsnftlbsil = new SqlCommand("delete from sinifsilmetalebi where ogrId=@p1", baglanti);
                ogrsnftlbsil.Parameters.AddWithValue("@p1", kytsilid);
                ogrsnftlbsil.ExecuteNonQuery();
                //ogrdondurmatalebi için
                SqlCommand kytdondurma = new SqlCommand("delete from kytdondurmatalebi where OgrId=@p1", baglanti);
                kytdondurma.Parameters.AddWithValue("@p1", kytsilid);
                kytdondurma.ExecuteNonQuery();
                SqlCommand kmtsil = new SqlCommand("delete from kayitlar where id=@p1", baglanti);
                kmtsil.Parameters.AddWithValue("@p1", kytsilid);
                if (kmtsil.ExecuteNonQuery() > 0)
                {
                    baglanti.Close();
                    kytsilmebilekle();
                    kytbildirimleri();
                    button11_Click(new object(), new EventArgs());
                    MessageBox.Show("Silme İşlemi Başarılı");
                }
                else { MessageBox.Show("Silme İşlemi Başarısız"); }
            }
            else { MessageBox.Show("İşlem İptal Edildi"); }
            baglanti.Close();
        }

        private void cmb_ana2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Cmb_ana3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click_2(object sender, EventArgs e)
        {

        }
    }
}

