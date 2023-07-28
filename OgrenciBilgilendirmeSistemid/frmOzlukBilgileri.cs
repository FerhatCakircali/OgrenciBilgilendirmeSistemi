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
    public partial class frmOzlukBilgileri : Form
    {
        public frmOzlukBilgileri()
        {
            InitializeComponent();
        }

        SqlConnection con, con2;
        SqlCommand cmd, cmd2;
        SqlDataReader dr, dr2;

        public string ogrNo;
        private void OgrenciBilgiGetir()
        {
            OgrenciForm ogrenciForm = new OgrenciForm();
            ogrNo = OgrenciForm.ogrenciData;

            string ogrenciSorgu = "SELECT * FROM OgrenciBilgi where OgrenciNo=@OgrenciNo";
            con = new SqlConnection("Data Source=DESKTOP-QNH4MN6;Initial Catalog=OBS_DB;Integrated Security=True");
            cmd = new SqlCommand(ogrenciSorgu, con);
            cmd.Parameters.AddWithValue("@OgrenciNo", ogrNo);
            con.Open();
            dr = cmd.ExecuteReader();
            string adres, il, ilce, pkodu, telno;
            if (dr.Read())
            {

                adres = dr["Adres"].ToString().ToUpper();
                il = dr["Sehir"].ToString().ToUpper();
                ilce = dr["Ilce"].ToString().ToUpper();
                pkodu = dr["PostaKod"].ToString();
                telno = dr["TelefonNo"].ToString();

                txtAdres.Text = adres;
                txtSehir.Text = il;
                txtIlce.Text = ilce;
                txtPkodu.Text = pkodu;
                txtTelefon.Text = telno;
            }
            con.Close();
        }
        private void BankaBilgiLoad()
        {
            string bankaSorgu = "SELECT * FROM OgrenciBankaBilgi where OgrenciNo=@OgrenciNo";
            con2 = new SqlConnection("Data Source=DESKTOP-QNH4MN6;Initial Catalog=OBS_DB;Integrated Security=True");
            cmd2 = new SqlCommand(bankaSorgu, con2);
            cmd2.Parameters.AddWithValue("@OgrenciNo", ogrNo);
            con2.Open();
            dr2 = cmd2.ExecuteReader();

            string bankaAd, subeAd, subeKod, hesapNo, iban;
            if (dr2.Read())
            {
                bankaAd = dr2["BankaAd"].ToString();
                subeAd = dr2["SubeAd"].ToString();
                subeKod = dr2["SubeKod"].ToString();
                hesapNo = dr2["HesapNo"].ToString();
                iban = dr2["IBAN"].ToString();

                txtBankaAd.Text = bankaAd;
                txtSubeAd.Text = subeAd;
                txtSubeKod.Text = subeKod;
                txtHesapNo.Text = hesapNo;
                txtIBAN.Text = iban;

            }
            con2.Close();
        }

        private void frmOzlukBilgileri_Load(object sender, EventArgs e)
        {
            OgrenciBilgiGetir();
            BankaBilgiLoad();
        }

        string connectionString = "Data Source=DESKTOP-QNH4MN6;Initial Catalog=OBS_DB;Integrated Security=True";
        private void UpdateStudentData(string adres, string sehir, string ilce, string postaKod, string telefonNo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "UPDATE OgrenciBilgi SET Adres = @Adres, Sehir = @Sehir, Ilce = @Ilce, PostaKod = @PostaKod, TelefonNo = @TelefonNo WHERE OgrenciNo = @OgrenciNo";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@Adres", adres);
                    command.Parameters.AddWithValue("@Sehir", sehir);
                    command.Parameters.AddWithValue("@Ilce", ilce);
                    command.Parameters.AddWithValue("@PostaKod", postaKod);
                    command.Parameters.AddWithValue("@TelefonNo", telefonNo);
                    command.Parameters.AddWithValue("@OgrenciNo", ogrNo);

                    connection.Open();
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                connection.Close();
            }
        }
        private void UpdateBankData(string bankAd, string subeAd, string subeKod, string hesapNo, string iban)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "UPDATE OgrenciBankaBilgi SET BankaAd = @BankaAd, SubeAd = @SubeAd, SubeKod = @SubeKod, HesapNo = @HesapNo, IBAN = @IBAN WHERE OgrenciNo = @OgrenciNo";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@BankaAd", bankAd);
                    command.Parameters.AddWithValue("@SubeAd", subeAd);
                    command.Parameters.AddWithValue("@SubeKod", subeKod);
                    command.Parameters.AddWithValue("@HesapNo", hesapNo);
                    command.Parameters.AddWithValue("@IBAN", iban);
                    command.Parameters.AddWithValue("@OgrenciNo", ogrNo);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Bilgiler güncellendi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                connection.Close();
            }

        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string adres = txtAdres.Text;
            string sehir = txtSehir.Text;
            string ilce = txtIlce.Text;
            string postaKod = txtPkodu.Text;
            string telefonNo = txtTelefon.Text;

            UpdateStudentData(adres, sehir, ilce, postaKod, telefonNo);

            string bankaAd = txtBankaAd.Text;
            string subeAd = txtSubeAd.Text;
            string subeKod = txtSubeKod.Text;
            string hesapNo = txtHesapNo.Text;
            string iban = txtIBAN.Text;

            UpdateBankData(bankaAd, subeAd, subeKod, hesapNo, iban);

            OgrenciBilgiGetir();
            BankaBilgiLoad();
        }
    }
}
