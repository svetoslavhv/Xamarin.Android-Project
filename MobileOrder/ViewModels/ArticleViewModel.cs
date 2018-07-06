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
	public class ArticleViewModel
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string Group { get; set; }

		public decimal SellPrice { get; set; }

		public decimal Quantity { get; set; }

		public decimal Sum { get { return SellPrice * Quantity; } }

		public string SellPriceDisplay
		{
			get
			{
				return String.Format("{0:0.00}", SellPrice);
			}
		}

		public string QuantityDisplay
		{
			get
			{
				return String.Format("{0:0.000}", Quantity);
			}
		}

		public string SumDisplay
		{
			get
			{
				return String.Format("{0:0.00}", Sum);
			}
		}
	}
}