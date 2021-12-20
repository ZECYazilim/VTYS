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
    public partial class MesajGoruntule : Form
    {
        public MesajGoruntule()
        {
            InitializeComponent();
        }

        public int id;
        public int mesajNo;
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");

        void mesajiGetir()
        {
            baglanti.Open();
            NpgsqlCommand mesajAl = new NpgsqlCommand("select * from gelenmesaj where mesajno=@p1", baglanti);
            mesajAl.Parameters.AddWithValue("@p1", mesajNo);
            NpgsqlDataReader da = mesajAl.ExecuteReader();
            if (da.Read())
            {
                richTextBox1.Text = da["icerik"].ToString();
                LblYanitlayan.Text = da["ad"].ToString();
                LblYanitlayan.Text += " " + da["soyad"].ToString();
            }

            baglanti.Close();
        }
        private void MesajGoruntule_Load(object sender, EventArgs e)
        {
            mesajiGetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CariMesajGonder sendMessage = new CariMesajGonder();
            sendMessage.id = id;
            sendMessage.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            NpgsqlCommand changeReadSt = new NpgsqlCommand("update personelmesaj set readcontrol=true where mesajno=@p1", baglanti);
            changeReadSt.Parameters.AddWithValue("@p1", mesajNo);
            changeReadSt.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Mesaj Okundu olarak işaretlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
