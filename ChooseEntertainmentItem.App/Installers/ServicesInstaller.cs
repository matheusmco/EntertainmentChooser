using ChooseEntertainmentItem.Domain.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesInstaller
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            return services;
        }
    }
}