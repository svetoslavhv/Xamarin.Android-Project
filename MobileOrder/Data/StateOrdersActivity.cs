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
using MobileOrder.Globals;
using System.IO;
using MobileOrder.Model;

namespace MobileOrder.Data
{
	//Global class than contains variables if data folder is empty and if creating new order is true or false 
	public class StateOrdersActivity
	{
		//public static bool dataFolderEmpty = Directory.GetFiles(GlobalVariables.dataFolderPath).Length != 0 ? false : true;
		public static int dataFolderNumOrders = Directory.GetFiles(GlobalVariables.dataFolderPath).Length;
		public static bool creatingNewOrder;
		//contains the position of next order to be displayed
		public static int DisplayOrderPosition = 0;

		public static List<XmlOrder> xmlOrders = new List<XmlOrder>();
		
		private StateOrdersActivity()
		{
		}

		//public static int DataFolderNumOrders
		//{
		//	get
		//	{
		//		return Directory.GetFiles(GlobalVariables.dataFolderPath).Length;
		//	}
		//}

		//public static ref int GetDataFolderNumOrders()
		//{
		//	return ref dataFolderNumOrders;
		//}

		//public static ref bool IsCreatingNewOrder()
		//{
		//	return ref creatingNewOrder;
		//}
	}
}