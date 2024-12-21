using System;
using System.ComponentModel;

public static class Extend
{
    public static void dot(this string[] str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            str[i] += '.';
        }
    }
    public static void Zero(this Set set)
    {
        Set newSet = new Set(set.Length);
        for (int i = 0; i < set.Length; i++)
        {
            if (set.elements[i] != 0)
            {
                newSet.Add(set.elements[i]);
            }
        }
        set.elements = newSet.elements;
        set.Length = newSet.Length;
    }
}

public class Set
{
    public int[] elements;
    public int Length;

    public class Production
    {
        int Id;
        string OrgName;
        public Production(int id, string orgName)
        {
            Id = id;
            OrgName = orgName;
        }
        public void Display()
        {
            Console.WriteLine($"ID компании: {Id}, Организация: {OrgName}");
        }
    }
    public class Developer
    {
        string Name;
        int Id;
        string OrgName;
        public Developer(string name, int id, string orgName)
        {
            Name = name;
            Id = id;
            OrgName = orgName;
        }
        public void Display()
        {
            Console.WriteLine($"Разработчик: {Name}, ID: {Id}, Отдел: {OrgName}");
        }
    }
    public Set(int size)
    {
        elements = new int[size];
        Length = 0;
    }
    public void Add(int element)
    {
        if (Length < elements.Length)
        {
            elements[Length] = element;
            Length++;
        }
    }
    public int Element(int index)
    {
        return elements[index];
    }
    public bool Contains(int element)
    {
        for (int i = 0; i < Length; i++)
        {
            if (elements[i] == element)
                return true;
        }
        return false;
    }
    public static Set operator -(Set set, int element)
    {
        Set newSet = new Set(set.Length);
        for (int i = 0; i < set.Length; i++)
        {
            int currEl = set.Element(i);
            if (currEl != element)
            {
                newSet.Add(currEl);
            }
        }
        return newSet;
    }
    public static Set operator *(Set set1, Set set2)
    {
        Set intersection = new Set(set1.Length);

        for (int i = 0; i < set1.Length; i++)
        {
            int element = set1.Element(i);
            if (set2.Contains(element))
            {
                intersection.Add(element);
            }
        }

        return intersection;
    }
    public static bool operator <(Set set1, Set set2)
    {
        if (set1.Length != set2.Length)
        {
            return false;
        }
        for (int i = 0; i < set1.Length; i++)
        {
            if (!set2.Contains(set1.Element(i)))
                return false;
        }
        return true;
    }
    public static bool operator >(Set set1, Set set2)
    {
        for (int i = 0; i < set2.Length; i++)
        {
            if (!set1.Contains(set2.Element(i)))
                return false;
        }
        return true;
    }
    public static Set operator &(Set set1, Set set2)
    {
        Set union = new Set(set1.Length + set2.Length);
        for (int i = 0; i < set1.Length; i++)
        {
            union.Add(set1.Element(i));
        }
        for (int i = 0; i < set2.Length; i++)
        {
            if (!union.Contains(set2.Element(i)))
            {
                union.Add(set2.Element(i));
            }
        }
        return union;
    }
    public void Display()
    {
        for (int i = 0; i < Length; i++)
        {
            Console.Write(elements[i] + " ");
        }
        Console.WriteLine();
    }
}

public static class StatisticOperation
{
    public static int Sum(Set set)
    {
        int sum = 0;
        for (int i = 0; i < set.Length; i++)
        {
            sum += set.Element(i);
        }
        return sum;
    }

    public static int Difference(Set set)
    {
        int max = set.Element(0), min = set.Element(0);
        for (int i = 1; i < set.Length; i++)
        {
            int element = set.Element(i);
            if (element > max) max = element;
            if (element < min) min = element;
        }
        return max - min;
    }

    public static int Count(Set set)
    {
        return set.Length;
    }
}

public class Program
{
    public static void Main(string[] argc)
    {
        Set set1 = new Set(6);
        Set set2 = new Set(6);

        set1.Add(0);
        set1.Add(1);
        set1.Add(2);
        set1.Add(3);
        set1.Add(4);
        set1.Add(6);
        set2.Add(6);
        set2.Add(7);
        set2.Add(8);
        set2.Add(9);
        set2.Add(10);
        set2.Add(0);

        set1.Zero();
        set2.Zero();
        Console.WriteLine("Множество 1:");
        set1.Display();
        Console.WriteLine("Множество 2:");
        set2.Display();

        Set intersection = set1 * set2;
        Console.WriteLine("Пересечение множеств:");
        intersection.Display();

        Set union = set1 & set2;
        Console.WriteLine("Объединение множеств:");
        union.Display();

        Set del = set2 - 6;
        Console.WriteLine("Множество 2 после удаления элемента 3:");
        del.Display();

        Console.WriteLine($"Сумма элементов множества 1: {StatisticOperation.Sum(set1)}");
        Console.WriteLine($"Сумма элементов множества 2: {StatisticOperation.Sum(set2)}");
        Console.WriteLine($"Разница между максимальным и минимальным элементами множества 1: {StatisticOperation.Difference(set1)}");
        Console.WriteLine($"Разница между максимальным и минимальным элементами множества 2: {StatisticOperation.Difference(set2)}");
        Console.WriteLine($"Количество элементов в множестве 1: {StatisticOperation.Count(set1)}");
        Console.WriteLine($"Количество элементов в множестве 2: {StatisticOperation.Count(set2)}");

        Set.Production production = new Set.Production(812, "lab03");
        Console.WriteLine("Информация о компании:");
        production.Display();

        Set.Developer developer = new Set.Developer("Kuliashov A.A.", 555, "Program.cs");
        Console.WriteLine("Информация о разработчике:");
        developer.Display();
        string[] str = { "Hello", "World", "Привет", "Мир" };
        Extend.dot(str);
        for (int i = 0; i < str.Length; i++)
        {
            Console.Write(str[i]);
        }
    }
}