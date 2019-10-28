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

namespace TW2BOT.Library.Commands
{
    public interface IPresetScreen
    {
        ReadOnlyCollection<IWebElement> GetAttackButtons();
    }

    public class PresetScreen : BaseScreen, IPresetScreen
    {
        public PresetScreen(IBotConfig config, ILogger logger)
            : base(config, logger)
        {
        }

        private void WaitUntilInfoScreenVisible()
        {
            try
            {
                logger.Log("Waiting until 'preset screen' is ready...");

                var wait = new WebDriverWait(config.Driver, new TimeSpan(0, 0, 0, 10));
                wait.Until
                    (
                        SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible
                        (
                            By.XPath("//a[@tooltip-content='Send attack']")
                        )
                    );
            }
            catch(Exception ex)
            {
                logger.LogError(ex);
            }
        }

        public ReadOnlyCollection<IWebElement> GetAttackButtons()
        {
            ReadOnlyCollection<IWebElement> attackButtons;

            try
            {
                WaitUntilInfoScreenVisible();

                logger.Log($"Fetching attack buttons...");
                attackButtons = config.Driver.FindElements(By.XPath("//a[@tooltip-content='Send attack']"));
            }
            catch (Exception ex)
            {
                logger.Log($"An error occurred: {ex.Message}");
                throw;
            }

            return attackButtons;
        }


    }
}
