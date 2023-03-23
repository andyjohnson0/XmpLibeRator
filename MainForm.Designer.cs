namespace uk.andyjohnson.XmpLibeRator
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMui = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoMui = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMui = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.catalogueFilePathTbx = new System.Windows.Forms.TextBox();
            this.catalogueFileBrowseBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.outputDirPathTbx = new System.Windows.Forms.TextBox();
            this.outputDirBrowseBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.foldersTvw = new System.Windows.Forms.TreeView();
            this.selectAllCbx = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.includeImageExtInOutputFilenameCbx = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.mainMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(809, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMui});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitMui
            // 
            this.exitMui.Name = "exitMui";
            this.exitMui.Size = new System.Drawing.Size(93, 22);
            this.exitMui.Text = "E&xit";
            this.exitMui.Click += new System.EventHandler(this.ExitMui_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoMui,
            this.aboutMui});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // infoMui
            // 
            this.infoMui.Name = "infoMui";
            this.infoMui.Size = new System.Drawing.Size(137, 22);
            this.infoMui.Text = "&Information";
            this.infoMui.Click += new System.EventHandler(this.InfoMui_Click);
            // 
            // aboutMui
            // 
            this.aboutMui.Name = "aboutMui";
            this.aboutMui.Size = new System.Drawing.Size(137, 22);
            this.aboutMui.Text = "&About";
            this.aboutMui.Click += new System.EventHandler(this.AboutMui_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Catalogue file";
            // 
            // catalogueFilePathTbx
            // 
            this.catalogueFilePathTbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.catalogueFilePathTbx.Location = new System.Drawing.Point(98, 33);
            this.catalogueFilePathTbx.Name = "catalogueFilePathTbx";
            this.catalogueFilePathTbx.Size = new System.Drawing.Size(610, 23);
            this.catalogueFilePathTbx.TabIndex = 2;
            // 
            // catalogueFileBrowseBtn
            // 
            this.catalogueFileBrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.catalogueFileBrowseBtn.Location = new System.Drawing.Point(720, 32);
            this.catalogueFileBrowseBtn.Name = "catalogueFileBrowseBtn";
            this.catalogueFileBrowseBtn.Size = new System.Drawing.Size(81, 23);
            this.catalogueFileBrowseBtn.TabIndex = 3;
            this.catalogueFileBrowseBtn.Text = "Browse";
            this.catalogueFileBrowseBtn.UseVisualStyleBackColor = true;
            this.catalogueFileBrowseBtn.Click += new System.EventHandler(this.CatalogueFileBrowseBtn_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 597);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Output folder";
            // 
            // outputDirPathTbx
            // 
            this.outputDirPathTbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputDirPathTbx.Location = new System.Drawing.Point(98, 593);
            this.outputDirPathTbx.Name = "outputDirPathTbx";
            this.outputDirPathTbx.Size = new System.Drawing.Size(610, 23);
            this.outputDirPathTbx.TabIndex = 2;
            // 
            // outputDirBrowseBtn
            // 
            this.outputDirBrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.outputDirBrowseBtn.Location = new System.Drawing.Point(720, 593);
            this.outputDirBrowseBtn.Name = "outputDirBrowseBtn";
            this.outputDirBrowseBtn.Size = new System.Drawing.Size(81, 23);
            this.outputDirBrowseBtn.TabIndex = 3;
            this.outputDirBrowseBtn.Text = "Browse";
            this.outputDirBrowseBtn.UseVisualStyleBackColor = true;
            this.outputDirBrowseBtn.Click += new System.EventHandler(this.OutputDirBrowseBtn_Click);
            // 
            // startBtn
            // 
            this.startBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.startBtn.Location = new System.Drawing.Point(400, 644);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 27);
            this.startBtn.TabIndex = 3;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // foldersTvw
            // 
            this.foldersTvw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.foldersTvw.CheckBoxes = true;
            this.foldersTvw.Location = new System.Drawing.Point(98, 99);
            this.foldersTvw.Name = "foldersTvw";
            this.foldersTvw.Size = new System.Drawing.Size(703, 476);
            this.foldersTvw.TabIndex = 4;
            this.foldersTvw.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FoldersTvw_MouseClick);
            // 
            // selectAllCbx
            // 
            this.selectAllCbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectAllCbx.AutoSize = true;
            this.selectAllCbx.Location = new System.Drawing.Point(98, 74);
            this.selectAllCbx.Name = "selectAllCbx";
            this.selectAllCbx.Size = new System.Drawing.Size(72, 19);
            this.selectAllCbx.TabIndex = 5;
            this.selectAllCbx.Text = "Select all";
            this.selectAllCbx.UseVisualStyleBackColor = true;
            this.selectAllCbx.CheckedChanged += new System.EventHandler(this.SelectAllCbx_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Folders";
            // 
            // includeImageExtInOutputFilenameCbx
            // 
            this.includeImageExtInOutputFilenameCbx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.includeImageExtInOutputFilenameCbx.AutoSize = true;
            this.includeImageExtInOutputFilenameCbx.Checked = true;
            this.includeImageExtInOutputFilenameCbx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includeImageExtInOutputFilenameCbx.Location = new System.Drawing.Point(98, 633);
            this.includeImageExtInOutputFilenameCbx.Name = "includeImageExtInOutputFilenameCbx";
            this.includeImageExtInOutputFilenameCbx.Size = new System.Drawing.Size(239, 19);
            this.includeImageExtInOutputFilenameCbx.TabIndex = 5;
            this.includeImageExtInOutputFilenameCbx.Text = "Include image file extension in file name";
            this.includeImageExtInOutputFilenameCbx.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 680);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(809, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 702);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.includeImageExtInOutputFilenameCbx);
            this.Controls.Add(this.selectAllCbx);
            this.Controls.Add(this.foldersTvw);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.outputDirBrowseBtn);
            this.Controls.Add(this.outputDirPathTbx);
            this.Controls.Add(this.catalogueFileBrowseBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.catalogueFilePathTbx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "XMP LibeRator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip mainMenu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitMui;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutMui;
        private Label label1;
        private TextBox catalogueFilePathTbx;
        private Button catalogueFileBrowseBtn;
        private Label label2;
        private TextBox outputDirPathTbx;
        private Button outputDirBrowseBtn;
        private Button startBtn;
        private TreeView foldersTvw;
        private CheckBox selectAllCbx;
        private Label label3;
        private CheckBox includeImageExtInOutputFilenameCbx;
        private ToolStripMenuItem infoMui;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel;
        private ToolStripProgressBar toolStripProgressBar;
    }
}