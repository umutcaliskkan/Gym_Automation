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
    public partial class kullaniciEkle : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-BCV2AJ1;Initial Catalog=GymControlDB;Integrated Security=True");
        public kullaniciEkle()
        {
            InitializeComponent();
        }
        

        private void btn_Ekle_Click(object sender, EventArgs e)
        {
            TimeSpan fark;
            fark = DateTime.Parse(dTP2.Text) - DateTime.Parse(dTP1.Text);
            String sonuc = fark.TotalDays.ToString();
            lbl_Gun.Text = sonuc;

            int gun = Int32.Parse(sonuc);

            if (tb_adSoyad.Text == "" && tb_Mail.Text == "" && tb_Parola.Text == "" && Msk_Telefon.Text == "")
            {
                MessageBox.Show("Boş Yer Bırakılamaz !");
            }
            else if (gun<=0)
            {
                MessageBox.Show("Lütfen Geçerli Bir Tarih Girin !");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO kullanici(ad_soyad,mail,parola,telefon,kayit_baslama,kayit_bitis) VALUES (@ad_soyad, @mail, @parola,@telefon, @kayit_baslama, @kayit_bitis)",baglanti);
                komut.Parameters.AddWithValue("@ad_soyad", tb_adSoyad.Text);
                komut.Parameters.AddWithValue("@mail", tb_Mail.Text);
                komut.Parameters.AddWithValue("@parola", tb_Parola.Text);
                komut.Parameters.AddWithValue("@telefon", Msk_Telefon.Text);
                komut.Parameters.AddWithValue("@kayit_baslama", dTP1.Value);
                komut.Parameters.AddWithValue("@kayit_bitis", dTP2.Value);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Başarılı ");
            }

           
        }

       
    }
}
