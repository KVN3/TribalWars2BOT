using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TW2BOT.Forms.Controllers;
using TW2BOT.Forms.Infrastructure;
using TW2BOT.Library.Infrastructure;
using TW2BOT.Library.Models;
using TW2BOT.Library.Services.Handlers;

namespace TW2BOT.Forms
{
    public interface IMainForm
    {
        void UpdateListView(List<string> logs);
        void AddToListView(string log);
    }

    public partial class MainForm : Form, IMainForm
    {
        private readonly MainController controller;

        private readonly ILogger logger;

        public MainForm()
        {
            InitializeComponent();
            Initialize();

            this.logger = DIContainer.Instance.GetService<ILogger>();
            logger.LogsChanged += OnLogsChanged;
            logger.ErrorCaught += OnErrorCaught;

            this.controller = new MainController();

            //new BattleReport(new Village(), "Today at 12:39:38 PM", true);
        }

        public void OnLogsChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
                Invoke(new EventHandler(OnLogsChanged),
                            new object[] { sender, e });
            else
            {
                MyEventArgs args = (MyEventArgs)e;
                AddToListView(args.message);
            }
        }

        public void OnErrorCaught(object sender, EventArgs e)
        {
            if (InvokeRequired)
                Invoke(new EventHandler(OnLogsChanged),
                            new object[] { sender, e });
            else
            {
                // Restart thread
                bool isOn = controller.ToggleFarmingService();
            }
        }

        private void Initialize()
        {
            UpdateFarmingPreview(false);

            listView_logging.TileSize = new Size(listView_logging.Width - 15, 15);
            listView_logging.View = View.Tile;
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Btn_toggleFarmingService_Click(object sender, EventArgs e)
        {
            if (!controller.Connected)
                return;

            bool isOn = controller.ToggleFarmingService();
            UpdateFarmingPreview(isOn);
        }

        private void UpdateFarmingPreview(bool isActive)
        {
            if (isActive)
            {
                lbl_farmingStatus.Text = "ACTIVE";
                lbl_farmingStatus.ForeColor = Color.Green;

                btn_toggleFarmingService.Text = "Deactivate";
            }
            else
            {
                lbl_farmingStatus.Text = "INACTIVE";
                lbl_farmingStatus.ForeColor = Color.Red;

                btn_toggleFarmingService.Text = "Activate";
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void UpdateListView(List<string> logs)
        {
            for (int i = 0; i < logs.Count; i++)
            {
                listView_logging.Items.Add(logs[i]);
            }
        }

        public void AddToListView(string log)
        {
            listView_logging.Items.Add(log);
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (!controller.Connected)
                controller.ConnectToGame();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            AddToListView("text text text texttttttttttttttttttttttttttttt texttttttttttttttttttttttttttttt texttttttttttttttttttttttttttttt texttttttttttttttttttttttttttttt textfdffffffffffffffffffffffffffffffffftttttttttttttttttttttttttttttttttttttttttttt");
        }

        private void Btn_closeWindows_Click(object sender, EventArgs e)
        {
            controller.CloseWindows();
        }
    }
}
