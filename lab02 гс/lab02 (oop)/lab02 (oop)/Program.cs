using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;

public partial class Car {

    private static int ObjectCount;
    public static int a = 5;
    private readonly int id;
    private string make;
    private string model;
    private int year;
    private string color;
    private decimal cost;
    private string regNumber;
    private const int MaxYear = 2025;
    public string Make
    {
        get
        {
            return make;
        }
        set
        {
            make = value; 
        }
    }
    public string Model
    {
        get
        {
            return model;
        } 
        set
        {
            model = value;
        }
    }
    public int Year
    {
        get
        {
            return year;
        }
        set
        {
            if (value > MaxYear)
            {
                throw new ArgumentException("Некорректный год выпуска.");
            } 
            else {
                year = value;
            }
        }
    }
    public string Color
    {
        get
        {
            return color;
        }
        set
        {
            color = value;
        }
    }
    public decimal Price
    {
        get
        {
            return cost;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Проверьте цену.");
            } 
            else {
                cost = value;
            }
        }
    }
    public string RegNumber
    {
        get
        {
            return regNumber;
        }
        set
        {
            regNumber = value;
        }
    }
    static Car()
    {
        ObjectCount = 0;
    }
    public Car(string make, string model, int year, string color, decimal cost, string regNumber)
    {
        id = ObjectCount++;
        Make = make;
        Model = model;
        Year = year;
        Color = color;
        Price = cost;
        RegNumber = regNumber;
    }
    public int GetCarAge()
    {
        return 2024 - year;
    }
    public static void PrintInfo()
    {
        Console.WriteLine($"Общее количество объектов класса: {ObjectCount}");
    }
    public override bool Equals(object obj)
    {
        if (obj is Car car)
        {
            return (make.Equals(car.make) && model.Equals(car.model) && year.Equals(car.year) && color.Equals(car.color) && cost.Equals(car.cost) && regNumber.Equals(car.regNumber));
        }
        return false;
    }
    public void ModCost(ref decimal newCost, out bool isMod)
    {
        if (newCost >= 0)
        {
            cost = newCost;
            isMod = true;
        }
        else
        {
            isMod = false;
        }
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(make, model, year, color, cost, regNumber);
    }
    public override string ToString()
    {
        return $"ID: {id}, Марка: {make}, Модель: {model}, Год: {year}, Цвет: {color}, Цена: {cost}, Регистрационный номер: {regNumber}";
    }
    public static void MakeCars(Car[] cars, string make)
    {
        foreach (var car in cars)
        {
            if (car.Make == make)
            {
                Console.WriteLine(car.ToString());
                if (a==5)
                {
                    break;
                }
            }
        }
    }
    public static void AgeCars(Car[] cars, string make, int years)
    {
        foreach (var car in cars)
        {
            if ((car.Make == make) && (car.GetCarAge() > years))
            {
                Console.WriteLine(car.ToString());
                
            }
        }
    }
}

public partial class Car
{
    public static void AnonType()
    {
        var anonCar = new { Make = "Dodge", Model = "Challenger SRT Hellcat RedEye WideBody", Year = 2018 };
        Console.WriteLine($"Анонимный тип - Марка: {anonCar.Make}, Модель: {anonCar.Model}, Год выпуска: {anonCar.Year}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Car car1 = new Car("Corvette", "c5", 2001, "black", 17500, "0715CC");
        Car car2 = new Car("Audi", "rs6", 2010, "blue", 20000, "8596RS");
        Car car3 = new Car("BMW", "M3 Touring", 2023, "green", 110000, "2686MS");
        Car car4 = new Car("Porsche", "GT3RS", 2022, "orange", 420000, "1112GT");
        Car car5 = new Car("Bentley", "Flying Spur", 2020, "silver", 400000, "6214FS");
        Car car6 = new Car("BMW", "e38", 1999, "black", 10000, "2513JG");
        Car car7 = new Car("Porsche", "boxter 718", 2019, "blue", 57000, "0210DI");
        Car car8 = new Car("Land Rover", "Range Rover Sport", 2020, "brown", 100000, "2065KV");
        Car car9 = new Car("Cadillac", "Eldorado VII", 1971, "white", 30000, "777719");
        Car car10 = new Car("Dodge", "Challenger", 2022, "green", 120000, "0707RW");
        Car car11 = new Car("BMW", "e46", 2002, "silver", 5500, "0546MC");
        Car car12 = new Car("Audi", "S8 D2", 2000, "blue", 11500, "1368WU");
        Car car13 = new Car("Honda", "Civic 5", 1994, "red", 3500, "5049IO");
        Car car14 = new Car("Mercedes - Benz", "E - class w124", 1997, "white", 3000, "4099KK");
        Car car15 = new Car("Porsche", "992 Turbo S", 2023, "red", 250000, "5044FF");
        Car.a = 6;

        decimal newCost = 22700;
        bool isMod;
        car10.ModCost(ref newCost, out isMod);
        Console.WriteLine($"Цена изменена: {isMod}, Новая цена: {car10.Price}");

        Console.WriteLine(car1.Equals(car2) ? "Автомобили одинаковы" : "Автомобили разные.");

        Car[] cars = { car1, car2, car3, car4, car5, car6, car7, car8, car9, car10, car11, car12, car13, car14, car15 };
        Console.WriteLine("\nСписок автомобилей марки Porche: ");
        Car.MakeCars(cars, "Porsche");

        Console.WriteLine("\nСписок автомобилей марки BMW, эксплуатируемых более 20 лет: ");
        Car.AgeCars(cars, "BMW", 20);

        Car.AnonType();
        Car.PrintInfo();
    }

}
