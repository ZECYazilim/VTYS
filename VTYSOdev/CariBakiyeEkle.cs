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
    public partial class CariBakiyeEkle : Form
    {
        public CariBakiyeEkle()
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
        private void CariBakiyeEkle_Load(object sender, EventArgs e)
        {
            getCaris();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(CmbCari.SelectedValue.ToString());
                int bakiye = int.Parse(textBox1.Text);
                baglanti.Open();
                NpgsqlCommand last_seen = new NpgsqlCommand("CALL bakiye_yukle(" + id + ","+bakiye+")", baglanti);
                last_seen.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Bakiye yükleme işlemi başarıyla tamamlandı.", "Bakiye Yüklendi.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bilinmeyen bir hata oluştu.\nHata Kodu:\n" + ex, "An error ocured.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
