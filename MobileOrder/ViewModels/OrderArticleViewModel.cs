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
	public class OrderArticleViewModel
	{
		public string ArticleId { get; set; }

		public string LotId { get; set; }

		public string ArticleName { get; set; }

		public string PriceDiscount { get; set; }

		public string ArticlePrice { get; set; }

		public string ArticleQuantity { get; set; }

		public string ArticleTotalPrice { get; set; }
	}
}