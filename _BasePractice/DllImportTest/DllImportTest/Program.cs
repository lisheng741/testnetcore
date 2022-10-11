
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

[SupportedOSPlatform("windows")]
[DllImport("user32.dll", CharSet = CharSet.Unicode)]
static extern int MessageBox(IntPtr hwnd, String text, String caption, uint type);

if (OperatingSystem.IsWindows())
{
    MessageBox(new IntPtr(0), "Hello world!", "Hello Diaglog", 0);
}

var os = Environment.OSVersion;
Console.WriteLine(os.VersionString);
Console.WriteLine(os.Platform);
