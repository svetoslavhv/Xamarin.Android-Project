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

namespace MobileOrder.Globals
{
	//class containing global variables for all project
	public class GlobalVariables
	{
		//string apkFolder = FilesDir.Path;
		//string pathFromserverFolder = apkFolder + "/backup/fromserver";
		//string pathVendingFolder = pathFromserverFolder + "/vending";
		//string pathTranFolder = pathFromserverFolder + "/tran";
		//string pathDebtFolder = pathFromserverFolder + "/debt";

		public static string databasePath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSell";
		public static string databaseName = "MobileSell.db";
		public static string mobileSellFolderPath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSell";
		public static string backupFolderPath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSell/backup";
		public static string fromserverFolderPath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSell/backup/fromserver";
		public static string vendingFolderPath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSell/vending";
		public static string tranFolderPath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSell/tran";
		public static string debtFolderPath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSell/debt";
		public static string dataFolderPath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSell/data";

		//public static string apkDebtFolderPath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSell/debt";
		//public static string apkTranFolderPath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSell/tran";
		//public static string apkVendingFolderPath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSell/vending";


		private GlobalVariables()
		{
		}

	}
}