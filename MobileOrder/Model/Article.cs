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
using System.ComponentModel.DataAnnotations;

namespace MobileOrder.Model
{
		//sqlite table name
		[Table("articles")]
		[XmlRoot(ElementName = "articlessql", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public class Article
		{
			//sqlite columns names
			[PrimaryKey]
			[Column("id")]
			[XmlElement(ElementName = "id", Namespace = "http://tempuri.org/DataSet1.xsd")]
			public string Id { get; set; }
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
			[Column("sell_price")]
			[XmlElement(ElementName = "sell_price", Namespace = "http://tempuri.org/DataSet1.xsd")]
			//[DisplayFormat(DataFormatString = "{0:0.00}")]
			public decimal SellPrice { get; set; }
			public string SellPriceDisplay
			{
				get
				{
					return String.Format("{0:0.00}", SellPrice);
				}
			}

			[Column("sellid")]
			[XmlElement(ElementName = "sellid", Namespace = "http://tempuri.org/DataSet1.xsd")]
			public string SellId { get; set; }
			[Column("prod_price")]
			[XmlElement(ElementName = "whole_price", Namespace = "http://tempuri.org/DataSet1.xsd")]
			public decimal WholePrice { get; set; }
			[Column("groups")]
			[XmlElement(ElementName = "group", Namespace = "http://tempuri.org/DataSet1.xsd")]
			public string Group { get; set; }
			[Column("barcode")]
			[XmlElement(ElementName = "barcode", Namespace = "http://tempuri.org/DataSet1.xsd")]
			public string Barcode { get; set; }
			[Column("measure")]
			[XmlElement(ElementName = "measure", Namespace = "http://tempuri.org/DataSet1.xsd")]
			public string Measure { get; set; }
			
		}

		[XmlRoot(ElementName = "DataSet1", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public class Articles : IPlural<Article>
	{
			[XmlElement(ElementName = "articlessql", Namespace = "http://tempuri.org/DataSet1.xsd")]
			public List<Article> All { get; set; }
		}

	

}