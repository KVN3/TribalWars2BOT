using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TW2BOT.Library.Infrastructure;

namespace TW2BOT.Library.Services
{
    public interface ITribalWarsConnectionService
    {
        bool EnterGame();
        bool Login();
        bool ConnectToFirstActiveWorld();
    }

    public class TribalWarsConnectionService : ITribalWarsConnectionService
    {
        private IBotConfig config;
        private Random random;

        public TribalWarsConnectionService(IBotConfig config)
        {
            this.config = config;
            this.random = new Random();
        }

        public bool EnterGame()
        {
            config.Driver.Navigate().GoToUrl(RouteNames.TribalWarsURL);

            bool success = Login();

            if (success)
                return ConnectToFirstActiveWorld();
            else
                return false;
        }

        public bool Login()
        {
            IWebElement loginDiv = config.Driver.FindElement(By.CssSelector(".input.user"));
            IWebElement loginInput = loginDiv.FindElement(By.ClassName("ng-pristine"));
            loginInput.SendKeys(RouteNames.Username);

            Thread.Sleep(random.Next(300, 1100));

            IWebElement passwordDiv = config.Driver.FindElement(By.CssSelector(".input.pw"));
            IWebElement passwordInput = passwordDiv.FindElement(By.ClassName("ng-pristine"));
            passwordInput.SendKeys(RouteNames.Password);

            Thread.Sleep(random.Next(600, 1100));

            config.Driver.FindElement(By.ClassName("button-login")).Click();

            return true;
        }

        public bool ConnectToFirstActiveWorld()
        {
            // Wait till it's visible
            var wait = new WebDriverWait(config.Driver, new TimeSpan(0, 0, 0, 10));
            wait.Until
                (
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible
                    (
                        By.CssSelector(".btn-orange.btn-border.small-icon")
                    )
                );

            Thread.Sleep(random.Next(300, 1400));

            // Click the first world button
            config.Driver.FindElements(By.CssSelector(".btn-orange.btn-border.small-icon"))[0].Click();

            return true;
        }
    }
}
