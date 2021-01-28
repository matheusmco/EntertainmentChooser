using ChooseEntertainmentItem.Domain.Repositories;
using ChooseEntertainmentItem.Infra.Repositories.CSV;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoriesInstaller
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBacklogItemRepository, CsvBacklogItemRepository>();
            services.AddScoped<IDoneItemRepository, CsvDoneItemRepository>();
            return services;
        }
    }
}