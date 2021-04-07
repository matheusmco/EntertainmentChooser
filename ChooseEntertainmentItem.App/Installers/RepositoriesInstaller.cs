using ChooseEntertainmentItem.Domain.Repositories;
using ChooseEntertainmentItem.Infra.EFContexts;
using ChooseEntertainmentItem.Infra.Repositories.EF;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoriesInstaller
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBacklogItemRepository, BacklogItemRepository>();
            services.AddScoped<IDoneItemRepository, DoneItemRepository>();
            // TODO: pass by options
            services.AddScoped<Context>(_ => new SqliteContext("Data Source=/home/matheus/repos/ChooseEntertainmentItem/items.db"));
            return services;
        }
    }
}