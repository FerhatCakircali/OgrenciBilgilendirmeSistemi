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
    public partial class frmNotListesi : Form
    {
        public frmNotListesi()
        {
            InitializeComponent();
        }

        private string ogrNo;
        string connectionString = "Data Source=DESKTOP-QNH4MN6;Initial Catalog=OBS_DB;Integrated Security=True";
        private void ShowNotlar()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    OgrenciForm ogrenciForm = new OgrenciForm();
                    ogrNo = OgrenciForm.ogrenciData;

                    string query = @"SELECT N.DersKodu, AD.DersAdi, N.sDurum, N.AraSinav, N.Final, N.Ort, N.HarfNot, N.Durumu 
                             FROM Notlar N INNER JOIN AlinanDersler AD 
                             ON N.DersKodu = AD.DersKodu 
                             WHERE N.OgrenciNo = @OgrenciNo";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@OgrenciNo", ogrNo); 

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

        private void frmNotListesi_Load(object sender, EventArgs e)
        {
            ShowNotlar();
        }
    }
}
