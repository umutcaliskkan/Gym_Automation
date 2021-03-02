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
    public partial class diyet : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-BCV2AJ1;Initial Catalog=GymControlDB;Integrated Security=True");
        public diyet()
        {
            InitializeComponent();
        }

        private void diyet_Load(object sender, EventArgs e)
        {
            combo_Kisi.Items.Clear();
         
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select k.ad_soyad,k.id FROM kullanici k;", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = oku[0].ToString();
                item.value = oku[1].ToString();
                combo_Kisi.Items.Add(item);

              //  combo_Kisi.Items.Add(oku[1].ToString());

            }
            baglanti.Close(); 
        }

        private void Btn_Getir_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("Select * FROM diyet d where d.kullanici_id=@kullanici", baglanti);// Diyet tablosundan sorgu yapıldı combobox ta secilene göre
            string kullanici_id = ((ComboBoxItem)combo_Kisi.SelectedItem).value.ToString();//Combobox ta secilen degeri alıp stringe cevirip degiskene atildi
            int k_adi = Int32.Parse(kullanici_id); //alınan string degiskeni de  int e cevirildi
            komut3.Parameters.AddWithValue("@kullanici", k_adi);
            baglanti.Open();
            komut3.ExecuteNonQuery(); // komutu calistir
            SqlDataReader oku2 = komut3.ExecuteReader(); // gelen verileri oku

            while(oku2.Read())
            {
                //Database te ki ogun textleri okunuyor ve Textbox a yaziliyor
                tb_kahvalti.Text = oku2[2].ToString();
                tb_kusluk.Text = oku2[3].ToString();
                tb_ogle.Text = oku2[4].ToString();
                tb_ikindi.Text = oku2[5].ToString();
                tb_aksam.Text = oku2[6].ToString();
                tb_araogun.Text = oku2[7].ToString();


                //Database ten saatleri cekme
                string str_kahvalti = oku2["kahvalti_saat"].ToString(); //saat sütunu okunuyor ve stringe cevirilip degiskene atanıyor
                DateTime dt_kahvalti = DateTime.Parse(str_kahvalti);//sonra bu stringi alıp time a ceviriyor
                t_kusluk.Value = dt_kahvalti; // sonra degiskeni formda ki saatte gösteriyor

                string str_kusluk = oku2["kusluk_saat"].ToString();
                DateTime dt_kusluk = DateTime.Parse(str_kusluk);
                t_kusluk.Value = dt_kusluk;

                string str_ogle = oku2["ogle_saat"].ToString();
                DateTime dt_ogle = DateTime.Parse(str_ogle);
                t_kusluk.Value = dt_ogle;

                string str_ikindi = oku2["ikindi_saat"].ToString();
                DateTime dt_ikindi = DateTime.Parse(str_ikindi);
                t_kusluk.Value = dt_ikindi;

                string str_aksam = oku2["aksam_saat"].ToString();
                DateTime dt_aksam = DateTime.Parse(str_aksam);
                t_kusluk.Value = dt_aksam;

                string str_ara = oku2["ara_saat"].ToString();
                DateTime dt_ara = DateTime.Parse(str_ara);
                t_ara.Value = dt_ara;
             

                //veritabanında diyet tablosunda 14 ve 15. sütunlardan tarih verileri okunuyor
                dTP_baslangic.Value = (DateTime)oku2[14];
                dTP_Bitis.Value = (DateTime)oku2[15];
            }
            baglanti.Close();
        }

        private void btn_Ekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("INSERT INTO diyet(kullanici_id,kahvalti,kusluk,ogle,ikindi,aksam,ara,kahvalti_saat,kusluk_saat,ogle_saat,ikindi_saat,aksam_saat,ara_saat,baslangic_tarihi,bitis_tarihi) VALUES (@kullanici_id,@kahvalti,@kusluk,@ogle,@ikindi,@aksam,@ara,@kahvalti_saat,@kusluk_saat,@ogle_saat,@ikindi_saat,@aksam_saat,@ara_saat,@baslangic_tarihi,@bitis_tarihi)", baglanti);

            string kullanici_id = ((ComboBoxItem)combo_Kisi.SelectedItem).value.ToString();
            komut2.Parameters.AddWithValue("@kullanici_id", kullanici_id);

            komut2.Parameters.AddWithValue("@kahvalti", tb_kahvalti.Text);
            komut2.Parameters.AddWithValue("@kahvalti_saat", t_kahvalti.Value);

            komut2.Parameters.AddWithValue("@kusluk", tb_kusluk.Text);
            komut2.Parameters.AddWithValue("@kusluk_saat", t_kusluk.Value);

            komut2.Parameters.AddWithValue("@ogle", tb_ogle.Text);
            komut2.Parameters.AddWithValue("@ogle_saat", t_ogle.Value);

            komut2.Parameters.AddWithValue("@ikindi", tb_ikindi.Text);
            komut2.Parameters.AddWithValue("@ikindi_saat", t_ikindi.Value);

            komut2.Parameters.AddWithValue("@aksam", tb_aksam.Text);
            komut2.Parameters.AddWithValue("@aksam_saat", t_akşam.Value);

            komut2.Parameters.AddWithValue("@ara", tb_araogun.Text);
            komut2.Parameters.AddWithValue("@ara_saat", t_ara.Value);

            komut2.Parameters.AddWithValue("@baslangic_tarihi", dTP_baslangic.Value);
            komut2.Parameters.AddWithValue("@bitis_tarihi", dTP_Bitis.Value);

            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Diyet Ekleme Başarılı ");
        }
    }
}
