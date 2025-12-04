using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Telefon
{
    public string Nazva { get; set; }
    public string Virobnik { get; set; }
    public decimal Cina { get; set; }
    public DateTime DataVypusku { get; set; }

    public Telefon(string nazva, string virobnik, decimal cina, DateTime data)
    {
        Nazva = nazva;
        Virobnik = virobnik;
        Cina = cina;
        DataVypusku = data;
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        List<Telefon> telefoni = new List<Telefon>()
        {
            new Telefon("iPhone 13", "Apple", 999, new DateTime(2021, 9, 24)),
            new Telefon("iPhone 10", "Apple", 650, new DateTime(2017, 11, 3)),
            new Telefon("Galaxy S22", "Samsung", 850, new DateTime(2022, 2, 15)),
            new Telefon("Galaxy S21", "Samsung", 700, new DateTime(2021, 1, 29)),
            new Telefon("Xperia XZ2", "Sony", 450, new DateTime(2018, 4, 1)),
            new Telefon("Xperia 1 IV", "Sony", 1200, new DateTime(2022, 6, 11)),
            new Telefon("Nokia 3310", "Nokia", 60, new DateTime(2000, 9, 1)),
            new Telefon("Redmi Note 10", "Xiaomi", 299, new DateTime(2021, 3, 4)),
            new Telefon("Pixel 7", "Google", 899, new DateTime(2022, 10, 13)),
            new Telefon("Galaxy A51", "Samsung", 380, new DateTime(2020, 1, 1))
        };

        //Запити

        Console.WriteLine("1) Кількість телефонів: " + telefoni.Count);

        Console.WriteLine("2) Телефони з ціною > 100: " +
            telefoni.Count(t => t.Cina > 100));

        Console.WriteLine("3) Телефони з ціною від 400 до 700: " +
            telefoni.Count(t => t.Cina >= 400 && t.Cina <= 700));

        Console.Write("4) Введіть виробника: ");
        string vn = Console.ReadLine();
        Console.WriteLine("   Кількість телефонів виробника " + vn + ": " +
            telefoni.Count(t => t.Virobnik.Equals(vn, StringComparison.OrdinalIgnoreCase)));

        var minCina = telefoni.OrderBy(t => t.Cina).First();
        Console.WriteLine("\n5) Мінімальна ціна: " + minCina.Nazva + " — " + minCina.Cina);

        var maxCina = telefoni.OrderByDescending(t => t.Cina).First();
        Console.WriteLine("6) Максимальна ціна: " + maxCina.Nazva + " — " + maxCina.Cina);

        var naystarishyy = telefoni.OrderBy(t => t.DataVypusku).First();
        Console.WriteLine("\n7) Найстаріший телефон:");
        Vyvesty(naystarishyy);

        var naynovishyy = telefoni.OrderByDescending(t => t.DataVypusku).First();
        Console.WriteLine("\n8) Найсвіжіший телефон:");
        Vyvesty(naynovishyy);

        Console.WriteLine("\n9) Середня ціна: " + telefoni.Average(t => t.Cina));

        Console.WriteLine("\n10) П'ять найдорожчих телефонів:");
        VyvestyKol(telefoni.OrderByDescending(t => t.Cina).Take(5));

        Console.WriteLine("\n11) П'ять найдешевших телефонів:");
        VyvestyKol(telefoni.OrderBy(t => t.Cina).Take(5));

        Console.WriteLine("\n12) Три найстаріші:");
        VyvestyKol(telefoni.OrderBy(t => t.DataVypusku).Take(3));

        Console.WriteLine("\n13) Три найновіші:");
        VyvestyKol(telefoni.OrderByDescending(t => t.DataVypusku).Take(3));

        Console.WriteLine("\n14) Статистика за виробниками:");
        var statVirobnyky = telefoni
            .GroupBy(t => t.Virobnik)
            .Select(g => new { Virobnik = g.Key, Kilkist = g.Count() });

        foreach (var s in statVirobnyky)
            Console.WriteLine($"{s.Virobnik} – {s.Kilkist}");

        Console.WriteLine("\n15) Статистика моделей:");
        var statModeli = telefoni
            .GroupBy(t => t.Nazva)
            .Select(g => new { Model = g.Key, Kilkist = g.Count() });

        foreach (var s in statModeli)
            Console.WriteLine($"{s.Model} – {s.Kilkist}");

        Console.WriteLine("\n16) Статистика за роками випуску:");
        var statRoky = telefoni
            .GroupBy(t => t.DataVypusku.Year)
            .Select(g => new { Rik = g.Key, Kilkist = g.Count() })
            .OrderBy(g => g.Rik);

        foreach (var s in statRoky)
            Console.WriteLine($"{s.Rik} – {s.Kilkist}");
    }

    //Вивід

    static void Vyvesty(Telefon t)
    {
        Console.WriteLine($"Назва: {t.Nazva}");
        Console.WriteLine($"Виробник: {t.Virobnik}");
        Console.WriteLine($"Ціна: {t.Cina}");
        Console.WriteLine($"Дата випуску: {t.DataVypusku:dd.MM.yyyy}");
    }

    static void VyvestyKol(IEnumerable<Telefon> kol)
    {
        foreach (var t in kol)
        {
            Console.WriteLine($"[{t.Nazva}] {t.Virobnik}, {t.Cina}, {t.DataVypusku:dd.MM.yyyy}");
        }
    }
}

