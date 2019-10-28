using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TW2BOT.Library.Infrastructure;
using TW2BOT.Library.Models;
using TW2BOT.Library.Services.Handlers;

namespace TW2BOT.Library.Commands
{
    public interface IWorldMapScreen
    {
        Task ClickVillageInfoButton(CancellationToken ct);
        Task ClickPresetsButton(CancellationToken ct);
    }

    public class WorldMapScreen : BaseScreen, IWorldMapScreen
    {
        private readonly IBattleReportScreen reportCommands;

        public WorldMapScreen(IBotConfig config, ILogger logger, IBattleReportScreen reportCommands)
            : base(config, logger)
        {
            this.reportCommands = reportCommands;
        }

        public async Task ClickVillageInfoButton(CancellationToken ct)
        {
            try
            {
                logger.Log("Waiting for village info button to appear...");
                var wait = new WebDriverWait(config.Driver, new TimeSpan(0, 0, 0, 10));
                wait.Until
                    (
                        SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible
                        (
                            By.XPath("//div[@tooltip-content='Village Information']")
                        )
                    );

                logger.Log("Clicking village info button...");
                config.Driver.FindElement(By.XPath("//div[@tooltip-content='Village Information']")).Click();

                await Task.Delay(random.Next(300, 600));
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }
        }

        public async Task ClickPresetsButton(CancellationToken ct)
        {
            try
            {
                logger.Log("Waiting for 'presets' button to appear...");
                var wait = new WebDriverWait(config.Driver, new TimeSpan(0, 0, 0, 10));
                wait.Until
                    (
                        SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible
                        (
                            By.XPath("//div[@tooltip-content='Presets']")
                        )
                    );

                logger.Log("Clicking 'presets' button...");
                config.Driver.FindElement(By.XPath("//div[@tooltip-content='Presets']")).Click();

                await Task.Delay(random.Next(400, 700));
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }
        }
    }
}
