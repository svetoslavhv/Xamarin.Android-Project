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
	[Table("routes")]
	[XmlRoot(ElementName = "routes", Namespace = "http://tempuri.org/DataSet1.xsd")]
	public class Route
	{
		//sqlite columns names
		[Column("id")]
		[XmlElement(ElementName = "routeid", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string RouteId { get; set; }

		[Column("name")]
		[XmlElement(ElementName = "routename", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string RouteName { get; set; }
	}

	[XmlRoot(ElementName = "DataSet1", Namespace = "http://tempuri.org/DataSet1.xsd")]
	public class Routes : IPlural<Route>
	{
		[XmlElement(ElementName = "routes", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public List<Route> All { get; set; }
	}
}