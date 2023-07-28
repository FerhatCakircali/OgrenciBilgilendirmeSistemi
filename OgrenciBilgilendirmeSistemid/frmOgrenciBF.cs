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
    public partial class frmOgrenciBF : Form
    {
        public frmOgrenciBF()
        {
            InitializeComponent();
        }

        SqlConnection con, con2;
        SqlCommand cmd, cmd2;
        SqlDataReader dr, dr2;

        private string ogrNo;

        string connectionString = "Data Source=DESKTOP-QNH4MN6;Initial Catalog=OBS_DB;Integrated Security=True";

        private void GetOgrenciBilgiFormu()
        {
            OgrenciForm ogrenciForm = new OgrenciForm();
            ogrNo = OgrenciForm.ogrenciData;

            string ogrenciSorgu = "SELECT * FROM OgrenciBilgi where OgrenciNo=@OgrenciNo";
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand(ogrenciSorgu, con);
            cmd.Parameters.AddWithValue("@OgrenciNo", ogrNo);
            con.Open();
            dr = cmd.ExecuteReader();

            string tcNo, ad, soyad, eposta, sinif, fakulte, bolum, program, tercihS, puanT, puan;
            if (dr.Read())
            {
                tcNo = dr["TcNo"].ToString();
                ad = dr["Ad"].ToString().ToUpper();
                soyad = dr["Soyad"].ToString().ToUpper();
                eposta = dr["EPosta"].ToString();
                sinif = dr["Sinif"].ToString();
                fakulte = dr["Fakulte"].ToString().ToUpper();
                bolum = dr["Bolum"].ToString().ToUpper();
                program = dr["Program"].ToString().ToUpper();
                tercihS = dr["TercihSirasi"].ToString();
                puanT = dr["PuanTuru"].ToString().ToUpper();
                puan = dr["Puani"].ToString();
                byte[] imgByte = (byte[])dr["OgrenciResim"];

                MemoryStream ms = new MemoryStream(imgByte);
                pcBoxOgrResim.Image = Image.FromStream(ms);

                txtOgrNo.Text = ogrNo;
                txtTcNo.Text = tcNo;
                txtAd.Text = ad;
                txtSoyad.Text = soyad;
                txtEposta.Text = eposta;
                txtSinif.Text = sinif;
                txtFakulte.Text = fakulte;
                txtBolum.Text = bolum;
                txtProgram.Text = program;
                txtTercih.Text = tercihS;
                txtPuanT.Text = puanT;
                txtPuan.Text = puan;

            }
            con.Close();
        }

        private void GetIletisimBilgileri()
        {
            string iletisimSorgu = "SELECT * FROM OgrenciBilgi where OgrenciNo=@OgrenciNo";
            con2 = new SqlConnection(connectionString);
            cmd2 = new SqlCommand(iletisimSorgu, con2);
            cmd2.Parameters.AddWithValue("@OgrenciNo", ogrNo);
            con2.Open();
            dr2 = cmd2.ExecuteReader();

            string adres, il, ilce, pkodu, telno;
            if (dr2.Read())
            {
                adres = dr2["Adres"].ToString().ToUpper();
                il = dr2["Sehir"].ToString().ToUpper();
                ilce = dr2["Ilce"].ToString().ToUpper();
                pkodu = dr2["PostaKod"].ToString();
                telno = dr2["TelefonNo"].ToString();

                txtAdres.Text = adres;
                txtSehir.Text = il;
                txtIlce.Text = ilce;
                txtPkodu.Text = pkodu;
                txtTelefon.Text = telno;
            }
            con2.Close();
        }



        private void frmOgrenciBF_Load(object sender, EventArgs e)
        {
            GetOgrenciBilgiFormu();
            GetIletisimBilgileri();
        }
    }
}
