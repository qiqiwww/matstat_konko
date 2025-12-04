using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        //Файл з файлами
        const string firstFile = "firstFile.txt";

        if (!File.Exists(firstFile))
        {
            Console.WriteLine("Файл firstFile.txt не знайдено!");
            return;
        }

        //Зчитування списку файлів
        string[] fileList = File.ReadAllLines(firstFile);

        if (fileList.Length == 0)
        {
            Console.WriteLine("У файлі firstFile.txt немає шляхів до текстів.");
            return;
        }

        //Цикл для аналізу файлів
        while (true)
        {
            Console.WriteLine("ДОСТУПНІ ТЕКСТОВІ ФАЙЛИ");
            for (int i = 0; i < fileList.Length; i++)
                Console.WriteLine($"{i} — {fileList[i]}");

            Console.Write("Виберіть файл за індексом: ");
            int index;

            while (!int.TryParse(Console.ReadLine(), out index) ||
                   index < 0 || index >= fileList.Length)
            {
                Console.Write("Невірне значення. Спробуйте ще раз: ");
            }

            string chosenFile = fileList[index];

            if (!File.Exists(chosenFile))
            {
                Console.WriteLine($"Файл {chosenFile} не знайдено!");
                continue;
            }

            Console.WriteLine($"\nАналіз файлу: {chosenFile}");

            string text = File.ReadAllText(chosenFile);

            //Приведення до нижнього регістру та видалення пунктуації
            char[] separators =
            {
                ' ', '\n', '\r', '\t', ',', '.', '!', '?', ':', ';', '-', '—', '(', ')', '\"', '\'', '[', ']', '{', '}'
            };

            var words = text
                .ToLower()
                .Split(separators, StringSplitOptions.RemoveEmptyEntries);

            //Підрахунок слів
            Dictionary<string, int> stats = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (!stats.ContainsKey(word))
                    stats[word] = 0;
                stats[word]++;
            }

            //Вивід результату
            Console.WriteLine("\nСтатистика");

            foreach (var entry in stats.OrderByDescending(x => x.Value))
                Console.WriteLine($"{entry.Key} : {entry.Value}");

            //Збереження у файл
            Console.Write("\nЗберегти результат у файл? (y/n): ");
            string save = Console.ReadLine().Trim().ToLower();

            if (save == "y")
            {
                string outFile = $"stat_{Path.GetFileNameWithoutExtension(chosenFile)}.txt";

                using StreamWriter sw = new StreamWriter(outFile);

                foreach (var entry in stats.OrderByDescending(x => x.Value))
                    sw.WriteLine($"{entry.Key} : {entry.Value}");

                Console.WriteLine($"Статистику збережено в файл: {outFile}");
            }

            //Чи продовжувати далі
            Console.Write("\nАналізувати інші файли? (y/n): ");
            string cont = Console.ReadLine().Trim().ToLower();

            if (cont != "y")
            {
                Console.WriteLine("Роботу завершено.");
                break;
            }
        }
    }
}

