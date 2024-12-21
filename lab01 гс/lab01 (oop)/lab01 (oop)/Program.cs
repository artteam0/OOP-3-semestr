using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        private static object strrts;

        static void Main()
        {
            //types//

            //1//
            bool a = false;
            byte b = 0;
            sbyte c = 1;
            char d = 'a';
            decimal e = 6E7m;
            double f = 2.5;
            float g = 10.01F;
            int h = -2;
            uint i = 9;
            nint j = -99;
            nuint k = 11;
            long l = -1111;
            ulong r = 122313;
            short v = 2;
            ushort s = 3;
            //2//
            //явное
            double kk = 123.45;
            int ll = (int)k;
            Console.WriteLine(kk);
            long mm = 32767;
            short nn = (short)mm;
            Console.WriteLine(mm);
            float oo = 9.99f;
            int pp = (int)oo;
            Console.WriteLine(oo);
            int qq = 255;
            byte rr = (byte)qq;
            Console.WriteLine(qq);
            double ss = 5.6789;
            float tt = (float)s;
            Console.WriteLine(ss);
            //неявное
            int aa = 100;
            long bb = aa;
            Console.WriteLine(bb);
            float cc = 10.5f;
            double dd = c;
            Console.WriteLine(cc);
            char ee = 'A';
            int ff = ee;
            Console.WriteLine(ee);
            byte gg = 255;
            int hh = gg;
            Console.WriteLine(gg);
            ushort ii = 50000;
            uint jj = i;
            Console.WriteLine(ii);

            Convert.ToDouble(c);
            Console.WriteLine(c);
            //3//
            int u = 10;
            Console.WriteLine(u);
            object obj = u;
            int u2 = (int)obj; //распаковка
            Console.WriteLine(u2);
            //4//
            var num1 = 10; //int
            var num2 = 10.01; //double
            var num3 = 10.01f; //float
            Console.WriteLine(num1.GetType());
            Console.WriteLine(num2.GetType());
            Console.WriteLine(num3.GetType());
            //5//
            object vv = null;
            Console.WriteLine(vv);
            //6//
            var w = 64;
            Console.WriteLine(w);
            // w=2.1E6;
            Console.WriteLine(w);

            //string//
            //1//
            string symb1 = "qwerty", symb2 = "qwertyy";
            Console.WriteLine(System.String.Compare(symb1, symb2));
            //2//
            string str1 = "Hello World";
            string str2 = "Привет Мир";
            string str3 = "10";

            Console.WriteLine(string.Concat(str1, str2, str3));

            Console.WriteLine(string.Copy(str1));

            Console.WriteLine((str1, 10));
            string[] words = str1.Split(' ');

            Console.WriteLine(string.Join(", ", words));

            Console.WriteLine(str3.Insert(1, " "));

            Console.WriteLine(str2.Remove(8));

            int day = 26;
            Console.WriteLine($"Сегодня {day} декабря, мой день рождения");
            //3//
            string no1 = "";
            string no2 = null;
            Console.WriteLine(System.String.Compare(no1, no2));
            Console.WriteLine(System.String.IsNullOrEmpty(no1));
            Console.WriteLine(System.String.IsNullOrEmpty(no2));
            //4//
            StringBuilder sb = new StringBuilder("Hello, World!");
            sb.Insert(0, "Hello,");
            sb.Append("World!");
            sb.Replace("World!", "Мир");
            Console.WriteLine(sb);

            //array//
            //1//
            int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };
            for (int it = 0; it < matrix.GetLength(0); it++)
            {
                for (int jt = 0; jt < matrix.GetLength(1); jt++)
                {
                    Console.Write("{0}\t", matrix[it, jt]);
                }
                Console.WriteLine();
            }
            //2//
            string[] array = { "КПО", "ООП", "СЯП", "АСД", "КСиС", };
            Console.WriteLine("Содержимое массива:");
            foreach (string stud in array) //для каждого элемента
            {
                Console.WriteLine(stud);
            }
            Console.WriteLine($"Длина массива: {array.Length}");
            Console.Write("Введите индекс элемента для изменения (0 - {0}): ", array.Length - 1);
            int index = int.Parse(Console.ReadLine()); //преобразование в int
            Console.Write("Введите новое значение: ");
            string newValue = Console.ReadLine();
            array[index] = newValue;
            Console.WriteLine("Обновлённое содержимое массива:");
            foreach (string stud in array)
            {
                Console.WriteLine(stud);
            }
            //3//
            double[][] arr = new double[3][];
            arr[0] = new double[2] { 1, 2 };
            arr[1] = new double[3] { 1, 2, 3 };
            arr[2] = new double[4] { 1, 2, 3, 4 };
            for (i = 0; i < arr.Length; i++)
            {
                for (j = 0; j < arr[i].Length; j++) //текущая строка
                {
                    Console.Write(arr[i][j] + " ");
                }
            }
            //4//
            var x = "HelloWorld";
            Console.WriteLine(" ");
            Console.WriteLine(x.GetType());
            var y = new int[1, 2, 2];
            Console.WriteLine(y.GetType());

            //кортежи//
            //1, 2//
            (int, string, char, string, ulong) cort = (52, "Hello World", '!', "iPhone", 7952812);
            Console.WriteLine($"Int:{cort.Item1},string:{cort.Item2},char:{cort.Item3},string:{cort.Item4},ulong:{cort.Item5}");
            Console.WriteLine($"Int:{cort.Item1},char:{cort.Item3},ulong:{cort.Item5}");
            //3//
            var cortex = ("Hello", 52);
            (string name, int age) = cortex; //var
            string name2;
            int age2;
            (name2, age2) = cortex;
            Console.WriteLine(name);
            Console.WriteLine(age);
            Console.WriteLine(name2);
            Console.WriteLine(age2);
            //4//
            (int, int) s1 = (1, 2);
            (int, int) s2 = (1, 2);
            Console.WriteLine(s1 == s2);
            //5//
            int[] num = { 10, 8, 15, 2, 1, 6 };
            string str = "Hello World";
            var result = AArray(num, str);
            Console.WriteLine($"Максимум: {result.max}, Минимум: {result.min}, Сумма: {result.sum}, Первая буква: {result.firstLetter}");
            (int max, int min, int sum, char firstLetter) AArray(int[] array, string str)
            {
                int max = int.MinValue;
                int min = int.MaxValue;
                int sum = 0;

                foreach (var number in array)
                {
                    if (number > max) max = number;
                    if (number < min) min = number;
                    sum += number;
                }
                char firstLetter = str.Length > 0 ? str[0] : '\0'; //если содержит , то первый символ , иначе нулевой

                return (max, min, sum, firstLetter);
            }
            //6//
            void Func1()
            {
                int a = int.MaxValue;
                unchecked { a = a + 1; }
                Console.WriteLine($"Func1: a после переполнения = {a}");

            }
            void Func2()
            {
                int a = int.MaxValue;
                try
                {
                    checked { a = a + 1; }
                }
                catch (OverflowException ex)
                {

                    Console.WriteLine("Func2: " + ex.Message);
                }
            }
            Func1();
            Func2();

        }
    }
 }

