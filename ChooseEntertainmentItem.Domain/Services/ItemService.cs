using System.Collections.Generic;
using System.Linq;
using ChooseEntertainmentItem.Domain.Configs;
using ChooseEntertainmentItem.Domain.Models;
using ChooseEntertainmentItem.Domain.Repositories;

namespace ChooseEntertainmentItem.Domain.Services
{
    public interface IItemService
    {
        IEnumerable<BacklogItem> CalculateBacklogItemsPriority();
    }

    public class ItemService : IItemService
    {
        private IItemRepository repository;
        private readonly ItemsFiltersOptions options;

        public ItemService(IItemRepository repository, ItemsFiltersOptions options)
        {
            this.repository = repository;
            this.options = options;
        }

        public IEnumerable<BacklogItem> CalculateBacklogItemsPriority()
        {
            var tags = new Dictionary<string, int>();

            var doneItems = repository.GetDoneItems();
            var points = doneItems.Count();
            foreach (var item in doneItems.OrderByDescending(_ => _.DoneDate))
            {
                foreach (var tag in item.Tags.Split('/'))
                {
                    if (tags.Keys.Contains(tag))
                        tags[tag] += points;
                    else
                        tags.Add(tag, points);
                }
                points--;
            }

            var backlogItems = repository.GetBacklogItems();
            if (options.ItemType != "ALL")
                backlogItems = backlogItems.Where(_ => _.Tags.Split('/').Contains(options.ItemType)).ToList();

            foreach (var item in backlogItems)
            {
                foreach (var tag in tags.Keys)
                {
                    if (item.Tags.Split('/').ToList().Contains(tag))
                        item.AddScore(tags[tag]);
                }
                if (options.ShouldIncludePrice)
                    item.AddScore((int)item.Price);
            }

            return backlogItems;
        }
    }
}