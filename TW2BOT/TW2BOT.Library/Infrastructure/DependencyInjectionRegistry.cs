using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW2BOT.Library.Commands;
using TW2BOT.Library.Commands.HUD;
using TW2BOT.Library.Commands.Windows;
using TW2BOT.Library.Services;
using TW2BOT.Library.Services.Handlers;

namespace TW2BOT.Library.Infrastructure
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddTribalWarsBotServices(this IServiceCollection services)
        {
            // Services
            services.AddSingleton<ITribalWarsConnectionService, TribalWarsConnectionService>();
            services.AddSingleton<ITribalWarsFarmingService, TribalWarsFarmingService>();

            // Screens
            services.AddSingleton<IBottomBarScreen, BottomBarScreen>();
            services.AddSingleton<ITopBarScreen, TopBarScreen>();

            services.AddSingleton<IPresetScreen, PresetScreen>();
            services.AddSingleton<IBattleReportScreen, BattleReportScreen>();
            services.AddSingleton<ISearchWorldMapScreen, SearchWorldMapScreen>();
            services.AddSingleton<IVillageInfoScreen, VillageInfoScreen>();
            services.AddSingleton<IWorldMapScreen, WorldMapScreen>();

            // Config
            services.AddSingleton<IBotConfig, BotConfig>();
            services.AddSingleton<ILogger, Logger>();

            return services;
        }
    }
}
