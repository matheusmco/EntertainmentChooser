using ChooseEntertainmentItem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChooseEntertainmentItem.Infra.EFContexts
{
    public abstract class Context : DbContext
    {
        protected virtual string ConnectionString { get; set; }

        public DbSet<BacklogItem> BacklogItems { get; set; }
        public DbSet<DoneItem> DoneItems { get; set; }
    }

    public class SqliteContext : Context
    {

        public SqliteContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(ConnectionString);
            // => options.UseSqlite("Data Source=/home/matheus/repos/ChooseEntertainmentItem/items.db");
    }
}