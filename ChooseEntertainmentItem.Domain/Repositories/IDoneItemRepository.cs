using System.Collections.Generic;
using ChooseEntertainmentItem.Domain.Models;

namespace ChooseEntertainmentItem.Domain.Repositories
{
    public interface IDoneItemRepository
    {
        IEnumerable<DoneItem> All();
        void Add(DoneItem item);
    }
}