using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PSONotify {
	public class ChatLogLine {
		private const int DATE = 0;
		private const int MESSAGE_ID = 1;
		private const int AUDIENCE = 2;
		private const int SENDER_ID = 3;
		private const int CHARACTER_NAME = 4;
		private const int MESSAGE_TEXT = 5;
		public string date {get;set;}
		public int messageId { get; set; }
		public string chatTargetAudience { get; set; }
		public int senderId { get; set; }
		public string senderCharacterName { get; set; }
		public string messageText {get;set;}

		public ChatLogLine() {
			
		}
		public void parseString(string line) {
			string[] lines = line.Split('\t');
			if(lines != null) {
				date = lines[ DATE ];
				messageId = Convert.ToInt32(lines[ MESSAGE_ID ]);
				chatTargetAudience = lines[ AUDIENCE ];
				senderId = Convert.ToInt32(lines[ SENDER_ID ]);
				senderCharacterName = lines[ CHARACTER_NAME ];
				messageText = lines[ MESSAGE_TEXT ];
				lines = null;
			}
		}
	}
}
