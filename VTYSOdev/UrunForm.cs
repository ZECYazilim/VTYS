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
    public partial class UrunForm : Form
    {
        public UrunForm()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        void tabloyuGuncelle()
        {
            string sorgu = "select * from urunler ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void BtnListele_Click(object sender, EventArgs e)
        {
            tabloyuGuncelle();
        }

        private void UrunForm_Load(object sender, EventArgs e)
        {
            tabloyuGuncelle();
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from kategoriler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbKategori.DisplayMember = "kategoriad";
            CmbKategori.ValueMember = "kategoriid";
            CmbKategori.DataSource = dt;
            baglanti.Close();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand urunEkle = new NpgsqlCommand("insert into urunler (urunid,urunad,stok,alisfiyat,satisfiyat,gorsel,kategori) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);
            urunEkle.Parameters.AddWithValue("@p1", Convert.ToInt16(TxtUrunId.Text));
            urunEkle.Parameters.AddWithValue("@p2", TxtUrunAd.Text);
            urunEkle.Parameters.AddWithValue("@p3", Convert.ToInt16(NumericStok.Value.ToString()));
            urunEkle.Parameters.AddWithValue("@p4", double.Parse(TxtAlis.Text));
            urunEkle.Parameters.AddWithValue("@p5", double.Parse(TxtSatis.Text));
            urunEkle.Parameters.AddWithValue("@p6", TxtGorsel.Text);
            urunEkle.Parameters.AddWithValue("@p7",int.Parse(CmbKategori.SelectedValue.ToString()));
            urunEkle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(TxtUrunAd.Text + " ürünü veri tabanına başarıyla kaydedildi", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tabloyuGuncelle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand urunSil = new NpgsqlCommand("delete from urunler where urunid=@p1", baglanti);
            urunSil.Parameters.AddWithValue("@p1", int.Parse(TxtUrunId.Text));
            urunSil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("İlgili ürün başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            tabloyuGuncelle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand urunGuncelle = new NpgsqlCommand("update urunler set urunad=@p1,stok=@p2,alisfiyat=@p3,satisfiyat=@p4,kategori=@p5 where urunid=@p6", baglanti);
            urunGuncelle.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
            urunGuncelle.Parameters.AddWithValue("@p2", Convert.ToInt16(NumericStok.Value.ToString()));
            urunGuncelle.Parameters.AddWithValue("@p3", double.Parse(TxtAlis.Text));
            urunGuncelle.Parameters.AddWithValue("@p4", double.Parse(TxtSatis.Text));
            urunGuncelle.Parameters.AddWithValue("@p5", int.Parse(CmbKategori.SelectedValue.ToString()));
            urunGuncelle.Parameters.AddWithValue("@p6", int.Parse(TxtUrunId.Text));
            urunGuncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(TxtUrunAd.Text + " ürününün bilgileri başarıyla güncellendi", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            tabloyuGuncelle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand goruntule = new NpgsqlCommand("Select * from urunlistesi", baglanti);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(goruntule);
            DataSet dt = new DataSet();
            da.Fill(dt);
            dataGridView1.DataSource = dt.Tables[0];
            baglanti.Close();
        }
    }
}
