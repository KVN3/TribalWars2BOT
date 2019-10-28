using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TW2BOT.Library.Infrastructure;
using TW2BOT.Library.Models;
using TW2BOT.Library.Services.Handlers;

namespace TW2BOT.Library.Commands.HUD
{
    public interface ISearchWorldMapScreen
    {
        Task SelectVillage(Coordinates coordinates, CancellationToken ct);
        Task ClickJumpToCoordinatesButton(CancellationToken ct);
    }

    public class SearchWorldMapScreen : BaseScreen, ISearchWorldMapScreen
    {
        public SearchWorldMapScreen(IBotConfig config, ILogger logger)
            : base(config, logger)
        {
        }

        public async Task SelectVillage(Coordinates coordinates, CancellationToken ct)
        {
            try
            {
                logger.Log($"Selecting village ({coordinates.x}|{coordinates.y})...");
                WaitUntilSearchVillageWindowReady();
                await InputCoordinates(coordinates, ct);
                await ClickJumpToCoordinatesButton(ct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }
        }

        private void WaitUntilSearchVillageWindowReady()
        {
            try
            {
                logger.Log("Waiting until 'search village window' is ready...");

                var wait = new WebDriverWait(config.Driver, new TimeSpan(0, 0, 0, 10));
                wait.Until
                    (
                        SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible
                        (
                            By.XPath("//input[@ng-model='coordinates.x']")
                        )
                    );
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }
        }

        public async Task ClickJumpToCoordinatesButton(CancellationToken ct)
        {
            try
            {
                logger.Log("Clicking 'jump to coords' button...");
                config.Driver.FindElement(By.XPath("//div[@ng-click='jumpTo(coordinates)']")).Click();
                await Task.Delay(random.Next(300, 1000));
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }
        }

        private async Task InputCoordinates(Coordinates coordinates, CancellationToken ct)
        {
            try
            {
                logger.Log($"Inputting coordinates ({coordinates.x}|{coordinates.y})...");
                IWebElement inputX = config.Driver.FindElement(By.XPath("//input[@ng-model='coordinates.x']"));
                inputX.Clear();
                inputX.SendKeys(coordinates.x.ToString());
                await Task.Delay(random.Next(300, 550));

                IWebElement inputY = config.Driver.FindElement(By.XPath("//input[@ng-model='coordinates.y']"));
                inputY.Clear();
                inputY.SendKeys(coordinates.y.ToString());
                await Task.Delay(random.Next(300, 550));
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }
        }
    }
}
