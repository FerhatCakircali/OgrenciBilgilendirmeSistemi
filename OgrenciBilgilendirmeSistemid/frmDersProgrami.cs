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
    public partial class frmDersProgrami : Form
    {
        public frmDersProgrami()
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
                    string queryPazartesi = "SELECT DersSaati, DersKodu, DersAdi, Derslik," +
                        " OgrElemUnvan + ' ' + OgrElemAd + ' ' + OgrElemSoyad AS OgretimElemani " +
                        "FROM ProgramDersleri INNER JOIN OgretimElemaniBilgi " +
                        "ON ProgramDersleri.OgrElemNo = OgretimElemaniBilgi.OgrElemNo " +
                        "WHERE DersGunu = 'P.tesi'";
                    SqlDataAdapter adapterPazartesi = new SqlDataAdapter(queryPazartesi, connection);
                    DataTable dataTablePazartesi = new DataTable();
                    adapterPazartesi.Fill(dataTablePazartesi);
                    dgvPtesi.DataSource = dataTablePazartesi;

                    string querySali = "SELECT DersSaati, DersKodu, DersAdi, Derslik," +
                        " OgrElemUnvan + ' ' + OgrElemAd + ' ' + OgrElemSoyad AS OgretimElemani " +
                        "FROM ProgramDersleri INNER JOIN OgretimElemaniBilgi " +
                        "ON ProgramDersleri.OgrElemNo = OgretimElemaniBilgi.OgrElemNo " +
                        "WHERE DersGunu = 'Salı'"; 
                    SqlDataAdapter adapterSali = new SqlDataAdapter(querySali, connection);
                    DataTable dataTableSali = new DataTable();
                    adapterSali.Fill(dataTableSali);
                    dgvSali.DataSource = dataTableSali;

                    string queryCarsamba = "SELECT DersSaati, DersKodu, DersAdi, Derslik," +
                        " OgrElemUnvan + ' ' + OgrElemAd + ' ' + OgrElemSoyad AS OgretimElemani " +
                        "FROM ProgramDersleri INNER JOIN OgretimElemaniBilgi " +
                        "ON ProgramDersleri.OgrElemNo = OgretimElemaniBilgi.OgrElemNo " +
                        "WHERE DersGunu = 'Çarşamba'"; 
                    SqlDataAdapter adapterCarsamba = new SqlDataAdapter(queryCarsamba, connection);
                    DataTable dataTableCarsamba = new DataTable();
                    adapterCarsamba.Fill(dataTableCarsamba);
                    dgvCarsamba.DataSource = dataTableCarsamba;

                    string queryPersembe = "SELECT DersSaati, DersKodu, DersAdi, Derslik" +
                        ", OgrElemUnvan + ' ' + OgrElemAd + ' ' + OgrElemSoyad AS OgretimElemani " +
                        "FROM ProgramDersleri INNER JOIN OgretimElemaniBilgi " +
                        "ON ProgramDersleri.OgrElemNo = OgretimElemaniBilgi.OgrElemNo " +
                        "WHERE DersGunu = 'Perşembe'"; 
                    SqlDataAdapter adapterPersembe = new SqlDataAdapter(queryPersembe, connection);
                    DataTable dataTablePersembe = new DataTable();
                    adapterPersembe.Fill(dataTablePersembe);
                    dgvPersembe.DataSource = dataTablePersembe;

                    string queryCuma = "SELECT DersSaati, DersKodu, DersAdi, Derslik," +
                        " OgrElemUnvan + ' ' + OgrElemAd + ' ' + OgrElemSoyad AS OgretimElemani " +
                        "FROM ProgramDersleri INNER JOIN OgretimElemaniBilgi " +
                        "ON ProgramDersleri.OgrElemNo = OgretimElemaniBilgi.OgrElemNo " +
                        "WHERE DersGunu = 'Cuma'";
                    SqlDataAdapter adapterCuma = new SqlDataAdapter(queryCuma, connection);
                    DataTable dataTableCuma = new DataTable();
                    adapterCuma.Fill(dataTableCuma);
                    dgvCuma.DataSource = dataTableCuma;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void frmDersProgrami_Load(object sender, EventArgs e)
        {
            ShowProgramDersleri();
        }
    }
}
