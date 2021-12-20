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
    public partial class CariMain : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");

        public CariMain()
        {
            InitializeComponent();
        }
        public int girisYapanId;
        public bool gelenMesajVarMi=false;
        public int mesajId=0;
        void gelenMesaj()
        {
            baglanti.Open();
            int okunmayanMesaj = 0;
            NpgsqlCommand mesajKontrol = new NpgsqlCommand("select count(*) from personelmesaj AS okunmamismesaj where cariid=@p1 and readcontrol=false", baglanti);
            mesajKontrol.Parameters.AddWithValue("@p1", girisYapanId);
            NpgsqlDataReader getMessageSum = mesajKontrol.ExecuteReader();
            if (getMessageSum.Read())
            {
                okunmayanMesaj = int.Parse(getMessageSum["count"].ToString());
            }
            baglanti.Close();
            if (okunmayanMesaj > 0)
            {
                gelenMesajVarMi = true;
                MessageBox.Show(okunmayanMesaj.ToString() + " yeni okunmamış mesajınız var.", "Bildirim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LblMesaj.Text = okunmayanMesaj.ToString() + " yeni okunmamış mesajınız var !";
                LblMesaj.Visible = true;
                linkLabel1.Visible = true;

            }
        }
        int mesajNo()
        {
            baglanti.Open();
            NpgsqlCommand getNumber = new NpgsqlCommand("select mesajno from personelmesaj where cariid=@p1 and readcontrol=false", baglanti);
            getNumber.Parameters.AddWithValue("@p1", girisYapanId);
            NpgsqlDataReader da = getNumber.ExecuteReader();
            if (da.Read())
            {
                mesajId = int.Parse(da["mesajno"].ToString());
            }
            baglanti.Close();
            return mesajId;
        }
        private void CariMain_Load(object sender, EventArgs e)
        {
            string isim;
            baglanti.Open();
            NpgsqlCommand cariGetir = new NpgsqlCommand("select * from cariler where cariid=@p1",baglanti);
            cariGetir.Parameters.AddWithValue("@p1", girisYapanId);
            NpgsqlDataReader kontrol = cariGetir.ExecuteReader();
            if (kontrol.Read())
            {
                isim = kontrol["cariad"].ToString();
                isim += " " + kontrol["carisoyad"].ToString();
                label2.Text = isim;
                label3.Text = kontrol["bakiye"].ToString() + " ₺";
            }
            else
            {
                MessageBox.Show("Bir hata oluştu");
            }
            baglanti.Close();
            gelenMesaj();
            baglanti.Open();
            NpgsqlCommand sms_kalan = new NpgsqlCommand("select kalan_hak_sms("+girisYapanId+")", baglanti);
            NpgsqlDataReader get_kalan = sms_kalan.ExecuteReader();
            if (get_kalan.Read())
            {
                label4.Text = get_kalan["kalan_hak_sms"].ToString()+ " SMS";
            }
            baglanti.Close();
            baglanti.Open();
            NpgsqlCommand last_seen = new NpgsqlCommand("CALL cari_son_giris(" + girisYapanId + ",CURRENT_DATE)", baglanti);
            last_seen.ExecuteNonQuery();
            baglanti.Close();

        }


        private void CariMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void tamamlananSiparişlerimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CTamamlananSiparisler goruntule = new CTamamlananSiparisler();
            goruntule.id = girisYapanId;
            goruntule.Show();
        }

        private void hesabımıPasifHaleGetirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CariDeaktivasyon deaktivasyon = new CariDeaktivasyon();
            deaktivasyon.id = girisYapanId;
            deaktivasyon.Show();
        }

        private void bilgilerimiGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CariSelfGuncelleme guncelle = new CariSelfGuncelleme();
            guncelle.id = girisYapanId;
            guncelle.Show();
        }

        private void yeniMesajTalebiOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CariMesajGonder sendMessage = new CariMesajGonder();
            sendMessage.id = girisYapanId;
            sendMessage.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MesajGoruntule goruntule = new MesajGoruntule();
            goruntule.id = girisYapanId;
            goruntule.mesajNo = mesajNo();
            goruntule.Show();
        }

        private void yeniDestekTalebiOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DestekTalebi destekTalebi = new DestekTalebi();
            destekTalebi.girisYapanId = girisYapanId;
            destekTalebi.Show();
        }

        private void sMSGönderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SmsGonder smsGonder = new SmsGonder();
            smsGonder.gonderenId = girisYapanId;
            smsGonder.Show();
        }
    }
}
