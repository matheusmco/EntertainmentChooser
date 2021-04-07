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
        private IBacklogItemRepository backlogRepository;
        private IDoneItemRepository doneRepository;

        public ItemService(IBacklogItemRepository backlogRepository, IDoneItemRepository doneRepository)
        {
            this.backlogRepository = backlogRepository;
            this.doneRepository = doneRepository;
        }

        public IEnumerable<BacklogItem> CalculateBacklogItemsPriority(bool shouldIncludePrice, string itemType)
        {
            var tags = new Dictionary<string, int>();

            var doneItems = doneRepository.All();
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

            var backlogItems = backlogRepository.All();
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
        }
    }
}