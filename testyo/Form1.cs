//#define DEBUG
//#define DEBUGMODE_UPCOMING

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using Microsoft.Win32;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;
using System.Media;
using System.Security.Principal;
using System.Reflection;


namespace PSONotify {


	public partial class PreferencesWindow: Form {
		public List<Ship> ships = null;
		public List<UpdateFrequency> updateFrequencies = null;
		private const string TWITTER_URL = "https://twitter.com/pso2_emg_bot";
		private System.Timers.Timer m_Timer = null;
		private Dictionary<string, string> areas = null;
		private System.Timers.Timer m_PollTimer = null;
		private System.Timers.Timer m_NotificationTimer = null;
		private bool m_RepeatNotifications = false;
		private Icon m_IconStorageSpace = null;
		private bool m_SilenceNotificationsWhileGameIsRunning = true;
		private Logger debug = null;
		private bool m_IconIsBlue = false;
		private Icon m_IconMag = null;
		private Icon m_IconRed = null;
		private Icon m_IconBlue = null;
		private int m_NotifyStyle = 0;
		private bool m_UserAllowsUpdates;
		private int m_MinuteCounter = 0;
		private System.Timers.Timer m_StopWatch = null;
		private QuestDefinition m_ActiveQuest = null;
		private QuestDefinition m_NullQuest = null;

		private QuestDefinition ActiveQuest {
			get {
				if(this.m_ActiveQuest != null) {
					return this.m_ActiveQuest;
				} 
				return m_NullQuest; //something to return
			}
			set {
				m_ActiveQuest = null;
				m_ActiveQuest = value;
			}
		}

		public PreferencesWindow() {
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			InitializeComponent();
			this.debug = new Logger();
			this.debug.Disabled = true;
			debug.log("--- (UTC) Date: " + DateTime.UtcNow.ToShortDateString() + "-----------------------------\r\n" + ProductName + " Ver." + ProductVersion + " Developer Log. Use it wisely.");
			debug.log("initializing timers");
			this.m_Timer = new System.Timers.Timer();
			this.m_NotificationTimer = new System.Timers.Timer();
			this.m_NotificationTimer.Elapsed += notificationTimer_Elapsed;
			this.m_PollTimer = new System.Timers.Timer();
			this.m_PollTimer.AutoReset = true;
			this.m_PollTimer.Interval = NotifyCore.TIME_MINUTE;
			this.m_PollTimer.Elapsed += pollTimer_Elapsed;
			m_StopWatch = new System.Timers.Timer();
			m_StopWatch.AutoReset = true;
			m_StopWatch.Interval = NotifyCore.TIME_MINUTE;
			m_StopWatch.Elapsed += m_StopWatch_Elapsed;
			debug.log("initializing tray icon");
			notifyIcon1.Text = "PSONotify. \r\nI'll let you know when Emergency Quests are up <3";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.m_IconStorageSpace = this.m_IconBlue = new Icon(GetType(), "miniblue.ico");
			this.m_IconMag = new Icon(GetType(), "mag.ico");
			this.m_IconRed = notifyIcon1.Icon;
			NotifyCore.Instance.settings.disableLogging = true;
			debug.log("initializing ships");
			this.ships = new List<Ship>() {
                new Ship("Ship 01:Feoh", 1),
                new Ship("Ship 02:Ur", 2),
                new Ship("Ship 03:Thorn", 3),
                new Ship("Ship 04:Ansur", 4),
                new Ship("Ship 05:Laguz", 5),
                new Ship("Ship 06:Ken", 6),
                new Ship("Ship 07:Geofu", 7),
                new Ship("Ship 08:Wynn", 8),
                new Ship("Ship 09:Hagall", 9),
                new Ship("Ship 10:Nauthiz", 10),
            };
			ShipSelectionCombobox.DataSource = ships;
			ShipSelectionCombobox.DisplayMember = "name";
			ShipSelectionCombobox.ValueMember = "id";
			debug.log("initializing update frequencies");

			this.updateFrequencies = new List<UpdateFrequency>() {
				new UpdateFrequency("Every minute", NotifyCore.TIME_MINUTE),
				new UpdateFrequency("Every 2 minutes", 2 * NotifyCore.TIME_MINUTE),
                new UpdateFrequency("Every 5 minutes", 5 * NotifyCore.TIME_MINUTE),
				new UpdateFrequency("Never", -1),
            };
			debug.log("initializing known areas");

			this.areas = new Dictionary<string, string>() {
			    {"森林", "Naberius Forest"},
			    {"局所地域", "the Arks are preparing for large scale operations"},
			    {"砂漠", "Lilipa Desert"},
			    {"砂漠遊撃戦", "Lilipa Desert: Guerrilla Warfare"},
			    {"浮遊大陸", "Amduscia Floating Continent"},
			    {"ホワイトデーは大わらわ", "Amduscia: White day"},
			    {"坑道", "Big Vardha"},
			    {"ダークファルス", "Dark Falz Hands/Elder"},
				{"猛る黒曜の暴腕", "Boosted Dark Falz Hands/Elder"},
			    {"火山", "Volcano"},
			    {"ダーカー", "Urban/City"},
			    {"暴走龍の怨嗟", "Chrome Dragon"},
				{"generic", "Generic EQ (event/no details)"}
		    };
			debug.log("restoring settings to prefs UI");
			this.m_NullQuest = new QuestDefinition();
			this.m_NullQuest.AreaName = "nullQuest";
			//restore saved settings to prefs UI
			this.loadSettings();
			if(NotifyCore.GameIsInstalled) {
				if(NotifyCore.Instance.settings.chatCommands.enableOnLaunch) {
					this.enableChatCommands();
				}
			}

			debug.log("Restored user preferences from file");

			if(NotifyCore.Instance.settings.userSavedSettings == true) {
				this.WindowState = FormWindowState.Minimized;
			} else {
				debug.log("Not checking for EQ's, user has not set up their preferences yet. Speaking of which, i'll show them the preferences window..");
				this.displayNotification("PSO Notify", "Don't worry, I'm not going to show you this window every time you run me ; ) [Yes i am, every now and then. Beta software, ppl!]");
			}
			debug.log("initialization complete");
			this.m_PollTimer.Start();


		}
		void loadSettings() {
			//load data
			NotifyCore.Instance.magTimer.NotifyInterval = NotifyCore.Instance.settings.magTimer.interval;
			NotifyCore.Instance.magTimer.AdvanceNotifyInterval = NotifyCore.Instance.settings.magTimer.advanceNotify;

			if(!NotifyCore.GameIsInstalled) { //chatcommands is disabled, but we still need to disable the UI
				ChatCommandsActivateButton.Enabled = false;
				ChatCommandsUserIdTextbox.Enabled = false;
			}
			//restore UI


			QuestNotificationRepeatComboxbox.DataSource = updateFrequencies;
			QuestNotificationRepeatComboxbox.DisplayMember = "name";
			QuestNotificationRepeatComboxbox.ValueMember = "value";
			int notificationFreq = (QuestNotificationRepeatComboxbox.SelectedItem as UpdateFrequency).value;
			if(notificationFreq > 0) {
				if(this.m_NotificationTimer == null) {
					this.m_NotificationTimer = new System.Timers.Timer();
				}
				this.m_NotificationTimer.Interval = notificationFreq;
			}
			ShipSelectionCombobox.SelectedIndex = NotifyCore.Instance.settings.ship - 1;
			ChatCommandsUserIdTextbox.Text = NotifyCore.Instance.settings.chatCommands.psoPlayerId.ToString();
			
			if(NotifyCore.Instance.settings.magTimer.interval < MagFeedingTimeoutNumeric.Minimum) {
				MagFeedingTimeoutNumeric.Value = MagFeedingTimeoutNumeric.Minimum;
			} else {
				MagFeedingTimeoutNumeric.Value = NotifyCore.Instance.settings.magTimer.interval;
			}
			if(NotifyCore.Instance.settings.magTimer.advanceNotify < 1) {
				MagtimerAdvanceNotificationNumeric.Value = 1;
				MagTimerAdvanceNotifyEnabledCheckbox.Checked = false;
				MagtimerAdvanceNotificationNumeric.Enabled = false;
			} else {
				MagtimerAdvanceNotificationNumeric.Value = NotifyCore.Instance.settings.magTimer.advanceNotify;
				MagTimerAdvanceNotifyEnabledCheckbox.Checked = true;
				MagtimerAdvanceNotificationNumeric.Enabled = true;
			}
			openAtLoginCheckbox.Checked = NotifyCore.Instance.settings.openAtLogin;
			QuestNotificationRepeatComboxbox.SelectedIndex = NotifyCore.Instance.settings.notificationRepetitionPolicy;
			this.m_SilenceNotificationsWhileGameIsRunning = noNotificationsWhilePSOIsRunningCheckbox.Checked = NotifyCore.Instance.settings.notificationSettings[ UserSettings.NotifySettingEmergencyQuest ].willObaySilenceWhilePlaying;
			QuestNotificationStyleCombobox.SelectedIndex = NotifyCore.Instance.settings.notificationSettings[ UserSettings.NotifySettingEmergencyQuest ].style;
			audioNotifyStyleCombobox.SelectedIndex = NotifyCore.Instance.settings.notificationSettings[ UserSettings.NotifySettingEmergencyQuest ].soundEffectId;
			versionLabel.Text = ProductName + " Ver. " + ProductVersion;
			string updateChannel = NotifyCore.Instance.settings.updateChannel;
			if(updateChannel.Equals("developer")) {
				UpdateChannelCombobox.SelectedIndex = 0;
			} else {
				UpdateChannelCombobox.SelectedIndex = 1;
			}
			CheckForUpdatesCheckbox.Checked = this.m_UserAllowsUpdates = NotifyCore.Instance.settings.checkForUpdates;
			DevDisableLoggingCheckbox.Checked = debug.Disabled = NotifyCore.Instance.settings.disableLogging;
			if(NotifyCore.IsRunningAsAdmin) {
				openAtLoginCheckbox.Enabled = true;
				disableWordCensorCheckbox.Enabled = true;
			}
			ChatCommandsEnableOnLaunchCheckbox.Checked = NotifyCore.Instance.settings.chatCommands.enableOnLaunch;

			this.ActiveQuest.Mode = NotifyCore.ANNOUNCEMODE_NOTSET;
			NotifyCore.Instance.OnQuestNotification += questDetectedHandler;
			NotifyCore.Instance.OnQuestPollNothingToReport += () => {
				this.displayNotification("Nothing worth mentioning.. sorry!", "Rest assured, i'll keep checking. Just for you <3");
			};
		}
		void m_StopWatch_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
			++m_MinuteCounter;
		}



