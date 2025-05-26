using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;
using Gma.System.MouseKeyHook; // 全局钩子库
using System.Globalization;
using System.Resources;

namespace MMD_FrameShot_Save
{
    public partial class Main : Form
    {
        // ==== 内存函数 =====
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        // 写内存设置帧数（补充WriteProcessMemory）
        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);
        private void WriteInt32(IntPtr address, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(processHandle, address, buffer, buffer.Length, out _);
        }
        // 获取窗口句柄
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        const int PROCESS_VM_READ = 0x0010;
        const int PROCESS_QUERY_INFORMATION = 0x0400;

        Process mmdProcess;
        IntPtr processHandle = IntPtr.Zero;

        // ==== 全局监听按键 =====
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        const uint MOD_ALT = 0x1;
        const uint MOD_CONTROL = 0x2;
        const uint MOD_SHIFT = 0x4;
        const uint MOD_WIN = 0x8;

        // ==== 定义ini文件路径 =====
        private IniFile ini;
        private string iniPath = Path.Combine(Application.StartupPath, "config.ini");

        // ==== 全局钩子 =====
        private IKeyboardMouseEvents m_GlobalHook;
        private bool isPicking = false;

        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(Point p);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        // 加载变量
        private bool isLoading = false;
        private bool isBatchSaving = false; // 是否批量保存
        private Process currentAhkProcess = null; // 当前AHK进程
        private volatile bool isSavingInProgress = false; // 是否正在保存

        // 语言资源文件
        private ResourceManager resManager = new ResourceManager("MMD_FrameShot_Save.LanguageList.Resources", typeof(Main).Assembly);
        private CultureInfo currentCulture = CultureInfo.CurrentUICulture;

        // 保存文件列表
        private List<string> savedFileList = new List<string>();

        // ==== 切换键盘布局 =====
        /***
         * 原使用C#编写了切换输入法代码，但经过测试无法切换到英文输入法。
         * 建议使用AHK脚本或其他方法来实现输入法切换。
         ****/

        // ----------------
        // ==== 主入口 =====
        // ----------------
        public Main()
        {
            InitializeComponent();
            ini = new IniFile(iniPath); // 创建ini文件
            imageType.Items.AddRange(new string[] { "bmp", "jpg", "png", "dds", "dib", "pfm", "hdr" }); // 添加图片格式选项
            // imageType.SelectedIndex = 2; // 默认png，会和读取ini配置冲突

            // 进度条数量显示
            Progressdisplay.Text = "";
            Progressdisplay.Visible = false;
            this.Controls.Add(Progressdisplay);

            // 文件后缀
            Filesuffix.TextChanged += (s, e) => ini.Write("Config", "Filesuffix", Filesuffix.Text);
            // 转换格式
            ConvertFormat.SelectedIndexChanged += (s, e) => ini.Write("Config", "ConvertFormat", ConvertFormat.SelectedItem?.ToString() ?? "");
            // 输出路径
            imgcompress_OutPath.TextChanged += (s, e) => ini.Write("Config", "imgcompress_OutPath", imgcompress_OutPath.Text);
            // 源目录
            SourceDirectory.CheckedChanged += (s, e) => ini.Write("Config", "SourceDirectory", SourceDirectory.Checked ? "1" : "0");
            // 删除源文件
            DeleteSourceFiles.CheckedChanged += (s, e) => ini.Write("Config", "DeleteSourceFiles", DeleteSourceFiles.Checked ? "1" : "0");
            // JPG/Webp/Avif压缩质量
            JPGqualityValue.ValueChanged += (s, e) => ini.Write("Config", "JPGqualityValue", JPGqualityValue.Value.ToString());
            // PNG质量
            PNGqualityValue.ValueChanged += (s, e) => ini.Write("Config", "PNGqualityValue", PNGqualityValue.Value.ToString());
            // 是否启用图片尺寸
            imgSize.CheckedChanged += (s, e) => ini.Write("Config", "imgSize", imgSize.Checked ? "1" : "0");
            // 图片尺寸宽
            imgSizeWidthValue.ValueChanged += (s, e) => ini.Write("Config", "imgSizeWidthValue", imgSizeWidthValue.Value.ToString());
            // 图片尺寸高
            imgSizeHeightValue.ValueChanged += (s, e) => ini.Write("Config", "imgSizeHeightValue", imgSizeHeightValue.Value.ToString());

            // 窗体自动置顶
            this.TopMost = isTopMost;
            if (isTopMost)
            {
                TOPUP.BackColor = Color.DodgerBlue;
            }
            else
            {
                TOPUP.BackColor = SystemColors.Control;
            }
        }

        // 日志时间戳封装
        private void AppendLog(string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            loglist.AppendText($"[{timestamp}] {message}\r\n");
            loglist.SelectionStart = loglist.Text.Length;
            loglist.ScrollToCaret();
        }

