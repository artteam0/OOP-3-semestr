using System;
using System.Diagnostics.CodeAnalysis;

namespace lab10
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public long CardNum { get; set; }
        public decimal Balance { get; set; }
        public string City { get; set; }

        public Customer(int id, string firstName, string lastName, string address, long cardNum, decimal balance, string city)
        {
            if (cardNum.ToString().Length != 16)
                throw new ArgumentException("Номер карты должен состоять из 16 цифр.");

            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Address = address;
            CardNum = cardNum;
            Balance = balance;
            City = city;
        }
        public void Dep(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Сумма для депа не может быть отрицательной.");
            }
            Balance += amount;
        }
        public void Spisanie(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Сумма для списания должна быть отрицательной.");
            }
            if (amount > Balance)
            {
                throw new ArgumentException("Недостаточно средств.");
            }
            Balance -= amount;
        }
    }
    class Program
    {
        static void Main()
        {
            string[] Months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            int n = 3;
            var length = Months.Where(Months => Months.Length == n);
            Console.WriteLine($"Месяцы с длиной строки 3: {string.Join(", ", length)}");

            string[] SummerAndWinter = { "June", "July", "August", "December", "January", "February" };
            var SumAndWint = Months.Where(Months => SummerAndWinter.Contains(Months));
            Console.WriteLine($"Летние и зимние месяцы: {string.Join(", ", SumAndWint)}");

            var alphabet = Months.OrderBy(Months => Months);
            Console.WriteLine($"Месяцы в алфавитном порядке: {string.Join(", ", alphabet)}");

            var filt = Months.Where(Month => Month.Contains('u') && Month.Length >= 4);
            Console.WriteLine($"Отсортированные месяцы: {string.Join(", ", filt)}");

            var custom = new List<Customer>
            {
                new Customer(1,  "Артём",     "Кулешов",       "П.Панченко 74-44",      5548320014070809, 485000, "Минск"  ),
                new Customer(2,  "Арсений",   "Шайба",         "пр.Машерова 14-11",     9856325710690157, 10,     "Минск"  ),
                new Customer(3,  "Кирилл",    "Дмитроченко",   "Свердлова 13-5",        1256985402367145, 120000, "Минск"  ),
                new Customer(4,  "Илья",      "Старовойтов",   "Железнодорожная 85-92", 6305486504365874, 97000,  "Минск"  ),
                new Customer(5,  "Владислав", "Лужецкий",      "Ангарская 17-28",       4650456406546154, 54000,  "Москва" ),
                new Customer(6,  "Иван",      "Александрович", "Партизанский пр. 61-8", 5405461426406504, 55000,  "Москва" ),
                new Customer(7,  "Герман",    "Статько",       "Мазурова 12-102",       7865416426546504, 12000,  "Москва" ),
                new Customer(8,  "Вадим",     "Федорович",     "Ленина 8-17",           5406054065465420, 37000,  "Москва" ),
                new Customer(9,  "Иван",      "Чемурако",      "Московская 40-67",      6542650654654068, 87000,  "Москва" ),
                new Customer(10, "Глеб",      "Смольянинов",   "пр.Держинского 107-89", 8697980075065459, 67000,  "Минск"  ),
            };

            var sorted = custom.OrderBy(c => c.LastName).ThenBy(c => c.FirstName);
            Console.WriteLine("\nОтсортированные покупатели: ");
            foreach (var customer in sorted)
            {
                Console.WriteLine($"{customer.LastName} {customer.FirstName}");
            }

            long max = 6000000000000000;
            long min = 5000000000000000;
            var filtCard = custom.Where(c => c.CardNum >= min && c.CardNum <= max);
            Console.WriteLine("\nПокупатели с номером карты в заданном интервале: ");
            foreach(var customer in filtCard)
            {
                Console.WriteLine($"{customer.LastName} {customer.FirstName} - {customer.CardNum}");
            }

            var richest = custom.OrderByDescending(c => c.Balance).FirstOrDefault();
            Console.WriteLine("\nПокупатель с максимальным балансом:");
            if (richest != null)
            {
                Console.WriteLine($"{richest.LastName} {richest.FirstName} - Баланс: {richest.Balance}");
            }

            var top = custom.OrderByDescending(c => c.Balance).Take(5);
            Console.WriteLine("\nПять покупателей с максимальной суммой на карте:");
            foreach (var customer in top)
            {
                Console.WriteLine($"{customer.LastName} {customer.FirstName} - Баланс: {customer.Balance}");
            }

            var a = custom
            .Where(c => c.Balance > 50000 && c.City == "Минск")
            .OrderByDescending(c => c.Balance)
            .GroupBy(c => c.City)
            .Select(group => new
            {
                City = group.Key,
                TotalBalance = group.Sum(c => c.Balance),
                AverageBalance = group.Average(c => c.Balance),
                Customers = group.ToList()
            })
            .Where(result => result.Customers.Count>1);
            Console.WriteLine("\n");

            foreach (var result in a)
            {
                Console.WriteLine($"Город: {result.City}");
                Console.WriteLine($"Общий баланс: {result.TotalBalance}");
                Console.WriteLine($"Средний баланс: {result.AverageBalance}");
                Console.WriteLine("Покупатели:");
                foreach (var customer in result.Customers)
                {
                    Console.WriteLine($"{customer.LastName} {customer.FirstName}, Баланс: {customer.Balance}");
                }

                string str1 = "12345";
                Console.WriteLine(string.Join(", ", str1.ToCharArray()));
            }
        }
    }
}
