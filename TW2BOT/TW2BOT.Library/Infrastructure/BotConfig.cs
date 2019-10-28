using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW2BOT.Library.Models;

namespace TW2BOT.Library.Infrastructure
{
    public interface IBotConfig
    {
        IWebDriver Driver { get; }

        List<ArmyPreset> ArmyPresets { get; }

        List<List<Village>> FarmVillages { get; }
    }

    public class BotConfig : IBotConfig
    {
        public IWebDriver Driver { get; }
        public List<ArmyPreset> ArmyPresets { get; }

        // Priority-based listing allows us to 
        public List<List<Village>> FarmVillages { get; }


        public BotConfig()
        {
            Driver = new FirefoxDriver(RouteNames.DriverPath);

            ArmyPresets = new List<ArmyPreset>();
            FarmVillages = new List<List<Village>>();
            InitArmyPresets();
            InitFarmVillages();
        }

        private void InitArmyPresets()
        {
            ArmyPresets.Add(new ArmyPreset(UnitType.Spear, 25));
            ArmyPresets.Add(new ArmyPreset(UnitType.Axe, 40));
        }

        private void InitFarmVillages()
        {
            List<Village> primaryTargets = new List<Village>();
            primaryTargets.Add(new Village("Brannon's village", new Coordinates(497, 576)));
            primaryTargets.Add(new Village("Barbarian village", new Coordinates(496, 575)));
            primaryTargets.Add(new Village("Barbarian village", new Coordinates(496, 578)));
            primaryTargets.Add(new Village("Barbarian village", new Coordinates(496, 579)));
            primaryTargets.Add(new Village("Barbarian village", new Coordinates(493, 576)));
            primaryTargets.Add(new Village("Barbarian village", new Coordinates(492, 577)));
            primaryTargets.Add(new Village("Barbarian village", new Coordinates(501, 579)));
            primaryTargets.Add(new Village("Barbarian village", new Coordinates(502, 576)));
            primaryTargets.Add(new Village("Barbarian village", new Coordinates(490, 583)));
            primaryTargets.Add(new Village("Barbarian village", new Coordinates(490, 580)));
            primaryTargets.Add(new Village("Barbarian village", new Coordinates(492, 581)));
            FarmVillages.Add(primaryTargets);

            List<Village> secundaryTargets = new List<Village>();
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(501, 579)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(502, 581)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(502, 576)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(503, 577)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(492, 581)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(502, 572)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(501, 571)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(502, 570)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(502, 572)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(502, 570)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(502, 568)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(500, 567)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(500, 570)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(498, 571)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(498, 572)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(498, 570)));
            secundaryTargets.Add(new Village("Barbarian village", new Coordinates(496, 568)));
            FarmVillages.Add(secundaryTargets);
        }
    }
}
