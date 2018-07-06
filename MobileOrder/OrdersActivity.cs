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
using System.Diagnostics;
using MobileOrder.Data;
using static Android.App.DatePickerDialog;
using Android.Icu.Util;
using MobileOrder.ViewModels;
using Android.Support.V7.Widget;
using MobileOrder.RecyclerViewHelperClasses;
using System.Xml;
using MobileOrder.Globals;
using MobileOrder.RequestsToDatabaseMethods;
using MobileOrder.Model;
using MobileOrder.CustomExceptions;
using System.IO;
using System.Xml.Serialization;
using Android.Views.InputMethods;
using Android.Graphics;

namespace MobileOrder
{
	[Activity(Label = "OrdersActivity", WindowSoftInputMode = SoftInput.AdjustNothing)]
	public class OrdersActivity : Activity, IOnDateSetListener
	{
		private int year = DateTime.Now.Year, month = DateTime.Now.Month - 1, day = DateTime.Now.Day + 1;
		Button btnDatePicker;
		Button btnNewOrder;
		Button btnSelectCustomer;
		Button btnNewRow;
		Button btnLeftArrow;
		Button btnRightArrow;
		Button btnBon;
		Button btnInvoice;
		Button btnSaveOrder;
		TextView textViewCustomerName;
		DatePickerDialog datePickerDialog;
		TextView textViewOrderNumber;
		TextView textViewOrderDateAndHour;
		TextView totalPriceAllArticlesTextView;
		TextView textViewCustomerId;

		List<OrderArticleViewModel> listOfOrderArticles;

		// RecyclerView instance that displays the articles:
		RecyclerView orderArticlesRecyclerView;

		// Layout manager that lays out each card in the RecyclerView:
		RecyclerView.LayoutManager orderArticlesLayoutManager;

		// Adapter that accesses the data set (articles):
		OrderArticlesAdapter orderArticlesAdapter;

		//contains list of xml orders from data folder

		Settings settings;

		//string currentOrdersActivityState;

		//bool isDataFolderEmpty;
		//bool isCreatingNewOrder;

		//set btnDatePicker's text to be whatever user has selected
		public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
		{
			this.year = year;
			this.month = month + 1;
			this.day = dayOfMonth;
			string selectedDate = String.Format(day + "." + this.month + "." + year);
			//datePickerDialog.DatePicker.DateTime = new DateTime(this.year, this.month, this.day);
			CurrentOrder.executionOrderDate = new DateTime(year, month + 1, dayOfMonth);
			btnDatePicker.Text = CurrentOrder.executionOrderDate.ToString("dd.MM.yyyy");
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Orders);

			settings = CurrentSettings.GetSettings();

			//isDataFolderEmpty = StateOrdersActivity.IsDataFolderEmpty();
			//isCreatingNewOrder = StateOrdersActivity.IsCreatingNewOrder();

			//ENABLE-DISABLE BUTTONS
			//check if orders folder is empty
			//if orders folder is empty disable all buttons except "Нова" else enable all buttons
			//btnSelectCustomer.Enabled = (OrdersFolder is not Empty) and (textViewCustomerName is not empty)
			//btnNewRow.Enabled = OrdersFolder is not Empty) and (textViewCustomerName is not empty)
			//buttonSave.Enable = (recyclerView is not empty)
			//leftArrow.Enabled = (OrdersFolder not empty) and (OrderFolder has more than one value)

			btnLeftArrow = FindViewById<Button>(Resource.Id.btnLeftArrow);
			btnRightArrow = FindViewById<Button>(Resource.Id.btnRightArrow);
			btnBon = FindViewById<Button>(Resource.Id.btnBon);
			btnInvoice = FindViewById<Button>(Resource.Id.btnInvoice);
			btnSaveOrder = FindViewById<Button>(Resource.Id.btnSaveOrder);
			btnDatePicker = FindViewById<Button>(Resource.Id.btnDatePicker);
			btnSelectCustomer = FindViewById<Button>(Resource.Id.btnSelectCustomer);
			btnNewOrder = FindViewById<Button>(Resource.Id.btnNewOrder);
			btnNewRow = FindViewById<Button>(Resource.Id.btnNewRow);

