using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Diagnostics;

namespace PSONotify {
	public partial class NotifyCore {
		private static NotifyCore s_SingletonInstance = null;
		private static Uri s_CloudUri = null;
		private UserSettings m_UserSettings = null;
		public MagTimer magTimer = null;
		private BackgroundWorker m_ThreadedJob = null;

		public delegate void QuestNotifyDelegate(QuestDefinition quest);

		public event QuestNotifyDelegate OnQuestNotification;
		//defines
		private const string URL_TWITTER = "https://twitter.com/pso2_emg_bot";
		private const string USERAGENTSTRING = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_4) AppleWebKit/536.30.1 (KHTML, like Gecko) Version/6.0.5 Safari/536.30.1";
		public const int TIME_SECOND = 1000;
		public const int TIME_MINUTE = TIME_SECOND * 60;
		public const int TIME_HOUR = TIME_MINUTE * 60;
		public static string PSOWORDCENSORFILEPATH = @"\SEGA\PHANTASYSTARONLINE2\pso2_bin\data\win32\ffbff2ac5b7a7948961212cefd4d402c";
		public const int NOTIFICATIONPOLICY_1MIN = 0;
		public const int NOTIFICATIONPOLICY_2MIN = 1;
		public const int NOTIFICATIONPOLICY_5MIN = 2;
		public const int NOTIFICATIONPOLICY_NEVER = 3;
		public const int NOTIFYSTYLE_BUBBLE_ONLY = 0;
		public const int NOTIFYSTYLE_AUDIO_ONLY = 1;
		public const int NOTIFYSTYLE_EVERYTHING = 2;
		public const int NOTIFYSTYLE_ICON_ONLY = 3;
		public const int ANNOUNCEMODE_NOTSET = -1;
		public const int ANNOUNCEMODE_QUEST = 0;
		public const int ANNOUNCEMODE_INTERRUPTRANKING = 1;
		public const int ANNOUNCEMODE_LIVEEVENT = 2;
		private const int SETTINGSVERSION = 1;
		//event/delegates
		//properties
		public string AppPath {
			get {
				return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			}
		}
		//public bool UserAllowsNotify {
		//	get {
		//		if(this.m_SilenceNotificationsWhileGameIsRunning) {
		//			if(!GameIsRunning) {
		//				return true;
		//			}
		//		} else {
		//			return true;
		//		}
		//		return false;
		//	}
		//}
		public UserSettings settings {
			get { 
				if(this.m_UserSettings == null) {
					this.m_UserSettings = UserSettings.FromJsonFile(Path.Combine(NotifyCore.AppDataFolder, "settings.json"));
				}
				return this.m_UserSettings;
			}
			set {
				this.m_UserSettings = null;
				this.m_UserSettings = value;
			}
		}

		public static bool GameIsRunning {
			get {
				if(!IsRunningOnMono) {
					var pso2 = System.Diagnostics.Process.GetProcessesByName("pso2");
					if(pso2.Length > 0) {
						return true;
					}
				}
				return false;
			}
		}

		public static bool IsRunningOnMono {
			get {
				return Type.GetType("Mono.Runtime") != null;
			}
		}

		public static NotifyCore Instance {
			get {
				if(NotifyCore.s_SingletonInstance == null) {
					NotifyCore.s_SingletonInstance = new NotifyCore();
				}
				return NotifyCore.s_SingletonInstance;
			}
		}

		public static string AppDataFolder {
			get {
				string appDataFolder = "null";
				if(IsRunningOnMono) {
					appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Library/Application Support";
				} else {
					appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				}
				if(!Directory.Exists(Path.Combine(appDataFolder, "PSONotify"))) {
					Directory.CreateDirectory(Path.Combine(appDataFolder, "PSONotify"));
				}
				appDataFolder = Path.Combine(appDataFolder, "PSONotify");
				return appDataFolder;
			}
		}

		public static Uri CloudUrl {
			get {
				if(NotifyCore.s_CloudUri == null) {
					NotifyCore.s_CloudUri = new Uri("http://digitalhaze.eu/psonotify/", UriKind.Absolute);
				}
				return NotifyCore.s_CloudUri;
			}
		}

