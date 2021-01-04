using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ChooseEntertainmentItem.Domain.Services.Contracts;
using ChooseEntertainmentItem.Domain.Services.Impl;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace ChooseEntertainmentItem
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = GetServiceProvider();

            var path = args[0]; // TODO: send by DI to repository
            var shouldIncludePrice = args.Count() >= 2 && args[1].ToUpper() == "S";
            var itemType = args.Count() < 3 ? "ALL" : args[2];
            // var tags = new Dictionary<string, int>();

            var itemService = serviceProvider.GetService<IItemService>();

            var items = itemService.CalculateBacklogItemsPriority(shouldIncludePrice);

            // TODO: move to Domain

            // TODO: use repository
            // using (var reader = new StreamReader($"{path}/doneItems.csv"))
            // using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            // {
            //     var doneItems = csv.GetRecords<DoneItem>().ToList();

            //     var peso = doneItems.Count();
            //     foreach (var item in doneItems.OrderByDescending(_ => _.DoneDate))
            //     {
            //         foreach (var tag in item.Tags.Split('/'))
            //         {
            //             if (tags.Keys.Contains(tag))
            //                 tags[tag] += peso;
            //             else
            //                 tags.Add(tag, peso);
            //         }
            //         peso--;
            //     }
            // }

            // using (var reader = new StreamReader($"{path}/backlogItems.csv"))
            // using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            // {
            //     var backlogItems = csv.GetRecords<BacklogItem>().ToList();
            //     if (itemType != "ALL")
            //         backlogItems = backlogItems.Where(_ => _.Tags.Split('/').Contains(itemType)).ToList();

            //     foreach (var item in backlogItems)
            //     {
            //         foreach (var tag in tags.Keys)
            //         {
            //             if (item.Tags.Split('/').ToList().Contains(tag))
            //                 item.Score += tags[tag];
            //         }
            //         if (isIncludePrice)
            //             item.Score += (int)item.Price;
            //     }

            //     foreach (var item in backlogItems.OrderBy(_ => _.Score).ThenBy(_ => _.Price))
            //         Console.WriteLine($"Name: {item.Name}, Score: {item.Score}");
            // }
        }

        private static ServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            return services.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
        }
    }
}