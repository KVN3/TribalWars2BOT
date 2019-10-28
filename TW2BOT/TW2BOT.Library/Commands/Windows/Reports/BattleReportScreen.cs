using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW2BOT.Library.Infrastructure;
using TW2BOT.Library.Models;
using TW2BOT.Library.Services.Handlers;

namespace TW2BOT.Library.Commands
{
    public interface IBattleReportScreen
    {
        void WaitUntilReportReady();
        string ReadTimeOfAttack();
        bool IsFullHaul();
        BattleReport ReadReport();
    }

    public class BattleReportScreen : BaseScreen, IBattleReportScreen
    {
        public BattleReportScreen(IBotConfig config, ILogger logger)
            : base(config, logger)
        {

        }

        public void WaitUntilReportReady()
        {
            try
            {
                logger.Log("Waiting until report is ready...");

                // Wait till it's visible
                var wait = new WebDriverWait(config.Driver, new TimeSpan(0, 0, 0, 10));
                wait.Until
                    (
                        SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible
                        (
                            By.ClassName("report-title")
                        )
                    );
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }
        }

        public bool IsFullHaul()
        {
            try
            {
                return SeleniumExtension.IsElementPresent(By.XPath("//div[@ng-if='report.haul === HAUL_TYPES.FULL']"), config.Driver);
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }

            return false;
        }

        public string ReadTimeOfAttack()
        {
            string timeOfAttack = "";

            try
            {
                timeOfAttack = config.Driver.FindElement(By.ClassName("report-date")).Text;
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }

            return timeOfAttack;
        }

        public BattleReport ReadReport()
        {
            WaitUntilReportReady();

            BattleReport battleReport = null;

            try
            {
                logger.Log("Reading report...");
                bool isFullHaul = IsFullHaul();
                string timeOfAttack = ReadTimeOfAttack();
                battleReport = new BattleReport(new Village(), timeOfAttack, isFullHaul);
            }
            catch (Exception ex)
            {
                logger.Log($"An error occurred: {ex.Message}");
                throw;
            }


            return battleReport;
        }

        public async Task ClickAttackAgainButton()
        {
            try
            {
                logger.Log("Clicking attack again button...");
                config.Driver.FindElement(By.XPath("//a[@ng-click='attackAgain()']")).Click();
                await Task.Delay(random.Next(400, 1200));
            }
            catch (Exception ex)
            {
                logger.Log($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
