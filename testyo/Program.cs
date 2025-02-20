using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSONotify
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			string path = Directory.GetCurrentDirectory();
			if(File.Exists(path + @"\payload.uud")) {
				//MessageBox.Show("Found Sparkle update");
				try {
					var processes = Process.GetProcessesByName("Sparkly");
					if(processes != null) {
						foreach(Process p in processes) {
							//MessageBox.Show("Process: " + p.ToString());
							p.Kill();
						}
					} 
				} catch {
					
				}
				try {
					var processes = Process.GetProcessesByName("Sparkle");
					if(processes != null) {
						foreach(Process p in processes) {
							//MessageBox.Show("Process: " + p.ToString());
							p.Kill();
						}
					}
				} catch {
					
				}
				try {
					var processes = Process.GetProcessesByName("Sparkle.exe");
					if(processes != null) {
						foreach(Process p in processes) {
							MessageBox.Show("Process: " + p.ToString());
							p.Kill();
						}
					}
				} catch {
					
				}
				//MessageBox.Show("Sparkle exited, now unpacking Sparkle update");
				File.Delete(path + @"\Sparkle.exe");
				File.Move(path + @"\payload.uud", path + @"\Sparkle.exe");
			}
			if(File.Exists(path + @"\data.7z")) {
				File.Delete(path + @"\data.7z");
			}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PreferencesWindow());
        }
    }
}
