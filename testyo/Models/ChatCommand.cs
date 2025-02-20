using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSONotify {
	public class ChatCommand {
		public string trigger { get; set; }
		public int id { get; set; }
		public List<ChatCommand> subCommands = null;

		public ChatCommand(string t, int i) {
			trigger = t;
			id = i;
		}
		public ChatCommand(string t, int i, List<ChatCommand> cmd) {
			trigger = t;
			id = i;
			subCommands = cmd;
		}
	}
}
