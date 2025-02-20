using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

/*
	code by Leroy Ketelaars, (c) 2013
	adds an easy way to check if a property exists to json.net
*/


namespace PSONotify {
	class JsonExt {
		public static List<string> GetPropertyList(JObject json) {
			return json.Properties().Select(p => p.Name).ToList();
		}
		public static List<string> GetPropertyList(string jsonData) {
			JObject data = JObject.Parse(jsonData);
			return data.Properties().Select(p => p.Name).ToList();
		}
		public static bool HasProperty(JObject jsonData, string propertyName) {
			if(jsonData != null) {
				if(jsonData[ propertyName ] != null) {
					return true;
				}
			}
			return false;
		}
	}
	public class JsonInstanceFactory<T> { // <- weird fucking Template/'Generics' programming shit in C#
		public static T ToObjectInstance(string jsonString) {
			string typename = typeof(T).ToString();
			JObject jsonData = null;
			try {
				jsonData = JObject.Parse(jsonString);
			} catch {
				Debugger.Log(0, null, "JSON.ToObjectInstance(" + typeof(T).ToString() + ") failed: malformed data");
				return default(T);
			}
			try {
				T instance = jsonData.ToObject<T>();
				return instance;
			} catch(Newtonsoft.Json.JsonException e) {
				Debugger.Log(0, null, e.Message);
				Debugger.Break();
			}
			return default(T);
		}
	}
}
