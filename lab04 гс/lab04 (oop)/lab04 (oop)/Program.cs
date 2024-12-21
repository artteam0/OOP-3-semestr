using System;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

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
public class Bush : Plant
{
    public override string PlantName()
    {
        return "Куст";
    }
    public override string PlantCost()
    {
        return "50$";
    }
    public void ToString(Bush qq)
    {
        Console.WriteLine("Название" + qq.PlantName);
        Console.WriteLine("Стоимость" + qq.PlantCost());
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
    public void ToString(Flower qq)
    {
        Console.WriteLine("Название" + qq.PlantName);
        Console.WriteLine("Стоимость" + qq.PlantCost());
    }
}
public class Rose : Color, Iphone
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
    public void ToString(Gladiolus qq)
    {
        Console.WriteLine("Название" + qq.PlantName);
        Console.WriteLine("Стоимость" + qq.PlantCost());
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
    public void ToString(Cactus qq)
    {
        Console.WriteLine("Название" + qq.PlantName);
        Console.WriteLine("Стоимость" + qq.PlantCost());
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

public class BaseClass
{
    internal void BaseFunc()
    {
        Console.WriteLine("BaseFunc");
    }
}
    class CCClass : BaseClass
    {

    }

public class Program
{
    public static void Main()
    {
        Bush   Bush1      =  new  Bush()      ;
        Bush   Bush2      =  new  Bush()      ;
        Plant  Flower     =  new  Flower()    ;
        Rose   Rose       =  new  Rose()      ;
        Plant  Gladiolus  =  new  Gladiolus() ;
        Plant  Cactus     =  new  Cactus()    ;
        Plant  Paper      =  new  Paper()     ;

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
        Paper paper =new Paper() ;
        Console.WriteLine(paper.a);

        CCClass classs = new CCClass();
        classs.BaseFunc();
    }
}