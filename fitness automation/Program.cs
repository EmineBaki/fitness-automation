using System;
using System.Collections.Generic;

class Program
{
    static List<string> uyeler = new List<string>(); // Üyeleri tutmak için bir liste
    static string adminKullaniciAdi = "admin";
    static string adminSifre = "admin123"; // Admin şifresi

    static void Main()
    {
        Console.Title = "Süslü Giriş Ekranı";
        Console.ForegroundColor = ConsoleColor.Red;   // Metin rengini değiştir

        Console.WriteLine("*****************************************************************");
        Console.WriteLine("*                                                               *");
        Console.WriteLine("*       İron Man Fitness Otomasyonu'na Hoş Geldiniz!            *");
        Console.WriteLine("*                                                               *");
        Console.WriteLine("*                                                               *");
        Console.WriteLine("*****************************************************************");

        Console.ResetColor(); // Renkleri sıfırla

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("*****************************************************************");
        Console.ResetColor();

        bool girisBasarili = false;

        // Kullanıcı adı ve şifreyi doğru girene kadar sormaya devam et
        while (!girisBasarili)
        {
            // Kullanıcı adı ve şifreyi iste
            Console.Write("Kullanıcı Adı: ");
            string girilenKullaniciAdi = Console.ReadLine();

            Console.Write("Şifre: ");
            string girilenSifre = MaskeleSifreGirisi(); // Şifreyi maskele

            // Doğru kullanıcı adı ve şifreyi kontrol et
            girisBasarili = GirisKontrol(girilenKullaniciAdi, girilenSifre);

            if (!girisBasarili)
            {
                Console.WriteLine("Hatalı kullanıcı adı veya şifre. Tekrar deneyin.");
            }
        }

        Console.WriteLine("Giriş başarılı!\n");
        static bool GirisKontrol(string kullaniciAdi, string sifre)
        {
            // Girilen kullanıcı adı ve şifreyi kontrol et
            if (kullaniciAdi == adminKullaniciAdi && sifre == adminSifre)
            {
                return true;
            }
            else if (uyeler.Contains(kullaniciAdi) && sifre == "sifre*") // Diğer kullanıcıları kontrol et
            {
                return true;
            }

            return false;
        }

        // Giriş başarılıysa diğer işlemlere geç
        AnaMenu();
    }

