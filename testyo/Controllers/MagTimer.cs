using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace PSONotify {
	public class MagTimer {
		private Timer m_Timer = null;
		private int m_ElapsedCount = 0;
		private int m_ElapsedTargetCount = 0;
		private int m_AdvanceNoticeMinutes = 0;
		private bool m_AutoReset = false;
		private const int Time_Second = 1000;
		private const int Time_Minute = Time_Second * 60;
		private const int Time_Hour = Time_Minute * 60;
		public delegate void callTime();
		public delegate void callAdvanceNotify(int minutesRemaining);
		public delegate void updateTime(int minutes);
		public event callTime FeedTime;
		public event callAdvanceNotify AdvanceNotify;
		public event updateTime UpdateUiTime;

		public MagTimer() {
			m_Timer = new Timer();
			m_Timer.Interval = Time_Minute;
			m_Timer.AutoReset = true;
			m_Timer.Elapsed += timerElapsed;
			m_ElapsedCount = 0;
			m_ElapsedTargetCount = 0;
			m_AdvanceNoticeMinutes = 5;
		}

		void timerElapsed(object sender, ElapsedEventArgs e) {
			if(m_ElapsedTargetCount > 0) {
				if(this.UpdateUiTime != null) {
					this.UpdateUiTime(this.PercentagePassed);
				}
				if(++m_ElapsedCount >= m_ElapsedTargetCount) {
					if(this.FeedTime != null) {
						this.FeedTime();
						if(m_AutoReset) {
							this.resetTimer();
						}
					}
					m_ElapsedCount = 0;
					return;
				} // the return above turns the code below into an else block
				if(m_AdvanceNoticeMinutes > 0) {
					if((m_ElapsedTargetCount - m_AdvanceNoticeMinutes) > 0) {
						if(m_ElapsedCount == (m_ElapsedTargetCount - m_AdvanceNoticeMinutes)) {
							if(this.AdvanceNotify != null) {
								this.AdvanceNotify(m_AdvanceNoticeMinutes);
							}
						}
					}
				}
			}
		}

		public void startTimer(int minutes, bool autoreset = false) {
			m_ElapsedTargetCount = minutes;
			m_ElapsedCount = 0;
			m_AutoReset = autoreset;
			m_Timer.Stop();
			m_Timer.Start();
		}
		public void resetTimer() {
			m_ElapsedCount = 0;
			m_Timer.Stop();
			m_Timer.Start();
		}
		public void stopTimer() {
			m_Timer.Stop();
			m_ElapsedCount = 0;
		}
		public int MinutesRemaining {
			get {
				if((m_ElapsedTargetCount - m_ElapsedCount) > 0) {
					return m_ElapsedTargetCount - m_ElapsedCount;
				} else {
					return 0;
				}
			}
		}
		public int MinutesRemainingAdvanceNotify {
			get { return (m_ElapsedTargetCount - m_AdvanceNoticeMinutes) - (m_ElapsedCount + 1); }
		}
		public int NotifyInterval {
			get { return m_ElapsedTargetCount; }
			set {
				m_ElapsedTargetCount = value;
			}
		}
		public int AdvanceNotifyInterval {
			get { return m_AdvanceNoticeMinutes; }
			set {
				m_AdvanceNoticeMinutes = value;
			}
		}
		public bool AutoResetOnceFeedTimeWasNotified {
			set { m_AutoReset = value; }
			get { return m_AutoReset; }
		}
		public int PercentagePassed {
			get {
				float percent = 100 / m_ElapsedTargetCount;
				float percentDone = m_ElapsedCount * percent;
				return (int)Math.Round(percentDone);
			}
		}
	}
}
