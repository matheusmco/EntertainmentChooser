using ChooseEntertainmentItem.Domain.Repositories;
using ChooseEntertainmentItem.Infra.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoriesInstaller
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IItemRepository, CsvItemRepository>();
            return services;
        }
    }
}