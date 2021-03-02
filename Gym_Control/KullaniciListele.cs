using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Gym_Control
{
    public partial class KullaniciListele : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-BCV2AJ1;Initial Catalog=GymControlDB;Integrated Security=True");
        public KullaniciListele()
        {
            InitializeComponent();
        }

        void kullanici_listele()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From kullanici", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dGV1.DataSource = dt;
            baglanti.Close();

        }
        private void KullaniciListele_Load(object sender, EventArgs e)
        {
            kullanici_listele();
        }

        private void dGV1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            lbl_Id.Text = dGV1.CurrentRow.Cells[0].Value.ToString();
            tb_AdSoyad.Text = dGV1.CurrentRow.Cells[1].Value.ToString();
            tb_mail.Text = dGV1.CurrentRow.Cells[2].Value.ToString();
            tb_parola.Text = dGV1.CurrentRow.Cells[3].Value.ToString();
            msk_telefon.Text = dGV1.CurrentRow.Cells[4].Value.ToString();

        }

        private void btn_Sil_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Silmeyi Onaylıyor musun ?", "Silme İşlemi", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from kullanici Where id=@id", baglanti);
                komut.Parameters.AddWithValue("@id", Convert.ToInt32(lbl_Id.Text));
                baglanti.Open();
                komut.ExecuteNonQuery(); // Komutu çalıştır
                baglanti.Close();
                kullanici_listele();
            }
           
        }

        private void Btn_Guncelle_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Güncellemeyi Onaylıyor musun ?", "Güncelleme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut2 = new SqlCommand("UPDATE kullanici SET ad_soyad=@ad_soyad, mail=@mail, parola=@parola,telefon=@telefon WHERE id=@id", baglanti);//Secilen id ye gore guncelleme yapilacak
                baglanti.Open();
                komut2.Parameters.AddWithValue("@id", Convert.ToInt32(lbl_Id.Text));
                komut2.Parameters.AddWithValue("@ad_soyad", tb_AdSoyad.Text);
                komut2.Parameters.AddWithValue("@mail", tb_mail.Text);
                komut2.Parameters.AddWithValue("@parola", tb_parola.Text);
                komut2.Parameters.AddWithValue("@telefon", msk_telefon.Text);
                komut2.ExecuteNonQuery(); // komutlari calistir
                baglanti.Close();
                kullanici_listele();
            }
            
          
        }
    }
}
