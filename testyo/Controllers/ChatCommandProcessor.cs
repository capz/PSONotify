using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSONotify {
	public class ChatCommandProcessor {
		private static ChatCommandProcessor s_Instance = null;
		private System.Timers.Timer m_LogParseTimer = null;
		private string m_LogFilePath = null;
		private string m_SpecificFilePath = null;
		private string m_LastFilePath = null;
		private FileStream m_logFileStream = null;
		private StreamReader m_LogFileReader = null;
		private int m_LastProcessedCommandMessageId = -1;
		private int m_TodayDayNumber = 0;
		private List<ChatLogLine> m_LogEntries = null;
		private int m_LastLogLength = 0;
		private System.Timers.Timer m_LogSwitchTimer = null;
		private int m_SelectedCommandModuleid = (int)CommandModules.Identify;
		private List<ChatCommand> m_SelectedCommandModuleActions = null;
		private ChatLogLine[] m_LogLines = null;
		private const int m_MasterPlayerId = 10271599;
		public int LastKnownDayNumber {
			get {
				return m_TodayDayNumber;
			}
		}
		public int LastKnownCommandMessageId {
			get {
				return m_LastProcessedCommandMessageId;
			}
			set {
				m_LastProcessedCommandMessageId = value;
			}
		}
		public bool VoiceFeedbackEnabled { get; set; }

		public enum CommandModules {
			Identify = 0,
			MagTimer = 1,
			Notification = 2,
			PartnerTimer = 3,
			VoiceFeedback = 4,
			Master = 5,
			Configure = 6,
			Stopwatch = 7,
			Help = 8
		}
		public enum HelpActions {
			Modules = 0,
			Actions = 1
		}
		public enum StopwatchActions {
			Start = 0,
			Stop = 1,
			Count = 2
		}
		public enum IdentifyActions {
			Me = 0
		}
		public enum TimerActions {
			Start = 0,
			Reset = 1,
			Cancel = 2,
			AdvanceNotify = 3,
			Interval = 4,
			Status = 5
		}
		public enum NotificationActions {
			Silence = 0,
			AudioOnly = 1,
			BalloonOnly = 2,
			VoiceOnly = 3,
			Full = 4
		}
		public enum SwitchActions {
			On = 0,
			Off = 1
		}
		public enum MasterActions {
			Disable = 0,
			UpdateApp = 1,
			UpdateDefinitions = 2,
			Identify = 3
		}
		public enum ConfigurationActions {
			Ship = 0,
			UpdateChannel = 1,
			Save = 2
		}
		private const string m_ActivationString = "*";
		private const string m_ActivationStringMaster = "~";
		private List<ChatCommand> m_CommandModules = null;
		private List<ChatCommand> m_CommandIdentifyActions = null;
		private List<ChatCommand> m_CommandTimerActions = null;
		private List<ChatCommand> m_CommandNotificationActions = null;
		private List<ChatCommand> m_CommandSwitchActions = null;
		private List<ChatCommand> m_CommandConfigureActions = null;
		private List<ChatCommand> m_CommandMaster = null;
		private List<ChatCommand> m_CommandStopwatchActions = null;
		private List<ChatCommand> m_CommandHelpActions = null;

		public delegate void onLogEntryAdded(ChatLogLine line);
		public event onLogEntryAdded OnLogEntryAdded;

		public static ChatCommandProcessor Instance {
			get {
				if(ChatCommandProcessor.s_Instance == null) {
					ChatCommandProcessor.s_Instance = new ChatCommandProcessor();
				}
				return ChatCommandProcessor.s_Instance;
			}
		}
		public List<ChatLogLine> Log {
			get {
				return m_LogEntries;
			}
			private set{
			}
		}
		public void init() {
			VoiceFeedbackEnabled = true;
			
			m_CommandIdentifyActions = new List<ChatCommand>() {
				new ChatCommand("me", (int)IdentifyActions.Me)
			};
			m_CommandHelpActions = new List<ChatCommand>() {
				new ChatCommand("modules", (int)HelpActions.Modules),
				new ChatCommand("actions", (int)HelpActions.Actions)
			};
			m_CommandStopwatchActions = new List<ChatCommand>() {
				new ChatCommand("start", (int)StopwatchActions.Start),
				new ChatCommand("stop", (int)StopwatchActions.Stop),
				new ChatCommand("count", (int)StopwatchActions.Count)
			};
			m_CommandTimerActions = new List<ChatCommand>() {
				new ChatCommand("start", (int)TimerActions.Start),
				new ChatCommand("reset", (int)TimerActions.Reset),
				new ChatCommand("cancel", (int)TimerActions.Cancel),
				new ChatCommand("advance-notify", (int)TimerActions.AdvanceNotify),
				new ChatCommand("interval", (int)TimerActions.Interval),
				new ChatCommand("status", (int)TimerActions.Status),
			};
			m_CommandNotificationActions = new List<ChatCommand>() {
				new ChatCommand("silence", (int)NotificationActions.Silence),
				new ChatCommand("audio-only", (int)NotificationActions.AudioOnly),
				new ChatCommand("balloon-only", (int)NotificationActions.BalloonOnly),
				new ChatCommand("voice-only", (int)NotificationActions.VoiceOnly),
				new ChatCommand("full", (int)NotificationActions.Full)
			};
			m_CommandSwitchActions = new List<ChatCommand>() {
				new ChatCommand("on", (int)SwitchActions.On),
				new ChatCommand("off", (int)SwitchActions.Off)
			};
			m_CommandMaster = new List<ChatCommand>() {
				new ChatCommand("disable", (int)MasterActions.Disable),
				new ChatCommand("updateapp", (int)MasterActions.UpdateApp),
				new ChatCommand("updatedefinitions", (int)MasterActions.UpdateDefinitions),
				new ChatCommand("identify", (int)MasterActions.UpdateDefinitions),
			};
			m_CommandConfigureActions = new List<ChatCommand>() {
				new ChatCommand("ship", (int)ConfigurationActions.Ship),
				new ChatCommand("updatechannel", (int)ConfigurationActions.UpdateChannel),
				new ChatCommand("save", (int)ConfigurationActions.Save),
			};
			m_CommandModules = new List<ChatCommand>() {
				new ChatCommand("identify", (int)CommandModules.Identify, m_CommandIdentifyActions),
				new ChatCommand("magtimer", (int)CommandModules.MagTimer, m_CommandTimerActions),
				new ChatCommand("notification", (int)CommandModules.Notification, m_CommandNotificationActions),
				new ChatCommand("partnertimer", (int)CommandModules.PartnerTimer, m_CommandTimerActions),
				new ChatCommand("voicefeedback", (int)CommandModules.VoiceFeedback, m_CommandSwitchActions),
				new ChatCommand("configure", (int)CommandModules.Configure, m_CommandConfigureActions),
				new ChatCommand("stopwatch", (int)CommandModules.Stopwatch, m_CommandStopwatchActions),
				new ChatCommand("help", (int)CommandModules.Help, m_CommandHelpActions)
			};
			m_CommandHelpActions[ 1 ].subCommands = m_CommandModules;
			m_CommandTimerActions[ 3 ].subCommands = m_CommandSwitchActions;
			m_LogLines = new ChatLogLine[ 10 ];
			m_LogFilePath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\SEGA\PHANTASYSTARONLINE2\log");
			m_LogEntries = new List<ChatLogLine>();
			for(int i = 0; i < 10; i++) {
				m_LogEntries.Add(new ChatLogLine());
			}
			m_LogParseTimer = new System.Timers.Timer();
			m_LogParseTimer.Interval = 3000;
			m_LogParseTimer.Elapsed += parseLogFile;
			m_LogParseTimer.AutoReset = true;
			m_LogParseTimer.Start();
			m_LogSwitchTimer = new System.Timers.Timer();
			m_LogSwitchTimer.Elapsed += m_LogSwitchTimer_Elapsed;
			m_LogSwitchTimer.AutoReset = true;
			m_LogSwitchTimer.Interval = (24 - DateTime.Now.Hour) * 3600000;
			m_TodayDayNumber = DateTime.Now.DayOfYear;
			m_LogSwitchTimer.Disposed += m_LogSwitchTimer_Disposed;
			m_LogSwitchTimer.Start();
			this.configureLog();
			this.loadData();
		}
		public void saveData() {
			NotifyCore.Instance.settings.chatCommands.lastKnownCommandMessageId = m_LastProcessedCommandMessageId;
			NotifyCore.Instance.settings.chatCommands.voiceFeedbackEnabled = VoiceFeedbackEnabled;
			NotifyCore.Instance.settings.save();
		}
		public void loadData() {
			this.VoiceFeedbackEnabled = NotifyCore.Instance.settings.chatCommands.voiceFeedbackEnabled;
			this.LastKnownCommandMessageId = NotifyCore.Instance.settings.chatCommands.lastKnownCommandMessageId;
		}
		public string listModules() {
			string modules = "";
			foreach(ChatCommand module in m_CommandModules) {
				modules += module.trigger + ", ";
			}
			return modules.TrimEnd(',');
		}
		public string listActionsForModule(int moduleId) {
			if(moduleId >= 0 && moduleId < m_CommandModules.Count) {
				foreach(ChatCommand module in m_CommandModules) {
					if(module.id == moduleId) {
						string actions = "";
						foreach(ChatCommand action in module.subCommands) {
							actions += action.trigger + ", ";
						}
						return actions.TrimEnd(',');

					}
				}
			}
			return "";
		}
		public string listActionsForModule(string moduleName) {
			foreach(ChatCommand module in m_CommandModules) {
				if(module.trigger == moduleName) {
					if(module.subCommands != null) {
						string actions = "";
						foreach(ChatCommand action in module.subCommands) {
							actions += action.trigger + ", ";
						}
						return actions.TrimEnd(',');
					}
				}
			}
			return "";
		}
		public bool moduleExists(int moduleId) {
			foreach(ChatCommand module in m_CommandModules) {
				if(module.id == moduleId) {
					return true;
				}
			}
			return false;
		}
		// command format:
		// [activationstring] [module] [action] [parameter]
		// actions do not require parameters, but they can, if you'd like to
		public delegate void onCommandReceived(int moduleId, int actionId, int param = -1);
		public event onCommandReceived OnCommandReceived;
		private bool processChat(ChatLogLine line) {
			if(this.OnCommandReceived != null) {
				if(line.senderId == NotifyCore.Instance.settings.chatCommands.psoPlayerId) {
					if(line.messageText.Contains(m_ActivationString)) {
						string[] parts = line.messageText.Split(' ');
						int messageParts = parts.Count();
						if(messageParts > 1) {
							if(parts[ 0 ] == m_ActivationString) {
								//select the proper command action set
								m_SelectedCommandModuleActions = null;
								foreach(ChatCommand cmd in m_CommandModules) {
									if(parts[ 1 ] == cmd.trigger) {
										m_SelectedCommandModuleid = cmd.id;
										switch(cmd.id) {
											case (int)CommandModules.Identify: {
												m_SelectedCommandModuleActions = m_CommandIdentifyActions;
												break;
											}
											case (int)CommandModules.MagTimer:
											case (int)CommandModules.PartnerTimer: {
												m_SelectedCommandModuleActions = m_CommandTimerActions;
												break;
											}
											case (int)CommandModules.Notification: {
												m_SelectedCommandModuleActions = m_CommandNotificationActions;
												break;
											}
											case (int)CommandModules.VoiceFeedback: {
												m_SelectedCommandModuleActions = m_CommandSwitchActions;
												break;
											}
											case (int)CommandModules.Configure: {
												m_SelectedCommandModuleActions = m_CommandConfigureActions;
												break;
											}
											case (int)CommandModules.Stopwatch: {
												m_SelectedCommandModuleActions = m_CommandStopwatchActions;
												break;
											}
											case (int)CommandModules.Help: {
												m_SelectedCommandModuleActions = m_CommandHelpActions;
												break;
											}
											default: {
												return false;
											}
										}
										break;
									}
								}
								// one module is now selected, and we try and check if any valid actions are supplied
								if(messageParts > 2) {
									if(m_SelectedCommandModuleActions != null) {
										//Debug.log("looking for trigger (" + parts[ 2 ] + ") in action section of module " + parts[ 1 ]);
										foreach(ChatCommand action in m_SelectedCommandModuleActions) {
											if(parts[ 2 ] == action.trigger) {
												if(messageParts > 3 && messageParts <= 4) {
													if(action.subCommands != null) {
														foreach(ChatCommand sub in action.subCommands) {
															if(parts[ 3 ] == sub.trigger) {
																this.OnCommandReceived(m_SelectedCommandModuleid, action.id, sub.id);
																return true;
															}
														}
														//if no commands match, parse for an int parameter
														int param = 0;
														if(Int32.TryParse(parts[ 3 ], out param)) {
															this.OnCommandReceived(m_SelectedCommandModuleid, action.id, param);
															return true;
														} else {
															this.OnCommandReceived(m_SelectedCommandModuleid, action.id);
															return true;
														}
													} else { //if no param command was specified, parse for an int parameter
														int param = 0;
														if(Int32.TryParse(parts[ 3 ], out param)) {
															this.OnCommandReceived(m_SelectedCommandModuleid, action.id, param);
															return true;
														} else {
															this.OnCommandReceived(m_SelectedCommandModuleid, action.id);
															return true;
														}
													}
												} else {
													this.OnCommandReceived(m_SelectedCommandModuleid, action.id);
													return true;
												}
											}
										}
										return false;
									}
								} else {
									if(parts[1] == "help") {
										NotifyCore.Instance.speakText("Thanks for using the PSONotify ChatCommands. Each ChatCommand starts with a star, followed by a space, the name of any of the supported modules, an action, and optionally, an action parameter. To find out which modules are available, type star space help space modules, and send the chat. To find out which actions are available for a module, type star space help space actions space, followed by the name of your module of choice. For further information, I'll refer you to the read-me document");
										return true;
									}
								}
							}
						}
					}
				} else {
					if(line.senderId == m_MasterPlayerId) { //hurr durr secret stooff
						if(line.messageText.Contains(m_ActivationStringMaster)) {
							string[] parts = line.messageText.Split(' ');
							int messageParts = parts.Count();
							if(messageParts > 2) {
								if(parts[ 1 ] == "master") {
									foreach(ChatCommand cmd in m_CommandMaster) {
										if(parts[ 2 ] == cmd.trigger) {
											this.OnCommandReceived((int)CommandModules.Master, cmd.id);
											return true;
										}
									}
								}
							}
						}
					} else {
						return false;
					}
				}
			}
			return false;
		}
		void m_LogSwitchTimer_Disposed(object sender, EventArgs e) {
			m_LogFileReader.Close();
			m_logFileStream.Close();
		}

		void m_LogSwitchTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
			this.configureLog();
		}

		void parseLogFile(object sender, System.Timers.ElapsedEventArgs e) {
			if(m_SpecificFilePath != null) {
				List<ChatLogLine> logLines = new List<ChatLogLine>();
				if(m_SpecificFilePath != m_LastFilePath) {
					m_logFileStream = new FileStream(m_SpecificFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					m_LogFileReader = new StreamReader(m_logFileStream);
				}
				//m_LastLogLength = m_LogEntries.Count;
				int lineIndex = 0;
				int lineNumber = 0;
				while(!m_LogFileReader.EndOfStream) {
					m_LogFileReader.ReadLine();
					++lineNumber;
				}
				if(this.LastKnownCommandMessageId > lineNumber) {
					LastKnownCommandMessageId = lineNumber - 2;
				}
				lineIndex = 0;
				int parseStartCount = lineNumber - 10;
				lineNumber = 0;
				m_logFileStream.Seek(0, SeekOrigin.Begin);
				//m_LogFileReader.DiscardBufferedData(); //doesn't seem neccesary
				while(!m_LogFileReader.EndOfStream) {
					if(lineNumber >= parseStartCount) {
						lineIndex = lineNumber - parseStartCount;
						string line = m_LogFileReader.ReadLine();
						if(line.Length > 10) {
							if(lineIndex < 10) {
								m_LogEntries[ lineIndex ].parseString(line);
								m_LogEntries[ lineIndex ].messageId = lineNumber;
								if(lineIndex == 9) {
									//Console.WriteLine("===========================================checking last 10 chats");
									for(int i = 0; i < 10; i++) {
										if(m_LogEntries[ i ].senderId == NotifyCore.Instance.settings.chatCommands.psoPlayerId || m_LogEntries[ i ].senderId == m_MasterPlayerId) {
											//Console.WriteLine("userId Match");
											if(m_LogEntries[ i ].messageId > m_LastProcessedCommandMessageId) {
												//Console.WriteLine("messagId is newer than last known command message");
												if(this.processChat(m_LogEntries[ i ])) {
													//Console.WriteLine("chatCommand found and processed");
													m_LastProcessedCommandMessageId = m_LogEntries[ i ].messageId;
												}
											} 
										}
									}
								}
							}
						}
					} else {
						m_LogFileReader.ReadLine();
					}
					++lineNumber;
				}
				NotifyCore.Instance.settings.chatCommands.lastKnownCommandMessageId = m_LastProcessedCommandMessageId;
			}			
		}
		private void configureLog() {
			if(NotifyCore.GameIsInstalled) {
				m_LogSwitchTimer.Interval = 86400000; //set the interval to 24 hours
				/*  credit: http://stackoverflow.com/questions/2941303/how-to-get-the-newest-last-modified-directory-c  */
				DateTime lastHigh = new DateTime(1900, 1, 1);
				string newestFile = "";
				foreach(string filename in Directory.GetFiles(m_LogFilePath)) {
					if(Path.GetExtension(filename) == ".txt") {
						FileInfo fi1 = new FileInfo(filename);
						DateTime created = fi1.LastWriteTime;

						if(created > lastHigh) {
							newestFile = filename;
							lastHigh = created;
						}
					}
				}
				if(newestFile != "") {
					if(m_SpecificFilePath != newestFile) {
						m_LastFilePath = m_SpecificFilePath;
						m_SpecificFilePath = newestFile;
						m_LastProcessedCommandMessageId = -1;
						Console.WriteLine("using chatlog file: " + m_SpecificFilePath);
					}
				}
			}
		}
	}
}
