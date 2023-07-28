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
    public partial class frmAnasayfa : Form
    {
        public frmAnasayfa()
        {
            InitializeComponent();

        }
        SqlConnection con, con2;
        SqlCommand cmd, cmd2;
        SqlDataReader dr, dr2;

        private string ogrNo;
        private string danismanNo;
        private string connectionString = "Data Source=DESKTOP-QNH4MN6;Initial Catalog=OBS_DB;Integrated Security=True";

        private string DanismanBilgiGetir()
        {
            OgrenciForm ogrenciForm = new OgrenciForm();
            ogrNo = OgrenciForm.ogrenciData;

            string ogrenciSorgu = "SELECT * FROM OgrenciDanismanBilgi where OgrenciNo=@OgrenciNo";
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand(ogrenciSorgu, con);
            cmd.Parameters.AddWithValue("@OgrenciNo", ogrNo);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                danismanNo = dr["OgrElemNo"].ToString();
            }
            con.Close();
            return danismanNo;

        }
        private void DanismanBilgiLoad()
        {
            string danismanSorgu = "SELECT * FROM OgretimElemaniBilgi where OgrElemNo=@OgrElemNo";
            con2 = new SqlConnection(connectionString);
            cmd2 = new SqlCommand(danismanSorgu, con2);
            cmd2.Parameters.AddWithValue("@OgrElemNo", DanismanBilgiGetir());
            con2.Open();
            dr2 = cmd2.ExecuteReader();

            string unvan, ad, soyad;
            if (dr2.Read())
            {
                unvan = dr2["OgrElemUnvan"].ToString();
                ad = dr2["OgrElemAd"].ToString().ToUpper();
                soyad = dr2["OgrElemSoyad"].ToString().ToUpper();
               
                lblDanismanAdSoyad.Text = unvan + " " + ad + " " + soyad;
                
            }
            con2.Close();
        }

        
        private double GetAgno() 
        {
            double agno = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT AVG(CONVERT(float, Ort)) FROM Notlar " +
                        "WHERE OgrenciNo = @OgrenciNo", connection);
                    command.Parameters.AddWithValue("@OgrenciNo", ogrNo);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        double ortalama = Convert.ToDouble(result);
                        agno = ortalama / 25.0;
                    }
                }
               
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
            return agno;

        }

        private void OgrenimBilgileriGetir()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT Fakulte, Bolum, Sinif, KayitTarihi FROM OgrenciBilgi " +
                        "WHERE OgrenciNo = @OgrenciNo", connection);
                    command.Parameters.AddWithValue("@OgrenciNo", ogrNo);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        lblFakulte.Text = reader["Fakulte"].ToString();
                        lblBolum.Text = reader["Bolum"].ToString();
                        lblOgrSinif.Text = reader["Sinif"].ToString()+". SINIF";
                        lblKayitTarih.Text = reader["KayitTarihi"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void frmAnasayfa_Load(object sender, EventArgs e)
        {
            DanismanBilgiLoad();
            OgrenimBilgileriGetir();
            lblAgno.Text = GetAgno().ToString();
        }

    }
}
