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
    public partial class Frm_OgrtAta : Form
    {
        public Frm_OgrtAta()
        {
            InitializeComponent();
        }

        public int snfid;

        private void Frm_OgrtAta_Load(object sender, EventArgs e)
        {

        }

        private void Btn_OgrtAta_Click(object sender, EventArgs e)
        {
            Frm_AnaMenu frm_ana = new Frm_AnaMenu();
            if (cmbBx_egitimtipi.Text != string.Empty & CmbBx_Kurs.Text != string.Empty & CmbBx_Haftaicison.Text != string.Empty & CmbBx_Saat.Text != string.Empty & CmbBx_Ogrtler.Text != string.Empty)
            {
                int bosmu = 1;
                frm_ana.baglanti.Open();//öğretmen atama için bosmu sorgulaması 
                SqlCommand ogrtbosmu = new SqlCommand("select count(SiniflarinOgrtmenleriID) from siniflarinogretmenleri where ogrtID=@p1 and haftaicisonu=@p2 and saat=@p3 and OgrtAtamaTalebi=1", frm_ana.baglanti);
                ogrtbosmu.Parameters.AddWithValue("@p1", CmbBx_Ogrtler.SelectedValue);
                ogrtbosmu.Parameters.AddWithValue("@p2", CmbBx_Haftaicison.Text);
                ogrtbosmu.Parameters.AddWithValue("@p3", CmbBx_Saat.Text);
                SqlDataReader oku = ogrtbosmu.ExecuteReader();
                while (oku.Read())
                {
                    bosmu = int.Parse(oku[0].ToString());
                }
                frm_ana.baglanti.Close();
                if (bosmu == 0)
                {
                    //öğretmenin boş olduunu anladık şimdi parametreleri giriyoz yalnız talep 0 yapçaz ki yöneticide görsün
                    frm_ana.baglanti.Open();
                    SqlCommand kmttalep = new SqlCommand("insert into siniflarinogretmenleri (SinifID,OgrtID,egitimtipi,kurstipi,haftaicisonu,saat,ogrtatamatalebi) values(@p1,@p2,@p3,@p4,@p5,@p6,0) ", frm_ana.baglanti);
                    kmttalep.Parameters.AddWithValue("@p1", snfid);
                    kmttalep.Parameters.AddWithValue("@p2", CmbBx_Ogrtler.SelectedValue);
                    kmttalep.Parameters.AddWithValue("@p3", cmbBx_egitimtipi.Text);
                    kmttalep.Parameters.AddWithValue("@p4", CmbBx_Kurs.Text);
                    kmttalep.Parameters.AddWithValue("@p5", CmbBx_Haftaicison.Text);
                    kmttalep.Parameters.AddWithValue("@p6", CmbBx_Saat.Text);
                    if (kmttalep.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Öğretmen Atama Talebi Gönderildi");
                        Close();
                    }
                    else { MessageBox.Show("İşlem Başarısız"); }
                }
                else { MessageBox.Show("Hocanın Ders Saati Doludur, Başka Hoca Seçiniz"); }
            }
            else { MessageBox.Show("Lütfen Boş Bırakmayınız"); }
            frm_ana.baglanti.Close();
        }

        private void CmbBx_Kurs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //kurs ne ise o branşta olan öğretmenler listelencek öğretmenlere
            Frm_AnaMenu frmogrtgncl = new Frm_AnaMenu();
            frmogrtgncl.baglanti.Open();
            SqlDataAdapter kmtogrtsnf = new SqlDataAdapter("Select *from ogretmenler where Brans=@p1", frmogrtgncl.baglanti);
            kmtogrtsnf.SelectCommand.Parameters.AddWithValue("@p1", CmbBx_Kurs.Text);
            DataTable ogretmentablo = new DataTable();
            kmtogrtsnf.Fill(ogretmentablo);
            CmbBx_Ogrtler.DataSource = ogretmentablo;
            CmbBx_Ogrtler.DisplayMember = "Ogretmen";
            CmbBx_Ogrtler.ValueMember = "OgrtID";
            frmogrtgncl.baglanti.Close();
        }
    }
}
