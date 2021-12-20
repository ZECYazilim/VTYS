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
    public partial class KategoriForm : Form
    {
        public KategoriForm()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        void tabloyuGuncelle()
        {
            string sorgu = "select * from kategoriler where kategoridurum=true";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void BtnListele_Click(object sender, EventArgs e)
        {
            tabloyuGuncelle();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into kategoriler (kategoriid,kategoriad) values (@c1,@c2)", baglanti);
            komut1.Parameters.AddWithValue("@c1", Convert.ToInt16(TxtKategoriID.Text));
            komut1.Parameters.AddWithValue("@c2", TxtKategoriAd.Text);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(TxtKategoriAd.Text + " kategorisi başarıyla eklendi", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tabloyuGuncelle();
        }

        private void KategoriForm_Load(object sender, EventArgs e)
        {
            tabloyuGuncelle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand kategoriGuncelle = new NpgsqlCommand("update kategoriler set kategoriad=@p1 where kategoriid=@p2",baglanti);
            kategoriGuncelle.Parameters.AddWithValue("@p1", TxtKategoriAd.Text);
            kategoriGuncelle.Parameters.AddWithValue("@p2", int.Parse(TxtKategoriID.Text));
            kategoriGuncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(TxtKategoriAd.Text + " isimli kategori başarıyla güncellendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tabloyuGuncelle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand kategoriSil = new NpgsqlCommand("update kategoriler set kategoridurum=@p1 where kategoriid=@p2", baglanti);
            kategoriSil.Parameters.AddWithValue("@p1", false);
            kategoriSil.Parameters.AddWithValue("@p2", int.Parse(TxtKategoriID.Text));
            kategoriSil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("İlgili kategori pasif hale getirilmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            tabloyuGuncelle();
        }
    }
}
