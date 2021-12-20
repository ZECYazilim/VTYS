using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VTYSOdev
{
    public partial class CariSelfGuncelleme : Form
    {
        public CariSelfGuncelleme()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");

        public int id;
        private void CariSelfGuncelleme_Load(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand getInfo = new NpgsqlCommand("select * from cariler where cariid=@id", baglanti);
                getInfo.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader getBox = getInfo.ExecuteReader();
                if (getBox.Read())
                {
                    TxtCariAd.Text = getBox["cariad"].ToString();
                    TxtCariSoyad.Text = getBox["carisoyad"].ToString();
                    TxtVergiDairesi.Text = getBox["vergidairesi"].ToString();
                    TxtKullaniciAd.Text = getBox["kullaniciad"].ToString();
                    TxtSifre.Text = getBox["sifre"].ToString();
                    TxtMail.Text = getBox["carimail"].ToString();
                    TxtTelNo.Text = getBox["caritelefon"].ToString();
                }
                baglanti.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Bilinmeyen bir hata oluştu.\nHata Kodu:\n" + ex, "SQL Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cari Bigileriniz  Güncellensin mi ?", "Onay İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    baglanti.Open();
                    NpgsqlCommand update = new NpgsqlCommand("update cariler set cariad=@p1,carisoyad=@p2,vergidairesi=@p3,kullaniciad=@p4,sifre=@p5,carimail=@p6,caritelefon=@p7 where cariid=@p8", baglanti);
                    update.Parameters.AddWithValue("@p1", TxtCariAd.Text);
                    update.Parameters.AddWithValue("@p2", TxtCariSoyad.Text);
                    update.Parameters.AddWithValue("@p3", TxtVergiDairesi.Text);
                    update.Parameters.AddWithValue("@p4", TxtKullaniciAd.Text);
                    update.Parameters.AddWithValue("@p5", TxtSifre.Text);
                    update.Parameters.AddWithValue("@p6", TxtMail.Text);
                    update.Parameters.AddWithValue("@p7", TxtTelNo.Text);
                    update.Parameters.AddWithValue("@p8", id);
                    update.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Bilgileriniz başarıyla güncellenmiştir.\nDİKKAT!\nUygulama yeniden başlatılacaktır!", "İşlem Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Restart();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Bilinmeyen bir hata oluştu.\nHata Kodu:\n" + ex, "SQL Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
