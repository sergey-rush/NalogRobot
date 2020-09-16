using System;
using System.IO;
using System.Windows.Forms;

namespace NalogRobot
{
    public class Stat
    {
        public static void SaveSummary(string text)
        {
            string path = (Path.GetDirectoryName(Application.ExecutablePath));
            using (var writer = new StreamWriter(path + "\\NalogRobot.log", true))
            {
                writer.WriteLine(DateTime.Now.ToString("F") + " " + text);
            }
        }
    }
}