using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW2BOT.Library.Infrastructure;
using TW2BOT.Library.Services.Handlers;

namespace TW2BOT.Library.Commands.Windows
{
    public interface IVillageInfoScreen
    {
        ReadOnlyCollection<IWebElement> GetLastReports();

        bool IsAttacking();
    }

    public class VillageInfoScreen : BaseScreen, IVillageInfoScreen
    {
        public VillageInfoScreen(IBotConfig config, ILogger logger)
            : base(config, logger)
        {

        }

        public ReadOnlyCollection<IWebElement> GetLastReports()
        {
            try
            {
                WaitUntilInfoScreenVisible();

                logger.Log("Fetching last reports off the village info screen...");
                ReadOnlyCollection<IWebElement> reports =
                    config.Driver.FindElements(By.XPath("//div[@ng-click='showReport(report.id, false, true)']"));
                logger.Log($"Reports found: {reports.Count}");
                return reports;
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }

            return null;
        }

        private void WaitUntilInfoScreenVisible()
        {
            try
            {
                logger.Log("Waiting until 'village info screen' is ready...");

                var wait = new WebDriverWait(config.Driver, new TimeSpan(0, 0, 0, 10));
                wait.Until
                    (
                        SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible
                        (
                            By.Id("village-info")
                        )
                    );
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }
        }

        public bool IsAttacking()
        {
            bool isAttacking = true;

            try
            {
                logger.Log("Checking for attacks already underway...");
                isAttacking = SeleniumExtension.IsElementPresent(By.XPath("//canvas[@tooltip-content='Attacking']"), config.Driver);
                logger.Log($"Already attacking: {isAttacking}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }

            return isAttacking;
        }
    }
}
