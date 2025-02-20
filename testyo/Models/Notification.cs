using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSONotify.Models {
	public class Notification {
		public delegate void notifyDelegate(Notification sender);
		public event notifyDelegate OnTrigger;
		public event notifyDelegate AdvanceNotify;
		public Notification() {
			soundEffectId = 0;
			intervalMin = -1;
			elapsedMin = -1;
			enabled = false;
			autoResetsAfterElapse = true;
		}

		public int style {
			get;
			set;
		}
		public string name {
			get;
			set;
		}
		public int soundEffectId {
			get;
			set;
		}
		//at how many minutes after startMinutes the notification should be triggered
		public int intervalMin {
			get;
			set;
		}
		public int preNotifyMin {
			get;
			set;
		}
		public bool willObaySilenceWhilePlaying {
			get;
			set;
		}
		public int elapsedMin {
			get;
			private set;
		}
		public bool autoResetsAfterElapse {
			get;
			set;
		}
		public bool enabled {
			get;
			set;
		}
		//checks if the notification should be triggered, and fires it's OnTrigger event when it should
		public void check() {
			if(this.intervalMin > 0 && this.elapsedMin >= 0) {
				if(this.elapsedMin > this.intervalMin) {
					this.elapsedMin = -1;
					this.intervalMin = -1;
					return;
				}
				if(this.elapsedMin == this.preNotifyMin) {
					if(this.AdvanceNotify != null) {
						this.AdvanceNotify(this);
					}
				}
				if(this.elapsedMin == this.intervalMin) {
					if(this.OnTrigger != null) {
						this.OnTrigger(this);
					}
					if(autoResetsAfterElapse) {
						elapsedMin = 0;
					}
					return;
				}
				++elapsedMin;
			}
		}
	}
}
