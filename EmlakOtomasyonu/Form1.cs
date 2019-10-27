﻿using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmlakOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }   

        private void cb_İl_SelectedValueChanged(object sender, EventArgs e)
        {
            Ev.sehir = (cb_İl.SelectedIndex)+1;
            string dosya_yolu = "../../semtler.txt";
            if (!File.Exists(dosya_yolu))
            {
                MessageBox.Show("Semt Dosyası Bulunmuyor");
                return;
            }
            else
            {
                List<string> semtler = DosyaIslemleri.DosyaOkumaSemt(dosya_yolu, cb_İl.Text);
                cbSemt.Items.Clear();
                foreach (string semt in semtler)
                {
                    cbSemt.Items.Add(semt);
                }
                cbSemt.Enabled = true;
            }
        }

        private void rbSatilik_CheckedChanged(object sender, EventArgs e)
        {
            FormKontrol.SatilikRadioButton(this);
        }

        private void rbKiralik_CheckedChanged(object sender, EventArgs e)
        {
            FormKontrol.KiralikRadioButton(this);
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            bool kontrol = FormKontrol.FormButtonKontrol(this);
            if (kontrol)
            {
                if (rbSatilik.Checked)
                {
                    SatilikEv satilikEv = new SatilikEv(
                        int.Parse(txtOdaSayisi.Text), int.Parse(txtKatNumarasi.Text),
                    cbSemt.Text, int.Parse(txtAlan.Text), DateTime.Parse(yapim.Text), int.Parse(txtFiyat.Text));
                    Ev.evler.Add(satilikEv);
                    MessageBox.Show("Satılık ev eklendi");
                    string dosya_yolu = "../../satilik.txt";
                    FileStream fs = new FileStream(dosya_yolu, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);

                    foreach(Ev ev in Ev.evler)
                    {
                        sw.WriteLine(ev.EmlakNumarasi + "|"+ev.OdaSayisi + "|" + ev.KatNumarasi + "|" + ev.Semt + "|" + ev.Alan + "|" + ev.turuSayi + "|" + ev.Yas + "|" + ev.YapimTarihi);
                    }
                    sw.Close();
                    fs.Close();
                }
                else
                {
                    Console.WriteLine("*");
                    KiralikEv kiralikEv = new KiralikEv(int.Parse(txtOdaSayisi.Text), int.Parse(txtKatNumarasi.Text),
                        cbSemt.Text, int.Parse(txtAlan.Text), DateTime.Parse(yapim.Text), decimal.Parse(txtKira.Text), decimal.Parse(txtDepozito.Text));
                    Ev.evler.Add(kiralikEv);
                    MessageBox.Show("Kiralık ev eklendi");
                }
            }
            else
            {
                MessageBox.Show("Boş yerleri doldurunuz");
            }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*string dosya_yolu = "../../satilik.txt";
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("windows-1254"));
            string yazi = sr.ReadLine();

            while(yazi != null)
            {
                Console.WriteLine()
            }*/
        }
    }
}