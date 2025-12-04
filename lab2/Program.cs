using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public class Platizh
{
    public long RozRahPlat { get; set; }
    public long RozRahOtr { get; set; }
    public DateOnly Data { get; set; }
    public decimal Suma { get; set; }


    public Platizh(long rozrahplat, long rozrahotr, DateOnly data, decimal suma)
    {
        RozRahPlat = rozrahplat;
        RozRahOtr = rozrahotr;
        Data = data;
        Suma = suma;
    }

    public Platizh() { }

}

class Program
{
    private static List<Platizh> list = new List<Platizh>();
    static void Main()
    {

        Console.OutputEncoding = Encoding.UTF8;


        Platizh platizh1 = new Platizh { RozRahPlat = 253029900000, RozRahOtr = 600700000026, Data = new DateOnly(2024, 10, 5), Suma = 3245256 };
        Platizh platizh2 = new Platizh { RozRahPlat = 150987234065, RozRahOtr = 800123456789, Data = new DateOnly(2024, 3, 12), Suma = 8342655 };
        Platizh platizh3 = new Platizh { RozRahPlat = 555123456789, RozRahOtr = 482951736184, Data = new DateOnly(2024, 1, 5), Suma = 6123784 };
        Platizh platizh4 = new Platizh { RozRahPlat = 888999000111, RozRahOtr = 671244598712, Data = new DateOnly(2024, 11, 30), Suma = 8452197 };
        Platizh platizh5 = new Platizh { RozRahPlat = 482951736184, RozRahOtr = 150987234065, Data = new DateOnly(2024, 6, 15), Suma = 7215489 };
        list.Add(platizh1);
        list.Add(platizh2);
        list.Add(platizh3);
        list.Add(platizh4);
        list.Add(platizh5);


        while (true)
        {
            Console.WriteLine("1 – Додати платіж");
            Console.WriteLine("2 – Видалити платіж");
            Console.WriteLine("3 – Редагувати платіж");
            Console.WriteLine("4 – Виконати запит (дата + мінімальна сума)");
            Console.WriteLine("5 – Показати всі платежі");
            Console.WriteLine("0 – Вийти");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddPlatizh(); break;
                case "2": DeletePlatizh(); break;
                case "3": EditPlatizh(); break;
                case "4": Zapyt(); break;
                case "5": PokazatyVsi(); break;
                case "0": return;
                default: Console.WriteLine("Невірний вибір"); break;
            }
        }
    }

    static void AddPlatizh()
    {
        Console.Write("Рахунок платника");
        long rozrahplat = long.Parse(Console.ReadLine());

        Console.Write("Рахунок отримувача");
        long rozrahpotr = long.Parse(Console.ReadLine());

        Console.Write("Дата (рррр-мм-дд): ");
        DateOnly data = DateOnly.Parse(Console.ReadLine());

        Console.Write("Сума: ");
        decimal suma = decimal.Parse(Console.ReadLine());

        Platizh newPlatizh = new Platizh { RozRahPlat = rozrahplat, RozRahOtr = rozrahpotr, Data = data, Suma = suma };
        list.Add(newPlatizh);
        Console.WriteLine("Платіж додано");
    }

    static void DeletePlatizh()
    {
        Console.WriteLine("Уведіть індекс платежу для видалення");
        int index = int.Parse(Console.ReadLine());

        if (index >= 0 && index < list.Count)
        {
            Platizh vidalen = list[index];
            list.RemoveAt(index);
            Console.WriteLine("Платіж видалено");
        }
        else {
            Console.WriteLine("Неправильний індекс");
        }
    }

    static void EditPlatizh()
    {
        Console.Write("Введіть індекс для редагування: ");
        int index = int.Parse(Console.ReadLine());

        if (index < 0 || index >= list.Count)
        {
            Console.WriteLine("Неправильний індекс.");
            return;
        }

        Platizh p = list[index];

        Console.WriteLine("Enter - щоб залишити без змін.");

        Console.WriteLine($"Рахунок платника ({p.RozRahPlat}): ");
        string input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
            p.RozRahPlat = long.Parse(input);

        Console.WriteLine($"Рахунок отримувача ({p.RozRahOtr}): ");
        input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
            p.RozRahOtr = long.Parse(input);

        Console.WriteLine($"Дата ({p.Data}): ");
        input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
            p.Data = DateOnly.Parse(input);

        Console.WriteLine($"Сума ({p.Suma}): ");
        input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
            p.Suma = decimal.Parse(input);

        Console.WriteLine("Платіж змінено.");
    }

    static void Zapyt()
    {
        Console.Write("Введіть початкову дату (рррр-мм-дд): ");
        DateOnly from = DateOnly.Parse(Console.ReadLine());

        Console.Write("Введіть кінцеву дату (рррр-мм-дд): ");
        DateOnly to = DateOnly.Parse(Console.ReadLine());

        Console.Write("Мінімальна сума: ");
        decimal minSuma = decimal.Parse(Console.ReadLine());

        var result = list.Where(p => p.Data >= from && p.Data <= to && p.Suma >= minSuma);

        if (!result.Any())
        {
            Console.WriteLine("Платежів не знайдено.");
            File.WriteAllText("result.txt", "Платежів не знайдено.");
            return;
        }

        Console.WriteLine("\n--- Результат ---");

        using StreamWriter sw = new StreamWriter("result.txt");

        foreach (var p in result)
        {
            string line =
                $"Платник: {p.RozRahPlat}\n" +
                $"Отримувач: {p.RozRahOtr}\n" +
                $"Дата: {p.Data:dd.MM.yyyy}\n" +
                $"Сума: {p.Suma}\n";

            Console.WriteLine(line);
            sw.WriteLine(line);
        }

        Console.WriteLine("Результат записано у файл result.txt");
    }

    static void PokazatyVsi()
    {
        for (int i = 0; i < list.Count; i++)
        {
            var p = list[i];
            Console.WriteLine($"[{i}] {p.RozRahPlat} → {p.RozRahOtr}, {p.Data}, {p.Suma}");
        }
    }
}
    