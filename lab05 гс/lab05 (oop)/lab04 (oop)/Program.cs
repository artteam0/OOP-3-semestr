using System;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace lab04
{
    public enum FlowerColor
    {
        White, Yellow, Blue, Red, Green
    }
    public struct FlowerInf
    {
        public string Name { get; set; }
        public FlowerColor Color { get; set; }
        public double Cost { get; set; }

        public FlowerInf(string name, FlowerColor color, double cost)
        {
            Name = name;
            Color = color;
            Cost = cost;
        }
    }

    interface Iphone
    {
        public string PlantColor();
    }
    public abstract class Color
    {
        public abstract string PlantColor();
        public abstract string PlantName();
        public abstract string PlantCost();
    }
    public abstract class Plant
    {
        public abstract string PlantName();
        public abstract string PlantCost();
        public virtual string PlantColor()
        {
            return $"{PlantName()} - {PlantCost()}";
        }
        public override string ToString()
        {
            return PlantColor();
        }
    }
    public partial class Bush : Plant
    {
        public override string PlantName()
        {
            return "Куст";
        }
        public override string PlantCost()
        {
            return "50$";
        }
        public bool Equals(Bush bush)
        {
            if (bush is Bush)
            {
                return PlantName() == bush.PlantName() && PlantCost() == bush.PlantCost();
            }
            return false;
        }
        public override int GetHashCode()
        {
            return PlantName().GetHashCode() ^ PlantCost().GetHashCode();
        }
        public override string PlantColor()
        {
            return $"{base.PlantColor()} - Цвет.";
        }
    }
    public class Flower : Plant
    {
        public override string PlantName()
        {
            return "Цветок";
        }
        public override string PlantCost()
        {
            return "15$";
        }
        public override string PlantColor()
        {
            return "Blue";
        }
        public void ToString(Flower qq)
        {
            Console.WriteLine("Название" + qq.PlantName);
            Console.WriteLine("Стоимость" + qq.PlantCost());
            Console.WriteLine("Цвет" + qq.PlantColor());
        }
    }
    public class Rose : Plant, Iphone
    {
        public override string PlantName()
        {
            return "Роза";
        }
        public override string PlantCost()
        {
            return "20$";
        }
        public override string PlantColor()
        {
            return "White";
        }
        string Iphone.PlantColor()
        {
            return "Red";
        }

        public void ToString(Rose qq)
        {
            Console.WriteLine("Название" + qq.PlantName);
            Console.WriteLine("Стоимость" + qq.PlantCost());
        }
    }
    public class Gladiolus : Plant
    {
        public override string PlantName()
        {
            return "Гладиолус";
        }
        public override string PlantCost()
        {
            return "18$";
        }
        public override string PlantColor()
        {
            return "Yellow";
        }
        public void ToString(Gladiolus qq)
        {
            Console.WriteLine("Название" + qq.PlantName);
            Console.WriteLine("Стоимость" + qq.PlantCost());
            Console.WriteLine("Цвет" + qq.PlantColor());
        }
    }
    public class Cactus : Plant
    {
        public override string PlantName()
        {
            return "Кактус";
        }
        public override string PlantCost()
        {
            return "200$";
        }
        public override string PlantColor()
        {
            return "Green";
        }
        public void ToString(Cactus qq)
        {
            Console.WriteLine("Название" + qq.PlantName);
            Console.WriteLine("Стоимость" + qq.PlantCost());
            Console.WriteLine("Цвет" + qq.PlantColor());
        }
    }
    public class Paper : Plant
    {
        public override string PlantName()
        {
            return "Бумага";
        }
        public override string PlantCost()
        {
            return "20000000$";
        }
        public void ToString(Paper qq)
        {
            Console.WriteLine("Название" + qq.PlantName);
            Console.WriteLine("Стоимость" + qq.PlantCost());
        }
        private int _a = 5;
        public int a
        {
            get { return _a; }
            set { _a = value; }
        }
    }
    public sealed class Buket : Plant
    {
        public override string PlantName()
        {
            return "Букет";
        }
        public override string PlantCost()
        {
            return "180$";
        }
        public void ToString(Buket qq)
        {
            Console.WriteLine("Название" + qq.PlantName);
            Console.WriteLine("Стоимость" + qq.PlantCost());
        }
    }
    public class Printer
    {
        public void IAmPrinting(Plant plant)
        {
            Console.WriteLine(plant.ToString());
        }
    }

    public class BuketContainer
    {
        private List<Plant> plants = new List<Plant>();
        public void Add(Plant plant) => plants.Add(plant);
        public void Delete(Plant plant) => plants.Remove(plant);
        public Plant Get(int index) => plants[index];
        public void Set(int index, Plant plant) => plants[index] = plant;

        public void PrintAll()
        {
            foreach (var plant in plants)
                Console.WriteLine(plant.ToString());
        }
        public double TotalCost()
        {
            double totalCost = 0;
            foreach (var plant in plants)
                totalCost += double.Parse(plant.PlantCost().TrimEnd('$'));
            return totalCost;
        }
        public Plant FindByColor(string color)
        {
            return plants.Find(plant => plant.PlantColor() == color);
        }
        public void SortByName()
        {
            plants.Sort((x, y) => x.PlantName().CompareTo(y.PlantName()));
        }
        public List<Plant> GetPlants()
        {
            return new List<Plant>(plants);
        }
    }
    public class BuketControl
    {
        private BuketContainer container;
        public BuketControl(BuketContainer container)
        {
            this.container = container;
        }
        public void SortByName()
        {
            container.SortByName();
        }
        public void DisplayCost()
        {
            Console.WriteLine($"Общая стоимость букета: {container.TotalCost()}$");
        }

        public void DisplayColor(string color)
        {
            var foundPlant = container.FindByColor(color);
            if (foundPlant != null)
                Console.WriteLine($"Найденный цветок: {foundPlant.PlantName()} с цветом {color}");
            else
                Console.WriteLine("Цветок с заданным цветом не найден.");
        }
    }
    public class Program
    {
        public static void Main()
        {
            BuketContainer buket = new BuketContainer();

            buket.Add(new Rose());
            buket.Add(new Gladiolus());
            buket.Add(new Cactus());
            buket.Add(new Flower());

            BuketControl controll = new BuketControl(buket);

            controll.DisplayCost();
            controll.SortByName();
            Console.WriteLine("Отсортированные имена растений:");
            foreach (var plant in buket.GetPlants())
            {
                Console.WriteLine(plant.PlantName());
            }
            controll.DisplayColor("Yellow");

            Bush Bush1 = new Bush();
            Bush Bush2 = new Bush();
            Plant Flower = new Flower();
            Rose Rose = new Rose();
            Plant Gladiolus = new Gladiolus();
            Plant Cactus = new Cactus();
            Plant Paper = new Paper();

            Plant[] plants = { Flower, Gladiolus, Cactus, Paper };

            Printer printer = new Printer();
            foreach (var plant in plants)
            {
                printer.IAmPrinting(plant);
            }

            Console.WriteLine(Rose.PlantColor());
            Iphone a = Rose;
            Console.WriteLine(a.PlantColor());

            Console.WriteLine($"Сравнение двух кустов: {Bush1.Equals(Bush2)}");

            foreach (var plant in plants)
            {
                if (plant is Iphone phone)
                {
                    Console.WriteLine($"{plant.PlantName()} {phone.PlantColor()}");
                }
            }
            Paper paper = new Paper();
            Console.WriteLine(paper.a);
        }
    }
}