			totalPriceAllArticlesTextView = FindViewById<TextView>(Resource.Id.totalPriceAllArticlesTextView);
			orderArticlesRecyclerView = FindViewById<RecyclerView>(Resource.Id.orderArticlesRecyclerView);

			textViewOrderNumber = FindViewById<TextView>(Resource.Id.textViewOrderNumber);
			textViewOrderDateAndHour = FindViewById<TextView>(Resource.Id.textViewOrderDateAndHour);

			textViewCustomerName = FindViewById<TextView>(Resource.Id.textViewCustomerName);
			textViewCustomerId = FindViewById<TextView>(Resource.Id.textViewCustomerId);

			//TODO:
			//if currently we are not creating an order display first order
			if (StateOrdersActivity.creatingNewOrder == false)
			{
				//fill in OrdersActivity views with data from xml orders
				//get all orders from data folder and map them to object OrderViewModel
				//string xmlFilePath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/Settings4/settings4.xml";
				//XmlSerializer deserializer = new XmlSerializer(typeof(Settings));
				//TextReader textReader = new StreamReader(xmlFilePath);
				//settings = (Settings)deserializer.Deserialize(textReader);
				//textReader.Close();
				EnableDisableButtons();
				//get all file names of saved orders in data folder in ascending order
				var allSavedOrdersFileNames = Directory.GetFiles(GlobalVariables.dataFolderPath, "*.xml").Select(System.IO.Path.GetFileName).OrderBy(f => f);
				if (allSavedOrdersFileNames.ToList().Count != 0)
				{
					foreach (string xmlFileName in allSavedOrdersFileNames)
					{
						//map xml files to c# objects and add it to list of StateOrdersActivity.xmlOrders
						string xmlFullFilePath = GlobalVariables.dataFolderPath + "/" + xmlFileName;
						XmlSerializer deserializer = new XmlSerializer(typeof(XmlOrder));
						TextReader textReader = new StreamReader(xmlFullFilePath);
						XmlOrder xmlOrder = (XmlOrder)deserializer.Deserialize(textReader);
						StateOrdersActivity.xmlOrders.Add(xmlOrder);
						textReader.Close();
					}

					//display first order
					DisplayOrder(StateOrdersActivity.DisplayOrderPosition);
				}

			}
			else
			{
				//set values to widgets again so that we don't loose them when we are navigating throught another activities
				EnableDisableButtons();
				//recieve the customer name and set it to textViewCustomerName
				textViewCustomerName.Text = CurrentOrder.orderCustomerName;
				//recieve the customer id and set it to textViewCustomerId
				textViewCustomerId.Text = CurrentOrder.orderCustomerId;
				textViewOrderNumber.Text = CurrentOrder.orderNumber;
				textViewOrderDateAndHour.Text = CurrentOrder.orderDateAndHour;
				orderArticlesLayoutManager = new LinearLayoutManager(this);
				orderArticlesRecyclerView.SetLayoutManager(orderArticlesLayoutManager);
				orderArticlesAdapter = new OrderArticlesAdapter(CurrentOrder.listOfOrderArticles, this);
				orderArticlesRecyclerView.SetAdapter(orderArticlesAdapter);
				totalPriceAllArticlesTextView.Text = orderArticlesAdapter.CaltulateTotalPriceOfAllOrderArticles().ToString();
				btnDatePicker.Text = CurrentOrder.executionOrderDate.ToString("dd.MM.yyyy");

			}

			//TODO:
			//if StateOrdersActivity.creatingNewOrder == false display first order

			//datePickerDialog = new DatePickerDialog(this, this, CurrentOrder.executionOrderDate.Year, CurrentOrder.executionOrderDate.Month, CurrentOrder.executionOrderDate.Day);
			//datePickerDialog.DatePicker.MinDate = (long)(DateTime.Now.AddDays(1) - new DateTime(1970, 1, 1)).TotalMilliseconds;

			//set btnDatePicker's text to be DateTime.Now
			//btnDatePicker.Text = CurrentOrder.executionOrderDate;

			btnDatePicker.Click += delegate
			{
				datePickerDialog = new DatePickerDialog(this, this, CurrentOrder.executionOrderDate.Year, CurrentOrder.executionOrderDate.Month - 1, CurrentOrder.executionOrderDate.Day);
				datePickerDialog.DatePicker.MinDate = DateTimeOffset.Now.AddDays(1).AddSeconds(-1).ToUnixTimeMilliseconds();
				datePickerDialog.Show();
			};

