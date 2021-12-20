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
    public partial class SmsGonder : Form
    {
        public SmsGonder()
        {
            InitializeComponent();
        }
        public int gonderenId;
        public int providerId;
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");

        private void SmsGonder_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand oku = new NpgsqlCommand("select smsproviderid from carisms where cariid=@p1", baglanti);
            oku.Parameters.AddWithValue("@p1", gonderenId);
            NpgsqlDataReader getProviderID = oku.ExecuteReader();
            if (getProviderID.Read())
            {
                providerId = int.Parse(getProviderID["smsproviderid"].ToString());
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand sendSms = new NpgsqlCommand("insert into gonderilensms (gonderenid,alicitel,icerik,providerid) values (@p1,@p2,@p3,@p4)", baglanti);
                sendSms.Parameters.AddWithValue("@p1", gonderenId);
                sendSms.Parameters.AddWithValue("@p2", maskedTextBox1.Text);
                sendSms.Parameters.AddWithValue("@p3", richTextBox1.Text);
                sendSms.Parameters.AddWithValue("@p4", providerId);
                sendSms.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show(maskedTextBox1.Text + " numaralı şahısa sms iletildi.", "SMS Gönderildi..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                richTextBox1.Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Bilinmeyen Bir hata oluştu.\nHata Kodu :\n" + ex, "An error ocured !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
