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
using MobileOrder.ViewModels;
using MobileOrder.Model;

namespace MobileOrder.Data
{
	public class CurrentOrder
	{
		//contains current order values which is global to all application
		public static List<OrderArticleViewModel> listOfOrderArticles = new List<OrderArticleViewModel>();
		public static string orderCustomerName;
		public static string orderCustomerId;
		public static string orderNumber;
		public static string orderDateAndHour;
		public static DateTime executionOrderDate = DateTime.Now.AddDays(1);

		private CurrentOrder()
		{
		}
		
	}
}