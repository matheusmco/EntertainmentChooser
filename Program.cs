using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace ChooseEntertainmentItem
{
    class Program
    {
        static void Main(string[] args)
        {
            var tags = new Dictionary<string, int>();

            using (var reader = new StreamReader("items/doneItems.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var doneItems = csv.GetRecords<DoneItem>().ToList();

                var peso = doneItems.Count();
                foreach (var item in doneItems.OrderByDescending(_ => _.DoneDate))
                {
                    foreach (var tag in item.Tags.Split('/'))
                    {
                        if (tags.Keys.Contains(tag))
                            tags[tag] += peso;
                        else
                            tags.Add(tag, peso);
                    }
                    peso--;
                }
            }

            using (var reader = new StreamReader("items/backlogItems.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // csv.Configuration.HeaderValidated = (null, null) => { return null; });
                var backlogItems = csv.GetRecords<BacklogItem>().ToList();
                foreach (var item in backlogItems)
                {
                    foreach (var tag in tags.Keys)
                    {
                        if (item.Tags.Split('/').ToList().Contains(tag))
                            item.Score += tags[tag];
                    }
                }

                foreach (var item in backlogItems.OrderBy(_ => _.Score).OrderBy(_ => _.Price))
                    Console.WriteLine($"Name: {item.Name}, Score: {item.Score}");
            }
        }
    }

    class DoneItem : Item
    {
        [Index(2)]
        public DateTime DoneDate { get; set; }
    }

    class BacklogItem : Item
    {
        [Index(2)]
        public double Price { get; set; }
        [Ignore]
        public int Score { get; set; }
    }

    abstract class Item
    {
        [Index(0)]
        public string Name { get; set; }
        [Index(1)]
        public string Tags { get; set; }
    }
}