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


namespace Gym_Control
{ 
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-BCV2AJ1;Initial Catalog=GymControlDB;Integrated Security=True");
       

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            
            if(tb_kullanici.Text=="" && tb_sifre.Text == "")
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurunuz");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Select * From yonetici where kullanici_adi=@p1 and sifre=@p2", baglanti);
                komut.Parameters.AddWithValue("@p1", tb_kullanici.Text);
                komut.Parameters.AddWithValue("@p2", tb_sifre.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    AnaMenu frAnamenu = new AnaMenu();
                    this.Hide();
                    frAnamenu.Show();
                }
                else
                {
                    MessageBox.Show("Hatalı Giriş Bilgileri !");
                }
                baglanti.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
