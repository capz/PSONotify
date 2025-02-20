using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace PSONotify.Controllers {
	public class NotificationController {
		//static data
		private static NotificationController m_Instance = null;

		//member data
		private bool m_InitializedOk = false;
		private System.Timers.Timer m_Timer = null;
		private List<Models.Notification> m_Notifications = null;

		//properties

		/** the .Instance property allows you to access the only available instance of this controller, 
		 *	and will create one if one doesn't exist yet. It will not allow you to set one */
		public static NotificationController Instance {
			get{
				if(NotificationController.m_Instance == null) {
					NotificationController.m_Instance = new NotificationController();
				}
				return NotificationController.m_Instance;
			}
			private set {

			}
		}

		//methods & constructor
		private NotificationController() {
			NotificationController.Instance.deserializeFromFile(NotifyCore.AppDataFolder + @"\notificationconfigurations.json");
			if(Instance.m_Notifications != null) { //continue initialization/startup
				if(Instance.m_Timer == null) {
					Instance.m_Timer = new System.Timers.Timer();
					Instance.m_Timer.AutoReset = true;
					Instance.m_Timer.Interval = 1000;
					Instance.m_Timer.Elapsed += Timer_Elapsed;
				}
			}
		}

		private void deserializeFromFile(string file) {
			if(File.Exists(file)) {
				string jsonData = File.ReadAllText(file);
				m_Notifications = JsonInstanceFactory<Models.NotificationSettingsInstance>.ToObjectInstance(jsonData).notifications;
			} else {
				System.Windows.Forms.MessageBox.Show("The notifications settings file was not found at [" + file + "]", "An error occurred");
			}
		}

		void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
			foreach(Models.Notification notification in m_Notifications) {
				notification.check();
			}
		}

		public Models.Notification getpolicyByName(string name) {
			foreach(Models.Notification notification in m_Notifications) {
				if(notification.name == name) {
					return notification;
				}
			}
			return null;
		}
	}
}
