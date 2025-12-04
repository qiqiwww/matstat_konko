using System;
using System.Collections.Generic;
using System.Linq;

class Employer
{
    public string Pib { get; set; }
    public DateTime DataNarodzhennya { get; set; }
    public decimal Zarplata { get; set; }
    public int StazhRokiv { get; set; }
    public string Osvita { get; set; } // Вища, середня
    public string Posada { get; set; } // Президент, Менеджер, Робітник

    public Employer(string pib, DateTime dataNar, decimal zarplata, int stazh, string osvita, string posada)
    {
        Pib = pib;
        DataNarodzhennya = dataNar;
        Zarplata = zarplata;
        StazhRokiv = stazh;
        Osvita = osvita;
        Posada = posada;
    }

    public int Vik => DateTime.Now.Year - DataNarodzhennya.Year;
}

class President : Employer
{
    public President(string pib, DateTime dataNar, decimal zarplata, int stazh, string osvita)
        : base(pib, dataNar, zarplata, stazh, osvita, "Президент") { }
}

class Manager : Employer
{
    public Manager(string pib, DateTime dataNar, decimal zarplata, int stazh, string osvita)
        : base(pib, dataNar, zarplata, stazh, osvita, "Менеджер") { }
}

class Worker : Employer
{
    public Worker(string pib, DateTime dataNar, decimal zarplata, int stazh, string osvita)
        : base(pib, dataNar, zarplata, stazh, osvita, "Робітник") { }
}

class Company
{
    public string Nazva { get; set; }
    public List<Employer> Spivrobitnyky { get; set; } = new List<Employer>();

    public Company(string nazva) { Nazva = nazva; }

    // Кількість робітників
    public int KilkistRobitnykiv => Spivrobitnyky.Count(e => e.Posada == "Робітник");

    // Загальна зарплата для робітників
    public decimal SumZarplatyRobitnykiv => Spivrobitnyky
        .Where(e => e.Posada == "Робітник").Sum(e => e.Zarplata);
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Company komp = new Company("TechCorp");

        // Створюємо співробітників
        komp.Spivrobitnyky.Add(new President("Андрій Шевченко", new DateTime(1970, 5, 12), 100000, 30, "Вища"));
        komp.Spivrobitnyky.Add(new Manager("Олена Петренко", new DateTime(1985, 3, 21), 50000, 15, "Вища"));
        komp.Spivrobitnyky.Add(new Manager("Іван Коваленко", new DateTime(1990, 7, 10), 48000, 12, "Вища"));
        komp.Spivrobitnyky.Add(new Worker("Наталія Бойко", new DateTime(1995, 10, 5), 30000, 5, "Середня"));
        komp.Spivrobitnyky.Add(new Worker("Михайло Савчук", new DateTime(1992, 11, 12), 32000, 8, "Середня"));
        komp.Spivrobitnyky.Add(new Worker("Катерина Мельник", new DateTime(2000, 10, 20), 28000, 2, "Середня"));
        komp.Spivrobitnyky.Add(new Worker("Павло Олійник", new DateTime(1998, 6, 1), 29000, 4, "Вища"));
        komp.Spivrobitnyky.Add(new Worker("Олексій Ткаченко", new DateTime(1999, 12, 15), 31000, 3, "Середня"));
        komp.Spivrobitnyky.Add(new Worker("Юлія Шевчук", new DateTime(1987, 10, 8), 35000, 10, "Вища"));
        komp.Spivrobitnyky.Add(new Worker("Софія Кравченко", new DateTime(2002, 1, 5), 27000, 1, "Середня"));

        // 1. Кількість робітників
        Console.WriteLine("Кількість робітників: " + komp.KilkistRobitnykiv);

        // 2. Обсяг зарплати робітників
        Console.WriteLine("Загальна зарплата робітників: " + komp.SumZarplatyRobitnykiv);

        // 3. 10 робітників з найбільшим стажем
        var top10Stazh = komp.Spivrobitnyky
            .Where(e => e.Posada == "Робітник")
            .OrderByDescending(e => e.StazhRokiv)
            .Take(10)
            .ToList();

        var naymenshiyVikZVyschOsv = top10Stazh
            .Where(e => e.Osvita == "Вища")
            .OrderBy(e => e.Vik)
            .FirstOrDefault();

        Console.WriteLine("\nНаймолодший робітник з вищою освітою серед топ-10 за стажем:");
        if (naymenshiyVikZVyschOsv != null) Vyvesty(naymenshiyVikZVyschOsv);

        // 4. Молодий та старший менеджер
        var menyery = komp.Spivrobitnyky.Where(e => e.Posada == "Менеджер").ToList();
        var molodyyMen = menyery.OrderBy(e => e.Vik).First();
        var starshyyMen = menyery.OrderByDescending(e => e.Vik).First();

        Console.WriteLine("\nМолодший менеджер:");
        Vyvesty(molodyyMen);

        Console.WriteLine("\nСтарший менеджер:");
        Vyvesty(starshyyMen);

        // 5. Робітники, народжені у жовтні
        var robOct = komp.Spivrobitnyky
            .Where(e => e.Posada == "Робітник" && e.DataNarodzhennya.Month == 10)
            .ToList();

        Console.WriteLine("\nРобітники, народжені у жовтні:");
        foreach (var r in robOct)
        {
            Console.WriteLine($"{r.Pib} ({r.Posada})");
        }

        // 6. Володимири
        var volodymyry = komp.Spivrobitnyky
            .Where(e => e.Pib.Contains("Volodymyr"))
            .ToList();

        Console.WriteLine("\nВсі Володимири на підприємстві:");
        foreach (var v in volodymyry)
        {
            Console.WriteLine($"{v.Pib} ({v.Posada})");
        }

        // 7. Наймолодший співробітник і премія
        var naymolodshyy = komp.Spivrobitnyky.OrderBy(e => e.DataNarodzhennya).Last();
        decimal premia = naymolodshyy.Zarplata / 3;
        Console.WriteLine($"\nНаймолодший співробітник: {naymolodshyy.Pib}, премія: {premia}");
    }

    static void Vyvesty(Employer e)
    {
        Console.WriteLine($"ПІБ: {e.Pib}");
        Console.WriteLine($"Дата народження: {e.DataNarodzhennya:dd.MM.yyyy}");
        Console.WriteLine($"Посада: {e.Posada}");
        Console.WriteLine($"Зарплата: {e.Zarplata}");
        Console.WriteLine($"Стаж: {e.StazhRokiv} років");
        Console.WriteLine($"Освіта: {e.Osvita}");
    }
}