			btnRightArrow.Click += delegate
			{
				StateOrdersActivity.DisplayOrderPosition += 1;

				//enable disable btnRightArrow depending on if current order is last order or not 
				bool isLastOrder = StateOrdersActivity.DisplayOrderPosition == StateOrdersActivity.xmlOrders.Count - 1;
				btnRightArrow.Enabled = isLastOrder ? false : true;

				//enable disable btnLeftArrow depending on if current order is first order or not 
				bool isFirstOrder = StateOrdersActivity.DisplayOrderPosition == 0;
				btnLeftArrow.Enabled = isFirstOrder ? false : true;


				DisplayOrder(StateOrdersActivity.DisplayOrderPosition);

			};

			btnLeftArrow.Click += delegate
			{
				StateOrdersActivity.DisplayOrderPosition -= 1;

				//enable disable btnRightArrow depending on if current order is last order or not 
				bool isLastOrder = StateOrdersActivity.DisplayOrderPosition == StateOrdersActivity.xmlOrders.Count - 1;
				btnRightArrow.Enabled = isLastOrder ? false : true;

				//enable disable btnLeftArrow depending on if current order is first order or not 
				bool isFirstOrder = StateOrdersActivity.DisplayOrderPosition == 0;
				btnLeftArrow.Enabled = isFirstOrder ? false : true;

				DisplayOrder(StateOrdersActivity.DisplayOrderPosition);
			};

			int lastOrderNumber;
			string orderNumber;
			btnNewOrder.Click += delegate
			{
				//check if user is currently creating new order on clicking "Нова"
				if (StateOrdersActivity.creatingNewOrder == true)
				{
					//ask user if he is sure he wants to start creating new order, 
					//the data of the order he was currently creating will be lost
					AlertDialog.Builder alertClearCurrentOrderData = new AlertDialog.Builder(this);
					alertClearCurrentOrderData.SetMessage("Сигурни ли сте че искате да създадете нова поръчка? \nДанните за текущата ще бъдат загубени");
					alertClearCurrentOrderData.SetPositiveButton("ДA", delegate
					{
						StateOrdersActivity.creatingNewOrder = false;
						ClearScreenData();
						btnNewOrder.PerformClick();
					});
					alertClearCurrentOrderData.SetNegativeButton("НЕ", delegate
					{
						alertClearCurrentOrderData.Dispose();
					});
					alertClearCurrentOrderData.Create().Show();
				}
				else
				{
					//clear screen data from OrdersActivity
					ClearScreenData();

					//Enable-Disable buttons on "Нова" button pressed
					StateOrdersActivity.creatingNewOrder = true;
					btnDatePicker.Text = CurrentOrder.executionOrderDate.ToString("dd.MM.yyyy");
					EnableDisableButtons();

					//create order number
					if (StateOrdersActivity.dataFolderNumOrders == 0)
					{
						//get last order number from MobileSell/object.xml and set it to textViewOrderNumber
						XmlDocument doc = new XmlDocument();
						doc.Load(GlobalVariables.mobileSellFolderPath + "/object.xml");
						XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
						nsmgr.AddNamespace("ab", "http://tempuri.org/DataSet1.xsd");
						string f1 = doc.SelectSingleNode("//ab:f1", nsmgr).InnerText.ToString();
						lastOrderNumber = Convert.ToInt32(f1);
						orderNumber = (lastOrderNumber + 1).ToString();
						//set order number to global variable CurrentOrder.orderNumber so its value is not lost when going to another activities
						CurrentOrder.orderNumber = orderNumber.ToString();
						//textViewOrderNumber.Text = CurrentOrder.orderNumber;

						////get DateTime.Now + Hour and set it to global variable CurrentOrder.orderDateAndHour so its value is not lost when going to another activities
						//CurrentOrder.orderDateAndHour = DateTime.Now.ToString("dd.MM.yyyy  HH:mm");
						//textViewOrderDateAndHour.Text = CurrentOrder.orderDateAndHour;
					}
					else
					{
						//Всеки следващ е името на първия + 1
						//get xml file name with the biggest name 
						string biggestXmlFileName = Directory.GetFiles(GlobalVariables.dataFolderPath, "*.xml").Select(System.IO.Path.GetFileNameWithoutExtension).
										OrderByDescending(f => f).FirstOrDefault();
						CurrentOrder.orderNumber = (Convert.ToInt32(biggestXmlFileName) + 1).ToString();

					}

					textViewOrderNumber.Text = CurrentOrder.orderNumber;

					//set execution date
					//CurrentOrder.executionOrderDate = DateTime.Now.AddDays(1).ToString("dd.MM.yyyy");
					//btnDatePicker.Text = CurrentOrder.executionOrderDate;

					//get DateTime.Now + Hour and set it to global variable CurrentOrder.orderDateAndHour so its value is not lost when going to another activities
					CurrentOrder.orderDateAndHour = DateTime.Now.ToString("dd.MM.yyyy  HH:mm");
					textViewOrderDateAndHour.Text = CurrentOrder.orderDateAndHour;
				}

			};

