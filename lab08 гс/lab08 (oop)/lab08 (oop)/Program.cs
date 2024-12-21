using static lab08.Fabric;
using static lab08.String;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace lab08
{
    class Fabric
    {
        public delegate void SalaryDelegate(int amount);
        public class Director
        {
            public event SalaryDelegate RaiseSalary;
            public event SalaryDelegate LowerSalary;

            public void toRaise(int amount) => RaiseSalary?.Invoke(amount);
            public void toLower(int amount) => LowerSalary?.Invoke(amount);
        }

        public class Worker
        {
            public int salary = 500;
            public string name = " ";

            public void Raise(int amount) => salary += amount;
            public void Lower(int amount) => salary -= amount;
            public override string ToString()
            {
                return "Name: " + name + "\n" + "Salary: " + salary;
            }
        }
    }

    public class String
    {
        public string str;
        public delegate void StringDelegate();
        public String(string Str) => str = Str;
        public void RemoveMarks()
        {
            foreach (var i in str)
            {
                if (i == ',' || i == '.' || i == '?' || i == '!' || i == ':' || i == ';') 
                {
                    str = str.Replace(i, '\0');
                }
            }
        }
        public void AddSybbol()
        {
            str = str + '@';
        }
        public string ToUpper()
        {
            return str.ToUpper();
        }
        public void ReplaceSpace()
        {
            foreach (var i in str)
            {
                if (i == ' ')
                {
                    str = str.Replace(i, '^');
                }
            }
        }
        public bool NotEmpty(string str)
        {
            return str.Length != 0;
        }
    }

    class Program
    {
        static void Main()
        {
            Director director = new Director();
            Worker worker1 = new Worker();
            worker1.name = "Artsiom";
            Worker worker2 = new Worker();
            worker2.name = "Lomalmo";

            director.RaiseSalary += worker1.Raise;
            director.RaiseSalary += worker2.Raise;
            director.LowerSalary += worker2.Lower;
            director.RaiseSalary += worker1.Raise;

            director.toRaise(100);
            director.toLower(500);

            Console.WriteLine(worker1);
            Console.WriteLine(worker2);

            String str1 = new String("Nowadays, everybody wanna talk like they got somethin' to say");
            String str2 = new String("But nothin' comes out when they move their lips");
            String str3 = new String("Just a bunch of gibberish and motherfuckers act like they forgot about Dre");

            Action action1_1 = str1.RemoveMarks;
            Action action1_2 = str2.RemoveMarks;
            Action action1_3 = str3.RemoveMarks;
            Action action2_1 = str1.AddSybbol;
            Action action2_2 = str2.AddSybbol;
            Action action2_3 = str3.AddSybbol;
            Action action3_1 = str1.ReplaceSpace;
            Action action3_2 = str2.ReplaceSpace;
            Action action3_3 = str3.ReplaceSpace;

            Func<string> upper1 = str1.ToUpper;
            Func<string> upper2 = str2.ToUpper;
            Func<string> upper3 = str3.ToUpper;
            Predicate<string> NotEmpty1 = str1.NotEmpty;
            Predicate<string> NotEmpty2 = str2.NotEmpty;
            Predicate<string> NotEmpty3 = str3.NotEmpty;

            if ((NotEmpty1(str1.str)) || (NotEmpty2(str2.str)) || (NotEmpty3(str3.str))) 
            {
                action1_1.Invoke();
                action1_2.Invoke();
                action1_3.Invoke();
                action2_1.Invoke();
                action2_2.Invoke();
                action2_3.Invoke();
                action3_1.Invoke();
                action3_2.Invoke();
                action3_3.Invoke();

                Console.WriteLine(upper1());
                Console.WriteLine(upper2());
                Console.WriteLine(upper3());
            }
        }
    }
}