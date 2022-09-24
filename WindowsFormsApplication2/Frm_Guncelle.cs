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
    public partial class Frm_Guncelle : Form
    {
        public Frm_Guncelle()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source = 185.81.154.166, 1433; Network Library = DBMSSOCN; Initial Catalog = kayitlar; User ID = hasan; Password=H.12345");

        public int id { get; set; }

        private void CmbBx_il_SelectedIndexChanged(object sender, EventArgs e)
        {
            Frm_AnaMenu frm_ana = new Frm_AnaMenu();
            if (CmbBx_il.SelectedIndex != -1)
            {
                frm_ana.baglanti.Open();
                frm_ana.adaptor = new SqlDataAdapter("select * from ilceler where il = " + CmbBx_il.SelectedValue, frm_ana.baglanti);
                DataTable tablo5 = new DataTable();
                frm_ana.adaptor.Fill(tablo5);
                CmbBx_ilce.ValueMember = "id";
                CmbBx_ilce.DisplayMember = "ilce";
                CmbBx_ilce.DataSource = tablo5;
                frm_ana.baglanti.Close();
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
            CmbBx_KursSecim.SelectedItem = null;
            CmbBx_HaftaicSon.SelectedItem = null;
            CmbBx_SbhOglAks.SelectedItem = null;
            CmbBx_ilce.Text = null;
            CmbBx_ilce.SelectedItem = null;
            CmbBx_Cinsiyet.SelectedItem = null;
            CmbBx_il.SelectedItem = null;
            CmbBx_il.Text = null;
            DtTmPckr_DogTar.Value = DateTime.Now;
            PctrBx_OgrGuncelle.SizeMode = PictureBoxSizeMode.StretchImage;
            PctrBx_OgrGuncelle.ImageLocation = Application.StartupPath + "\\Resimler\\NoFoto.png";

           
        }

        private void Btn_Guncelle_Click(object sender, EventArgs e)
        {
            Frm_AdminPanel frm = (Frm_AdminPanel)Application.OpenForms["Frm_AdminPanel"];

            byte[] resim;
            if (OgrGuncelle_OFD.FileName == "OgrGuncelle_OFD")
            {
                ogrguncellefoto = Application.StartupPath + "\\Resimler\\NoFoto.png";
            } else
            {
                ogrguncellefoto = Application.StartupPath + "\\Resimler\\NoFoto.png";
            }

            FileStream fs = new FileStream(ogrguncellefoto, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            resim = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();

            baglanti.Open();
            SqlCommand komutGuncelle = new SqlCommand("update kayitlar set ad=@p1 , soyad=@p2,cepno=@p3,evtel=@p4,eposta=@p5,tcno=@p6,il=@p7,ilce=@p8,adres=@p9,cinsiyet=@p10,ciltno=@p11,ailesirano=@p12,dogumtarihi=@p13,resim=@p14,egitimtipi=@p15,kurssecimi=@p16,haftaicison=@p17,saat=@p18 where id=@p19", baglanti);
            komutGuncelle.Parameters.AddWithValue("@p19", id);
            komutGuncelle.Parameters.AddWithValue("@p1", TxtBx_Ad.Text);
            komutGuncelle.Parameters.AddWithValue("@p2", TxtBx_Soyad.Text);
            komutGuncelle.Parameters.AddWithValue("@p3", TxtBx_Cep.Text);
            komutGuncelle.Parameters.AddWithValue("@p4", TxtBx_Ev.Text);
            komutGuncelle.Parameters.AddWithValue("@p5", TxtBx_EPosta.Text);
            komutGuncelle.Parameters.AddWithValue("@p6", TxtBx_Tc.Text);
            komutGuncelle.Parameters.AddWithValue("@p7", CmbBx_il.Text);
            komutGuncelle.Parameters.AddWithValue("@p8", CmbBx_ilce.Text);
            komutGuncelle.Parameters.AddWithValue("@p9", TxtBx_Adres.Text);
            komutGuncelle.Parameters.AddWithValue("@p10", CmbBx_Cinsiyet.Text);
            komutGuncelle.Parameters.AddWithValue("@p11", TxtBx_Cilt.Text);
            komutGuncelle.Parameters.AddWithValue("@p12", TxtBx_AileSira.Text);
            komutGuncelle.Parameters.AddWithValue("@p13", DtTmPckr_DogTar.Value);

            SqlCommand rsmnullkmt = new SqlCommand("update kayitlar set resim = NULL where id = @p1", baglanti);
            rsmnullkmt.Parameters.AddWithValue("@p1", id);
            if (rsmnullkmt.ExecuteNonQuery()>0)
            {
            komutGuncelle.Parameters.Add("@p14", SqlDbType.Image, resim.Length).Value = resim;
            }
            komutGuncelle.Parameters.AddWithValue("@p15", CmbBx_EğitimTipi.Text);
            komutGuncelle.Parameters.AddWithValue("@p16", CmbBx_KursSecim.Text);
            komutGuncelle.Parameters.AddWithValue("@p17", CmbBx_HaftaicSon.Text);
            komutGuncelle.Parameters.AddWithValue("@p18", CmbBx_SbhOglAks.Text);
            if (komutGuncelle.ExecuteNonQuery() > 0)
            {
                baglanti.Close();
                MessageBox.Show("Güncelleştirme İşlemi Başarılı...");

                frm.listView2.Items.Clear();
                baglanti.Close();
                frm.baglanti.Open();
                frm.komut = new SqlCommand("Select * From kayitlar where ad = '" + frm.TxtBx_AdAra.Text + "' and soyad = '" + frm.TxtBx_SoyadAra.Text + "' and tcno = '" + frm.TxtBx_TcAra.Text + "'", frm.baglanti);
                frm.komut.ExecuteNonQuery();
                frm.okuyucu = frm.komut.ExecuteReader();
                while (frm.okuyucu.Read())
                {
                    ListViewItem item = new ListViewItem(frm.okuyucu["id"].ToString());
                    item.SubItems.Add(frm.okuyucu["ad"].ToString());
                    item.SubItems.Add(frm.okuyucu["soyad"].ToString());
                    item.SubItems.Add(frm.okuyucu["cepno"].ToString());
                    item.SubItems.Add(frm.okuyucu["evtel"].ToString());
                    item.SubItems.Add(frm.okuyucu["eposta"].ToString());
                    item.SubItems.Add(frm.okuyucu["tcno"].ToString());
                    item.SubItems.Add(frm.okuyucu["il"].ToString());
                    item.SubItems.Add(frm.okuyucu["ilce"].ToString());
                    item.SubItems.Add(frm.okuyucu["adres"].ToString());
                    item.SubItems.Add(frm.okuyucu["cinsiyet"].ToString());
                    item.SubItems.Add(frm.okuyucu["ciltno"].ToString());
                    item.SubItems.Add(frm.okuyucu["ailesirano"].ToString());
                    item.SubItems.Add(frm.okuyucu["dogumtarihi"].ToString());
                    item.SubItems.Add(frm.okuyucu["resim"].ToString());
                    item.SubItems.Add(frm.okuyucu["egitimtipi"].ToString());
                    item.SubItems.Add(frm.okuyucu["kurssecimi"].ToString());
                    item.SubItems.Add(frm.okuyucu["haftaicison"].ToString());
                    item.SubItems.Add(frm.okuyucu["saat"].ToString());
                    frm.listView2.Items.Add(item);
                }
                frm.baglanti.Close();
                frm.TxtBx_TcAra.Clear();
                frm.TxtBx_AdAra.Clear();
                frm.TxtBx_SoyadAra.Clear();
                kytgünbildirimi();
                frm.kytbildirimleri();
                Close();
            }
            else { MessageBox.Show("Güncelleştirme İşlemi Başarısız!"); }
            baglanti.Close();
        }

        private void kytgünbildirimi()
        {
            Frm_AdminPanel frm_adm = (Frm_AdminPanel)Application.OpenForms["Frm_AdminPanel"];
            frm_adm.Lst_VwYeniKytveYeniTlbBildirimleri.Items.Clear();
            frm_adm.baglanti.Open();
            SqlCommand kmtkytbildirim = new SqlCommand("insert into kayitbildirimleri (bildirimler) values (@p1)", frm_adm.baglanti);
            kmtkytbildirim.Parameters.AddWithValue("@p1", TxtBx_Tc.Text + " TC Numaralı Öğrencinin Kaydı Güncellenmiştir.");
            kmtkytbildirim.ExecuteNonQuery();
            frm_adm.baglanti.Close();
        }

        private void TxtBx_Ad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                       && !char.IsSeparator(e.KeyChar);
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Sadece harf girebilirsiniz...", "Uyarı");
            }
        }

        private void TxtBx_Tc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Sadece rakam girebilirsiniz...", "Uyarı");
            }
        }

        private void Frm_Guncelle_Load(object sender, EventArgs e)
        {
            TxtBx_Tc.MaxLength = 11;
        }

        public string ogrguncellefoto;

        private void Btn_ResimEkle_Click(object sender, EventArgs e)
        {
            OgrGuncelle_OFD.Title = "Resim Aç";
            OgrGuncelle_OFD.Filter = "Jpeg Dosyası (*.jpg)|*.jpg|Gif Dosyası (*.gif)|*.gif|Png Dosyası (*.png)|*.png|Tif Dosyası (*.tif)|*.tif";
            if (OgrGuncelle_OFD.ShowDialog() == DialogResult.OK)
            {
                PctrBx_OgrGuncelle.Image = Image.FromFile(OgrGuncelle_OFD.FileName);
                ogrguncellefoto = OgrGuncelle_OFD.FileName.ToString();
            }
            else
            {
                ogrguncellefoto = Application.StartupPath + "\\Resimler\\NoFoto.png";
                PctrBx_OgrGuncelle.ImageLocation = Application.StartupPath + "\\Resimler\\NoFoto.png";
            }
            Bitmap bmpKucuk = new Bitmap(PctrBx_OgrGuncelle.Image, 220, 150);
            PctrBx_OgrGuncelle.SizeMode = PictureBoxSizeMode.CenterImage;
            PctrBx_OgrGuncelle.SizeMode = PictureBoxSizeMode.StretchImage;
            PctrBx_OgrGuncelle.Image = bmpKucuk;
        }
    }
}
