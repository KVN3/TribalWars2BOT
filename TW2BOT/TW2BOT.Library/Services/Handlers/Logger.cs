using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW2BOT.Library.Services.Handlers
{
    public interface ILogger
    {
        List<string> Logs { get; }

        void Log(string message);
        void LogError(Exception ex);
        event EventHandler LogsChanged;
        event EventHandler ErrorCaught;
    }

    public class Logger : ILogger
    {
        public List<string> Logs { get; }

        public event EventHandler LogsChanged;
        public event EventHandler ErrorCaught;

        public Logger()
        {
            Logs = new List<string>();
        }

        public void Log(string message)
        {
            OnLogsChanged(new MyEventArgs(message));
        }

        public void LogError(Exception ex)
        {
            Log($"An error occurred: {ex.Message}");
            Console.WriteLine($"An error occurred: {ex.Message}");

            OnErrorCaught(new MyEventArgs(ex.Message));
            //throw (ex);
        }

        protected virtual void OnErrorCaught(MyEventArgs e)
        {
            ErrorCaught?.Invoke(this, e);
        }

        protected virtual void OnLogsChanged(MyEventArgs e)
        {
            LogsChanged?.Invoke(this, e);
        }
    }

    public class MyEventArgs : EventArgs
    {
        // class members
        public string message;

        public MyEventArgs(string message)
        {
            this.message = message;
        }
    }
}
