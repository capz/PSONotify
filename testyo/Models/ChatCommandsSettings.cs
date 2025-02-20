using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSONotify {
	public class ChatCommandsSettings {
		public bool voiceFeedbackEnabled { get; set; }
		public int psoPlayerId { get; set; }
		public int lastKnownCommandMessageId { get; set; }
		public bool enableOnLaunch { get; set; }

		public ChatCommandsSettings() {
			voiceFeedbackEnabled = true;
			psoPlayerId = 0;
			lastKnownCommandMessageId = -1;
			enableOnLaunch = false;
		}
	}
}
