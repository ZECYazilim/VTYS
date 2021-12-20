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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public string girisYapan;
        public int id;
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");

        int getProblemCount()
        {
            int count = 0;
            baglanti.Open();
            NpgsqlCommand mesajKontrol = new NpgsqlCommand("select count(*) from sorunbildir where durum=false", baglanti);
            NpgsqlDataReader getCount = mesajKontrol.ExecuteReader();
            if (getCount.Read())
            {
                count = int.Parse(getCount["count"].ToString()); 
            }
            baglanti.Close();
            return count;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void kategoriİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KategoriForm kategoriForm = new KategoriForm();
            kategoriForm.Show();
        }

        private void ürünİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UrunForm urunForm = new UrunForm();
            urunForm.Show();
        }

        private void cariEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CariEkle cariEkle = new CariEkle();
            cariEkle.Show();
        }

        private void carileriGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CariGoruntule cariGoruntule = new CariGoruntule();
            cariGoruntule.Show();
        }

        private void cariSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CariSil cariSil = new CariSil();
            cariSil.Show();
        }

        private void cariBilgileriniGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CariGuncelle cariGuncelle = new CariGuncelle();
            cariGuncelle.Show();
        }

        private void departmanİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Departman departman = new Departman();
            departman.Show();
        }

        private void personelEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonelEkle personel = new PersonelEkle();
            personel.Show();
        }

        private void personelGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PersonelGoruntule personelGoruntule = new PersonelGoruntule();
            personelGoruntule.Show();
        }

        private void personelGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PersonelGuncelle personelGuncelle = new PersonelGuncelle();
            personelGuncelle.Show();
        }

        private void personelSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PersonelSil personelSil = new PersonelSil();
            personelSil.Show();
        }

        private void yeniSatışYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            YeniSatis yeniSatis = new YeniSatis();
            yeniSatis.Show();
        }

        private void satışlarıGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            SatisGoruntule satisGoruntule = new SatisGoruntule();
            satisGoruntule.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            int okunmayanMesaj = 0;
            label2.Text = girisYapan;
            NpgsqlCommand mesajKontrol = new NpgsqlCommand("select count(*) from mesajlar AS okunmamismesaj where personelid=@p1 and readcontrol=false",baglanti);
            mesajKontrol.Parameters.AddWithValue("@p1", id);
            NpgsqlDataReader getMessageSum = mesajKontrol.ExecuteReader();
            if (getMessageSum.Read())
            {
                okunmayanMesaj = int.Parse(getMessageSum["count"].ToString());
            }
            baglanti.Close();
            if (okunmayanMesaj > 0)
            {
                MessageBox.Show(okunmayanMesaj.ToString() + " yeni okunmamış mesajınız var.", "Bildirim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LblMesaj.Text = okunmayanMesaj.ToString() + " yeni okunmamış mesajınız var !";
                LblMesaj.Visible = true;
            }
            if (getProblemCount() > 0)
            {
                LblSorun.Text = getProblemCount().ToString() + " yeni sorun bildirimi bulundu !\nLütfen En kısa sürede kontrol ediniz.";
                LblSorun.Visible = true;
            }
            baglanti.Open();
            NpgsqlCommand last_seen = new NpgsqlCommand("CALL son_giris_tarihi(" + id + ",CURRENT_DATE)", baglanti);
            last_seen.ExecuteNonQuery();
            baglanti.Close();
        }

        private void mesajlarıGörüntüleCevaplaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PersonelMesajGonder mesajGonder = new PersonelMesajGonder();
            mesajGonder.id = id;
            mesajGonder.Show();
        }

        private void sorunTalepleriniGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GelenDestek goruntule = new GelenDestek();
            goruntule.Show();
        }

        private void cariSMSPaketiTanımlamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            SMSPaket smsTanimla = new SMSPaket();
            smsTanimla.Show();
        }

        private void cariBakiyeEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CariBakiyeEkle bakiyeEkle = new CariBakiyeEkle();
            bakiyeEkle.Show();
        }
    }
}
