using ChooseEntertainmentItem.Domain.Models;
using CsvHelper.Configuration;

namespace ChooseEntertainmentItem.Infra.Mappers
{
    public class BacklogItemMap : ClassMap<BacklogItem>
    {
        public BacklogItemMap()
        {
            Map(_ => _.Name).Name("Name");
            Map(_ => _.Tags).Name("Tags");
            Map(_ => _.Price).Name("Price");
            Map(_ => _.RawTier).Name("Tier");
        }
    }

    public class DoneItemMap : ClassMap<DoneItem>
    {
        public DoneItemMap()
        {
            Map(_ => _.Name).Name("Name");
            Map(_ => _.Tags).Name("Tags");
            Map(_ => _.DoneDate).Name("DoneDate");
        }
    }
}