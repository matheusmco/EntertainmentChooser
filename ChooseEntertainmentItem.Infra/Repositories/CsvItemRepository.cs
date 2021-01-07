using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ChooseEntertainmentItem.Domain.Models;
using ChooseEntertainmentItem.Domain.Repositories;
using ChooseEntertainmentItem.Infra.Mappers;
using CsvHelper;

namespace ChooseEntertainmentItem.Infra.Repositories
{
    public class CsvItemRepository : IItemRepository
    {
        private readonly string backlogFilePath;
        private readonly string doneFilePath;

        public CsvItemRepository(string backlogFilePath, string doneFilePath)
        {
            this.backlogFilePath = backlogFilePath;
            this.doneFilePath = doneFilePath;
        }

        public IEnumerable<BacklogItem> GetBacklogItems()
        {
            using (var reader = new StreamReader(backlogFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<BacklogItemMap>();
                return csv.GetRecords<BacklogItem>().ToList();
            }
        }

        public IEnumerable<DoneItem> GetDoneItems()
        {
            using (var reader = new StreamReader(doneFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<DoneItemMap>();
                return csv.GetRecords<DoneItem>().ToList();
            }
        }
    }
}