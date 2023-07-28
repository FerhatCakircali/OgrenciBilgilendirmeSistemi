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
    public partial class frmFotoGuncelle : Form
    {
        public frmFotoGuncelle()
        {
            InitializeComponent();
        }


        private string ogrNo;

        string connectionString = "Data Source=DESKTOP-QNH4MN6;Initial Catalog=OBS_DB;Integrated Security=True";
        private void ShowEskiResim()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OgrenciForm ogrenciForm = new OgrenciForm();
                    ogrNo = OgrenciForm.ogrenciData;

                    SqlCommand command = new SqlCommand("SELECT OgrenciResim FROM OgrenciBilgi WHERE OgrenciNo = @OgrenciNo", connection);
                    command.Parameters.AddWithValue("@OgrenciNo", ogrNo);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            byte[] eskiResimBytes = (byte[])reader["OgrenciResim"];
                            MemoryStream memoryStream = new MemoryStream(eskiResimBytes);
                            pictureBoxEski.Image = Image.FromStream(memoryStream);
                        }
                        else
                        {
                            pictureBoxEski.Image = null;
                        }
                    }
                    else
                    {
                        pictureBoxEski.Image = null;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        bool aktifMi = false;
        private void UpdateResim(byte[] yeniResimBytes)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("UPDATE OgrenciBilgi SET OgrenciResim = @Resim WHERE OgrenciNo = @OgrenciNo", connection);
                    command.Parameters.AddWithValue("@Resim", yeniResimBytes);
                    command.Parameters.AddWithValue("@OgrenciNo", ogrNo);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Resim güncellendi.");
                        ShowEskiResim();
                        aktifMi = true;
                    }
                    else
                    {
                        MessageBox.Show("Resim güncellenirken hata oluştu.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        byte[] yeniResimBytes;
       
    private void btnResimSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.gif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image yeniResim = Image.FromFile(openFileDialog.FileName);
                    pictureBoxYeni.Image = yeniResim;

                    // Resmi byte dizisine dönüştür
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        yeniResim.Save(memoryStream, yeniResim.RawFormat);
                        yeniResimBytes = memoryStream.ToArray();
                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            UpdateResim(yeniResimBytes);
            OgrenciForm ogrenciForm = new OgrenciForm();
            if(aktifMi)
            ogrenciForm.Show();

        }

        private void frmFotoGuncelle_Load(object sender, EventArgs e)
        {
            ShowEskiResim();
        }
    }
}
