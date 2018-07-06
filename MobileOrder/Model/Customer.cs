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
	[Table("customers")]
	[XmlRoot(ElementName = "Customers", Namespace = "http://tempuri.org/DataSet1.xsd")]
	public class Customer
	{
		//sqlite columns names
		[Column("id")]
		[XmlElement(ElementName = "id", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Id { get; set; }

		[Column("company")]
		[XmlElement(ElementName = "company", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Company { get; set; }

		[Column("recipient")]
		[XmlElement(ElementName = "recipient", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Recipient { get; set; }

		[Column("discount")]
		[XmlElement(ElementName = "discount", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Discount { get; set; }

		[Column("debt")]
		[XmlElement(ElementName = "debt", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Debt { get; set; }

		[Column("object")]
		[XmlElement(ElementName = "object", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Object { get; set; }

		[Column("subject")]
		[XmlElement(ElementName = "subobject", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string SubObject { get; set; }

		[Column("objectaddress")]
		[XmlElement(ElementName = "objectaddress", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Objectaddress { get; set; }

		[Column("mol")]
		[XmlElement(ElementName = "person", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Person { get; set; }

		[Column("typeprice")]
		[XmlElement(ElementName = "typeprice", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string TypePrice { get; set; }

		[Column("routeid")]
		[XmlElement(ElementName = "route", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Route { get; set; }

		[Column("model")]
		[XmlElement(ElementName = "model", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Model { get; set; }

		[Column("city")]
		[XmlElement(ElementName = "city", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string City { get; set; }

		[Column("region")]
		[XmlElement(ElementName = "region", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Region { get; set; }

		[Column("dealer")]
		[XmlElement(ElementName = "dealer", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Dealer { get; set; }

		[Column("segment")]
		[XmlElement(ElementName = "segment", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Segment { get; set; }

		[Column("agreement")]
		[XmlElement(ElementName = "agreement", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Agreement { get; set; }

		[Column("assembly")]
		[XmlElement(ElementName = "assembly", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Assembly { get; set; }

		[Column("dateassembly")]
		[XmlElement(ElementName = "dateassembly", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string DateAssembly { get; set; }

		[Column("phone")]
		[XmlElement(ElementName = "phone", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Phone { get; set; }

		[Column("recipe")]
		[XmlElement(ElementName = "recipe", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Recipe { get; set; }

		[Column("lastselldate")]
		//[XmlElement(ElementName = "lastselldate", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string LastSellDate { get; set; }

		[Column("lastquantity")]
		public int LastQuantity { get; set; }

		[Column("segment_id")]
		//[XmlElement(ElementName = "segment", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string SegmentId { get; set; }

		[Column("refid")]
		public string RefId { get; set; }

		[Column("rowflag")]
		public int RowFlag { get; set; }

		[Column("mstate")]
		[XmlElement(ElementName = "mstate", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Mstate { get; set; }

		[Column("taxnumber")]
		[XmlElement(ElementName = "vatnumber", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string VatNumber { get; set; }

		[Column("bulstat")]
		[XmlElement(ElementName = "taxnumber", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string TaxNumber { get; set; }

		[Column("address")]
		[XmlElement(ElementName = "address", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Address { get; set; }
	}

	//[XmlRoot(ElementName = "vatnumber", Namespace = "http://tempuri.org/DataSet1.xsd")]
	//public class Vatnumber
	//{
	//	[XmlAttribute(AttributeName = "space", Namespace = "http://www.w3.org/XML/1998/namespace")]
	//	public string Space { get; set; }
	//}

	//[XmlRoot(ElementName = "taxnumber", Namespace = "http://tempuri.org/DataSet1.xsd")]
	//public class Taxnumber
	//{
	//	[XmlAttribute(AttributeName = "space", Namespace = "http://www.w3.org/XML/1998/namespace")]
	//	public string Space { get; set; }
	//}

	//[XmlRoot(ElementName = "address", Namespace = "http://tempuri.org/DataSet1.xsd")]
	//public class Address
	//{
	//	[XmlAttribute(AttributeName = "space", Namespace = "http://www.w3.org/XML/1998/namespace")]
	//	public string Space { get; set; }
	//}

	[XmlRoot(ElementName = "DataSet1", Namespace = "http://tempuri.org/DataSet1.xsd")]
	public class Customers : IPlural<Customer>
	{
		[XmlElement(ElementName = "Customers", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public List<Customer> All { get; set; }
	}

}