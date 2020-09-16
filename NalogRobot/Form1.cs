using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace NalogRobot
{
    public partial class Form1 : Form
    {
        

        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        //Нажатие на левую кнопку мыши
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //Поднятие левой кнопки мыши
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        //перемещение указателя мыши
        private const int MOUSEEVENTF_MOVE = 0x0001;
        [System.Runtime.InteropServices.DllImport("user32.dll",
         CharSet = System.Runtime.InteropServices.CharSet.Auto,
         CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        public NotifyIcon notifyIcon1 = new NotifyIcon();
        public ContextMenu contextMenu1 = new ContextMenu();
        private Point pointPreview1 = new Point(), pointExport1 = new Point(), pointCloseDeclaration1 = new Point(), pointPreview2 = new Point(), pointExport2 = new Point(), pointCloseDeclaration2 = new Point();
        private Point pointPreview = new Point(), pointExport = new Point(), pointCloseDeclaration = new Point();
        private string path = "C:\\Users\\9973-01-102\\AppData\\Local\\Temp"; //здесь меняем место сохранения файлов робота по умолчанию
        private string pathtomove;
        private bool terminated = false;
        private int count = 0;
        /*[return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool BlockInput([In, MarshalAs(UnmanagedType.Bool)] bool fBlockIt);
        */

        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE_LL = 14;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_MOUSEMOVE = 0x200;
        /// <summary>
        /// The WM_LBUTTONDOWN message is posted when the user presses the left mouse button 
        /// </summary>
        private const int WM_LBUTTONDOWN = 0x201;
        /// <summary>
        /// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button
        /// </summary>
        private const int WM_RBUTTONDOWN = 0x204;
        /// <summary>
        /// The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button 
        /// </summary>
        private const int WM_MBUTTONDOWN = 0x207;
        /// <summary>
        /// The WM_LBUTTONUP message is posted when the user releases the left mouse button 
        /// </summary>
        private const int WM_LBUTTONUP = 0x202;
        /// <summary>
        /// The WM_RBUTTONUP message is posted when the user releases the right mouse button 
        /// </summary>
        private const int WM_RBUTTONUP = 0x205;
        /// <summary>
        /// The WM_MBUTTONUP message is posted when the user releases the middle mouse button 
        /// </summary>
        private const int WM_MBUTTONUP = 0x208;
        /// <summary>
        /// The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button 
        /// </summary>
        private const int WM_LBUTTONDBLCLK = 0x203;
        /// <summary>
        /// The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button 
        /// </summary>
        private const int WM_RBUTTONDBLCLK = 0x206;
        /// <summary>
        /// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button 
        /// </summary>
        private const int WM_MBUTTONDBLCLK = 0x209;
        /// <summary>
        /// The WM_MOUSEWHEEL message is posted when the user presses the mouse wheel. 
        /// </summary>
        private const int WM_MOUSEWHEEL = 0x020A;
        private static LowLevelKeyboardProc _procKeyboard = HookCallbackKeyboard;
        private static LowLevelMouseProc _procMouse = HookCallbackMouse;
        private static IntPtr _hookIDKeyboard = IntPtr.Zero;
        private static IntPtr _hookIDMouse = IntPtr.Zero;
        public static void InitHook()
        {
            _hookIDKeyboard = SetHook(_procKeyboard);
            _hookIDMouse = SetHook(_procMouse);
        }

        /*public static void Main()
        {
            _hookID = SetHook(_proc);
            Application.Run();
            UnhookWindowsHookEx(_hookID);
        }*/

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static int LshiftTime = 0;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallbackKeyboard(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //Thread.Sleep(1000);
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN && Watcher.running)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                //MessageBox.Show(vkCode.ToString());

                Keys keys = (Keys)vkCode;
                if (keys == Keys.LShiftKey)
                    LshiftTime = Environment.TickCount;
                else if (keys == Keys.D && Environment.TickCount - LshiftTime < 500)
                {
                    Watcher.br = true;

                }

                //Console.WriteLine((Keys)vkCode + " " + nCode);
                return (System.IntPtr)1;
            }
            /*else if (nCode >= 0 && (wParam == (IntPtr)WM_MBUTTONDOWN || wParam == (IntPtr)WM_MOUSEMOVE) && Watcher.running)
            {
                return (System.IntPtr)1;
            }*/

            return CallNextHookEx(_hookIDKeyboard, nCode, wParam, lParam);
        }

        private static IntPtr HookCallbackMouse(int nCode, IntPtr wParam, IntPtr lParam)
        {
           
            if (nCode >= 0 && (wParam == (IntPtr)WM_MOUSEWHEEL || wParam == (IntPtr)WM_LBUTTONDBLCLK || wParam == (IntPtr)WM_RBUTTONDBLCLK || wParam == (IntPtr)WM_MBUTTONDBLCLK || wParam == (IntPtr)WM_LBUTTONDOWN || wParam == (IntPtr)WM_RBUTTONDOWN || wParam == (IntPtr)WM_RBUTTONUP || wParam == (IntPtr)WM_MBUTTONDOWN || wParam == (IntPtr)WM_MOUSEMOVE) && Watcher.running)
            {
                return (System.IntPtr)1;
            }

            return CallNextHookEx(_hookIDMouse, nCode, wParam, lParam);
        }



        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("Fdsfds");
            //if (e.KeyCode == Keys.LWin) 
            e.SuppressKeyPress = false;
        }
    

        public Form1()
        {
            InitHook();


            InitializeComponent();
            createIconMenuStructure();
            LoadIni();
            path = Path.GetTempPath();
           


            Watcher.Run(path, pathtomove);
           
        }

        private void SaveLog(string s)
        {
            var text = s;
            string path = (Path.GetDirectoryName(Application.ExecutablePath));
            using (var writer = new StreamWriter(path + "\\NalogRobot.log", true))
            {
                writer.WriteLine(DateTime.Now.ToString() + " " + text);
            }
        }

        private void LoadIni()
        {
            Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            string[] Lines = File.ReadAllLines("NalogRobot.ini");
            int x = Convert.ToInt32(Lines[1].Split(',')[0].Trim().Split('=')[1]);
            int y = Convert.ToInt32(Lines[1].Split(',')[1].Trim().Split('=')[1]);
            pointPreview1.X = (int)(((double)x / resolution.Width) * 65536);
            pointPreview1.Y = (int)(((double)y / resolution.Height) * 65536);

            x = Convert.ToInt32(Lines[3].Split(',')[0].Trim().Split('=')[1]);
            y = Convert.ToInt32(Lines[3].Split(',')[1].Trim().Split('=')[1]);
            pointExport1.X = (int)(((double)x / resolution.Width) * 65536);
            pointExport1.Y = (int)(((double)y / resolution.Height) * 65536);

            pathtomove = Lines[5].Split('=')[1].Trim();
     

            x = Convert.ToInt32(Lines[7].Split(',')[0].Trim().Split('=')[1]);
            y = Convert.ToInt32(Lines[7].Split(',')[1].Trim().Split('=')[1]);
       
            pointCloseDeclaration1.X = (int)(((double)x / resolution.Width) * 65536);
            pointCloseDeclaration1.Y = (int)(((double)y / resolution.Height) * 65536);

            count = Convert.ToInt32(Lines[9].Split('=')[1]);

            x = Convert.ToInt32(Lines[11].Split(',')[0].Trim().Split('=')[1]);
            y = Convert.ToInt32(Lines[11].Split(',')[1].Trim().Split('=')[1]);
          
            pointPreview2.X = (int)(((double)x / resolution.Width) * 65536);
            pointPreview2.Y = (int)(((double)y / resolution.Height) * 65536);

            x = Convert.ToInt32(Lines[13].Split(',')[0].Trim().Split('=')[1]);
            y = Convert.ToInt32(Lines[13].Split(',')[1].Trim().Split('=')[1]);
         
            pointExport2.X = (int)(((double)x / resolution.Width) * 65536);
            pointExport2.Y = (int)(((double)y / resolution.Height) * 65536);

            x = Convert.ToInt32(Lines[15].Split(',')[0].Trim().Split('=')[1]);
            y = Convert.ToInt32(Lines[15].Split(',')[1].Trim().Split('=')[1]);
        
            pointCloseDeclaration2.X = (int)(((double)x / resolution.Width) * 65536);
            pointCloseDeclaration2.Y = (int)(((double)y / resolution.Height) * 65536);

        }

        public void createIconMenuStructure()
        {
            contextMenu1.MenuItems.Add("Запуск");
            contextMenu1.MenuItems.Add("Выход");
            contextMenu1.MenuItems[0].Click += startRobot;
            contextMenu1.MenuItems[1].Click += exitRobot;
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.ContextMenu = contextMenu1;
            notifyIcon1.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
   
        private void exitRobot(object sender, EventArgs e)
        {
            Close();
        }

        private bool starting = false;
        
        private void startRobot(object sender, EventArgs e)
        {

            Thread thread = new Thread(delegate ()
            {
                
              

                OptionsForm optionsForm = new OptionsForm();
                if (optionsForm.ShowDialog() == DialogResult.OK)
                {
                    if (optionsForm.comboBox1.SelectedIndex == 0)
                    {
                        pointPreview = pointPreview1; pointExport = pointExport1; pointCloseDeclaration = pointCloseDeclaration1;
                    }
                    else if (optionsForm.comboBox1.SelectedIndex == 1)
                    {
                        pointPreview = pointPreview2; pointExport = pointExport2; pointCloseDeclaration = pointCloseDeclaration2;
                    }
                }
                else
                    return;
                //BlockInput(true);
                terminated = false;
                Watcher.br = false;
                Watcher.running = true;
                Thread.Sleep(3000);  
                List<string> RegNomers = new List<string>();
            
                Watcher.running = false;
                mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, 32768, 22000, 0, 0);
                mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 32768, 22000, 0, 0);
                Watcher.running = true;
                //пауза в 0.5 сек
                Thread.Sleep(500);
                Watcher.running = false;
                for (int i = 0; i < 50; i++)
                    SendKeys.SendWait("{UP}");
                Watcher.running = true;
                int index = 0;
                while (Watcher.running && index < count)
                {

                    Thread.Sleep(500);
                                      
                    if (Watcher.br)
                        break;
                    Watcher.running = false;
                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, pointPreview.X, pointPreview.Y, 0, 0);
                    //for (int i = 0; i < 20; i++)
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, pointPreview.X, pointPreview.Y, 0, 0);
                        //Thread.Sleep(10);
                    }
                    Watcher.running = true;
                    Thread.Sleep(25000);
                    if (Watcher.br)
                        break;
                 
                    Watcher.running = false;
                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, pointExport.X, pointExport.Y, 0, 0);
                    //for (int i = 0; i < 20; i++)
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, pointExport.X, pointExport.Y, 0, 0);
                        //Thread.Sleep(10);
                    }
                    Watcher.running = true;
                    Thread.Sleep(2000);
                    if (Watcher.br)
                        break;
                    Watcher.running = false;
                    SendKeys.SendWait("{ENTER}");
                    Watcher.running = true;
                    Thread.Sleep(6000);
                    if (Watcher.br)
                        break;
                  
                    Watcher.running = false;
                    SendKeys.SendWait("(%{F4})");
                    Watcher.running = true;
                    //mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, 65000, 1000, 0, 0);
                    //mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 65000, 1000, 0, 0);
                    Thread.Sleep(1000);
                    if (Watcher.br)
                        break;
                 
                    Watcher.running = false;
                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, pointCloseDeclaration.X, pointCloseDeclaration.Y, 0, 0);
                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, pointCloseDeclaration.X, pointCloseDeclaration.Y, 0, 0);
                    Watcher.running = true;
                    Thread.Sleep(1500);
                    if (Watcher.br)
                        break;
                    Watcher.running = false;
                    SendKeys.SendWait("{DOWN}");
                    Watcher.running = true;
                    index++;
                }

                

                //BlockInput(false);
                Thread.Sleep(3000);
                Watcher.running = false;
                string info = "Обработано " + index.ToString() + " циклов, обработано " + Watcher.fileMovedCount.ToString() + " деклараций.";
                SaveLog(info);
                MessageBox.Show(info);
            });
            thread.Start();
        }
    }

    public class Watcher
    {
        static  string Pathtomove;
        public static bool running = false;
        public static bool br = false;
        static List<string> filenameRemoved = new List<string>(); 
        public static void Run(string path, string pathtomove)
        {
            Pathtomove = pathtomove;

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;

            watcher.NotifyFilter = /*NotifyFilters.LastAccess | */NotifyFilters.LastWrite
               /*| NotifyFilters.FileName | NotifyFilters.DirectoryName*/;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            //watcher.Created += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;

        }
        public static int fileMovedCount = 0;
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            if (e.Name.Contains(".xlsx"))
            {
                if (!filenameRemoved.Contains(e.Name))
                    fileMovedCount++;
       
                filenameRemoved.Add(e.Name);
                Thread th = new Thread(delegate ()
                {
                    
                    Thread.Sleep(15000);
                    try
                    {

                        File.Move(e.FullPath, Pathtomove + "\\" + e.Name);
                        
                    }
                    catch
                    {
                    }
                });
                th.Start();
            }
        }

    }


}
