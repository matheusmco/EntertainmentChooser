using System.Collections.Generic;
using ChooseEntertainmentItem.Domain.Models;

namespace ChooseEntertainmentItem.Domain.Repositories
{
    public interface IBacklogItemRepository
    {
        IEnumerable<BacklogItem> Get();
        void Add(BacklogItem item);
    }
}