namespace MMD_FrameShot_Save
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.LanguageText = new System.Windows.Forms.Label();
            this.LanguageUIList = new System.Windows.Forms.ComboBox();
            this.FrameUP = new System.Windows.Forms.TextBox();
            this.FrameDown = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CurrentFrameView = new System.Windows.Forms.Label();
            this.FileNameText = new System.Windows.Forms.Label();
            this.Fileprefix = new System.Windows.Forms.TextBox();
            this.FiletypeText = new System.Windows.Forms.Label();
            this.imageType = new System.Windows.Forms.ComboBox();
            this.OutPutText = new System.Windows.Forms.Label();
            this.OutPath = new System.Windows.Forms.TextBox();
            this.outpath_button = new System.Windows.Forms.Button();
            this.FinalSave_button = new System.Windows.Forms.Button();
            this.loglist = new System.Windows.Forms.RichTextBox();
            this.resolutiontiptext = new System.Windows.Forms.Label();
            this.frameTimer = new System.Windows.Forms.Timer(this.components);
            this.TOPUP = new System.Windows.Forms.Button();
            this.widthValue = new System.Windows.Forms.Label();
            this.HeightValue = new System.Windows.Forms.Label();
            this.resolutionX = new System.Windows.Forms.Label();
            this.ShortcutkeysText = new System.Windows.Forms.Label();
            this.Shortcutkeys = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.waitingtimeText = new System.Windows.Forms.Label();
            this.delaytimeValue = new System.Windows.Forms.NumericUpDown();
            this.SaveAfterFolder = new System.Windows.Forms.CheckBox();
            this.mstext = new System.Windows.Forms.Label();
            this.TOPUPTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.FrameClear = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Singleframe = new System.Windows.Forms.RadioButton();
            this.FramerangeText = new System.Windows.Forms.RadioButton();
            this.FramerangeTip = new System.Windows.Forms.Label();
            this.locationViewXY = new System.Windows.Forms.Label();
            this.coordinate = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.recognizeKey = new System.Windows.Forms.Button();
            this.hotkeyStatusLabel = new System.Windows.Forms.Label();
            this.KeyEdit = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Promptcontent = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.FilenamesuffixText = new System.Windows.Forms.Label();
            this.Filesuffix = new System.Windows.Forms.TextBox();
            this.ConvertFormatText = new System.Windows.Forms.Label();
            this.ConvertFormat = new System.Windows.Forms.ComboBox();
            this.DeleteSourceFiles = new System.Windows.Forms.CheckBox();
            this.SourceDirectory = new System.Windows.Forms.CheckBox();
            this.imgcompress_OutPath_button = new System.Windows.Forms.Button();
            this.imgcompress_OutPath = new System.Windows.Forms.TextBox();
            this.imgcompress_OutPathText = new System.Windows.Forms.Label();
            this.Enableimagecompress = new System.Windows.Forms.CheckBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.imageSizeX = new System.Windows.Forms.Label();
            this.JPGqualityText = new System.Windows.Forms.Label();
            this.JPGqualityValue = new System.Windows.Forms.NumericUpDown();
            this.imgSizeHeightValue = new System.Windows.Forms.NumericUpDown();
            this.PNGqualityText = new System.Windows.Forms.Label();
            this.PNGqualityValue = new System.Windows.Forms.NumericUpDown();
            this.imgSize = new System.Windows.Forms.CheckBox();
            this.imgSizeWidthValue = new System.Windows.Forms.NumericUpDown();
            this.StopSavebutton = new System.Windows.Forms.Button();
            this.saveProgressBar = new System.Windows.Forms.ProgressBar();
            this.Progressdisplay = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.delaytimeValue)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.JPGqualityValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSizeHeightValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PNGqualityValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSizeWidthValue)).BeginInit();
            this.SuspendLayout();
            // 
            // LanguageText
            // 
            this.LanguageText.AutoSize = true;
            this.LanguageText.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LanguageText.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LanguageText.Location = new System.Drawing.Point(401, 31);
            this.LanguageText.Name = "LanguageText";
            this.LanguageText.Size = new System.Drawing.Size(129, 29);
            this.LanguageText.TabIndex = 0;
            this.LanguageText.Text = "界面语言";
            this.LanguageText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LanguageUIList
            // 
            this.LanguageUIList.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LanguageUIList.FormattingEnabled = true;
            this.LanguageUIList.Location = new System.Drawing.Point(545, 23);
            this.LanguageUIList.Name = "LanguageUIList";
            this.LanguageUIList.Size = new System.Drawing.Size(198, 37);
            this.LanguageUIList.TabIndex = 1;
            this.LanguageUIList.SelectedIndexChanged += new System.EventHandler(this.LanguageUIList_SelectedIndexChanged);
            // 
            // FrameUP
            // 
            this.FrameUP.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FrameUP.Location = new System.Drawing.Point(193, 84);
            this.FrameUP.Name = "FrameUP";
            this.FrameUP.Size = new System.Drawing.Size(127, 41);
            this.FrameUP.TabIndex = 4;
            this.FrameUP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrameNumber_KeyPress);
            // 
            // FrameDown
            // 
            this.FrameDown.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FrameDown.Location = new System.Drawing.Point(382, 85);
            this.FrameDown.Name = "FrameDown";
            this.FrameDown.Size = new System.Drawing.Size(127, 41);
            this.FrameDown.TabIndex = 5;
            this.FrameDown.Text = "300000";
            this.FrameDown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrameNumber_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(330, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "—";
            // 
            // CurrentFrameView
            // 
            this.CurrentFrameView.AutoSize = true;
            this.CurrentFrameView.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CurrentFrameView.Location = new System.Drawing.Point(490, 93);
            this.CurrentFrameView.Name = "CurrentFrameView";
            this.CurrentFrameView.Size = new System.Drawing.Size(111, 33);
            this.CurrentFrameView.TabIndex = 7;
            this.CurrentFrameView.Text = "当前帧";
            this.CurrentFrameView.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CurrentFrameView.Click += new System.EventHandler(this.CurrentFrameView_Click);
            // 
            // FileNameText
            // 
            this.FileNameText.AutoSize = true;
            this.FileNameText.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FileNameText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FileNameText.Location = new System.Drawing.Point(11, 24);
            this.FileNameText.Name = "FileNameText";
            this.FileNameText.Size = new System.Drawing.Size(158, 29);
            this.FileNameText.TabIndex = 8;
            this.FileNameText.Text = "文件名前缀";
            this.FileNameText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FileNameText.Click += new System.EventHandler(this.FileNameText_Click);
            // 
            // Fileprefix
            // 
            this.Fileprefix.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Fileprefix.Location = new System.Drawing.Point(210, 17);
            this.Fileprefix.Name = "Fileprefix";
            this.Fileprefix.Size = new System.Drawing.Size(206, 41);
            this.Fileprefix.TabIndex = 9;
            this.Fileprefix.TextChanged += new System.EventHandler(this.Fileprefix_TextChanged);
            // 
            // FiletypeText
            // 
            this.FiletypeText.AutoSize = true;
            this.FiletypeText.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FiletypeText.Location = new System.Drawing.Point(458, 24);
            this.FiletypeText.Name = "FiletypeText";
            this.FiletypeText.Size = new System.Drawing.Size(129, 29);
            this.FiletypeText.TabIndex = 10;
            this.FiletypeText.Text = "文件类型";
            this.FiletypeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imageType
            // 
            this.imageType.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imageType.FormattingEnabled = true;
            this.imageType.Location = new System.Drawing.Point(618, 17);
            this.imageType.Name = "imageType";
            this.imageType.Size = new System.Drawing.Size(152, 37);
            this.imageType.TabIndex = 11;
            this.imageType.SelectedIndexChanged += new System.EventHandler(this.imageType_SelectedIndexChanged);
            // 
            // OutPutText
            // 
            this.OutPutText.AutoSize = true;
            this.OutPutText.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OutPutText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OutPutText.Location = new System.Drawing.Point(11, 96);
            this.OutPutText.Name = "OutPutText";
            this.OutPutText.Size = new System.Drawing.Size(129, 29);
            this.OutPutText.TabIndex = 12;
            this.OutPutText.Text = "导出路径";
            this.OutPutText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OutPutText.Click += new System.EventHandler(this.OutPutText_Click);
            // 
            // OutPath
            // 
            this.OutPath.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OutPath.Location = new System.Drawing.Point(150, 86);
            this.OutPath.Name = "OutPath";
            this.OutPath.Size = new System.Drawing.Size(469, 41);
            this.OutPath.TabIndex = 13;
            this.OutPath.TextChanged += new System.EventHandler(this.OutPath_TextChanged);
            // 
            // outpath_button
            // 
            this.outpath_button.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outpath_button.Location = new System.Drawing.Point(647, 84);
            this.outpath_button.Name = "outpath_button";
            this.outpath_button.Size = new System.Drawing.Size(125, 57);
            this.outpath_button.TabIndex = 14;
            this.outpath_button.Text = "浏览";
            this.outpath_button.UseVisualStyleBackColor = true;
            this.outpath_button.Click += new System.EventHandler(this.outpath_button_Click);
            // 
            // FinalSave_button
            // 
            this.FinalSave_button.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FinalSave_button.Location = new System.Drawing.Point(152, 491);
            this.FinalSave_button.Name = "FinalSave_button";
            this.FinalSave_button.Size = new System.Drawing.Size(224, 62);
            this.FinalSave_button.TabIndex = 15;
            this.FinalSave_button.Text = "保存";
            this.FinalSave_button.UseVisualStyleBackColor = true;
            this.FinalSave_button.Click += new System.EventHandler(this.FinalSave_button_Click);
            // 
            // loglist
            // 
            this.loglist.Font = new System.Drawing.Font("宋体", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loglist.Location = new System.Drawing.Point(28, 574);
            this.loglist.Name = "loglist";
            this.loglist.Size = new System.Drawing.Size(802, 139);
            this.loglist.TabIndex = 16;
            this.loglist.Text = "";
            this.loglist.TextChanged += new System.EventHandler(this.loglist_TextChanged);
            // 
            // resolutiontiptext
            // 
            this.resolutiontiptext.AutoSize = true;
            this.resolutiontiptext.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.resolutiontiptext.Location = new System.Drawing.Point(38, 93);
            this.resolutiontiptext.Name = "resolutiontiptext";
            this.resolutiontiptext.Size = new System.Drawing.Size(158, 29);
            this.resolutiontiptext.TabIndex = 17;
            this.resolutiontiptext.Text = "当前分辨率";
            this.resolutiontiptext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.resolutiontiptext.Click += new System.EventHandler(this.resolutiontiptext_Click);
            // 
            // frameTimer
            // 
            this.frameTimer.Enabled = true;
            this.frameTimer.Tick += new System.EventHandler(this.frameTimer_Tick);
            // 
            // TOPUP
            // 
            this.TOPUP.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.TOPUP.BackColor = System.Drawing.SystemColors.Control;
            this.TOPUP.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TOPUP.Image = ((System.Drawing.Image)(resources.GetObject("TOPUP.Image")));
            this.TOPUP.Location = new System.Drawing.Point(764, 20);
            this.TOPUP.Name = "TOPUP";
            this.TOPUP.Size = new System.Drawing.Size(74, 57);
            this.TOPUP.TabIndex = 18;
            this.TOPUPTip2.SetToolTip(this.TOPUP, "窗口置顶");
            this.TOPUP.UseVisualStyleBackColor = false;
            this.TOPUP.Click += new System.EventHandler(this.TOPUP_Click);
            // 
            // widthValue
            // 
            this.widthValue.AutoSize = true;
            this.widthValue.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.widthValue.Location = new System.Drawing.Point(218, 93);
            this.widthValue.Name = "widthValue";
            this.widthValue.Size = new System.Drawing.Size(73, 29);
            this.widthValue.TabIndex = 19;
            this.widthValue.Text = "1920";
            this.widthValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.widthValue.Click += new System.EventHandler(this.widthValue_Click);
            // 
            // HeightValue
            // 
            this.HeightValue.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HeightValue.Location = new System.Drawing.Point(328, 93);
            this.HeightValue.Name = "HeightValue";
            this.HeightValue.Size = new System.Drawing.Size(80, 29);
            this.HeightValue.TabIndex = 20;
            this.HeightValue.Text = "1080";
            this.HeightValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.HeightValue.Click += new System.EventHandler(this.HeightValue_Click);
            // 
            // resolutionX
            // 
            this.resolutionX.AutoSize = true;
            this.resolutionX.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.resolutionX.Location = new System.Drawing.Point(294, 93);
            this.resolutionX.Name = "resolutionX";
            this.resolutionX.Size = new System.Drawing.Size(28, 29);
            this.resolutionX.TabIndex = 21;
            this.resolutionX.Text = "X";
            // 
            // ShortcutkeysText
            // 
            this.ShortcutkeysText.AutoSize = true;
            this.ShortcutkeysText.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ShortcutkeysText.Location = new System.Drawing.Point(11, 225);
            this.ShortcutkeysText.Name = "ShortcutkeysText";
            this.ShortcutkeysText.Size = new System.Drawing.Size(100, 29);
            this.ShortcutkeysText.TabIndex = 23;
            this.ShortcutkeysText.Text = "快捷键";
            // 
            // Shortcutkeys
            // 
            this.Shortcutkeys.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Shortcutkeys.Location = new System.Drawing.Point(138, 218);
            this.Shortcutkeys.Name = "Shortcutkeys";
            this.Shortcutkeys.Size = new System.Drawing.Size(200, 41);
            this.Shortcutkeys.TabIndex = 24;
            this.Shortcutkeys.TextChanged += new System.EventHandler(this.Shortcutkeys_TextChanged);
            this.Shortcutkeys.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Shortcutkeys_KeyDown);
            this.Shortcutkeys.Leave += new System.EventHandler(this.Shortcutkeys_Leave);
            // 
            // waitingtimeText
            // 
            this.waitingtimeText.AutoSize = true;
            this.waitingtimeText.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.waitingtimeText.Location = new System.Drawing.Point(11, 160);
            this.waitingtimeText.Name = "waitingtimeText";
            this.waitingtimeText.Size = new System.Drawing.Size(187, 29);
            this.waitingtimeText.TabIndex = 25;
            this.waitingtimeText.Text = "保存等待时间";
            // 
            // delaytimeValue
            // 
            this.delaytimeValue.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.delaytimeValue.InterceptArrowKeys = false;
            this.delaytimeValue.Location = new System.Drawing.Point(209, 152);
            this.delaytimeValue.Name = "delaytimeValue";
            this.delaytimeValue.Size = new System.Drawing.Size(128, 41);
            this.delaytimeValue.TabIndex = 26;
            this.delaytimeValue.ValueChanged += new System.EventHandler(this.delaytimeValue_ValueChanged);
            // 
            // SaveAfterFolder
            // 
            this.SaveAfterFolder.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.SaveAfterFolder.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SaveAfterFolder.Location = new System.Drawing.Point(422, 158);
            this.SaveAfterFolder.Name = "SaveAfterFolder";
            this.SaveAfterFolder.Size = new System.Drawing.Size(365, 88);
            this.SaveAfterFolder.TabIndex = 27;
            this.SaveAfterFolder.Text = "保存后自动打开文件夹";
            this.SaveAfterFolder.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.SaveAfterFolder.UseVisualStyleBackColor = true;
            this.SaveAfterFolder.CheckedChanged += new System.EventHandler(this.SaveAfterFolder_CheckedChanged);
            // 
            // mstext
            // 
            this.mstext.AutoSize = true;
            this.mstext.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mstext.Location = new System.Drawing.Point(349, 157);
            this.mstext.Name = "mstext";
            this.mstext.Size = new System.Drawing.Size(43, 29);
            this.mstext.TabIndex = 28;
            this.mstext.Text = "ms";
            // 
            // FrameClear
            // 
            this.FrameClear.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FrameClear.Location = new System.Drawing.Point(530, 79);
            this.FrameClear.Name = "FrameClear";
            this.FrameClear.Size = new System.Drawing.Size(124, 55);
            this.FrameClear.TabIndex = 30;
            this.FrameClear.Text = "清除";
            this.FrameClear.UseVisualStyleBackColor = true;
            this.FrameClear.Click += new System.EventHandler(this.FrameClear_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Font = new System.Drawing.Font("宋体", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(28, 143);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(810, 332);
            this.tabControl1.TabIndex = 31;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Singleframe);
            this.tabPage1.Controls.Add(this.FramerangeText);
            this.tabPage1.Controls.Add(this.FramerangeTip);
            this.tabPage1.Controls.Add(this.locationViewXY);
            this.tabPage1.Controls.Add(this.coordinate);
            this.tabPage1.Controls.Add(this.FrameClear);
            this.tabPage1.Controls.Add(this.FrameUP);
            this.tabPage1.Controls.Add(this.FrameDown);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.Location = new System.Drawing.Point(8, 42);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(794, 282);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "开始";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Singleframe
            // 
            this.Singleframe.AutoSize = true;
            this.Singleframe.Location = new System.Drawing.Point(23, 33);
            this.Singleframe.Name = "Singleframe";
            this.Singleframe.Size = new System.Drawing.Size(189, 33);
            this.Singleframe.TabIndex = 36;
            this.Singleframe.TabStop = true;
            this.Singleframe.Text = "仅单帧保存";
            this.Singleframe.UseVisualStyleBackColor = true;
            this.Singleframe.CheckedChanged += new System.EventHandler(this.Singleframe_CheckedChanged);
            // 
            // FramerangeText
            // 
            this.FramerangeText.AutoSize = true;
            this.FramerangeText.Location = new System.Drawing.Point(23, 91);
            this.FramerangeText.Name = "FramerangeText";
            this.FramerangeText.Size = new System.Drawing.Size(131, 33);
            this.FramerangeText.TabIndex = 35;
            this.FramerangeText.TabStop = true;
            this.FramerangeText.Text = "连续帧";
            this.FramerangeText.UseVisualStyleBackColor = true;
            this.FramerangeText.CheckedChanged += new System.EventHandler(this.FramerangeText_CheckedChanged);
            // 
            // FramerangeTip
            // 
            this.FramerangeTip.Location = new System.Drawing.Point(13, 159);
            this.FramerangeTip.Name = "FramerangeTip";
            this.FramerangeTip.Size = new System.Drawing.Size(606, 121);
            this.FramerangeTip.TabIndex = 33;
            this.FramerangeTip.Text = "如果要用连续帧请先点击“+”按钮，并将光标移动到【帧操作】方框内进行定位，然后再输入需要连续截取的帧数。";
            // 
            // locationViewXY
            // 
            this.locationViewXY.Font = new System.Drawing.Font("宋体", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.locationViewXY.Location = new System.Drawing.Point(662, 161);
            this.locationViewXY.Name = "locationViewXY";
            this.locationViewXY.Size = new System.Drawing.Size(103, 94);
            this.locationViewXY.TabIndex = 32;
            // 
            // coordinate
            // 
            this.coordinate.Font = new System.Drawing.Font("宋体", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.coordinate.Image = global::MMD_FrameShot_Save.Properties.Resources.cross_hair_16x16;
            this.coordinate.Location = new System.Drawing.Point(670, 79);
            this.coordinate.Name = "coordinate";
            this.coordinate.Size = new System.Drawing.Size(74, 60);
            this.coordinate.TabIndex = 31;
            this.coordinate.UseVisualStyleBackColor = true;
            this.coordinate.Click += new System.EventHandler(this.coordinate_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.recognizeKey);
            this.tabPage2.Controls.Add(this.hotkeyStatusLabel);
            this.tabPage2.Controls.Add(this.KeyEdit);
            this.tabPage2.Controls.Add(this.FileNameText);
            this.tabPage2.Controls.Add(this.Fileprefix);
            this.tabPage2.Controls.Add(this.mstext);
            this.tabPage2.Controls.Add(this.FiletypeText);
            this.tabPage2.Controls.Add(this.SaveAfterFolder);
            this.tabPage2.Controls.Add(this.delaytimeValue);
            this.tabPage2.Controls.Add(this.imageType);
            this.tabPage2.Controls.Add(this.Shortcutkeys);
            this.tabPage2.Controls.Add(this.waitingtimeText);
            this.tabPage2.Controls.Add(this.OutPutText);
            this.tabPage2.Controls.Add(this.OutPath);
            this.tabPage2.Controls.Add(this.ShortcutkeysText);
            this.tabPage2.Controls.Add(this.outpath_button);
            this.tabPage2.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage2.Location = new System.Drawing.Point(8, 42);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(794, 282);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // recognizeKey
            // 
            this.recognizeKey.Image = global::MMD_FrameShot_Save.Properties.Resources.check_16x16;
            this.recognizeKey.Location = new System.Drawing.Point(440, 216);
            this.recognizeKey.Name = "recognizeKey";
            this.recognizeKey.Size = new System.Drawing.Size(70, 53);
            this.recognizeKey.TabIndex = 36;
            this.recognizeKey.UseVisualStyleBackColor = true;
            this.recognizeKey.Click += new System.EventHandler(this.recognizeKey_Click);
            // 
            // hotkeyStatusLabel
            // 
            this.hotkeyStatusLabel.AutoSize = true;
            this.hotkeyStatusLabel.Location = new System.Drawing.Point(522, 230);
            this.hotkeyStatusLabel.Name = "hotkeyStatusLabel";
            this.hotkeyStatusLabel.Size = new System.Drawing.Size(187, 29);
            this.hotkeyStatusLabel.TabIndex = 35;
            this.hotkeyStatusLabel.Text = "尚未设置热键";
            // 
            // KeyEdit
            // 
            this.KeyEdit.Image = global::MMD_FrameShot_Save.Properties.Resources.write_16x16;
            this.KeyEdit.Location = new System.Drawing.Point(360, 217);
            this.KeyEdit.Name = "KeyEdit";
            this.KeyEdit.Size = new System.Drawing.Size(70, 53);
            this.KeyEdit.TabIndex = 34;
            this.KeyEdit.UseVisualStyleBackColor = true;
            this.KeyEdit.Click += new System.EventHandler(this.KeyEdit_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.Promptcontent);
            this.tabPage3.Location = new System.Drawing.Point(8, 42);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(794, 282);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "小提示";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Promptcontent
            // 
            this.Promptcontent.BackColor = System.Drawing.SystemColors.Window;
            this.Promptcontent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Promptcontent.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Promptcontent.Location = new System.Drawing.Point(3, 3);
            this.Promptcontent.Multiline = true;
            this.Promptcontent.Name = "Promptcontent";
            this.Promptcontent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Promptcontent.Size = new System.Drawing.Size(788, 276);
            this.Promptcontent.TabIndex = 0;
            this.Promptcontent.Text = "提示";
            this.Promptcontent.TextChanged += new System.EventHandler(this.Promptcontent_TextChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.FilenamesuffixText);
            this.tabPage4.Controls.Add(this.Filesuffix);
            this.tabPage4.Controls.Add(this.ConvertFormatText);
            this.tabPage4.Controls.Add(this.ConvertFormat);
            this.tabPage4.Controls.Add(this.DeleteSourceFiles);
            this.tabPage4.Controls.Add(this.SourceDirectory);
            this.tabPage4.Controls.Add(this.imgcompress_OutPath_button);
            this.tabPage4.Controls.Add(this.imgcompress_OutPath);
            this.tabPage4.Controls.Add(this.imgcompress_OutPathText);
            this.tabPage4.Controls.Add(this.Enableimagecompress);
            this.tabPage4.Location = new System.Drawing.Point(8, 42);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(794, 282);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "图片压缩设置";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // FilenamesuffixText
            // 
            this.FilenamesuffixText.AutoSize = true;
            this.FilenamesuffixText.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FilenamesuffixText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FilenamesuffixText.Location = new System.Drawing.Point(20, 80);
            this.FilenamesuffixText.Name = "FilenamesuffixText";
            this.FilenamesuffixText.Size = new System.Drawing.Size(158, 29);
            this.FilenamesuffixText.TabIndex = 14;
            this.FilenamesuffixText.Text = "文件名后缀";
            this.FilenamesuffixText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Filesuffix
            // 
            this.Filesuffix.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Filesuffix.Location = new System.Drawing.Point(241, 75);
            this.Filesuffix.Name = "Filesuffix";
            this.Filesuffix.Size = new System.Drawing.Size(203, 41);
            this.Filesuffix.TabIndex = 15;
            // 
            // ConvertFormatText
            // 
            this.ConvertFormatText.AutoSize = true;
            this.ConvertFormatText.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConvertFormatText.Location = new System.Drawing.Point(20, 142);
            this.ConvertFormatText.Name = "ConvertFormatText";
            this.ConvertFormatText.Size = new System.Drawing.Size(129, 29);
            this.ConvertFormatText.TabIndex = 16;
            this.ConvertFormatText.Text = "转换格式";
            this.ConvertFormatText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConvertFormat
            // 
            this.ConvertFormat.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConvertFormat.FormattingEnabled = true;
            this.ConvertFormat.Location = new System.Drawing.Point(241, 142);
            this.ConvertFormat.Name = "ConvertFormat";
            this.ConvertFormat.Size = new System.Drawing.Size(149, 37);
            this.ConvertFormat.TabIndex = 17;
            // 
            // DeleteSourceFiles
            // 
            this.DeleteSourceFiles.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.DeleteSourceFiles.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DeleteSourceFiles.Location = new System.Drawing.Point(552, 121);
            this.DeleteSourceFiles.Name = "DeleteSourceFiles";
            this.DeleteSourceFiles.Size = new System.Drawing.Size(236, 70);
            this.DeleteSourceFiles.TabIndex = 13;
            this.DeleteSourceFiles.Text = "删除源文件";
            this.DeleteSourceFiles.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.DeleteSourceFiles.UseVisualStyleBackColor = true;
            // 
            // SourceDirectory
            // 
            this.SourceDirectory.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.SourceDirectory.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SourceDirectory.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.SourceDirectory.Location = new System.Drawing.Point(552, 47);
            this.SourceDirectory.Name = "SourceDirectory";
            this.SourceDirectory.Size = new System.Drawing.Size(242, 62);
            this.SourceDirectory.TabIndex = 12;
            this.SourceDirectory.Text = "源目录";
            this.SourceDirectory.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.SourceDirectory.UseVisualStyleBackColor = true;
            this.SourceDirectory.CheckedChanged += new System.EventHandler(this.SourceDirectory_CheckedChanged);
            // 
            // imgcompress_OutPath_button
            // 
            this.imgcompress_OutPath_button.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imgcompress_OutPath_button.Location = new System.Drawing.Point(634, 204);
            this.imgcompress_OutPath_button.Name = "imgcompress_OutPath_button";
            this.imgcompress_OutPath_button.Size = new System.Drawing.Size(99, 57);
            this.imgcompress_OutPath_button.TabIndex = 7;
            this.imgcompress_OutPath_button.Text = "浏览";
            this.imgcompress_OutPath_button.UseVisualStyleBackColor = true;
            this.imgcompress_OutPath_button.Click += new System.EventHandler(this.imgcompress_OutPath_button_Click);
            // 
            // imgcompress_OutPath
            // 
            this.imgcompress_OutPath.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imgcompress_OutPath.Location = new System.Drawing.Point(164, 206);
            this.imgcompress_OutPath.Name = "imgcompress_OutPath";
            this.imgcompress_OutPath.Size = new System.Drawing.Size(451, 41);
            this.imgcompress_OutPath.TabIndex = 6;
            // 
            // imgcompress_OutPathText
            // 
            this.imgcompress_OutPathText.AutoSize = true;
            this.imgcompress_OutPathText.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imgcompress_OutPathText.Location = new System.Drawing.Point(20, 211);
            this.imgcompress_OutPathText.Name = "imgcompress_OutPathText";
            this.imgcompress_OutPathText.Size = new System.Drawing.Size(129, 29);
            this.imgcompress_OutPathText.TabIndex = 5;
            this.imgcompress_OutPathText.Text = "输出目录";
            // 
            // Enableimagecompress
            // 
            this.Enableimagecompress.AutoSize = true;
            this.Enableimagecompress.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Enableimagecompress.Location = new System.Drawing.Point(25, 20);
            this.Enableimagecompress.Name = "Enableimagecompress";
            this.Enableimagecompress.Size = new System.Drawing.Size(219, 33);
            this.Enableimagecompress.TabIndex = 0;
            this.Enableimagecompress.Text = "开启图片压缩";
            this.Enableimagecompress.UseVisualStyleBackColor = true;
            this.Enableimagecompress.CheckedChanged += new System.EventHandler(this.Enableimagecompress_CheckedChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.imageSizeX);
            this.tabPage5.Controls.Add(this.JPGqualityText);
            this.tabPage5.Controls.Add(this.JPGqualityValue);
            this.tabPage5.Controls.Add(this.imgSizeHeightValue);
            this.tabPage5.Controls.Add(this.PNGqualityText);
            this.tabPage5.Controls.Add(this.PNGqualityValue);
            this.tabPage5.Controls.Add(this.imgSize);
            this.tabPage5.Controls.Add(this.imgSizeWidthValue);
            this.tabPage5.Location = new System.Drawing.Point(8, 42);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(794, 282);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "图片压缩调整";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // imageSizeX
            // 
            this.imageSizeX.AutoSize = true;
            this.imageSizeX.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imageSizeX.Location = new System.Drawing.Point(413, 175);
            this.imageSizeX.Name = "imageSizeX";
            this.imageSizeX.Size = new System.Drawing.Size(28, 29);
            this.imageSizeX.TabIndex = 35;
            this.imageSizeX.Text = "X";
            // 
            // JPGqualityText
            // 
            this.JPGqualityText.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.JPGqualityText.Location = new System.Drawing.Point(14, 28);
            this.JPGqualityText.Name = "JPGqualityText";
            this.JPGqualityText.Size = new System.Drawing.Size(414, 62);
            this.JPGqualityText.TabIndex = 1;
            this.JPGqualityText.Text = "JPG/Webp/Avif压缩质量";
            // 
            // JPGqualityValue
            // 
            this.JPGqualityValue.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.JPGqualityValue.Location = new System.Drawing.Point(445, 26);
            this.JPGqualityValue.Name = "JPGqualityValue";
            this.JPGqualityValue.Size = new System.Drawing.Size(120, 41);
            this.JPGqualityValue.TabIndex = 2;
            // 
            // imgSizeHeightValue
            // 
            this.imgSizeHeightValue.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imgSizeHeightValue.Location = new System.Drawing.Point(460, 173);
            this.imgSizeHeightValue.Name = "imgSizeHeightValue";
            this.imgSizeHeightValue.Size = new System.Drawing.Size(120, 41);
            this.imgSizeHeightValue.TabIndex = 11;
            // 
            // PNGqualityText
            // 
            this.PNGqualityText.AutoSize = true;
            this.PNGqualityText.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PNGqualityText.Location = new System.Drawing.Point(16, 95);
            this.PNGqualityText.Name = "PNGqualityText";
            this.PNGqualityText.Size = new System.Drawing.Size(174, 29);
            this.PNGqualityText.TabIndex = 3;
            this.PNGqualityText.Text = "PNG压缩级别";
            // 
            // PNGqualityValue
            // 
            this.PNGqualityValue.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PNGqualityValue.Location = new System.Drawing.Point(286, 93);
            this.PNGqualityValue.Name = "PNGqualityValue";
            this.PNGqualityValue.Size = new System.Drawing.Size(86, 41);
            this.PNGqualityValue.TabIndex = 4;
            // 
            // imgSize
            // 
            this.imgSize.AutoSize = true;
            this.imgSize.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imgSize.Location = new System.Drawing.Point(21, 173);
            this.imgSize.Name = "imgSize";
            this.imgSize.Size = new System.Drawing.Size(161, 33);
            this.imgSize.TabIndex = 10;
            this.imgSize.Text = "图片尺寸";
            this.imgSize.UseVisualStyleBackColor = true;
            this.imgSize.CheckedChanged += new System.EventHandler(this.imgSize_CheckedChanged);
            // 
            // imgSizeWidthValue
            // 
            this.imgSizeWidthValue.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imgSizeWidthValue.Location = new System.Drawing.Point(281, 172);
            this.imgSizeWidthValue.Name = "imgSizeWidthValue";
            this.imgSizeWidthValue.Size = new System.Drawing.Size(120, 41);
            this.imgSizeWidthValue.TabIndex = 9;
            // 
            // StopSavebutton
            // 
            this.StopSavebutton.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StopSavebutton.Location = new System.Drawing.Point(458, 491);
            this.StopSavebutton.Name = "StopSavebutton";
            this.StopSavebutton.Size = new System.Drawing.Size(222, 62);
            this.StopSavebutton.TabIndex = 32;
            this.StopSavebutton.Text = "暂停";
            this.StopSavebutton.UseVisualStyleBackColor = true;
            this.StopSavebutton.Click += new System.EventHandler(this.StopSavebutton_Click);
            // 
            // saveProgressBar
            // 
            this.saveProgressBar.Location = new System.Drawing.Point(28, 733);
            this.saveProgressBar.Name = "saveProgressBar";
            this.saveProgressBar.Size = new System.Drawing.Size(645, 30);
            this.saveProgressBar.TabIndex = 33;
            this.saveProgressBar.Visible = false;
            // 
            // Progressdisplay
            // 
            this.Progressdisplay.AutoSize = true;
            this.Progressdisplay.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Progressdisplay.Location = new System.Drawing.Point(693, 734);
            this.Progressdisplay.Name = "Progressdisplay";
            this.Progressdisplay.Size = new System.Drawing.Size(148, 29);
            this.Progressdisplay.TabIndex = 34;
            this.Progressdisplay.Text = "9999/9999";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 784);
            this.Controls.Add(this.Progressdisplay);
            this.Controls.Add(this.saveProgressBar);
            this.Controls.Add(this.StopSavebutton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.resolutionX);
            this.Controls.Add(this.HeightValue);
            this.Controls.Add(this.widthValue);
            this.Controls.Add(this.TOPUP);
            this.Controls.Add(this.resolutiontiptext);
            this.Controls.Add(this.loglist);
            this.Controls.Add(this.FinalSave_button);
            this.Controls.Add(this.CurrentFrameView);
            this.Controls.Add(this.LanguageUIList);
            this.Controls.Add(this.LanguageText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "MMD FrameShot Save";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.delaytimeValue)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.JPGqualityValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSizeHeightValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PNGqualityValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSizeWidthValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LanguageText;
        private System.Windows.Forms.ComboBox LanguageUIList;
        private System.Windows.Forms.TextBox FrameUP;
        private System.Windows.Forms.TextBox FrameDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CurrentFrameView;
        private System.Windows.Forms.Label FileNameText;
        private System.Windows.Forms.TextBox Fileprefix;
        private System.Windows.Forms.Label FiletypeText;
        private System.Windows.Forms.ComboBox imageType;
        private System.Windows.Forms.Label OutPutText;
        private System.Windows.Forms.TextBox OutPath;
        private System.Windows.Forms.Button outpath_button;
        private System.Windows.Forms.Button FinalSave_button;
        private System.Windows.Forms.RichTextBox loglist;
        private System.Windows.Forms.Label resolutiontiptext;
        private System.Windows.Forms.Timer frameTimer;
        private System.Windows.Forms.Button TOPUP;
        private System.Windows.Forms.Label widthValue;
        private System.Windows.Forms.Label HeightValue;
        private System.Windows.Forms.Label resolutionX;
        private System.Windows.Forms.Label ShortcutkeysText;
        private System.Windows.Forms.TextBox Shortcutkeys;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label waitingtimeText;
        private System.Windows.Forms.NumericUpDown delaytimeValue;
        private System.Windows.Forms.CheckBox SaveAfterFolder;
        private System.Windows.Forms.Label mstext;
        private System.Windows.Forms.ToolTip TOPUPTip2;
        private System.Windows.Forms.Button FrameClear;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button coordinate;
        private System.Windows.Forms.Label locationViewXY;
        private System.Windows.Forms.Label FramerangeTip;
        private System.Windows.Forms.Button StopSavebutton;
        private System.Windows.Forms.ProgressBar saveProgressBar;
        private System.Windows.Forms.Label Progressdisplay;
        private System.Windows.Forms.Button KeyEdit;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox Promptcontent;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox Enableimagecompress;
        private System.Windows.Forms.Label imgcompress_OutPathText;
        private System.Windows.Forms.Button imgcompress_OutPath_button;
        private System.Windows.Forms.TextBox imgcompress_OutPath;
        private System.Windows.Forms.CheckBox DeleteSourceFiles;
        private System.Windows.Forms.CheckBox SourceDirectory;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label JPGqualityText;
        private System.Windows.Forms.NumericUpDown JPGqualityValue;
        private System.Windows.Forms.Label PNGqualityText;
        private System.Windows.Forms.NumericUpDown PNGqualityValue;
        private System.Windows.Forms.NumericUpDown imgSizeWidthValue;
        private System.Windows.Forms.CheckBox imgSize;
        private System.Windows.Forms.NumericUpDown imgSizeHeightValue;
        private System.Windows.Forms.Label FilenamesuffixText;
        private System.Windows.Forms.TextBox Filesuffix;
        private System.Windows.Forms.Label ConvertFormatText;
        private System.Windows.Forms.ComboBox ConvertFormat;
        private System.Windows.Forms.Label imageSizeX;
        private System.Windows.Forms.RadioButton Singleframe;
        private System.Windows.Forms.RadioButton FramerangeText;
        private System.Windows.Forms.Label hotkeyStatusLabel;
        private System.Windows.Forms.Button recognizeKey;
    }
}