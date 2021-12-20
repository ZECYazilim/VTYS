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
    public partial class CariDeaktivasyon : Form
    {
        public CariDeaktivasyon()
        {
            InitializeComponent();
        }
        public int id;
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                try
                {
                    baglanti.Open();
                    NpgsqlCommand suspendAccount = new NpgsqlCommand("update cariler set caridurum=false where cariid=@p1", baglanti);
                    suspendAccount.Parameters.AddWithValue("@p1", id);
                    suspendAccount.ExecuteNonQuery();
                    MessageBox.Show("Hesabınız Başarıyla deaktif hale getirilmiştir.\nUygulama yeniden başlaycaktır.", "İşlem Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    baglanti.Close();
                    Application.Restart();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bilinmeyen bir hata oluştu.\nHata Kodu:\n" + ex, "Hata !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Onay kutucuğunu işaretlemediğiniz için işlem BAŞARISZDIR.", "Onay Kutusu Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
