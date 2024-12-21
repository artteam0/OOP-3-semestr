using System;
using System.ComponentModel;

namespace lab07
{
    public interface Obob<T>
    {
        void Add(T element);
        void Remove(T element);
        void Display();
        bool IsContains(T element);
    }
    public static class Open
    {
        public static void ReadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception("Файла нет");
            }
            string a = File.ReadAllText(fileName);
            Console.WriteLine(a);
        }
    }
    public class Flower : Plant
    {
        public int price;
        public override string ToString()
        {
            return $"Цветок: {name}, цена: {price}";
        }
    }
    public class Bush : Plant
    {
        public string color;
        public override string ToString()
        {
            return $"Куст: {name}, цвет: {color}";
        }
    }
    public class Plant : IComparable<Plant>
    {
        public string name;
        public virtual string ToString()
        {
            return $"Растение: {name}";
        }
        public int CompareTo(Plant other)
        {
            return name.CompareTo(other.name);
        }
    }
    public class CollectionType<T> : Obob<T> where T : Plant, IComparable<T>
    {
        private SortedSet<T> set = new SortedSet<T> ();
        public string ToString(SortedSet<T> set)
        {
            string a = "";
            foreach (T element in set)
            {
                a += element.ToString() + "\n";
            }
            return a;
        }
        public void Add(T element)
        {
            set.Add(element);
        }
        public void Remove(T element)
        {
            try
            {
                if (!set.Contains(element))
                {
                    throw new Exception("Нет такого элемента");
                }
                set.Remove(element);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Удалено");
            }
        }
        public bool IsContains(T element)
        {
            return set.Contains(element);
        }
        public void Display()
        {
            foreach (T t in set)
            {
                Console.WriteLine(t.ToString());
            }
        }
        public void Save(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    File.Create(filename);
                }
                File.WriteAllText(filename, ToString(set));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }
    }
    public class Program
    {
        public static void Main(string[] argc)
        {
            CollectionType<Bush> flower_1 = new CollectionType<Bush>();
            CollectionType<Flower> flower_2 = new CollectionType<Flower>();
            flower_1.Add(new Bush { name = "Куст", color = "green" });
            flower_1.Add(new Bush { name = "Bush", color = "green" });
            flower_2.Add(new Flower { name = "Куст", price = 10 });
            flower_2.Add(new Flower { name = "Bush", price = 20 });
            flower_1.Display();
            flower_2.Display();
            flower_1.Save("lab07.txt");
            flower_2.Save("lab07.txt");

            Open.ReadFromFile("lab07.txt");




            //Set set1 = new Set(6);
            //Set set2 = new Set(6);

            //set1.Add(0);
            //set1.Add(1);
            //set1.Add(2);
            //set1.Add(3);
            //set1.Add(4);
            //set1.Add(6);
            //set2.Add(6);
            //set2.Add(7);
            //set2.Add(8);
            //set2.Add(9);
            //set2.Add(10);
            //set2.Add(0);

            //set1.Zero();
            //set2.Zero();
            //Console.WriteLine("Множество 1:");
            //set1.Display();
            //Console.WriteLine("Множество 2:");
            //set2.Display();

            //Set intersection = set1 * set2;
            //Console.WriteLine("Пересечение множеств:");
            //intersection.Display();

            //Set union = set1 & set2;
            //Console.WriteLine("Объединение множеств:");
            //union.Display();

            //Set del = set2 - 6;
            //Console.WriteLine("Множество 2 после удаления элемента 3:");
            //del.Display();

            //Console.WriteLine($"Сумма элементов множества 1: {StatisticOperation.Sum(set1)}");
            //Console.WriteLine($"Сумма элементов множества 2: {StatisticOperation.Sum(set2)}");
            //Console.WriteLine($"Разница между максимальным и минимальным элементами множества 1: {StatisticOperation.Difference(set1)}");
            //Console.WriteLine($"Разница между максимальным и минимальным элементами множества 2: {StatisticOperation.Difference(set2)}");
            //Console.WriteLine($"Количество элементов в множестве 1: {StatisticOperation.Count(set1)}");
            //Console.WriteLine($"Количество элементов в множестве 2: {StatisticOperation.Count(set2)}");

            //Set.Production production = new Set.Production(812, "lab03");
            //Console.WriteLine("Информация о компании:");
            //production.Display();

            //Set.Developer developer = new Set.Developer("Kuliashov A.A.", 555, "Program.cs");
            //Console.WriteLine("Информация о разработчике:");
            //developer.Display();
            //string[] str = { "Hello", "World", "Привет", "Мир" };
            //Extend.dot(str);
            //for (int i = 0; i < str.Length; i++)
            //{
            //    Console.Write(str[i]);
            //}
        }
    }
}