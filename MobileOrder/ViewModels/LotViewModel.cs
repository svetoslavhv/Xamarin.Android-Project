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

namespace MobileOrder.ViewModels
{
	public class LotViewModel
	{
		public string LotId { get; set; }

		public string Name { get; set; }

		public string ExpireDate { get; set; }

		public string QuantityDisplay { get; set; }
		
	}
}