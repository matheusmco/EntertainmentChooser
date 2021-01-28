using System.Collections.Generic;
using ChooseEntertainmentItem.Domain.Models;
using ChooseEntertainmentItem.Domain.Repositories;
using ChooseEntertainmentItem.Infra.EFContexts;

namespace ChooseEntertainmentItem.Infra.Repositories.EF
{
    public class BacklogItemRepository : IBacklogItemRepository
    {
        private Context context { get; set; }

        public BacklogItemRepository(Context context)
        {
            this.context = context;
        }

        public void Add(BacklogItem item)
        {
            context.Add(item);
            context.SaveChanges();
        }

        public IEnumerable<BacklogItem> Get()
            => context.BacklogItems;
    }
}