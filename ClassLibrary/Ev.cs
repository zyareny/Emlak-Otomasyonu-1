﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Ev
    {
        public static List<Ev> evler = new List<Ev>(1000);
        public static int sehir = 0;
        public static int id = 1;

        public int OdaSayisi {get;set;}
        public int KatNumarasi { get; set; }
        public string Semt { get; set; }
        public DateTime YapimTarihi { get; set; }
        bool Aktif { get; set; }
        public decimal EmlakNumarasi
        {   // Şehir Plaka Kodu-YapimTarihYili-ID  -- 42 2018 001  --- Her ev için farklı bir numara
            get;set; 
        }
        public decimal Alan { get; set; }
        public int Yas { get; set; }
        public int turuSayi = 0;

        public enum Turu
        {
            Daire, Bahceli, Dubleks, Mustakil
        }

        DateTime dtNow = DateTime.Now;

        public Ev(  int OdaSayisi = 0, 
                    int KatNumarasi = 0, 
                    string Semt = "Selçuk Üniversitesi", 
                    decimal Alan = 10, 
                    DateTime dtNow = new DateTime())
        { 
            if(OdaSayisi >= 0)
            {
                this.OdaSayisi = OdaSayisi;
            }
            else
            {
                this.OdaSayisi = 0;
            }
            this.KatNumarasi = KatNumarasi;
            this.Semt = Semt;
            if(Alan > 0)
            {
                this.Alan = Alan;
            }
            else
            {
                this.Alan = 0;
            }

            if (DateTime.Now >= dtNow)
            {
                this.YapimTarihi = dtNow;
            }
            else
            {
                this.YapimTarihi = DateTime.Now;
            }

            Yas = DateTime.Now.Year - YapimTarihi.Year;

           // EmlakNumarasi = string.Format((sehir)+YapimTarihi.Year+id);

        }

        virtual public string EvBilgileri()
        {
            return string.Format("Oda Sayısı : {0} Kat Numarası : {1} Semt : {2} Alan : {3} Turu : {4} Yaşı: {5}",
                OdaSayisi, KatNumarasi,Semt, Alan, (Turu)turuSayi, Yas);
        }

        public int FiyatHesapla()
        {
            string dosya_yolu = "../../room_cost.txt";
            int katSayi = 200;

            if (!File.Exists(dosya_yolu))
            {
                return katSayi * OdaSayisi;
            }
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            string yazi = sw.ReadLine();
            {
                string[] dizi = yazi.Split('|');
                if (dizi[0].Equals((Turu)turuSayi))
                {
                    katSayi = int.Parse(dizi[1]);
                }
            }
            sw.Close();
            fs.Close();
            return katSayi * OdaSayisi;
        }
    }

    public class KiralikEv : Ev
    {
        decimal Kira, Depozito; 
        
        public KiralikEv(int OdaSayisi, int KatNumarasi, string Semt, decimal Alan, DateTime YapimTarihi, decimal Kira = 800, decimal Depozito = 100): base(OdaSayisi,KatNumarasi ,Semt, Alan, YapimTarihi)
        {
            if(Kira > 0)
            {
                this.Kira = Kira;
            }
            else
            {
                this.Kira = 0;
            }

            if(Depozito > 0)
            {
                this.Depozito = Depozito;
            }
            else
            {
                this.Depozito = 0;
            }
        }

        public override string EvBilgileri()
        {
            return string.Format("Oda Sayısı : {0} Kat Numarası : {1} Semt : {2} Alan : {3} Turu : {4} Yaşı: {5} Kira : {6} Depozito : {7}", 
                OdaSayisi, KatNumarasi,Semt, Alan, (Turu)turuSayi, Yas, Kira, Depozito);
        }
    }

    public class SatilikEv : Ev
    {
        decimal Fiyat;

        public SatilikEv(int OdaSayisi, int KatNumarasi, string Semt, decimal Alan, DateTime YapimTarihi, decimal Fiyat = 800) : base(OdaSayisi, KatNumarasi, Semt, Alan,YapimTarihi)
        {
            if(Fiyat > 0)
            {
                this.Fiyat = Fiyat;
            }
            else
            {
                this.Fiyat = 0;
            }
        }

        public override string EvBilgileri()
        {
            return string.Format("Oda Sayısı : {0} Kat Numarası : {1} Semt : {2} Alan : {3} Turu : {4} Yaşı: {5} Fiyat : {6}",
                OdaSayisi, KatNumarasi,Semt, Alan, (Turu)turuSayi, Yas, Fiyat);
        }
    }
}
