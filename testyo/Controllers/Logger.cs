using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace PSONotify {
	public sealed class Logger: IDisposable {
		private StreamWriter m_FileStream = null;
		private int m_LoggingLevel = ERROR;
		public const int DEBUG = 1;
		public const int ERROR = 0;
		public const int INFO = 2;
		public bool LogToFile {
			get;
			set;
		}
		public bool Disabled { get; set; }

		public void log(string message, int lvl = INFO) {
			Debugger.Log(0, null, message + "\n");
			if(!this.Disabled) {
				if(LogToFile) {
					if(lvl >= this.m_LoggingLevel) {
						switch(lvl) {
							case DEBUG: {
								file.WriteLine("[Debug | " + DateTime.Now.ToShortTimeString() + "]: " + message);
								return;
							}
							case ERROR: {
								file.WriteLine("[Error | " + DateTime.Now.ToShortTimeString() + "]: " + message);
								return;
							}
							default: {
								file.WriteLine("[Info | " + DateTime.Now.ToShortTimeString() + "]: " + message);
								return;
							}
						}
					}
				}
			}
		}
		public Logger(bool toFile = true) {
			LogToFile = toFile;
		}
		private StreamWriter file {
			get {
				if(this.m_FileStream == null) {
					string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
					//System.Windows.Forms.MessageBox.Show("starting logging @ " + (directory + "\\log.txt"));
					try {
						this.m_FileStream = new StreamWriter(directory + "\\log.txt", false);
						this.m_FileStream.AutoFlush = true;
					} catch {
						System.Windows.Forms.MessageBox.Show("log file in use");
						Application.Exit();
					}
				}
				return this.m_FileStream;
			}
		}
		public int level {
			get { return this.m_LoggingLevel; }
			set { this.m_LoggingLevel = value; }
		}

		public void Dispose() {
			//this.m_FileStream.Dispose();
		}
	}
}
