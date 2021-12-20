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
    public partial class YeniSatis : Form
    {
        public YeniSatis()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        void getCaris()
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from cariler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbCari.DisplayMember = "cariad";
            CmbCari.ValueMember = "cariid";
            CmbCari.DataSource = dt;
            baglanti.Close();
        }
        void getUruns()
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from urunler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbUrun.DisplayMember = "urunad";
            CmbUrun.ValueMember = "urunid";
            CmbUrun.DataSource = dt;
            baglanti.Close();
        }
        void getPersonels()
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from personel where durum=true", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbPersonel.DisplayMember = "ad";
            CmbPersonel.ValueMember = "personelid";
            CmbPersonel.DataSource = dt;
            baglanti.Close();
        }

        private void YeniSatis_Load(object sender, EventArgs e)
        {
            getCaris();
            getPersonels();
            getUruns();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime suan = DateTime.Today;
            baglanti.Open();
            NpgsqlCommand satisYap = new NpgsqlCommand("insert into satislar (satistarih,cariid,urunid,personelid,toplam_tutar) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
            satisYap.Parameters.AddWithValue("@p1", suan);
            satisYap.Parameters.AddWithValue("@p2", int.Parse(CmbCari.SelectedValue.ToString()));
            satisYap.Parameters.AddWithValue("@p3", int.Parse(CmbUrun.SelectedValue.ToString()));
            satisYap.Parameters.AddWithValue("@p4", int.Parse(CmbPersonel.SelectedValue.ToString()));
            satisYap.Parameters.AddWithValue("@p5", Convert.ToInt16(textBox1.Text));
            satisYap.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(CmbCari.Text + " kişisine seçilen ürünün satışı gerçekleşti.", "İşlem Tamamlandı.", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
