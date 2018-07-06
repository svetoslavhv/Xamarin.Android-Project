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
using FR.Ganfra.Materialspinner;
using Android.Support.V7.Widget;
using MobileOrder.RecyclerViewHelperClasses;
using MobileOrder.Model;
using SQLite;
using MobileOrder.ViewModels;
using Android.Graphics.Drawables;
using MobileOrder.Globals;
using MobileOrder.RequestsToDatabaseMethods;

namespace MobileOrder
{
	//TODO: set SellPrice to have 2 numbers after decimal separator 
	//TODO: set Quantity to have 3 numbers after decimal separator
	[Activity(Label = "InventoryActivity")]
	public class InventoryActivity : Activity
	{
		MaterialSpinner spinner;

		List<Group> groups = new List<Group>();

		ArrayAdapter<Group> adapter;

		// RecyclerView instance that displays the articles:
		RecyclerView articlesRecyclerView;

		// Layout manager that lays out each card in the RecyclerView:
		RecyclerView.LayoutManager articlesLayoutManager;

		// Adapter that accesses the data set (articles):
		ArticlesAdapter articlesAdapter;

		//Articles that are managed by the adapter:
		//List<ArticleViewModel> articlesFromGroup;
		List<ArticleViewModel> allArticles;


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Inventory);

			//get all groups from database
			using (var connection = new SQLiteConnection(System.IO.Path.Combine(GlobalVariables.databasePath, "MobileSell.db")))
			{
				var allGroups = connection.Table<Group>();
				groups.AddRange(allGroups);
			}

			//fill in spinner with items
			spinner = FindViewById<MaterialSpinner>(Resource.Id.spinner);
			adapter = new ArrayAdapter<Group>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, groups);
			adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spinner.Adapter = adapter;

			spinner.Background = Drawable.CreateFromXml(Resources, Resources.GetXml(Resource.Drawable.material_spinner_border));

			//on activity start get all articles
			allArticles = DatabaseRequest.GetAllFromTable<Article>().Select(x =>
																	new ArticleViewModel
																	{
																		Id = x.Id,
																		Group = x.Group,
																		Name = x.Name,
																		SellPrice = x.SellPrice,
																		Quantity = x.Quantity
																	}).ToList();

			//pass all articles to the RecyclerView
			articlesRecyclerView = FindViewById<RecyclerView>(Resource.Id.articlesRecyclerView);
			articlesLayoutManager = new LinearLayoutManager(this);
			articlesRecyclerView.SetLayoutManager(articlesLayoutManager);
			articlesAdapter = new ArticlesAdapter(allArticles);
			articlesRecyclerView.SetAdapter(articlesAdapter);

			TextView totalValue = FindViewById<TextView>(Resource.Id.totalValue);
			//calculate total sum of all articles in RecyclerView and set it to totalValue TextView
			decimal totalSumAllArticles = articlesAdapter.listOfArticles.Sum(x => x.Sum);
			totalValue.Text = String.Format("{0:0.00}", totalSumAllArticles);

			//get all articles from selected group
			spinner.ItemSelected += (s, e) =>
			{
				if(e.Position != -1)
				{
					var groupId = groups.ElementAt(e.Position).Id;
					//get all articles from group which is currently selected
					List<ArticleViewModel> articlesFromGroup = allArticles.Where(x => x.Group == groupId).ToList();
					
					//fill in the RecyclerView with articlesFromGroup
					//articlesLayoutManager = new LinearLayoutManager(this);
					//articlesRecyclerView.SetLayoutManager(articlesLayoutManager);
					articlesAdapter = new ArticlesAdapter(articlesFromGroup);
					articlesRecyclerView.SetAdapter(articlesAdapter);

					//calculate total value of articles
					//decimal totalSum = 0;
					//foreach(ArticleViewModel article in articlesFromGroup)
					//{
					//	totalSum += article.Sum;
					//}

					//calculate total sum of all articles in RecyclerView and set it to totalValue TextView
					totalSumAllArticles = articlesAdapter.listOfArticles.Sum(x => x.Sum);
					totalValue.Text = String.Format("{0:0.00}", totalSumAllArticles);
				}
				else
				{
					//if no group is selected display all articles on screen
					//articlesLayoutManager = new LinearLayoutManager(this);
					//articlesRecyclerView.SetLayoutManager(articlesLayoutManager);
					articlesAdapter = new ArticlesAdapter(allArticles);
					articlesRecyclerView.SetAdapter(articlesAdapter);
					//calculate total sum of all articles in RecyclerView and set it to totalValue TextView
					totalSumAllArticles = articlesAdapter.listOfArticles.Sum(x => x.Sum);
					totalValue.Text = String.Format("{0:0.00}", totalSumAllArticles);
				}
				
			};

			Button btnPrint = FindViewById<Button>(Resource.Id.btnPrint);
			btnPrint.Click += delegate {
				StartActivity(typeof(PrintingActivity));
			};

		}
		
	}
	
}