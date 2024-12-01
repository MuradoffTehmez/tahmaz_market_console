using System;
using System.Collections.Generic;
using System.Linq;

class Məhsul
{
    public string Ad { get; set; }
    public decimal AlışQiyməti { get; set; }
    public decimal SatışQiyməti { get; set; }
    public int Miqdar { get; set; }
    public int SatışSayı { get; set; } = 0; // Satış statistikası üçün
}

class Program
{
    private static readonly List<Məhsul> məhsullar = new List<Məhsul>();
    private static decimal balans = 1000m;
    private static bool adminStatusu = false;

    static void Main()
    {
        Console.WriteLine("Market Tətbiqinə Xoş Gəlmisiniz!");
        Console.Write("Hesab növünü seçin (admin/satıcı): ");
        string istifadəçiTipi = Console.ReadLine()?.ToLower();

        adminStatusu = istifadəçiTipi == "admin";

        bool davamEdilsin = true;
        while (davamEdilsin)
        {
            ƏsasMenyuGöstər();
            Console.Write("Əməliyyat seçin: ");
            string seçim = Console.ReadLine();

            switch (seçim)
            {
                case "1":
                    MəhsullarıGöstər();
                    break;
                case "2":
                    if (adminStatusu) MəhsulƏlavəEt();
                    else Console.WriteLine("Satıcı olaraq məhsul əlavə etməyə icazəniz yoxdur.");
                    break;
                case "3":
                    MəhsulSat();
                    break;
                case "4":
                    BalansıYoxla();
                    break;
                case "5":
                    SatışStatistikası();
                    break;
                case "6":
                    ƏnÇoxSatılanMəhsul();
                    break;
                case "7":
                    Console.WriteLine("Market Tətbiqindən istifadə etdiyiniz üçün təşəkkür edirik!");
                    davamEdilsin = false;
                    break;
                default:
                    Console.WriteLine("Yanlış seçim. Yenidən cəhd edin.");
                    break;
            }
        }
    }

    private static void ƏsasMenyuGöstər()
    {
        Console.WriteLine("\nƏsas Menyu:");
        Console.WriteLine("1. Məhsulları göstər");
        Console.WriteLine("2. Məhsul əlavə et");
        Console.WriteLine("3. Məhsul sat");
        Console.WriteLine("4. Balansı yoxla");
        Console.WriteLine("5. Satış statistikası");
        Console.WriteLine("6. Ən çox satılan məhsul");
        Console.WriteLine("7. Çıxış\n");
    }

    private static void MəhsullarıGöstər()
    {
        if (məhsullar.Count == 0)
        {
            Console.WriteLine("Heç bir məhsul mövcud deyil.");
            return;
        }

        Console.WriteLine("Mövcud Məhsullar:");
        foreach (var məhsul in məhsullar)
        {
            Console.WriteLine($"Ad: {məhsul.Ad}, Alış Qiyməti: {məhsul.AlışQiyməti:C}, Satış Qiyməti: {məhsul.SatışQiyməti:C}, Miqdar: {məhsul.Miqdar}");
        }
    }

    private static void MəhsulƏlavəEt()
    {
        Məhsul yeniMəhsul = new Məhsul();

        Console.Write("Məhsulun adını daxil edin: ");
        yeniMəhsul.Ad = Console.ReadLine();

        yeniMəhsul.AlışQiyməti = SorğuEdiciDecimal("Məhsulun alış qiymətini daxil edin: ");
        yeniMəhsul.SatışQiyməti = SorğuEdiciDecimal("Məhsulun satış qiymətini daxil edin: ");
        yeniMəhsul.Miqdar = SorğuEdiciTam("Məhsulun miqdarını daxil edin: ");

        məhsullar.Add(yeniMəhsul);
        Console.WriteLine($"'{yeniMəhsul.Ad}' məhsulu uğurla əlavə edildi.");
    }

    private static void MəhsulSat()
    {
        if (məhsullar.Count == 0)
        {
            Console.WriteLine("Satmaq üçün məhsul yoxdur.");
            return;
        }

        MəhsullarıGöstər();

        Console.Write("Satmaq istədiyiniz məhsulun adını daxil edin: ");
        string məhsulAdi = Console.ReadLine();
        var məhsul = məhsullar.FirstOrDefault(m => m.Ad.Equals(məhsulAdi, StringComparison.OrdinalIgnoreCase));

        if (məhsul == null)
        {
            Console.WriteLine("Daxil etdiyiniz məhsul tapılmadı.");
            return;
        }

        int satışMiqdarı = SorğuEdiciTam("Satmaq istədiyiniz miqdarı daxil edin: ");
        if (satışMiqdarı <= 0)
        {
            Console.WriteLine("Miqdar müsbət rəqəm olmalıdır.");
            return;
        }

        if (satışMiqdarı > məhsul.Miqdar)
        {
            Console.WriteLine($"Anbarda kifayət qədər məhsul yoxdur. Mövcud miqdar: {məhsul.Miqdar}");
            return;
        }

        decimal satışDəyəri = satışMiqdarı * məhsul.SatışQiyməti;
        balans += satışDəyəri;
        məhsul.Miqdar -= satışMiqdarı;
        məhsul.SatışSayı += satışMiqdarı;

        if (məhsul.Miqdar == 0)
        {
            məhsullar.Remove(məhsul);
            Console.WriteLine($"'{məhsul.Ad}' məhsulu tam satıldı və anbar siyahısından çıxarıldı.");
        }

        Console.WriteLine($"Satış tamamlandı: {məhsul.Ad}, Miqdar: {satışMiqdarı}, Ümumi dəyər: {satışDəyəri:C}, Yenilənmiş balans: {balans:C}");
    }

    private static void BalansıYoxla()
    {
        Console.WriteLine($"Cari balans: {balans:C}");
    }

    private static void SatışStatistikası()
    {
        if (məhsullar.Count == 0)
        {
            Console.WriteLine("Hələ ki, heç bir məhsul satılmayıb.");
            return;
        }

        Console.WriteLine("Satış Statistikası:");
        foreach (var məhsul in məhsullar.Where(m => m.SatışSayı > 0))
        {
            Console.WriteLine($"Ad: {məhsul.Ad}, Satış Sayı: {məhsul.SatışSayı}");
        }
    }

    private static void ƏnÇoxSatılanMəhsul()
    {
        if (məhsullar.Count == 0)
        {
            Console.WriteLine("Satış üçün məhsul yoxdur.");
            return;
        }

        var ənÇoxSatılan = məhsullar.OrderByDescending(m => m.SatışSayı).FirstOrDefault();
        if (ənÇoxSatılan != null && ənÇoxSatılan.SatışSayı > 0)
        {
            Console.WriteLine($"Ən çox satılan məhsul: {ənÇoxSatılan.Ad}, Satış Sayı: {ənÇoxSatılan.SatışSayı}");
        }
        else
        {
            Console.WriteLine("Hələ ki, heç bir məhsul satılmayıb.");
        }
    }

    private static decimal SorğuEdiciDecimal(string mesaj)
    {
        decimal dəyər;
        while (true)
        {
            Console.Write(mesaj);
            if (decimal.TryParse(Console.ReadLine(), out dəyər)) return dəyər;
            Console.WriteLine("Düzgün bir rəqəm daxil edin.");
        }
    }

    private static int SorğuEdiciTam(string mesaj)
    {
        int dəyər;
        while (true)
        {
            Console.Write(mesaj);
            if (int.TryParse(Console.ReadLine(), out dəyər)) return dəyər;
            Console.WriteLine("Düzgün bir tam rəqəm daxil edin.");
        }
    }
}
