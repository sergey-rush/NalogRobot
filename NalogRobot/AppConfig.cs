using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Xml;

namespace NalogRobot
{
    public static class AppConfig
    {
        public static NameValueCollection Settings;
        public static string ConnectionString { get; set; }
        public static string AppPath { get; set; }

        static AppConfig()
        {
            //string ass = Assembly.GetExecutingAssembly().GetName().CodeBase;
            //AppPath = Path.GetDirectoryName(ass);
            //string configFile = Path.Combine(AppPath, "App.config");

            //if (!File.Exists(configFile))
            //{
            //    throw new FileNotFoundException(string.Format("Application configuration file '{0}' not found.", configFile));
            //}

            //XmlDocument xmlDocument = new XmlDocument();
            //xmlDocument.Load(configFile);
            //XmlNodeList nodeList = xmlDocument.GetElementsByTagName("appSettings");

            //Settings = new NameValueCollection();

            //foreach (XmlNode node in nodeList)
            //{
            //    foreach (XmlNode key in node.ChildNodes)
            //    {
            //        Settings.Add(key.Attributes["key"].Value, key.Attributes["value"].Value);
            //    }
            //}

            //ConnectionString = string.Format("DataSource={0}", Path.Combine(AppPath, Settings["Database"]));
        }
    }
}
