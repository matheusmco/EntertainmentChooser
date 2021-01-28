using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ChooseEntertainmentItem.Domain.Models;
using ChooseEntertainmentItem.Domain.Repositories;
using ChooseEntertainmentItem.Infra.Configs;
using ChooseEntertainmentItem.Infra.Mappers;
using CsvHelper;

namespace ChooseEntertainmentItem.Infra.Repositories.CSV
{
    public class CsvBacklogItemRepository : IBacklogItemRepository
    {
        private readonly ItemsFilesNamesOptions options;

        public CsvBacklogItemRepository(ItemsFilesNamesOptions options)
        {
            this.options = options;
        }

        public void Add(BacklogItem item)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<BacklogItem> Get()
        {
            using (var reader = new StreamReader(Path.Combine(options.Path, options.BacklogFileName)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<BacklogItemMap>();
                return csv.GetRecords<BacklogItem>().ToList();
            }
        }
    }
}