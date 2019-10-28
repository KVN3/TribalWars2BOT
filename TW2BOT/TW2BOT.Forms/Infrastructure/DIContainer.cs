using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW2BOT.Forms.Controllers;
using TW2BOT.Library.Infrastructure;

namespace TW2BOT.Forms.Infrastructure
{
    public sealed class DIContainer
    {
        private static readonly IServiceProvider _instance = Build();
        public static IServiceProvider Instance => _instance;

        static DIContainer()
        {

        }

        private DIContainer()
        {

        }

        private static IServiceProvider Build()
        {
            var services = new ServiceCollection();

            // Add the bot services
            services.AddTribalWarsBotServices();


            services.AddSingleton<IMainForm, MainForm>();

            return services.BuildServiceProvider();
        }
    }
}