		void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
			MessageBox.Show("PSONotify unhandled exception: \n" + e.ExceptionObject.ToString());
		}
		void setIconBlue() {
			if(!this.m_IconIsBlue) {
				this.m_IconIsBlue = true;
				this.setSwapIcons();
			}
		}
		void setIconRed() {
			if(this.m_IconIsBlue) {
				this.m_IconIsBlue = false;
				this.setSwapIcons();
			}
		}
		void setSwapIcons() {
			Icon icon = notifyIcon1.Icon;
			notifyIcon1.Icon = this.m_IconStorageSpace;
			this.m_IconStorageSpace = icon;
			icon = null;
		}

		void notificationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
			debug.log("Notifying user as per their settings");
			this.notifyUserOfEmergencyQuest();
		}

		void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
			debug.log("master timer elapsed", Logger.DEBUG);
			if(this.m_Timer.Interval < NotifyCore.TIME_HOUR) { //now continue following an hourly check
				this.m_Timer.Interval = NotifyCore.TIME_HOUR;
				debug.log("rescheduled master timer to fire again in one hour", Logger.DEBUG);
			}
			//this.m_PollTimer.Start();
			if(this.m_UserAllowsUpdates) {
				debug.log("let's check for updates..");
				this.checkForUpdates();
			}
		}
		void pollTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
			DateTime t = DateTime.Now;
			if(!this.ActiveQuest.IsActive && !this.ActiveQuest.AnnouncementActive) {
				if(t.Minute >= 45 && t.Minute <= 59) {
					debug.log("checking for EQ", Logger.DEBUG);
					NotifyCore.Instance.checkForQuests();
				}
			} else {
				if(this.ActiveQuest.AnnouncementActive) { // transition from pre-announce to active
					debug.log("an eq is announced, checking if i should be transitioning to ACTIVE mode");
					if(t.Minute >= 0 && t.Minute < this.ActiveQuest.DurationInMinutes) {
						debug.log("Transitioning into Active quest mode, starting EQ-End timer, and notifying user EQ Changed to Active", Logger.DEBUG);
						this.ActiveQuest.IsActive = true;
						this.ActiveQuest.AnnouncementActive = false;
						this.notifyUserOfEmergencyQuest();
					} else {
						debug.log("i guess not..");
					}
				} else if(this.ActiveQuest.IsActive) {
					debug.log("quest active");
					if(t.Minute > this.ActiveQuest.DurationInMinutes) {
						debug.log("but actually.. it expired! so i'll kill it");
						this.ActiveQuest.IsActive = false;
						this.setIconRed(); //change back to red icon
						if(this.m_RepeatNotifications) {
							this.m_NotificationTimer.Stop();
						}
					}
				}
			}
		}

		private void quitApplication_Click(object sender, EventArgs e) {
			shutDownApp();
		}
		private void shutDownApp() {
			ChatCommandProcessor.Instance.saveData();
			notifyIcon1.Visible = false;
			Environment.Exit(0);
		}

		private void checkForUpdates(bool userTriggered = false) {
			if(userTriggered || this.m_UserAllowsUpdates) {
				//make a call to our cool updater, Sparkle
				try {
					string pathToExe = Directory.GetCurrentDirectory();
					debug.log("Launching " + pathToExe + @"\Sparkle.exe for updates");
					if(NotifyCore.Instance.settings.updateChannel == "developer") {
						Process.Start(pathToExe + @"\Sparkle.exe", ProductName + " " + ProductVersion + " \"http://digitalhaze.eu/psonotify/debug.bundle/\"");
					} else {
						Process.Start(pathToExe + @"\Sparkle.exe", ProductName + " " + ProductVersion + " \"http://digitalhaze.eu/psonotify/update.bundle/\"");
					}
				} catch {
					debug.log("Sparkle was unable to start. No updates! :(");
					//don't do anything, the windows says exe isnt there, but i know it is, so shit makes no sense
				}
			}
		}
		void playWav(string location) {
			SoundPlayer player = new SoundPlayer();
			player.LoadCompleted += playAudio_LoadComplete;
			player.SoundLocation = location;
			player.LoadAsync();
		}

		void playAudio_LoadComplete(object sender, AsyncCompletedEventArgs e) {
			SoundPlayer player = sender as SoundPlayer;
			try {
				player.Play();
			} catch {
				debug.log("Failed to play sound from [" + player.SoundLocation + "]", Logger.ERROR);
			}
		}
		void updateCheckBalloon_Click(object sender, EventArgs e) {
			notifyIcon1.BalloonTipClicked -= this.updateCheckBalloon_Click;
		}

		private void showPreferencesWindow_Click(object sender, EventArgs e) {
			if(NotifyCore.Instance.settings.magTimer.interval < MagFeedingTimeoutNumeric.Minimum || NotifyCore.Instance.settings.magTimer.interval > MagFeedingTimeoutNumeric.Maximum) {
				MagFeedingTimeoutNumeric.Value = MagFeedingTimeoutNumeric.Maximum;
			} else {
				MagFeedingTimeoutNumeric.Value = NotifyCore.Instance.settings.magTimer.interval;
			}
			//re-check disable wordcensor state		
			if(NotifyCore.IsRunningAsAdmin) {
				if(!NotifyCore.GameIsRunning) {
					disableWordCensorCheckbox.Enabled = true;
					string path = NotifyCore.AppsFolder + NotifyCore.PSOWORDCENSORFILEPATH;
					if(File.Exists(path)) {
						disableWordCensorCheckbox.Checked = false;
						disableWordCensorCheckbox.Text = "";
					} else {
						if(File.Exists(path + ".bak")) {
							disableWordCensorCheckbox.Text = "";
							disableWordCensorCheckbox.Checked = true;
						} else {
							//something has changed beyond our control, so we disable this feature entirely
							disableWordCensorCheckbox.Checked = false;
							disableWordCensorCheckbox.Enabled = false;
							disableWordCensorCheckbox.Text = "(not safe to use)";
						}
					}
				} else {
					disableWordCensorCheckbox.Text = "(exit game to enable)";
					disableWordCensorCheckbox.Enabled = false;
				}
			}

			this.WindowState = FormWindowState.Normal;
			Show();
		}
		private void saveSettings() {
			NotifyCore.Instance.settings.ship = ShipSelectionCombobox.SelectedIndex + 1;
			NotifyCore.Instance.settings.openAtLogin = openAtLoginCheckbox.Checked;
			NotifyCore.Instance.settings.notificationRepetitionPolicy = QuestNotificationRepeatComboxbox.SelectedIndex;
			bool shipWasChanged = !(NotifyCore.Instance.settings.ship == (ShipSelectionCombobox.SelectedIndex + 1));
			NotifyCore.Instance.settings.notificationSettings[ UserSettings.NotifySettingEmergencyQuest ].willObaySilenceWhilePlaying = noNotificationsWhilePSOIsRunningCheckbox.Checked;
			NotifyCore.Instance.settings.userSavedSettings = true;
			NotifyCore.Instance.settings.notificationSettings[ UserSettings.NotifySettingEmergencyQuest ].style = QuestNotificationStyleCombobox.SelectedIndex;
			NotifyCore.Instance.settings.notificationSettings[ UserSettings.NotifySettingEmergencyQuest ].soundEffectId = audioNotifyStyleCombobox.SelectedIndex;
			NotifyCore.Instance.settings.checkForUpdates = this.m_UserAllowsUpdates;
			NotifyCore.Instance.settings.disableLogging = DevDisableLoggingCheckbox.Checked;
			if(NotifyCore.Instance.settings.chatCommands.psoPlayerId == 0) {
				NotifyCore.Instance.settings.chatCommands.psoPlayerId = Convert.ToInt32(ChatCommandsUserIdTextbox.Text);
			}
			if(MagTimerAdvanceNotifyEnabledCheckbox.Checked == false) {
				NotifyCore.Instance.settings.magTimer.advanceNotify = 0;
			} else {
				NotifyCore.Instance.settings.magTimer.advanceNotify = (NotifyCore.Instance.magTimer.AdvanceNotifyInterval / NotifyCore.TIME_MINUTE);
			}
			if(NotifyCore.IsRunningAsAdmin) {
				string originalFile = NotifyCore.AppsFolder + NotifyCore.PSOWORDCENSORFILEPATH;
				string backupFile = originalFile + ".bak";
				if(disableWordCensorCheckbox.Checked) {
					//delete old copies of this file, it was updated anyway, else we wouldnt be here
					if(File.Exists(originalFile)) {
						if(File.Exists(backupFile)) {
							File.Delete(backupFile);
						}
						File.Move(originalFile, backupFile);
					}
				} else {
					if(!File.Exists(originalFile)) {
						if(File.Exists(backupFile)) {
							File.Move(backupFile, originalFile);
						}
					}

				}
				NotifyCore.OpenAtLogin(openAtLoginCheckbox.Checked);
			}
			//command processor also calls settings.save, so no need to call it again
			ChatCommandProcessor.Instance.saveData();
			//NotifyCore.Instance.settings.save();
			Hide();

			if(shipWasChanged) {
				NotifyCore.Instance.checkForQuests();
			}
		}
		private void buttonDone_Click(object sender, EventArgs e) {
			saveSettings();
		}
		private void notifyuserOfMagFeedingTime(int minutesRemaining = 0) {
			this.audioNotify();
			if(minutesRemaining > 0) {
				this.displayNotification("PSONotify MagTimer", "Attention: Mag Requires Feeding in " + minutesRemaining + " minutes");
			} else {
				this.displayNotification("PSONotify MagTimer", "Mag Requires Feeding, please see to it!");
			}
		}
		private void notifyUserOfEmergencyQuest() {
			switch(this.m_NotifyStyle) {
				case NotifyCore.NOTIFYSTYLE_ICON_ONLY: {
					return;
				}
				case NotifyCore.NOTIFYSTYLE_EVERYTHING: {
					if(this.ActiveQuest.IsActive) {
						this.audioNotify();
						switch(this.ActiveQuest.Mode) {
							case NotifyCore.ANNOUNCEMODE_QUEST: {
								this.displayNotification("PSO 2 Emergency Quest is now underway", this.ActiveQuest.AreaName + "\r\nTime remaining: " + (this.ActiveQuest.DurationInMinutes - DateTime.Now.Minute) + " min");
								break;
							}
							case NotifyCore.ANNOUNCEMODE_LIVEEVENT: {
								this.displayNotification("A PSO 2 Live Event is now underway", this.ActiveQuest.AreaName + "\r\nTime remaining: " + (this.ActiveQuest.DurationInMinutes - DateTime.Now.Minute) + " min");
								break;
							}
							case NotifyCore.ANNOUNCEMODE_INTERRUPTRANKING: {
								this.displayNotification("A PSO 2 Interrupt ranking is now underway", this.ActiveQuest.AreaName + "\r\nTime remaining: " + (this.ActiveQuest.DurationInMinutes - DateTime.Now.Minute) + " min");
								break;
							}
						}
						return;
					}
					if(this.ActiveQuest.AnnouncementActive) {
						this.audioNotify();
						switch(this.ActiveQuest.Mode) {
							case NotifyCore.ANNOUNCEMODE_QUEST: {
								this.displayNotification("A PSO 2 Emergency Quest will begin soon", this.ActiveQuest.AreaName + "\r\nTime until start: " + (60 - DateTime.Now.Minute) + " min");
								break;
							}
							case NotifyCore.ANNOUNCEMODE_LIVEEVENT: {
								this.displayNotification("A PSO 2 Live Event will begin soon", this.ActiveQuest.AreaName + "\r\nTime remaining: " + (this.ActiveQuest.DurationInMinutes - DateTime.Now.Minute) + " min");
								break;
							}
							case NotifyCore.ANNOUNCEMODE_INTERRUPTRANKING: {
								this.displayNotification("A PSO 2 Interrupt ranking will begin soon", this.ActiveQuest.AreaName + "\r\nTime remaining: " + (this.ActiveQuest.DurationInMinutes - DateTime.Now.Minute) + " min");
								break;
							}
						}
						return;
					}
					return;
				}
				case NotifyCore.NOTIFYSTYLE_BUBBLE_ONLY: {
					if(this.ActiveQuest.IsActive) {
						switch(this.ActiveQuest.Mode) {
							case NotifyCore.ANNOUNCEMODE_QUEST: {
								this.displayNotification("PSO 2 Emergency Quest is now underway", this.ActiveQuest.AreaName + "\r\nTime remaining: " + (this.ActiveQuest.DurationInMinutes - DateTime.Now.Minute) + " min");
								break;
							}
							case NotifyCore.ANNOUNCEMODE_LIVEEVENT: {
								this.displayNotification("A PSO 2 Live Event is now underway", this.ActiveQuest.AreaName + "\r\nTime remaining: " + (this.ActiveQuest.DurationInMinutes - DateTime.Now.Minute) + " min");
								break;
							}
							case NotifyCore.ANNOUNCEMODE_INTERRUPTRANKING: {
								this.displayNotification("A PSO 2 Interrupt ranking is now underway", this.ActiveQuest.AreaName + "\r\nTime remaining: " + (this.ActiveQuest.DurationInMinutes - DateTime.Now.Minute) + " min");
								break;
							}
						}
						return;
					}
					if(this.ActiveQuest.AnnouncementActive) {
						switch(this.ActiveQuest.Mode) {
							case NotifyCore.ANNOUNCEMODE_QUEST: {
								this.displayNotification("A PSO 2 Emergency Quest will begin soon", this.ActiveQuest.AreaName + "\r\nTime until start: " + (60 - DateTime.Now.Minute) + " min");
								break;
							}
							case NotifyCore.ANNOUNCEMODE_LIVEEVENT: {
								this.displayNotification("A PSO 2 Live Event will begin soon", this.ActiveQuest.AreaName + "\r\nTime remaining: " + (this.ActiveQuest.DurationInMinutes - DateTime.Now.Minute) + " min");
								break;
							}
							case NotifyCore.ANNOUNCEMODE_INTERRUPTRANKING: {
								this.displayNotification("A PSO 2 Interrupt ranking will begin soon", this.ActiveQuest.AreaName + "\r\nTime remaining: " + (this.ActiveQuest.DurationInMinutes - DateTime.Now.Minute) + " min");
								break;
							}
						}
						return;
					}
					return;
				}
				case NotifyCore.NOTIFYSTYLE_AUDIO_ONLY: {
					if(this.ActiveQuest.IsActive || this.ActiveQuest.AnnouncementActive) {
						this.audioNotify();
						return;
					}
					return;
				}
			}
		}
		private bool userAllowsNotify {
			get {
				if(this.m_SilenceNotificationsWhileGameIsRunning) {
					if(!NotifyCore.GameIsRunning) {
						return true;
					}
				} else {
					return true;
				}
				return false;
			}
		}
		private void displayNotification(string title, string message, bool forceDisplay = false) {
			debug.log("Notify: " + title + ", " + message);
			if(this.userAllowsNotify) {
				if(NotifyCore.GameIsRunning && ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
					NotifyCore.Instance.speakText(title + ".\n" + message);
				} else {
					notifyIcon1.ShowBalloonTip(0, title, message, ToolTipIcon.Info);
				}
			}
		}
		private void audioNotify() {
			if(this.userAllowsNotify) {
				System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
				System.IO.Stream s = a.GetManifestResourceStream("PSONotify." + (this.audioNotifyStyleCombobox.SelectedIndex + 1) + ".wav");
				using(SoundPlayer p = new SoundPlayer()) {
					p.Stream = s;
					p.LoadCompleted += delegate {
						p.Play();
					};
					p.LoadAsync();
				}

			}
		}
		private void audioNotify(object sender, EventArgs e) {
			this.audioNotify();
		}

		private void checkForEQ_Click(object sender, EventArgs e) {
			debug.log("User requested manual EQ-check");
			NotifyCore.Instance.checkForQuests(true);
		}

		private void shipSelectionChanged(object sender, EventArgs e) {
			if(ShipSelectionCombobox.SelectedItem != null) {
				Ship ship = ShipSelectionCombobox.SelectedItem as Ship;
				debug.log("Changed Ship into: " + ship.name, Logger.DEBUG);
				NotifyCore.Instance.settings.ship = ship.id;
			}
		}

		private void updateIntervalSelectionChanged(object sender, EventArgs e) {
			if(QuestNotificationRepeatComboxbox.SelectedItem != null) {
				UpdateFrequency freq = QuestNotificationRepeatComboxbox.SelectedItem as UpdateFrequency;
				if(freq.value > 0) {
					debug.log("Changed Notification repeat interval into: once every " + freq.name);
					this.m_RepeatNotifications = true;
					if(this.m_NotificationTimer != null) {
						this.m_NotificationTimer.Interval = freq.value;
					}
				} else {
					this.m_RepeatNotifications = false;
					debug.log("Changed Notification repeat interval into: Don't repeat");
				}
			}
		}

		private void checkForUpdatesCheckbox_Changed(object sender, EventArgs e) {
			this.m_UserAllowsUpdates = CheckForUpdatesCheckbox.Checked;
			if(CheckForUpdatesCheckbox.Checked) {
				debug.log("Checking for updates is now enabled");
			} else {
				debug.log("Checking for updates is now disabled");
			}
		}

		private void userUpdateCheck_Click(object sender, EventArgs e) {
			debug.log("User requested manual update check");
			this.checkForUpdates(true);
		}

		private void showBuildDetails_Click(object sender, MouseEventArgs e) {
			if(e.Button == System.Windows.Forms.MouseButtons.Right) {
				this.displayNotification("PSONotify Version Info", "Current Ver: " + ProductVersion);
			}
		}

		private void noNotificationsWhilePSOIsRunningCheckbox_CheckedChanged(object sender, EventArgs e) {
			this.m_SilenceNotificationsWhileGameIsRunning = noNotificationsWhilePSOIsRunningCheckbox.Checked;
		}


		private void button2_Click(object sender, EventArgs e) {
			this.audioNotify();
		}


		private void notificationStyleCombobox_SelectedIndexChanged(object sender, EventArgs e) {
			this.m_NotifyStyle = QuestNotificationStyleCombobox.SelectedIndex;
		}

		private void questDetectedHandler(QuestDefinition quest) {
			this.ActiveQuest = quest;
			if(this.ActiveQuest.IsActive || this.ActiveQuest.AnnouncementActive) {
				if(areas.ContainsKey(this.ActiveQuest.AreaName)) {
					debug.log("Translating known area name: " + this.ActiveQuest.AreaName + " into english: " + areas[ this.ActiveQuest.AreaName ]);
					this.ActiveQuest.AreaName = areas[ this.ActiveQuest.AreaName ];
				} else {
					debug.log("Using name as-is because it's either unknown, InterruptRanking or LiveEvent: " + this.ActiveQuest.AreaName);
				}
				if(this.m_RepeatNotifications) {
					debug.log("Started re-notification timer", Logger.DEBUG);
					this.m_NotificationTimer.Start();
				}
				this.setIconBlue();//change to blue icon
				this.notifyUserOfEmergencyQuest();
			}
		}

		private void UpdateChannelCombobox_SelectedIndexChanged(object sender, EventArgs e) {
			if(this.UpdateChannelCombobox.SelectedIndex == 0) {
				NotifyCore.Instance.settings.updateChannel = "release";
			} else {
				NotifyCore.Instance.settings.updateChannel = "developer";
			}
			debug.log("Changed update channel: " + NotifyCore.Instance.settings.updateChannel);
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e) {
			if(this.debug != null) {
				debug.Disabled = DevDisableLoggingCheckbox.Checked;
			}
		}

		private void noNotificationsWhilePSOIsRunningCheckbox_CheckedChanged_1(object sender, EventArgs e) {
			this.m_SilenceNotificationsWhileGameIsRunning = noNotificationsWhilePSOIsRunningCheckbox.Checked;
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			ProcessStartInfo sInfo = new ProcessStartInfo("https://twitter.com/leroy_k");
			Process.Start(sInfo);
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			ProcessStartInfo sInfo = new ProcessStartInfo("https://twitter.com/pso2_emg_bot");
			Process.Start(sInfo);
		}

		private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			ProcessStartInfo sInfo = new ProcessStartInfo("http://www.guildportal.com/Guild.aspx?GuildID=473076&TabID=4080564");
			Process.Start(sInfo);
		}

		private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			ProcessStartInfo sInfo = new ProcessStartInfo("http://phantasystarhispano.com/");
			Process.Start(sInfo);
		}

		private void button3_Click(object sender, EventArgs e) {
			notifyIcon1.Icon = this.m_IconMag;
		}

		private void MagFeedingTimeoutNumeric_ValueChanged(object sender, EventArgs e) {
			NotifyCore.Instance.settings.magTimer.interval = (int)MagFeedingTimeoutNumeric.Value;
			NotifyCore.Instance.magTimer.NotifyInterval = NotifyCore.Instance.settings.magTimer.interval;
			MagTimerTimeRemainingLabel.Text = NotifyCore.Instance.settings.magTimer.interval + " minutes";
			if(NotifyCore.Instance.magTimer.NotifyInterval > 2) {
				MagtimerAdvanceNotificationNumeric.Maximum = NotifyCore.Instance.magTimer.NotifyInterval - 1;
			} else {
				MagtimerAdvanceNotificationNumeric.Maximum = 2;
				MagTimerAdvanceNotifyEnabledCheckbox.Checked = false;
			}
		}

		private void MagtimerStartButton_Click(object sender, EventArgs e) {
			startMagTimer();
		}
		private void startMagTimer() {
			NotifyCore.Instance.magTimer.UpdateUiTime += interfaceUpdate;
			NotifyCore.Instance.magTimer.startTimer(NotifyCore.Instance.magTimer.NotifyInterval);
			if(this.InvokeRequired) { //if running on non-UI thread
				this.Invoke((MethodInvoker)delegate {
					MagTimerStartButton.Enabled = false;
					MagFeedingTimeoutNumeric.Enabled = false;
					MagTimerStopButton.Enabled = true;
					MagTimerAdvanceNotifyEnabledCheckbox.Enabled = false;
					MagtimerAdvanceNotificationNumeric.Enabled = false;
					contextMenuStrip1.Items[ 1 ].Text = "Reset MagTimer";
					contextMenuStrip1.Items[ 1 ].Click -= MagtimerStartButton_Click;
					contextMenuStrip1.Items[ 1 ].Click += MagTimerResetButton_Click;
					this.notifyIcon1.Icon = this.m_IconMag;
				});
			} else { //if we ARE running on UI thread
				MagTimerStartButton.Enabled = false;
				MagFeedingTimeoutNumeric.Enabled = false;
				MagTimerStopButton.Enabled = true;
				MagTimerAdvanceNotifyEnabledCheckbox.Enabled = false;
				MagtimerAdvanceNotificationNumeric.Enabled = false;
				contextMenuStrip1.Items[ 1 ].Text = "Reset MagTimer";
				contextMenuStrip1.Items[ 1 ].Click -= MagtimerStartButton_Click;
				contextMenuStrip1.Items[ 1 ].Click += MagTimerResetButton_Click;
				this.notifyIcon1.Icon = this.m_IconMag;
			}
			NotifyCore.Instance.magTimer.FeedTime += magTimer_FeedTime;
			NotifyCore.Instance.magTimer.AdvanceNotify += magTimer_AdvanceNotify;
		}

		void magTimer_AdvanceNotify(int minutesRemaining) {
			this.notifyuserOfMagFeedingTime(minutesRemaining);
		}

		private void magTimer_FeedTime() {
			this.notifyuserOfMagFeedingTime();
		}

		private void interfaceUpdate(int percentageRemaining) {
			//when using delegates, updating UI doesnt always work. if it doesnt, use this:
			if(MagTimerTimeRemainingLabel.InvokeRequired) {
				this.Invoke((MethodInvoker)delegate {
					MagTimerTimeRemainingLabel.Text = NotifyCore.Instance.magTimer.MinutesRemaining + " minutes";
				});
			}
		}
		private void MagTimerResetButton_Click(object sender, EventArgs e) {
			resetMagTimer();
		}
		private void MagTimerStopButton_Click(object sender, EventArgs e) {
			stopMagTimer();
		}
		private void stopMagTimer() {
			NotifyCore.Instance.magTimer.FeedTime -= magTimer_FeedTime;
			NotifyCore.Instance.magTimer.AdvanceNotify -= magTimer_AdvanceNotify;
			NotifyCore.Instance.magTimer.stopTimer();
			if(this.InvokeRequired) {
				this.Invoke((MethodInvoker)delegate { //used when called on non-UI thread
					MagTimerStartButton.Enabled = true;
					MagFeedingTimeoutNumeric.Enabled = true;
					MagTimerStopButton.Enabled = false;
					MagTimerAdvanceNotifyEnabledCheckbox.Enabled = true;
					MagtimerAdvanceNotificationNumeric.Enabled = true;
					contextMenuStrip1.Items[ 2 ].Text = "Start MagTimer";
					contextMenuStrip1.Items[ 2 ].Click += MagtimerStartButton_Click;
					contextMenuStrip1.Items[ 2 ].Click -= MagTimerResetButton_Click;
					if(this.notifyIcon1.Icon == this.m_IconMag) {
						this.notifyIcon1.Icon = this.m_IconRed;
					}
				});
			} else { // used when called on UI-thread
				MagTimerStartButton.Enabled = true;
				MagFeedingTimeoutNumeric.Enabled = true;
				MagTimerStopButton.Enabled = false;
				MagTimerAdvanceNotifyEnabledCheckbox.Enabled = true;
				MagtimerAdvanceNotificationNumeric.Enabled = true;
				contextMenuStrip1.Items[ 2 ].Text = "Start MagTimer";
				contextMenuStrip1.Items[ 2 ].Click += MagtimerStartButton_Click;
				contextMenuStrip1.Items[ 2 ].Click -= MagTimerResetButton_Click;
				if(this.notifyIcon1.Icon == this.m_IconMag) {
					this.notifyIcon1.Icon = this.m_IconRed;
				}
			}
		}

		private void resetMagTimer() {
			NotifyCore.Instance.magTimer.resetTimer();
		}

		private void button4_Click(object sender, EventArgs e) {
			this.Text = NotifyCore.Instance.magTimer.MinutesRemaining + " minutes";
		}

		private void checkBox2_CheckedChanged_1(object sender, EventArgs e) {
			MagtimerAdvanceNotificationNumeric.Enabled = MagTimerAdvanceNotifyEnabledCheckbox.Checked;
			if(MagFeedingTimeoutNumeric.Value > 1) {
				MagtimerAdvanceNotificationNumeric.Maximum = MagFeedingTimeoutNumeric.Value - 1;
			} else {
				MagtimerAdvanceNotificationNumeric.Maximum = 1;
				MagtimerAdvanceNotificationNumeric.Enabled = false;
			}
			NotifyCore.Instance.magTimer.AdvanceNotifyInterval = (int)MagtimerAdvanceNotificationNumeric.Value;
		}

		private void MagtimerAdvanceNotificationNumeric_ValueChanged(object sender, EventArgs e) {
			if(MagTimerAdvanceNotifyEnabledCheckbox.Checked) {
				NotifyCore.Instance.settings.magTimer.advanceNotify = (int)MagtimerAdvanceNotificationNumeric.Value;
				NotifyCore.Instance.magTimer.AdvanceNotifyInterval = (NotifyCore.Instance.settings.magTimer.advanceNotify * NotifyCore.TIME_MINUTE);
			}
		}

		private void button3_Click_1(object sender, EventArgs e) {
			enableChatCommands();
		}
		public void enableChatCommands() {
			if(NotifyCore.GameIsInstalled) {
				ChatCommandsActivateButton.Enabled = false;
				ChatCommandsUserIdTextbox.Enabled = false;
				if(ChatCommandsUserIdTextbox.Text != NotifyCore.Instance.settings.chatCommands.psoPlayerId.ToString()) {
					if(ChatCommandsUserIdTextbox.Text.Length == 8) {
						int userId = 0;
						if(Int32.TryParse(ChatCommandsUserIdTextbox.Text, out userId)) {
							if(userId != NotifyCore.Instance.settings.chatCommands.psoPlayerId) {
								NotifyCore.Instance.settings.chatCommands.psoPlayerId = userId;
							}
						} else {
							debug.log("refused to enable ChatCommands; playerId supplied is malformed");
							return;
						}
					}
				}
				ChatCommandProcessor.Instance.init();
				ChatCommandProcessor.Instance.OnCommandReceived += Instance_OnCommandReceived;
				debug.log(" enabled chatcommands for user with playerid: " + NotifyCore.Instance.settings.chatCommands.psoPlayerId);
			}
		}

		void Instance_OnCommandReceived(int moduleId, int actionId, int param = -1) {
			debug.log("ChatCommand: module " + moduleId + " action " + actionId + " param " + param);

			switch(moduleId) {
				case (int)ChatCommandProcessor.CommandModules.VoiceFeedback: {
					switch(actionId) {
						case (int)ChatCommandProcessor.SwitchActions.On: {
							ChatCommandProcessor.Instance.VoiceFeedbackEnabled = true;
							NotifyCore.Instance.speakText("Voice feedback is now enabled");
							break;
						}
						case (int)ChatCommandProcessor.SwitchActions.Off: {
							ChatCommandProcessor.Instance.VoiceFeedbackEnabled = false;
							NotifyCore.Instance.speakText("Voice feedback is now disabled");
							break;
						}
						default:
						break;
					}
					break;
				}
				case (int)ChatCommandProcessor.CommandModules.Configure: {
					switch(actionId) {
						case (int)ChatCommandProcessor.ConfigurationActions.Ship: {
							if(param > 0) {
								NotifyCore.Instance.settings.ship = param;
								ShipSelectionCombobox.SelectedIndex = param - 1;
								if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
									NotifyCore.Instance.speakText("player ship changed to ship number: " + param);
								}
							}
							break;
						}
						case (int)ChatCommandProcessor.ConfigurationActions.UpdateChannel: {
							if(param > -1) {
								if(param == 0) {
									NotifyCore.Instance.settings.updateChannel = "release";
								} else {
									NotifyCore.Instance.settings.updateChannel = "developer";
								}
								if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
									NotifyCore.Instance.speakText("changed application update channel to " + NotifyCore.Instance.settings.updateChannel);
								}
							} else {
								if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
									NotifyCore.Instance.speakText("application update channel is currently set to the " + NotifyCore.Instance.settings.updateChannel + "-channel");
								}
							}
							break;
						}
						case (int)ChatCommandProcessor.ConfigurationActions.Save: {
							this.saveSettings();
							if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
								NotifyCore.Instance.speakText("I've saved your settings");
							}
							break;
						}
					}
					break;
				}
				case (int)ChatCommandProcessor.CommandModules.Master: {
					switch(actionId) {
						case (int)ChatCommandProcessor.MasterActions.Identify: {
							//not currently implemented
							break;
						}
						case (int)ChatCommandProcessor.MasterActions.Disable: {
							shutDownApp();
							break;
						}
						case (int)ChatCommandProcessor.MasterActions.UpdateApp: {
							this.checkForUpdates();
							break;
						}
						case (int)ChatCommandProcessor.MasterActions.UpdateDefinitions: {
							NotifyCore.Instance.checkQuestDefinitions();
							break;
						}
						default:
						break;
					}
					break;
				}
				case (int)ChatCommandProcessor.CommandModules.MagTimer: {
					switch(actionId) {
						case (int)ChatCommandProcessor.TimerActions.Start: {
							if(param == -2) {
								if(m_MinuteCounter >= MagFeedingTimeoutNumeric.Minimum) {
									NotifyCore.Instance.magTimer.NotifyInterval = m_MinuteCounter;
									if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
										NotifyCore.Instance.speakText("mag timer started with feeding interval from stopwatch");
									}
								} else {
									NotifyCore.Instance.magTimer.NotifyInterval = 20;
									if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
										NotifyCore.Instance.speakText("mag timer started. stopwatch count was too low, so I took the liberty to set it to 20 minutes");
									}
								}
								
							} else {
								if(param > 0) {
									if(param < MagFeedingTimeoutNumeric.Minimum) {
										NotifyCore.Instance.speakText("really now? no mag takes " + param + " minutes to finish dinner. Try something between 20 and 50");
										break;
									} else {
										NotifyCore.Instance.magTimer.NotifyInterval = param;
										if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
											NotifyCore.Instance.speakText("mag timer started. will prompt for feeding in " + param + " minutes");
										}
									}
								} else {
									if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
										NotifyCore.Instance.speakText("mag timer started");
									}
								}
							}
							startMagTimer();
							break;
						}
						case (int)ChatCommandProcessor.TimerActions.AdvanceNotify: {
							if(param > -1) {
								if(param > 1) {
									if(param > MagtimerAdvanceNotificationNumeric.Minimum && param < MagtimerAdvanceNotificationNumeric.Maximum) {
										NotifyCore.Instance.settings.magTimer.advanceNotify = param;
										if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
											NotifyCore.Instance.speakText("mag timer advance notification has been changed, it is now set to trigger " + param + " minutes before feeding time");
										}
									} else {
										if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
											NotifyCore.Instance.settings.magTimer.advanceNotify = 5;
											NotifyCore.Instance.speakText("mag timer advance notification has been changed. The interval you've specified was not between " + MagtimerAdvanceNotificationNumeric.Minimum + " and " + MagtimerAdvanceNotificationNumeric.Maximum + ", So I took the liberity to set it to 5 minutes before feeding time");
										}
									}
									NotifyCore.Instance.magTimer.AdvanceNotifyInterval = NotifyCore.Instance.settings.magTimer.advanceNotify;
									//updating the UI causes some funky bugs for some reason, so it's disabled until i figure out how/why
									// bug: continues to spam exception and repeats feedback voice message
									if(this.InvokeRequired) {
										this.Invoke((MethodInvoker)delegate {
											MagtimerAdvanceNotificationNumeric.Value = NotifyCore.Instance.magTimer.AdvanceNotifyInterval;
										});
									} else {
										MagtimerAdvanceNotificationNumeric.Value = NotifyCore.Instance.magTimer.AdvanceNotifyInterval;
									}
								} else {
									if(param == (int)ChatCommandProcessor.SwitchActions.Off) {
										NotifyCore.Instance.magTimer.AdvanceNotifyInterval = 0;
										if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
											NotifyCore.Instance.speakText("mag timer advance notification has been disabled");
										}
									} else {
										NotifyCore.Instance.magTimer.AdvanceNotifyInterval = NotifyCore.Instance.settings.magTimer.advanceNotify;
										if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
											NotifyCore.Instance.speakText("mag timer advance notification has been enabled, you will be notified " + NotifyCore.Instance.magTimer.NotifyInterval + " minutes before feeding time");
										}
									}
								}
							} else {
								if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
									NotifyCore.Instance.speakText("mag timer advance notification is currently set at " + NotifyCore.Instance.magTimer.AdvanceNotifyInterval + " minutes before feeding");
								}
							}
							break;
						}
						case (int)ChatCommandProcessor.TimerActions.Interval: {
							if(param > -1) {
								NotifyCore.Instance.magTimer.NotifyInterval = NotifyCore.Instance.settings.magTimer.interval = param;
								if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
									NotifyCore.Instance.speakText("mag timer notification interval has been changed, it is now set to trigger every " + param + " minutes");
									return;
								}
							} else {
								if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
									NotifyCore.Instance.speakText("mag timer notification is currently set to trigger every " + NotifyCore.Instance.magTimer.NotifyInterval + " minutes");
								}
							}
							break;
						}
						case (int)ChatCommandProcessor.TimerActions.Cancel: {
							stopMagTimer();
							if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
								NotifyCore.Instance.speakText("mag timer was cancelled");
							}
							break;
						}
						case (int)ChatCommandProcessor.TimerActions.Reset: {
							resetMagTimer();
							if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
								NotifyCore.Instance.speakText("mag timer was reset");
							}
							break;
						}
						case (int)ChatCommandProcessor.TimerActions.Status: {
							if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
								if(NotifyCore.Instance.magTimer.MinutesRemaining > 0) {
									NotifyCore.Instance.speakText("mag timer has " + NotifyCore.Instance.magTimer.MinutesRemaining + " minutes remaining until next feeding");
									if(NotifyCore.Instance.magTimer.AdvanceNotifyInterval > 0) {
										if(NotifyCore.Instance.magTimer.MinutesRemaining > NotifyCore.Instance.magTimer.AdvanceNotifyInterval) {
											NotifyCore.Instance.speakText("also, " + NotifyCore.Instance.magTimer.MinutesRemainingAdvanceNotify + "minutes are left until advance-notification");
										}
									}
								} else {
									NotifyCore.Instance.speakText("mag timer is not currently running");
								}
							}
							break;
						}
					}
					break;
				}
				case (int)ChatCommandProcessor.CommandModules.Stopwatch: {
					switch(actionId) {
						case (int)ChatCommandProcessor.StopwatchActions.Start: {
							m_MinuteCounter = 0;
							m_StopWatch.Stop();
							m_StopWatch.Start();
							if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
								NotifyCore.Instance.speakText("stopwatch started");
							}
							break;
						}
						case (int)ChatCommandProcessor.StopwatchActions.Stop: {
							m_StopWatch.Stop();
							if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
								NotifyCore.Instance.speakText("stopwatch stopped at " + m_MinuteCounter + " minutes");
							}
							break;
						}
						case (int)ChatCommandProcessor.StopwatchActions.Count: {
							if(ChatCommandProcessor.Instance.VoiceFeedbackEnabled) {
								NotifyCore.Instance.speakText("stopwatch reads " + m_MinuteCounter + " minutes");
							}
							break;
						}
					}
					break;
				}
				case (int)ChatCommandProcessor.CommandModules.Help: {

					switch(actionId) {
						case (int)ChatCommandProcessor.HelpActions.Modules: {
							NotifyCore.Instance.speakText("the following modules are currently available: " + ChatCommandProcessor.Instance.listModules());
							break;
						}
						case (int)ChatCommandProcessor.HelpActions.Actions: {
							if(ChatCommandProcessor.Instance.moduleExists(param)) {
								NotifyCore.Instance.speakText("the following actions are available for this module: " + ChatCommandProcessor.Instance.listActionsForModule(param));
							} else {
								NotifyCore.Instance.speakText("i'm terribly sorry, but no such module exists");

							}
							break;
						}
					}
					break;
				}
				default:
				break;
			}
		}

		private void button2_Click_1(object sender, EventArgs e) {
			this.checkForUpdates(true);
		}

		private void checkBox2_CheckedChanged_2(object sender, EventArgs e) {
			NotifyCore.Instance.settings.chatCommands.enableOnLaunch = ChatCommandsEnableOnLaunchCheckbox.Checked;
		}

		private void tabPage1_Click(object sender, EventArgs e) {

		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e) {

		}

		private void button3_Click_2(object sender, EventArgs e) {
			Debugger.Log(0, null, NotifyCore.generateFingerprint() + "\n");
		}
	}
}
