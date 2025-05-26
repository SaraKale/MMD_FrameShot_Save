using System;
using System.Windows.Forms;

namespace MMD_FrameShot_Save
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main()); // 这里是你的主窗口类
        }
    }
}
