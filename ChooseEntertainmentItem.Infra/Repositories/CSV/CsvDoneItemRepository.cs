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
    public class CsvDoneItemRepository : IDoneItemRepository
    {
        private readonly ItemsFilesNamesOptions options;

        public CsvDoneItemRepository(ItemsFilesNamesOptions options)
        {
            this.options = options;
        }

        public void Add(DoneItem item)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DoneItem> Get()
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