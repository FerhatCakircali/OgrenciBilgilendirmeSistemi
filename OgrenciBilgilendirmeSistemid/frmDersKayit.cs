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

namespace OgrenciBilgilendirmeSistemid
{
    public partial class frmDersKayit : Form
    {
        public frmDersKayit()
        {
            InitializeComponent();
        }

        string connectionString = "Data Source=DESKTOP-QNH4MN6;Initial Catalog=OBS_DB;Integrated Security=True";
        private void ShowDersKayit()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT DersKodu, DersAdi, Z_S, T_U, Krd, AKTS FROM AlinanDersler";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    dataGV.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void GetProgramDersleriSUM()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"SELECT SUM(CAST(Krd AS INT)) AS ToplamKredi, 
                    SUM(CAST(AKTS AS INT)) AS ToplamAKTS, SUM(CAST(T_U AS INT)) AS ToplamSaat, 
                    COUNT(*) AS DersSayisi FROM AlinanDersler";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        lblKredi.Text = reader["ToplamKredi"].ToString();
                        lblAKTS.Text = reader["ToplamAKTS"].ToString();
                        lblTU.Text = reader["ToplamSaat"].ToString();
                        lblDS.Text = reader["DersSayisi"].ToString();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void frmDersKayit_Load(object sender, EventArgs e)
        {
            ShowDersKayit();
            GetProgramDersleriSUM();
        }
    }
}
