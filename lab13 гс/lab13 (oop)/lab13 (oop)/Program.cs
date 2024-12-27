using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace lab13
{
    public interface ISerializer
    {
        void Serialize<T>(T obj, FileStream stream);
        T Deserialize<T>(FileStream stream);
    }

    public class JsonSerializerWrapper : ISerializer
    {
        public void Serialize<T>(T obj, FileStream stream)
        {
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                WriteIndented = true
            };
            JsonSerializer.Serialize(stream, obj, options);
        }

        public T Deserialize<T>(FileStream stream)
        {
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                WriteIndented = true
            };
            return JsonSerializer.Deserialize<T>(stream, options);
        }
    }

    public class XmlSerializerWrapper : ISerializer
    {
        public void Serialize<T>(T obj, FileStream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, obj);
        }

        public T Deserialize<T>(FileStream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }
    }

    [Serializable]
    public class Cactus
    {
        public string Name { get; set; } = "Cactus";
        public int Cost { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, cost: {Cost}";
        }
    }

    class Program
    {
        static void TestSerialization(ISerializer serializer, Cactus cactus, string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(cactus, fileStream);
            }
            Cactus deserializedCactus;
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                deserializedCactus = serializer.Deserialize<Cactus>(fileStream);
            }

            Console.WriteLine($"Исходный объект: {cactus}");
            Console.WriteLine($"Десериализованный объект: {deserializedCactus}");

            Console.WriteLine($"Сериализовано в {fileName}.\n");
        }

        static void Main()
        {
            Cactus cactus = new Cactus { Name = "CACTUS", Cost = 100 };

            TestSerialization(new JsonSerializerWrapper(), cactus, "Cactus.json");
            Cactus deserializedCactusJson;
            using (var fileStream = new FileStream("Cactus.json", FileMode.Open))
            {
                deserializedCactusJson = new JsonSerializerWrapper().Deserialize<Cactus>(fileStream);
            }
            Console.WriteLine("Десериализованный кактус из JSON:");
            Console.WriteLine(deserializedCactusJson.ToString());

            TestSerialization(new XmlSerializerWrapper(), cactus, "Cactus.xml");
            Cactus deserializedCactusXml;
            using (var fileStream = new FileStream("Cactus.xml", FileMode.Open))
            {
                deserializedCactusXml = new XmlSerializerWrapper().Deserialize<Cactus>(fileStream);
            }
            Console.WriteLine("Десериализованный кактус из XML:");
            Console.WriteLine(deserializedCactusXml.ToString());

            XElement cactusXml = new XElement("CACTUS",
                new XElement("Cactus",
                    new XElement("Name", "Кактус"),
                    new XElement("Cost", 100)
                ),
                new XElement("cACTUS",
                    new XElement("Name", "Кактусище"),
                    new XElement("Cost", 100000)
                )
            );

            cactusXml.Save("CactusLinq.xml");

            Console.WriteLine("Все кактусы из XML:");
            var allCactus = cactusXml.Elements("Cactus");
            foreach (var cactusElement in allCactus)
            {
                Console.WriteLine(cactusElement.Element("Name")?.Value);
            }
        }
    }
}