			btnSelectCustomer.Click += delegate
			{
				//desable btnSelectCustomer so user doesn't press it a second time while activity is loading
				//because it will throw exception
				btnSelectCustomer.Enabled = false;
				StartActivity(typeof(SelectCustomerActivity));
			};

			btnNewRow.Click += delegate
			{
				StartActivity(typeof(ArticlesListActivity));
			};

			btnSaveOrder.Click += delegate
			{
				//check if all data is entered by the user
				if (textViewCustomerName.Text != "" && btnDatePicker.Text != "" && orderArticlesAdapter.listOfOrderArticles.Count != 0)
				{
					string xmlFullFilePath = GlobalVariables.dataFolderPath + "/" + textViewOrderNumber.Text + ".xml";
					try
					{
						//SAVE ORDER IN XML FILE
						//get the time when order creation was started
						string orderCreationStarted = textViewOrderDateAndHour.Text;
						//set textViewOrderDateAndHour.Text to be Datetime.Now
						string orderCreationEnded = DateTime.Now.ToString("dd.MM.yyyy  HH:mm");
						textViewOrderDateAndHour.Text = orderCreationEnded;
						//get interval between start creating order and end creating order
						string interval = orderCreationStarted + " - " + textViewOrderDateAndHour.Text;

						//set textViewOrderDateAndHour to DateTime.Now
						XmlWriterSettings xmlSettings = new XmlWriterSettings();
						xmlSettings.Indent = true;
						//get name and id from object.xml
						XmlDocument doc = new XmlDocument();
						doc.Load(GlobalVariables.mobileSellFolderPath + "/object.xml");
						XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
						nsmgr.AddNamespace("ab", "http://tempuri.org/DataSet1.xsd");
						string id = doc.SelectSingleNode("//ab:id", nsmgr).InnerText.ToString();
						string name = doc.SelectSingleNode("//ab:name", nsmgr).InnerText.ToString();

						using (XmlWriter writer = XmlWriter.Create(xmlFullFilePath, xmlSettings))
						{
							writer.WriteStartElement("xmlOrder");
							writer.WriteStartElement("DemandHeader");
							writer.WriteElementString("id", textViewOrderNumber.Text);
							writer.WriteElementString("sale_id", id);
							writer.WriteElementString("sell_name", name);
							writer.WriteElementString("datetime", textViewOrderDateAndHour.Text);
							writer.WriteElementString("typedoc", "2");
							writer.WriteElementString("docname", "ПОРЪЧКА");
							writer.WriteElementString("cust_id", textViewCustomerId.Text);
							writer.WriteElementString("cust_name", textViewCustomerName.Text);
							writer.WriteElementString("worktime", interval);
							writer.WriteElementString("tp", settings.Sales.TpId);
							writer.WriteElementString("km", "");
							writer.WriteElementString("paysum", "0");
							writer.WriteElementString("f1", "");
							writer.WriteElementString("f2", btnDatePicker.Text);
							writer.WriteElementString("f3", "");
							writer.WriteElementString("paytype", "1");
							writer.WriteElementString("invoiceid", "");
							writer.WriteElementString("bonid", "");
							writer.WriteEndElement();
							//write articles into xml file
							foreach (OrderArticleViewModel orderArticle in orderArticlesAdapter.listOfOrderArticles)
							{
								//get article price without discount
								decimal articlePriceWithoutDiscount = DatabaseRequest.GetAllFromTable<Article>()
																						.Where(x => x.Id == orderArticle.ArticleId)
																						.Select(x => x.SellPrice).FirstOrDefault();
								writer.WriteStartElement("Demand");
								writer.WriteElementString("id", CurrentOrder.orderNumber);
								writer.WriteElementString("artid", orderArticle.ArticleId);
								writer.WriteElementString("name", orderArticle.ArticleName);
								writer.WriteElementString("quantity", orderArticle.ArticleQuantity);
								writer.WriteElementString("price", articlePriceWithoutDiscount.ToString());
								writer.WriteElementString("sum", orderArticle.ArticleTotalPrice);
								writer.WriteElementString("discount", orderArticle.PriceDiscount);
								writer.WriteElementString("pprice", orderArticle.ArticlePrice);
								writer.WriteElementString("certificate", "");
								writer.WriteElementString("f1", "");
								writer.WriteElementString("f2", "");
								writer.WriteElementString("f3", "");
								writer.WriteEndElement();

							}
							writer.WriteEndElement();
							writer.Flush();
						}

						////add created order in xmlOrders list
						//XmlSerializer deserializer = new XmlSerializer(typeof(XmlOrder));
						//TextReader textReader = new StreamReader(xmlFullFilePath);
						//XmlOrder xmlOrder = (XmlOrder)deserializer.Deserialize(textReader);
						//StateOrdersActivity.xmlOrders.Add(xmlOrder);
						//textReader.Close();

						//Decrease quantity in tables article and sertif 
						DatabaseRequest.UpdateQuantities(orderArticlesAdapter.listOfOrderArticles);
						//say to user "Записа беще успешен"
						AlertDialog.Builder alertSaveSuccess = new AlertDialog.Builder(this);
						alertSaveSuccess.SetMessage("Поръчката беше записана успешно");
						alertSaveSuccess.SetPositiveButton("OK", delegate
						{
							//clear screen data from OrdersActivity
							ClearScreenData();

							//add created order in xmlOrders list
							XmlSerializer deserializer = new XmlSerializer(typeof(XmlOrder));
							TextReader textReader = new StreamReader(xmlFullFilePath);
							XmlOrder xmlOrder = (XmlOrder)deserializer.Deserialize(textReader);
							StateOrdersActivity.xmlOrders.Add(xmlOrder);
							textReader.Close();

							//set state ordersactivity creating new to false
							StateOrdersActivity.creatingNewOrder = false;

							//increase number of orders created by 1
							StateOrdersActivity.dataFolderNumOrders += 1;

							//set first order to be displayed
							StateOrdersActivity.DisplayOrderPosition = 0;

							//Recreate();
							EnableDisableButtons();
							DisplayOrder(StateOrdersActivity.DisplayOrderPosition);
							//Finish();
							alertSaveSuccess.Dispose();
						});
						alertSaveSuccess.Create().Show();
					}
					catch (InsufficientQuantityException ex)
					{
						//if error occurs during creating xml or decreasing the data in the database, delete created xml
						if (File.Exists(xmlFullFilePath))
						{
							File.Delete(xmlFullFilePath);
						}
						//display error message "Insuficent quantity"
						AlertDialog.Builder alertInsufficientQuantity = new AlertDialog.Builder(this);
						alertInsufficientQuantity.SetMessage(ex.Message);
						alertInsufficientQuantity.SetPositiveButton("OK", delegate
						{
							alertInsufficientQuantity.Dispose();
						});
						alertInsufficientQuantity.Create().Show();
					}
					catch (Exception ex)
					{
						//if error occurs during creating xml or decreasing the data in the database, delete created xml
						if (File.Exists(xmlFullFilePath))
						{
							File.Delete(xmlFullFilePath);
						}
						//display error message "Insuficent quantity"
						AlertDialog.Builder alertError = new AlertDialog.Builder(this);
						string errorMessage = "Грешка: " + ex.Message + "\n" + "поръчката не беше записана";
						alertError.SetMessage(errorMessage);
						alertError.SetPositiveButton("OK", delegate
						{
							alertError.Dispose();
						});
						alertError.Create().Show();
					}

				}
				else
				{
					//if some data is missing display error message
					//create error message
					string errorMessage = "Моля въведете: ";
					errorMessage += String.IsNullOrEmpty(textViewCustomerName.Text) ? "име на клиент, " : "";
					errorMessage += String.IsNullOrEmpty(btnDatePicker.Text) ? "дата на изпълнение на поръчка, " : "";
					errorMessage += orderArticlesAdapter.listOfOrderArticles.Count == 0 ? "артикул" : "";
					errorMessage = errorMessage.TrimEnd(',', ' ');

					//display error message
					AlertDialog.Builder alertDataMissing = new AlertDialog.Builder(this);
					alertDataMissing.SetMessage(errorMessage);
					alertDataMissing.SetPositiveButton("OK", delegate
					{
						alertDataMissing.Dispose();
					});
					alertDataMissing.Create().Show();
				}
			};


		}

