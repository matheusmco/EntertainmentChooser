using System;
using System.Linq;
using ChooseEntertainmentItem.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace ChooseEntertainmentItem;
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
            var services = GetServices();

            var itemService = services.GetService<IItemService>();

            var backlogItems = itemService.CalculateBacklogItemsPriority();

            // TODO: override to string
            foreach (var item in backlogItems.OrderBy(_ => _.GetFinalScore()).ThenBy(_ => _.Price))
                Console.WriteLine($"Name: {item.Name}, Score: {item.GetFinalScore()}");
        }
        catch (Exception ex)
        {
            logger.Fatal(ex, "Error on startup");
        }
    }

    private static ServiceProvider GetServices()
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