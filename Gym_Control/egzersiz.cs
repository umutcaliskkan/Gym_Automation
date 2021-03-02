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
    public partial class egzersiz : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-BCV2AJ1;Initial Catalog=GymControlDB;Integrated Security=True");
        public egzersiz()
        {
            InitializeComponent();
        }

        void diyet_listele()
        {
            baglanti.Open();
            
            
            string kullanici_id = ((ComboBoxItem)cb_isim.SelectedItem).value.ToString();
            int k_adi = Int32.Parse(kullanici_id);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM egzersiz_programi d WHERE d.kisi_id='"+k_adi+"'", baglanti);
            
            DataTable dt = new DataTable();
            da.Fill(dt);
            dGV.DataSource = dt;
            baglanti.Close();
        }
        private void egzersiz_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT k.ad_soyad,k.id FROM kullanici k;", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = oku[0].ToString();
                item.value = oku[1].ToString();
                cb_isim.Items.Add(item);
            }
            baglanti.Close();
            
        }

        private void btn_getir_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM egzersizler WHERE egzersiz_index=@index", baglanti);
            komut.Parameters.AddWithValue("@index", cb_bolge.SelectedIndex);
            baglanti.Open();
            komut.ExecuteNonQuery();
            SqlDataReader oku = komut.ExecuteReader();
            cb_hareket.Items.Clear();
            while (oku.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = oku[1].ToString();
                item.value = oku[3].ToString();
                cb_hareket.Items.Add(item.ToString());
            }
            baglanti.Close();
        }

        private void Btn_Kaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("INSERT INTO egzersiz_programi(kisi_id,gun,bolge,hareket,set_sayisi,tekrar) VALUES (@kisi_id,@gun,@bolge,@hareket,@set_sayisi,@tekrar)", baglanti);

            string kullanici_id = ((ComboBoxItem)cb_isim.SelectedItem).value.ToString();
            int k_adi = Int32.Parse(kullanici_id);

            int gun = int.Parse(cb_gun.SelectedItem.ToString());
            int set = int.Parse(cb_set.SelectedItem.ToString());
            int tekrar = int.Parse(cb_tekrar.SelectedItem.ToString());

            komut2.Parameters.AddWithValue("@kisi_id", k_adi);
            komut2.Parameters.AddWithValue("@gun",gun);
            komut2.Parameters.AddWithValue("@bolge", cb_bolge.SelectedItem.ToString());
            komut2.Parameters.AddWithValue("@hareket", cb_hareket.SelectedItem.ToString());
            komut2.Parameters.AddWithValue("@set_sayisi", set);
            komut2.Parameters.AddWithValue("@tekrar", tekrar);
            
            komut2.ExecuteNonQuery();
            baglanti.Close();
            diyet_listele();
            MessageBox.Show("Egzersiz Kayit Başarılı ");

        }

        private void btn_kisi_Getir_Click(object sender, EventArgs e)
        {
            diyet_listele();
        }
    }
}
