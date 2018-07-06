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
using SQLite;
using static MobileOrder.SynchronizationActivity;

namespace MobileOrder.Model
{
	//sqlite table name
	[Table("custdisc")]
	[XmlRoot(ElementName = "custdiscount", Namespace = "http://tempuri.org/DataSet1.xsd")]
	public class CustDiscount
	{
		//sqlite columns names
		[Column("customerid")]
		[XmlElement(ElementName = "customerid", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string CustomerId { get; set; }
		[Column("artid")]
		[XmlElement(ElementName = "group", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Group { get; set; }
		[Column("discount")]
		[XmlElement(ElementName = "discount", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Discount { get; set; }
		[Column("price")]
		[XmlElement(ElementName = "price", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Price { get; set; }
	}

	[XmlRoot(ElementName = "DataSet1", Namespace = "http://tempuri.org/DataSet1.xsd")]
	public class CustDiscounts : IPlural<CustDiscount>
	{
		[XmlElement(ElementName = "custdiscount", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public List<CustDiscount> All { get; set; }
	}
}