using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMLProjekat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // SqlConnection veza = new SqlConnection("Data Source=DESKTOP-6LPEK0P\\SQLEXPRESS;Initial Catalog=ednevnik2022;Integrated Security=true");
            SqlConnection veza = new SqlConnection("Server=DESKTOP-6LPEK0P\\SQLEXPRESS;Integrated security=SSPI;database=master");
            string naredba = "CREATE DATABASE Xmlproba";
            SqlCommand komanda = new SqlCommand(naredba, veza);
            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            veza = new SqlConnection("Data Source=DESKTOP-6LPEK0P\\SQLEXPRESS;Initial Catalog=Xmlproba;Integrated Security=true");
            naredba = "CREATE TABLE Adresar(id INT IDENTITY, ime NVARCHAR(20), prezime NVARCHAR(20), telefon VARCHAR(20))";
            komanda = new SqlCommand(naredba, veza);
            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection("Data Source=DESKTOP-6LPEK0P\\SQLEXPRESS;Initial Catalog=Xmlproba;Integrated Security=true");
            DataTable adresar = new DataTable("Adrese");
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Adresar", veza);
            da.Fill(adresar);
            // StreamWriter pisi = new StreamWriter("nosim_adrese.xml");
            adresar.WriteXml("nosim_adrese.xml", XmlWriteMode.WriteSchema);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            DataSet ds = new DataSet();
            ds.ReadXml("nosim_adrese.xml");
            DataTable adrese = ds.Tables[0];
            */
            DataTable adrese = new DataTable();
            adrese.ReadXml("nosim_adrese.xml");

            MessageBox.Show("redova=" + adrese.Rows.Count);
            SqlConnection veza = new SqlConnection("Data Source=DESKTOP-6LPEK0P\\SQLEXPRESS;Initial Catalog=Xmlproba;Integrated Security=true");
            veza.Open();
            for (int i = 0; i < adrese.Rows.Count; i++)
            {
                string naredba = "INSERT INTO Adresar values('";
                naredba+= adrese.Rows[i]["ime"].ToString();
                naredba+= "', '"+ adrese.Rows[i]["prezime"].ToString();
                naredba+= "','"+ adrese.Rows[i]["telefon"].ToString();
                naredba+= "')";
                SqlCommand komanda = new SqlCommand(naredba, veza);
                komanda.ExecuteNonQuery();
            }
            veza.Close();
        }
    }
}
