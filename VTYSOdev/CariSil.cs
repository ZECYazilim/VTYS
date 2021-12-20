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
    public partial class CariSil : Form
    {
        public CariSil()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        void tabloyuGetir()
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select cariid,cariad,carisoyad from cariler", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void CariSil_Load(object sender, EventArgs e)
        {
            tabloyuGetir();   
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seciliAlan = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[seciliAlan].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[seciliAlan].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[seciliAlan].Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show(textBox2.Text+" "+textBox3.Text+ " isimli cari silinsin mi ?", "Onay İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    baglanti.Open();
                    NpgsqlCommand cariPasiflestir = new NpgsqlCommand("update cariler set caridurum=false where cariid=@p2", baglanti);     
                    cariPasiflestir.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));                   
                    cariPasiflestir.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show(textBox2.Text + " " + textBox3.Text + " isimli cari başarıyla pasifleştirildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabloyuGetir();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hatalı işlem yaptınız,lütfen yeniden deneyin.\nHata Kodu:\n" + ex, "Hata !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }
    }
}
