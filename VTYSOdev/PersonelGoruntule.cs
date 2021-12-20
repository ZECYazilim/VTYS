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
    public partial class PersonelGoruntule : Form
    {
        public PersonelGoruntule()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        void tabloyuGuncelle()
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from personelview", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void PersonelGoruntule_Load(object sender, EventArgs e)
        {
            tabloyuGuncelle();
        }
    }
}
