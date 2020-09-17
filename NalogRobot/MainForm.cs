﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Windows.Automation;
using NLog;

namespace NalogRobot
{
    public partial class MainForm : Form
    {
        private NotifyIcon notifyIcon1 = new NotifyIcon();
        private ContextMenu contextMenu1 = new ContextMenu();
        private Config config;

        /// <summary>
        /// Application files default location 
        /// здесь меняем место сохранения файлов робота по умолчанию
        /// </summary>
        private string tempDir = Path.GetTempPath();
        
        /*public static void Main()
        {
            _hookID = SetHook(_proc);
            Application.Run();
            UnhookWindowsHookEx(_hookID);
        }*/

        private static int LshiftTime = 0;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("Fdsfds");
            //if (e.KeyCode == Keys.LWin) 
            e.SuppressKeyPress = false;
        }

        public MainForm()
        {
            InitHook();
            InitializeComponent();
            CreateIconMenuStructure();
            Settings.LoadConfig();
        }

        private void Run(object sender, EventArgs e)
        {
            logger.Info("Run method called");

            Thread thread = new Thread(delegate()
            {
                OptionsForm optionsForm = new OptionsForm();
                if (optionsForm.ShowDialog() == DialogResult.OK)
                {
                    config = Settings.Select(optionsForm.comboBox1.SelectedIndex);
                    DirectoryWatcher.StartWatching(tempDir, config.TargetDir);
                }
                else
                {
                    return;
                }

                DirectoryWatcher.BreakLoop = false;
                DirectoryWatcher.IsRunning = true;
                logger.Info("Waiting 3 sec");
                Thread.Sleep(3000);
                DirectoryWatcher.IsRunning = false;

                logger.Info("Mouse is about to move to Start point {0} x {1}", config.Start.X, config.Start.Y);
                mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, config.Start.X, config.Start.Y, 0, 0);

                logger.Info("Mouse is about to click left button");
                mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN, config.Start.X, config.Start.Y, 0, 0);
                Thread.Sleep(150);
                mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP, config.Start.X, config.Start.Y, 0, 0);

                DirectoryWatcher.IsRunning = true;
                logger.Info("Waiting 0.5 sec");
                Thread.Sleep(500);
                DirectoryWatcher.IsRunning = false;

                for (int i = 0; i < 50; i++)
                {
                    SendKeys.SendWait("{UP}");
                }

                logger.Info("Arrow button UP 50 times pressed");

                DirectoryWatcher.IsRunning = true;
                int index = 0;

