using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TV.Data;


namespace ConsoleApplication1
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Item> xmlItems = GetItemsFromXml();
            IEnumerable<TVItem> dbItems = GetItemsFromDb();
            var repo = new TVItemsRepository(Properties.Settings.Default.ConStr);
            repo.ClearChangeLogs();
            Console.WriteLine("Checking xml items against db items");
            int counter = 0;
            foreach (Item xmlItem in xmlItems)
            {
                counter++;
                if (counter % 1000 == 0)
                {
                    Console.WriteLine(counter);
                }
                TVItem dbItem = dbItems.FirstOrDefault(t => t.ItemNumber == xmlItem.ItemNumber);
                if (dbItem == null)
                {
                    repo.AddNewItemChangeLog(xmlItem.ItemNumber);
                    repo.AddItem(ConvertToTVItem(xmlItem));
                }
                else
                {
                    var changes = GetChanges(dbItem, xmlItem);
                    if (changes.Any())
                    {
                        foreach (var change in changes)
                        {
                            repo.ChangeItemChangeLog(change);
                        }

                        repo.UpdateItem(ConvertToTVItem(xmlItem));
                    }
                }

            }

            counter = 0;
            Console.WriteLine("Checking db items against xml items");
            foreach (TVItem dbItem in dbItems)
            {
                counter++;
                if (counter % 1000 == 0)
                {
                    Console.WriteLine(counter);
                }
                Item xmlItem = xmlItems.FirstOrDefault(t => t.ItemNumber == dbItem.ItemNumber);
                if (xmlItem == null)
                {
                    repo.RemoveItemChangeLog(dbItem.ItemNumber);
                    repo.DeleteItem(dbItem.ItemNumber);
                }
            }

            Console.WriteLine("Done");


            Console.ReadKey(true);
        }

        private static TVItem ConvertToTVItem(Item xmlItem)
        {
            var result = new TVItem();
            foreach (PropertyInfo prop in xmlItem.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var value = prop.GetValue(xmlItem);
                result.GetType().GetProperty(prop.Name).SetValue(result, value);
            }
            return result;
        }

        private static IEnumerable<ChangeLog> GetChanges(TVItem dbItem, Item xmlItem)
        {
            List<ChangeLog> changeLogs = new List<ChangeLog>();
            foreach (PropertyInfo prop in dbItem.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var dbValue = prop.GetValue(dbItem);
                var xmlValue = xmlItem.GetType().GetProperty(prop.Name).GetValue(xmlItem);
                if (!object.Equals(dbValue, xmlValue))
                {
                    ChangeLog changeLog = new ChangeLog
                    {
                        ChangeType = prop.Name,
                        ItemNumber = dbItem.ItemNumber,
                        FromValue = dbValue.ToString(),
                        ToValue = xmlValue.ToString()
                    };
                    changeLogs.Add(changeLog);
                }
            }

            return changeLogs;
        }

        private static IEnumerable<Item> GetItemsFromXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Root));
            using (var stream = new FileStream("tv.xml", FileMode.Open))
            {
                Root root = serializer.Deserialize(stream) as Root;
                return root.Truserv.TrueValueItems;
            }
        }

        private static IEnumerable<TVItem> GetItemsFromDb()
        {
            return new TVItemsRepository(Properties.Settings.Default.ConStr).GetAll();
        }
    }

    [XmlRoot("Root")]
    public class Root
    {
        [XmlElement("Truserv")]
        public Truserv Truserv { get; set; }
    }

    public class Truserv
    {
        [XmlElement("Item")]
        public Item[] TrueValueItems { get; set; }
    }

    public class Item
    {
        [XmlAttribute("item_nbr")]
        public int ItemNumber { get; set; }
        [XmlAttribute("member_cost")]
        public decimal MemberCost { get; set; }
        [XmlAttribute("short_description")]
        public string ShortDescription { get; set; }
        [XmlAttribute("class_code")]
        public int ClassCode { get; set; }
        [XmlAttribute("subclass_code")]
        public int SubClassCode { get; set; }
        [XmlAttribute("vendor_name")]
        public string VendorName { get; set; }
        [XmlAttribute("upc")]
        public string UPC { get; set; }
        [XmlAttribute("long_description")]
        public string LongDescription { get; set; }
        [XmlAttribute("weight")]
        public decimal Weight { get; set; }
        [XmlAttribute("length")]
        public decimal Length { get; set; }
        [XmlAttribute("width")]
        public decimal Width { get; set; }
        [XmlAttribute("height")]
        public decimal Height { get; set; }
        [XmlAttribute("model")]
        public string Model { get; set; }
    }
}