		public static bool GameIsInstalled {
			get {
				if(!IsRunningOnMono) {
					if(Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"SEGA\PHANTASYSTARONLINE2"))) {
						return true;
					}
				}
				return false;
			}
		}
		//member data
		private List<AreaDefinition> m_QuestDefinitions = null;

		public delegate void QuestDefinitionsDelegate(List<AreaDefinition> areas);

		public event QuestDefinitionsDelegate OnQuestDefinitionsInitialized;
		//ctor
		private NotifyCore() {
			this.m_ThreadedJob = new BackgroundWorker();
			this.m_ThreadedJob.DoWork += this.pollTwitterThreaded;
			this.m_ThreadedJob.RunWorkerCompleted += this.workCompleted;
			checkUserSettings();
			initPlatformSpecificCode();
			this.magTimer = new MagTimer();
		}
		//methods
		partial void openAtLogin(bool enabled);

		partial void balloonNotify(string title, string message);

		partial void audioNotify();

		partial void checkForUpdates();

		partial void platformSpecificSpeakText(string text);

		public void speakText(string text) {
			this.platformSpecificSpeakText(text);
		}

		partial void initPlatformSpecificCode();

		void checkAndUpgradeSettings() {

		}

		public void checkForQuests(bool userTriggered = false) {
			QuestDefinition definition = new QuestDefinition();
			definition.ShipId = settings.ship;
			definition.PollWasUserTriggered = userTriggered;
			this.m_ThreadedJob.RunWorkerAsync(definition);
		}

		public static string generateFingerprint() {
			return Guid.NewGuid().ToString("N");
		}

		public void checkQuestDefinitions() {
			WebClient wc = new WebClient();
			wc.DownloadStringCompleted += ProcessQuestNameDefinitions;
			wc.DownloadStringAsync(new Uri(NotifyCore.CloudUrl, "quests.json"));
		}

		private void ProcessQuestNameDefinitions(object sender, DownloadStringCompletedEventArgs e) {
			//fetch the local quest definitions version, if any, or set up the folder structure if nothing exists yet
			int localDefinitionsVersion = -1;
			
			//psonotify data folder exists at this point
			var definitionsFile = Path.Combine(NotifyCore.AppDataFolder, "questdefinitions.json");
			//try and fetch the local definitions version. But no biggie if it doesn't exist yet
			JObject localQuestAreasDefinitions = null;
			if(File.Exists(definitionsFile)) {
				string jsonDefinitionsData = File.ReadAllText(definitionsFile);
				localQuestAreasDefinitions = JObject.Parse(jsonDefinitionsData);
				localDefinitionsVersion = (int)localQuestAreasDefinitions.SelectToken("version");
			}
			JObject remoteQuestAreasDefinitions = JObject.Parse(e.Result);
			int remoteDefinitionsVersion = (int)remoteQuestAreasDefinitions.SelectToken("version");
			Debugger.Log(0, null, "Server definitions version: " + remoteDefinitionsVersion + ", mine: " + localDefinitionsVersion + "\n");
			if(remoteDefinitionsVersion > localDefinitionsVersion) {
				File.WriteAllText(definitionsFile, e.Result, Encoding.Unicode);
				localQuestAreasDefinitions = null;
				localQuestAreasDefinitions = remoteQuestAreasDefinitions;
				//quest definitions should now always be up to date
			} else {
				remoteQuestAreasDefinitions = null;
			}
			JArray quests = (JArray)localQuestAreasDefinitions[ "quests" ];
			this.m_QuestDefinitions = quests.ToObject<List<AreaDefinition>>();
			if(this.OnQuestDefinitionsInitialized != null) {
				this.OnQuestDefinitionsInitialized(this.m_QuestDefinitions);
			}
		}

		private void checkUserSettings() {
			string settingsFile = Path.Combine(NotifyCore.AppDataFolder, "settings.json");
			if(!File.Exists(settingsFile)) {
				WebClient wc = new WebClient();
				wc.DownloadFileCompleted += userSettingsDownloaded;
				wc.DownloadFileAsync(new Uri(NotifyCore.CloudUrl, "settings.json"), settingsFile);
			} else {
				restoreUserSettings();
			}
		}

		private void userSettingsDownloaded(object sender, AsyncCompletedEventArgs e) {
			restoreUserSettings();
		}

		private void restoreUserSettings() {
			flushSettings();
			//make use of some bs so the settings will be auto-loaded from disk
			bool userSaved = this.settings.userSavedSettings;
		}

		public void flushSettings() {
			this.m_UserSettings = null;
		}

		public delegate void DelegateQuestNothingToReport();

		public event DelegateQuestNothingToReport OnQuestPollNothingToReport;

		private void workCompleted(object sender, RunWorkerCompletedEventArgs e) {
			QuestDefinition args = e.Result as QuestDefinition;
			if(args != null) {
				if(!args.NotFound) {
					if(this.OnQuestNotification != null) {
						this.OnQuestNotification(args);
					}
				} else {
					if(this.OnQuestPollNothingToReport != null) {
						this.OnQuestPollNothingToReport();
					}
				}
			}
		}

		private void pollTwitterThreaded(object sender, DoWorkEventArgs e) {
				QuestDefinition args = e.Argument as QuestDefinition;
				//debug.log("Performing Twitter check for EQ's on Ship " + NotifyCore.Instance.settings.ship + " on a new thread.. UTCTime is: " + DateTime.UtcNow.ToShortTimeString());
	
				HtmlWeb page = new HtmlWeb();
				page.AutoDetectEncoding = true;
				page.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_4) AppleWebKit/536.30.1 (KHTML, like Gecko) Version/6.0.5 Safari/536.30.1";
				HtmlAgilityPack.HtmlDocument doc = null;
				try {
					//debug.log("Attempting to load tweets from " + TWITTER_URL);
	
					doc = page.Load(URL_TWITTER) as HtmlAgilityPack.HtmlDocument;
				} catch {
					//debug.log("No network connection available");
					Debugger.Log(0, null, "No network connection available\n");
					doc = null;
				}
				if(doc != null) {
					//debug.log("Extracting data");
					HtmlNode tweetsElements = doc.GetElementbyId("stream-items-id") as HtmlNode;
					if(tweetsElements != null) {
						var nodes = tweetsElements.SelectNodes("li");
						var times = tweetsElements.SelectNodes(".//*[contains(@class, 'js-relative-timestamp')]");
	
						if(nodes.Count > 0) {
							//extra check to guarantee that even if the nodecount != the amount of relative times found, we only parse as many tweets as the lower number of the two
							//it a tweet doesn't have a relative date, then it's too old anyhow!
							int runCount = 0;
							if(nodes.Count <= times.Count) {
								runCount = nodes.Count;
							} else {
								runCount = times.Count;
							}
	
							int nodeCounter = 0;
							foreach(HtmlNode node in nodes) {
								if(nodeCounter <= runCount) {
									if(times[ nodeCounter ].InnerText.Contains('m') || times[ nodeCounter ].InnerText.Contains('s')) {
										//debug.log("tweet is " + times[ nodeCounter ].InnerText + " old");
										var tweetTextNode = node.SelectSingleNode(".//p");
										string tweetText = "";
										if(tweetTextNode != null) {
											tweetText = tweetTextNode.InnerHtml;
											//debug.log("processing tweet " + nodeCounter);
											string shortString = "x";
											string startTime = "x";
											string durationMinutes = "x";
											//debug.log("detecting type of announcement (All ships/Ships-specific), and which ship");
	
											//debug.log("extracting announcement type");
											args.Mode = ANNOUNCEMODE_NOTSET;
											if(tweetText.Contains("【インタラプトランキング】")) {
												args.Mode = ANNOUNCEMODE_INTERRUPTRANKING;
											} else if(tweetText.Contains("【緊急クエスト】")) {
												args.Mode = ANNOUNCEMODE_QUEST;
											} else if(tweetText.Contains("【ライブイベント】")) {
												args.Mode = ANNOUNCEMODE_LIVEEVENT;
											} else {
												continue;
											}
	
											switch(args.Mode) {
												case ANNOUNCEMODE_LIVEEVENT:
													{
														//debug.log("Live Event");
														args.AreaName = "A Special Live Event!";
														break;
													}
												case ANNOUNCEMODE_INTERRUPTRANKING:
													{
														//debug.log("Interrupt Ranking");
														args.AreaName = "Interrupt Ranking";
														break;
													}
												case ANNOUNCEMODE_QUEST:
												default:
													{
														//debug.log("Emergency Quest");
														//name will be set later
														break;
													}
											}
	
											int ship = 999;
											if(Int32.TryParse(tweetText.Substring(12, 2), out ship)) {
												//debug.log("This tweet's ship is:" + ship);
												if(ship == NotifyCore.Instance.settings.ship) {
													shortString = tweetText.Substring(18);
													startTime = shortString.Substring(0, shortString.IndexOf(':'));
													durationMinutes = shortString.Substring(shortString.IndexOf('～') + 1).Substring(shortString.IndexOf(':') + 1).Substring(0, 2);
												} else {
													//debug.log("This aint my ship, next tweet!");
													++nodeCounter;
													continue;
												}
											} else {
												if(node.FirstChild.InnerText.Contains("全Ship")) { //general all-ship quest announcement
													//debug.log("All ships");
													shortString = tweetText.Substring(17);
													startTime = shortString.Substring(0, shortString.IndexOf(':'));
													durationMinutes = shortString.Substring(shortString.IndexOf('～') + 1).Substring(shortString.IndexOf(':') + 1).Substring(0, 2);
												} else {
													//debug.log(string.Format("This aint my ship (ship {0}), next tweet!", ship));
													++nodeCounter;
													continue;
												}
											}
	
											//debug.log("Extracting time and duration");
											DateTime JstTime = DateTime.UtcNow.AddHours(9);
											//debug.log("Current Time in JST: " + JstTime.ToShortTimeString() + ", StartTimeHours: " + startTime + ", Duration: " + durationMinutes + " minutes", Logger.DEBUG);
											int startHour = 0;
											if(Int32.TryParse(startTime, out startHour)) {
												if(Int32.TryParse(durationMinutes, out args.DurationInMinutes)) {
													args.IsActive = false;
													args.AnnouncementActive = false;
													if(startHour == JstTime.Hour) {
														if(JstTime.Minute < args.DurationInMinutes) {
															//debug.log("Holy shit a Quest/Event is running NOW! Duration is " + args.durationInMinutes + " minutes");
															args.IsActive = true;
															args.AnnouncementActive = false;
														}
													} else {
														if((startHour - 1) >= 0) {
															--startHour;
														} else {
															startHour = 23;
														}
														if(JstTime.Hour == startHour) {
															//debug.log("Sweet, a Quest/Event is coming up! Duration is " + args.durationInMinutes + " minutes");
															args.IsActive = false;
															args.AnnouncementActive = true;
														}
													}
													//only bother checking for an eq name if an eq is actually announced
													if(args.IsActive || args.AnnouncementActive) {
														int areaNameStartIndex = shortString.IndexOf('（') + 1;
														if(areaNameStartIndex > 0) {
															int stringlength = shortString.IndexOf('）') - areaNameStartIndex;
															if(stringlength > -1) {
																args.AreaName = shortString.Substring(areaNameStartIndex, stringlength);
															} else {
																args.AreaName = "Unknown/Multiple";
																//debug.log("Area name was not defined by the twitter bot, what the hell?");
															}
														} else {
															args.AreaName = "Unknown Area/Multiple Areas";
															//debug.log("Area name was not defined by the twitter bot. Nice... Generic announcements..");
														}
														e.Result = args;
														return;
													}
													//debug.log("Nothing of interest.. sorry!");
												} else {
													args.NotFound = true;
													//debug.log("Tweet doesn't contain any duration, discarded");
												}
											} else {
												args.NotFound = true;
												//debug.log("Tweet doesn't contain any time, discarded");
											}
											e.Result = args;
											return;
										}
										args.NotFound = true;
										//debug.log("Tweet is malformed, discarded");
									}
									args.NotFound = true;
									//debug.log(string.Format("Tweet is too old ({0}), discarded", times[ nodeCounter ].InnerText));
								}
	
							}
	
							if(nodeCounter > nodes.Count) {
								args.NotFound = true;
								//debug.log("no relevant information found in last 20 tweets");
							}
						} else {
							//debug.log("twitter data polling: tweet elements were not found", Logger.ERROR);
						}
					} else {
						//debug.log("twitter data polling: element with id stream-items-id was not found", Logger.ERROR);
					}
				}
				e.Result = args;
			}

			//debug.log("Threaded eq polling ended");
			
		
	}
}
