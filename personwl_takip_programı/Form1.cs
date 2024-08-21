using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//System.Data.OleDb kütüphanesinin eklenmesi
using System.Data.OleDb;
namespace personwl_takip_programı
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //veri tabanı dosya yolu ve provider nesnesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source = personel.accdb");

        //formlar arası veri aktarımında kullanılacak değişkenler
        public static string Tcno, Ad, Soyad, Yetki;
        //yerel değişkenler
        int hak = 3;
        bool durum = false;


        private void button1_Click(object sender, EventArgs e)
        {
            if (hak != 0)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if (kayitokuma["Kullaniciadi"].ToString() == textBox1.Text && kayitokuma["Parola"].ToString() == textBox2.Text
                            && kayitokuma["Yetki"].ToString() == "Yönetici")
                        {
                            durum = true;
                            Tcno = kayitokuma.GetValue(0).ToString();
                            Ad = kayitokuma.GetValue(1).ToString();
                            Soyad = kayitokuma.GetValue(2).ToString();
                            Yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();
                            Form2 frm2 = new Form2();
                            frm2.Show();
                            break;


                        }
                    }

                    if (radioButton2.Checked == true)
                    {
                        if (kayitokuma["Kullaniciadi"].ToString() == textBox1.Text && kayitokuma["Parola"].ToString() == textBox2.Text
                            && kayitokuma["Yetki"].ToString() == "Kullanici")
                        {
                            durum = true;
                            Tcno = kayitokuma.GetValue(0).ToString();
                            Ad = kayitokuma.GetValue(1).ToString();
                            Soyad = kayitokuma.GetValue(2).ToString();
                            Yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();
                            Form3 frm3 = new Form3();
                            frm3.Show();
                            break;


                        }
                    }
                }

                if (durum == false)

                    hak--;
                baglantim.Close();

            }
                label5.Text = Convert.ToString(hak);
                if (hak == 0)
                {
                    button1.Enabled = false;
                    MessageBox.Show("Giriş hakkı kalmadı!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

            
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Girişi...";
            this.AcceptButton = button1;
            this.CancelButton = button2;
            label5.Text = Convert.ToString(hak);
            radioButton1.Checked = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
