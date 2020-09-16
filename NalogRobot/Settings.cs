using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using NLog;

namespace NalogRobot
{
    public class Settings
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static readonly Dictionary<int, Config> Configs = new Dictionary<int, Config>();

        public static void LoadConfig()
        {
            logger.Info("LoadConfig called");

            Config config1 = new Config();

            Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            try
            {
                string[] lines = File.ReadAllLines("NalogRobot.ini");

                int x = Convert.ToInt32(lines[1].Split(',')[0].Trim().Split('=')[1]);
                int y = Convert.ToInt32(lines[1].Split(',')[1].Trim().Split('=')[1]);

                config1.Start.X = (int)(((double)x / resolution.Width) * 65536);
                config1.Start.Y = (int)(((double)y / resolution.Height) * 65536);

                x = Convert.ToInt32(lines[3].Split(',')[0].Trim().Split('=')[1]);
                y = Convert.ToInt32(lines[3].Split(',')[1].Trim().Split('=')[1]);

                config1.Preview.X = (int)(((double)x / resolution.Width) * 65536);
                config1.Preview.Y = (int)(((double)y / resolution.Height) * 65536);

                x = Convert.ToInt32(lines[5].Split(',')[0].Trim().Split('=')[1]);
                y = Convert.ToInt32(lines[5].Split(',')[1].Trim().Split('=')[1]);

                config1.Export.X = (int)(((double)x / resolution.Width) * 65536);
                config1.Export.Y = (int)(((double)y / resolution.Height) * 65536);

                x = Convert.ToInt32(lines[7].Split(',')[0].Trim().Split('=')[1]);
                y = Convert.ToInt32(lines[7].Split(',')[1].Trim().Split('=')[1]);

                config1.Close.X = (int)(((double)x / resolution.Width) * 65536);
                config1.Close.Y = (int)(((double)y / resolution.Height) * 65536);

                config1.TargetDir = lines[17].Split('=')[1].Trim();
                config1.Count = Convert.ToInt32(lines[19].Split('=')[1]);

                Configs.Add(0, config1);

                Config config2 = new Config();

                x = Convert.ToInt32(lines[9].Split(',')[0].Trim().Split('=')[1]);
                y = Convert.ToInt32(lines[9].Split(',')[1].Trim().Split('=')[1]);

                config2.Start.X = (int)(((double)x / resolution.Width) * 65536);
                config2.Start.Y = (int)(((double)y / resolution.Height) * 65536);

                x = Convert.ToInt32(lines[11].Split(',')[0].Trim().Split('=')[1]);
                y = Convert.ToInt32(lines[11].Split(',')[1].Trim().Split('=')[1]);

                config2.Preview.X = (int)(((double)x / resolution.Width) * 65536);
                config2.Preview.Y = (int)(((double)y / resolution.Height) * 65536);

                x = Convert.ToInt32(lines[13].Split(',')[0].Trim().Split('=')[1]);
                y = Convert.ToInt32(lines[13].Split(',')[1].Trim().Split('=')[1]);

                config2.Export.X = (int)(((double)x / resolution.Width) * 65536);
                config2.Export.Y = (int)(((double)y / resolution.Height) * 65536);

                x = Convert.ToInt32(lines[15].Split(',')[0].Trim().Split('=')[1]);
                y = Convert.ToInt32(lines[15].Split(',')[1].Trim().Split('=')[1]);

                config2.Close.X = (int)(((double)x / resolution.Width) * 65536);
                config2.Close.Y = (int)(((double)y / resolution.Height) * 65536);

                config2.Count = config1.Count;
                config2.TargetDir = config1.TargetDir;
                Configs.Add(1, config2);

            }
            catch (Exception ex)
            {
                logger.Error(ex, "LoadConfig error", null);
            }
        }

        public static Config Select(int index)
        {
            logger.Info("Settings Select {0} called", index);
            return Configs[index];
        }
    }
}