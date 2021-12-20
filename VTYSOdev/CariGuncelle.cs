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
    public partial class CariGuncelle : Form
    {
        public CariGuncelle()
        {
            InitializeComponent();
        }
        private string id = "";
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        void tabloyuGuncelle()
        {
            string sorgu = "select * from cariListesi";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void CariGuncelle_Load(object sender, EventArgs e)
        {
            tabloyuGuncelle();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seciliAlan = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[seciliAlan].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[seciliAlan].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[seciliAlan].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[seciliAlan].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[seciliAlan].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.Rows[seciliAlan].Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.Rows[seciliAlan].Cells[9].Value.ToString();
            id = dataGridView1.Rows[seciliAlan].Cells[8].Value.ToString();
            string cariDurum = dataGridView1.Rows[seciliAlan].Cells[7].Value.ToString();
            if (cariDurum == "True")
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Seçili Cari Bilgileri Güncellensin mi ?", "Onay İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool aktifController = (radioButton1.Checked == true) ? true : false;
                    baglanti.Open();
                    NpgsqlCommand cariGuncelle = new NpgsqlCommand("update cariler set cariad=@p1,carisoyad=@p2,vergidairesi=@p3,kullaniciad=@p4,sifre=@p5,carimail=@p6,caridurum=@p7,caritelefon=@p8 where cariid=@p9", baglanti);
                    cariGuncelle.Parameters.AddWithValue("@p1", textBox1.Text);
                    cariGuncelle.Parameters.AddWithValue("@p2", textBox2.Text);
                    cariGuncelle.Parameters.AddWithValue("@p3", textBox3.Text);
                    cariGuncelle.Parameters.AddWithValue("@p4", textBox4.Text);
                    cariGuncelle.Parameters.AddWithValue("@p5", textBox5.Text);
                    cariGuncelle.Parameters.AddWithValue("@p6", textBox6.Text);
                    cariGuncelle.Parameters.AddWithValue("@p7", aktifController);
                    cariGuncelle.Parameters.AddWithValue("@p8", textBox7.Text);
                    cariGuncelle.Parameters.AddWithValue("@p9", int.Parse(id));
                    cariGuncelle.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Cari Bilgileri başarıyla güncellendi.", "İşlem Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabloyuGuncelle();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Geçersiz İşlem yaptınız,Lütfen geçerli bir cari seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
    }
}