		/// <summary>
		/// Enable-Disable buttons depending on if data folder is empty or if it has one or more orders in it
		/// is empty and if I'm currently creating new order or not
		/// </summary>
		private void EnableDisableButtons()
		{
			if (StateOrdersActivity.creatingNewOrder == true)
			{

				btnSelectCustomer.Enabled = true;
				btnDatePicker.Enabled = true;
				btnLeftArrow.Enabled = false;
				btnRightArrow.Enabled = false;
				btnBon.Enabled = false;
				btnInvoice.Enabled = false;
				btnNewOrder.Enabled = true;
				btnSaveOrder.Enabled = true;
				btnNewRow.Enabled = true;
			}

			if (StateOrdersActivity.creatingNewOrder == false)
			{
				//if data folder is empty 
				if (StateOrdersActivity.dataFolderNumOrders == 0)
				{
					btnSelectCustomer.Enabled = false;
					btnDatePicker.Enabled = false;
					btnLeftArrow.Enabled = false;
					btnRightArrow.Enabled = false;
					btnBon.Enabled = false;
					btnInvoice.Enabled = false;
					btnNewOrder.Enabled = true;
					btnSaveOrder.Enabled = false;
					btnNewRow.Enabled = false;
				}

				//if there is only one order in data folder
				if (StateOrdersActivity.dataFolderNumOrders == 1)
				{
					btnSelectCustomer.Enabled = false;
					btnDatePicker.Enabled = false;
					btnLeftArrow.Enabled = false;
					btnRightArrow.Enabled = false;
					btnBon.Enabled = true;
					btnInvoice.Enabled = true;
					btnNewOrder.Enabled = true;
					btnSaveOrder.Enabled = false;
					btnNewRow.Enabled = false;
				}

				//if there is more than one order in data folder
				if (StateOrdersActivity.dataFolderNumOrders > 1)
				{
					btnSelectCustomer.Enabled = false;
					btnDatePicker.Enabled = false;
					btnLeftArrow.Enabled = false;
					btnRightArrow.Enabled = true;
					btnBon.Enabled = true;
					btnInvoice.Enabled = true;
					btnNewOrder.Enabled = true;
					btnSaveOrder.Enabled = false;
					btnNewRow.Enabled = false;
				}
			}

		}

