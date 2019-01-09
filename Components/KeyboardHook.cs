using System;

using System.Diagnostics;

using System.Windows.Forms;
using System.Windows.Input;
using System.Runtime.InteropServices;
namespace TryToOnTopAndImageCatch
{
    public sealed class KeyboardHook 
    {

        private const int WH_KEYBOARD_LL = 13;

        //private const int WM_KEYDOWN = 257;

        private static LowLevelKeyboardProc _proc = HookCallback;

        private static IntPtr _hookID = IntPtr.Zero;
        public static void SetHook()
        {
            _hookID = SetHook(_proc);

          //  Application.Run();

            
        }
        public static void UnSetHook()
        {
            UnhookWindowsHookEx(_hookID);
        }
        private static IntPtr SetHook(LowLevelKeyboardProc proc)

        {

            using (Process curProcess = Process.GetCurrentProcess())

            using (ProcessModule curModule = curProcess.MainModule)

            {

                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,

                    GetModuleHandle(curModule.ModuleName), 0);

            }

        }


        private delegate IntPtr LowLevelKeyboardProc(

            int nCode, IntPtr wParam, IntPtr lParam);

        static IntPtr WM_KEYDOWN = (IntPtr)0x100;
        static IntPtr WM_KEYUP = (IntPtr)0x101;
        static IntPtr WM_SYSKEYDOWN = (IntPtr)0x104;
        static IntPtr WM_SYSKEYUP = (IntPtr)0x105;
        static bool ShiftOn = false;
        static bool AltOn = false;
        static bool FourOn = false;
        static bool FiveOn = false;
        private static IntPtr HookCallback(

            int nCode, IntPtr wParam, IntPtr lParam)

        {
            if (nCode >= 0)
            {
                    if ((wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                    {
                        int t = Marshal.ReadInt32(lParam);
                        if (t == 160)
                            ShiftOn = true;
                        if (t == 164)
                            AltOn = true;
                        if (t == 52)
                            FourOn = true;
                    if (t == 53)
                        FiveOn = true; ;
                }
                    else if ((wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                    {
                        int t = Marshal.ReadInt32(lParam);
                        if (t == 160)
                            ShiftOn = false;
                        if (t == 164)
                            AltOn = false;
                        if (t == 52)
                            FourOn = false;
                        if (t == 53)
                            FiveOn = false;
                }
                    if (ShiftOn && AltOn && FourOn)
                    {
                        ShiftOn = false;
                        AltOn = false;
                        FourOn = false;
                        FiveOn = false;
                        Program.f.Apply();
                    }
                    if (ShiftOn && AltOn && FiveOn)
                    {
                        ShiftOn = false;
                        AltOn = false;
                        FourOn = false;
                        FiveOn = false;
                        Program.f.Decline();
                    }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr SetWindowsHookEx(int idHook,

            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        [return: MarshalAs(UnmanagedType.Bool)]

        private static extern bool UnhookWindowsHookEx(IntPtr hhk);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,

            IntPtr wParam, IntPtr lParam);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
