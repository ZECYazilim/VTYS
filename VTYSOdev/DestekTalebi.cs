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
    public partial class DestekTalebi : Form
    {
        public DestekTalebi()
        {
            InitializeComponent();
        }
        public int number;
        public int girisYapanId;
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        private void loadCaptchaImage()
        {
            Random r1 = new Random();
            number = r1.Next(1000, 9999);
            var image = new Bitmap(this.picCaptha.Width, this.picCaptha.Height);
            var font = new Font("TimesNewRoman", 15, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(image);
            graphics.DrawString(number.ToString(), font, Brushes.DarkRed, new Point(0, 0));
            picCaptha.Image = image;
        }
        private void DestekTalebi_Load(object sender, EventArgs e)
        {
            loadCaptchaImage();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtDogrulamaKodu.Text == number.ToString())
            {
                baglanti.Open();
                NpgsqlCommand bildirimGonder = new NpgsqlCommand("insert into sorunbildir (sorunadi,icerik,cariid) values (@p1,@p2,@p3)", baglanti);
                bildirimGonder.Parameters.AddWithValue("@p1", cmbSorun.Text);
                bildirimGonder.Parameters.AddWithValue("@p2", richTextBox1.Text);
                bildirimGonder.Parameters.AddWithValue("@p3", girisYapanId);
                bildirimGonder.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Talebiniz teknik ekibe ulaştırılmıştır.\nEn yakın zamanda işleme alınacaktır.", "Gönderildi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                richTextBox1.Clear();
                txtDogrulamaKodu.Clear();
            }
            else
            {
                MessageBox.Show("Doğrulama kodunu kontrol edip tekrar deneyiniz.", "Doğrulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
