using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW2BOT.Library.Commands;
using TW2BOT.Library.Infrastructure;

namespace TW2BOT.Console
{


    public class MainController
    {
        private readonly IWebDriver _driver;
        private readonly TribalWarsConnectionService _tw;

        public MainController()
        {
            Debug.WriteLine("Starting program.");

            _driver = new FirefoxDriver(RouteNames.DriverPath);
            _driver.Manage().Timeouts().PageLoad = new TimeSpan(0, 0, 0, 10);

            _tw = new TribalWarsConnectionService(_driver);
        }

        public void Start()
        {
            bool success = _tw.EnterGame();
        }
    }
}
