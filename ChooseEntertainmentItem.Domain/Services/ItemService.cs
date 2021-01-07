using System.Collections.Generic;
using System.Linq;
using ChooseEntertainmentItem.Domain.Models;
using ChooseEntertainmentItem.Domain.Repositories;

namespace ChooseEntertainmentItem.Domain.Services
{
    public interface IItemService
    {
        IEnumerable<BacklogItem> CalculateBacklogItemsPriority(bool shouldIncludePrice, string itemType);
    }

    public class ItemService : IItemService
    {
        private IItemRepository repository;

        public ItemService(IItemRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<BacklogItem> CalculateBacklogItemsPriority(bool shouldIncludePrice, string itemType)
        {
            var tags = new Dictionary<string, int>();

            var doneItems = repository.GetDoneItems();
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

            var backlogItems = repository.GetBacklogItems();
            if (itemType != "ALL")
                backlogItems = backlogItems.Where(_ => _.Tags.Split('/').Contains(itemType)).ToList();

            foreach (var item in backlogItems)
            {
                foreach (var tag in tags.Keys)
                {
                    if (item.Tags.Split('/').ToList().Contains(tag))
                        item.Score += tags[tag];
                }
                if (shouldIncludePrice)
                    item.Score += (int)item.Price;
            }

            return backlogItems;

            // using (var reader = new StreamReader($"{path}/doneItems.csv"))
            // using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            // {
            //     var doneItems = csv.GetRecords<DoneItem>().ToList();

            //     
            // }

            // using (var reader = new StreamReader($"{path}/backlogItems.csv"))
            // using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            // {
            //     var backlogItems = csv.GetRecords<BacklogItem>().ToList();

            // }
        }
    }
}