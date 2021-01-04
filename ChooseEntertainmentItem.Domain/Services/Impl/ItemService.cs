using System.Collections.Generic;
using ChooseEntertainmentItem.Domain.Models;
using ChooseEntertainmentItem.Domain.Services.Contracts;

namespace ChooseEntertainmentItem.Domain.Services.Impl
{
    public class ItemService : IItemService
    {
        public IEnumerable<BacklogItem> CalculateBacklogItemsPriority(bool shouldIncludePrice)
        {
            throw new System.NotImplementedException();
        }
    }
}