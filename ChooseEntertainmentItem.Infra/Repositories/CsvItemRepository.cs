using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ChooseEntertainmentItem.Domain.Configs;
using ChooseEntertainmentItem.Domain.Models;
using ChooseEntertainmentItem.Domain.Repositories;
using ChooseEntertainmentItem.Infra.Mappers;
using CsvHelper;

namespace ChooseEntertainmentItem.Infra.Repositories
{
    public class CsvItemRepository : IItemRepository
    {
        private readonly ItemsFilesNamesOptions options;

        public CsvItemRepository(ItemsFilesNamesOptions options)
        {
            this.options = options;
        }

        public IEnumerable<BacklogItem> GetBacklogItems()
        {
            using (var reader = new StreamReader(Path.Combine(options.Path, options.BacklogFileName)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<BacklogItemMap>();
                return csv.GetRecords<BacklogItem>().ToList();
            }
        }

        public IEnumerable<DoneItem> GetDoneItems()
        {
            using (var reader = new StreamReader(Path.Combine(options.Path, options.DoneFileName)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<DoneItemMap>();
                return csv.GetRecords<DoneItem>().ToList();
            }
        }
    }
}