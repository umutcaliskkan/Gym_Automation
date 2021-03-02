using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gym_Control
{
    public partial class AnaMenu : Form
    {
        public AnaMenu()
        {
            InitializeComponent();
        }

        private void Btn_KullaniciEkle_Click(object sender, EventArgs e)
        {
            kullaniciEkle frkullaniciekle = new kullaniciEkle();
            frkullaniciekle.Show();
        }

        private void Btn_KullaniciListele_Click(object sender, EventArgs e)
        {
            KullaniciListele frkullaniciListele = new KullaniciListele();
            frkullaniciListele.Show();

        }

        private void Btn_Egzersiz_Click(object sender, EventArgs e)
        {
            egzersiz fregzersiz = new egzersiz();
            fregzersiz.Show();
        }

        private void Btn_Diyet_Click(object sender, EventArgs e)
        {
            diyet frdiyet = new diyet();
            frdiyet.Show();
        }
    }
}
