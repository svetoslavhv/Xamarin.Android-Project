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
	[Table("groups")]
	[XmlRoot(ElementName = "groups", Namespace = "http://tempuri.org/DataSet1.xsd")]
	public class Group
	{
		//sqlite columns names
		[Column("groupid")]
		[XmlElement(ElementName = "id", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Id { get; set; }

		[Column("name")]
		[XmlElement(ElementName = "name", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}

	[XmlRoot(ElementName = "DataSet1", Namespace = "http://tempuri.org/DataSet1.xsd")]
	public class Groups : IPlural<Group>
	{
		[XmlElement(ElementName = "groups", Namespace = "http://tempuri.org/DataSet1.xsd")]
		public List<Group> All { get; set; }
	}
}