    static void AnaMenu()
    {
        string secim = "";
        while (!secim.Equals("4"))
        {
            Console.WriteLine("1. Üye Ekle");
            Console.WriteLine("2. Üye Listele");
            Console.WriteLine("3. Üye Sil");
            Console.WriteLine("4. Üye Düzenle");
            Console.WriteLine("5. Çıkış");

            secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    UyeEkle();

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("*****************************************************************");
                    Console.ResetColor();

                    Kullanici kullanici = KullaniciBilgileriniAl();
                    IEgzersizProgrami egzersizProgrami = EgzersizProgramiSec();

                    Console.WriteLine("\nFitness programınız:");
                    Console.WriteLine(egzersizProgrami.GetProgram());

                    DateTime antrenmanTarihi = AntrenmanTarihiniSec();
                    TimeSpan antrenmanSaati = AntrenmanSaatiSec();
                    string antrenmanHocasi = AntrenmanHocasiSec();
                    string aylikUcret = AylıkUcretiSec();

                    Console.WriteLine($"\nSeçilen Antrenman Bilgileri:\nTarih: {antrenmanTarihi.ToShortDateString()}\nSaat: {antrenmanSaati}\nHoca: {antrenmanHocasi}\nAylık Ücret: {aylikUcret:C}");

                    Console.WriteLine("\nTeşekkür ederiz. Sağlıklı günler dileriz!");

                    AnaMenu();
                    break;
                case "2":
                    UyeleriListele();
                    AnaMenu();
                    break;
                case "3":
                    UyeSil();
                    AnaMenu();
                    break;

                case "4":
                    UyeDüzenle();
                    AnaMenu();
                    break;

                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }


        }
    }

    // Şifreyi maskelemek için yardımcı metot
    static string MaskeleSifreGirisi()
    {
        string sifre = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

            // Sadece sayılar ve harfler geçerli olsun
            if (char.IsLetterOrDigit(key.KeyChar))
            {
                sifre += key.KeyChar;
                Console.Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && sifre.Length > 0)
            {
                sifre = sifre.Substring(0, sifre.Length - 1);
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine(); // Yeni satıra geç

        return sifre;
    }

    static void UyeEkle()
    {
        string uyeAdi;
        bool gecerliUyeAdi;

        do
        {
            Console.WriteLine("Üye adını ve soyadını giriniz:");
            uyeAdi = Console.ReadLine();

            // Girilen değerin geçerli bir string olup olmadığını ve sadece sayılardan oluşmadığını kontrol et
            gecerliUyeAdi = !string.IsNullOrEmpty(uyeAdi) && !uyeAdi.All(char.IsDigit);

            if (!gecerliUyeAdi)
            {
                Console.WriteLine("Geçersiz üye adı. Lütfen tekrar deneyin.");
            }

        } while (!gecerliUyeAdi);

        uyeler.Add(uyeAdi);  // Girilen üye adı eklenir.

        Console.WriteLine($"Üye {uyeAdi} başarıyla eklendi.");
    }



    static void UyeleriListele()
    {
        Console.WriteLine("Üyeler:");

        foreach (var uye in uyeler)
        {
            Console.WriteLine(uye); // foreach döngüsü ile üyelerin içindeki üye isimlerini listeler.
        }
    }

    static void UyeSil()
    {
        Console.WriteLine("Üye adını ve soyadını giriniz:");
        string uyeAdi = Console.ReadLine();

        // Kullanıcıdan silme işlemini onaylamasını isteyen bir if bloğu
        Console.WriteLine($"'{uyeAdi}' isimli üyeyi silmek istediğinizden emin misiniz? (E/H)");
        string onay = Console.ReadLine();

        if (onay.ToUpper() == "E")
        {
            if (uyeler.Contains(uyeAdi))
            {
                uyeler.Remove(uyeAdi);
                Console.WriteLine($"Üye {uyeAdi} başarıyla silindi.");
            }
            else
            {
                Console.WriteLine($"Üzgünüz, '{uyeAdi}' isimli üye bulunamadı.");
            }
        }
        else
        {
            Console.WriteLine($"Üye silme işlemi iptal edildi.");
        }
    }

    static void UyeDüzenle()
    {
        Console.WriteLine("Düzenlemek istediğiniz üye adını ve soyadını giriniz:");
        string uyeAdi = Console.ReadLine();

        if (uyeler.Contains(uyeAdi))
        {
            Console.WriteLine($"Yeni adı ve soyadı giriniz:");
            string yeniAdSoyad = Console.ReadLine();

            // Diğer üye bilgilerini düzenlemek için gerekli kodları ekleyin

            // Örnek olarak, uyeler listesindeki üyenin adını güncelliyoruz
            uyeler[uyeler.IndexOf(uyeAdi)] = yeniAdSoyad;

            Console.WriteLine($"Üye {uyeAdi} başarıyla düzenlendi.");
        }
        else
        {
            Console.WriteLine($"Üzgünüz, '{uyeAdi}' isimli üye bulunamadı.");
        }
    }



    static string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");
            }
            else if (key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine(); // Enter'a basıldığında yeni satıra geç.
        return password;
    }
    static DateTime AntrenmanTarihiniSec()
    {
        Console.Write("\nAntrenmanlara başlangıç tarihini girin (GG/AA/YYYY): ");
        DateTime tarih;

        while (true)
        {
            string userInput = Console.ReadLine();
            if (DateTime.TryParseExact(userInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tarih))
            {
                return tarih;
            }
            else
            {
                Console.WriteLine("Geçersiz tarih formatı. Lütfen tekrar girin (GG/AA/YYYY): ");
            }
        }
    }
    static TimeSpan AntrenmanSaatiSec()
    {
        Console.Write("Antrenman yapmak istediğiniz saati girin (HH:mm veya hh:mm tt): ");

        while (true)
        {
            string giris = Console.ReadLine();
            if (DateTime.TryParseExact(giris, new[] { "HH:mm", "hh:mm tt" }, null, System.Globalization.DateTimeStyles.None, out var tarih))
            {
                return tarih.TimeOfDay;
            }
            else
            {
                Console.WriteLine("Geçersiz saat formatı. Lütfen tekrar girin (HH:mm veya hh:mm tt): ");
            }
        }
    }
    static string AntrenmanHocasiSec()
    {
        Console.WriteLine("\nLütfen aşağıdaki antrenman hocalarından birini seçiniz:");
        Console.WriteLine("1. Emine Baki");
        Console.WriteLine("2. Semih Dursun");
        Console.WriteLine("3. Hira Yaşar");
        Console.WriteLine("4. Zümra Yıldız");
        Console.WriteLine("5. Yusuf Can");
        Console.WriteLine("6. Mehmet Yılmaz");

        string secim = Console.ReadLine();

        switch (secim)
        {
            case "1":
                Console.WriteLine("Eğitmeniniz: Emine Baki");
                break;
            case "2":
                Console.WriteLine("Eğitmeniniz: Semih Dursun");
                break;
            case "3":
                Console.WriteLine("Eğitmeniniz: Hira Yaşar");
                break;
            case "4":
                Console.WriteLine("Eğitmeniniz: Zümra Yıldız");
                break;
            case "5":
                Console.WriteLine("Eğitmeniniz: Yusuf Can");
                break;
            case "6":
                Console.WriteLine("Eğitmeniniz: Mehmet Yılmaz");
                break;
            default:
                Console.WriteLine("Geçersiz seçim. Varsayılan olarak Emine Baki seçildi.");
                secim = "1"; // Varsayılan olarak 1 değeri atanır.
                break;
        }

        return secim;
    }
    static string AylıkUcretiSec()
    {
        string secim;

        do
        {
            Console.WriteLine("\nLütfen aşağıdaki aylık paketlerden birini seçiniz:");
            Console.WriteLine("1. Haftanın her günü paketi: 2000 TL ");
            Console.WriteLine("2. Pazartesi-Salı Grubu: 1200 TL");
            Console.WriteLine("3. Salı-Çarşamba Grubu: 1200 TL");
            Console.WriteLine("4. Çarşamba-Perşembe Grubu: 1200 TL");
            Console.WriteLine("5. Perşembe-Cuma Grubu: 1200 TL");
            Console.WriteLine("6. Haftasonu Grubu: 1600 TL");

            secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    Console.WriteLine("Haftanın her günü paketine üyesiniz.");
                    break;
                case "2":
                    Console.WriteLine("Pazartesi-Salı Grubu'na üyesiniz.");
                    break;
                case "3":
                    Console.WriteLine("Salı-Çarşamba Grubu'na üyesiniz.");
                    break;
                case "4":
                    Console.WriteLine("Çarşamba-Perşembe Grubu'na üyesiniz.");
                    break;
                case "5":
                    Console.WriteLine("Perşembe-Cuma Grubu'na üyesiniz.");
                    break;
                case "6":
                    Console.WriteLine("Haftasonu Grubu'na üyesiniz.");
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen bir grubu seçiniz.");
                    break;
            }
        } while (secim != "1" && secim != "2" && secim != "3" && secim != "4" && secim != "5" && secim != "6");

        return secim;
    }



    //üye  bilgileri düzenleme, kilo yas değererini int al,kullanıcı aadı ve şifre al admin sayfası,süslü bir giriş ekranı //pararlells desktop  
    static Kullanici KullaniciBilgileriniAl()
    {
        string ad;
        bool isAdValid = false;

        do
        {
            Console.Write("Adınızı girin: ");
            ad = Console.ReadLine();

            // Kullanıcının girdiği değerin bir sayı olup olmadığını kontrol et
            if (!int.TryParse(ad, out _))
            {
                isAdValid = true;
            }
            else
            {
                Console.WriteLine("Geçersiz değer! Lütfen sadece harf kullanın.");
            }

        } while (!isAdValid);

        Console.WriteLine("Merhaba, " + ad + "! Hoş geldiniz.");

        string cinsiyet;

        do
        {
            Console.Write("Cinsiyetinizi girin (Erkek/Kadın): ");
            cinsiyet = Console.ReadLine();

            // Kullanıcının girdiği değeri kontrol et
            if (cinsiyet.ToLower() == "erkek" || cinsiyet.ToLower() == "kadın")
            {
                Console.WriteLine("Cinsiyet geçerli.");
            }
            else
            {
                Console.WriteLine("Geçersiz değer! Lütfen 'Erkek' veya 'Kadın' girin.");
            }

        } while (!(cinsiyet.ToLower() == "erkek" || cinsiyet.ToLower() == "kadın"));

        Console.WriteLine("Cinsiyetiniz: " + cinsiyet);
        int yas;

        // Yaş girişi
        do
        {
            Console.Write("Yaşınızı girin: ");
            string yasInput = Console.ReadLine();

            // Kullanıcının girdiği yaşın bir tam sayı olup olmadığını kontrol et
            if (int.TryParse(yasInput, out yas))
            {
                Console.WriteLine("Yaş geçerli.");
                break; // Döngüyü sonlandır
            }
            else
            {
                Console.WriteLine("Geçersiz değer! Lütfen bir tam sayı girin.");
            }

        } while (true);

        Console.WriteLine("Yaşınız: " + yas);

        int kilo;

        // Kilo girişi
        do
        {
            Console.Write("Kilonuzu kilogram cinsinden girin: ");
            string kiloInput = Console.ReadLine();

            // Kullanıcının girdiği kilonun bir tam sayı olup olmadığını kontrol et
            if (int.TryParse(kiloInput, out kilo))
            {
                Console.WriteLine("Kilo geçerli.");
                break; // Döngüyü sonlandır
            }
            else
            {
                Console.WriteLine("Geçersiz değer! Lütfen bir tam sayı girin.");
            }

        } while (true);

        Console.WriteLine("Kilonuz: " + kilo + " kg");

        int boy;

        // Boy girişi
        do
        {
            Console.Write("Boyunuzu metre cinsinden girin: ");
            string boyInput = Console.ReadLine();

            // Kullanıcının girdiği boyun bir tam sayı olup olmadığını kontrol et
            if (int.TryParse(boyInput, out boy))
            {
                Console.WriteLine("Boy geçerli.");
                break; // Döngüyü sonlandır
            }
            else
            {
                Console.WriteLine("Geçersiz değer! Lütfen bir tam sayı girin.");
            }

        } while (true);

        Console.WriteLine("Boyunuz: " + boy + " metre");

        // VKI metodunu çağır ve değerleri ileterek VKI aralığını ekrana yazdır
        VKI(HesaplaVKI(kilo, boy));

        static void VKI(double vki)
        {
            // VKI aralıklarını belirle
            if (vki < 18.5)
                Console.WriteLine("Vücut Kitle Endeksiniz: Zayıf");
            else if (vki < 24.9)
                Console.WriteLine("Vücut Kitle Endeksiniz: Normal");
            else if (vki < 29.9)
                Console.WriteLine("Vücut Kitle Endeksiniz: Fazla Kilolu");
            else if (vki < 34.9)
                Console.WriteLine("Vücut Kitle Endeksiniz: Şişman (Obezite - Sınıf I)");
            else if (vki < 39.9)
                Console.WriteLine("Vücut Kitle Endeksiniz: Şişman (Obezite - Sınıf II)");
            else
                Console.WriteLine("Vücut Kitle Endeksiniz: Çok Şişman (Aşırı Obezite - Sınıf III)");
        }

        static double HesaplaVKI(double kilo, double boy)
        {
            // VKI formülü
            double vki = kilo / (boy * boy);

            // VKI değerini ekrana yazdırır.
            Console.WriteLine($"Hesaplanan VKI: {vki}");

            // VKI değerini geri döndürür.
            return vki;
        }



        // Günlük adım sayısını al
        int adimSayisi;

        // Adım sayısı girişi
        do
        {
            Console.Write("Günlük adım sayınızı girin: ");
            string adimSayisiInput = Console.ReadLine();

            // Kullanıcının girdiği adım sayısının bir tam sayı olup olmadığını kontrol et
            if (int.TryParse(adimSayisiInput, out adimSayisi))
            {
                Console.WriteLine("Adım sayısı geçerli.");
                break; // Döngüyü sonlandır
            }
            else
            {
                Console.WriteLine("Geçersiz değer! Lütfen bir tam sayı girin.");
            }

        } while (true);

        Console.WriteLine("Günlük adım sayınız: " + adimSayisi);

        // Günlük adım sayısına göre değerlendirme yap
        if (adimSayisi < 5000)
        {
            Console.WriteLine("Adım sayınız oldukça az");
        }
        else if (adimSayisi < 10000 && adimSayisi >= 5000)
        {
            Console.WriteLine("Daha fazla adım atmalısınız");
        }
        else
        {
            Console.WriteLine("Böyle devam edin.");
        }
        Console.WriteLine("*************************");

        return new Kullanici(ad, cinsiyet, yas, kilo, boy, adimSayisi);
    }

    static IEgzersizProgrami EgzersizProgramiSec()
    {
        while (true)
        {
            Console.WriteLine("\nLütfen aşağıdaki egzersiz programlarından birini seçin:");
            Console.WriteLine("1. Güç Antrenmanı");
            Console.WriteLine("2. Kardiyo Antrenmanı");
            Console.WriteLine("3. Esneklik Antrenmanı");
            Console.WriteLine("4.Uzun Koşu Antremanı");
            Console.WriteLine("5.Plates");
            Console.WriteLine("6.Tempo Antrenmanı");
            Console.WriteLine("7.Anaerobik Fitness");
            Console.WriteLine("8.Dayanıklılık Antrenmanı");

            string secimStr = Console.ReadLine();

            if (int.TryParse(secimStr, out int secim) && secim >= 1 && secim <= 8)
            {
                switch (secim)
                {
                    case 1:
                        return new GucAntrenmani();
                    case 2:
                        return new KardiyovaskulerAntrenman();
                    case 3:
                        return new EsneklikAntrenmani();
                    case 4:
                        return new UzunKosuAntrenmani();
                    case 5:
                        return new Pilates();
                    case 6:
                        return new TempoAntrenmani();
                    case 7:
                        return new AneorabikFitness();
                    case 8:
                        return new DayaniklilikAntrenmani();
                }
            }

            Console.WriteLine("Geçersiz seçim. Lütfen 1 ile 8 arasında geçerli bir değer girin.");
        }
    }

}

