using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace NalogRobot
{
    static class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [STAThread]
        static void Main()
        {
            //long tr = DateTime.Now.AddDays(-3).Ticks;
            LogManager.Setup().SetupSerialization(s =>
                s.RegisterObjectTransformation<System.Net.WebException>(ex => new
                {
                    Type = ex.GetType().ToString(),
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    InnerException = ex.InnerException,
                    Status = ex.Status,
                    Response = ex.Response.ToString(),  // Call your custom method to render stream as string
                })
            );

            logger.Info("Application started");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
