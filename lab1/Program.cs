using System;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

public class Platizh
{
    public long RozRahPlat {  get; set; }
    public long RozRahOtr {  get; set; }
    public DateOnly Data {  get; set; }
    public decimal Suma {  get; set; }


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
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Platizh[] platezhi = new Platizh[] {
            new Platizh(253029900000, 600700000026, new DateOnly(2024,10,5), 3245256),
            new Platizh(150987234065, 800123456789, new DateOnly(2024,3,12), 8342655),
            new Platizh(555123456789, 482951736184, new DateOnly(2024,1,5), 6123784),
            new Platizh(888999000111, 671244598712, new DateOnly(2024,11,30), 8452197),
            new Platizh(482951736184, 150987234065, new DateOnly(2024,6,15), 7215489),

        };



        //Серіалізація та Десеріалізація json
        string json = JsonSerializer.Serialize(platezhi);
        File.WriteAllText("platezhi.json", json);
        Platizh[] platezhijson = JsonSerializer.Deserialize<Platizh[]>(File.ReadAllText("platezhi.json"));



        //Серіалізація та Десеріалізація xml
        XmlSerializer xmlSerializer= new XmlSerializer(typeof(Platizh[]));
        using (FileStream fs = new FileStream("platezhi.xml", FileMode.Create))
        {
            xmlSerializer.Serialize(fs, platezhi);
        }
        Platizh[] platezhixml;
        using(FileStream fs = new FileStream("platezhi.xml", FileMode.Open))
        {
            platezhixml=(Platizh[])xmlSerializer.Deserialize(fs);
        }


        Console.WriteLine("Уведіть початкову та кінцеву дату: ");
        DateOnly VidChisla = DateOnly.Parse(Console.ReadLine());
        DateOnly DoChisla = DateOnly.Parse(Console.ReadLine());

        Console.WriteLine("Уведіть мінімальну сумму перерахунку коштів");
        Decimal minSuma = Decimal.Parse(Console.ReadLine());

        bool znaydeno=false;

        foreach (var platizh in platezhi)
        {
            if (platizh.Data >= VidChisla && platizh.Data <= DoChisla && platizh.Suma >= minSuma)
            {
                Console.WriteLine($"Рахунок платника: {platizh.RozRahPlat}");
                Console.WriteLine($"Рахунок отримувача: {platizh.RozRahOtr}");
                Console.WriteLine($"Дата: {platizh.Data:dd.MM.yyyy}");
                Console.WriteLine($"Сума: {platizh.Suma:C}\n");
                znaydeno = true;
            } 
        }
            
        if(!znaydeno)
            {
                Console.WriteLine("Відповідних платежів не знайдено");
            }
        

    }
}