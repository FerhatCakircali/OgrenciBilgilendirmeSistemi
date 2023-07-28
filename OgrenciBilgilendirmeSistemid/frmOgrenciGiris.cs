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
    public partial class frmOgrenciGiris : Form
    {
        public frmOgrenciGiris()
        {
            InitializeComponent();
            LoadCaptcha();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Programdan çıkılsın mı?", "ÇIKIŞ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void Btn_Close_MouseMove(object sender, MouseEventArgs e)
        {
            Btn_Close.FillColor = Color.Red;

        }

        private void Btn_Close_MouseLeave(object sender, EventArgs e)
        {
            Btn_Close.FillColor = Color.Transparent;
        }

        private void btnPwShowHide_MouseLeave(object sender, EventArgs e)
        {
            txtSifre.PasswordChar = '*';
        }

        private void btnPwShowHide_MouseMove(object sender, MouseEventArgs e)
        {
            txtSifre.PasswordChar = '\0';
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        static int captcha;
        private void LoadCaptcha()
        {
            Random r1 = new Random();
            int s1= r1.Next(1, 91);
            Random r2 = new Random();
            int s2 = r2.Next(1, 10);
            lblCaptcha.Text = s1 + " + " + s2 + " = ?";
            captcha = s1 + s2;
        }
        private void btnGiris_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT * FROM OgrenciGiris where OgrenciNo=@OgrenciNo AND Sifre=@Sifre";
            con = new SqlConnection("Data Source=DESKTOP-QNH4MN6;Initial Catalog=OBS_DB;Integrated Security=True");
            cmd = new SqlCommand(sorgu, con);
            cmd.Parameters.AddWithValue("@OgrenciNo", txtOgenciNo.Text);
            cmd.Parameters.AddWithValue("@Sifre", txtSifre.Text);
            con.Open();
            dr = cmd.ExecuteReader();
           if(!string.IsNullOrEmpty(txtOgenciNo.Text) && !string.IsNullOrEmpty(txtSifre.Text) && !string.IsNullOrEmpty(txtCaptcha.Text))
            {
                if (dr.Read())
                {
                    int captchaTxt = int.Parse(txtCaptcha.Text);
                    if (captcha == captchaTxt)
                    {
                        
                        OgrenciForm ogrenciForm = new OgrenciForm();
                        OgrenciForm.ogrenciData = txtOgenciNo.Text;
                        ogrenciForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        LoadCaptcha();
                    }
                }
                else
                {
                    MessageBox.Show("Öğrenci numaranız veya şifreniz yanlış!");
                }

            }
            else
            {
                MessageBox.Show("Boş değer girdiniz. Lütfen bütün alanları doldurun!");
            }

            con.Close();
        }
    }
}
