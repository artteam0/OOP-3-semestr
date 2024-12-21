using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace lab09
{
    public class Worker
    {
        public string Name { get; set; }
        public string Pos { get; set; }
        public double Salary { get; set; }
        public int Id { get; set; }

        public Worker(string name, string pos, double salary, int id)
        {
            Name = name;
            Pos = pos;
            Salary = salary;
            Id = id;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Pos: {Pos}, Salary: {Salary}, Id: {Id}";
        }
    }

    public class WorkerCollection : IEnumerable<Worker>
    {
        private Hashtable workers = new Hashtable();

        public void Add(Worker worker)
        {
            workers[worker.Id] = worker;
        }

        public void Remove(int id)
        {
            workers.Remove(id);
        }

        public Worker Find(int id)
        {
            return workers[id] as Worker;
        }

        public IEnumerator<Worker> GetEnumerator()
        {
            foreach (var key in workers.Keys)
            {
                yield return (Worker)workers[key];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Print()
        {
            foreach (Worker worker in this)
            {
                Console.WriteLine(worker);
            }
        }
    }

    class Program
    {
        public static void ObservableWorkers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) //action - хар
            {
                Console.WriteLine("\nЭлемент добавлен:");
                foreach (Worker newitem in e.NewItems)
                {
                    Console.WriteLine(newitem);
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                Console.WriteLine("\nЭлемент удален:");
                foreach (Worker olditem in e.OldItems)
                {
                    Console.WriteLine(olditem);
                }
            }
        }
        static void Main()
        {
            WorkerCollection workers = new WorkerCollection
            {
                new Worker("Artsiom", "Manager", 48000, 1),
                new Worker("Arseniy", "Developer", 35000, 2),
                new Worker("Kirill", "Developer", 35000, 3)
            };

            Console.WriteLine("Все работники:");
            workers.Print();

            workers.Remove(2);
            Console.WriteLine("\nПосле удаления работника с Id 2:");
            workers.Print();

            Worker foundWorker = workers.Find(3);
            Console.WriteLine($"\nНайденный работник с Id 3: {foundWorker}");

            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Console.WriteLine("Коллекция List:");
            Console.WriteLine(string.Join(", ", numbers));

            int a = 4;
            numbers.RemoveRange(3, a);
            Console.WriteLine("Коллекция List после удаления элементов:");
            Console.WriteLine(string.Join(", ", numbers));

            numbers.Add(12);
            numbers.Insert(0, 0);
            numbers.AddRange(new List<int> { 13, 14, 15, 16, 17 });
            Console.WriteLine("Коллекция List после добавления элементов:");
            Console.WriteLine(string.Join(", ", numbers));

            Dictionary<int, int> numDictionary = new Dictionary<int, int>();

            for (int i = 0; i < numbers.Count; i++) 
            {
                numDictionary[i] = numbers[i];
            }

            Console.WriteLine("Коллекция Dictionary:");
            foreach (var pair in numDictionary)
            {
                Console.WriteLine($"Ключ: {pair.Key}, Значение: {pair.Value}");
            }

            int search = 11;
            bool found = numDictionary.ContainsKey(search);
            Console.WriteLine($"Значение {search} найдено: {found}");

            ObservableCollection<Worker> observableWorkers = new ObservableCollection<Worker>();
            observableWorkers.CollectionChanged += ObservableWorkers_CollectionChanged;

            observableWorkers.Add(new Worker("Anton", "Eng", 40000, 4));
            observableWorkers.Add(new Worker("Ilya", "Scam", 5000000000, 5));
            observableWorkers.Remove(observableWorkers[0]);
        }
    }
}