interface IEgzersizProgrami  // Bir sınıf sadece bir sınıftan türetilebiliyorken birden fazla arayüzden türetilebilir.
{
    string GetProgram();
}

class GucAntrenmani : IEgzersizProgrami  //GucAntrenmanini IEgzersizProgramindan miras aldık,yani IEgzersizProgrami içinde olan her şeyi GucAntrenmani da kapsayacak..
{
    public string GetProgram()
    {
        return "Güç Antrenmanı:\n1. Bench Press\n2. Squat\n3. Deadlift";
    }
}

class KardiyovaskulerAntrenman : IEgzersizProgrami
{
    public string GetProgram()
    {
        return "Kardiyo Antrenmanı:\n1. Koşu\n2. Bisiklet Sürme\n3. Yüzme";
    }
}

class EsneklikAntrenmani : IEgzersizProgrami
{
    public string GetProgram()
    {
        return "Esneklik Antrenmanı:\n1. Yoga\n2. Pilates\n3. Esneme Egzersizleri";
    }
}

class UzunKosuAntrenmani : IEgzersizProgrami
{
    public string GetProgram()
    {
        return "Uzun Koşu Antrenmanı:\n1.Koşu Bandı\n2.Pilates\n3.Bisiklet";
    }
}

class Pilates : IEgzersizProgrami
{
    public string GetProgram()
    {
        return "Pilates:\n1. Yoga\n2. Pilates\n3. Esneme Egzersizleri";
    }
}

class TempoAntrenmani : IEgzersizProgrami
{
    public string GetProgram()
    {
        return "Tempo Antrenmanı :\n1.Bisiklet \n2. Pilates\n3. Koşu";
    }
}

class AneorabikFitness : IEgzersizProgrami
{
    public string GetProgram()
    {
        return "Aneorabik Fitness :\n1.Yoga\n2. Pilates\n3. Ağırlık Kaldırma";
    }
}

class DayaniklilikAntrenmani : IEgzersizProgrami
{
    public string GetProgram()
    {
        return "Dayanıklılık Antrenmanı :\n1.Yüzme\n2. Kardiyo\n3. Ağırlık Kaldırma";
    }
}



class Kullanici
{
    public string Ad { get; }
    public string Cinsiyet { get; }
    public int Yas { get; }
    public double Kilo { get; }
    public double Boy { get; }
    public int GunlukAdimSayisi { get; set; }

    public Kullanici(string ad, string cinsiyet, int yas, double kilo, double boy, int gunlukAdimSayisi)
    {
        Ad = ad;
        Cinsiyet = cinsiyet;
        Yas = yas;
        Kilo = kilo;
        Boy = boy;
        GunlukAdimSayisi = gunlukAdimSayisi;
    }
}


