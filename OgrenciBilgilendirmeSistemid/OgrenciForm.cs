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
    public partial class OgrenciForm : Form
    {
        private Form activeForm = null;
        private void openChild(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_FormlarinGecisi.Controls.Add(childForm);
            panel_FormlarinGecisi.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        public OgrenciForm()
        {
            InitializeComponent();
            CostumizeDesign();
            openChild(new frmAnasayfa());
        }
        private void CostumizeDesign() 
        {
            panel_GI.Visible = false;
            panel_DDI.Visible = false;
            panel_FI.Visible = false;
            panel_KI.Visible = false;
        }
        private void HideSubPanel()
        {
            if(panel_GI.Visible==true)
                panel_GI.Visible = false;

            if (panel_DDI.Visible == true)
                panel_DDI.Visible = false;

            if (panel_FI.Visible == true)
                panel_FI.Visible = false;

            if (panel_KI.Visible == true)
                panel_KI.Visible = false;
        }
        private void ShowSubPanel(Panel subPanel)
        {
            if (subPanel.Visible == false)
            {
                HideSubPanel();
                subPanel.Visible = true;
            }
            else
                subPanel.Visible = false;
        }

        private void ToolTip() 
        {
            toolTip1.SetToolTip(cboxBtn_Close, "ÇIKIŞ");
            toolTip1.SetToolTip(lbl_OgrenciNo_AdSoyad, "Profil");
            toolTip1.SetToolTip(pbox_AnasayfaDon, "ANASAYFA");
            toolTip1.SetToolTip(pBox_Ogrenci_Icon, "Fotoğraf Güncelle");
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public static string ogrenciData;
        private void BilgiGetir()
        {
          
            string sorgu = "SELECT * FROM OgrenciBilgi where OgrenciNo=@OgrenciNo";
            con = new SqlConnection("Data Source=DESKTOP-QNH4MN6;Initial Catalog=OBS_DB;Integrated Security=True");
            cmd = new SqlCommand(sorgu, con);
            cmd.Parameters.AddWithValue("@OgrenciNo", ogrenciData);
            con.Open();
            dr = cmd.ExecuteReader();
                 
            string ad , soyad;
            if(dr.Read())
            {
                ad = dr["Ad"].ToString().ToUpper();
                soyad= dr["Soyad"].ToString().ToUpper();
                byte[] imgByte = (byte[])dr["OgrenciResim"];

                lbl_OgrenciNo_AdSoyad.Text = ogrenciData + " - " + ad + " " + soyad;

                if (imgByte != null)
                {
                    MemoryStream ms = new MemoryStream(imgByte);

                    pBox_Ogrenci_Icon.Image = Image.FromStream(ms);

                }
                else
                {
                    pBox_Ogrenci_Icon.Image = null;
                    MessageBox.Show("Öğrencinin resmi bulunamadı!");
                }
            }

            con.Close();
        }
        public void OgrenciForm_Load(object sender, EventArgs e)
        {
            ToolTip();
            BilgiGetir();
        }

        private void btn_Genel_Islem_Click(object sender, EventArgs e)
        {
            ShowSubPanel(panel_GI);
        }

        #region Genel İşlemlerin Altındaki Butonlar
        private void btn_Ozluk_Bilgileri_Click(object sender, EventArgs e)
        {
            openChild(new frmOzlukBilgileri());
        }

        public void btn_Danisman_Bilgileri_Click(object sender, EventArgs e)
        {
            openChild(new frmDanismanBilgileri());
        }

        private void btn_Alinan_Dersler_Click(object sender, EventArgs e)
        {
            openChild(new frmAlinanDersler());
        }

        private void btn_Program_Dersleri_Click(object sender, EventArgs e)
        {
            openChild(new frmProgramDersleri());
        }

        private void btn_Sinav_Takvimi_Click(object sender, EventArgs e)
        {
            openChild(new frmSinavTakvimi());
        }

        private void btn_Ders_Programi_Click(object sender, EventArgs e)
        {
            openChild(new frmDersProgrami());
        }

      
        #endregion Genel İşlemlerin Altındaki Butonlar

        private void btn_Ders_Donem_Islem_Click(object sender, EventArgs e)
        {
            ShowSubPanel(panel_DDI);
        }

        #region Ders ve Dönem İşlemlerinin Altındaki Butonlar
        private void btn_Ders_Kayit_Click(object sender, EventArgs e)
        {
            openChild(new frmDersKayit());
        }

        private void btn_Not_Listesi_Click(object sender, EventArgs e)
        {
            openChild(new frmNotListesi());
        }

        private void btn_Devamsizlik_Durumu_Click(object sender, EventArgs e)
        {
            openChild(new frmDevamsizlikDurumu());
        }

        #endregion Ders ve Dönem İşlemlerinin Altındaki Butonlar

        private void btn_Form_Islem_Click(object sender, EventArgs e)
        {
            ShowSubPanel(panel_FI);
        }

        #region Form İşlemlerinin Altındaki Butonlar
        private void btn_Ogrenci_BF_Click(object sender, EventArgs e)
        {
            openChild(new frmOgrenciBF());
            //
            //.. kod
            //

        }
        #endregion Form İşlemlerinin Altındaki Butonlar

        private void btn_Kullanici_Islem_Click(object sender, EventArgs e)
        {
            ShowSubPanel(panel_KI);
        }

        #region Kullancı İşlemlerinin Altındaki Butonlar
        private void btn_Sifre_Degistir_Click(object sender, EventArgs e)
        {
            openChild(new frmSifreDegistir());
        }
        private void btn_Foto_Guncelle_Click(object sender, EventArgs e)
        {
            openChild(new frmFotoGuncelle());
        }

        #endregion Kullancı İşlemlerinin Altındaki Butonlar

        private void pBoxOkul_Logo_Click(object sender, EventArgs e)
        {

            openChild(new frmAnasayfa());
        }
        private void pbox_AnasayfaDon_Click(object sender, EventArgs e)
        {
            openChild(new frmAnasayfa());
        }
        private void pBox_Ogrenci_Icon_Click(object sender, EventArgs e)
        {
            openChild(new frmFotoGuncelle());
        }

        private void cboxBtn_Close_Click_1(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Programdan çıkılsın mı?", "ÇIKIŞ", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void cboxBtn_Close_MouseMove(object sender, MouseEventArgs e)
        {
            cboxBtn_Close.FillColor = Color.Red;
        }

        private void cboxBtn_Close_MouseLeave(object sender, EventArgs e)
        {
            cboxBtn_Close.FillColor = Color.Transparent;
        }

    }
}
