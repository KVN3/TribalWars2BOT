using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW2BOT.Library.Infrastructure;
using TW2BOT.Library.Models;
using TW2BOT.Library.Services.Handlers;

namespace TW2BOT.Library.Commands.HUD
{
    public interface ITopBarScreen
    {
        Dictionary<UnitType, int> GetUnitAmounts();
    }

    public class TopBarScreen : BaseScreen, ITopBarScreen
    {
        public TopBarScreen(IBotConfig config, ILogger logger)
            : base(config, logger)
        {

        }

        public Dictionary<UnitType, int> GetUnitAmounts()
        {
            Dictionary<UnitType, int> unitAmounts = new Dictionary<UnitType, int>();

            try
            {
                ReadOnlyCollection<IWebElement> unitListings = config.Driver.FindElements(By.XPath("//li[@ng-repeat='unitName in ::unitOrder']"));

                for (int i = 0; i < unitListings.Count; i++)
                {
                    string amountText = unitListings[i].FindElement(By.ClassName("amount-border")).FindElement(By.ClassName("amount")).Text;

                    UnitType unit = (UnitType)i;
                    int amount = Int32.Parse(amountText);
                    unitAmounts.Add(unit, amount);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }

            return unitAmounts;
        }
    }
}
