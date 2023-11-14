// РАБОТАЕТ И С ОРИЕНТИРОВАННЫМИ ГРАФАМИ

using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    // Класс вершины
    // Содержит имя вершины и список её соседей
    class Ver
    {
        public string name;
        public List<Ver> neighs;
        public Ver(string name_)
        {
            name = name_;
            neighs = new List<Ver>();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // Ввод вершин в общий их список
            Console.WriteLine("Введите число вершин:");
            int v = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите названия стольки вершин, каждое с новой строки:");
            List<Ver> vers = new List<Ver>();
            for (int i=0; i<v; i++) { vers.Add(new Ver(Console.ReadLine())); }
            Console.WriteLine("Для каждой из этих вершин введите через пробел всех её соседей,");
            // И определение их соседей
            foreach (Ver now in vers)
            {
                Console.WriteLine($"Для {now.name}: ");
                var ns = Console.ReadLine().Split();
                for (int i=0; i<ns.Length; i++)
                {
                    foreach (Ver a in vers)
                    {
                        if (a.name == ns[i])
                        {
                            now.neighs.Add(a);
                        }
                    }
                }
            }

            // Вывод исходного графа (для удобства понимания)
            Console.WriteLine("\nГраф введён:");
            foreach (Ver now in vers)
            {
                string names = "";
                foreach (Ver nei in now.neighs)
                {
                    names += $"{nei.name} ";
                }
                Console.WriteLine($"Из {now.name} можно пройти в: {names}");
            }

            // Обработка лишних вершин (выполнение самого задания)
            int e = 0, everchecked = 0;
            // Перебираем вершины несколько раз
            while (everchecked == 0 || e > 0)
            {
                e = 0; everchecked = 1;
                for (int i=0; i<vers.Count; i++)
                {
                    // Если у взятой вершины 2 соседа - она лишняя,
                    if (vers[i].neighs.Count == 2)
                    {
                        // Проводим нужные манипуляции
                        e++;
                        vers[i].neighs[0].neighs.Add(vers[i].neighs[1]);
                        if (vers[i].neighs[1].neighs.Contains(vers[i])) { vers[i].neighs[1].neighs.Add(vers[i].neighs[0]); }
                        foreach (Ver a in vers)
                        {
                            if (a.neighs.Contains(vers[i])) { a.neighs.Remove(vers[i]); }
                        }
                        vers.RemoveAt(i);
                    }
                }
            }

            // Вывод полученного графа
            Console.WriteLine("\nИзменённый граф:");
            foreach (Ver now in vers)
            {
                string names = "";
                foreach (Ver nei in now.neighs)
                {
                    names += $"{nei.name} ";
                }
                Console.WriteLine($"Из {now.name} можно пройти в: {names}");
            }
        }
    }
}
