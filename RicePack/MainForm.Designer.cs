namespace RicePack
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openButton = new System.Windows.Forms.ToolStripMenuItem();
            this.openAGTButton = new System.Windows.Forms.ToolStripMenuItem();
            this.openLOFButton = new System.Windows.Forms.ToolStripMenuItem();
            this.openNTXButton = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveButton = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveAsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTdfButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.findButton = new System.Windows.Forms.ToolStripMenuItem();
            this.findBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.columnReplaceButton = new System.Windows.Forms.ToolStripMenuItem();
            this.columnReplaceValueBox = new System.Windows.Forms.ToolStripTextBox();
            this.aboutButton = new System.Windows.Forms.ToolStripMenuItem();
            this.fileTreeView = new System.Windows.Forms.TreeView();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.viewTab = new System.Windows.Forms.TabPage();
            this.tdfGridView = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.viewTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tdfGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.archiveToolStripMenuItem,
            this.tdfToolStripMenuItem,
            this.aboutButton});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(959, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openButton,
            this.fileSaveButton,
            this.fileSaveAsButton});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openButton
            // 
            this.openButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openAGTButton,
            this.openLOFButton,
            this.openNTXButton});
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(216, 26);
            this.openButton.Text = "Open";
            // 
            // openAGTButton
            // 
            this.openAGTButton.Name = "openAGTButton";
            this.openAGTButton.Size = new System.Drawing.Size(216, 26);
            this.openAGTButton.Text = "AGT";
            this.openAGTButton.Click += new System.EventHandler(this.openAGTButton_Click);
            // 
            // openLOFButton
            // 
            this.openLOFButton.Name = "openLOFButton";
            this.openLOFButton.Size = new System.Drawing.Size(216, 26);
            this.openLOFButton.Text = "LOF";
            this.openLOFButton.Click += new System.EventHandler(this.openLOFButton_Click);
            // 
            // openNTXButton
            // 
            this.openNTXButton.Name = "openNTXButton";
            this.openNTXButton.Size = new System.Drawing.Size(216, 26);
            this.openNTXButton.Text = "NTX";
            this.openNTXButton.Click += new System.EventHandler(this.openNTXButton_Click);
            // 
            // fileSaveButton
            // 
            this.fileSaveButton.Name = "fileSaveButton";
            this.fileSaveButton.Size = new System.Drawing.Size(216, 26);
            this.fileSaveButton.Text = "Save";
            this.fileSaveButton.Click += new System.EventHandler(this.fileSaveButton_Click);
            // 
            // fileSaveAsButton
            // 
            this.fileSaveAsButton.Name = "fileSaveAsButton";
            this.fileSaveAsButton.Size = new System.Drawing.Size(216, 26);
            this.fileSaveAsButton.Text = "Save As";
            this.fileSaveAsButton.Click += new System.EventHandler(this.fileSaveAsButton_Click);
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAllToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.replaceToolStripMenuItem,
            this.addFileToolStripMenuItem});
            this.archiveToolStripMenuItem.Enabled = false;
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.archiveToolStripMenuItem.Text = "Archive";
            // 
            // exportAllToolStripMenuItem
            // 
            this.exportAllToolStripMenuItem.Name = "exportAllToolStripMenuItem";
            this.exportAllToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.exportAllToolStripMenuItem.Text = "Export All";
            this.exportAllToolStripMenuItem.Click += new System.EventHandler(this.exportAllToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.exportToolStripMenuItem.Text = "Export Selected";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.replaceToolStripMenuItem.Text = "Replace Selected";
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
            // 
            // addFileToolStripMenuItem
            // 
            this.addFileToolStripMenuItem.Name = "addFileToolStripMenuItem";
            this.addFileToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.addFileToolStripMenuItem.Text = "Add File";
            this.addFileToolStripMenuItem.Click += new System.EventHandler(this.addFileToolStripMenuItem_Click);
            // 
            // tdfToolStripMenuItem
            // 
            this.tdfToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTdfButton,
            this.toolStripSeparator1,
            this.findButton,
            this.findBox,
            this.toolStripSeparator2,
            this.columnReplaceButton,
            this.columnReplaceValueBox});
            this.tdfToolStripMenuItem.Enabled = false;
            this.tdfToolStripMenuItem.Name = "tdfToolStripMenuItem";
            this.tdfToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.tdfToolStripMenuItem.Text = "TDF";
            // 
            // saveTdfButton
            // 
            this.saveTdfButton.Name = "saveTdfButton";
            this.saveTdfButton.Size = new System.Drawing.Size(253, 26);
            this.saveTdfButton.Text = "Save";
            this.saveTdfButton.Click += new System.EventHandler(this.saveTdfButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(250, 6);
            // 
            // findButton
            // 
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(253, 26);
            this.findButton.Text = "Find";
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // findBox
            // 
            this.findBox.Name = "findBox";
            this.findBox.Size = new System.Drawing.Size(100, 27);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(250, 6);
            // 
            // columnReplaceButton
            // 
            this.columnReplaceButton.Name = "columnReplaceButton";
            this.columnReplaceButton.Size = new System.Drawing.Size(253, 26);
            this.columnReplaceButton.Text = "Replace Selected Column";
            this.columnReplaceButton.Click += new System.EventHandler(this.columnReplaceButton_Click);
            // 
            // columnReplaceValueBox
            // 
            this.columnReplaceValueBox.Name = "columnReplaceValueBox";
            this.columnReplaceValueBox.Size = new System.Drawing.Size(100, 27);
            // 
            // aboutButton
            // 
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(62, 24);
            this.aboutButton.Text = "About";
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // fileTreeView
            // 
            this.fileTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileTreeView.Location = new System.Drawing.Point(4, 4);
            this.fileTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.fileTreeView.Name = "fileTreeView";
            this.fileTreeView.Size = new System.Drawing.Size(311, 434);
            this.fileTreeView.TabIndex = 1;
            this.fileTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.fileTreeView_AfterSelect);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.viewTab);
            this.tabControl.Location = new System.Drawing.Point(4, 4);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(628, 434);
            this.tabControl.TabIndex = 2;
            // 
            // viewTab
            // 
            this.viewTab.Controls.Add(this.tdfGridView);
            this.viewTab.Location = new System.Drawing.Point(4, 25);
            this.viewTab.Margin = new System.Windows.Forms.Padding(4);
            this.viewTab.Name = "viewTab";
            this.viewTab.Padding = new System.Windows.Forms.Padding(4);
            this.viewTab.Size = new System.Drawing.Size(620, 405);
            this.viewTab.TabIndex = 0;
            this.viewTab.Text = "View";
            this.viewTab.UseVisualStyleBackColor = true;
            // 
            // tdfGridView
            // 
            this.tdfGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tdfGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tdfGridView.Location = new System.Drawing.Point(9, 9);
            this.tdfGridView.Margin = new System.Windows.Forms.Padding(4);
            this.tdfGridView.Name = "tdfGridView";
            this.tdfGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect;
            this.tdfGridView.Size = new System.Drawing.Size(600, 386);
            this.tdfGridView.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fileTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Size = new System.Drawing.Size(959, 442);
            this.splitContainer1.SplitterDistance = 319;
            this.splitContainer1.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 473);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "RicePack";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.viewTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tdfGridView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openButton;
        private System.Windows.Forms.ToolStripMenuItem fileSaveButton;
        private System.Windows.Forms.ToolStripMenuItem fileSaveAsButton;
        private System.Windows.Forms.ToolStripMenuItem aboutButton;
        private System.Windows.Forms.TreeView fileTreeView;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage viewTab;
        private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.DataGridView tdfGridView;
        private System.Windows.Forms.ToolStripMenuItem tdfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTdfButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem findButton;
        private System.Windows.Forms.ToolStripTextBox findBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem columnReplaceButton;
        private System.Windows.Forms.ToolStripTextBox columnReplaceValueBox;
        private System.Windows.Forms.ToolStripMenuItem exportAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAGTButton;
        private System.Windows.Forms.ToolStripMenuItem openLOFButton;
        private System.Windows.Forms.ToolStripMenuItem openNTXButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

