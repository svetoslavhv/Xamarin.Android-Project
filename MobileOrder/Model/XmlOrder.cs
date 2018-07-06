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

namespace MobileOrder.Model
{
	[XmlRoot(ElementName = "DemandHeader")]
	public class DemandHeader
	{
		[XmlElement(ElementName = "id")]
		public string Id { get; set; }
		[XmlElement(ElementName = "sale_id")]
		public string SaleId { get; set; }
		[XmlElement(ElementName = "sell_name")]
		public string SellName { get; set; }
		[XmlElement(ElementName = "datetime")]
		public string DateTime { get; set; }
		[XmlElement(ElementName = "typedoc")]
		public string TypeDoc { get; set; }
		[XmlElement(ElementName = "docname")]
		public string DocName { get; set; }
		[XmlElement(ElementName = "cust_id")]
		public string CustId { get; set; }
		[XmlElement(ElementName = "cust_name")]
		public string CustName { get; set; }
		[XmlElement(ElementName = "worktime")]
		public string WorkTime { get; set; }
		[XmlElement(ElementName = "tp")]
		public string Tp { get; set; }
		[XmlElement(ElementName = "km")]
		public string Km { get; set; }
		[XmlElement(ElementName = "paysum")]
		public string PaySum { get; set; }
		[XmlElement(ElementName = "f1")]
		public string F1 { get; set; }
		[XmlElement(ElementName = "f2")]
		public string F2 { get; set; }
		[XmlElement(ElementName = "f3")]
		public string F3 { get; set; }
		[XmlElement(ElementName = "paytype")]
		public string PayType { get; set; }
		[XmlElement(ElementName = "invoiceid")]
		public string InvoiceId { get; set; }
		[XmlElement(ElementName = "bonid")]
		public string BonId { get; set; }
	}

	[XmlRoot(ElementName = "Demand")]
	public class Demand
	{
		[XmlElement(ElementName = "id")]
		public string Id { get; set; }
		[XmlElement(ElementName = "artid")]
		public string ArtId { get; set; }
		[XmlElement(ElementName = "name")]
		public string Name { get; set; }
		[XmlElement(ElementName = "quantity")]
		public string Quantity { get; set; }
		[XmlElement(ElementName = "price")]
		public string Price { get; set; }
		[XmlElement(ElementName = "sum")]
		public string Sum { get; set; }
		[XmlElement(ElementName = "discount")]
		public string Discount { get; set; }
		[XmlElement(ElementName = "pprice")]
		public string Pprice { get; set; }
		[XmlElement(ElementName = "certificate")]
		public string Certificate { get; set; }
		[XmlElement(ElementName = "f1")]
		public string F1 { get; set; }
		[XmlElement(ElementName = "f2")]
		public string F2 { get; set; }
		[XmlElement(ElementName = "f3")]
		public string F3 { get; set; }
	}

	[XmlRoot(ElementName = "xmlOrder")]
	public class XmlOrder
	{
		[XmlElement(ElementName = "DemandHeader")]
		public DemandHeader DemandHeader { get; set; }
		[XmlElement(ElementName = "Demand")]
		public List<Demand> Demands { get; set; }
	}
}