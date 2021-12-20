﻿using Npgsql;
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
    public partial class CTamamlananSiparisler : Form
    {
        public int id;
        public CTamamlananSiparisler()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dbOdev;user ID=postgres; password=1234");
        void tabloyuGuncelle()
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select satisid,urunad,personel,satistarih from satislistesi where cariid=@id", baglanti);
            da.SelectCommand.Parameters.AddWithValue("@id", id);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void CTamamlananSiparisler_Load(object sender, EventArgs e)
        {
            tabloyuGuncelle();
        }
    }
}
