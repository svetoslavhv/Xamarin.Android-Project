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
using Java.Util.Zip;
using MobileOrder.Data;
using XmlToCSharpClasses;
using System.IO;
using System.Xml.Serialization;
using MobileOrder.Model;
using MobileOrder.Events;
using SQLite;
using MobileOrder.Globals;
using MobileOrder.RequestsToDatabaseMethods;
using MobileOrder.HelperMethods;
using System.Xml;
using System.Xml.Linq;

namespace MobileOrder
{
	[Activity(Label = "SynchronizationActivity")]
	public class SynchronizationActivity : Activity
	{
		
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Synchronization);
			
			Settings settings = CurrentSettings.GetSettings();

			Button btnGetData = FindViewById<Button>(Resource.Id.btnGetData);
			btnGetData.Click += delegate
			{
				//define names to zip files
				int objectId = Convert.ToInt32(settings.Synchronization.TpId);
				string basedataZipName = "basedata_" + objectId + ".zip";
				string dutybasedataZipName = "dutybasedata_" + objectId + ".zip";
				string tranbasedataZipName = "tranbasedata_" + objectId + ".zip";
				string vendbasedataZipName = "vendbasedata_" + objectId + ".zip";

				//check if SOAP synchronization is ON
				if (settings.Synchronization.Servicefiletransfer == true)
				{
					
					//create alert dialog to confirm file transfer
					Dialog dialogConfirmTransfer = new Dialog(this);
					AlertDialog.Builder alertConfirmTransfer = new AlertDialog.Builder(this);
					alertConfirmTransfer.SetMessage("Внимание ако данните не са предадени ще бъдат загубени. Потвърдете вземането на нови данни.");
					alertConfirmTransfer.SetPositiveButton("ДА", delegate
					{
						//alertConfirmTransfer.Dispose();
						
						try
						{
							dialogConfirmTransfer.Cancel();
							
							//TODO: Display alert dialog "Getting data. Please wait..." The dialog stays on screen
							//while until zip files are get from service, saved into app and unzipped
							//make it fireing eventhandler
							
							//get zip files from service
							MobileSellReference.Service1 service = new MobileSellReference.Service1();
							service.Url = settings.Synchronization.Msellurl;

							//get result from service as 2 dimensional array and showing file transfer progress,
							//the first dimension contains the zip file ,the second dimension contains error message if exists
							byte[][] resultFromService = service.ToPPC(basedataZipName, objectId);

							//get only zip files
							byte[] basedataZipFile = resultFromService[0];
							byte[] dutybasedataZipFile = resultFromService[3];
							byte[] tranbasedataZipFile = resultFromService[2];
							byte[] vendbasedataZipFile = resultFromService[1];

							//get the full path of the zip file
							string basedataZipFullPath = GlobalVariables.fromserverFolderPath + "/" + basedataZipName;
							string dutybasedataZipFullPath = GlobalVariables.fromserverFolderPath + "/" + dutybasedataZipName;
							string tranbasedataZipFullPath = GlobalVariables.fromserverFolderPath + "/" + tranbasedataZipName;
							string vendbasedataZipFullPath = GlobalVariables.fromserverFolderPath + "/" + vendbasedataZipName;

							//delete existing zip and xml files from all folders before saving new ones
							Helper.DeleteAllFromFolders(GlobalVariables.fromserverFolderPath, GlobalVariables.vendingFolderPath, GlobalVariables.tranFolderPath, GlobalVariables.debtFolderPath);

							//delete database records
							Helper.EmptyMobileSellDB();
							
							//save zip files in fromserver folder
							System.IO.File.WriteAllBytes(basedataZipFullPath, basedataZipFile);
							System.IO.File.WriteAllBytes(dutybasedataZipFullPath, dutybasedataZipFile);
							System.IO.File.WriteAllBytes(tranbasedataZipFullPath, tranbasedataZipFile);
							System.IO.File.WriteAllBytes(vendbasedataZipFullPath, vendbasedataZipFile);

							//unzip zip files in their corresponding folders
							Helper.UnzipFile(basedataZipFullPath, GlobalVariables.fromserverFolderPath);
							Helper.UnzipFile(dutybasedataZipFullPath, GlobalVariables.debtFolderPath);
							Helper.UnzipFile(tranbasedataZipFullPath, GlobalVariables.tranFolderPath);
							Helper.UnzipFile(vendbasedataZipFullPath, GlobalVariables.vendingFolderPath);
							
							//Import xml files into database
							ImportXMLFileIntoDatabase<Article, Articles>(GlobalVariables.fromserverFolderPath, "articles.xml", "артикули");
							ImportXMLFileIntoDatabase<Customer, Customers>(GlobalVariables.fromserverFolderPath, "customers.xml", "клиенти");
							ImportXMLFileIntoDatabase<Sertif, Sertifs>(GlobalVariables.fromserverFolderPath, "sertif.xml", "сертификати");
							ImportXMLFileIntoDatabase<CustDiscount, CustDiscounts>(GlobalVariables.fromserverFolderPath, "custdisc.xml", "отстъпки");
							ImportXMLFileIntoDatabase<Group, Groups>(GlobalVariables.fromserverFolderPath, "groups.xml", "групи");
							ImportXMLFileIntoDatabase<Route, Routes>(GlobalVariables.fromserverFolderPath, "routes.xml", "маршрути");

							//copy object.xml from fromserver folder to MobileSell folder
							File.Copy(GlobalVariables.fromserverFolderPath + "/object.xml", GlobalVariables.mobileSellFolderPath + "/object.xml", true);
							
							//deleting files from all folders
							Helper.DeleteAllFromFolders(GlobalVariables.fromserverFolderPath, GlobalVariables.vendingFolderPath, GlobalVariables.tranFolderPath, GlobalVariables.debtFolderPath);
							
						}
						catch (Exception ex)
						{
							//if during the process of saving zip files into folder 
							//or during their unziping into their corresponding folders 
							//error occurs we delete all that have been saved or unziped from the folders
							Helper.DeleteAllFromFolders(GlobalVariables.fromserverFolderPath, GlobalVariables.vendingFolderPath, GlobalVariables.tranFolderPath, GlobalVariables.debtFolderPath);
							
							//delete database records
							Helper.EmptyMobileSellDB();

							//hide alert dialog "Wait until the data is get" if error occurs while transfering zip files
							//alertGettingData.Dispose();
							//create alert if error occurs while transfering zip files
							AlertDialog.Builder alertFileTransferError = new AlertDialog.Builder(this);
							alertFileTransferError.SetMessage(ex.Message);
							alertFileTransferError.SetPositiveButton("OK", delegate
							{
								alertFileTransferError.Dispose();
							});

							Dialog dialogSoapSynchronizationOFF = alertFileTransferError.Create();
							dialogSoapSynchronizationOFF.Show();
						}
					});

					alertConfirmTransfer.SetNegativeButton("НЕ", delegate
					{
						dialogConfirmTransfer.Cancel();
					});

					dialogConfirmTransfer = alertConfirmTransfer.Create();
					dialogConfirmTransfer.Show();
					
				}
				else
				{
					//create alert soap synhronization is set to OFF
					AlertDialog.Builder alertSoapSynchronizationOFF = new AlertDialog.Builder(this);
					alertSoapSynchronizationOFF.SetMessage("SOAP синхронизацията е изключена");
					alertSoapSynchronizationOFF.SetPositiveButton("OK", delegate
					{
						alertSoapSynchronizationOFF.Dispose();
					});

					Dialog dialogSoapSynchronizationOFF = alertSoapSynchronizationOFF.Create();
					dialogSoapSynchronizationOFF.Show();
				}
				
			};
			
		}
		
		/// <summary>
		///Method to import XML file content into database given xml file location, xml file name, the location of the database, objects to be imported in bulgarian in plural
		/// </summary>
		//Dialog dialogImportSuccessful;
		public void ImportXMLFileIntoDatabase<T, U>(string xmlFileLocation, string xmlFileName, string objectsToImport)
			where U : IPlural<T> where T : new()
		{
			string xmlFileFullPath = xmlFileLocation + "/" + xmlFileName;
			XmlSerializer deserializer = new XmlSerializer(typeof(U));
			TextReader textReader = new StreamReader(xmlFileFullPath);
			U u = (U)deserializer.Deserialize(textReader);
			textReader.Close();
			//Import into database
			using (var connection = new SQLiteConnection(System.IO.Path.Combine(GlobalVariables.databasePath, "MobileSell.db")))
			{
				try
				{
					//connection.DeleteAll<T>();
					List<T> all = u.All;
					connection.InsertAll(all);
					int numElementsImported = connection.Table<T>().ToList().Count;
					//dialogImportSuccessful.Hide();
					//AlertDialog.Builder alertImportSuccessful = new AlertDialog.Builder(this);
					//alertImportSuccessful.SetMessage("Импортирани са " + numElementsImported + " " + objectsToImport);
					//dialogImportSuccessful = alertImportSuccessful.Create();
					//dialogImportSuccessful.Show();
					//recapitulationSuccessMessage += ", " + numElementsImported + " " + objectsToImport;
					Toast.MakeText(this, "Импортирани са " + numElementsImported + " " + objectsToImport, ToastLength.Short).Show();
					//recapitulationSuccessMessage += ", " + numElementsImported + " " + objectsToImport;
				}
				catch (Exception ex)
				{
					//throw exception
					//Toast.MakeText(this, "Грешка при импорт на " + objectsToImport, ToastLength.Short).Show();
					//if a single xml is not imported successfully into the database
					//set allXMLFilesImportedIntoDatabase to false
					//allXMLFilesImportedIntoDatabase = false;
					//recapitulationFailMessage += ", " + objectsToImport;

				}
			}
		}

		public interface IPlural<T>
		{
			List<T> All { get; set; }
		}
		
	}
}
