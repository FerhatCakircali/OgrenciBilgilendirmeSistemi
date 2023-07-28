using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OgrenciBilgilendirmeSistemid
{
    public partial class frmSifreDegistir : Form
    {
        public frmSifreDegistir()
        {
            InitializeComponent();
        }
        private string ogrNo;
        string connectionString = "Data Source=DESKTOP-QNH4MN6;Initial Catalog=OBS_DB;Integrated Security=True";

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string eskiSifre = txtEskiSifre.Text;
            string yeniSifre = txtYeniSifre.Text;
            string yeniSifreTekrar = txtYeniSifreTekrar.Text;

            if (yeniSifre == yeniSifreTekrar)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    OgrenciForm ogrenciForm = new OgrenciForm();
                    ogrNo = OgrenciForm.ogrenciData;

                   
                    string query = "UPDATE OgrenciGiris SET Sifre = @YeniSifre WHERE OgrenciNo = @OgrenciNo AND Sifre = @EskiSifre";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@YeniSifre", yeniSifre);
                    command.Parameters.AddWithValue("@OgrenciNo", ogrNo);
                    command.Parameters.AddWithValue("@EskiSifre", eskiSifre);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Şifre değiştirme işlemi başarılı.");
                    }
                    else
                    {
                        MessageBox.Show("Eski şifre yanlış. Şifre değiştirme işlemi başarısız.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Yeni şifreler uyuşmuyor. Lütfen doğru şekilde girin.");
            }
        }
    }
}
