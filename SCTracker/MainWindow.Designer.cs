namespace SCTracker
{
    partial class MainWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ScriptInOut = new System.Windows.Forms.TabControl();
            this.tab_sc = new System.Windows.Forms.TabPage();
            this.ParameterTable = new System.Windows.Forms.DataGridView();
            this.ParameterType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScriptText = new System.Windows.Forms.RichTextBox();
            this.tab_tracker = new System.Windows.Forms.TabPage();
            this.TrackerOutputText = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenSCMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckOpcodesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.OpenedFile = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ScriptInOut.SuspendLayout();
            this.tab_sc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ParameterTable)).BeginInit();
            this.tab_tracker.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScriptInOut
            // 
            this.ScriptInOut.Controls.Add(this.tab_sc);
            this.ScriptInOut.Controls.Add(this.tab_tracker);
            this.ScriptInOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScriptInOut.Location = new System.Drawing.Point(0, 24);
            this.ScriptInOut.Name = "ScriptInOut";
            this.ScriptInOut.SelectedIndex = 0;
            this.ScriptInOut.Size = new System.Drawing.Size(800, 426);
            this.ScriptInOut.TabIndex = 0;
            // 
            // tab_sc
            // 
            this.tab_sc.Controls.Add(this.ParameterTable);
            this.tab_sc.Controls.Add(this.ScriptText);
            this.tab_sc.Location = new System.Drawing.Point(4, 22);
            this.tab_sc.Name = "tab_sc";
            this.tab_sc.Padding = new System.Windows.Forms.Padding(3);
            this.tab_sc.Size = new System.Drawing.Size(792, 400);
            this.tab_sc.TabIndex = 0;
            this.tab_sc.Text = "Smart Contract Script";
            this.tab_sc.UseVisualStyleBackColor = true;
            // 
            // ParameterTable
            // 
            this.ParameterTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ParameterTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParameterType,
            this.Value});
            this.ParameterTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ParameterTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ParameterTable.Location = new System.Drawing.Point(3, 210);
            this.ParameterTable.Name = "ParameterTable";
            this.ParameterTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ParameterTable.Size = new System.Drawing.Size(786, 187);
            this.ParameterTable.TabIndex = 1;
            // 
            // ParameterType
            // 
            dataGridViewCellStyle4.NullValue = "byte[]";
            this.ParameterType.DefaultCellStyle = dataGridViewCellStyle4;
            this.ParameterType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.ParameterType.HeaderText = "Type";
            this.ParameterType.Items.AddRange(new object[] {
            "byte[]",
            "BigInteger",
            "bool",
            "string"});
            this.ParameterType.Name = "ParameterType";
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // ScriptText
            // 
            this.ScriptText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScriptText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScriptText.Location = new System.Drawing.Point(3, 6);
            this.ScriptText.Name = "ScriptText";
            this.ScriptText.Size = new System.Drawing.Size(789, 198);
            this.ScriptText.TabIndex = 0;
            this.ScriptText.Text = "";
            this.ScriptText.TextChanged += new System.EventHandler(this.ScriptText_TextChanged);
            // 
            // tab_tracker
            // 
            this.tab_tracker.Controls.Add(this.TrackerOutputText);
            this.tab_tracker.Location = new System.Drawing.Point(4, 22);
            this.tab_tracker.Name = "tab_tracker";
            this.tab_tracker.Padding = new System.Windows.Forms.Padding(3);
            this.tab_tracker.Size = new System.Drawing.Size(792, 400);
            this.tab_tracker.TabIndex = 1;
            this.tab_tracker.Text = "Tracker output";
            this.tab_tracker.UseVisualStyleBackColor = true;
            // 
            // TrackerOutputText
            // 
            this.TrackerOutputText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TrackerOutputText.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TrackerOutputText.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.TrackerOutputText.Location = new System.Drawing.Point(3, 3);
            this.TrackerOutputText.Name = "TrackerOutputText";
            this.TrackerOutputText.ReadOnly = true;
            this.TrackerOutputText.Size = new System.Drawing.Size(793, 369);
            this.TrackerOutputText.TabIndex = 0;
            this.TrackerOutputText.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.ToolsMenuItem,
            this.AboutMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "MainMenu";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenSCMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMenuItem.Text = "File";
            // 
            // OpenSCMenuItem
            // 
            this.OpenSCMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.binaryToolStripMenuItem,
            this.textToolStripMenuItem});
            this.OpenSCMenuItem.Name = "OpenSCMenuItem";
            this.OpenSCMenuItem.Size = new System.Drawing.Size(136, 22);
            this.OpenSCMenuItem.Text = "Open Script";
            // 
            // binaryToolStripMenuItem
            // 
            this.binaryToolStripMenuItem.Name = "binaryToolStripMenuItem";
            this.binaryToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.binaryToolStripMenuItem.Text = "Binary";
            this.binaryToolStripMenuItem.Click += new System.EventHandler(this.binaryToolStripMenuItem_Click);
            // 
            // textToolStripMenuItem
            // 
            this.textToolStripMenuItem.Name = "textToolStripMenuItem";
            this.textToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.textToolStripMenuItem.Text = "Text";
            this.textToolStripMenuItem.Click += new System.EventHandler(this.textToolStripMenuItem_Click);
            // 
            // ToolsMenuItem
            // 
            this.ToolsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CheckOpcodesMenuItem});
            this.ToolsMenuItem.Name = "ToolsMenuItem";
            this.ToolsMenuItem.Size = new System.Drawing.Size(47, 20);
            this.ToolsMenuItem.Text = "Tools";
            // 
            // CheckOpcodesMenuItem
            // 
            this.CheckOpcodesMenuItem.Enabled = false;
            this.CheckOpcodesMenuItem.Name = "CheckOpcodesMenuItem";
            this.CheckOpcodesMenuItem.Size = new System.Drawing.Size(180, 22);
            this.CheckOpcodesMenuItem.Text = "Check Opcodes";
            this.CheckOpcodesMenuItem.Click += new System.EventHandler(this.CheckOpcodesMenuItem_Click);
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpMenuItem});
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.Size = new System.Drawing.Size(52, 20);
            this.AboutMenuItem.Text = "About";
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(99, 22);
            this.HelpMenuItem.Text = "Help";
            this.HelpMenuItem.Click += new System.EventHandler(this.HelpMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenedFile,
            this.ProgressLabel,
            this.ProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 428);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip";
            // 
            // OpenedFile
            // 
            this.OpenedFile.Name = "OpenedFile";
            this.OpenedFile.Size = new System.Drawing.Size(785, 17);
            this.OpenedFile.Spring = true;
            this.OpenedFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(52, 17);
            this.ProgressLabel.Text = "Progress";
            this.ProgressLabel.Visible = false;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 16);
            this.ProgressBar.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.ScriptInOut);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "SCTracker";
            this.ScriptInOut.ResumeLayout(false);
            this.tab_sc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ParameterTable)).EndInit();
            this.tab_tracker.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl ScriptInOut;
        private System.Windows.Forms.TabPage tab_sc;
        private System.Windows.Forms.TabPage tab_tracker;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel ProgressLabel;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.ToolStripMenuItem OpenSCMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.RichTextBox TrackerOutputText;
        private System.Windows.Forms.RichTextBox ScriptText;
        private System.Windows.Forms.ToolStripStatusLabel OpenedFile;
        private System.Windows.Forms.ToolStripMenuItem ToolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CheckOpcodesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textToolStripMenuItem;
        private System.Windows.Forms.DataGridView ParameterTable;
        private System.Windows.Forms.DataGridViewComboBoxColumn ParameterType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}