using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW2BOT.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainController = new MainController();
            mainController.Start();
        }
    }
}
