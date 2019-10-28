using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW2BOT.Library.Services;
using TW2BOT.Library.Infrastructure;
using TW2BOT.Forms.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using TW2BOT.Library.Services.Handlers;
using TW2BOT.Library.Commands.HUD;

namespace TW2BOT.Forms.Controllers
{
    public class MainController
    {
        private readonly IBotConfig config;
        private readonly ITribalWarsConnectionService connectionService;
        private readonly ITribalWarsFarmingService farmingService;
        private readonly ILogger logger;
        private readonly IBottomBarScreen bottomBarScreen;

        private CancellationTokenSource cts;

        public bool Connected { get; set; }

        public MainController()
        {
            Debug.WriteLine("Starting program.");

            this.config = DIContainer.Instance.GetService<IBotConfig>();
            this.connectionService = DIContainer.Instance.GetService<ITribalWarsConnectionService>();
            this.farmingService = DIContainer.Instance.GetService<ITribalWarsFarmingService>();
            this.logger = DIContainer.Instance.GetService<ILogger>();
            this.bottomBarScreen = DIContainer.Instance.GetService<IBottomBarScreen>();

            config.Driver.Manage().Timeouts().PageLoad = new TimeSpan(0, 0, 0, 10);
        }

        public void ConnectToGame()
        {
            Connected = connectionService.EnterGame();
        }

        public bool ToggleFarmingService()
        {
            farmingService.IsServiceOn = !farmingService.IsServiceOn;

            if (!farmingService.IsRunning && farmingService.IsServiceOn)
            {
                cts = new CancellationTokenSource();
                farmingService.RunAsync(cts.Token);
            }

            if (farmingService.IsRunning && !farmingService.IsServiceOn)
            {
                cts.Cancel();
                farmingService.IsRunning = false;
            }

            return farmingService.IsServiceOn;
        }

        public async Task RestartFarmingService()
        {
            config.Driver.Navigate().Refresh();

            logger.Log("Restarting thread...");
            farmingService.IsServiceOn = false;
            farmingService.IsRunning = false;
            cts.Cancel();

            await Task.Delay(5000);

            cts = new CancellationTokenSource();
            _ = farmingService.RunAsync(cts.Token);
        }

        public async Task CloseWindows()
        {
            await bottomBarScreen.CloseAllWindows(new CancellationTokenSource().Token);
        }
    }
}
