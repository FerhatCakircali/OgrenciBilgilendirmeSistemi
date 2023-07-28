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
    public partial class frmProgramDersleri : Form
    {
        public frmProgramDersleri()
        {
            InitializeComponent();
        }

        string connectionString = "Data Source=DESKTOP-QNH4MN6;Initial Catalog=OBS_DB;Integrated Security=True";
        private void ShowProgramDersleri()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT Sb, DersKodu, DersAdi, Sinif, U, L, T, Z, Krd, AKTS " +
                        "FROM ProgramDersleri";
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
        private void frmProgramDersleri_Load(object sender, EventArgs e)
        {
            ShowProgramDersleri();
        }
    }
}
