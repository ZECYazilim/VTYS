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
    public partial class PersonelSil : Form
    {
        public PersonelSil()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        void tabloyuGetir()
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select personelid,ad,soyad from personel where durum=true", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void PersonelSil_Load(object sender, EventArgs e)
        {
            tabloyuGetir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(textBox2.Text + " " + textBox3.Text + " isimli personel silinsin mi ?", "Onay İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    baglanti.Open();
                    NpgsqlCommand personelSil = new NpgsqlCommand("update personel set durum=@p1 where personelid=@p2", baglanti);
                    personelSil.Parameters.AddWithValue("@p1",false);
                    personelSil.Parameters.AddWithValue("@p2", int.Parse(textBox1.Text));
                    personelSil.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show(textBox2.Text + " " + textBox3.Text + " isimli personel başarıyla silindi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabloyuGetir();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hatalı işlem yaptınız,lütfen yeniden deneyin.\nHata Kodu:\n" + ex, "Hata !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seciliAlan = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[seciliAlan].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[seciliAlan].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[seciliAlan].Cells[2].Value.ToString();
        }
    }
}