        // ---------------------
        // 窗体加载事件
        // ---------------------
        private void Main_Load(object sender, EventArgs e)
        {
            isLoading = true; // 开始加载，禁用事件写入

            // 检查配置文件是否存在
            if (!File.Exists(iniPath))
            {
                ini.Write("Config", "Shortcutkeys", ""); // 快捷键
                ini.Write("Config", "Fileprefix", "Camera"); // 文件名前缀
                ini.Write("Config", "imageType", "png"); // 图片格式
                ini.Write("Config", "OutPath", ""); // 导出路径
                ini.Write("Config", "delaytimeValue", ""); // 保存延迟时间
                ini.Write("Config", "SaveAfterFolder", "0"); // 保存后打开文件夹
                ini.Write("Config", "Language", "en-US"); // 语言
                ini.Write("Config", "FrameBoxX", "0"); // 窗体X坐标
                ini.Write("Config", "FrameBoxY", "0"); // 窗体Y坐标
                ini.Write("Config", "Filesuffix", "compress"); // 文件后缀
                ini.Write("Config", "ConvertFormat", ""); // 转换格式
                ini.Write("Config", "imgcompress_OutPath", ""); // 输出路径
                ini.Write("Config", "SourceDirectory", "0"); // 源目录
                ini.Write("Config", "DeleteSourceFiles", "0"); // 删除源文件
                ini.Write("Config", "JPGqualityValue", "90"); // JPG/Webp/Avif压缩质量
                ini.Write("Config", "PNGqualityValue", "9"); // PNG质量
                ini.Write("Config", "imgSize", "0"); // 是否启用图片尺寸
                ini.Write("Config", "imgSizeWidthValue", "1920"); // 图片尺寸宽
                ini.Write("Config", "imgSizeHeightValue", "1080"); // 图片尺寸高
            }

            // 自定义保存时间
            delaytimeValue.Minimum = 5000; // 最小值
            delaytimeValue.Maximum = 60000; // 最大值
            delaytimeValue.Increment = 500; // 递增值
            //delaytimeValue.Value = 5000; // 控件数值

            JPGqualityValue.Maximum = 100; // JPG/Webp/Avif压缩质量最大值
            PNGqualityValue.Maximum = 9; // PNG压缩级别最大值
            imgSizeWidthValue.Maximum = 9999; // 图片尺寸宽最大值
            imgSizeHeightValue.Maximum = 9999; // 图片尺寸高最大值
            imgSizeWidthValue.Minimum = 1; // 图片尺寸宽最小值
            imgSizeHeightValue.Minimum = 1; // 图片尺寸高最小值

            // 图片压缩-转换格式下拉框
            ConvertFormat.Items.Clear();
            ConvertFormat.Items.AddRange(new object[] { "jpg", "png", "gif", "tga", "webp", "avif" });

            // --------------
            // 读取ini配置
            // --------------
            Shortcutkeys.Text = ini.Read("Config", "Shortcutkeys"); // 读取快捷键
            Fileprefix.Text = ini.Read("Config", "Fileprefix"); // 读取文件名前缀
            // 图片文件类型
            string type = ini.Read("Config", "imageType", "png").Trim(); // 读取图片格式
            foreach (var item in imageType.Items)
            {
                if (string.Equals(item.ToString(), type, StringComparison.OrdinalIgnoreCase))
                {
                    imageType.SelectedItem = item;
                    break;
                }
            }
            // 读取导出路径
            OutPath.Text = ini.Read("Config", "OutPath");
            // 读取保存延迟等待时间
            if (int.TryParse(ini.Read("Config", "delaytimeValue", "5000"), out int delay))
                delaytimeValue.Value = delay;
            // 读取保存后打开文件夹
            SaveAfterFolder.Checked = ini.Read("Config", "SaveAfterFolder") == "1"; 
            // 读取坐标值
            string xStr = ini.Read("Config", "FrameBoxX", "0");
            string yStr = ini.Read("Config", "FrameBoxY", "0");
            frameBoxX = int.TryParse(xStr, out var x) ? x : 0;
            frameBoxY = int.TryParse(yStr, out var y) ? y : 0;
            locationViewXY.Text = $"X={frameBoxX} Y={frameBoxY}";
            // 文件后缀
            Filesuffix.Text = ini.Read("Config", "Filesuffix", "");
            // 转换格式
            string convertFormat = ini.Read("Config", "ConvertFormat", "");
            if (!string.IsNullOrEmpty(convertFormat) && ConvertFormat.Items.Contains(convertFormat))
                ConvertFormat.SelectedItem = convertFormat;
            // 输出路径
            imgcompress_OutPath.Text = ini.Read("Config", "imgcompress_OutPath", "");
            // 源目录
            SourceDirectory.Checked = ini.Read("Config", "SourceDirectory", "0") == "1";
            // 删除源文件
            DeleteSourceFiles.Checked = ini.Read("Config", "DeleteSourceFiles", "0") == "1";
            // JPG/Webp/Avif压缩质量
            if (int.TryParse(ini.Read("Config", "JPGqualityValue", "85"), out int jpgQ))
                JPGqualityValue.Value = Math.Min(Math.Max(jpgQ, JPGqualityValue.Minimum), JPGqualityValue.Maximum);
            // PNG质量
            if (int.TryParse(ini.Read("Config", "PNGqualityValue", "9"), out int pngQ))
                PNGqualityValue.Value = Math.Min(Math.Max(pngQ, PNGqualityValue.Minimum), PNGqualityValue.Maximum);
            // 是否启用图片尺寸
            imgSize.Checked = ini.Read("Config", "imgSize", "0") == "1";
            // 图片尺寸宽
            if (int.TryParse(ini.Read("Config", "imgSizeWidthValue", "0"), out int w))
                imgSizeWidthValue.Value = Math.Min(Math.Max(w, imgSizeWidthValue.Minimum), imgSizeWidthValue.Maximum);
            // 图片尺寸高
            if (int.TryParse(ini.Read("Config", "imgSizeHeightValue", "0"), out int h))
                imgSizeHeightValue.Value = Math.Min(Math.Max(h, imgSizeHeightValue.Minimum), imgSizeHeightValue.Maximum);

            // 自动更新帧数
            frameTimer = new System.Windows.Forms.Timer();
            frameTimer.Interval = 1000; // 每秒执行一次
            frameTimer.Tick += frameTimer_Tick;
            frameTimer.Start();
            // 日志只读
            loglist.ReadOnly = true;

            isLoading = false; // 加载完毕，允许事件写入

            // 启动时仅开启单帧保存，禁用连续帧选项
            Singleframe.Checked = true; // 启用单帧保存
            FramerangeText.Checked = false; // 禁用连续帧选项
            FrameUP.Text = ""; // 清空帧开始框
            FrameDown.Text = ""; // 清空帧结束框
            locationViewXY.Text = ""; // 清空坐标值
            FrameUP.Enabled = false; // 禁用连续帧开始输入框
            FrameDown.Enabled = false; // 禁用连续帧结束输入框
            FrameClear.Enabled = false; // 禁用清空按钮

            // 启动窗体时禁用图片压缩相关控件
            Filesuffix.Enabled = false; // 文件后缀
            ConvertFormat.Enabled = false; // 转换格式
            imgcompress_OutPath.Enabled = false; // 输出路径
            imgcompress_OutPath_button.Enabled = false; // 选择输出路径按钮
            SourceDirectory.Enabled = false; // 源文件夹
            DeleteSourceFiles.Enabled = false; // 删除源文件
            JPGqualityValue.Enabled = false; // JPG/Webp/Avif压缩质量
            PNGqualityValue.Enabled = false; // PNG压缩级别
            imgSize.Enabled = false; // 图片尺寸
            imgSizeWidthValue.Enabled = false; // 图片宽度
            imgSizeHeightValue.Enabled = false; // 图片高度

            // 语言切换事件
            LanguageUIList.Items.AddRange(new object[] { "English", "简体中文", "繁體中文", "日本語" });
            LanguageUIList.SelectedIndexChanged += LanguageUIList_SelectedIndexChanged;
            string lang = ini.Read("Config", "Language", "en-US");
            switch (lang)
            {
                case "en-US":
                    currentCulture = new CultureInfo("en-US");
                    LanguageUIList.SelectedIndex = 0;
                    break;
                case "zh-CN":
                    currentCulture = new CultureInfo("zh-CN");
                    LanguageUIList.SelectedIndex = 1;
                    break;
                case "zh-TW":
                    currentCulture = new CultureInfo("zh-TW");
                    LanguageUIList.SelectedIndex = 2;
                    break;
                case "ja-JP":
                    currentCulture = new CultureInfo("ja-JP");
                    LanguageUIList.SelectedIndex = 3;
                    break;
                default:
                    currentCulture = new CultureInfo("en-US");
                    LanguageUIList.SelectedIndex = 0;
                    break;
            }
            ApplyLocalization();
        }

        // 文件名前缀文本
        private void FileNameText_Click(object sender, EventArgs e)
        {

        }

        // 导出路径文本
        private void OutPutText_Click(object sender, EventArgs e)
        {

        }

        // 当前分辨率文本
        private void resolutiontiptext_Click(object sender, EventArgs e)
        {

        }

        // 当前帧数显示
        private void CurrentFrameView_Click(object sender, EventArgs e)
        {

        }

