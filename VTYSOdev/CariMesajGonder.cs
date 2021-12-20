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
    public partial class CariMesajGonder : Form
    {
        public CariMesajGonder()
        {
            InitializeComponent();
        }
        public int id;
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");

        private void CariMesajGonder_Load(object sender, EventArgs e)
        {
            richTextBox1.MaxLength = 250;
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from personel", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "ad";
            comboBox1.ValueMember = "personelid";
            comboBox1.DataSource = dt;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand mesajGonder = new NpgsqlCommand("insert into mesajlar (cariid,personelid,icerik) values (@p1,@p2,@p3)", baglanti);
                mesajGonder.Parameters.AddWithValue("@p1", id);
                mesajGonder.Parameters.AddWithValue("@p2", int.Parse(comboBox1.SelectedValue.ToString()));
                mesajGonder.Parameters.AddWithValue("@p3", richTextBox1.Text);
                mesajGonder.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show(comboBox1.Text + " personelimize başarıyla mesaj gönderdiniz.\nEn kısa sürede dönüş sağlanacaktır.", "Mesaj Gönderildi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Bilinmeyen bir hata oluştu.\nHata Kodu:\n" + ex, "SQL Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            richTextBox1.Clear();
        }
    }
}
