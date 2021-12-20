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
    public partial class CariGiris : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");

        public CariGiris()
        {
            InitializeComponent();
        }

        private void CariGiris_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void CariGiris_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = 0;
            bool hesapControl;
            try
            {
                baglanti.Open();
                NpgsqlCommand giris = new NpgsqlCommand("select * from cariler where kullaniciad=@p1 and sifre=@p2", baglanti);
                giris.Parameters.AddWithValue("@p1", textBox1.Text);
                giris.Parameters.AddWithValue("@p2", textBox2.Text);
                NpgsqlDataReader kontrol = giris.ExecuteReader();
                if (kontrol.Read())
                {
                    hesapControl = bool.Parse(kontrol["caridurum"].ToString());
                    id = int.Parse(kontrol["cariid"].ToString());
                    if (hesapControl == true)
                    {
                        CariMain yonlendir = new CariMain();
                        yonlendir.girisYapanId = id;
                        yonlendir.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Hesabınız pasif haldedir,bir hata olduğunu düşünüyorsanız destek talebinde bulunabilirsiniz.", "Account Suspended !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bilinmeyen bir hata oluştu,lütfen kullanıcı adı ve şifrenizi kontrol ediniz.\nHata Kodu:\n" + ex, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglanti.Close();
        }
    }
}
