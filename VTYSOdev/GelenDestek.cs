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
    public partial class GelenDestek : Form
    {
        public GelenDestek()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        void guncelle()
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from sorunbildir where durum=false order by sorunno", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbBildirim.DisplayMember = "sorunno";
            CmbBildirim.ValueMember = "sorunno";
            CmbBildirim.DataSource = dt;
        }
        private void GelenDestek_Load(object sender, EventArgs e)
        {
            guncelle();
        }

        private void CmbBildirim_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand verileriGetir = new NpgsqlCommand("select * from sorungoruntule where sorunno=@p1", baglanti);
            verileriGetir.Parameters.AddWithValue("@p1", int.Parse(CmbBildirim.SelectedValue.ToString()));
            NpgsqlDataReader getContent = verileriGetir.ExecuteReader();
            if (getContent.Read())
            {
                textBox1.Text=getContent["sorunadi"].ToString();
                richTextBox1.Text = getContent["icerik"].ToString();
                label2.Text = getContent["cariad"].ToString() + " " + getContent["carisoyad"].ToString();
            }
            baglanti.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand arsivle = new NpgsqlCommand("update sorunbildir set durum=true where sorunno=@p1", baglanti);
            arsivle.Parameters.AddWithValue("@p1", int.Parse(CmbBildirim.SelectedValue.ToString()));
            arsivle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Seçilen talep arşivlendi.", "İşlem Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            guncelle();
        }
    }
}
