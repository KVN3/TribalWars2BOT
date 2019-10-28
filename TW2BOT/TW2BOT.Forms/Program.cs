using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TW2BOT.Forms.Controllers;
using TW2BOT.Forms.Infrastructure;
using TW2BOT.Library.Infrastructure;

namespace TW2BOT.Forms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IMainForm mainForm = DIContainer.Instance.GetService<IMainForm>();
            MainForm form = (MainForm) mainForm;
            Application.Run(form);
        }
    }
}
