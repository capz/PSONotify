using System;
using System.Net;
using System.Net.Sockets;

namespace PSONotify {
	public class NtpClient {
		
		public static DateTime GetNetworkTime() {
			return GetNetworkTime("ntp.exnet.com");
		}

		public static DateTime GetNetworkTime(string ntpServer) {
			IPAddress[] address = null;
			address = Dns.GetHostEntry(ntpServer).AddressList;

			if(address == null || address.Length == 0) {
				throw new ArgumentException("Couldn not resolve Time Server " + ntpServer + ".", "ntpServer");
			}
			
			IPEndPoint ep = new IPEndPoint(address[0], 123);
			
			return GetNetworkTime(ep);
		}

		public static DateTime GetNetworkTime(EndPoint ep) {
			Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			
			s.Connect(ep);
			
			byte[] ntpData = new byte[48]; // RFC 2030 
			ntpData[0] = 0x1B;
			for(int i = 1; i < 48; i++)
				ntpData[i] = 0;
			
			s.Send(ntpData);
			s.Receive(ntpData);
			
			byte offsetTransmitTime = 40;
			ulong intpart = 0;
			ulong fractpart = 0;
			
			for(int i = 0; i <= 3; i++)
				intpart = 256 * intpart + ntpData[offsetTransmitTime + i];
			
			for(int i = 4; i <= 7; i++)
				fractpart = 256 * fractpart + ntpData[offsetTransmitTime + i];
			
			ulong milliseconds = (intpart * 1000 + (fractpart * 1000) / 0x100000000L);
			s.Close();
			
			TimeSpan timeSpan = TimeSpan.FromTicks((long)milliseconds * TimeSpan.TicksPerMillisecond);
			
			DateTime dateTime = new DateTime(1900, 1, 1);
			dateTime += timeSpan;
			
			TimeSpan offsetAmount = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);
			DateTime networkDateTime = (dateTime + offsetAmount);
			
			return networkDateTime;
		}
	}
}

