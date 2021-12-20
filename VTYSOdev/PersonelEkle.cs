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
    public partial class PersonelEkle : Form
    {
        public PersonelEkle()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        private void PersonelEkle_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from departman", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "departmanad";
            comboBox1.ValueMember = "departmanid";
            comboBox1.DataSource = dt;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand urunEkle = new NpgsqlCommand("insert into personel (kullaniciad,sifre,ad,soyad,departmanid) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
                urunEkle.Parameters.AddWithValue("@p1", textBox3.Text);
                urunEkle.Parameters.AddWithValue("@p2", textBox4.Text);
                urunEkle.Parameters.AddWithValue("@p3", textBox1.Text);
                urunEkle.Parameters.AddWithValue("@p4", textBox2.Text);
                urunEkle.Parameters.AddWithValue("@p5", int.Parse(comboBox1.SelectedValue.ToString()));
                urunEkle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show(textBox1.Text + " " + textBox2.Text + " isimli personel veri tabanına başarıyla kaydedildi", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Geçersiz veri girişi yaptınız\nHata Kodu:\n" + ex, "Hata !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }
    }
}
