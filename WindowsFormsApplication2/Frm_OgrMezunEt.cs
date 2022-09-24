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
using System.Data.Sql;

namespace WindowsFormsApplication2
{
    public partial class Frm_OgrMezunEt : Form
    {
        public Frm_OgrMezunEt()
        {
            InitializeComponent();
        }

        public int snfdetayid { get; set; }

        SqlConnection baglanti = new SqlConnection("Data Source = 185.81.154.166, 1433; Network Library = DBMSSOCN; Initial Catalog = kayitlar; User ID = hasan; Password=Hsn.12345");

        private void Btn_MezEt_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LstVw_OgrMezEtList.CheckedItems.Count; i++)
            {
                if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
                SqlCommand kmt = new SqlCommand("update kayitlar set Sinif=NULL, Ogretmen=NULL,Mezun=1 where id=@p1", baglanti);
                kmt.Parameters.AddWithValue("@p1", LstVw_OgrMezEtList.CheckedItems[i].Text);
                kmt.ExecuteNonQuery();
                baglanti.Close();
            }

            if (baglanti.State == ConnectionState.Closed) { baglanti.Open(); }
            SqlCommand ogrsayisi = new SqlCommand("select *from siniflardetay where id=@p1", baglanti);
            Frm_AdminPanel adm = (Frm_AdminPanel)Application.OpenForms["Frm_AdminPanel"];
            ogrsayisi.Parameters.AddWithValue("@p1", snfdetayid);

            SqlDataReader oku = ogrsayisi.ExecuteReader();
            int ogrsay = 0;
            while (oku.Read())
            {
                ogrsay = int.Parse(oku["kayitliogrenci"].ToString());
            }
            oku.Close();
            ogrsay = ogrsay - LstVw_OgrMezEtList.CheckedItems.Count;


            SqlCommand ogrdegis = new SqlCommand("update  siniflardetay set kayitliogrenci=@p2 where id=@p1", baglanti);
            ogrdegis.Parameters.AddWithValue("@p1", snfdetayid);
            ogrdegis.Parameters.AddWithValue("@p2", ogrsay);
            ogrdegis.ExecuteNonQuery();
            MessageBox.Show("Seçili Öğrenciler Mezun Edilmiştir");
            baglanti.Close();


            adm.button3_Click(new object(), new EventArgs());

            Close();
        }

        private void Frm_OgrMezunEt_Load(object sender, EventArgs e)
        {

        }
    }
}
