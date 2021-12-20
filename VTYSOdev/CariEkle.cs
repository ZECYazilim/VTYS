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
    public partial class CariEkle : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        public CariEkle()
        {
            InitializeComponent();
        }
        public string ilceid = "";
        public string sehirid = "";
        public string koyid = "";
        public string mahalleid = "";
        private void CariEkle_Load(object sender, EventArgs e)
        {
            BtnPasif.Checked = true;
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from sehirler order by sehirid", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbSehir.DisplayMember = "sehirad";
            CmbSehir.ValueMember = "sehirid";
            CmbSehir.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                bool aktifController = (BtnAktif.Checked == true) ? true : false;
                NpgsqlCommand yeniCari = new NpgsqlCommand("insert into cariler (cariad,carisoyad,carisehirid,vergidairesi,kullaniciad,sifre,carimail,caridurum,caritelefon,ilceid,koyid,mahalleid) values(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)", baglanti);
                yeniCari.Parameters.AddWithValue("@p1", TxtCariAd.Text);
                yeniCari.Parameters.AddWithValue("@p2", TxtCariSoyad.Text);
                yeniCari.Parameters.AddWithValue("@p3", int.Parse(sehirid));
                yeniCari.Parameters.AddWithValue("@p4", TxtVergiDairesi.Text);
                yeniCari.Parameters.AddWithValue("@p5", TxtKullaniciAd.Text);
                yeniCari.Parameters.AddWithValue("@p6", TxtSifre.Text);
                yeniCari.Parameters.AddWithValue("@p7", TxtMail.Text);
                yeniCari.Parameters.AddWithValue("@p8", aktifController);
                yeniCari.Parameters.AddWithValue("@p9", TxtTelNo.Text);
                yeniCari.Parameters.AddWithValue("@p10", int.Parse(ilceid));
                yeniCari.Parameters.AddWithValue("@p11", int.Parse(koyid));
                yeniCari.Parameters.AddWithValue("@p12", int.Parse(mahalleid));
                yeniCari.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show(TxtCariAd.Text + " " + TxtCariSoyad.Text + " isimli cari başarıyla kaydedildi.", "Kayıt işlemi başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "belirtilen hatasından ötürü kayıt başarısızdır.", "Hata !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }          
        }

        private void CmbSehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            CmbIlce.Items.Clear();

                NpgsqlCommand getContent = new NpgsqlCommand("select * from sehirler where sehirad=@p1", baglanti);
                getContent.Parameters.AddWithValue("@p1", CmbSehir.Text);
                NpgsqlDataReader getSehirID = getContent.ExecuteReader();
                if (getSehirID.Read())
                {
                    sehirid = getSehirID["sehirid"].ToString();
                }
            baglanti.Close();
            baglanti.Open();
                NpgsqlCommand ilceleriGetir = new NpgsqlCommand("select * from ilceler where sehirid=@p1", baglanti);
                ilceleriGetir.Parameters.AddWithValue("@p1", int.Parse(sehirid));
                NpgsqlDataReader getIlce = ilceleriGetir.ExecuteReader();
                while (getIlce.Read())
                {
                    CmbIlce.Items.Add(getIlce["ilcead"].ToString());
                }
                baglanti.Close();
            
        }

        private void CmbIlce_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            CmbKoy.Items.Clear();
            
            NpgsqlCommand getContent = new NpgsqlCommand("select * from ilceler where ilcead=@p1", baglanti);
            getContent.Parameters.AddWithValue("@p1", CmbIlce.Text);
            NpgsqlDataReader getIlceId = getContent.ExecuteReader();
            if (getIlceId.Read())
            {
                ilceid = getIlceId["ilceid"].ToString();
            }
            baglanti.Close();
            baglanti.Open();
            NpgsqlCommand koyleriGetir = new NpgsqlCommand("select * from koyler where ilceid=@p1", baglanti);
            koyleriGetir.Parameters.AddWithValue("@p1", int.Parse(ilceid));
            NpgsqlDataReader getIlce = koyleriGetir.ExecuteReader();
            while (getIlce.Read())
            {
                CmbKoy.Items.Add(getIlce["koyad"].ToString());
            }
            baglanti.Close();
        }

        private void CmbKoy_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            CmbMahalle.Items.Clear();
            NpgsqlCommand getContent = new NpgsqlCommand("select * from koyler where koyad=@p1 and ilceid=@p2", baglanti);
            getContent.Parameters.AddWithValue("@p1", CmbKoy.Text);
            getContent.Parameters.AddWithValue("@p2", int.Parse(ilceid));
            NpgsqlDataReader getKoyid = getContent.ExecuteReader();
            if (getKoyid.Read())
            {
                koyid = getKoyid["koyid"].ToString();
            }
            baglanti.Close();
            baglanti.Open();
            NpgsqlCommand mahalleGetir  = new NpgsqlCommand("select * from mahalleler where koyid=@p1", baglanti);
            mahalleGetir.Parameters.AddWithValue("@p1", int.Parse(koyid));
            NpgsqlDataReader getMahalle = mahalleGetir.ExecuteReader();
            while (getMahalle.Read())
            {
                CmbMahalle.Items.Add(getMahalle["mahallead"].ToString());
            }
            baglanti.Close();
        }

        private void CmbMahalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand getContent = new NpgsqlCommand("select * from mahalleler where mahallead=@p1", baglanti);
            getContent.Parameters.AddWithValue("@p1", CmbMahalle.Text);
            NpgsqlDataReader getmahalleId = getContent.ExecuteReader();
            if (getmahalleId.Read())
            {
                mahalleid = getmahalleId["mahalleid"].ToString();
            }
            baglanti.Close();

        }
    }
}
