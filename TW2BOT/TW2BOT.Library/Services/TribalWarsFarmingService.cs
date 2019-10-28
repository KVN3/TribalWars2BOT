using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TW2BOT.Library.Commands;
using TW2BOT.Library.Commands.HUD;
using TW2BOT.Library.Commands.Windows;
using TW2BOT.Library.Infrastructure;
using TW2BOT.Library.Models;
using TW2BOT.Library.Services.Handlers;

namespace TW2BOT.Library.Services
{
    public interface ITribalWarsFarmingService
    {
        bool IsServiceOn { get; set; }
        bool IsRunning { get; set; }

        Task RunAsync(CancellationToken ct);
    }

    public class TribalWarsFarmingService : ITribalWarsFarmingService
    {
        public bool IsServiceOn { get; set; }
        public bool IsRunning { get; set; }

        private Random random;
        private readonly IBotConfig config;
        private readonly IWorldMapScreen worldMapScreen;
        private readonly IBottomBarScreen bottomBarScreen;
        private readonly ISearchWorldMapScreen searchWorldMapScreen;
        private readonly IVillageInfoScreen villageInfoScreen;
        private readonly IBattleReportScreen battleReportScreen;
        private readonly ITopBarScreen topBarScreen;
        private readonly IPresetScreen presetScreen;
        private readonly ILogger logger;
        private Dictionary<UnitType, int> unitAmounts;

        private List<int> unavailablePresetIds;

        public TribalWarsFarmingService(IBotConfig config, IWorldMapScreen worldMapScreen, IBottomBarScreen bottomBarScreen, ISearchWorldMapScreen searchWorldMapScreen,
            IVillageInfoScreen villageInfoScreen, IBattleReportScreen battleReportScreen, ITopBarScreen topBarScreen, IPresetScreen presetScreen, ILogger logger)
        {
            IsServiceOn = false;
            IsRunning = false;

            random = new Random();
            this.config = config;
            this.worldMapScreen = worldMapScreen;
            this.bottomBarScreen = bottomBarScreen;
            this.searchWorldMapScreen = searchWorldMapScreen;
            this.villageInfoScreen = villageInfoScreen;
            this.battleReportScreen = battleReportScreen;
            this.topBarScreen = topBarScreen;
            this.presetScreen = presetScreen;
            this.logger = logger;
        }

