using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSONotify {
	public class TimerSettings {
		public int advanceNotify { get; set; }
		public int interval { get; set; }

		public TimerSettings() {
			advanceNotify = 2;
			interval = 40;
		}
	}
}
