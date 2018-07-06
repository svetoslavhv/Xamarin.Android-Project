using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;
using System.Xml;
using Android.Runtime;
using System;
using Android.Content.Res;
using System.Drawing;
using Java.Lang;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using XmlToCSharpClasses;
using System.Diagnostics;
using MobileOrder.Data;
using Android.Support.V7.App;
using MobileOrder.RequestsToDatabaseMethods;
using MobileOrder.Model;

namespace MobileOrder
{
	[Activity(Label = "MobileOrder for Android ver 1.0", MainLauncher = true, Theme ="@style/Theme.AppCompat")]
	public class MainActivity : AppCompatActivity
	{
		
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);


			//get MobileSell.db

			//save it into the SPECIAL FOLDER

			//var rootPath = FilesDir.Path;
			//var ordersFolderPath = rootPath + "/orders";
			//Directory.CreateDirectory(ordersFolderPath);
			//var directories = System.IO.Directory.EnumerateDirectories(rootPath);
			//foreach (var directory in directories)
			//{
			//	var currentDir = directory;
			//}
			//MobileSell.db file path
			//string MobileSellDBpath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSell2/MobileSell.db";
			//string MobileSellDBpath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSellFromAPK/MobileSell.db";
			////create folders in  apk folder
			//var pathToNewFolder = rootPath + "/backup" + "/fromserver";
			////Directory.CreateDirectory(pathToNewFolder);
			//var pathVending = pathToNewFolder + "/vending";
			//var pathTran = pathToNewFolder + "/tran";
			////var pathDebt = pathToNewFolder + "/debt";
			//Directory.CreateDirectory(pathVending);
			//Directory.CreateDirectory(pathTran);
			//Directory.CreateDirectory(pathDebt);
			//Directory.Delete(pathVending);

			//File.Delete(rootPath + "/MobileSell.db");
			//File.Copy(rootPath + "/MobileSell.db", Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSell/MobileSell.db");

			//System.IO.Directory.Move(rootPath + "/backup", Android.OS.Environment.ExternalStorageDirectory.ToString() + "/MobileSell");


			//var filesAfter = System.IO.Directory.GetFiles(rootPath);
			//var allArticles = DatabaseRequest.GetAllFromTable<Article>();
			//decimal value = 3.45616m;
			//decimal valueAfterRounding = System.Math.Round(value, 4);

			//Loading settings from xml file
			Settings settings = CurrentSettings.GetSettings();
			
			Button btnOrders = FindViewById<Button>(Resource.Id.btnOrders);
			btnOrders.Click += delegate {
				StartActivity(typeof(OrdersActivity));
			};
			
			Button btnCustomers = FindViewById<Button>(Resource.Id.btnCustomers);
			btnCustomers.Click += delegate {
				StartActivity(typeof(CustomersActivity));
			};

			Button btnInventory = FindViewById<Button>(Resource.Id.btnInventory);
			btnInventory.Click += delegate {
				StartActivity(typeof(InventoryActivity));
			};

			Button btnRoutes = FindViewById<Button>(Resource.Id.btnRoutes);
			btnRoutes.Click += delegate {
				StartActivity(typeof(RoutesActivity));
			};

			
			Button btnSynchronization = FindViewById<Button>(Resource.Id.btnSynchronization);
			btnSynchronization.Click += delegate {
				StartActivity(typeof(SynchronizationActivity));
			};

			Button btnReports = FindViewById<Button>(Resource.Id.btnReports);
			btnReports.Click += delegate {
				StartActivity(typeof(ReportsActivity));
			};
			
			Button btnSettings = FindViewById<Button>(Resource.Id.btnSettings);
			btnSettings.Click += delegate {
				//display alert dialog asking for admin pass
				LayoutInflater layoutInflater = LayoutInflater.From(this);
				View userInputDialogBox = layoutInflater.Inflate(Resource.Layout.user_input_dialog_box, null);
				Android.Support.V7.App.AlertDialog.Builder alertDialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
				alertDialogBuilder.SetView(userInputDialogBox);
				var userInput = userInputDialogBox.FindViewById<EditText>(Resource.Id.userInput);
				alertDialogBuilder.SetPositiveButton("OK", delegate
				{
					//take action according to if input password from user is equal to adminpass or not
					if(userInput.Text == settings.Synchronization.Adminpass)
					{
						StartActivity(typeof(SettingsActivity));
					}
					else
					{
						LayoutInflater layoutInflaterWrongPass = LayoutInflater.From(this);
						View wrongPasswordDialogBox = layoutInflater.Inflate(Resource.Layout.wrong_password_dialog_box, null);
						Android.Support.V7.App.AlertDialog.Builder alertDialogWrongPassBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
						alertDialogWrongPassBuilder.SetView(wrongPasswordDialogBox);
						alertDialogWrongPassBuilder.SetPositiveButton("OK", delegate
						{
							alertDialogWrongPassBuilder.Dispose();
						});
						Android.Support.V7.App.AlertDialog alertDialogWrongPass = alertDialogWrongPassBuilder.Create();
						alertDialogWrongPass.Show();
					}
				})
				.SetNegativeButton("Cancel", delegate
				{
					alertDialogBuilder.Dispose();
				});

				Android.Support.V7.App.AlertDialog alertDialog = alertDialogBuilder.Create();
				alertDialog.Show();
			};

			Button btnAboutProgram = FindViewById<Button>(Resource.Id.btnAboutProgram);
			btnAboutProgram.Click += delegate {
				StartActivity(typeof(AboutProgramActivity));
			};
			
		}

		//Display exit confirmation message when back button pressed once and exits application when back button pressed twice
		Toast toast = Toast.MakeText(Application.Context, "Натиснете още веднъж за излизане", ToastLength.Short);
		bool doubleBackToExitPressedOnce = false;
		public override void OnBackPressed()
		{
			if (doubleBackToExitPressedOnce)
			{
				toast.Cancel();
				FinishAffinity();
			}
			
			toast.SetMargin(0, 0.20f);
			Stopwatch stopwatch = Stopwatch.StartNew();
			toast.Show();
			stopwatch.Stop();
			var time = stopwatch.ElapsedMilliseconds;

			this.doubleBackToExitPressedOnce = true;

			new Handler().PostDelayed(() =>
			{
				doubleBackToExitPressedOnce = false;
			}, 2000);
		
		}

	}
}