		/// <summary>
		/// Displays order on screen
		/// </summary>
		/// <param name="position">the position of the order in folder</param>
		private void DisplayOrder(int position)
		{
			textViewCustomerName.Text = StateOrdersActivity.xmlOrders[position].DemandHeader.CustName;
			textViewOrderNumber.Text = StateOrdersActivity.xmlOrders[position].DemandHeader.Id;

			textViewOrderDateAndHour.Text = StateOrdersActivity.xmlOrders[position].DemandHeader.DateTime;
			btnDatePicker.Text = StateOrdersActivity.xmlOrders[position].DemandHeader.F2;

			orderArticlesLayoutManager = new LinearLayoutManager(this);
			orderArticlesRecyclerView.SetLayoutManager(orderArticlesLayoutManager);
			//map Demands to OrderArticlesViewModel
			List<OrderArticleViewModel> orderArticles = StateOrdersActivity.xmlOrders[position].Demands
																	.Select(x => new OrderArticleViewModel
																	{
																		ArticleId = x.ArtId,
																		ArticlePrice = x.Pprice,
																		ArticleName = x.Name,
																		ArticleQuantity = x.Quantity,
																		ArticleTotalPrice = x.Sum
																	}).ToList();
			orderArticlesAdapter = new OrderArticlesAdapter(orderArticles, this, false);
			orderArticlesRecyclerView.SetAdapter(orderArticlesAdapter);
			totalPriceAllArticlesTextView.Text = orderArticlesAdapter.CaltulateTotalPriceOfAllOrderArticles().ToString();
		}

