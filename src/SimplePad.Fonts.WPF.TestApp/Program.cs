using System;

namespace SimplePad.Fonts.TestApp;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        App app = new();
        app.InitializeComponent();
        app.Run();
    }
}
