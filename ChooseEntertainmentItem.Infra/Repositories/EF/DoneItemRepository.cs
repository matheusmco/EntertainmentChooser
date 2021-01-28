using System.Collections.Generic;
using ChooseEntertainmentItem.Domain.Models;
using ChooseEntertainmentItem.Domain.Repositories;
using ChooseEntertainmentItem.Infra.EFContexts;

namespace ChooseEntertainmentItem.Infra.Repositories.EF
{
    public class DoneItemRepository : IDoneItemRepository
    {
        private Context context { get; set; }

        public DoneItemRepository(Context context)
        {
            this.context = context;
        }

        public void Add(DoneItem item)
        {
            context.Add(item);
            context.SaveChanges();
        }

        public IEnumerable<DoneItem> Get()
            => context.DoneItems;
    }
}