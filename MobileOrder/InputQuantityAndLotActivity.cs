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
using Android.Support.V7.Widget;
using MobileOrder.RecyclerViewHelperClasses;
using MobileOrder.Model;
using MobileOrder.RequestsToDatabaseMethods;
using MobileOrder.ViewModels;
using System.Drawing;
using Android.Content.Res;
using Android.Text;
using MobileOrder.HelperClases;
using MobileOrder.Data;
using Android.Views.InputMethods;
using Android.Graphics;

namespace MobileOrder
{
	[Activity(Label = "InputQuantityAndLotActivity", Theme = "@style/Theme.AppCompat", WindowSoftInputMode = SoftInput.AdjustNothing)]
	public class InputQuantityAndLotActivity : Activity
	{
		// RecyclerView instance that displays the articles:
		RecyclerView lotsRecyclerView;

		// Layout manager that lays out each card in the RecyclerView:
		RecyclerView.LayoutManager lotsLayoutManager;

		// Adapter that accesses the data set (articles):
		LotsAdapter lotsAdapter;

		//Articles that are managed by the adapter:
		//List<ArticleViewModel> articlesFromGroup;
		List<LotViewModel> lots;

		Button btnSelect;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.InputQuantityAndLot);

			//get id of selected article
			var articleId = Intent.GetStringExtra("articleId");

			//get article name and price from selected article and set it its TextViews
			var article = DatabaseRequest.GetAllFromTable<Article>()
				.Where(x => x.Id == articleId).FirstOrDefault();

			TextView articleName = FindViewById<TextView>(Resource.Id.articleName);
			articleName.Text = article.Name;

			TextView articlePriceTextView = FindViewById<TextView>(Resource.Id.articlePriceTextView);
			articlePriceTextView.Text = article.SellPriceDisplay;

			TextView articlePriceAfterDiscountTextView = FindViewById<TextView>(Resource.Id.articlePriceAfterDiscountTextView);
			articlePriceAfterDiscountTextView.Text = articlePriceTextView.Text;

			TextView totalPriceTextView = FindViewById<TextView>(Resource.Id.totalPriceTextView);
			EditText quantityEditText = FindViewById<EditText>(Resource.Id.quantityEditText);

			EditText discountEditText = FindViewById<EditText>(Resource.Id.discountEditText);
			discountEditText.SetFilters(new IInputFilter[] { new DecimalFilter(2) });
			discountEditText.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {
				//calculate article price after discount applied and set it to articlePriceAfterDiscountTextView
				decimal articlePrice = Convert.ToDecimal(articlePriceTextView.Text);
				decimal discountPercentage;
				if (!Decimal.TryParse(e.Text.ToString(), out discountPercentage))
				{
					discountPercentage = 0;
				}
				decimal articleQuantity;
				if (!Decimal.TryParse(quantityEditText.Text, out articleQuantity))
				{
					articleQuantity = 0;
				};
				decimal discountAmount = Math.Round(articlePrice * (discountPercentage / 100), 4);
				decimal articlePriceAfterDiscount = articlePrice - discountAmount; 
				articlePriceAfterDiscountTextView.Text = articlePriceAfterDiscount.ToString();
				//calculate total price
				decimal totalPrice = Math.Round(articlePriceAfterDiscount * articleQuantity, 2);
				totalPriceTextView.Text = totalPrice.ToString();
			};
			
			//EditText quantityEditText = FindViewById<EditText>(Resource.Id.quantityEditText);
			quantityEditText.SetFilters(new IInputFilter[] { new DecimalFilter(3) });
			quantityEditText.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {
				//calculate total price and set it to totalPriceTextView
				decimal articlePriceAfterDiscount = Convert.ToDecimal(articlePriceAfterDiscountTextView.Text);
				decimal articleQuantity;
				decimal totalPrice;
				//if entered value is not a number we set articleQuantity to 0
				if (!Decimal.TryParse(e.Text.ToString(), out articleQuantity))
				{
					articleQuantity = 0;
				};

				totalPrice = Math.Round(articlePriceAfterDiscount * articleQuantity, 2);
				totalPriceTextView.Text = totalPrice.ToString();
			};
			
