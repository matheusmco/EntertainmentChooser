using System.Collections.Generic;
using ChooseEntertainmentItem.Domain.Models;

namespace ChooseEntertainmentItem.Domain.Services.Contracts
{
    public interface IItemService
    {
        IEnumerable<BacklogItem> CalculateBacklogItemsPriority(bool shouldIncludePrice);
    }
}