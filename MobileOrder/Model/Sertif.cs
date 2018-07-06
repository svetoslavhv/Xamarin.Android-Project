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
	[Table("sertif")]
	[XmlRoot(ElementName = "sertif", Namespace = "http://tempuri.org/DataSet1.xsd")]
	public class Sertif
	{
		//sqlite columns names
		[Column("artid")]
		[XmlElement(ElementName = "artid", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string ArtId { get; set; }

		[Column("name")]
		[XmlElement(ElementName = "name", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Name { get; set; }

		[Column("quantity")]
		[XmlElement(ElementName = "quantity", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public decimal Quantity { get; set; }
		public string QuantityDisplay
		{
			get
			{
				return String.Format("{0:0.000}", Quantity);
			}
		}

		[PrimaryKey]
		[Column("lotid")]
		[XmlElement(ElementName = "lotid", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Lotid { get; set; }

		[Column("expire_date")]
		[XmlElement(ElementName = "expire_date", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string ExpireDate { get; set; }
	}

	[XmlRoot(ElementName = "DataSet1", Namespace = "http://tempuri.org/DataSet1.xsd")]
	public class Sertifs : IPlural<Sertif>
	{
		[XmlElement(ElementName = "sertif", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public List<Sertif> All { get; set; }
	}
}