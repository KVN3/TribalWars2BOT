using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW2BOT.Library.Services.Handlers
{
    public static class RandomHandler
    {
        private static Random random = new Random();

        public static int TinyWaitDuration()
        {
            return random.Next(500, 1300);
        }

        public static int SmallWaitDuration()
        {
            return random.Next(500, 1300);
        }
    }
}
