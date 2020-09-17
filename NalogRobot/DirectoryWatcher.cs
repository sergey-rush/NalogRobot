using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using NLog;

namespace NalogRobot
{
    public class DirectoryWatcher
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string targetDirectory;
        public static bool IsRunning = false;
        public static bool BreakLoop = false;
        private static List<FileInfo> movedFiles = new List<FileInfo>();

        public static void StartWatching(string path, string targetDir)
        {
            targetDirectory = targetDir;

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;

            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            //watcher.Created += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;

        }

        public static int fileMovedCount => movedFiles.Count;

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            logger.Info("DirectoryWatcher OnChanged called: {0}", e.Name);

            if (e.Name.Contains(".xlsx") || e.Name.Contains(".xls"))
            {
                Thread.Sleep(15000);
                try
                {
                    if (File.Exists(e.FullPath))
                    {
                        if (!e.FullPath.Contains("$"))
                        {
                            //File.Move(e.FullPath, targetDirectory + "\\" + e.Name);
                            //logger.Info("File {0} is successfully moved", e.FullPath);

                            FileInfo fileInfo = new FileInfo(e.FullPath);
                            movedFiles.Add(fileInfo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "DirectoryWatcher OnChanged File Move", null);
                }
            }
        }

        public static void MoveFiles()
        {
            foreach (FileInfo fi in movedFiles)
            {
                if (File.Exists(fi.FullName))
                {
                    File.Move(fi.FullName, targetDirectory + "\\" + fi.Name);
                    logger.Info("File {0} is successfully moved", fi.FullName);
                }
                else
                {
                    logger.Info("File {0} doesn't exist", fi.FullName);
                }
            }
        }
    }
}