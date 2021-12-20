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
    public partial class Departman : Form
    {
        public Departman()
        {
            InitializeComponent();
        }
        private int departmanID = 0;
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        void tabloyuGuncelle()
        {
            string sorgu = "select * from departman";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void Departman_Load(object sender, EventArgs e)
        {
            tabloyuGuncelle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into departman (departmanad,durum) values (@c1,@c2)", baglanti);
            komut1.Parameters.AddWithValue("@c1", textBox1.Text);
            komut1.Parameters.AddWithValue("@c2", true);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(textBox1.Text + " departmanı başarıyla eklendi", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tabloyuGuncelle();
            textBox1.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seciliAlan = dataGridView1.SelectedCells[0].RowIndex;
            departmanID = int.Parse(dataGridView1.Rows[seciliAlan].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.Rows[seciliAlan].Cells[1].Value.ToString();
            string cariDurum = dataGridView1.Rows[seciliAlan].Cells[2].Value.ToString();
            if (cariDurum == "True")
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                bool aktifController = (radioButton1.Checked == true) ? true : false;
                baglanti.Open();
                NpgsqlCommand departmanGuncelle = new NpgsqlCommand("update departman set departmanad=@p1,durum=@p2 where departmanid=@p3", baglanti);
                departmanGuncelle.Parameters.AddWithValue("@p1", textBox1.Text);
                departmanGuncelle.Parameters.AddWithValue("@p2", aktifController);
                departmanGuncelle.Parameters.AddWithValue("@p3", departmanID);
                departmanGuncelle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show(textBox1.Text + " isimli departman başarıyla güncellendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabloyuGuncelle();
            }
            catch (Exception)
            {
                MessageBox.Show("Geçersiz işlem yaptınız.", "Hata !", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand departmanGuncelle = new NpgsqlCommand("update departman set durum=false where departmanid=@p1", baglanti);
                departmanGuncelle.Parameters.AddWithValue("@p1", departmanID);
                departmanGuncelle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show(textBox1.Text + " isimli departman başarıyla pasif hale getirildi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabloyuGuncelle();
            }
            catch (Exception)
            {
                MessageBox.Show("Geçersiz işlem yaptınız.", "Hata !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
