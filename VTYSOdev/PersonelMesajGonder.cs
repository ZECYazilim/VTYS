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
    public partial class PersonelMesajGonder : Form
    {
        public PersonelMesajGonder()
        {
            InitializeComponent();
        }
        public int id;
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        void mesajiGetir()
        {
            baglanti.Open();
            NpgsqlCommand mesajAl = new NpgsqlCommand("select * from mesajtopersonel where mesajid=@p1", baglanti);
            mesajAl.Parameters.AddWithValue("@p1", int.Parse(comboBox1.SelectedValue.ToString()));
            NpgsqlDataReader da = mesajAl.ExecuteReader();
            if (da.Read())
            {
                richTextBox1.Text = da["icerik"].ToString();
                LblGonderen.Text = da["cariad"].ToString();
            }
             
            baglanti.Close();
        }
        void makeItRead()
        {
            baglanti.Open();
            NpgsqlCommand changeReadSt = new NpgsqlCommand("update mesajlar set readcontrol=true where mesajid=@p1", baglanti);
            changeReadSt.Parameters.AddWithValue("@p1", int.Parse(comboBox1.SelectedValue.ToString()));
            changeReadSt.ExecuteNonQuery();
            baglanti.Close();
        }
        int getCustomerId()
        {
            baglanti.Open();
            int id=0;
            NpgsqlCommand getId = new NpgsqlCommand("select cariid from mesajlar where mesajid=@p1", baglanti);
            getId.Parameters.AddWithValue("@p1", int.Parse(comboBox1.SelectedValue.ToString()));
            NpgsqlDataReader da = getId.ExecuteReader();
            if (da.Read())
            {
                id = int.Parse(da["cariid"].ToString());
            }
            baglanti.Close();
            return id;
        }
        private void PersonelMesajGonder_Load(object sender, EventArgs e)
        {
            richTextBox1.MaxLength = 250;
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from mesajlar where personelid=@p1 and readcontrol=false", baglanti);
            da.SelectCommand.Parameters.AddWithValue("@p1", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "mesajid";
            comboBox1.ValueMember = "mesajid";
            comboBox1.DataSource = dt;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mesajiGetir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int cariNo = getCustomerId();
            try
            {
                baglanti.Open();
                NpgsqlCommand mesajGonder = new NpgsqlCommand("insert into personelmesaj (cariid,personelid,icerik) values (@p1,@p2,@p3)", baglanti);
                mesajGonder.Parameters.AddWithValue("@p1",cariNo);
                mesajGonder.Parameters.AddWithValue("@p2", id);
                mesajGonder.Parameters.AddWithValue("@p3", richTextBox2.Text);
                mesajGonder.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("yanıtınız ilgili cariye gönderildi.", "Mesaj Gönderildi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                makeItRead();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Bilinmeyen bir hata oluştu.\nHata Kodu:\n" + ex, "SQL Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            richTextBox1.Clear();
        }
    }
}
