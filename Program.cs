using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public decimal AlisQiymeti { get; set; }
    public decimal SatisQiymeti { get; set; }
    public int Sayi { get; set; }
}
class Program
{

    static List<Product> mehsullar = new List<Product>();
    static decimal balans = 1000;
    static bool isAdmin = false;
    static void Main()
    {
        Console.WriteLine("Xoş gəlmisiniz!!");
        Console.Write("Giriş tipini seçin (admin/satıcı): ");
        string girisTipi = Console.ReadLine();
        if (girisTipi.ToLower() == "admin")
        {
            isAdmin = true;
        }
        bool davamEdilsin = true;

        while (davamEdilsin)
        {
            Console.WriteLine("1. Məhsulları göstər");
            Console.WriteLine("2. Məhsul əlavə et");
            Console.WriteLine("3. Məhsul sat");
            Console.WriteLine("4. Balansı yoxla");
            Console.WriteLine("5. Çıxış");
            Console.Write("Əməliyyatı seçin: ");
            string secim = Console.ReadLine();
            switch (secim)
            {
                case "1":
                    MəhsullarıGöstər();
                    break;
                case "2":
                    if (isAdmin)
                    {
                        MəhsulƏlavəEt();
                    }
                    else
                    {
                        Console.WriteLine("Satıcı hesabı ilə məhsul əlavə etmək üçün icazəniz yoxdur.");
                    }
                    break;
                case "3":
                    MəhsulSat();
                    break;
                case "4":
                    BalansıYoxla();
                    break;
                case "5":
                    Console.WriteLine("Təşəkkür edirik ki, Market tətbiqini istifadə etdiyiniz üçün!");
                    davamEdilsin = false;
                    break;
                default:
                    Console.WriteLine("Yanlış seçim. Zəhmət olmasa düzgün bir əməliyyatı seçin.");
                    break;
            }
        }
    }
    static void MəhsullarıGöstər()
    {
        Console.WriteLine("Mövcud məhsullar:");
        foreach (Product məhsul in mehsullar)
        {
            Console.WriteLine($"Ad: {məhsul.Name}, Alış qiyməti: {məhsul.AlisQiymeti:C}, Satis qiymeti: {məhsul.SatisQiymeti:C}, Sayı: {məhsul.Sayi}");
        }
    }
    static void MəhsulƏlavəEt()
    {
        Product yeniMəhsul = new Product();
        Console.Write("Məhsulun adını daxil edin: ");
        yeniMəhsul.Name = Console.ReadLine();
        Console.Write("Alış qiymətini daxil edin: ");
        yeniMəhsul.AlisQiymeti = decimal.Parse(Console.ReadLine());
        Console.Write("Satis qiymətini daxil edin: ");
        yeniMəhsul.SatisQiymeti = decimal.Parse(Console.ReadLine());
        Console.Write("Sayını daxil edin: ");
        yeniMəhsul.Sayi = int.Parse(Console.ReadLine());
        mehsullar.Add(yeniMəhsul);
        Console.WriteLine($"{yeniMəhsul.Name} məhsulu əlavə edildi.");
    }

    static void MəhsulSat()
    {
        MəhsullarıGöstər();
        Console.Write("Satmaq istədiyiniz məhsulun adını daxil edin: ");
        string satilanMəhsulAdi = Console.ReadLine();
        Product satilanMəhsul = mehsullar.Find(m => m.Name == satilanMəhsulAdi);
        if (satilanMəhsul != null)
        {
            Console.Write("Satış sayını daxil edin: ");
            int satışSayi = int.Parse(Console.ReadLine());
            if (satışSayi <= 0)
            {
                Console.WriteLine("Satış sayı 0-dan kiçik ola bilməz.");
            }
            else if (satışSayi > satilanMəhsul.Sayi)
            {
                Console.WriteLine($"Anbarda {satilanMəhsul.Sayi} ədəd {satilanMəhsul.Name} mövcuddur. Daxil etdiyiniz say anbardakı saydan çoxdur.");
            }
            else
            {
                decimal satisQiymeti = satilanMəhsul.SatisQiymeti * satışSayi;

                if (satisQiymeti <= balans)
                {
                    balans -= satisQiymeti;
                    satilanMəhsul.Sayi -= satışSayi;
                    if (satilanMəhsul.Sayi == 0)
                    {
                        mehsullar.Remove(satilanMəhsul);
                    }

                    Console.WriteLine($"{satışSayi} {satilanMəhsul.Name} satıldı. Qalıq balans: {balans:C}");
                }
                else
                {
                    Console.WriteLine("Balansınız kifayət qədər deyil.");
                }
            }
        }
        else
        {
            Console.WriteLine("Belə bir məhsul mövcud deyil.");
        }
    }
    static void BalansıYoxla()
    {
        Console.WriteLine($"Balansınız: {balans:C}");
    }
}