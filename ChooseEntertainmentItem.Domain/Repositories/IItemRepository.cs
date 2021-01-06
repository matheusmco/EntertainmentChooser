using ChooseEntertainmentItem.Domain.Models;
using System.Collections.Generic;

namespace ChooseEntertainmentItem.Domain.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<DoneItem> GetDoneItems();
        IEnumerable<BacklogItem> GetBacklogItems();
    }
}