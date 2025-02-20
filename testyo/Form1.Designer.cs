using System;
using System.IO;
namespace PSONotify
{
    partial class PreferencesWindow
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
			this.debug.Dispose();
			this.m_IconStorageSpace.Dispose();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesWindow));
			this.button1 = new System.Windows.Forms.Button();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.SettingsTab = new System.Windows.Forms.TabPage();
			this.button2 = new System.Windows.Forms.Button();
			this.label15 = new System.Windows.Forms.Label();
			this.disableWordCensorCheckbox = new System.Windows.Forms.CheckBox();
			this.CheckForUpdatesCheckbox = new System.Windows.Forms.CheckBox();
			this.openAtLoginCheckbox = new System.Windows.Forms.CheckBox();
			this.EmergencyQuestsTab = new System.Windows.Forms.TabPage();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.ShipSelectionCombobox = new System.Windows.Forms.ComboBox();
			this.QuestNotificationRepeatComboxbox = new System.Windows.Forms.ComboBox();
			this.audioNotifyTestButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.audioNotifyStyleCombobox = new System.Windows.Forms.ComboBox();
			this.noNotificationsWhilePSOIsRunningCheckbox = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.QuestNotificationStyleCombobox = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.MagTimerTab = new System.Windows.Forms.TabPage();
			this.tabControl3 = new System.Windows.Forms.TabControl();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.MagtimerAdvanceNotificationNumeric = new System.Windows.Forms.NumericUpDown();
			this.resetModeManualOption = new System.Windows.Forms.RadioButton();
			this.MagTimerStopButton = new System.Windows.Forms.Button();
			this.resetModeAutoOption = new System.Windows.Forms.RadioButton();
			this.MagTimerResetButton = new System.Windows.Forms.Button();
			this.MagTimerStartButton = new System.Windows.Forms.Button();
			this.label16 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.MagFeedingTimeoutNumeric = new System.Windows.Forms.NumericUpDown();
			this.MagTimerTimeRemainingLabel = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.MagTimerAdvanceNotifyEnabledCheckbox = new System.Windows.Forms.CheckBox();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.ChatCommandsTab = new System.Windows.Forms.TabPage();
			this.ChatCommandsEnableOnLaunchCheckbox = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.ChatCommandsUserIdTextbox = new System.Windows.Forms.TextBox();
			this.ChatCommandsActivateButton = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.CreditTab = new System.Windows.Forms.TabPage();
			this.linkLabel4 = new System.Windows.Forms.LinkLabel();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label8 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.DevDisableLoggingCheckbox = new System.Windows.Forms.CheckBox();
			this.UpdateChannelCombobox = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.versionLabel = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button3 = new System.Windows.Forms.Button();
			this.contextMenuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.SettingsTab.SuspendLayout();
			this.EmergencyQuestsTab.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.MagTimerTab.SuspendLayout();
			this.tabControl3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MagtimerAdvanceNotificationNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MagFeedingTimeoutNumeric)).BeginInit();
			this.ChatCommandsTab.SuspendLayout();
			this.CreditTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(426, 175);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(66, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Save";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.buttonDone_Click);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "PSONotify";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.DoubleClick += new System.EventHandler(this.showPreferencesWindow_Click);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem5,
            this.toolStripSeparator2,
            this.toolStripMenuItem4,
            this.toolStripMenuItem2,
            this.toolStripSeparator1,
            this.toolStripMenuItem3});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.ShowImageMargin = false;
			this.contextMenuStrip1.Size = new System.Drawing.Size(147, 126);
			this.contextMenuStrip1.Text = "PSONotify";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
			this.toolStripMenuItem1.Text = "Check for EQ";
			this.toolStripMenuItem1.ToolTipText = "Impatient, are we?";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.checkForEQ_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(146, 22);
			this.toolStripMenuItem5.Text = "Start MagTimer";
			this.toolStripMenuItem5.Click += new System.EventHandler(this.MagtimerStartButton_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(146, 22);
			this.toolStripMenuItem4.Text = "Check for Updates";
			this.toolStripMenuItem4.Click += new System.EventHandler(this.userUpdateCheck_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(146, 22);
			this.toolStripMenuItem2.Text = "Preferences";
			this.toolStripMenuItem2.Click += new System.EventHandler(this.showPreferencesWindow_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(146, 22);
			this.toolStripMenuItem3.Text = "Quit";
			this.toolStripMenuItem3.ToolTipText = "I\'m sorry, dave. I didn\'t mean to upset you :\'(";
			this.toolStripMenuItem3.Click += new System.EventHandler(this.quitApplication_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.SettingsTab);
			this.tabControl1.Controls.Add(this.EmergencyQuestsTab);
			this.tabControl1.Controls.Add(this.MagTimerTab);
			this.tabControl1.Controls.Add(this.ChatCommandsTab);
			this.tabControl1.Controls.Add(this.CreditTab);
			this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
			this.tabControl1.Location = new System.Drawing.Point(145, 5);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.Padding = new System.Drawing.Point(0, 0);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(351, 167);
			this.tabControl1.TabIndex = 19;
			// 
			// SettingsTab
			// 
			this.SettingsTab.Controls.Add(this.button2);
			this.SettingsTab.Controls.Add(this.label15);
			this.SettingsTab.Controls.Add(this.disableWordCensorCheckbox);
			this.SettingsTab.Controls.Add(this.CheckForUpdatesCheckbox);
			this.SettingsTab.Controls.Add(this.openAtLoginCheckbox);
			this.SettingsTab.Location = new System.Drawing.Point(4, 22);
			this.SettingsTab.Margin = new System.Windows.Forms.Padding(0);
			this.SettingsTab.Name = "SettingsTab";
			this.SettingsTab.Size = new System.Drawing.Size(343, 141);
			this.SettingsTab.TabIndex = 0;
			this.SettingsTab.Text = "Settings";
			this.SettingsTab.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(255, 13);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 23;
			this.button2.Text = "check";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click_1);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.label15.Location = new System.Drawing.Point(11, 48);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(319, 13);
			this.label15.TabIndex = 21;
			this.label15.Text = "------------- Features that require PSONotify to run as Admin --------------";
			// 
			// disableWordCensorCheckbox
			// 
			this.disableWordCensorCheckbox.AutoSize = true;
			this.disableWordCensorCheckbox.Enabled = false;
			this.disableWordCensorCheckbox.Location = new System.Drawing.Point(17, 105);
			this.disableWordCensorCheckbox.Name = "disableWordCensorCheckbox";
			this.disableWordCensorCheckbox.Size = new System.Drawing.Size(122, 17);
			this.disableWordCensorCheckbox.TabIndex = 7;
			this.disableWordCensorCheckbox.Text = "Disable word censor";
			this.disableWordCensorCheckbox.UseVisualStyleBackColor = true;
			// 
			// CheckForUpdatesCheckbox
			// 
			this.CheckForUpdatesCheckbox.AutoSize = true;
			this.CheckForUpdatesCheckbox.Location = new System.Drawing.Point(17, 18);
			this.CheckForUpdatesCheckbox.Name = "CheckForUpdatesCheckbox";
			this.CheckForUpdatesCheckbox.Size = new System.Drawing.Size(113, 17);
			this.CheckForUpdatesCheckbox.TabIndex = 18;
			this.CheckForUpdatesCheckbox.Text = "Check for updates";
			this.CheckForUpdatesCheckbox.UseVisualStyleBackColor = true;
			this.CheckForUpdatesCheckbox.CheckedChanged += new System.EventHandler(this.checkForUpdatesCheckbox_Changed);
			// 
			// openAtLoginCheckbox
			// 
			this.openAtLoginCheckbox.AutoSize = true;
			this.openAtLoginCheckbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.openAtLoginCheckbox.CausesValidation = false;
			this.openAtLoginCheckbox.Enabled = false;
			this.openAtLoginCheckbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.openAtLoginCheckbox.Location = new System.Drawing.Point(17, 73);
			this.openAtLoginCheckbox.Name = "openAtLoginCheckbox";
			this.openAtLoginCheckbox.Size = new System.Drawing.Size(117, 17);
			this.openAtLoginCheckbox.TabIndex = 17;
			this.openAtLoginCheckbox.Text = "Start with Windows";
			this.openAtLoginCheckbox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.openAtLoginCheckbox.UseVisualStyleBackColor = true;
			// 
			// EmergencyQuestsTab
			// 
			this.EmergencyQuestsTab.Controls.Add(this.tabControl2);
			this.EmergencyQuestsTab.Location = new System.Drawing.Point(4, 22);
			this.EmergencyQuestsTab.Margin = new System.Windows.Forms.Padding(0);
			this.EmergencyQuestsTab.Name = "EmergencyQuestsTab";
			this.EmergencyQuestsTab.Size = new System.Drawing.Size(343, 141);
			this.EmergencyQuestsTab.TabIndex = 1;
			this.EmergencyQuestsTab.Text = "Notifications";
			this.EmergencyQuestsTab.UseVisualStyleBackColor = true;
			// 
			// tabControl2
			// 
			this.tabControl2.Controls.Add(this.tabPage1);
			this.tabControl2.Controls.Add(this.tabPage2);
			this.tabControl2.Controls.Add(this.tabPage3);
			this.tabControl2.Location = new System.Drawing.Point(-4, 3);
			this.tabControl2.Multiline = true;
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(351, 142);
			this.tabControl2.TabIndex = 23;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.ShipSelectionCombobox);
			this.tabPage1.Controls.Add(this.QuestNotificationRepeatComboxbox);
			this.tabPage1.Controls.Add(this.audioNotifyTestButton);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.audioNotifyStyleCombobox);
			this.tabPage1.Controls.Add(this.noNotificationsWhilePSOIsRunningCheckbox);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Controls.Add(this.QuestNotificationStyleCombobox);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(343, 116);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Quest/Event";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(28, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Ship";
			// 
			// ShipSelectionCombobox
			// 
			this.ShipSelectionCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ShipSelectionCombobox.FormattingEnabled = true;
			this.ShipSelectionCombobox.Location = new System.Drawing.Point(70, 6);
			this.ShipSelectionCombobox.Name = "ShipSelectionCombobox";
			this.ShipSelectionCombobox.Size = new System.Drawing.Size(115, 21);
			this.ShipSelectionCombobox.TabIndex = 6;
			// 
			// QuestNotificationRepeatComboxbox
			// 
			this.QuestNotificationRepeatComboxbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.QuestNotificationRepeatComboxbox.FormattingEnabled = true;
			this.QuestNotificationRepeatComboxbox.Location = new System.Drawing.Point(70, 33);
			this.QuestNotificationRepeatComboxbox.Name = "QuestNotificationRepeatComboxbox";
			this.QuestNotificationRepeatComboxbox.Size = new System.Drawing.Size(115, 21);
			this.QuestNotificationRepeatComboxbox.TabIndex = 5;
			// 
			// audioNotifyTestButton
			// 
			this.audioNotifyTestButton.Location = new System.Drawing.Point(200, 87);
			this.audioNotifyTestButton.Name = "audioNotifyTestButton";
			this.audioNotifyTestButton.Size = new System.Drawing.Size(59, 23);
			this.audioNotifyTestButton.TabIndex = 24;
			this.audioNotifyTestButton.Text = "Test";
			this.audioNotifyTestButton.UseVisualStyleBackColor = true;
			this.audioNotifyTestButton.Click += new System.EventHandler(this.audioNotify);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Repeat";
			// 
			// audioNotifyStyleCombobox
			// 
			this.audioNotifyStyleCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.audioNotifyStyleCombobox.FormattingEnabled = true;
			this.audioNotifyStyleCombobox.Items.AddRange(new object[] {
            "PSO1 Error",
            "PSO1 Notify",
            "PSO1 Yahoo",
            "PSO1 Angry",
            "PSO1 Sad",
            "PSO1 Confused",
            "PSO1 Notify B",
            "PSO1 Confused B",
            "PSO1 Cry",
            "PSO1 Use Item",
            "PSO1 Clear",
            "PSO1 Part Clear",
            "PSO1 Mail",
            "PSO2 AC Charge",
            "PSO2 Confirm",
            "PSO2 Error",
            "PSO2 Select.Change"});
			this.audioNotifyStyleCombobox.Location = new System.Drawing.Point(70, 88);
			this.audioNotifyStyleCombobox.Name = "audioNotifyStyleCombobox";
			this.audioNotifyStyleCombobox.Size = new System.Drawing.Size(115, 21);
			this.audioNotifyStyleCombobox.TabIndex = 23;
			// 
			// noNotificationsWhilePSOIsRunningCheckbox
			// 
			this.noNotificationsWhilePSOIsRunningCheckbox.AutoSize = true;
			this.noNotificationsWhilePSOIsRunningCheckbox.Location = new System.Drawing.Point(200, 10);
			this.noNotificationsWhilePSOIsRunningCheckbox.Name = "noNotificationsWhilePSOIsRunningCheckbox";
			this.noNotificationsWhilePSOIsRunningCheckbox.Size = new System.Drawing.Size(124, 17);
			this.noNotificationsWhilePSOIsRunningCheckbox.TabIndex = 19;
			this.noNotificationsWhilePSOIsRunningCheckbox.Text = "Silence while playing";
			this.noNotificationsWhilePSOIsRunningCheckbox.UseVisualStyleBackColor = true;
			this.noNotificationsWhilePSOIsRunningCheckbox.CheckedChanged += new System.EventHandler(this.noNotificationsWhilePSOIsRunningCheckbox_CheckedChanged_1);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(7, 91);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(27, 13);
			this.label7.TabIndex = 22;
			this.label7.Text = "SFX";
			// 
			// QuestNotificationStyleCombobox
			// 
			this.QuestNotificationStyleCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.QuestNotificationStyleCombobox.FormattingEnabled = true;
			this.QuestNotificationStyleCombobox.Items.AddRange(new object[] {
            "Bubble only",
            "Audio only",
            "All combined",
            "None",
            "Flash Tray Icon"});
			this.QuestNotificationStyleCombobox.Location = new System.Drawing.Point(70, 60);
			this.QuestNotificationStyleCombobox.Name = "QuestNotificationStyleCombobox";
			this.QuestNotificationStyleCombobox.Size = new System.Drawing.Size(115, 21);
			this.QuestNotificationStyleCombobox.TabIndex = 20;
			this.QuestNotificationStyleCombobox.SelectedIndexChanged += new System.EventHandler(this.notificationStyleCombobox_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 63);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(55, 13);
			this.label6.TabIndex = 21;
			this.label6.Text = "Feedback";
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(343, 116);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "MagTimer";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(343, 116);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Support Partner Timer";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// MagTimerTab
			// 
			this.MagTimerTab.BackColor = System.Drawing.SystemColors.Window;
			this.MagTimerTab.Controls.Add(this.tabControl3);
			this.MagTimerTab.Location = new System.Drawing.Point(4, 22);
			this.MagTimerTab.Margin = new System.Windows.Forms.Padding(0);
			this.MagTimerTab.Name = "MagTimerTab";
			this.MagTimerTab.Size = new System.Drawing.Size(343, 141);
			this.MagTimerTab.TabIndex = 4;
			this.MagTimerTab.Text = "Timers";
			// 
			// tabControl3
			// 
			this.tabControl3.Controls.Add(this.tabPage4);
			this.tabControl3.Controls.Add(this.tabPage5);
			this.tabControl3.Location = new System.Drawing.Point(-4, 3);
			this.tabControl3.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.tabControl3.Multiline = true;
			this.tabControl3.Name = "tabControl3";
			this.tabControl3.SelectedIndex = 0;
			this.tabControl3.Size = new System.Drawing.Size(359, 147);
			this.tabControl3.TabIndex = 21;
			this.tabControl3.TabStop = false;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.MagtimerAdvanceNotificationNumeric);
			this.tabPage4.Controls.Add(this.resetModeManualOption);
			this.tabPage4.Controls.Add(this.MagTimerStopButton);
			this.tabPage4.Controls.Add(this.resetModeAutoOption);
			this.tabPage4.Controls.Add(this.MagTimerResetButton);
			this.tabPage4.Controls.Add(this.MagTimerStartButton);
			this.tabPage4.Controls.Add(this.label16);
			this.tabPage4.Controls.Add(this.label14);
			this.tabPage4.Controls.Add(this.MagFeedingTimeoutNumeric);
			this.tabPage4.Controls.Add(this.MagTimerTimeRemainingLabel);
			this.tabPage4.Controls.Add(this.label12);
			this.tabPage4.Controls.Add(this.MagTimerAdvanceNotifyEnabledCheckbox);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(351, 121);
			this.tabPage4.TabIndex = 0;
			this.tabPage4.Text = " Mag";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// MagtimerAdvanceNotificationNumeric
			// 
			this.MagtimerAdvanceNotificationNumeric.Enabled = false;
			this.MagtimerAdvanceNotificationNumeric.Location = new System.Drawing.Point(60, 61);
			this.MagtimerAdvanceNotificationNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.MagtimerAdvanceNotificationNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.MagtimerAdvanceNotificationNumeric.Name = "MagtimerAdvanceNotificationNumeric";
			this.MagtimerAdvanceNotificationNumeric.Size = new System.Drawing.Size(47, 20);
			this.MagtimerAdvanceNotificationNumeric.TabIndex = 29;
			this.MagtimerAdvanceNotificationNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.MagtimerAdvanceNotificationNumeric.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.MagtimerAdvanceNotificationNumeric.ValueChanged += new System.EventHandler(this.MagtimerAdvanceNotificationNumeric_ValueChanged);
			// 
			// resetModeManualOption
			// 
			this.resetModeManualOption.AutoSize = true;
			this.resetModeManualOption.Checked = true;
			this.resetModeManualOption.Enabled = false;
			this.resetModeManualOption.Location = new System.Drawing.Point(111, 91);
			this.resetModeManualOption.Name = "resetModeManualOption";
			this.resetModeManualOption.Size = new System.Drawing.Size(98, 17);
			this.resetModeManualOption.TabIndex = 27;
			this.resetModeManualOption.TabStop = true;
			this.resetModeManualOption.Text = "Manually Reset";
			this.resetModeManualOption.UseVisualStyleBackColor = true;
			// 
			// MagTimerStopButton
			// 
			this.MagTimerStopButton.Enabled = false;
			this.MagTimerStopButton.Image = ((System.Drawing.Image)(resources.GetObject("MagTimerStopButton.Image")));
			this.MagTimerStopButton.Location = new System.Drawing.Point(304, 6);
			this.MagTimerStopButton.Name = "MagTimerStopButton";
			this.MagTimerStopButton.Size = new System.Drawing.Size(28, 23);
			this.MagTimerStopButton.TabIndex = 28;
			this.MagTimerStopButton.UseVisualStyleBackColor = true;
			this.MagTimerStopButton.Click += new System.EventHandler(this.MagTimerStopButton_Click);
			// 
			// resetModeAutoOption
			// 
			this.resetModeAutoOption.AutoSize = true;
			this.resetModeAutoOption.Enabled = false;
			this.resetModeAutoOption.Location = new System.Drawing.Point(9, 91);
			this.resetModeAutoOption.Name = "resetModeAutoOption";
			this.resetModeAutoOption.Size = new System.Drawing.Size(78, 17);
			this.resetModeAutoOption.TabIndex = 26;
			this.resetModeAutoOption.Text = "Auto-Reset";
			this.resetModeAutoOption.UseVisualStyleBackColor = true;
			// 
			// MagTimerResetButton
			// 
			this.MagTimerResetButton.Image = ((System.Drawing.Image)(resources.GetObject("MagTimerResetButton.Image")));
			this.MagTimerResetButton.Location = new System.Drawing.Point(264, 6);
			this.MagTimerResetButton.Name = "MagTimerResetButton";
			this.MagTimerResetButton.Size = new System.Drawing.Size(34, 23);
			this.MagTimerResetButton.TabIndex = 25;
			this.MagTimerResetButton.UseVisualStyleBackColor = true;
			this.MagTimerResetButton.Click += new System.EventHandler(this.MagTimerResetButton_Click);
			// 
			// MagTimerStartButton
			// 
			this.MagTimerStartButton.Image = ((System.Drawing.Image)(resources.GetObject("MagTimerStartButton.Image")));
			this.MagTimerStartButton.Location = new System.Drawing.Point(231, 6);
			this.MagTimerStartButton.Name = "MagTimerStartButton";
			this.MagTimerStartButton.Size = new System.Drawing.Size(27, 23);
			this.MagTimerStartButton.TabIndex = 24;
			this.MagTimerStartButton.UseVisualStyleBackColor = true;
			this.MagTimerStartButton.Click += new System.EventHandler(this.MagtimerStartButton_Click);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(173, 37);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(43, 13);
			this.label16.TabIndex = 23;
			this.label16.Text = "minutes";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(6, 37);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(112, 13);
			this.label14.TabIndex = 22;
			this.label14.Text = "Time between feeding";
			// 
			// MagFeedingTimeoutNumeric
			// 
			this.MagFeedingTimeoutNumeric.Location = new System.Drawing.Point(121, 35);
			this.MagFeedingTimeoutNumeric.Maximum = new decimal(new int[] {
            45,
            0,
            0,
            0});
			this.MagFeedingTimeoutNumeric.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            0});
			this.MagFeedingTimeoutNumeric.Name = "MagFeedingTimeoutNumeric";
			this.MagFeedingTimeoutNumeric.Size = new System.Drawing.Size(47, 20);
			this.MagFeedingTimeoutNumeric.TabIndex = 20;
			this.MagFeedingTimeoutNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.MagFeedingTimeoutNumeric.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
			// 
			// MagTimerTimeRemainingLabel
			// 
			this.MagTimerTimeRemainingLabel.AutoSize = true;
			this.MagTimerTimeRemainingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MagTimerTimeRemainingLabel.Location = new System.Drawing.Point(118, 11);
			this.MagTimerTimeRemainingLabel.Name = "MagTimerTimeRemainingLabel";
			this.MagTimerTimeRemainingLabel.Size = new System.Drawing.Size(68, 13);
			this.MagTimerTimeRemainingLabel.TabIndex = 19;
			this.MagTimerTimeRemainingLabel.Text = "40 minutes";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(6, 11);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(116, 13);
			this.label12.TabIndex = 18;
			this.label12.Text = "Time until next feeding:";
			// 
			// MagTimerAdvanceNotifyEnabledCheckbox
			// 
			this.MagTimerAdvanceNotifyEnabledCheckbox.AutoSize = true;
			this.MagTimerAdvanceNotifyEnabledCheckbox.Location = new System.Drawing.Point(9, 63);
			this.MagTimerAdvanceNotifyEnabledCheckbox.Name = "MagTimerAdvanceNotifyEnabledCheckbox";
			this.MagTimerAdvanceNotifyEnabledCheckbox.Size = new System.Drawing.Size(276, 17);
			this.MagTimerAdvanceNotifyEnabledCheckbox.TabIndex = 30;
			this.MagTimerAdvanceNotifyEnabledCheckbox.Text = "Warn                    minutes in advance of feeding time";
			this.MagTimerAdvanceNotifyEnabledCheckbox.UseVisualStyleBackColor = true;
			this.MagTimerAdvanceNotifyEnabledCheckbox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged_1);
			// 
			// tabPage5
			// 
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(351, 121);
			this.tabPage5.TabIndex = 1;
			this.tabPage5.Text = "Support Partner";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// ChatCommandsTab
			// 
			this.ChatCommandsTab.Controls.Add(this.ChatCommandsEnableOnLaunchCheckbox);
			this.ChatCommandsTab.Controls.Add(this.label11);
			this.ChatCommandsTab.Controls.Add(this.ChatCommandsUserIdTextbox);
			this.ChatCommandsTab.Controls.Add(this.ChatCommandsActivateButton);
			this.ChatCommandsTab.Controls.Add(this.textBox1);
			this.ChatCommandsTab.Location = new System.Drawing.Point(4, 22);
			this.ChatCommandsTab.Name = "ChatCommandsTab";
			this.ChatCommandsTab.Padding = new System.Windows.Forms.Padding(3);
			this.ChatCommandsTab.Size = new System.Drawing.Size(343, 141);
			this.ChatCommandsTab.TabIndex = 5;
			this.ChatCommandsTab.Text = "Chat Commands";
			this.ChatCommandsTab.UseVisualStyleBackColor = true;
			// 
			// ChatCommandsEnableOnLaunchCheckbox
			// 
			this.ChatCommandsEnableOnLaunchCheckbox.AutoSize = true;
			this.ChatCommandsEnableOnLaunchCheckbox.Location = new System.Drawing.Point(9, 38);
			this.ChatCommandsEnableOnLaunchCheckbox.Name = "ChatCommandsEnableOnLaunchCheckbox";
			this.ChatCommandsEnableOnLaunchCheckbox.Size = new System.Drawing.Size(203, 17);
			this.ChatCommandsEnableOnLaunchCheckbox.TabIndex = 4;
			this.ChatCommandsEnableOnLaunchCheckbox.Text = "Enable Chat Commands automatically";
			this.ChatCommandsEnableOnLaunchCheckbox.UseVisualStyleBackColor = true;
			this.ChatCommandsEnableOnLaunchCheckbox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged_2);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 11);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(84, 13);
			this.label11.TabIndex = 3;
			this.label11.Text = "PSO2 Player ID:";
			// 
			// ChatCommandsUserIdTextbox
			// 
			this.ChatCommandsUserIdTextbox.Location = new System.Drawing.Point(96, 8);
			this.ChatCommandsUserIdTextbox.Name = "ChatCommandsUserIdTextbox";
			this.ChatCommandsUserIdTextbox.Size = new System.Drawing.Size(100, 20);
			this.ChatCommandsUserIdTextbox.TabIndex = 2;
			this.ChatCommandsUserIdTextbox.Text = "0";
			this.ChatCommandsUserIdTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// ChatCommandsActivateButton
			// 
			this.ChatCommandsActivateButton.Location = new System.Drawing.Point(262, 8);
			this.ChatCommandsActivateButton.Name = "ChatCommandsActivateButton";
			this.ChatCommandsActivateButton.Size = new System.Drawing.Size(75, 22);
			this.ChatCommandsActivateButton.TabIndex = 1;
			this.ChatCommandsActivateButton.Text = "Activate";
			this.ChatCommandsActivateButton.UseVisualStyleBackColor = true;
			this.ChatCommandsActivateButton.Click += new System.EventHandler(this.button3_Click_1);
			// 
			// textBox1
			// 
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.ForeColor = System.Drawing.Color.Tomato;
			this.textBox1.Location = new System.Drawing.Point(-4, 64);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(347, 81);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			// 
			// CreditTab
			// 
			this.CreditTab.Controls.Add(this.linkLabel4);
			this.CreditTab.Controls.Add(this.linkLabel3);
			this.CreditTab.Controls.Add(this.linkLabel2);
			this.CreditTab.Controls.Add(this.linkLabel1);
			this.CreditTab.Controls.Add(this.label8);
			this.CreditTab.Controls.Add(this.label5);
			this.CreditTab.Controls.Add(this.label3);
			this.CreditTab.Controls.Add(this.DevDisableLoggingCheckbox);
			this.CreditTab.Controls.Add(this.UpdateChannelCombobox);
			this.CreditTab.Controls.Add(this.label9);
			this.CreditTab.Location = new System.Drawing.Point(4, 22);
			this.CreditTab.Margin = new System.Windows.Forms.Padding(0);
			this.CreditTab.Name = "CreditTab";
			this.CreditTab.Size = new System.Drawing.Size(343, 141);
			this.CreditTab.TabIndex = 3;
			this.CreditTab.Text = "Credit";
			this.CreditTab.UseVisualStyleBackColor = true;
			// 
			// linkLabel4
			// 
			this.linkLabel4.ActiveLinkColor = System.Drawing.Color.Tomato;
			this.linkLabel4.AutoSize = true;
			this.linkLabel4.Cursor = System.Windows.Forms.Cursors.Hand;
			this.linkLabel4.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel4.LinkColor = System.Drawing.Color.Tomato;
			this.linkLabel4.Location = new System.Drawing.Point(194, 74);
			this.linkLabel4.Name = "linkLabel4";
			this.linkLabel4.Size = new System.Drawing.Size(127, 13);
			this.linkLabel4.TabIndex = 26;
			this.linkLabel4.TabStop = true;
			this.linkLabel4.Text = "phantasystarhispano.com";
			this.linkLabel4.Visible = false;
			this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
			// 
			// linkLabel3
			// 
			this.linkLabel3.ActiveLinkColor = System.Drawing.Color.Tomato;
			this.linkLabel3.AutoSize = true;
			this.linkLabel3.Cursor = System.Windows.Forms.Cursors.Hand;
			this.linkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel3.LinkColor = System.Drawing.Color.Tomato;
			this.linkLabel3.Location = new System.Drawing.Point(111, 61);
			this.linkLabel3.Margin = new System.Windows.Forms.Padding(0);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(71, 13);
			this.linkLabel3.TabIndex = 25;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "Team Mythos";
			this.linkLabel3.Visible = false;
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
			// 
			// linkLabel2
			// 
			this.linkLabel2.ActiveLinkColor = System.Drawing.Color.Tomato;
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel2.LinkColor = System.Drawing.Color.Tomato;
			this.linkLabel2.Location = new System.Drawing.Point(94, 35);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(88, 13);
			this.linkLabel2.TabIndex = 24;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "@pso2_emg_bot";
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel1.LinkColor = System.Drawing.Color.Tomato;
			this.linkLabel1.Location = new System.Drawing.Point(94, 13);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(57, 13);
			this.linkLabel1.TabIndex = 22;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "@Leroy_K";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(10, 35);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(70, 13);
			this.label8.TabIndex = 23;
			this.label8.Text = "Data Source:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(10, 13);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(62, 13);
			this.label5.TabIndex = 22;
			this.label5.Text = "Developer: ";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(10, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(319, 46);
			this.label3.TabIndex = 7;
			this.label3.Text = "App idea: Ookami, of                       (Ship 2)\r\nMag/Partner timer idea: Dr. " +
    "Pariolo, of\r\nSFX: Sonic Team/SEGA 1999, 2012 (sshh don\'t tell them)\r\n";
			this.label3.Visible = false;
			// 
			// DevDisableLoggingCheckbox
			// 
			this.DevDisableLoggingCheckbox.AutoSize = true;
			this.DevDisableLoggingCheckbox.Location = new System.Drawing.Point(232, 113);
			this.DevDisableLoggingCheckbox.Name = "DevDisableLoggingCheckbox";
			this.DevDisableLoggingCheckbox.Size = new System.Drawing.Size(102, 17);
			this.DevDisableLoggingCheckbox.TabIndex = 6;
			this.DevDisableLoggingCheckbox.Text = "Disable Logging";
			this.DevDisableLoggingCheckbox.UseVisualStyleBackColor = true;
			this.DevDisableLoggingCheckbox.Visible = false;
			this.DevDisableLoggingCheckbox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// UpdateChannelCombobox
			// 
			this.UpdateChannelCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.UpdateChannelCombobox.FormattingEnabled = true;
			this.UpdateChannelCombobox.Items.AddRange(new object[] {
            "Release",
            "Development"});
			this.UpdateChannelCombobox.Location = new System.Drawing.Point(100, 110);
			this.UpdateChannelCombobox.Name = "UpdateChannelCombobox";
			this.UpdateChannelCombobox.Size = new System.Drawing.Size(121, 21);
			this.UpdateChannelCombobox.TabIndex = 4;
			this.UpdateChannelCombobox.SelectedIndexChanged += new System.EventHandler(this.UpdateChannelCombobox_SelectedIndexChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(10, 114);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(84, 13);
			this.label9.TabIndex = 3;
			this.label9.Text = "Update Channel";
			// 
			// label10
			// 
			this.label10.BackColor = System.Drawing.Color.Tomato;
			this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.ForeColor = System.Drawing.Color.Snow;
			this.label10.Location = new System.Drawing.Point(0, 124);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(136, 70);
			this.label10.TabIndex = 27;
			this.label10.Text = "DEVELOPMENT VERSION\r\nSome UI is not functional, and bugs are bound to pop up";
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(NotifyCore.Instance.pollTwitterThreaded);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workCompleted);
			// 
			// versionLabel
			// 
			this.versionLabel.AutoSize = true;
			this.versionLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.versionLabel.Location = new System.Drawing.Point(146, 180);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(41, 13);
			this.versionLabel.TabIndex = 20;
			this.versionLabel.Text = "version";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::PSONotify.Properties.Resources.coolpix;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(136, 203);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 15;
			this.pictureBox1.TabStop = false;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(313, 176);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 28;
			this.button3.Text = "button3";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click_2);
			// 
			// PreferencesWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(499, 203);
			this.ControlBox = false;
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.versionLabel);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "PreferencesWindow";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Preferences";
			this.contextMenuStrip1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.SettingsTab.ResumeLayout(false);
			this.SettingsTab.PerformLayout();
			this.EmergencyQuestsTab.ResumeLayout(false);
			this.tabControl2.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.MagTimerTab.ResumeLayout(false);
			this.tabControl3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MagtimerAdvanceNotificationNumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MagFeedingTimeoutNumeric)).EndInit();
			this.ChatCommandsTab.ResumeLayout(false);
			this.ChatCommandsTab.PerformLayout();
			this.CreditTab.ResumeLayout(false);
			this.CreditTab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage EmergencyQuestsTab;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox QuestNotificationRepeatComboxbox;
		private System.Windows.Forms.CheckBox noNotificationsWhilePSOIsRunningCheckbox;
		private System.Windows.Forms.ComboBox QuestNotificationStyleCombobox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox audioNotifyStyleCombobox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button audioNotifyTestButton;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox UpdateChannelCombobox;
		private System.Windows.Forms.Label versionLabel;
		private System.Windows.Forms.TabPage CreditTab;
		private System.Windows.Forms.CheckBox DevDisableLoggingCheckbox;
		private System.Windows.Forms.TabPage SettingsTab;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.CheckBox disableWordCensorCheckbox;
		private System.Windows.Forms.CheckBox CheckForUpdatesCheckbox;
		private System.Windows.Forms.CheckBox openAtLoginCheckbox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox ShipSelectionCombobox;
		private System.Windows.Forms.TabPage MagTimerTab;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel linkLabel4;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TabPage ChatCommandsTab;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.Button ChatCommandsActivateButton;
		private System.Windows.Forms.TextBox ChatCommandsUserIdTextbox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.CheckBox ChatCommandsEnableOnLaunchCheckbox;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabControl tabControl3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.NumericUpDown MagtimerAdvanceNotificationNumeric;
		private System.Windows.Forms.Button MagTimerStopButton;
		private System.Windows.Forms.Button MagTimerResetButton;
		private System.Windows.Forms.Button MagTimerStartButton;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.NumericUpDown MagFeedingTimeoutNumeric;
		private System.Windows.Forms.Label MagTimerTimeRemainingLabel;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.CheckBox MagTimerAdvanceNotifyEnabledCheckbox;
		private System.Windows.Forms.RadioButton resetModeManualOption;
		private System.Windows.Forms.RadioButton resetModeAutoOption;
		private System.Windows.Forms.Button button3;
    }
}

