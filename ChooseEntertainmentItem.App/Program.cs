﻿using System;
using System.Linq;
using ChooseEntertainmentItem.Domain.Repositories;
using ChooseEntertainmentItem.Domain.Services;
using ChooseEntertainmentItem.Infra.Configs;
using ChooseEntertainmentItem.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChooseEntertainmentItem
{
    class Program
    {
        static string path;
        static IConfiguration configuration;

        static void Main(string[] args)
        {
            path = args[0];
            var serviceProvider = GetServiceProvider();
            var shouldIncludePrice = args.Count() >= 2 && args[1].ToUpper() == "S";
            var itemType = args.Count() < 3 ? "ALL" : args[2];

            var itemService = serviceProvider.GetService<IItemService>();

            var backlogItems = itemService.CalculateBacklogItemsPriority(shouldIncludePrice, itemType);

            foreach (var item in backlogItems.OrderBy(_ => _.Score).ThenBy(_ => _.Price))
                Console.WriteLine($"Name: {item.Name}, Score: {item.Score}");
        }

        private static ServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            return services.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();
            services.AddSingleton<ItemsFilesNamesOptions>(configuration.GetSection("ItemsFiles").Get<ItemsFilesNamesOptions>());

            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IItemRepository, CsvItemRepository>();
        }
    }
}