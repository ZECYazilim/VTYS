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
    public partial class SMSPaket : Form
    {
        public SMSPaket()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");

        private void SMSPaket_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from smsyukle", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbCari.DisplayMember = "cariad";
            CmbCari.ValueMember = "smsproviderid";
            CmbCari.DataSource = dt;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand SmsEkle = new NpgsqlCommand("update carisms set kalansms=@p1 where smsproviderid=@p2", baglanti);
                SmsEkle.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
                SmsEkle.Parameters.AddWithValue("@p2", int.Parse(CmbCari.SelectedValue.ToString()));
                SmsEkle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("İlgili carinin sms tanımla işlemi gerçekleşmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Bilinmeyen bir hata oluştu.\nHata Kodu:\n" + ex, "An error ocured !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }
    }
}
