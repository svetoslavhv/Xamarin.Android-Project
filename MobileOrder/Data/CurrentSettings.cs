using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using XmlToCSharpClasses;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Xml.Serialization;
using System.IO;

namespace MobileOrder.Data
{
	public class CurrentSettings
	{
		//contains settings object values which is global to all application
		private static Settings settings;
		private CurrentSettings()
		{
		}

		public static ref Settings GetSettings()
		{
			if (settings == null)
			{
				string xmlFilePath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/Settings4/settings4.xml";
				XmlSerializer deserializer = new XmlSerializer(typeof(Settings));
				TextReader textReader = new StreamReader(xmlFilePath);
				settings = (Settings)deserializer.Deserialize(textReader);
				textReader.Close();
			}
			return ref settings;
		}
	}
}