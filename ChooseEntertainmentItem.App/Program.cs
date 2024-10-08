﻿using System;
using System.Linq;
using ChooseEntertainmentItem.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace ChooseEntertainmentItem
{
    class Program
    {
        static Logger logger;

        static void Main(string[] args)
        {
            logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("log/error.log", restrictedToMinimumLevel: LogEventLevel.Error)
                .WriteTo.File("log/log.log", restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

            try
            {
                var serviceProvider = GetServiceProvider();
                var shouldIncludePrice = args.Count() >= 1 && args[0].ToUpper() == "S";
                var itemType = args.Count() < 2 ? "ALL" : args[1];

                var itemService = serviceProvider.GetService<IItemService>();

                var backlogItems = itemService.CalculateBacklogItemsPriority(shouldIncludePrice, itemType);

                // TODO: create a map
                foreach (var item in backlogItems.OrderBy(_ => _.GetFinalScore()).ThenBy(_ => _.Price))
                    Console.WriteLine($"Name: {item.Name}, Score: {item.GetFinalScore()}");
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Error on startup");
            }
        }

        private static ServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            return services.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(_ =>
            {
                _.AddSerilog(logger: logger, dispose: true);
            });

            services.AddCustomOptions();
            services.AddServices();
            services.AddRepositories();
        }
    }
}