using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pilgrimage;

public static class ServiceExtensions
{
    public static IServiceCollection AddPilgrimage(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IInventoryService, InventoryService>()
            .AddTransient<IInventoryFileStorage, InventoryLocalUserStorage>()
            .AddTransient<IInventorySerializer, InventorySerializer>()
            .AddSingleton<IQuestService, QuestService>()
            .AddSingleton<IItemService, ItemService>();

        return services;
    }
}