        // --------------------
        // ==== 自动更新数值 =====
        // ---------------------
        private void frameTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (mmdProcess == null || mmdProcess.HasExited)
                {
                    Process[] processes = Process.GetProcessesByName("MikuMikuDance"); // 获取MikuMikuDance进程
                    if (processes.Length == 0)
                    {
                        CurrentFrameView.Text = resManager.GetString("MMDNotrunning", currentCulture); // MikuMikuDance未运行
                        widthValue.Text = ""; // 清空宽度
                        HeightValue.Text = ""; // 清空高度
                        return;
                    }

                    mmdProcess = processes[0]; // 获取第一个进程
                    processHandle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, mmdProcess.Id); // 打开进程句柄
                }

                IntPtr frameAddress = IntPtr.Zero; // 读取帧数地址
                int? frameValue = null; // 读取帧数值

                // 读取帧数地址
                var candidates = new List<(string moduleName, int baseOffset, int[] offsets)>
                {
                    ("MikuMikuDance.exe", 0x1445F8, new int[] { 0x1450 }), // 9.26以上版本64位基址：MikuMikuDance.exe+1445F8  偏移：1450
                    ("MikuMikuDance.exe", 0x14295C, new int[] { 0x978 }), // 9.26版32位基址：MikuMikuDance.exe+14295C   偏移：978
                    ("MikuMikuDance.exe", 0x14593C, new int[] { 0x980 }), // 9.32版32位基址：MikuMikuDance.exe+14593C   偏移：980
                };

                // 只显示第一个有效帧数（32位和64位都支持）
                foreach (var candidate in FrameCandidates)
                {
                    IntPtr baseAddress = GetModuleBaseAddress(candidate.ModuleName);
                    if (baseAddress == IntPtr.Zero) continue;
                    IntPtr pointer = baseAddress + candidate.BaseOffset;
                    IntPtr finalAddress = ResolvePointerChain(pointer, new int[] { candidate.PointerOffset }); // 从候选列表选择地址
                    try
                    {
                        int value = ReadInt32(finalAddress);
                        // 只要不是0就用（避免0 / 100的情况，优先显示非0帧数）
                        if (value != 0)
                        {
                            frameValue = value;
                            break;
                        }
                        // 如果全是0，也会显示0
                        if (!frameValue.HasValue)
                            frameValue = value;
                    }
                    catch { }
                }
                if (frameValue.HasValue)
                {
                    CurrentFrameView.Text = string.Format(resManager.GetString("CurrentFrameViewText", currentCulture), frameValue.Value); // $"当前帧数：{frameValue.Value}"
                }
                else
                {
                    CurrentFrameView.Text = resManager.GetString("ReadFail", currentCulture); // 读取失败
                }
                // ========== 同时读取所有偏移 ==========
                try
                {
                    // 当前帧数
                    List<int> frameList = new List<int>();
                    foreach (var candidate in FrameCandidates)
                    {
                        IntPtr baseAddress = GetModuleBaseAddress(candidate.ModuleName);
                        if (baseAddress == IntPtr.Zero) continue;
                        IntPtr pointer = baseAddress + candidate.BaseOffset; // 基址
                        IntPtr finalAddress = ResolvePointerChain(pointer, new int[] { candidate.PointerOffset }); // 从候选列表选择地址
                        try
                        {
                            int value = ReadInt32(finalAddress);
                            if (!frameList.Contains(value))
                            {
                                frameList.Add(value);
                            }
                        }
                        catch { }
                    }
                    if (frameList.Count > 0)
                    {
                        CurrentFrameView.Text = string.Format(resManager.GetString("CurrentFrameViewText", currentCulture), frameValue.Value); // $"当前帧数：{frameValue.Value}"
                    }
                    else
                    {
                        CurrentFrameView.Text = resManager.GetString("ReadFail", currentCulture); // 读取失败
                    }

                    // 分辨率-宽度
                    List<int> widthList = new List<int>();
                    foreach (var candidate in WidthCandidates)
                    {
                        IntPtr baseAddress = GetModuleBaseAddress(candidate.ModuleName);
                        if (baseAddress == IntPtr.Zero) continue;
                        IntPtr pointer = baseAddress + candidate.BaseOffset;
                        IntPtr finalAddress = ResolvePointerChain(pointer, new int[] { candidate.PointerOffset }); // 从候选列表选择地址
                        try
                        {   // 读取分辨率宽度
                            int value = ReadInt32(finalAddress);
                            if (value >= 200 && value <= 9999 && !widthList.Contains(value)) // 设定最大4位数范围
                            {
                                widthList.Add(value);
                            }
                        }
                        catch { }
                    }
                    widthValue.Text = widthList.Count > 0 ? string.Join(" / ", widthList) : "";

                    // 分辨率-高度
                    List<int> heightList = new List<int>();
                    foreach (var candidate in HeightCandidates)
                    {
                        IntPtr baseAddress = GetModuleBaseAddress(candidate.ModuleName);
                        if (baseAddress == IntPtr.Zero) continue;
                        IntPtr pointer = baseAddress + candidate.BaseOffset;
                        IntPtr finalAddress = ResolvePointerChain(pointer, new int[] { candidate.PointerOffset });
                        try
                        {   // 读取分辨率高度
                            int value = ReadInt32(finalAddress);
                            if (value >= 200 && value <= 9999 && !heightList.Contains(value))  // 设定最大4位数范围
                            {
                                heightList.Add(value);
                            }
                        }
                        catch { }
                    }
                    HeightValue.Text = heightList.Count > 0 ? string.Join(" / ", heightList) : "";
                }
                catch
                {
                    widthValue.Text = "";
                    HeightValue.Text = "";
                }
            }
            catch
            {
                CurrentFrameView.Text = resManager.GetString("ReadFail", currentCulture); // 读取失败
                widthValue.Text = "";
                HeightValue.Text = "";
            }
        }

        // 获取模块基址
        private IntPtr GetModuleBaseAddress(string moduleName)
        {
            if (mmdProcess == null) return IntPtr.Zero;

            foreach (ProcessModule module in mmdProcess.Modules)
            {
                if (module.ModuleName.Equals(moduleName, StringComparison.OrdinalIgnoreCase))
                {
                    return module.BaseAddress;
                }
            }
            return IntPtr.Zero;
        }

        // 解析指针链
        private IntPtr ResolvePointerChain(IntPtr baseAddr, int[] offsets)
        {
            IntPtr currentAddr = baseAddr;
            int bytesRead;
            for (int i = 0; i < offsets.Length; i++)
            {
                if (IntPtr.Size == 8) // 64位
                {
                    byte[] buffer = new byte[8];
                    ReadProcessMemory(processHandle, currentAddr, buffer, buffer.Length, out bytesRead);
                    currentAddr = (IntPtr)(BitConverter.ToInt64(buffer, 0) + offsets[i]);
                }
                else // 32位
                {
                    byte[] buffer = new byte[4];
                    ReadProcessMemory(processHandle, currentAddr, buffer, buffer.Length, out bytesRead);
                    currentAddr = (IntPtr)(BitConverter.ToInt32(buffer, 0) + offsets[i]);
                }
            }
            return currentAddr;
        }

        // 读取模块内存整数值
        private int ReadInt32(IntPtr address)
        {
            byte[] buffer = new byte[4];
            int bytesRead;
            ReadProcessMemory(processHandle, address, buffer, buffer.Length, out bytesRead);
            return BitConverter.ToInt32(buffer, 0);
        }

        // 定义帧数候选地址结构
        public class MemoryAddressCandidate
        {
            public string ModuleName { get; set; }
            public int BaseOffset { get; set; }
            public int PointerOffset { get; set; }
            public string Description { get; set; }
        }
        // 定义分辨率候选地址结构
        public class ResolutionAddressCandidate
        {
            public string ModuleName { get; set; }
            public int BaseOffset { get; set; }
            public int PointerOffset { get; set; }
            public string Description { get; set; }
        }
		
        // 定义帧候选地址列表
        private static readonly List<MemoryAddressCandidate> FrameCandidates = new List<MemoryAddressCandidate>
        {
            new MemoryAddressCandidate { ModuleName = "MikuMikuDance.exe", BaseOffset = 0x1445F8, PointerOffset = 0x1450, Description = "64位/新版本" }, // 9.26以上版本64位基址：MikuMikuDance.exe+1445F8  偏移：1450
            new MemoryAddressCandidate { ModuleName = "MikuMikuDance.exe", BaseOffset = 0x14295C, PointerOffset = 0x978, Description = "9.26 32位" }, // 9.26版32位基址：MikuMikuDance.exe+14295C   偏移：978
            new MemoryAddressCandidate { ModuleName = "MikuMikuDance.exe", BaseOffset = 0x14593C, PointerOffset = 0x980, Description = "9.32 32位" } // 9.32版32位基址：MikuMikuDance.exe+14593C   偏移：980
        };
		
        // 定义分辨率候选地址列表-宽度
        private static readonly List<ResolutionAddressCandidate> WidthCandidates = new List<ResolutionAddressCandidate>
        {
            new ResolutionAddressCandidate { ModuleName = "MikuMikuDance.exe", BaseOffset = 0x1445F8, PointerOffset = 0xA18F0, Description = "9.26 64位宽度" }, // 9.26版64位宽度基址：MikuMikuDance.exe+1445F8  偏移：A18F0
            new ResolutionAddressCandidate { ModuleName = "MikuMikuDance.exe", BaseOffset = 0x14295C, PointerOffset = 0xA0884, Description = "9.26 32位宽度" }, // 9.26版32位宽度基址：MikuMikuDance.exe+14295C  偏移：A0884
            new ResolutionAddressCandidate { ModuleName = "MikuMikuDance.exe", BaseOffset = 0x1445F8, PointerOffset = 0xA18F8, Description = "9.32 64位宽度" }, // 9.32版64位宽度基址：MikuMikuDance.exe+1445F8  偏移：A18F8
            new ResolutionAddressCandidate { ModuleName = "MikuMikuDance.exe", BaseOffset = 0x14593C, PointerOffset = 0xA08D4, Description = "9.32 32位宽度" } // 9.32版32位宽度基址：MikuMikuDance.exe+14593C  偏移：A08D4   
        };
		
        // 定义分辨率候选地址列表-高度
        private static readonly List<ResolutionAddressCandidate> HeightCandidates = new List<ResolutionAddressCandidate>
        {
            new ResolutionAddressCandidate { ModuleName = "MikuMikuDance.exe", BaseOffset = 0x1445F8, PointerOffset = 0xA18F4, Description = "9.26 64位高度" }, // 9.26版64位高度基址：MikuMikuDance.exe+1445F8  偏移：A18F4
            new ResolutionAddressCandidate { ModuleName = "MikuMikuDance.exe", BaseOffset = 0x14295C, PointerOffset = 0xA0888, Description = "9.26 32位高度" }, // 9.26版32位高度基址：MikuMikuDance.exe+14295C  偏移：A0888
            new ResolutionAddressCandidate { ModuleName = "MikuMikuDance.exe", BaseOffset = 0x1445F8, PointerOffset = 0xA18FC, Description = "9.32 64位高度" }, // 9.32版64位高度基址：MikuMikuDance.exe+1445F8  偏移：A18FC
            new ResolutionAddressCandidate { ModuleName = "MikuMikuDance.exe", BaseOffset = 0x14593C, PointerOffset = 0xA08D8, Description = "9.32 32位高度" } // 9.32版32位高度基址：MikuMikuDance.exe+14593C  偏移：A08D8
        };

        // ==== 置顶图标 =====
        private bool isTopMost = true;
        private void TOPUP_Click(object sender, EventArgs e)
        {
            isTopMost = !isTopMost;
            this.TopMost = isTopMost;
            if (isTopMost)
            {
                // 置顶状态：蓝色背景，蓝色图标
                TOPUP.BackColor = Color.DodgerBlue;
            }
            else
            {
                // 未置顶状态：默认背景色，灰色图标
                TOPUP.BackColor = SystemColors.Control; 
            }
        }

        // 日志文本框
        private void loglist_TextChanged(object sender, EventArgs e)
        {
        }

        // 分辨率宽度
        private void widthValue_Click(object sender, EventArgs e)
        {
        }

        // 分辨率高度
        private void HeightValue_Click(object sender, EventArgs e)
        {
        }

        // ==== 仅单帧保存 =====
        private void Singleframe_CheckedChanged(object sender, EventArgs e)
        {
            // 如果选中单帧保存，禁用连续帧输入框
            if (Singleframe.Checked) {
                FrameUP.Text = ""; // 清空帧开始框
                FrameDown.Text = ""; // 清空帧结束框
                locationViewXY.Text = ""; // 清空坐标值
                FrameUP.Enabled = false; // 禁用帧开始框
                FrameDown.Enabled = false; // 禁用帧结束框
                FrameClear.Enabled = false; // 禁用清空按钮
                coordinate.Enabled = false; // 禁用坐标输入框
            }
            else {
                FrameUP.Enabled = true;
                FrameDown.Enabled = true;
                FrameClear.Enabled = true;
                coordinate.Enabled = true;
            }
        }

        // ==== 连续帧选项 ====
        private void FramerangeText_CheckedChanged(object sender, EventArgs e)
        {
            if (FramerangeText.Checked) // 如果开启
            {
                FrameUP.Text = ""; // 清空帧开始框
                FrameDown.Text = ""; // 清空帧结束框
                locationViewXY.Text = ""; // 清空坐标值
                Singleframe.Checked = false; // 禁用单帧保存
                FrameUP.Enabled = true; // 启用连续帧开始输入框
                FrameDown.Enabled = true; // 启用连续帧结束输入框
                FrameClear.Enabled = true; // 启用清空按钮
            }
            else
            {
                Singleframe.Checked = true; // 启用单帧保存
                FramerangeText.Checked = false; // 禁用连续帧选项
                FrameUP.Enabled = false; // 禁用连续帧开始输入框
                FrameDown.Enabled = false; // 禁用连续帧结束输入框
                FrameClear.Enabled = false; // 禁用清空按钮
            }
        }

        // --------------------
        // ==== 自定义按键 =====
        // --------------------
        private Keys saveHotkey = Keys.None;
        private int hotkeyId = 100; // 热键ID，任意唯一值
        private bool isEditingHotkey = false; // 是否正在编辑热键
        private void Shortcutkeys_TextChanged(object sender, EventArgs e)
        {    
            // 不在这里写入配置
            //if (string.IsNullOrWhiteSpace(Shortcutkeys.Text)) return; // 没有输入内容时直接返回
            //ini.Write("Config", "Shortcutkeys", Shortcutkeys.Text); // 保存到ini文件
        }
        private void Shortcutkeys_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true; // 阻止输入字符

            // 如果用户按下 Enter，触发 Leave（失焦）
            if (e.KeyCode == Keys.Enter)
            {
                RegisterHotkeyFromTextBox();
                e.SuppressKeyPress = true;
                return;
            }

            // 拼接热键组合
            List<string> keys = new List<string>();
            if (e.Control) keys.Add("Ctrl");
            if (e.Alt) keys.Add("Alt");
            if (e.Shift) keys.Add("Shift");

            // 添加主键
            if (e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.Menu)
            {
                keys.Add(e.KeyCode.ToString());
            }

            // 检查是否与MMD菜单栏冲突
            if (e.Alt && (
                e.KeyCode == Keys.F ||
                e.KeyCode == Keys.D ||
                e.KeyCode == Keys.V ||
                e.KeyCode == Keys.B ||
                e.KeyCode == Keys.M ||
                e.KeyCode == Keys.P ||
                e.KeyCode == Keys.K ||
                e.KeyCode == Keys.H))
            {
                MessageBox.Show(
                    resManager.GetString("HotkeyConflictMsg", currentCulture), // 这个按键与MMD菜单会有冲突，请更换其他按键。
                    resManager.GetString("HotkeyConflictTitle", currentCulture), // 快捷键冲突
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Shortcutkeys.Text = "";
                e.SuppressKeyPress = true;
                return;
            }

            Shortcutkeys.Text = string.Join("+", keys); // 显示按键信息
            // e.SuppressKeyPress = true; // 阻止实际的字符被输入到 TextBox
            // Shortcutkeys.ReadOnly = true;  // 不需要手动输入内容，只允许按键捕捉。
        }

        // 注册热键
        private void Shortcutkeys_Leave(object sender, EventArgs e)
        {
            string text = Shortcutkeys.Text.Trim();

            if (string.IsNullOrEmpty(text))
                return;

            // 解析修饰符和主键
            uint mod = 0;
            Keys key = Keys.None;
            if (text.Contains("Ctrl")) mod |= MOD_CONTROL;
            if (text.Contains("Alt")) mod |= MOD_ALT;
            if (text.Contains("Shift")) mod |= MOD_SHIFT;

            // 获取主键
            string[] parts = text.Split('+');
            if (parts.Length > 0)
            {
                string keyPart = parts[parts.Length - 1].Trim();
                Enum.TryParse(keyPart, out key);
            }

            // 防止注册无效热键
            if (key == Keys.None)
            {
                MessageBox.Show(resManager.GetString("invalidkey", currentCulture)); // 快捷键设置无效，请重新输入。
                Shortcutkeys.Text = "";
                return;
            }

            // 注销旧热键
            UnregisterHotKey(this.Handle, hotkeyId);

            // 注册新热键
            bool success = RegisterHotKey(this.Handle, hotkeyId, mod, (uint)key);
            if (success)
            {
                ini.Write("Config", "Shortcutkeys", text); // 写入 INI
                // 显示提示标签（或弹窗）
                hotkeyStatusLabel.Text = resManager.GetString("Keyregistered", currentCulture); // 热键已注册
                hotkeyStatusLabel.ForeColor = Color.Green;
                hotkeyStatusLabel.Visible = true;
            }
            else
            {
                MessageBox.Show(resManager.GetString("Registrationfail", currentCulture)); // 热键注册失败，可能已被其他程序占用。
                Shortcutkeys.Text = "";
            }

            Shortcutkeys.ReadOnly = true; // 锁定编辑
            //isEditingHotkey = false; // 退出编辑状态
        }

        // 双击快捷键输入框重新编辑
        //private void Shortcutkeys_DoubleClick(object sender, EventArgs e)
        //{
        //    isEditingHotkey = true;
        //    Shortcutkeys.ReadOnly = false;
        //    Shortcutkeys.Text = "";
        //    Shortcutkeys.Focus();
        //}

        // 点击编辑按钮输入框重新编辑快捷键
        private void KeyEdit_Click(object sender, EventArgs e)
        {
            if (!Shortcutkeys.ReadOnly) return; // 已经在编辑中，不重复处理

            Shortcutkeys.ReadOnly = false;
            Shortcutkeys.Focus();
            Shortcutkeys.SelectAll();
            hotkeyStatusLabel.Text = resManager.GetString("NewKey", currentCulture); // 请输入按键
            hotkeyStatusLabel.ForeColor = Color.Green; // 注册成功后变绿色
            hotkeyStatusLabel.Visible = true;
        }

        // 捕获快捷键并触发保存
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (saveHotkey != Keys.None &&
                !string.IsNullOrWhiteSpace(Shortcutkeys.Text) &&
                keyData == saveHotkey)
            {
                if (!isSavingInProgress)
                {
                    FinalSave_button_Click(null, null); // 触发保存逻辑
                }
                return true; // 表示已处理该快捷键
            }
            return base.ProcessCmdKey(ref msg, keyData); // 否则传给父类处理
        }

        //  响应全局热键
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            if (m.Msg == WM_HOTKEY)
            {
                if (m.WParam.ToInt32() == hotkeyId)
                {
                    FinalSave_button_Click(null, null); // 触发保存
                }
            }
            base.WndProc(ref m);
        }

        // 关闭窗体时注销热键
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, hotkeyId);
            if (currentAhkProcess != null)
            {
                try
                {
                    if (!currentAhkProcess.HasExited)
                        currentAhkProcess.Kill();
                    currentAhkProcess.Dispose();
                }
                catch { }
            }
            base.OnFormClosing(e);
        }

        // 确认按键按钮
        private void recognizeKey_Click(object sender, EventArgs e)
        {
            RegisterHotkeyFromTextBox();
        }

        private void RegisterHotkeyFromTextBox()
        {
            string text = Shortcutkeys.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                hotkeyStatusLabel.Text = ""; // 快捷键为空，未注册。
                return;
            }

            // 解析修饰符和主键
            uint mod = 0;
            Keys key = Keys.None;
            if (text.Contains("Ctrl")) mod |= MOD_CONTROL;
            if (text.Contains("Alt")) mod |= MOD_ALT;
            if (text.Contains("Shift")) mod |= MOD_SHIFT;

            string[] parts = text.Split(new[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0)
            {
                string keyPart = parts[parts.Length - 1].Trim();
                Enum.TryParse(keyPart, out key);
            }

            // 注销旧热键
            UnregisterHotKey(this.Handle, hotkeyId);

            // 注册新热键
            if (key != Keys.None)
            {
                bool success = RegisterHotKey(this.Handle, hotkeyId, mod, (uint)key);
                if (success)
                {
                    saveHotkey = key; // 保存热键
                    ini.Write("Config", "Shortcutkeys", Shortcutkeys.Text); // 写入INI
                    hotkeyStatusLabel.Text = resManager.GetString("Keyregistered", currentCulture); // 热键已注册
                }
                else
                {
                   // hotkeyStatusLabel.Text = "注册快捷键失败，可能已被其他程序占用。";
                }
            }

            Shortcutkeys.ReadOnly = true; // 锁定编辑
        }

        // ---------------------
        // ==== 导出路径 =====
        // ---------------------
        // 导出路径文本框
        private void OutPath_TextChanged(object sender, EventArgs e)
        {
            // 保存到ini文件
            ini.Write("Config", "OutPath", OutPath.Text);
        }

        // 导出路径按钮
        private void outpath_button_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    OutPath.Text = dialog.SelectedPath;
                }
            }
        }

        // 获取当前帧
        private int GetCurrentFrame()
        {
            int? firstValue = null;
            foreach (var candidate in FrameCandidates)
            {
                IntPtr baseAddress = GetModuleBaseAddress(candidate.ModuleName);
                if (baseAddress == IntPtr.Zero) continue;

                IntPtr pointer = baseAddress + candidate.BaseOffset;
                IntPtr finalAddress = ResolvePointerChain(pointer, new int[] { candidate.PointerOffset });
                try
                {
                    int value = ReadInt32(finalAddress);
                    if (value != 0)
                        return value; // 优先返回非0帧数
                    if (!firstValue.HasValue)
                        firstValue = value; // 记录第一个值
                }
                catch { }
            }
            return firstValue ?? -1; // 如果全是0，返回0；否则-1
        }

        // ---------------------
        // ==== 最终保存 =====
        // ---------------------
        // 保存按钮
        private void FinalSave_button_Click(object sender, EventArgs e)
        {
            if (isSavingInProgress) return; // 防止重复触发
            isSavingInProgress = true;

            if (FramerangeText.Checked == true) // 连续帧保存
            {
                // 检查是否已获取坐标
                if (string.IsNullOrWhiteSpace(locationViewXY.Text) || locationViewXY.Text == "X=0 Y=0")
                {
                    MessageBox.Show(resManager.GetString("PromptGetLocation", currentCulture)); // 请先在“+”处获取屏幕位置再保存噢！
                    isSavingInProgress = false;
                    return;
                }
                // 检查是否已输入帧数范围
                if (string.IsNullOrWhiteSpace(FrameUP.Text) || string.IsNullOrWhiteSpace(FrameDown.Text))
                {
                    MessageBox.Show(resManager.GetString("PromptInputFrameRange", currentCulture)); // 请先输入帧数范围再保存噢！
                    isSavingInProgress = false;
                    return;
                }
            }

            // 显示进度条（UI线程）
            this.Invoke(new Action(() =>
            {
                saveProgressBar.Value = 0;
                saveProgressBar.Visible = true; // 显示进度条
                Progressdisplay.Visible = true; // 显示帧数进度
            }));

            // 禁用相关控件，防止保存过程中用户误操作
            this.Invoke(new Action(() => {
                Singleframe.Enabled = false; // 禁用单帧保存
                FrameUP.Enabled = false; // 禁用连续帧开始框
                FrameDown.Enabled = false; // 禁用连续帧结束框
                FrameClear.Enabled = false; // 禁用连续帧清空按钮
                coordinate.Enabled = false; // 禁用坐标输入框
                OutPath.Enabled = false; // 禁用导出路径
                Fileprefix.Enabled = false; // 禁用文件名前缀
                imageType.Enabled = false; // 禁用图片格式
                delaytimeValue.Enabled = false; // 禁用保存等待时间
                SaveAfterFolder.Enabled = false; // 禁用保存后打开文件夹
                FinalSave_button.Enabled = false; // 禁用保存按钮，防止重复点击
                StopSavebutton.Enabled = true; // 允许暂停
                Enableimagecompress.Enabled = false; // 禁用开启图片压缩
                Filesuffix.Enabled = false; // 禁用文件后缀
                ConvertFormat.Enabled = false; // 禁用转换格式
                imgcompress_OutPath.Enabled = false; // 禁用输出路径
                imgcompress_OutPath_button.Enabled = false; // 禁用输出路径按钮
                SourceDirectory.Enabled = false; // 禁用源文件夹
                DeleteSourceFiles.Enabled = false; // 禁用删除源文件
                JPGqualityValue.Enabled = false; // 禁用JPG/Webp/Avif压缩质量
                PNGqualityValue.Enabled = false; // 禁用PNG压缩级别
                imgSize.Enabled = false; // 禁用图片尺寸
                imgSizeWidthValue.Enabled = false; // 禁用图片宽度
                imgSizeHeightValue.Enabled = false; //禁用图片高度
            }));

            Task.Run(() =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(OutPath.Text) || // 检查导出路径是否已选择
                        string.IsNullOrWhiteSpace(Fileprefix.Text) || // 检查文件名前缀是否已输入
                        imageType.SelectedItem == null)
                    {
                        MessageBox.Show(resManager.GetString("EnsurePathPrefixType", currentCulture)); // 请确保已选择路径、文件名前缀和图片格式！
                        return;
                    }
                    string prefix = Fileprefix.Text.Trim(); // 文件名前缀
                    string extension = imageType.SelectedItem.ToString().ToLower(); // 图片格式
                    string outputPath = OutPath.Text.Trim();  // 导出路径
                    if (string.IsNullOrEmpty(outputPath))
                    {
                        MessageBox.Show(resManager.GetString("PathEmpty", currentCulture)); // 导出路径为空，请先选择导出路径！
                        return;
                    }
                    string date = DateTime.Now.ToString("yyyyMMdd"); // 当前日期

                    // 创建导出路径
                    if (!Directory.Exists(outputPath))
                        Directory.CreateDirectory(outputPath);

                    // 清空保存列表
                    savedFileList.Clear();

                    // 检查是否为连续帧保存
                    int startFrame = 0, endFrame = 0;
                    bool isRange = int.TryParse(FrameUP.Text, out startFrame)
                        && int.TryParse(FrameDown.Text, out endFrame)
                        && startFrame <= endFrame;

                    // 读取坐标，异常时默认为 0
                    int frameBoxX = 0, frameBoxY = 0;
                    int.TryParse(ini.Read("Config", "FrameBoxX"), out frameBoxX);
                    int.TryParse(ini.Read("Config", "FrameBoxY"), out frameBoxY);

                    // -----------------
                    //  连续帧  
                    // -----------------
                    if (isRange)
                    {
                        isBatchSaving = true; // 开始批量保存
                        int totalFrames = endFrame - startFrame + 1; // 更新进度条
                        int currentFrameIndex = 0; // 当前帧索引

                        for (int frame = startFrame; frame <= endFrame; frame++)
                        {
                            // 写入帧数到内存（所有候选地址都写一遍，兼容不同MMD版本）
                            foreach (var candidate in FrameCandidates)
                            {
                                IntPtr baseAddress = GetModuleBaseAddress(candidate.ModuleName);
                                if (baseAddress == IntPtr.Zero) continue;
                                IntPtr pointer = baseAddress + candidate.BaseOffset;
                                IntPtr finalAddress = ResolvePointerChain(pointer, new int[] { candidate.PointerOffset });
                                WriteInt32(finalAddress, frame);
                            }

                            // 检查当前帧是否设置成功
                            int currentFrame = GetCurrentFrame();
                            if (currentFrame < 0)
                            {
                                AppendLog(resManager.GetString("LogNoFrame", currentCulture)); // 未能获取当前帧数，将忽略帧号保存。
                            }

                            // 自动递增文件名
                            int index = 1;
                            string filename;
                            string fullPath;
                            do
                            {
                                if (frame >= 0) // 如果获取到帧数，则包含帧号
                                                // 文件名格式，[文件前缀]_[帧数]_[日期时间]_[序号].[扩展名]
                                    filename = $"{prefix}_frame{frame}_{date}_{index:D3}.{extension}"; // 例如：剧照图_frame123_20250101_001.png
                                else // 如果无法获取帧数，则不包含帧号
                                    filename = $"{prefix}_{date}_{index:D3}.{extension}";
                                fullPath = Path.Combine(outputPath, filename); // 完整路径
                                index++; // 递增索引
                            } while (File.Exists(fullPath));

                            // 模拟导出操作，调用AHK脚本模拟操作，让AHK脚本输出日志
                            ProcessStartInfo psi = new ProcessStartInfo
                            {
                                FileName = "Script\\FrameRange_save.exe",
                                Arguments = $"{frameBoxX} {frameBoxY} {frame} \"{fullPath}\"",
                                UseShellExecute = false, // 不使用操作系统外壳程序启动，必须设为false，否则无法重定向输出。
                                RedirectStandardOutput = true, // 允许读取标准输出
                                RedirectStandardError = true, // 允许读取标准错误输出
                                CreateNoWindow = true // 防止弹出黑框窗口
                            };

                            currentAhkProcess = new Process
                            {
                                StartInfo = psi
                            };

                            currentAhkProcess.Start();

                            // 异步读取标准输出，会读取整个输出内容，适合一次性任务。
                            string output = currentAhkProcess.StandardOutput.ReadToEnd();
                            string error = currentAhkProcess.StandardError.ReadToEnd();

                            currentAhkProcess.WaitForExit(); // 等待退出

                            // 释放 AHK 进程资源（资源释放）
                            if (currentAhkProcess != null)
                            {
                                if (!currentAhkProcess.HasExited)
                                    currentAhkProcess.Kill();
                                currentAhkProcess.Dispose();
                                currentAhkProcess = null;
                            }

                            // 日志输出
                            if (loglist != null && !loglist.IsDisposed)
                            {
                                AppendLog(string.Format(resManager.GetString("LogSaving", currentCulture), filename)); // $"正在保存：{filename}..."
                            }

                            // 等待保存完成
                            int delayMs = (int)delaytimeValue.Value;
                            //Thread.Sleep(delayMs);
                            int slept = 0;
                            while (slept < delayMs)
                            {
                                if (!isBatchSaving) break;
                                Thread.Sleep(100);
                                slept += 100;
                            }

                            // 输出到日志
                            if (!string.IsNullOrWhiteSpace(output))
                            {
                                AppendLog(resManager.GetString("LogSaveSuccess", currentCulture)); // 文件保存成功！
                                AppendLog(string.Format(resManager.GetString("LogSavePath", currentCulture), fullPath)); // $"文件保存在：{fullPath}"
                                savedFileList.Add(fullPath); // 记录已保存文件
                            }

                            if (!string.IsNullOrWhiteSpace(error))
                            {
                                AppendLog(resManager.GetString("LogSaveFail", currentCulture)); // 文件未保存成功，可能输入失败或路径有误。
                            }

                            // 更新进度条
                            currentFrameIndex++;
                            int progress = (int)((currentFrameIndex * 100.0) / totalFrames);
                            this.Invoke(new Action(() =>
                            {
                                saveProgressBar.Value = progress;
                                Progressdisplay.Text = $"{currentFrameIndex}/{totalFrames}"; //{currentFrameIndex}是已保存的帧数,{totalFrames}是总帧数
                            }));

                            // 用户请求停止时跳出循环
                            if (!isBatchSaving)
                            {
                                currentAhkProcess?.Kill();
                                break;
                            }
                        }
                        isBatchSaving = false;
                    }
                    // -----------------
                    //  单帧保存  
                    // -----------------
                    else
                    {
                        int frame = GetCurrentFrame();
                        if (frame < 0)
                        {
                            AppendLog(resManager.GetString("LogNoFrame", currentCulture)); // 未能获取当前帧数，将忽略帧号保存。
                        }

                        // 自动递增文件名
                        int index = 1;
                        string filename;
                        string fullPath;
                        do
                        {
                            if (frame >= 0) // 如果获取到帧数，则包含帧号
                                            // 文件名格式，[文件前缀]_[帧数]_[日期时间]_[序号].[扩展名]
                                filename = $"{prefix}_frame{frame}_{date}_{index:D3}.{extension}";  // 例如：剧照图_frame123_20231001_001.png
                            else        // 如果无法获取帧数，则不包含帧号
                                filename = $"{prefix}_{date}_{index:D3}.{extension}";
                            fullPath = Path.Combine(outputPath, filename);  // 完整路径
                            index++; // 递增索引
                        } while (File.Exists(fullPath));

                        // 模拟导出操作，调用AHK脚本模拟操作，让AHK脚本输出日志
                        ProcessStartInfo psi = new ProcessStartInfo
                        {
                            FileName = "Script\\SingleSave.exe",
                            Arguments = $"\"{fullPath}\"",
                            UseShellExecute = false, // 不使用操作系统外壳程序启动，必须设为false，否则无法重定向输出。
                            RedirectStandardOutput = true, // 允许读取标准输出
                            RedirectStandardError = true, // 允许读取标准错误输出
                            CreateNoWindow = true // 防止弹出黑框窗口
                        };

                        currentAhkProcess = new Process
                        {
                            StartInfo = psi
                        };

                        currentAhkProcess.Start();

                        // 异步读取标准输出，会读取整个输出内容，适合一次性任务。
                        string output = currentAhkProcess.StandardOutput.ReadToEnd();
                        string error = currentAhkProcess.StandardError.ReadToEnd();

                        currentAhkProcess.WaitForExit(); // 等待退出

                        // 释放 AHK 进程资源（资源释放）
                        if (currentAhkProcess != null)
                        {
                            if (!currentAhkProcess.HasExited)
                                currentAhkProcess.Kill();
                            currentAhkProcess.Dispose();
                            currentAhkProcess = null;
                        }

                        // 日志输出
                        if (loglist != null && !loglist.IsDisposed)
                        {
                            AppendLog(string.Format(resManager.GetString("LogSaving", currentCulture), filename)); // $"正在保存：{filename}..."
                        }

                        // 等待保存完成
                        int delayMs = (int)delaytimeValue.Value;
                        //Thread.Sleep(delayMs);
                        int slept = 0;
                        while (slept < delayMs)
                        {
                            if (!isBatchSaving) break;
                            Thread.Sleep(100);
                            slept += 100;
                        }

                        // 输出到日志
                        if (!string.IsNullOrWhiteSpace(output))
                        {
                            AppendLog(resManager.GetString("LogSaveSuccess", currentCulture)); // 文件保存成功！
                            AppendLog(string.Format(resManager.GetString("LogSavePath", currentCulture), fullPath)); // $"文件保存在：{fullPath}"
                            savedFileList.Add(fullPath); // 记录已保存文件
                        }

                        if (!string.IsNullOrWhiteSpace(error))
                        {
                            AppendLog(resManager.GetString("LogSaveFail", currentCulture)); // 文件未保存成功，可能输入失败或路径有误。
                        }
                    }

                    //  如果勾选了“保存后自动打开文件夹”选项，自动打开导出路径的文件夹
                    if (SaveAfterFolder.Checked)
                    {
                        Process.Start("explorer.exe", outputPath); // 打开文件夹
                    }

                    // 如果勾选了“压缩图片”选项，进行图片压缩
                    if (Enableimagecompress.Checked)
                    {
                        CompressImages(outputPath, prefix, date, extension); // 遍历所有已保存的图片，进行压缩
                    }
                }
                finally
                {
                    isBatchSaving = false; // 批量保存结束
                    isSavingInProgress = false; // 恢复保存状态
                    this.Invoke(new Action(() => {
                        Singleframe.Enabled = true; // 恢复单帧保存
                        FrameUP.Enabled = true; // 恢复连续帧开始输入框
                        FrameDown.Enabled = true; // 恢复连续帧结束输入框
                        FrameClear.Enabled = true; // 恢复清空按钮
                        coordinate.Enabled = true; // 恢复坐标输入框
                        OutPath.Enabled = true; // 恢复导出路径输入框
                        Fileprefix.Enabled = true; // 恢复文件名前缀输入框
                        imageType.Enabled = true; // 恢复图片格式下拉框
                        delaytimeValue.Enabled = true; // 恢复保存等待时间输入框
                        SaveAfterFolder.Enabled = true; // 恢复保存后打开文件夹选项
                        FinalSave_button.Enabled = true; // 恢复保存按钮
                        StopSavebutton.Enabled = false; // 暂停按钮恢复为不可用
                        saveProgressBar.Visible = false; // 隐藏进度条
                        Progressdisplay.Visible = false; // 隐藏进度显示
                        // 如果开启了图片压缩
                        if (Enableimagecompress.Checked == true) 
                        {
                            Enableimagecompress.Enabled = true; // 恢复开启图片压缩
                            Filesuffix.Enabled = true; // 恢复文件后缀
                            ConvertFormat.Enabled = true; // 恢复转换格式
                            imgcompress_OutPath.Enabled = true; // 恢复输出路径
                            imgcompress_OutPath_button.Enabled = true; // 恢复输出路径按钮
                            SourceDirectory.Enabled = true; // 恢复源文件夹
                            DeleteSourceFiles.Enabled = true; // 恢复删除源文件
                            JPGqualityValue.Enabled = true; // 恢复JPG/Webp/Avif压缩质量
                            PNGqualityValue.Enabled = true; // 恢复PNG压缩级别
                            imgSize.Enabled = true; // 恢复图片尺寸
                            imgSizeWidthValue.Enabled = true; // 恢复图片宽度
                            imgSizeHeightValue.Enabled = true; // 恢复图片高度
                        }
                        else {
                            Enableimagecompress.Enabled = true; // 禁用开启图片压缩
                            Filesuffix.Enabled = false; // 禁用文件后缀
                            ConvertFormat.Enabled = false; // 禁用转换格式
                            imgcompress_OutPath.Enabled = false; // 禁用输出路径
                            imgcompress_OutPath_button.Enabled = false; // 禁用输出路径按钮
                            SourceDirectory.Enabled = false; // 禁用源文件夹
                            DeleteSourceFiles.Enabled = false; // 禁用删除源文件
                            JPGqualityValue.Enabled = false; // 禁用JPG/Webp/Avif压缩质量
                            PNGqualityValue.Enabled = false; // 禁用PNG压缩级别
                            imgSize.Enabled = false; // 禁用图片尺寸
                            imgSizeWidthValue.Enabled = false; // 禁用图片宽度
                            imgSizeHeightValue.Enabled = false; //禁用图片高度
                        }
                    }));
                }
            });
        }

        // 连续帧清除按键
        private void FrameClear_Click(object sender, EventArgs e)
        {
            FrameUP.Text = "";
            FrameUP.ReadOnly = false;
            FrameDown.Text = "";
            FrameDown.ReadOnly = false;
        }

        // 连续帧数字输入框
        private void FrameNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入数字和控制键（如退格）
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // 保存后自动打开文件夹 选项
        private void SaveAfterFolder_CheckedChanged(object sender, EventArgs e)
        {
            // 保存到ini文件
            ini.Write("Config", "SaveAfterFolder", SaveAfterFolder.Checked ? "1" : "0");
        }

        // 保存等待时间
        private void delaytimeValue_ValueChanged(object sender, EventArgs e)
        {
            if (isLoading) return; // 初始化时不写入
            ini.Write("Config", "delaytimeValue", delaytimeValue.Value.ToString());  // 保存到ini文件
        }

        // 文件名前缀文本框
        private void Fileprefix_TextChanged(object sender, EventArgs e)
        {
            // 保存到ini文件
            ini.Write("Config", "Fileprefix", Fileprefix.Text);
        }

        // 文件图片类型
        private void imageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return; // 初始化时不写入
            ini.Write("Config", "imageType", imageType.SelectedItem?.ToString() ?? ""); // 保存到ini文件
        }

        // 暂停保存按钮
        private void StopSavebutton_Click(object sender, EventArgs e)
        {
            isBatchSaving = false; // 关键：通知保存线程退出循环
            if (currentAhkProcess != null)
            {
                try
                {
                    if (!currentAhkProcess.HasExited)
                        currentAhkProcess.Kill();
                    currentAhkProcess.Dispose();
                    currentAhkProcess = null;
                }
                catch (Exception ex)
                {
                    AppendLog(string.Format(resManager.GetString("LogKillProcessException", currentCulture), ex.Message)); // "终止进程时发生异常：" + ex.Message
                }
            }
        }

        // -------------
        // ini文件操作类
        // -------------
        public class IniFile
        {
            public string Path;

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

            public IniFile(string path) { Path = path; }

            public void Write(string section, string key, string value)
            {
                WritePrivateProfileString(section, key, value, Path);
            }

            public string Read(string section, string key, string defaultValue = "")
            {
                var retVal = new StringBuilder(255);
                GetPrivateProfileString(section, key, defaultValue, retVal, 255, Path);
                return retVal.ToString();
            }
        }

        // ------------------
        // ==== 坐标拾取 =====
        // ------------------
        // 设定坐标变量
        private int frameBoxX = 0;
        private int frameBoxY = 0;

        // 坐标按钮
        private void coordinate_Click(object sender, EventArgs e)
        {
            isPicking = true;
            coordinate.BackColor = Color.Orange; // 开启拾取时变色
            this.Cursor = Cursors.Cross; // 鼠标变成十字
            // Cursors.Cross  十字线
            // Cursors.Hand  手形
            // Cursors.Default  默认光标
            // Cursors.WaitCursor  沙漏/等待

            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseDownExt += GlobalHook_MouseDownExt;
        }

        // 全局钩子鼠标按下事件
        private void GlobalHook_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            if (!isPicking) return;

            // 直接显示全局屏幕坐标
            locationViewXY.Text = $"X={e.X} Y={e.Y}";

            // 关闭拾取
            isPicking = false;
            coordinate.BackColor = SystemColors.Control;
            this.Cursor = Cursors.Default;
            m_GlobalHook.MouseDownExt -= GlobalHook_MouseDownExt;
            m_GlobalHook.Dispose();

            // 在 GlobalHook_MouseDownExt 里赋值
            frameBoxX = e.X;
            frameBoxY = e.Y;

            // 保存到ini文件，方便下次直接用
            ini.Write("Config", "FrameBoxX", frameBoxX.ToString());
            ini.Write("Config", "FrameBoxY", frameBoxY.ToString());
        }

        // 界面语言选项
        private void LanguageUIList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lang = "en-US";
            switch (LanguageUIList.SelectedIndex)
            {
                case 0: // English
                    lang = "en-US";
                    break;
                case 1: // 简体中文
                    lang = "zh-CN";
                    break;
                case 2: // 繁體中文
                    lang = "zh-TW";
                    break;
                case 3: // 日本語
                    lang = "ja-JP";
                    break;
            }
            currentCulture = new CultureInfo(lang);
            ini.Write("Config", "Language", lang); // 写入ini
            ApplyLocalization();
        }

        // 提示内容文本
        private void Promptcontent_TextChanged(object sender, EventArgs e)
        {
            Promptcontent.Multiline = true; // 允许多行
            Promptcontent.ReadOnly = true; // 只读
            Promptcontent.ScrollBars = ScrollBars.Vertical; // 显示垂直滚动条
            Promptcontent.Dock = DockStyle.Fill; // 填充整个标签页
            tabPage3.Controls.Add(Promptcontent); // 添加到标签页
        }
        private void ApplyLocalization()
        {
            // 多语言翻译，控件静态文本，按钮、标签等
            this.Text = resManager.GetString("FormTitle", currentCulture); // 标题
            LanguageText.Text = resManager.GetString("LanguageText", currentCulture); // 界面语言文本
            resolutiontiptext.Text = resManager.GetString("ResolutionTipText", currentCulture); // 当前分辨率文本
            Singleframe.Text = resManager.GetString("SingleFrameText", currentCulture); // 仅单帧保存文本
            FramerangeText.Text = resManager.GetString("FrameRangeText", currentCulture); // 连续帧文本
            FramerangeTip.Text = resManager.GetString("FrameRangeTipText", currentCulture); // 连续帧提示文本
            ShortcutkeysText.Text = resManager.GetString("ShortcutKeysText", currentCulture); // 快捷键文本
            FileNameText.Text = resManager.GetString("FileNameText", currentCulture); // 文件名前缀文本
            FiletypeText.Text = resManager.GetString("ImageTypeText", currentCulture); // 文件类型文本
            OutPutText.Text = resManager.GetString("OutPutText", currentCulture); // 导出路径文本
            waitingtimeText.Text = resManager.GetString("DelayTimeText", currentCulture); // 保存等待时间文本
            SaveAfterFolder.Text = resManager.GetString("SaveAfterFolderText", currentCulture); // 保存后自动打开文件夹文本
            tabPage1.Text = resManager.GetString("TabPage1Text", currentCulture); // 开始标签
            tabPage2.Text = resManager.GetString("TabPage2Text", currentCulture); // 设置标签
            tabPage3.Text = resManager.GetString("TabPage3Text", currentCulture); // 小提示标签
            tabPage4.Text = resManager.GetString("TabPage4Text", currentCulture); // 图片压缩设置标签
            tabPage5.Text = resManager.GetString("TabPage5Text", currentCulture); // 图片压缩调整标签
            FrameClear.Text = resManager.GetString("FrameClearText", currentCulture); // 连续帧清除按钮
            FinalSave_button.Text = resManager.GetString("FinalSaveButtonText", currentCulture); // 保存按钮
            outpath_button.Text = resManager.GetString("OutPathButtonText", currentCulture); // 导出路径-浏览按钮
            StopSavebutton.Text = resManager.GetString("StopSaveButtonText", currentCulture); // 暂停保存按钮
            TOPUPTip2.SetToolTip(TOPUP, resManager.GetString("TopMostTipText", currentCulture)); // 设置置顶提示文本  
            Promptcontent.Text = resManager.GetString("PromptContentText", currentCulture); // 提示内容文本
            Enableimagecompress.Text = resManager.GetString("EnableImageCompressText", currentCulture); // 启用图片压缩文本
            FilenamesuffixText.Text = resManager.GetString("FilenamesuffixText", currentCulture); // 文件后缀文本
            ConvertFormatText.Text = resManager.GetString("ConvertFormatLIST", currentCulture); // 转换格式文本
            imgcompress_OutPathText.Text = resManager.GetString("ImageCompressPathText", currentCulture); // 图片压缩输出路径文本
            imgcompress_OutPath_button.Text = resManager.GetString("ImageCompressOutPathButtonText", currentCulture); // 图片压缩输出路径-浏览按钮
            SourceDirectory.Text = resManager.GetString("SourceDirectoryText", currentCulture); // 源目录文本
            DeleteSourceFiles.Text = resManager.GetString("DeleteSourceFilesText", currentCulture); // 删除源文件文本
            JPGqualityText.Text = resManager.GetString("JPGQualityText", currentCulture); // JPG/Webp/Avif压缩质量文本
            PNGqualityText.Text = resManager.GetString("PNGQualityText", currentCulture); // PNG质量文本
            imgSize.Text = resManager.GetString("ImageSizeText", currentCulture); // 图片尺寸文本
            hotkeyStatusLabel.Text = resManager.GetString("HotkeyStatusLabelText", currentCulture); // 热键状态文本
        }

        // 开启图片压缩，控制启用/禁用相关控件
        private void Enableimagecompress_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = Enableimagecompress.Checked; // 启用图片压缩
            Filesuffix.Enabled = enabled; // 启用文件后缀
            ConvertFormat.Enabled = enabled; // 启用转换格式
            imgcompress_OutPath.Enabled = enabled; // 启用输出路径
            imgcompress_OutPath_button.Enabled = enabled; // 启用输出路径浏览按钮
            SourceDirectory.Enabled = enabled; // 启用源目录
            DeleteSourceFiles.Enabled = enabled; // 启用删除源文件
            JPGqualityValue.Enabled = enabled; // 启用JPG/Webp/Avif压缩质量
            PNGqualityValue.Enabled = enabled; // 启用PNG压缩级别
            imgSize.Enabled = enabled; // 启用图片尺寸
            imgSizeWidthValue.Enabled = enabled && imgSize.Checked; // 启用图片宽度
            imgSizeHeightValue.Enabled = enabled && imgSize.Checked; // 启用图片高度
        }

        // 图片尺寸开启
        private void imgSize_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = imgSize.Checked && Enableimagecompress.Checked;
            imgSizeWidthValue.Enabled = enabled; // 启用图片宽度
            imgSizeHeightValue.Enabled = enabled; // 启用图片高度
        }

        // 图片压缩源目录
        private void SourceDirectory_CheckedChanged(object sender, EventArgs e)
        {
            if (SourceDirectory.Checked)
            {
                imgcompress_OutPath.Enabled = false; // 禁用输出路径
                imgcompress_OutPath_button.Enabled = false; // 禁用浏览按钮
            }
            else
            {
                imgcompress_OutPath.Enabled = true; // 启用输出路径
                imgcompress_OutPath_button.Enabled = true; // 启用浏览按钮
            }
        }

        // 图片压缩输出路径
        private void imgcompress_OutPath_button_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imgcompress_OutPath.Text = dialog.SelectedPath;
                }
            }
        }

        // 图片压缩命令行
        private void CompressImages(string outputPath, string prefix, string date, string extension)
        {
            foreach (var fullPath in savedFileList)
            {
                string inputFile = fullPath;
                string suffix = Filesuffix.Text.Trim();
                string format = ConvertFormat.SelectedItem?.ToString() ?? extension;
                string outDir = SourceDirectory.Checked ? outputPath : imgcompress_OutPath.Text.Trim();
                string outName = Path.GetFileNameWithoutExtension(fullPath) + (string.IsNullOrEmpty(suffix) ? "" : $"_{suffix}") + "." + format;
                string outputFile = Path.Combine(outDir, outName);

                // 日志：正在压缩
                AppendLog(string.Format(resManager.GetString("Compressing", currentCulture), Path.GetFileName(inputFile), Path.GetFileName(outputFile))); // 输出日志：$"正在压缩：{Path.GetFileName(inputFile)} → {Path.GetFileName(outputFile)}"

                var args = new List<string>();
                args.Add($"\"{inputFile}\"");

                // 缩放
                if (imgSize.Checked)
                {   // magick input.jpg -filter Lanczos -resize 800x600 output.jpg
                    args.Add("-filter Lanczos");
                    args.Add($"-resize {imgSizeWidthValue.Value}x{imgSizeHeightValue.Value}");
                }

                // 质量/压缩参数
                if (format == "jpg" || format == "webp" || format == "avif") // magick input.jpg -quality 75 output.jpg
                {
                    int quality = JPGqualityValue.Value > 0 ? (int)JPGqualityValue.Value : 85;
                    args.Add($"-quality {quality}");
                }
                else if (format == "png") // magick input.png -define png:compression-level=9 output.png
                {
                    int level = PNGqualityValue.Value > 0 ? (int)PNGqualityValue.Value : 9;
                    args.Add($"-define png:compression-level={level}");
                }
                else if (format == "gif") //magick input.gif -layers Optimize output.gif
                {
                    args.Add("-layers Optimize");
                }
                else if (format == "tga") // magick input.tga -compress RLE output.tga
                {
                    args.Add("-compress RLE");
                }

                args.Add($"\"{outputFile}\"");

                // 调用magick.exe
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "magick.exe",
                    Arguments = string.Join(" ", args),
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                using (var proc = Process.Start(psi))
                {
                    proc.WaitForExit();
                }

                // 日志：压缩完成
                AppendLog(string.Format(resManager.GetString("CompressDone", currentCulture), Path.GetFileName(outputFile))); // 输出日志："图片压缩完成：{Path.GetFileName(outputFile)}"

                // 删除源文件
                if (DeleteSourceFiles.Checked && File.Exists(inputFile))
                {
                    try { File.Delete(inputFile); } catch { }
                }
            }
        }

        // ============ END ============
    }
}
