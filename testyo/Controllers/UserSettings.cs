using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace PSONotify {
	public class UserSettings {
		public int ship { get; set; }
		public bool userSavedSettings { get; set; }
		public bool openAtLogin { get; set; }
		public List<PSONotify.Models.Notification> notificationSettings { get; set; }
		public string updateChannel { get; set; }
		public bool disableLogging { get; set; }
		public int version { get; set; }
		public bool checkForUpdates { get; set; }
		public int notificationRepetitionPolicy { get; set; }
		public ChatCommandsSettings chatCommands { get; set; }
		public TimerSettings magTimer { get; set; }
		public TimerSettings partnerTimer { get; set; }
		public bool disableWordCensor { get; set; }
		public const int NotifySettingEmergencyQuest = 0;
		public const int NotifySettingMag = 1;
		public const int NotifySettingPartner = 2;
		public string guid {get;set;}

		public UserSettings() {
			ship = 2;
			userSavedSettings = false;
			openAtLogin = false;
			updateChannel = "release";
			disableLogging = false;
			version = 0;
			checkForUpdates = true;
			notificationRepetitionPolicy = 3;
			chatCommands = new ChatCommandsSettings();
			magTimer = new TimerSettings();
			partnerTimer = new TimerSettings();
			disableWordCensor = false;
			guid = NotifyCore.generateFingerprint();
		}

		public static UserSettings FromJsonString(string jsonString) {
			JObject jsonData = null;
			try {
				jsonData = JObject.Parse(jsonString);
			} catch {
				Debugger.Log(0, null, "UserSettings FromJsonString failed: malformed data");
				return null;
			}
			UserSettings settings = jsonData.ToObject<UserSettings>();
			Debugger.Log(0, null, "UserSettings Loaded from string");

			return settings;
		}
		public static UserSettings FromJsonFile(string file) {
			if(File.Exists(file)) {
				string jsonString = File.ReadAllText(file, Encoding.UTF8);
				Debugger.Log(0, null, "UserSettings Loaded from file");
				return UserSettings.FromJsonString(jsonString);
			}
			Debugger.Log(0, null, "UserSettings FromJsonFile failed: file does not exist @ " + file);
			return null;
		}
		public void save() {
			Debugger.Log(0, null, "UserSettings Saved to file");
			JObject jsonData = (JObject)JToken.FromObject(this);
			File.WriteAllText(Path.Combine(NotifyCore.AppDataFolder, "settings.json"), jsonData.ToString());
		}
	}
}
