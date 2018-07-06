using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Xml.Serialization;

namespace MobileOrder.Globals
{
	//settings.xml
	[XmlRoot("settings")]
	public class GlobalClass : Application
	{
		//section name="sales" 
		[XmlElement("custflag")]
		private bool Custflag { get; set; }
		[XmlElement("discustedit")]
		private bool Discustedit { get; set; }
		[XmlElement("detailheader")]
		private bool Detailheader { get; set; }
		[XmlElement("nullq")]
		private bool Nullq { get; set; }
		[XmlElement("allpay")]
		private bool Allpay { get; set; }
		[XmlElement("enableprice")]
		private bool Enableprice { get; set; }
		[XmlElement("disabletran")]
		private bool Disabletran { get; set; }
		[XmlElement("payforinv")]
		private bool Payforinv { get; set; }
		[XmlElement("disabledownload")]
		private bool Disabledownload { get; set; }
		[XmlElement("tpid")]
		private string Tpid { get; set; }
		[XmlElement("tp")]
		private string Tp { get; set; }
		[XmlElement("Decimaldigit")]
		private int Decimaldigit { get; set; }
		[XmlElement("paydays")]
		private int Paydays { get; set; }

		//section name="synchronization"
		[XmlElement("servicefiletransfer")]
		private bool Servicefiletransfer { get; set; }
		[XmlElement("tp_id")]
		private string Tp_id { get; set; }
		[XmlElement("user")]
		private string User { get; set; }
		[XmlElement("psw")]
		private string Psw { get; set; }
		[XmlElement("msellurl")]
		private string Msellurl { get; set; }
		[XmlElement("msellurl2")]
		private string Msellurl2 { get; set; }
		[XmlElement("server")]
		private string Server { get; set; }
		[XmlElement("server2")]
		private string Server2 { get; set; }
		[XmlElement("updir")]
		private string Updir { get; set; }
		[XmlElement("getdir")]
		private string Getdir { get; set; }
		[XmlElement("autoftp")]
		private bool Autoftp { get; set; }
		[XmlElement("adminpass")]
		private string Adminpass { get; set; }

		//section name="printing"
		[XmlElement("fpprinter")]
		private bool Fpprinter { get; set; }
		[XmlElement("printer")]
		private string Printer { get; set; }
		[XmlElement("printertype")]
		private string printertype { get; set; }
		[XmlElement("fpasnonfp")]
		private bool Fpasnonfp { get; set; }
		[XmlElement("secondinvoicecopy")]
		private bool Secondinvoicecopy { get; set; }
		[XmlElement("wm")]
		private bool Wm { get; set; }
		[XmlElement("nonfpprinter")]
		private string Nonfpprinter { get; set; }
		
	}
}