using ChooseEntertainmentItem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChooseEntertainmentItem.Infra.Data.EF
{
    public class EFContext : DbContext
    {
        DbSet<BacklogItem> BacklogItems { get; set; }
        DbSet<DoneItem> DoneItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=/home/matheus/repos/ChooseEntertainmentItem/items.db"); // TODO: use options to get ConnectionString

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BacklogItem>().Ignore(_ => _.Score);
        }
    }
}