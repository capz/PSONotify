using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSONotify {
	public class AreaDefinition {
		public string jp { get; set; }
		public string en { get; set; }

		public AreaDefinition(string j, string e) {
			this.jp = j;
			this.en = e;
		}
	}
}
