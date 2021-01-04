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
            var path = args[0];
            var isIncludePrice = args.Count() >= 2 && args[1].ToUpper() == "S";
            var itemType = args.Count() < 3 ? "ALL" : args[2];
            var tags = new Dictionary<string, int>();

            // TODO: move to Domain

            // TODO: use repository
            using (var reader = new StreamReader($"{path}/doneItems.csv"))
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

            using (var reader = new StreamReader($"{path}/backlogItems.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var backlogItems = csv.GetRecords<BacklogItem>().ToList();
                if (itemType != "ALL")
                    backlogItems = backlogItems.Where(_ => _.Tags.Split('/').Contains(itemType)).ToList();

                foreach (var item in backlogItems)
                {
                    foreach (var tag in tags.Keys)
                    {
                        if (item.Tags.Split('/').ToList().Contains(tag))
                            item.Score += tags[tag];
                    }
                    if (isIncludePrice)
                        item.Score += (int)item.Price;
                }

                foreach (var item in backlogItems.OrderBy(_ => _.Score).ThenBy(_ => _.Price))
                    Console.WriteLine($"Name: {item.Name}, Score: {item.Score}");
            }
        }
    }

    class DoneItem : Item
    {
        public string DoneDate { get; set; }
    }

    class BacklogItem : Item
    {
        public double Price { get; set; }
        [Ignore]
        public int Score { get; set; }
    }

    abstract class Item
    {
        public string Name { get; set; }
        public string Tags { get; set; }
    }
}