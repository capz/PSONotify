using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace PSONotify {
	public partial class NotifyCore {
		private SpeechSynthesizer m_Speech = null;

		//properties
		public static string AppsFolder {
			get {
				if(Environment.Is64BitOperatingSystem) {
					return @"C:\Program Files (x86)";
				}
				return @"C:\Program Files";
			}
			set {

			}
		}
		public static bool IsRunningAsAdmin {
			get {
				WindowsIdentity identity = WindowsIdentity.GetCurrent();
				WindowsPrincipal principal = new WindowsPrincipal(identity);
				return principal.IsInRole(WindowsBuiltInRole.Administrator);
			}
		}

		//methods
		public static void OpenAtLogin(bool enabled) {
			if(NotifyCore.IsRunningAsAdmin) {
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
				if(enabled) {
					registryKey.SetValue("PSONotify", Application.ExecutablePath);
					//debug.log("Set PSONotify to open at login", Logger.DEBUG);
				} else {
					registryKey.DeleteValue("PSONotify");
					//debug.log("No longer opening PSONotify at login", Logger.DEBUG);
				}
				registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			} else {
				MessageBox.Show("Failed to register for launch at startup. Reason: insufficient permissions", "PSONotify");
			}
		}
		//partial void openAtLogin(bool enabled) {
		//	//debug.log("Open at Login: " + (enabled ? "Yes" : "No"));
		//	//RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
		//	//if(enabled) {
		//	//	registryKey.SetValue("PSONotify", Application.ExecutablePath);
		//	//} else {
		//	//	registryKey.DeleteValue("PSONotify");
		//	//}
		//	//registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
		//}
		partial void balloonNotify(string title, string message) {
			throw new NotImplementedException();
		}
		partial void audioNotify() {
			//if(UserAllowsNotify) {
			//	System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
			//	System.IO.Stream s = a.GetManifestResourceStream("PSONotify." + (this.m_UserSelectedAudioSfxIndex) + ".wav");
			//	System.Media.SoundPlayer p = new System.Media.SoundPlayer();
			//	p.Stream = s;
			//	p.LoadCompleted += delegate {
			//		p.Play();
			//		p.Dispose();
			//	};
			//	p.LoadAsync();
			//}
		}
		partial void checkForUpdates() {
			//make a call to our cool updater, Sparkle
			
		}
		partial void platformSpecificSpeakText(string text) {
			this.m_Speech.SpeakAsync(text);
		}
		partial void initPlatformSpecificCode() {
			this.m_Speech = new SpeechSynthesizer();
			this.m_Speech.SetOutputToDefaultAudioDevice();
			this.m_Speech.Rate = 1;
		}
	}
}
