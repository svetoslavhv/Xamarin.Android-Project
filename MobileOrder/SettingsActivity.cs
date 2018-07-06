using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using XmlToCSharpClasses;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;
using System.Xml;
using MobileOrder.Data;
using System.Xml.Serialization;
using System.IO;

namespace MobileOrder
{
	[Activity(Label = "Settings")]
	public class SettingsActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			//Load MobileOrder Settings in a global settings object for all activities
			Settings settings = CurrentSettings.GetSettings();
			
			SetContentView(Resource.Layout.Settings);
			
			//SET SETTINGS OBJECT VALUES TO FIELDS

			//SALES
			Switch custflag = FindViewById<Switch>(Resource.Id.custflag);
			custflag.Checked = settings.Sales.Custflag;
			Switch discustedit = FindViewById<Switch>(Resource.Id.discustedit);
			discustedit.Checked = settings.Sales.Discustedit;
			Switch detailheader = FindViewById<Switch>(Resource.Id.detailheader);
			detailheader.Checked = settings.Sales.Detailheader;
			Switch nullq = FindViewById<Switch>(Resource.Id.nullq);
			nullq.Checked = settings.Sales.Nullq;
			Switch allpay = FindViewById<Switch>(Resource.Id.allpay);
			allpay.Checked = settings.Sales.Allpay;
			Switch enableprice = FindViewById<Switch>(Resource.Id.enableprice);
			enableprice.Checked = settings.Sales.Enableprice;
			Switch disabletran = FindViewById<Switch>(Resource.Id.disabletran);
			disabletran.Checked = settings.Sales.Disabletran;
			Switch payforinv = FindViewById<Switch>(Resource.Id.payforinv);
			payforinv.Checked = settings.Sales.Payforinv;
			Switch disabledownload = FindViewById<Switch>(Resource.Id.disabledownload);
			disabledownload.Checked = settings.Sales.Disabledownload;
			TextView tpid = FindViewById<TextView>(Resource.Id.tpid);
			tpid.Text = settings.Sales.TpId;
			TextView tp = FindViewById<TextView>(Resource.Id.tp);
			tp.Text = settings.Sales.Tp;
			TextView Decimaldigit = FindViewById<TextView>(Resource.Id.Decimaldigit);
			Decimaldigit.Text = settings.Sales.Decimaldigit.ToString();
			TextView paydays = FindViewById<TextView>(Resource.Id.paydays);
			paydays.Text = settings.Sales.Paydays.ToString();


			//SYNCHRONIZATION
			Switch servicefiletransfer = FindViewById<Switch>(Resource.Id.servicefiletransfer);
			servicefiletransfer.Checked = settings.Synchronization.Servicefiletransfer;
			TextView tp_id = FindViewById<TextView>(Resource.Id.tp_id);
			tp_id.Text = settings.Synchronization.TpId;
			TextView user = FindViewById<TextView>(Resource.Id.user);
			user.Text = settings.Synchronization.User;
			TextView psw = FindViewById<TextView>(Resource.Id.psw);
			psw.Text = settings.Synchronization.Psw;
			TextView msellurl = FindViewById<TextView>(Resource.Id.msellurl);
			msellurl.Text = settings.Synchronization.Msellurl;
			TextView msellurl2 = FindViewById<TextView>(Resource.Id.msellurl2);
			msellurl2.Text = settings.Synchronization.Msellurl2;
			TextView server = FindViewById<TextView>(Resource.Id.server);
			server.Text = settings.Synchronization.Server;
			TextView server2 = FindViewById<TextView>(Resource.Id.server2);
			server2.Text = settings.Synchronization.Server2;
			TextView updir = FindViewById<TextView>(Resource.Id.updir);
			updir.Text = settings.Synchronization.Updir;
			TextView getdir = FindViewById<TextView>(Resource.Id.getdir);
			getdir.Text = settings.Synchronization.Getdir;
			Switch autoftp = FindViewById<Switch>(Resource.Id.autoftp);
			autoftp.Checked = settings.Synchronization.Autoftp;
			TextView adminpass = FindViewById<TextView>(Resource.Id.adminpass);
			adminpass.Text = settings.Synchronization.Adminpass;

