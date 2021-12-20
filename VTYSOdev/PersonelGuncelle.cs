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
    public partial class PersonelGuncelle : Form
    {
        public PersonelGuncelle()
        {
            InitializeComponent();
        }
        private string id = "";
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        void tabloyuGuncelle()
        {
            string sorgu = "select * from personel";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void PersonelGuncelle_Load(object sender, EventArgs e)
        {
            tabloyuGuncelle();
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from departman", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbDepartman.DisplayMember = "departmanad";
            CmbDepartman.ValueMember = "departmanid";
            CmbDepartman.DataSource = dt;
            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seciliAlan = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[seciliAlan].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.Rows[seciliAlan].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[seciliAlan].Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.Rows[seciliAlan].Cells[1].Value.ToString();
            CmbDepartman.SelectedItem = dataGridView1.Rows[seciliAlan].Cells[4].Value.ToString();
            id = dataGridView1.Rows[seciliAlan].Cells[5].Value.ToString();
            string personelDurum = dataGridView1.Rows[seciliAlan].Cells[6].Value.ToString();
            if (personelDurum == "True")
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Seçili Personel Bilgileri Güncellensin mi ?", "Onay İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bool aktifController = (radioButton1.Checked == true) ? true : false;
                    baglanti.Open();
                    NpgsqlCommand cariGuncelle = new NpgsqlCommand("update personel set kullaniciad=@p1,sifre=@p2,ad=@p3,soyad=@p4,durum=@p5,departmanid=@p6 where personelid=@p7", baglanti);
                    cariGuncelle.Parameters.AddWithValue("@p1", textBox3.Text);
                    cariGuncelle.Parameters.AddWithValue("@p2", textBox4.Text);
                    cariGuncelle.Parameters.AddWithValue("@p3", textBox1.Text);
                    cariGuncelle.Parameters.AddWithValue("@p4", textBox2.Text);
                    cariGuncelle.Parameters.AddWithValue("@p5", aktifController);
                    cariGuncelle.Parameters.AddWithValue("@p6", int.Parse(CmbDepartman.SelectedValue.ToString()));
                    cariGuncelle.Parameters.AddWithValue("@p7", int.Parse(id));
                    cariGuncelle.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show(textBox1.Text+" "+textBox2.Text+" personelinin bilgileri başarıyla güncellendi.", "İşlem Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabloyuGuncelle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Geçersiz İşlem yaptınız,Lütfen geçerli bir personel seçiniz.\nHata Kodu :\n"+ex, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);        
                }

            }
        }
    }
}
