using System.Runtime.InteropServices;
using System.Windows.Interop;
using CV19.Infrastructure.Extensions;

namespace System.Windows
{
    static class WindowExtensions
    {
        private const string user32 = "user32.dll";

        [DllImport(user32, CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport(user32, CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, WM Msg, IntPtr wParam, IntPtr lParam);

        public static IntPtr SendMessage(this Window window, WM Msg, IntPtr wParam, IntPtr lParam) => 
            SendMessage(window.GetWindowHandle(), Msg, wParam, lParam);

        public static IntPtr SendMessage(this Window window, WM Msg, SC wParam, IntPtr lParam = default)
        {
            return SendMessage(
                hWnd: window.GetWindowHandle(), 
                Msg: (uint) Msg, 
                wParam: (IntPtr) wParam, 
                lParam: lParam == default ? (IntPtr)' ' : lParam);
        }

        public static IntPtr GetWindowHandle(this Window window)
        {
            var helper = new WindowInteropHelper(window);
            return helper.Handle;
        }
    }
}
