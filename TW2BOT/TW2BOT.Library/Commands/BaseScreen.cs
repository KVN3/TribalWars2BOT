using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW2BOT.Library.Infrastructure;
using TW2BOT.Library.Services.Handlers;

namespace TW2BOT.Library.Commands
{
    public class BaseScreen
    {
        protected IBotConfig config;
        protected readonly ILogger logger;
        protected Random random;

        public BaseScreen(IBotConfig config, ILogger logger)
        {
            this.config = config;
            this.logger = logger;
            this.random = new Random();
        }
    }
}
