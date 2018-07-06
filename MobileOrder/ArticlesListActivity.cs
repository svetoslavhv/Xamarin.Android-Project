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
using Android.Graphics.Drawables;
using MobileOrder.RequestsToDatabaseMethods;
using MobileOrder.ViewModels;

namespace MobileOrder
{
	[Activity(Label = "ArticlesListActivity")]
	public class ArticlesListActivity : Activity
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

			SetContentView(Resource.Layout.ArticlesList);

			//get all groups from database
			//using (var connection = new SQLiteConnection(System.IO.Path.Combine(GlobalVariables.databasePath, "MobileSell.db")))
			//{
			//	var allGroups = connection.Table<Group>();
			//	groups.AddRange(allGroups);
			//}

			var allGroups = DatabaseRequest.GetAllFromTable<Group>();
			groups.AddRange(allGroups);

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

			//create line between recyclerview items
			DividerItemDecoration itemDecoration = new DividerItemDecoration(this, DividerItemDecoration.Vertical);
			//pass all articles to the RecyclerView
			articlesRecyclerView = FindViewById<RecyclerView>(Resource.Id.articlesRecyclerView);
			articlesRecyclerView.AddItemDecoration(itemDecoration);
			articlesLayoutManager = new LinearLayoutManager(this);
			articlesRecyclerView.SetLayoutManager(articlesLayoutManager);
			articlesAdapter = new ArticlesAdapter(allArticles);
			articlesAdapter.ItemClick += OnItemClick;
			articlesRecyclerView.SetAdapter(articlesAdapter);

			//get all articles from selected group
			spinner.ItemSelected += (s, e) =>
			{
				if (e.Position != -1)
				{
					var groupId = groups.ElementAt(e.Position).Id;

					//get all articles from group which is currently selected
					List<ArticleViewModel> articlesFromGroup = allArticles.Where(x => x.Group == groupId).ToList();

					//fill in the RecyclerView with articlesFromGroup;
					articlesAdapter = new ArticlesAdapter(articlesFromGroup);
					articlesAdapter.ItemClick += OnItemClick;
					articlesRecyclerView.SetAdapter(articlesAdapter);

				}
				else
				{
					//if no group is selected display all articles on screen
					articlesLayoutManager = new LinearLayoutManager(this);
					articlesRecyclerView.SetLayoutManager(articlesLayoutManager);
					articlesAdapter = new ArticlesAdapter(allArticles);
					articlesAdapter.ItemClick += OnItemClick;
					articlesRecyclerView.SetAdapter(articlesAdapter);
				}
			};
			
		}

		private void OnItemClick(object sender, string articleId)
		{
			////pass data to activity
			//StartActivity(typeof(InputQuantityAndLotActivity));
			////Open activity "Въвеждане на количество и партида"
			////get all lots for article
			//var lots = DatabaseRequest.GetAllFromTable<Sertif>().Where(x => x.ArtId == articleId);

			var inputQuantityAndLotActivity = new Intent(this, typeof(InputQuantityAndLotActivity));
			inputQuantityAndLotActivity.PutExtra("articleId", articleId);
			StartActivity(inputQuantityAndLotActivity);
			Finish();
		}

		public override void OnBackPressed()
		{
			StartActivity(typeof(OrdersActivity));
		}
	}
}