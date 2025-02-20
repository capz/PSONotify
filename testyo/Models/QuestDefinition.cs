using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSONotify {
	public class QuestDefinition {
		public int ShipId;
		public bool AnnouncementActive;
		public bool IsActive;
		public string AreaName;
		public int DurationInMinutes;
		public bool PollWasUserTriggered;
		public int Mode;
		const int QUESTMODE_EMERGENCY = 0;
		const int QUESTMODE_INTERRUPT = 1;
		const int QUESTMODE_LIVEEVENT = 2;
		const int QUESTMODE_NOTSET = -1;
		public bool NotFound;
		public QuestDefinition() {
			this.ShipId = 0;
			this.AnnouncementActive = false;
			this.IsActive = false;
			this.AreaName = "Undefined Area";
			this.DurationInMinutes = 0;
			this.PollWasUserTriggered = false;
			this.Mode = QUESTMODE_NOTSET;
			this.NotFound = false;
		}
	}
}