		/// <summary>
		/// Clears all data from OrdersActivity
		/// </summary>
		private void ClearScreenData()
		{
			textViewCustomerName.Text = "";
			textViewCustomerId.Text = "";
			textViewOrderNumber.Text = "";
			textViewOrderDateAndHour.Text = "";
			CurrentOrder.orderCustomerName = "";
			CurrentOrder.executionOrderDate = DateTime.Now.AddDays(1);
			CurrentOrder.listOfOrderArticles = new List<OrderArticleViewModel>();
			orderArticlesLayoutManager = new LinearLayoutManager(this);
			orderArticlesRecyclerView.SetLayoutManager(orderArticlesLayoutManager);
			orderArticlesAdapter = new OrderArticlesAdapter(CurrentOrder.listOfOrderArticles, this);
			orderArticlesRecyclerView.SetAdapter(orderArticlesAdapter);
			totalPriceAllArticlesTextView.Text = "";
			btnDatePicker.Text = "";
		}

		public override void OnBackPressed()
		{
			//if we press back button while we are creating new order ask user if he is sure
			//and say to him that all data from current creating order will be lost
			if (StateOrdersActivity.creatingNewOrder == true)
			{
				AlertDialog.Builder alertClearCurrentOrderData = new AlertDialog.Builder(this);
				alertClearCurrentOrderData.SetMessage("Сигурни ли сте че искате да се върнете в главното меню? \nДанните за текущата поръчка ще бъдат загубени");
				alertClearCurrentOrderData.SetPositiveButton("ДA", delegate
				{
					CurrentOrder.listOfOrderArticles = new List<OrderArticleViewModel>();
					CurrentOrder.orderCustomerName = null;
					CurrentOrder.orderCustomerId = null;
					CurrentOrder.orderNumber = null;
					CurrentOrder.orderDateAndHour = null;
					CurrentOrder.executionOrderDate = DateTime.Now.AddDays(1);

					StateOrdersActivity.creatingNewOrder = false;
					StateOrdersActivity.DisplayOrderPosition = 0;
					StateOrdersActivity.xmlOrders = new List<XmlOrder>();
					
					StartActivity(typeof(MainActivity));
				});
				alertClearCurrentOrderData.SetNegativeButton("НЕ", delegate
				{
					alertClearCurrentOrderData.Dispose();
				});
				alertClearCurrentOrderData.Create().Show();

			}
			else
			{
				CurrentOrder.listOfOrderArticles = new List<OrderArticleViewModel>();
				CurrentOrder.orderCustomerName = null;
				CurrentOrder.orderCustomerId = null;
				CurrentOrder.orderNumber = null;
				CurrentOrder.orderDateAndHour = null;
				CurrentOrder.executionOrderDate = DateTime.Now.AddDays(1);

				StateOrdersActivity.creatingNewOrder = false;
				StateOrdersActivity.DisplayOrderPosition = 0;
				StateOrdersActivity.xmlOrders = new List<XmlOrder>();
				
				StartActivity(typeof(MainActivity));
			}

		}

		//hide keyboard when user taps outside EditText
		public override bool DispatchTouchEvent(MotionEvent ev)
		{
			if (ev.Action == MotionEventActions.Down)
			{
				var text = CurrentFocus as EditText;
				if (text != null)
				{
					var outRect = new Rect();
					text.GetGlobalVisibleRect(outRect);
					if (outRect.Contains((int)ev.RawX, (int)ev.RawY)) return base.DispatchTouchEvent(ev);
					text.ClearFocus();
					HideSoftKeyboard();
				}
			}
			return base.DispatchTouchEvent(ev);
		}

		protected void HideSoftKeyboard()
		{
			var inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
			inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);
		}
	}
}
