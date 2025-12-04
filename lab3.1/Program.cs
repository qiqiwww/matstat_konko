using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Firma
{
    public string Nazva { get; set; }
    public DateTime DataZasnuvannya { get; set; }
    public string ProfilBiznesu { get; set; }
    public string PIBDirektora { get; set; }
    public int KilkistSpivrobitnykiv { get; set; }
    public string Adresa { get; set; }

    public Firma(string nazva, DateTime dataZasnuvannya, string profil,
                 string pib, int kilkist, string adresa)
    {
        Nazva = nazva;
        DataZasnuvannya = dataZasnuvannya;
        ProfilBiznesu = profil;
        PIBDirektora = pib;
        KilkistSpivrobitnykiv = kilkist;
        Adresa = adresa;
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        List<Firma> firmy = new List<Firma>()
        {
            new Firma("FoodMarket", new DateTime(2018, 5, 10), "Маркетинг", "John White", 120, "London"),
            new Firma("WhiteTech", new DateTime(2023, 1, 20), "IT", "Mark Black", 85, "New York"),
            new Firma("FoodLine", new DateTime(2020, 7, 1), "Продажі", "Bob Green", 40, "London"),
            new Firma("MegaIT", new DateTime(2019, 3, 15), "IT", "Alice White", 250, "Berlin"),
            new Firma("MarketingPro", new DateTime(2021, 11, 10), "Маркетинг", "Steve Black", 310, "Paris"),
            new Firma("WhiteFoodCompany", new DateTime(2017, 2, 18), "Маркетинг", "Tom Black", 150, "London"),
        };

    
        //Запити


        Console.WriteLine("Всі фірми ");
        Vyvesty(firmy);

        Console.WriteLine("\nФірми з назвою Food ");
        Vyvesty(firmy.Where(f => f.Nazva.Contains("Food", StringComparison.OrdinalIgnoreCase)));

        Console.WriteLine("\nФірми у галузі маркетингу ");
        Vyvesty(firmy.Where(f => f.ProfilBiznesu.Equals("Маркетинг", StringComparison.OrdinalIgnoreCase)));

        Console.WriteLine("\nМаркетинг або IT ");
        Vyvesty(firmy.Where(f =>
            f.ProfilBiznesu.Equals("Маркетинг", StringComparison.OrdinalIgnoreCase) ||
            f.ProfilBiznesu.Equals("IT", StringComparison.OrdinalIgnoreCase)
        ));

        Console.WriteLine("\nКількість співробітників > 100 ");
        Vyvesty(firmy.Where(f => f.KilkistSpivrobitnykiv > 100));

        Console.WriteLine("\nКількість співробітників 100–300 ");
        Vyvesty(firmy.Where(f => f.KilkistSpivrobitnykiv >= 100 && f.KilkistSpivrobitnykiv <= 300));

        Console.WriteLine("\nФірми, що знаходяться у Лондоні ");
        Vyvesty(firmy.Where(f => f.Adresa.Equals("London", StringComparison.OrdinalIgnoreCase)));

        Console.WriteLine("\nФірми, у яких прізвище директора White ");
        Vyvesty(firmy.Where(f => f.PIBDirektora.Contains("White")));

        Console.WriteLine("\nФірми, засновані понад 2 роки тому ");
        Vyvesty(firmy.Where(f => (DateTime.Now - f.DataZasnuvannya).TotalDays > 365 * 2));

        Console.WriteLine("\nФірми, з дня заснування яких минуло більше 150 днів ");
        Vyvesty(firmy.Where(f => (DateTime.Now - f.DataZasnuvannya).TotalDays > 150));

        Console.WriteLine("\nПрізвище директора Black та назва фірми містить White ");
        Vyvesty(firmy.Where(f =>
            f.PIBDirektora.Contains("Black") &&
            f.Nazva.Contains("White", StringComparison.OrdinalIgnoreCase)
        ));
    }

    static void Vyvesty(IEnumerable<Firma> kolekciya)
    {
        foreach (var f in kolekciya)
        {
            Console.WriteLine($"Назва: {f.Nazva}");
            Console.WriteLine($"Дата заснування: {f.DataZasnuvannya:dd.MM.yyyy}");
            Console.WriteLine($"Профіль: {f.ProfilBiznesu}");
            Console.WriteLine($"ПІБ директора: {f.PIBDirektora}");
            Console.WriteLine($"Співробітників: {f.KilkistSpivrobitnykiv}");
            Console.WriteLine($"Адреса: {f.Adresa}");
            Console.WriteLine("--------------------------------");
        }
    }
}

