using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FileTest;

public class User32Helper
{
    const int WM_SETTEXT = 0x0C;
    const int WM_COMMAND = 0x111;


    /// <summary>
    /// 获取窗体的句柄函数
    /// </summary>
    /// <param name="lpClassName">窗口类名</param>
    /// <param name="lpWindowName">窗口标题名</param>
    /// <returns>返回句柄</returns>
    [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
    public static extern IntPtr FindWindowEx(IntPtr Parent, IntPtr Child, string ClassName, string WindowName);
    //设置进程窗口到最前       
    [DllImport("USER32.DLL")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);
    //模拟键盘事件         
    [DllImport("USER32.DLL")]
    public static extern void keybd_event(Byte bVk, Byte bScan, Int32 dwFlags, Int32 dwExtraInfo);
    public delegate bool CallBack(IntPtr hwnd, int lParam);
    [DllImport("USER32.DLL")]
    public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);
    //给CheckBox发送信息
    [DllImport("USER32.DLL", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern int SendMessage(IntPtr hwnd, UInt32 wMsg, int wParam, int lParam);
    [DllImport("USER32.DLL", EntryPoint = "SendMessage")]
    public static extern int SendMessage(IntPtr hwnd, UInt32 wMsg, int wParam, IntPtr lParam);
    //给Text发送信息
    [DllImport("USER32.DLL", EntryPoint = "SendMessage")]
    private static extern int SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, string lParam);
    [DllImport("USER32.DLL")]
    public static extern IntPtr GetWindow(IntPtr hWnd, int wCmd);


    [DllImport("user32.dll")]
    private static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int nMaxCount);  //获取窗口标题
    [DllImport("user32.dll")]
    private static extern void GetClassName(IntPtr hwnd, StringBuilder s, int nMaxCount);   //获取句柄类名

    public static void Get()
    {
        IntPtr upload = FindWindow("#32770", "打开"); //一级
        IntPtr comboBoxEx32 = FindWindowEx(upload, IntPtr.Zero, "ComboBoxEx32", null); //二级
        IntPtr comboBox = FindWindowEx(comboBoxEx32, IntPtr.Zero, "ComboBox", null); //三级
        IntPtr edit = FindWindowEx(comboBox, IntPtr.Zero, "Edit", null); //四级
        IntPtr btnOpen = FindWindowEx(upload, IntPtr.Zero, "Button", "打开(&O)"); //二级，按钮

        if (upload == IntPtr.Zero || comboBox == IntPtr.Zero || comboBoxEx32 == IntPtr.Zero || edit == IntPtr.Zero || btnOpen == IntPtr.Zero)
        {
            return;
        }

        Thread.Sleep(500);

        SendMessage(edit, WM_SETTEXT, IntPtr.Zero, "aaaa");
        Thread.Sleep(100);
        SendMessage(upload, WM_COMMAND, 1, btnOpen);
    }
}
