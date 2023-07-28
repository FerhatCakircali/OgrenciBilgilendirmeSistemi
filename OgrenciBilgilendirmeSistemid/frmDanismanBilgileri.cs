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
    public partial class frmDanismanBilgileri : Form
    {
        public frmDanismanBilgileri()
        {
            InitializeComponent();
        }

        SqlConnection con,con2;
        SqlCommand cmd, cmd2;
        SqlDataReader dr,dr2;

        private string ogrNo;
        private string danismanNo;
        private string DanismanBilgiGetir()
        {
            OgrenciForm ogrenciForm = new OgrenciForm();
            ogrNo = OgrenciForm.ogrenciData;

            string ogrenciSorgu = "SELECT * FROM OgrenciDanismanBilgi where OgrenciNo=@OgrenciNo";
            con = new SqlConnection("Data Source=DESKTOP-QNH4MN6;Initial Catalog=OBS_DB;Integrated Security=True");
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
            con2 = new SqlConnection("Data Source=DESKTOP-QNH4MN6;Initial Catalog=OBS_DB;Integrated Security=True");
            cmd2 = new SqlCommand(danismanSorgu, con2);
            cmd2.Parameters.AddWithValue("@OgrElemNo", DanismanBilgiGetir());
            con2.Open();
            dr2 = cmd2.ExecuteReader();

            string unvan, ad, soyad, fakulte, bolum, program, telno, eposta;
            if (dr2.Read())
            {
                unvan = dr2["OgrElemUnvan"].ToString();
                ad = dr2["OgrElemAd"].ToString().ToUpper();
                soyad = dr2["OgrElemSoyad"].ToString().ToUpper();
                fakulte = dr2["Fakulte"].ToString().ToUpper();
                bolum = dr2["Bolum"].ToString().ToUpper();
                program = dr2["Program"].ToString().ToUpper();
                telno = dr2["TelNo"].ToString();
                eposta = dr2["Eposta"].ToString();

                lblUAS.Text = unvan + " " + ad + " " + soyad;
                lblFakulte.Text = fakulte;
                lblBolum.Text = bolum;
                llblProgram.Text = program;
                lblEposta.Text = eposta;
                lblTel.Text = telno;
            }
            con2.Close();
        }
        private void frmDanismanBilgileri_Load(object sender, EventArgs e)
        {
            DanismanBilgiLoad();
        }

    }
}
