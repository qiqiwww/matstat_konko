using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class ZapytNaDruk
{
    public string Korystuvach { get; set; }
    public string Dokument { get; set; }
    public int Priorytet { get; set; } // 1 – найвищий, 10 – найнижчий
    public DateTime ChasNadhodzhennya { get; set; }

    public ZapytNaDruk(string korystuvach, string dokument, int priorytet)
    {
        Korystuvach = korystuvach;
        Dokument = dokument;
        Priorytet = priorytet;
        ChasNadhodzhennya = DateTime.Now;
    }
}

class ZapysStatystyky
{
    public string Korystuvach { get; set; }
    public string Dokument { get; set; }
    public DateTime ChasDruku { get; set; }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Пріоритетна черга: чим менше число — тим вищий пріоритет
        List<ZapytNaDruk> chergaDruku = new List<ZapytNaDruk>();

        // Статистика
        List<ZapysStatystyky> statystyka = new List<ZapysStatystyky>();

        while (true)
        {
            Console.WriteLine("\n===== МЕНЮ =====");
            Console.WriteLine("1 — Додати запит на друк");
            Console.WriteLine("2 — Обробити наступний документ");
            Console.WriteLine("3 — Показати статистику друку");
            Console.WriteLine("4 — Зберегти статистику у файл");
            Console.WriteLine("5 — Вихід");
            Console.Write("Ваш вибір: ");

            string vibyr = Console.ReadLine();
            Console.WriteLine();

            if (vibyr == "1")
            {
                Console.Write("Введіть ім'я користувача: ");
                string korystuvach = Console.ReadLine();

                Console.Write("Введіть назву документу: ");
                string dokument = Console.ReadLine();

                Console.Write("Введіть пріоритет (1–10, де 1 — найвищий): ");
                int priorytet = int.Parse(Console.ReadLine());

                chergaDruku.Add(new ZapytNaDruk(korystuvach, dokument, priorytet));
                Console.WriteLine("Запит додано до черги!");
            }
            else if (vibyr == "2")
            {
                if (chergaDruku.Count == 0)
                {
                    Console.WriteLine("Черга порожня!");
                    continue;
                }

                // Знаходимо найвищий пріоритет
                var nastupnyy = getNextByPriority(chergaDruku);

                Console.WriteLine($"Друкується документ: {nastupnyy.Dokument}");
                Console.WriteLine($"Користувач: {nastupnyy.Korystuvach}");
                Console.WriteLine($"Пріоритет: {nastupnyy.Priorytet}");
                Console.WriteLine($"Час друку: {DateTime.Now}");

                // Запис у статистику
                statystyka.Add(new ZapysStatystyky
                {
                    Korystuvach = nastupnyy.Korystuvach,
                    Dokument = nastupnyy.Dokument,
                    ChasDruku = DateTime.Now
                });

                chergaDruku.Remove(nastupnyy);
            }
            else if (vibyr == "3")
            {
                if (statystyka.Count == 0)
                {
                    Console.WriteLine("Статистика порожня!");
                    continue;
                }

                Console.WriteLine("Статистика друку");
                foreach (var st in statystyka)
                {
                    Console.WriteLine($"{st.ChasDruku}: {st.Korystuvach} — {st.Dokument}");
                }
            }
            else if (vibyr == "4")
            {
                if (statystyka.Count == 0)
                {
                    Console.WriteLine("Статистика порожня, нічого зберігати!");
                    continue;
                }

                string imyaFaylu = "stat_druk.txt";
                using (StreamWriter sw = new StreamWriter(imyaFaylu))
                {
                    foreach (var st in statystyka)
                    {
                        sw.WriteLine($"{st.ChasDruku}: {st.Korystuvach} — {st.Dokument}");
                    }
                }

                Console.WriteLine($"Статистику збережено у файл: {imyaFaylu}");
            }
            else if (vibyr == "5")
            {
                break;
            }
            else
            {
                Console.WriteLine("Некоректний вибір!");
            }
        }
    }

    // Вибір наступного документу з найвищим пріоритетом
    static ZapytNaDruk getNextByPriority(List<ZapytNaDruk> cherga)
    {
        // Спочатку вибираємо мінімальний пріоритет
        int minP = int.MaxValue;
        foreach (var z in cherga)
        {
            if (z.Priorytet < minP)
                minP = z.Priorytet;
        }

        // Потім вибираємо найперший за часом серед однаково пріоритетних
        ZapytNaDruk best = null;
        foreach (var z in cherga)
        {
            if (z.Priorytet == minP)
            {
                if (best == null || z.ChasNadhodzhennya < best.ChasNadhodzhennya)
                    best = z;
            }
        }
        return best;
    }
}

