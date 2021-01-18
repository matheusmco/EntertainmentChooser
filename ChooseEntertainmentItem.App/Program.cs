using System;
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
        static string path;
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
                path = args[0];
                var serviceProvider = GetServiceProvider();
                var shouldIncludePrice = args.Count() >= 2 && args[1].ToUpper() == "S";
                var itemType = args.Count() < 3 ? "ALL" : args[2];

                var itemService = serviceProvider.GetService<IItemService>();

                var backlogItems = itemService.CalculateBacklogItemsPriority(shouldIncludePrice, itemType);

                foreach (var item in backlogItems.OrderBy(_ => _.Score).ThenBy(_ => _.Price))
                    Console.WriteLine($"Name: {item.Name}, Score: {item.Score}");
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