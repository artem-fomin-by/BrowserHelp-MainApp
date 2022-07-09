using System.Runtime.InteropServices;

namespace Logic;

public static class Interop
{
    public enum DeviceCapsIndex : int
    {
        LOGPIXELSX = 88,
        LOGPIXELSY = 90
    }

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    public static extern int GetDeviceCaps(IntPtr hdc, DeviceCapsIndex index);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetCursorPos(ref Win32Point pt);

    [StructLayout(LayoutKind.Sequential)]
    public struct Win32Point
    {
        public Int32 X;
        public Int32 Y;
    };

    public static (int, int) GetDpi(IntPtr hWnd)
    {
        var context = GetDC(hWnd);
        var xDPI = GetDeviceCaps(context, DeviceCapsIndex.LOGPIXELSX);
        var yDPI = GetDeviceCaps(context, DeviceCapsIndex.LOGPIXELSY);
        return (xDPI, yDPI);
    }
}
