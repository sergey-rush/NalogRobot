﻿using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Diagnostics;

namespace NalogRobot
{
    /// <summary>
    /// Specifies key modifiers.
    /// </summary>
    [Flags]
    public enum KeyModifiers : uint
    {
        /// <summary>
        /// Empty modifiers
        /// </summary>
        None = 0x0000,
        /// <summary>
        /// Either ALT key must be held down.
        /// </summary>
        Alt = 0x0001,
        /// <summary>
        /// Either CTRL key must be held down.
        /// </summary>
        Control = 0x0002,
        /// <summary>
        /// Either SHIFT key must be held down.
        /// </summary>
        Shift = 0x0004,
        /// <summary>
        /// Either WINDOWS key was held down. 
        /// These keys are labeled with the Windows logo. 
        /// Keyboard shortcuts that involve the WINDOWS key are reserved for use by the operating system.
        /// </summary>
        Windows = 0x0008,
        //IgnoreAllModifier   = 0x0400,
        //OnKeyUp             = 0x0800,
        //MouseRight          = 0x4000,
        //MouseLeft           = 0x8000,
    }

    class InterceptKeys
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        public static void Init()
        {
            _hookID = SetHook(_proc);
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

        private static int LshiftTime = 0;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                //MessageBox.Show(vkCode.ToString());

                Keys keys = (Keys)vkCode;
                if (keys == Keys.LShiftKey)
                    LshiftTime = Environment.TickCount;
                else if (keys == Keys.D && Environment.TickCount - LshiftTime < 1000)
                {


                }

                Console.WriteLine((Keys)vkCode + " " + nCode);
                //return (System.IntPtr)1;
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }



    public class HotKey : IMessageFilter, IDisposable
    {
        #region Extern
        const int WM_HOTKEY = 0x312;
        const int ERROR_HOTKEY_ALREADY_REGISTERED = 0x581;

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RegisterHotKey(IntPtr hWnd, IntPtr id, KeyModifiers fsModifiers, Keys vk);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnregisterHotKey(IntPtr hWnd, IntPtr id);
        #endregion

        private IntPtr windowHandle;
        public event HandledEventHandler Pressed;

        public HotKey()
            : this(Keys.None, KeyModifiers.None)
        {
        }

        public HotKey(Keys keyCode, KeyModifiers modifiers)
        {
            this.KeyCode = keyCode;
            this.Modifiers = modifiers;
            Application.AddMessageFilter(this);
        }

        ~HotKey()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (this.IsRegistered)
                this.Unregister();

            this.windowHandle = IntPtr.Zero;
            this.Modifiers = KeyModifiers.None;
            this.KeyCode = Keys.None;
            this.Tag = 0;
        }

        private bool OnPressed()
        {
            HandledEventArgs e = new HandledEventArgs(false);
            if (this.Pressed != null)
                this.Pressed(this, e);

            return e.Handled;
        }

        /// <summary>
        /// Filters out a message before it is dispatched.
        /// </summary>
        /// <param name="message">
        /// The message to be dispatched. You cannot modify this message.
        /// </param>
        /// <returns>
        /// true to filter the message and stop it from being dispatched;
        /// false to allow the message to continue to the next filter or control.
        /// </returns>
        public bool PreFilterMessage(ref Message message)
        {
            //return true;
            if (message.Msg != WM_HOTKEY || !this.IsRegistered)
                return false;

            if (message.WParam == this.Guid)
                return this.OnPressed();

            return false;
        }

        /// <summary>
        /// Defines a system-wide hot key.
        /// </summary>
        /// <param name="windowControl">
        /// A handle to the window that will receive messages generated by the hot key. 
        /// </param>
        public void Register(Control window)
        {
            if (this.IsRegistered)
                throw new NotSupportedException("You cannot register a hotkey that is already registered");

            if (this.IsEmpty)
                throw new NotSupportedException("You cannot register an empty hotkey");

            if (window.IsDisposed)
                throw new ArgumentNullException("window");

            this.windowHandle = window.Handle;

            if (!RegisterHotKey(this.windowHandle, this.Guid, this.Modifiers, this.KeyCode))
            {
                if (Marshal.GetLastWin32Error() != ERROR_HOTKEY_ALREADY_REGISTERED)
                    throw new Win32Exception();
            }
            this.IsRegistered = true;
        }

        /// <summary>
        /// Frees a hot key previously registered by the calling thread.
        /// </summary>
        public void Unregister()
        {
            if (!this.IsRegistered)
                return;

            if (!UnregisterHotKey(this.windowHandle, this.Guid))
                throw new Win32Exception();

            this.IsRegistered = false;
        }

        public bool HasModifier(KeyModifiers modifiers)
        {
            return (this.Modifiers & modifiers) != 0;
        }

        public static HotKey Parse(object content)
        {
            if (content == null)
                return new HotKey();

            return Parse(content.ToString());
        }

        #region Fields

        private IntPtr Guid
        {
            get { return new IntPtr((int)Modifiers << 16 | (int)KeyCode & 0xFFFF); }
        }

        public bool IsEmpty
        {
            get { return (this.KeyCode == Keys.None); }
        }

        public bool IsRegistered { get; private set; }

        public KeyModifiers Modifiers { get; private set; }

        public Keys KeyCode { get; private set; }

        public int Tag { get; set; }

        #endregion
    }
}
