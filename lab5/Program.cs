using System;
using System.Collections.Generic;
using System.IO;

namespace lab5
{
    class Program {

        static void Main (string[] args) {
            Console.WriteLine("Task 1");
            Magazine magazine = new Magazine("QQQQQ", Frequency.Monthly, new DateTime(2020, 11, 25), 111);
            List<Article> articles = new List<Article>();
            articles.Add(new Article(new Person("Alex", "Ivanov", new DateTime(2001, 9, 10)), "ZZZ", 34.2));
            articles.Add(new Article(new Person("Petr", "Smirnov", new DateTime(2000, 8, 9)), "XXX", 35.6));
            articles.Add(new Article(new Person("Ivan", "Petrov", new DateTime(2002, 10, 11)), "CCC", 38.9));
            magazine.AddArticles(articles);
            Magazine copy = magazine.DeepCopy();
            Console.WriteLine("Исходный объект: "); Console.WriteLine(magazine.ToString());
            Console.WriteLine("Копия: "); Console.WriteLine(copy.ToString());
            Console.WriteLine("\nTask 2");
            Console.Write("Введите название файла: ");
            string filename = Console.ReadLine();
            FileInfo fileInfo = new FileInfo(filename);
            Magazine mag = new Magazine();
            if (fileInfo.Exists)
            {
                mag.Load(filename);
            }
            else
            {
                Console.WriteLine("Данного файла не существует. Создаем...");
                fileInfo.Create();
            }
            Console.WriteLine("\nTask 3");
            Console.WriteLine(mag.ToString());
            Console.WriteLine("\nTask 4");
            mag.AddFromConsole();
            mag.Save(filename);
            Console.WriteLine(mag.ToString());
            Console.WriteLine("\nTask 5");
            Magazine.Load(filename, ref mag);
            mag.AddFromConsole();
            Magazine.Save(filename, mag);
            Console.WriteLine("\nTask 6");
            Console.WriteLine(mag.ToString());
        }
    }
}