                while (DirectoryWatcher.IsRunning && index < config.Count)
                {
                    logger.Info("Begin loop: {0} of {2}", index, config.Count);

                    //if (IsProcessStopped())
                    //{
                    //    break;
                    //}

                    Tax tax = new Tax();

                    logger.Info("Waiting 0.5 sec");
                    Thread.Sleep(500);

                    if (DirectoryWatcher.BreakLoop)
                    {
                        break;
                    }

                    DirectoryWatcher.IsRunning = false;

                    logger.Info("Mouse is about to move to Preview point {0} x {1}", config.Preview.X, config.Preview.Y);
                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, config.Preview.X, config.Preview.Y, 0, 0);

                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN, config.Preview.X, config.Preview.Y, 0, 0);
                    Thread.Sleep(150);
                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP, config.Preview.X, config.Preview.Y, 0, 0);

                    DirectoryWatcher.IsRunning = true;
                    
                    Thread.Sleep(10000);

                    DirectoryWatcher.IsRunning = false;

                    SendKeys.SendWait("{DOWN}");
                    logger.Info("Table: Key ARROW DOWN sent");

                    DirectoryWatcher.IsRunning = true;

                    Thread.Sleep(2000);

                    var element = AutomationElement.FocusedElement;
                    if (element != null)
                    {
                        logger.Info("Element is not null");

                        object pattern;
                        if (element.TryGetCurrentPattern(TextPattern.Pattern, out pattern))
                        {
                            var tp = (TextPattern)pattern;
                            var sb = new StringBuilder();

                            foreach (var r in tp.GetSelection())
                            {
                                sb.AppendLine(r.GetText(-1));
                            }

                            var selectedText = sb.ToString();
                            logger.Info("SelectedText: {0}", selectedText);

                            
                            tax.RegNum = selectedText;
                            tax.ImportState = ImportState.Created;
                            tax.Id = Data.Instance.InsertTax(tax);

                        }
                    }

                    Thread.Sleep(2000);


                    if (DirectoryWatcher.BreakLoop)
                        break;

                    DirectoryWatcher.IsRunning = false;

                    logger.Info("Mouse is about to move to Export point {0} x {1}", config.Export.X, config.Export.Y);
                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, config.Export.X, config.Export.Y, 0, 0);

                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN, config.Export.X, config.Export.Y, 0, 0);
                    Thread.Sleep(150);
                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP, config.Export.X, config.Export.Y, 0, 0);

                    DirectoryWatcher.IsRunning = true;
                    
                    Thread.Sleep(2000);

                    if (DirectoryWatcher.BreakLoop)
                    {
                        break;
                    }

                    DirectoryWatcher.IsRunning = false;

                    SendKeys.SendWait("{ENTER}");
                    logger.Info("Key ENTER sent");

                    

                    DirectoryWatcher.IsRunning = true;
                    
                    Thread.Sleep(6000);

                    if (DirectoryWatcher.BreakLoop)
                    {
                        break;
                    }

                    DirectoryWatcher.IsRunning = false;

                    SendKeys.SendWait("(%{F4})");
                    logger.Info("Key F4 sent");

                    DirectoryWatcher.IsRunning = true;

                    Thread.Sleep(1000);
                    
                    if (DirectoryWatcher.BreakLoop)
                    {
                        break;
                    }

                    DirectoryWatcher.IsRunning = false;

                    logger.Info("Mouse is about to move to Close point {0} x {1}", config.Close.X, config.Close.Y);
                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, config.Close.X, config.Close.Y, 0, 0);

                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN, config.Close.X, config.Close.Y, 0, 0);
                    Thread.Sleep(150);
                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP, config.Close.X, config.Close.Y, 0, 0);

                    DirectoryWatcher.IsRunning = true;

                    Thread.Sleep(1500);

                    if (DirectoryWatcher.BreakLoop)
                    {
                        break;
                    }

                    DirectoryWatcher.IsRunning = false;

                    SendKeys.SendWait("{DOWN}");
                    logger.Info("Grid: Key ARROW DOWN sent");

                    DirectoryWatcher.IsRunning = true;
                    index++;
                }

                logger.Info("Waiting 30 sec");
                Thread.Sleep(DirectoryWatcher.fileMovedCount * 5000);
                DirectoryWatcher.MoveFiles();
                DirectoryWatcher.IsRunning = false;
                string info = $"Обработано {index} циклов, обработано {DirectoryWatcher.fileMovedCount} деклараций.";
                logger.Info(info);
                Stat.SaveSummary(info);
                MessageBox.Show(info);
            });
            thread.Start();
        }

        public void CreateIconMenuStructure()
        {
            contextMenu1.MenuItems.Add("Запуск", Run);
            contextMenu1.MenuItems.Add("Файлы", OpenFileForm);
            contextMenu1.MenuItems.Add("Выход", Exit);

            notifyIcon1.Icon = Properties.Resources.AppIcon;
            notifyIcon1.ContextMenu = contextMenu1;
            notifyIcon1.Visible = true;
        }

        private void OpenFileForm(object sender, EventArgs e)
        {
            FileForm fileForm = new FileForm();
            fileForm.Show();
        }

        private void Exit(object sender, EventArgs e)
        {
            Close();
        }

        public static bool IsProcessStopped()
        {
            Process[] pname = Process.GetProcessesByName("CSC.Hosting.UcHost");
            if (pname.Length == 0)
            {
                logger.Info("CSC.Hosting.UcHost does not exist");
                return true;
            }
            else
            {
                logger.Info("CSC.Hosting.UcHost is running");
                return false;
            }
        }
    }
}
