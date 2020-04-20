namespace PowerSearch
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.folderButton = new System.Windows.Forms.Button();
            this.fileButton = new System.Windows.Forms.Button();
            this.textButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.searchButton = new System.Windows.Forms.Button();
            this.textBoxSourceDirectory = new System.Windows.Forms.TextBox();
            this.filesTextBox = new System.Windows.Forms.TextBox();
            this.textTextBox = new System.Windows.Forms.TextBox();
            this.checkBoxSearchSubolder = new System.Windows.Forms.CheckBox();
            this.checkBoxRegexFile = new System.Windows.Forms.CheckBox();
            this.CheckBoxIgnoreCaseFiles = new System.Windows.Forms.CheckBox();
            this.checkBoxRegexText = new System.Windows.Forms.CheckBox();
            this.checkBoxIgnoreCaseText = new System.Windows.Forms.CheckBox();
            this.checkBoxRegexFolderSyntax = new System.Windows.Forms.CheckBox();
            this.checkBoxAJBDropboxes = new System.Windows.Forms.CheckBox();
            this.Host1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Logs1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView1 = new PowerSearch.CustomListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.CheckBoxIgnoreCaseFolder = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSubfolderPattern = new System.Windows.Forms.TextBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.labelHost1 = new System.Windows.Forms.Label();
            this.labelHost2 = new System.Windows.Forms.Label();
            this.labelHost3 = new System.Windows.Forms.Label();
            this.labelHost4 = new System.Windows.Forms.Label();
            this.labelHost5 = new System.Windows.Forms.Label();
            this.comboBoxHistory = new System.Windows.Forms.ComboBox();
            this.hostsLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderButton
            // 
            this.folderButton.Location = new System.Drawing.Point(12, 61);
            this.folderButton.Name = "folderButton";
            this.folderButton.Size = new System.Drawing.Size(75, 23);
            this.folderButton.TabIndex = 0;
            this.folderButton.Text = "Folde&r";
            this.folderButton.UseVisualStyleBackColor = true;
            this.folderButton.Click += new System.EventHandler(this.GeneralControl_CheckedChanged);
            // 
            // fileButton
            // 
            this.fileButton.Location = new System.Drawing.Point(12, 90);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(75, 23);
            this.fileButton.TabIndex = 1;
            this.fileButton.Text = "Fi&les";
            this.fileButton.UseVisualStyleBackColor = true;
            // 
            // textButton
            // 
            this.textButton.Location = new System.Drawing.Point(12, 119);
            this.textButton.Name = "textButton";
            this.textButton.Size = new System.Drawing.Size(75, 23);
            this.textButton.TabIndex = 2;
            this.textButton.Text = "(a*) &Text";
            this.textButton.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1248, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.SystemColors.Control;
            this.searchButton.Location = new System.Drawing.Point(12, 148);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 4;
            this.searchButton.Text = "&Search";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // textBoxSourceDirectory
            // 
            this.textBoxSourceDirectory.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxSourceDirectory.Font = new System.Drawing.Font("Arial", 8.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSourceDirectory.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBoxSourceDirectory.Location = new System.Drawing.Point(94, 63);
            this.textBoxSourceDirectory.Name = "textBoxSourceDirectory";
            this.textBoxSourceDirectory.Size = new System.Drawing.Size(192, 20);
            this.textBoxSourceDirectory.TabIndex = 7;
            this.toolTip1.SetToolTip(this.textBoxSourceDirectory, "Root Path of Search");
            this.textBoxSourceDirectory.TextChanged += new System.EventHandler(this.GeneralControl_CheckedChanged);
            this.textBoxSourceDirectory.MouseHover += new System.EventHandler(this.textBoxSourceDirectory_MouseHover);
            // 
            // filesTextBox
            // 
            this.filesTextBox.Location = new System.Drawing.Point(94, 92);
            this.filesTextBox.Name = "filesTextBox";
            this.filesTextBox.Size = new System.Drawing.Size(400, 20);
            this.filesTextBox.TabIndex = 8;
            this.filesTextBox.TextChanged += new System.EventHandler(this.GeneralControl_CheckedChanged);
            // 
            // textTextBox
            // 
            this.textTextBox.Location = new System.Drawing.Point(94, 121);
            this.textTextBox.Name = "textTextBox";
            this.textTextBox.Size = new System.Drawing.Size(400, 20);
            this.textTextBox.TabIndex = 9;
            this.textTextBox.TextChanged += new System.EventHandler(this.GeneralControl_CheckedChanged);
            // 
            // checkBoxSearchSubolder
            // 
            this.checkBoxSearchSubolder.AutoSize = true;
            this.checkBoxSearchSubolder.Location = new System.Drawing.Point(592, 67);
            this.checkBoxSearchSubolder.Name = "checkBoxSearchSubolder";
            this.checkBoxSearchSubolder.Size = new System.Drawing.Size(113, 17);
            this.checkBoxSearchSubolder.TabIndex = 11;
            this.checkBoxSearchSubolder.Text = "Search Su&bfolders";
            this.checkBoxSearchSubolder.UseVisualStyleBackColor = true;
            this.checkBoxSearchSubolder.CheckedChanged += new System.EventHandler(this.GeneralControl_CheckedChanged);
            // 
            // checkBoxRegexFile
            // 
            this.checkBoxRegexFile.AutoSize = true;
            this.checkBoxRegexFile.Location = new System.Drawing.Point(501, 96);
            this.checkBoxRegexFile.Name = "checkBoxRegexFile";
            this.checkBoxRegexFile.Size = new System.Drawing.Size(92, 17);
            this.checkBoxRegexFile.TabIndex = 12;
            this.checkBoxRegexFile.Text = "Regex S&yntax";
            this.checkBoxRegexFile.UseVisualStyleBackColor = true;
            this.checkBoxRegexFile.CheckedChanged += new System.EventHandler(this.GeneralControl_CheckedChanged);
            // 
            // CheckBoxIgnoreCaseFiles
            // 
            this.CheckBoxIgnoreCaseFiles.AutoSize = true;
            this.CheckBoxIgnoreCaseFiles.Location = new System.Drawing.Point(592, 96);
            this.CheckBoxIgnoreCaseFiles.Name = "CheckBoxIgnoreCaseFiles";
            this.CheckBoxIgnoreCaseFiles.Size = new System.Drawing.Size(83, 17);
            this.CheckBoxIgnoreCaseFiles.TabIndex = 13;
            this.CheckBoxIgnoreCaseFiles.Text = "Ignore &Case";
            this.CheckBoxIgnoreCaseFiles.UseVisualStyleBackColor = true;
            this.CheckBoxIgnoreCaseFiles.CheckedChanged += new System.EventHandler(this.GeneralControl_CheckedChanged);
            // 
            // checkBoxRegexText
            // 
            this.checkBoxRegexText.AutoSize = true;
            this.checkBoxRegexText.Location = new System.Drawing.Point(501, 124);
            this.checkBoxRegexText.Name = "checkBoxRegexText";
            this.checkBoxRegexText.Size = new System.Drawing.Size(92, 17);
            this.checkBoxRegexText.TabIndex = 14;
            this.checkBoxRegexText.Text = "Rege&x Syntax";
            this.checkBoxRegexText.UseVisualStyleBackColor = true;
            this.checkBoxRegexText.CheckedChanged += new System.EventHandler(this.GeneralControl_CheckedChanged);
            // 
            // checkBoxIgnoreCaseText
            // 
            this.checkBoxIgnoreCaseText.AutoSize = true;
            this.checkBoxIgnoreCaseText.Location = new System.Drawing.Point(592, 124);
            this.checkBoxIgnoreCaseText.Name = "checkBoxIgnoreCaseText";
            this.checkBoxIgnoreCaseText.Size = new System.Drawing.Size(83, 17);
            this.checkBoxIgnoreCaseText.TabIndex = 15;
            this.checkBoxIgnoreCaseText.Text = "&Ignore Case";
            this.checkBoxIgnoreCaseText.UseVisualStyleBackColor = true;
            this.checkBoxIgnoreCaseText.CheckedChanged += new System.EventHandler(this.GeneralControl_CheckedChanged);
            // 
            // checkBoxRegexFolderSyntax
            // 
            this.checkBoxRegexFolderSyntax.AutoSize = true;
            this.checkBoxRegexFolderSyntax.Location = new System.Drawing.Point(711, 67);
            this.checkBoxRegexFolderSyntax.Name = "checkBoxRegexFolderSyntax";
            this.checkBoxRegexFolderSyntax.Size = new System.Drawing.Size(92, 17);
            this.checkBoxRegexFolderSyntax.TabIndex = 16;
            this.checkBoxRegexFolderSyntax.Text = "&Regex Syntax";
            this.checkBoxRegexFolderSyntax.UseVisualStyleBackColor = true;
            this.checkBoxRegexFolderSyntax.CheckedChanged += new System.EventHandler(this.GeneralControl_CheckedChanged);
            // 
            // checkBoxAJBDropboxes
            // 
            this.checkBoxAJBDropboxes.AutoSize = true;
            this.checkBoxAJBDropboxes.Location = new System.Drawing.Point(809, 67);
            this.checkBoxAJBDropboxes.Name = "checkBoxAJBDropboxes";
            this.checkBoxAJBDropboxes.Size = new System.Drawing.Size(96, 17);
            this.checkBoxAJBDropboxes.TabIndex = 17;
            this.checkBoxAJBDropboxes.Text = "AJBDropboxes";
            this.checkBoxAJBDropboxes.UseVisualStyleBackColor = true;
            this.checkBoxAJBDropboxes.CheckedChanged += new System.EventHandler(this.GeneralControl_CheckedChanged);
            // 
            // Host1
            // 
            this.Host1.Text = "Host1";
            this.Host1.Width = 100;
            // 
            // Logs1
            // 
            this.Logs1.Text = "Logs1";
            this.Logs1.Width = 1400;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackColor = System.Drawing.SystemColors.Window;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Host1,
            this.Logs1});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 178);
            this.listView1.Name = "listView1";
            this.listView1.OwnerDraw = true;
            this.listView1.SelectedIndex = 0;
            this.listView1.SelectedIndices = ((System.Collections.Generic.List<int>)(resources.GetObject("listView1.SelectedIndices")));
            this.listView1.Size = new System.Drawing.Size(1224, 205);
            this.listView1.TabIndex = 18;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.VirtualMode = true;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewLogs_RetrieveVirtualItem);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(95, 26);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.progressBar.Location = new System.Drawing.Point(256, 148);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(980, 22);
            this.progressBar.TabIndex = 19;
            this.progressBar.Tag = "";
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1248, 24);
            this.menuStrip2.TabIndex = 20;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // CheckBoxIgnoreCaseFolder
            // 
            this.CheckBoxIgnoreCaseFolder.AutoSize = true;
            this.CheckBoxIgnoreCaseFolder.Location = new System.Drawing.Point(911, 66);
            this.CheckBoxIgnoreCaseFolder.Name = "CheckBoxIgnoreCaseFolder";
            this.CheckBoxIgnoreCaseFolder.Size = new System.Drawing.Size(83, 17);
            this.CheckBoxIgnoreCaseFolder.TabIndex = 21;
            this.CheckBoxIgnoreCaseFolder.Text = "Ignore Case";
            this.CheckBoxIgnoreCaseFolder.UseVisualStyleBackColor = false;
            this.CheckBoxIgnoreCaseFolder.CheckedChanged += new System.EventHandler(this.GeneralControl_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.label1.Location = new System.Drawing.Point(313, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Folder Regex";
            // 
            // textBoxSubfolderPattern
            // 
            this.textBoxSubfolderPattern.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBoxSubfolderPattern.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.textBoxSubfolderPattern.Location = new System.Drawing.Point(389, 64);
            this.textBoxSubfolderPattern.Name = "textBoxSubfolderPattern";
            this.textBoxSubfolderPattern.Size = new System.Drawing.Size(194, 20);
            this.textBoxSubfolderPattern.TabIndex = 23;
            this.textBoxSubfolderPattern.TextChanged += new System.EventHandler(this.GeneralControl_CheckedChanged);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(681, 124);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(903, 13);
            this.statusLabel.TabIndex = 24;
            this.statusLabel.Text = resources.GetString("statusLabel.Text");
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(94, 147);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 25;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(175, 148);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 26;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            // 
            // labelHost1
            // 
            this.labelHost1.AutoSize = true;
            this.labelHost1.Location = new System.Drawing.Point(682, 99);
            this.labelHost1.Name = "labelHost1";
            this.labelHost1.Size = new System.Drawing.Size(38, 13);
            this.labelHost1.TabIndex = 27;
            this.labelHost1.Text = "Host 1";
            // 
            // labelHost2
            // 
            this.labelHost2.AutoSize = true;
            this.labelHost2.Location = new System.Drawing.Point(798, 99);
            this.labelHost2.Name = "labelHost2";
            this.labelHost2.Size = new System.Drawing.Size(38, 13);
            this.labelHost2.TabIndex = 28;
            this.labelHost2.Text = "Host 2";
            // 
            // labelHost3
            // 
            this.labelHost3.AutoSize = true;
            this.labelHost3.Location = new System.Drawing.Point(914, 99);
            this.labelHost3.Name = "labelHost3";
            this.labelHost3.Size = new System.Drawing.Size(38, 13);
            this.labelHost3.TabIndex = 29;
            this.labelHost3.Text = "Host 3";
            // 
            // labelHost4
            // 
            this.labelHost4.AutoSize = true;
            this.labelHost4.Location = new System.Drawing.Point(1033, 99);
            this.labelHost4.Name = "labelHost4";
            this.labelHost4.Size = new System.Drawing.Size(38, 13);
            this.labelHost4.TabIndex = 30;
            this.labelHost4.Text = "Host 4";
            // 
            // labelHost5
            // 
            this.labelHost5.AutoSize = true;
            this.labelHost5.Location = new System.Drawing.Point(1152, 99);
            this.labelHost5.Name = "labelHost5";
            this.labelHost5.Size = new System.Drawing.Size(38, 13);
            this.labelHost5.TabIndex = 31;
            this.labelHost5.Text = "Host 5";
            // 
            // comboBoxHistory
            // 
            this.comboBoxHistory.FormattingEnabled = true;
            this.comboBoxHistory.Location = new System.Drawing.Point(94, 26);
            this.comboBoxHistory.Name = "comboBoxHistory";
            this.comboBoxHistory.Size = new System.Drawing.Size(121, 21);
            this.comboBoxHistory.TabIndex = 32;
            // 
            // hostsLabel
            // 
            this.hostsLabel.AutoSize = true;
            this.hostsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hostsLabel.Location = new System.Drawing.Point(253, 26);
            this.hostsLabel.Name = "hostsLabel";
            this.hostsLabel.Size = new System.Drawing.Size(0, 24);
            this.hostsLabel.TabIndex = 33;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 395);
            this.Controls.Add(this.hostsLabel);
            this.Controls.Add(this.comboBoxHistory);
            this.Controls.Add(this.labelHost5);
            this.Controls.Add(this.labelHost4);
            this.Controls.Add(this.labelHost3);
            this.Controls.Add(this.labelHost2);
            this.Controls.Add(this.labelHost1);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.textBoxSubfolderPattern);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CheckBoxIgnoreCaseFolder);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.checkBoxAJBDropboxes);
            this.Controls.Add(this.checkBoxRegexFolderSyntax);
            this.Controls.Add(this.checkBoxIgnoreCaseText);
            this.Controls.Add(this.checkBoxRegexText);
            this.Controls.Add(this.CheckBoxIgnoreCaseFiles);
            this.Controls.Add(this.checkBoxRegexFile);
            this.Controls.Add(this.checkBoxSearchSubolder);
            this.Controls.Add(this.textTextBox);
            this.Controls.Add(this.filesTextBox);
            this.Controls.Add(this.textBoxSourceDirectory);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.textButton);
            this.Controls.Add(this.fileButton);
            this.Controls.Add(this.folderButton);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "PowerSearch";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button folderButton;
        private System.Windows.Forms.Button fileButton;
        private System.Windows.Forms.Button textButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox textBoxSourceDirectory;
        private System.Windows.Forms.TextBox filesTextBox;
        private System.Windows.Forms.TextBox textTextBox;
        private System.Windows.Forms.CheckBox checkBoxSearchSubolder;
        private System.Windows.Forms.CheckBox checkBoxRegexFile;
        private System.Windows.Forms.CheckBox CheckBoxIgnoreCaseFiles;
        private System.Windows.Forms.CheckBox checkBoxRegexText;
        private System.Windows.Forms.CheckBox checkBoxIgnoreCaseText;
        private System.Windows.Forms.CheckBox checkBoxRegexFolderSyntax;
        private System.Windows.Forms.CheckBox checkBoxAJBDropboxes;
        private System.Windows.Forms.ColumnHeader Host1;
        private System.Windows.Forms.ColumnHeader Logs1;
        private CustomListView listView1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.CheckBox CheckBoxIgnoreCaseFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSubfolderPattern;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Label labelHost1;
        private System.Windows.Forms.Label labelHost2;
        private System.Windows.Forms.Label labelHost3;
        private System.Windows.Forms.Label labelHost4;
        private System.Windows.Forms.Label labelHost5;
        private System.Windows.Forms.ComboBox comboBoxHistory;
        private System.Windows.Forms.Label hostsLabel;
    }
}