			//get all lots of selected article
			lots = DatabaseRequest.GetAllFromTable<Sertif>()
				.Where(x => x.ArtId == articleId)
				.Select(x => new LotViewModel
				{
					Name = x.Name,
					ExpireDate = x.ExpireDate,
					QuantityDisplay = x.QuantityDisplay,
					LotId = x.Lotid,
				}).ToList();
			
			//fill in the RecyclerView with articles
			lotsRecyclerView = FindViewById<RecyclerView>(Resource.Id.lotRecyclerView);
			lotsLayoutManager = new LinearLayoutManager(this);
			lotsRecyclerView.SetLayoutManager(lotsLayoutManager);
			lotsAdapter = new LotsAdapter(lots);
			lotsRecyclerView.SetAdapter(lotsAdapter);

			btnSelect = FindViewById<Button>(Resource.Id.btnSelect);

			btnSelect.Click += delegate
			{
				//Check if we have lot selected if not show alert message saying that we have to choose some lot
				if (!String.IsNullOrEmpty(lotsAdapter.currentLotIdChecked) || lotsAdapter.listOfLots.Count == 0)
				{
					//Check if quantity typed from user is equal or greater ot equal to 0 than the quantity of selected lot
					//if not display alert message saying: "Entered quantity is less that lot quantity"
					decimal enteredQuantity = !String.IsNullOrEmpty(quantityEditText.Text) ? Convert.ToDecimal(quantityEditText.Text) : 0m;

					decimal quantity;
					bool hasLots;
					if (lotsAdapter.listOfLots.Count != 0)
					{
						hasLots = true;
						decimal selectedLotQuantity = DatabaseRequest.GetAllFromTable<Sertif>()
																				.Where(x => x.Lotid == lotsAdapter.currentLotIdChecked)
																				.Select(x => x.Quantity).FirstOrDefault();
						quantity = selectedLotQuantity;
					}
					else {
						hasLots = false;
						decimal articleQuantity = DatabaseRequest.GetAllFromTable<Article>()
																				.Where(x => x.Id == articleId)
																				.Select(x => x.Quantity).FirstOrDefault();
						quantity = articleQuantity;
					}
					if(enteredQuantity <= quantity && enteredQuantity > 0)
					{
						//redirect to OrdersActivity with articleId, lotid, price after discount, quantity
						var ordersActivity = new Intent(this, typeof(OrdersActivity));
						CurrentOrder.listOfOrderArticles.Add(
							new OrderArticleViewModel
							{
								ArticleId = articleId,
								LotId = lotsAdapter.currentLotIdChecked,
								ArticleName = articleName.Text,
								PriceDiscount = discountEditText.Text,
								ArticlePrice = articlePriceAfterDiscountTextView.Text,
								ArticleQuantity = quantityEditText.Text,
								ArticleTotalPrice = totalPriceTextView.Text
							});
						StartActivity(ordersActivity);
					}
					else
					{
						AlertDialog.Builder alertQuantityNotValid = new AlertDialog.Builder(this);
						alertQuantityNotValid.SetMessage("Въведеното количество е невалидно");
						alertQuantityNotValid.SetPositiveButton("OK", delegate
						{
							alertQuantityNotValid.Dispose();
						});
						alertQuantityNotValid.Create().Show();
					}
				}
				else
				{
					AlertDialog.Builder alertLotNotChoosen = new AlertDialog.Builder(this);
					alertLotNotChoosen.SetMessage("Моля изберете партида");
					alertLotNotChoosen.SetPositiveButton("OK", delegate
					{
						alertLotNotChoosen.Dispose();
					});

					alertLotNotChoosen.Create().Show();
					
				}

				//Finish();
			};
			
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

		public override void OnBackPressed()
		{
			StartActivity(typeof(ArticlesListActivity));
		}

	}
}