using System.Collections.Generic;
using ChooseEntertainmentItem.Domain.Models;
using ChooseEntertainmentItem.Domain.Repositories;

namespace ChooseEntertainmentItem.Infra.Repositories
{
    public class CsvItemRepository : IItemRepository
    {
        public IEnumerable<BacklogItem> GetBacklogItems()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DoneItem> GetDoneItems()
        {
            throw new System.NotImplementedException();
        }
    }
}