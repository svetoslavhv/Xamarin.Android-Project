/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Android.App;
using System.Xml;
using System.IO;
using MobileOrder.Data;

namespace XmlToCSharpClasses
{
	[XmlRoot(ElementName = "sales")]
	public class Sales 
	{
		[XmlElement(ElementName = "custflag")]
		public bool Custflag { get; set; }
		[XmlElement(ElementName = "discustedit")]
		public bool Discustedit { get; set; }
		[XmlElement(ElementName = "detailheader")]
		public bool Detailheader { get; set; }
		[XmlElement(ElementName = "nullq")]
		public bool Nullq { get; set; }
		[XmlElement(ElementName = "allpay")]
		public bool Allpay { get; set; }
		[XmlElement(ElementName = "enableprice")]
		public bool Enableprice { get; set; }
		[XmlElement(ElementName = "disabletran")]
		public bool Disabletran { get; set; }
		[XmlElement(ElementName = "payforinv")]
		public bool Payforinv { get; set; }
		[XmlElement(ElementName = "disabledownload")]
		public bool Disabledownload { get; set; }
		[XmlElement(ElementName = "tpid")]
		public string TpId { get; set; }
		[XmlElement(ElementName = "tp")]
		public string Tp { get; set; }
		[XmlElement(ElementName = "Decimaldigit")]
		public int Decimaldigit { get; set; }
		[XmlElement(ElementName = "paydays")]
		public int Paydays { get; set; }
	}

	[XmlRoot(ElementName = "synchronization")]
	public class Synchronization 
	{
		[XmlElement(ElementName = "servicefiletransfer")]
		public bool Servicefiletransfer { get; set; }
		[XmlElement(ElementName = "tp_id")]
		public string TpId { get; set; }
		[XmlElement(ElementName = "user")]
		public string User { get; set; }
		[XmlElement(ElementName = "psw")]
		public string Psw { get; set; }
		[XmlElement(ElementName = "msellurl")]
		public string Msellurl { get; set; }
		[XmlElement(ElementName = "msellurl2")]
		public string Msellurl2 { get; set; }
		[XmlElement(ElementName = "server")]
		public string Server { get; set; }
		[XmlElement(ElementName = "server2")]
		public string Server2 { get; set; }
		[XmlElement(ElementName = "updir")]
		public string Updir { get; set; }
		[XmlElement(ElementName = "getdir")]
		public string Getdir { get; set; }
		[XmlElement(ElementName = "autoftp")]
		public bool Autoftp { get; set; }
		[XmlElement(ElementName = "adminpass")]
		public string Adminpass { get; set; }
	}

	[XmlRoot(ElementName = "printing")]
	public class Printing 
	{
		[XmlElement(ElementName = "fpprinter")]
		public bool Fpprinter { get; set; }
		[XmlElement(ElementName = "printer")]
		public string Printer { get; set; }
		[XmlElement(ElementName = "printertype")]
		public PrinterType PrinterType { get; set; }
		[XmlElement(ElementName = "fpasnonfp")]
		public bool Fpasnonfp { get; set; }
		[XmlElement(ElementName = "secondinvoicecopy")]
		public bool Secondinvoicecopy { get; set; }
		[XmlElement(ElementName = "wm")]
		public bool Wm { get; set; }
		[XmlElement(ElementName = "nonfpprinter")]
		public string Nonfpprinter { get; set; }
	}
	
	[XmlRoot(ElementName = "settings")]
	public class Settings
	{
		[XmlElement(ElementName = "sales")]
		public Sales Sales { get; set; }
		[XmlElement(ElementName = "synchronization")]
		public Synchronization Synchronization { get; set; }
		[XmlElement(ElementName = "printing")]
		public Printing Printing { get; set; }
	}
	
}