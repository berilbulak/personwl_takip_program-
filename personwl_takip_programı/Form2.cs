using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//System.OleDb.Oledb kütüphanesinin tanımlanması
using System.Data.OleDb;
//System.Text.RegularExpression (Regex) kütüphanesinin tanımlanması
using System.Text.RegularExpressions;
//Giriş çıkış işlemleri için kütüphanenin tanımlanması
using System.IO;

namespace personwl_takip_programı
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }
        //Veri tabanı dosya yolu ve provider nesenlerinin belirlenmesi
        OleDbConnection baglantim= new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=personel.accdb");
        

        private void kullanicilari_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter kullanicilari_listele = new OleDbDataAdapter ("select Tcno AS [Tc No],Kullaniciadi AS[Kullanıcı Adı] from kullanicilar Order By Ad ASC ", baglantim);
                DataSet dshafiza = new DataSet();
                kullanicilari_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglantim.Close();
                
            }
            catch (Exception hatamsj) 
            {
                MessageBox.Show(hatamsj.Message,"SKY Personel Takip Programı",MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close() ;

                
            }

        }

        private void personelleri_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter personelleri_listele = new OleDbDataAdapter("select Tcno AS [Tc No],Kullaniciadi AS[Kullanıcı Adı] ,Dogumtarihi AS[Doğum Tarihi],Görevyeri AS[Görev Yeri],Maas AS [Maaş] from personeller Order By Ad ASC ", baglantim);
                DataSet dshafiza = new DataSet();
                personelleri_listele.Fill(dshafiza);
                dataGridView2.DataSource = dshafiza.Tables[0];
                baglantim.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();


            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //form2 ayarları
            pictureBox1.Height = 150;
            pictureBox1.Width = 150;
            //fotoğraf koymak için
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.Tcno + ".jpg");
            }

            catch 
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok.jpg");

            }
            //Kullanıcı işlemleri sekmesi
            this.Text = "Yönetici İşlemleri";
            label12.ForeColor = Color.DarkRed;
            label12.Text = Form1.Ad + " " + Form1.Soyad;
            textBox1.MaxLength = 11;
            textBox4.MaxLength = 8;
            toolTip1.SetToolTip(this.textBox1, "Tc Kimlik No 11 Karakter Olmalı!");
            radioButton1.Checked = true;

            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox5.MaxLength = 10;
            textBox6.MaxLength = 10;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            kullanicilari_goster();

            //Personel işlemleri sekmesi
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Width = 100;
            pictureBox2.Height = 100;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            maskedTextBox1.Mask = "00000000000";
            maskedTextBox2.Mask = "LL????????????????????";
            maskedTextBox3.Mask = "LL????????????????????";
            maskedTextBox4.Mask = "0000";
            maskedTextBox2.Text.ToUpper();
            maskedTextBox3.Text.ToUpper();

            comboBox1.Items.Add("İlköğretim");
            comboBox1.Items.Add("Ortaöğretim");
            comboBox1.Items.Add("Lise");
            comboBox1.Items.Add("Üniversite");

            comboBox2.Items.Add("Yönetici");
            comboBox2.Items.Add("Memur"); 
            comboBox2.Items.Add("Şöför");
            comboBox2.Items.Add("İşçiler");

            comboBox3.Items.Add("Arge");
            comboBox3.Items.Add("Bilgi İşlem");
            comboBox3.Items.Add("Myhasebe");
            comboBox3.Items.Add("Üretim");
            comboBox3.Items.Add("Nakliye");

            DateTime zaman = DateTime.Now;
            int yil = int.Parse(zaman.ToString("yyyy"));
            int ay = int.Parse(zaman.ToString("MM"));
            int gun = int.Parse(zaman.ToString("dd"));

            dateTimePicker1.MinDate = new DateTime(1960,1,1);
            dateTimePicker1.MaxDate = new DateTime(yil - 18, ay, gun);
            dateTimePicker1.Format = DateTimePickerFormat.Short;

            radioButton3.Checked = true;

        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 11)

                errorProvider1.SetError(textBox1, "Tc Kimlik No 11 Karakterli Olmalı!");
            else
                errorProvider1.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((int)e.KeyChar>=48 && (int)e.KeyChar<=57||(int)e.KeyChar==8)
                { e.Handled = false; }
            else
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsLetter(e.KeyChar)==true || char.IsControl(e.KeyChar)==true || char.IsSeparator(e.KeyChar)==true)
                { e.Handled = false; }

            else
                e.Handled = true;


        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            { e.Handled = false; }

            else
                e.Handled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length != 8)
                errorProvider1.SetError(textBox4, "Kullanıcı adı 8 karakter olmalı!");

            else
                errorProvider1.Clear();

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsLetter(e.KeyChar)==true || char.IsControl(e.KeyChar)==true || char.IsDigit(e.KeyChar)==true)
                { e.Handled = true; }
            else 
                  e.Handled = true; 
            
        }
        int parola_skoru = 0;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string parola_seviyesi = "" ;
            int kucuk_harf_skoru = 0, buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = textBox5.Text;
            //regex kütüphanesi için sifre string ifadesindeki türkçe ifadeler ingilizceye dönüştürülmeli
            string duzeltilmis_sifre = "";
            duzeltilmis_sifre = sifre;
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');

            if (sifre != duzeltilmis_sifre)
            {
                sifre = duzeltilmis_sifre;
                textBox5.Text = sifre;
                MessageBox.Show("Paroladaki türkçe karakterler ingilizceye dönüştürülmüştür.");
            }
            //1 küçük harf 10 puan 2 ve fazlası 20 puan
            int az_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;
             kucuk_harf_skoru = Math.Min(2,az_karakter_sayisi)*10;

            //1 büyük harf 10 puan 2 ve fazlası 20 puan
            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, az_karakter_sayisi) * 10;


            //1 rakam harf 10 puan 2 ve fazlası 20 puan
            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;

            //1 sembol harf 10 puan 2 ve fazlası 20 puan
            int sembol_sayisi = sifre.Length - az_karakter_sayisi-AZ_karakter_sayisi-rakam_sayisi;
            sembol_skoru = Math.Min(2, sembol_sayisi) * 10;

            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru;
            if (sifre.Length == 9)
                parola_skoru += 10;
            else if (sifre.Length == 10)
                parola_skoru += 20;

            if (kucuk_harf_skoru == 0 || buyuk_harf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0)
                label22.Text = "Büyük harf,küçük harf,rakam veya sembol mutlaka kullanılmalı!";
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
                label22.Text = "";

            if (parola_skoru < 70)
                parola_seviyesi = "Kabul edilemez";
            else if (parola_skoru == 70 || parola_skoru == 80)
                parola_seviyesi = "Güçlü";
            else if (parola_skoru == 90 || parola_skoru == 100)
                parola_seviyesi = "Çok Güçlü";

            label9.Text = "%" + Convert.ToString(parola_skoru);
            label10.Text = parola_seviyesi;
            progressBar1.Value = parola_skoru;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text == textBox5.Text)
                errorProvider1.SetError(textBox6, "Parola eşleşmiyor");
            else
                errorProvider1.Clear();
        }
    }
}
