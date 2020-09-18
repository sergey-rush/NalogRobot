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
        public static List<FileData> MovedFiles = new List<FileData>();
        public static ManualResetEvent SignalEvent { get; set; }
        public static Tax CurrentTax { get; set; }
        public static FileSystemWatcher watcher = new FileSystemWatcher();

        public static void StartWatching(string path, string targetDir)
        {
            targetDirectory = targetDir;
            
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        public static void StopWatching()
        {
            watcher.Changed -= new FileSystemEventHandler(OnChanged);
        }


        public static int FileMovedCount => MovedFiles.Count;

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
                            FileData fileData = new FileData();
                            FileInfo fileInfo = new FileInfo(e.FullPath);
                            fileData.FileInfo = fileInfo;
                            string targetPath = targetDirectory + "\\" + CurrentTax.RegNum + ".xlsx";
                            
                            CurrentTax.ImportState = ImportState.Completed;
                            CurrentTax.Updated = DateTime.Now;
                            CurrentTax.TempFile = e.Name;
                            CurrentTax.DestFile = targetPath;

                            fileData.Tax = CurrentTax;

                            MovedFiles.Add(fileData);

                            bool res = Data.Instance.UpdateTax(CurrentTax);
                            logger.Info("File {0} added to list", e.Name);
                            SignalEvent.Set();
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
            foreach (FileData fd in MovedFiles)
            {
                logger.Info("Moving file: Source {0} Target {1}", fd.FileInfo.FullName, fd.Tax.DestFile);

                try
                {
                    if (File.Exists(fd.FileInfo.FullName))
                    {
                        if (File.Exists(fd.Tax.DestFile))
                        {
                            File.Delete(fd.Tax.DestFile);
                        }

                        File.Move(fd.FileInfo.FullName, fd.Tax.DestFile);
                        logger.Info("File {0} is successfully moved to {1}", fd.FileInfo.FullName, fd.Tax.DestFile);
                    }
                    else
                    {
                        logger.Error("File {0} doesn't exist", fd.FileInfo.FullName);
                    }

                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error occured while moving the file");
                }
            }
        }
    }
}