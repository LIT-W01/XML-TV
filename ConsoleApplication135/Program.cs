using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApplication135
{
    class Program
    {
        static void Main(string[] args)
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(PeopleList));
            //using (FileStream stream = new FileStream("People.xml", FileMode.Open))
            //{
            //    PeopleList list = serializer.Deserialize(stream) as PeopleList;
            //    foreach (Person p in list.People)
            //    {
            //        Console.WriteLine(p);
            //    }
            //}

            XmlSerializer serializer = new XmlSerializer(typeof(Garage));
            using (FileStream stream = new FileStream("cars.xml", FileMode.Open))
            {
                Garage g = serializer.Deserialize(stream) as Garage;
                foreach (Tool tool in g.Tools)
                {
                    Console.WriteLine(tool);
                }
            }

            Console.ReadKey(true);
        }
    }

    [XmlRoot("People")]
    public class PeopleList
    {
        [XmlElement("Person")]
        public Person[] People { get; set; }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Age;
        }
    }

    [XmlRoot("Garage")]
    public class Garage
    {
        [XmlArray("Cars")]
        public Car[] Cars { get; set; }

        [XmlArray("Tools")]
        public Tool[] Tools { get; set; }

    }

    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }

    public class Tool
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("brand")]
        public string Brand { get; set; }

        public override string ToString()
        {
            return Type + " " + Brand;
        }
    }
}
