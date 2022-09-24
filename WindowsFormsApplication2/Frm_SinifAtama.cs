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
    public partial class Frm_SinifAtama : Form
    {
        public Frm_SinifAtama()
        {
            InitializeComponent();
        }

        public string id;
        Frm_AnaMenu frm_ana;

        //private void button2_Click(object sender, EventArgs e) //Sınıftan çıkar
        //{
        //    frm_ana = new Frm_AnaMenu();
        //    frm_ana.baglanti.Open();
        //    SqlCommand kmtsnfsiltalep = new SqlCommand("insert into sinifsilmetalebi (OgrID,SnfSilmeDurumu) values (@p1,1)", frm_ana.baglanti);
        //    kmtsnfsiltalep.Parameters.AddWithValue("@p1", id);
        //    if (kmtsnfsiltalep.ExecuteNonQuery() > 0)
        //    {
        //        MessageBox.Show("Sınıf Silme Talebi Yöneticiye Gönderildi");
        //        this.Close();
        //    }
        //    else { MessageBox.Show("İşlem Başarısız"); }
        //    frm_ana.baglanti.Close();
        //}

        private void button1_Click(object sender, EventArgs e) //Kaydet
        {
            //frm_ana = new Frm_AnaMenu();
            frm_ana = (Frm_AnaMenu)Application.OpenForms["Frm_AnaMenu"];

            frm_ana.baglanti.Open();
            SqlCommand kmtsnfkydet = new SqlCommand("Update Kayitlar set sinif=@p1 where id=@p2", frm_ana.baglanti);
            kmtsnfkydet.Parameters.AddWithValue("@p1", Lbl_Snf.Text);
            kmtsnfkydet.Parameters.AddWithValue("@p2", id);
            if (kmtsnfkydet.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("İşlem Başarılı");
                frm_ana.baglanti.Close();
                frm_ana.Btn_OgrGetir_Click(new object(), new EventArgs());//  Düğmeye basma tuşu
                Close();
            }
            else { MessageBox.Show("İşlem Başarısız"); }
            frm_ana.baglanti.Close();

        }

        private void Frm_SinifAtama_Load(object sender, EventArgs e)
        {

        }
    }
}
