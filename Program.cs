using System;
using System.Collections.Generic;
using System.Linq;

namespace ChooseEntertainmentItem
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<DoneItem>{
                new DoneItem
                {
                    Name = "Limites da fundação",
                    Tags =  "livro,scifi,space-opera",
                    DoneDate = new DateTime(2020,12,16)
                },
                new DoneItem
                {
                    Name = "Sandman 7",
                    Tags =  "hq,livro",
                    DoneDate = new DateTime(2020,10,30)
                },
            };

            var list2 = new List<BacklogItem>{
                new BacklogItem
                {
                    Name = "Cyberpunk 2077",
                    Tags = "jogo,scifi,cyberpunk,fps"
                },
                new BacklogItem
                {
                    Name = "O Islã e a formação da Europa",
                    Tags = "hq,historia,oriental"
                }
            };

            var peso = list.Count;
            var tags = new Dictionary<string, int>();
            foreach (var item in list.OrderByDescending(_ => _.DoneDate))
            {
                foreach (var tag in item.Tags.Split(','))
                {
                    if (tags.Keys.Contains(tag))
                        tags[tag] += peso;
                    else
                        tags.Add(tag, peso);
                }
                peso--;
            }

            foreach (var item in list2)
            {
                foreach (var tag in tags.Keys)
                {
                    if (item.Tags.Split(',').ToList().Contains(tag))
                        item.Score += tags[tag];
                }
            }

            foreach (var item in list2.OrderBy(_ => _.Score).OrderBy(_ => _.Price))
                Console.WriteLine($"Name: {item.Name}, Score: {item.Score}");
        }
    }

    class DoneItem : Item
    {
        public DateTime DoneDate { get; set; }
    }

    class BacklogItem : Item
    {
        public int Score { get; set; }
        public double Price { get; set; }
    }

    abstract class Item
    {
        public string Name { get; set; }
        public string Tags { get; set; }
    }
}