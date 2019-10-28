using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TW2BOT.Library.Infrastructure;
using TW2BOT.Library.Services.Handlers;

namespace TW2BOT.Library.Commands.HUD
{
    public interface IBottomBarScreen
    {
        Task ClickWorldMapButton(CancellationToken ct);
        Task CloseAllWindows(CancellationToken ct);
    }

    public class BottomBarScreen : BaseScreen, IBottomBarScreen
    {
        private readonly IBattleReportScreen reportCommands;

        public BottomBarScreen(IBotConfig config, ILogger logger, IBattleReportScreen reportCommands)
            : base(config, logger)
        {
            this.reportCommands = reportCommands;
        }

        public async Task ClickWorldMapButton(CancellationToken ct)
        {
            try
            {
                logger.Log("Clicking search world map button...");
                config.Driver.FindElement(By.Id("world-map")).Click();
                await Task.Delay(random.Next(300, 1000));
            }
            catch (Exception ex)
            {
                logger.Log($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task CloseAllWindows(CancellationToken ct)
        {
            try
            {
                logger.Log("Closing all windows...");

                IReadOnlyCollection<IWebElement> closeButtonsCollection = config.Driver.FindElements(
                    By.XPath("//a[@ng-click='closeWindow()']"));
                IReadOnlyCollection<IWebElement> closeButtonsCollection2 = config.Driver.FindElements(
                    By.XPath("//a[@ng-click='closeWindow();']"));

                IWebElement[] closeButtons = new IWebElement[closeButtonsCollection.Count + closeButtonsCollection2.Count];

                // Convert to array
                int c = 0;
                foreach (IWebElement button in closeButtonsCollection)
                {
                    closeButtons[c] = button;
                    c++;
                }

                foreach (IWebElement button in closeButtonsCollection2)
                {
                    closeButtons[c] = button;
                    c++;
                }

                // Click them in reverse order
                for (int i = closeButtons.Length - 1; i >= 0; i--)
                {
                    closeButtons[i].Click();
                    await Task.Delay(random.Next(300, 400));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }
        }
    }
}