        public async Task RunAsync(CancellationToken ct)
        {
            IsRunning = true;

            try
            {
                while (IsServiceOn)
                {
                    unavailablePresetIds = new List<int>();

                    unitAmounts = topBarScreen.GetUnitAmounts();

                    if (EnoughUnitsToSendAHalfPreset())
                        await PerformFarmingCycle(ct);
                    else
                        logger.Log("Not enough units to send at least a half preset - skipping cycle.");

                    if (!IsServiceOn)
                    {
                        logger.Log($"Farming service is set to off. Breaking out of while loop.");
                        break;
                    }

                    // Min 10 mins, max 24 mins wait before new cycle
                    int ms = random.Next(60000 * 2, 60000 * 5);

                    DateTime nextCycle = DateTime.Now.AddMilliseconds(ms);
                    logger.Log($"Cycle complete, pausing thread. Next village-loop at: {nextCycle.ToShortTimeString()}");

                    await Task.Delay(ms, ct);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                IsRunning = false;
            }
        }

        private bool EnoughUnitsToSendAHalfPreset()
        {
            foreach (ArmyPreset preset in config.ArmyPresets)
            {
                if (unitAmounts[preset.unit] >= (preset.amount / 2))
                    return true;
            }

            return false;
        }

        private async Task PerformFarmingCycle(CancellationToken ct)
        {
            await bottomBarScreen.CloseAllWindows(ct);

            foreach (List<Village> villages in config.FarmVillages)
            {
                // Here we shuffle the villages, so it's less bot-like
                logger.Log("Shuffling villages...");
                villages.Shuffle();
                logger.Log("------------------------------------------------------------------------------------");

                foreach (Village vill in villages)
                {
                    logger.Log($"Processing request for {vill.name} ({vill.coordinates.x}|{vill.coordinates.y})...");

                    if (!IsServiceOn)
                    {
                        logger.Log($"Farming service is set to off. Breaking out of village loop.");
                        break;
                    }

                    if (unavailablePresetIds.Count == config.ArmyPresets.Count)
                    {
                        logger.Log($"Not enough units available. Breaking out of village loop.");
                        break;
                    }

                    unitAmounts = topBarScreen.GetUnitAmounts();

                    await TryToAttack(vill, ct);
                    await Task.Delay(random.Next(500, 1000), ct);
                    await bottomBarScreen.CloseAllWindows(ct);
                    logger.Log("------------------------------------------------------------------------------------");
                }
            }

            await bottomBarScreen.CloseAllWindows(ct);
        }

        private async Task TryToAttack(Village village, CancellationToken ct)
        {
            // Locate the village and open it's information
            await bottomBarScreen.ClickWorldMapButton(ct);
            await searchWorldMapScreen.SelectVillage(village.coordinates, ct);
            await worldMapScreen.ClickVillageInfoButton(ct);

            // Only send the order if there's no current attack on the way and we should attack
            if (!villageInfoScreen.IsAttacking() && await ShouldAttack(ct))
            {
                await bottomBarScreen.CloseAllWindows(ct);
                await TrySendingAttackOrder(ct);
            }
        }

        /// <summary>
        /// Determines if this village should be attacked.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> ShouldAttack(CancellationToken ct)
        {
            ReadOnlyCollection<IWebElement> reports = villageInfoScreen.GetLastReports();

            // Previous attacks, predict if it's worth to attack based on the most recent report
            if (reports.Count > 0)
            {
                // We click the first report and read it
                reports[0].Click();
                BattleReport report = battleReportScreen.ReadReport();

                await Task.Delay(random.Next(450, 700), ct);

                return report.ShouldAttackAgain();
            }

            // No previous attacks means attack.
            return true;
        }

        private async Task TrySendingAttackOrder(CancellationToken ct)
        {
            await worldMapScreen.ClickPresetsButton(ct);

            ReadOnlyCollection<IWebElement> attackButtons = presetScreen.GetAttackButtons();

            for (int i = 0; i < config.ArmyPresets.Count; i++)
            {
                if (unavailablePresetIds.Contains(i))
                {
                    logger.Log($"Skipping preset, marked unavailable: {i}");
                    continue;
                }

                // More units required than available
                if (config.ArmyPresets[i].amount > unitAmounts[config.ArmyPresets[i].unit])
                {
                    logger.Log($"Not enough units for full preset, marking unavailable: {i}");
                    unavailablePresetIds.Add(i);

                    // We don't want to send the attack if we have less than half available
                    if ((config.ArmyPresets[i].amount / 2) > unitAmounts[config.ArmyPresets[i].unit])
                    {
                        logger.Log($"Less than half of the full preset available, not using this preset: {i}");
                        continue;
                    }
                }
                else
                {
                    // In the scenario that an attack was send that exhausted the pool for this preset
                    unitAmounts[config.ArmyPresets[i].unit] -= config.ArmyPresets[i].amount;
                    if ((config.ArmyPresets[i].amount / 2) > unitAmounts[config.ArmyPresets[i].unit])
                    {
                        logger.Log($"This attack exhausted the unit pool (less than half) for this preset, marking it unavailable: {i}");
                        unavailablePresetIds.Add(i);
                    }
                }

                // We send the attack and break out of the loop
                await Task.Delay(random.Next(100, 300), ct);
                logger.Log($"Clicking attack button for preset: {i}");
                attackButtons[i].Click();
                break;
            }
        }
    }
}
