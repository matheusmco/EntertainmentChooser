using ChooseEntertainmentItem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChooseEntertainmentItem.Infra.EFContexts
{
    // TODO: create a base class
    // TODO: override using a new one
    public abstract class BaseContext : DbContext
    {
        public DbSet<BacklogItem> BacklogItems { get; set; }
        public DbSet<DoneItem> DoneItems { get; set; }
    }

    public class SqliteContext : BaseContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=items.db"); // TODO: pass data source in config
    }
}