			//PRINTING
			Switch fpprinter = FindViewById<Switch>(Resource.Id.fpprinter);
			fpprinter.Checked = settings.Printing.Fpprinter;
			TextView printer = FindViewById<TextView>(Resource.Id.printer);
			printer.Text = settings.Printing.Printer;
			Spinner printertype = FindViewById<Spinner>(Resource.Id.printertype);
			int i = (int)settings.Printing.PrinterType;
			printertype.SetSelection(i);
			Switch fpasnonfp = FindViewById<Switch>(Resource.Id.fpasnonfp);
			fpasnonfp.Checked = settings.Printing.Fpasnonfp;
			Switch secondinvoicecopy = FindViewById<Switch>(Resource.Id.secondinvoicecopy);
			secondinvoicecopy.Checked = settings.Printing.Secondinvoicecopy;
			Switch wm = FindViewById<Switch>(Resource.Id.wm);
			wm.Checked = settings.Printing.Wm;
			TextView nonfpprinter = FindViewById<TextView>(Resource.Id.nonfpprinter);
			nonfpprinter.Text = settings.Printing.Nonfpprinter;
			
			//Save button clicked
			Button btnSave = FindViewById<Button>(Resource.Id.btnSave);
			btnSave.Click += delegate
			{
				//Remove keybord
				LinearLayout parentLayout = FindViewById<LinearLayout>(Resource.Id.parentLayout);
				parentLayout.RequestFocus();
				InputMethodManager inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
				inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

				//Set field values to settings object values

				//sales
				settings.Sales.Custflag = custflag.Checked;
				settings.Sales.Discustedit = discustedit.Checked;
				settings.Sales.Detailheader = detailheader.Checked;
				settings.Sales.Nullq = nullq.Checked;
				settings.Sales.Allpay = allpay.Checked;
				settings.Sales.Enableprice = enableprice.Checked;
				settings.Sales.Disabletran = disabletran.Checked;
				settings.Sales.Payforinv = payforinv.Checked;
				settings.Sales.Disabledownload = disabledownload.Checked;
				settings.Sales.TpId = tpid.Text;
				settings.Sales.Tp = tp.Text = settings.Sales.Tp;
				settings.Sales.Decimaldigit = Convert.ToInt32(Decimaldigit.Text);
				settings.Sales.Paydays = Convert.ToInt32(paydays.Text);

				//synchronization
				settings.Synchronization.Servicefiletransfer = servicefiletransfer.Checked;
				settings.Synchronization.TpId = tp_id.Text;
				settings.Synchronization.User = user.Text;
				settings.Synchronization.Psw = psw.Text;
				settings.Synchronization.Msellurl = msellurl.Text;
				settings.Synchronization.Msellurl2 = msellurl2.Text;
				settings.Synchronization.Server = server.Text;
				settings.Synchronization.Server2 = server2.Text;
				settings.Synchronization.Updir = updir.Text;
				settings.Synchronization.Getdir = getdir.Text;
				settings.Synchronization.Autoftp = autoftp.Checked;
				settings.Synchronization.Adminpass = adminpass.Text;

				//printing
				settings.Printing.Fpprinter = fpprinter.Checked;
				settings.Printing.Printer = printer.Text;
				settings.Printing.PrinterType = (PrinterType)printertype.SelectedItemPosition;
				settings.Printing.Secondinvoicecopy = secondinvoicecopy.Checked;
				settings.Printing.Wm = wm.Checked;
				settings.Printing.Nonfpprinter = nonfpprinter.Text;

				//Save settings object data into the xml file
				string xmlFilePath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/Settings4/settings4.xml";
				XmlSerializer serializer = new XmlSerializer(typeof(Settings));
				TextWriter textWriter = new StreamWriter(xmlFilePath);
				serializer.Serialize(textWriter, settings);
				textWriter.Close();
				
			};
		}
	}
}