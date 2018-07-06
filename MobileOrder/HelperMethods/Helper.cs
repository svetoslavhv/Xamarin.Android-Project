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
using System.IO;
using MobileOrder.Globals;
using SQLite;
using MobileOrder.Model;
using Java.Util.Zip;
using System.Xml.Serialization;

namespace MobileOrder.HelperMethods
{
	public class Helper
	{
		/// <summary>
		/// Method to copy all files from one folder to another folder
		/// </summary>
		/// <param name="sourceFolder"></param>
		/// <param name="destinationFolder"></param>
		public static void CopyAllFilesToFolder(string sourceFolder, string destinationFolder)
		{
			var allFilesFromSourceFolder = System.IO.Directory.GetFiles(sourceFolder);
			foreach (string fileFullPath in allFilesFromSourceFolder)
			{
				string fileName = Path.GetFileName(fileFullPath);
				string destination = destinationFolder + "/" + fileName;
				File.Copy(fileFullPath, destination);
			}
		}

		/// <summary>
		/// Method to empty MobileSell database
		/// </summary>
		public static void EmptyMobileSellDB()
		{
			using (var connection = new SQLiteConnection(System.IO.Path.Combine(GlobalVariables.databasePath, "MobileSell.db")))
			{
				connection.DeleteAll<Article>();
				connection.DeleteAll<CustDiscount>();
				connection.DeleteAll<Customer>();
				connection.DeleteAll<Group>();
				connection.DeleteAll<Route>();
				connection.DeleteAll<Sertif>();
			}
		}

		/// <summary>
		/// Method to delete the content of specified folders
		/// </summary>
		/// <param name="foldersPath">list of folders paths which content we want to delete</param>
		public static void DeleteAllFromFolders(params string[] foldersPath)
		{
			foreach (string folderPath in foldersPath)
			{
				Array.ForEach(Directory.GetFiles(folderPath), File.Delete);
			}
		}

		/// <summary>
		/// Method to unzip files given the full file path and the unzip location  
		/// </summary>
		/// <param name="zipFileFullPath"></param>
		/// <param name="unzipLocation"></param>
		public static void UnzipFile(string zipFileFullPath, string unzipLocation)
		{
			//MySQL database has to be turned on
			using (ZipInputStream s = new ZipInputStream(System.IO.File.OpenRead(zipFileFullPath)))
			{
				ZipEntry theEntry;
				while ((theEntry = s.NextEntry) != null)
				{
					string directoryName = Path.GetDirectoryName(theEntry.Name);
					string fileName = Path.GetFileName(theEntry.Name);
					if (fileName != String.Empty)
					{
						using (FileStream streamWriter = System.IO.File.Create(Path.Combine(unzipLocation, theEntry.Name)))
						{
							int size = 2048;
							byte[] data = new byte[size];
							while (true)
							{
								size = s.Read(data, 0, data.Length);
								if (size > 0)
								{
									streamWriter.Write(data, 0, size);
								}
								else
								{
									break;
								}
							}
						}
					}
				}
			}
		}
		
	}
}