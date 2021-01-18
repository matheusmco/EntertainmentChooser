using ChooseEntertainmentItem.Infra.Configs;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OptionsInstaller
    {
        public static IServiceCollection AddCustomOptions(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();
            services.AddSingleton<ItemsFilesNamesOptions>(configuration.GetSection("ItemsFiles").Get<ItemsFilesNamesOptions>());

            return services;
        }
    }
}