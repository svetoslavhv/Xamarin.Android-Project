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
using MobileOrder.ViewModels;
using MobileOrder.Data;
using Android.Text;
using MobileOrder.HelperClases;

namespace MobileOrder.RecyclerViewHelperClasses
{
	public class OrderArticlesAdapter : RecyclerView.Adapter
	{
		public List<OrderArticleViewModel> listOfOrderArticles;
		public decimal totalPriceAllArticles = 0;
		private Context currentContext;
		private bool editTextEnabled;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="orderArticles">The data source for RecyclerView</param>
		/// <param name="context">The current context</param>
		/// <param name="editEnabled">If we can edit the RecyclerView, it's true by default</param>
		public OrderArticlesAdapter(List<OrderArticleViewModel> orderArticles, Context context, bool editEnabled = true)
		{
			listOfOrderArticles = orderArticles;
			currentContext = context;
			editTextEnabled = editEnabled;
		}

		public override int ItemCount
		{
			get { return listOfOrderArticles.Count; }
		}


		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			OrderArticleViewHolder vh = holder as OrderArticleViewHolder;

			// Set the TextViews in this ViewHolder's CardView 
			// from this position in the photo album:
			vh.articleName.Text = listOfOrderArticles[position].ArticleName;
			vh.articlePrice.Text = listOfOrderArticles[position].ArticlePrice;
			vh.articleQuantity.Text = listOfOrderArticles[position].ArticleQuantity;
			vh.articleTotalPrice.Text = listOfOrderArticles[position].ArticleTotalPrice;

			//enable disable EditTexts depending on if we are creating order or we are looking at created orders
			vh.articlePrice.Enabled = editTextEnabled;
			vh.articleQuantity.Enabled = editTextEnabled;
			
			//set limit of numbers to input after the decimal separator
			vh.articlePrice.SetFilters(new IInputFilter[] { new DecimalFilter(4) });
			//calculate new article total price as user types value in articlePrice
			vh.articlePrice.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {
				listOfOrderArticles[position].ArticlePrice = e.Text.ToString();
				decimal articlePrice /*= Convert.ToDecimal(listOfOrderArticles[position].ArticlePrice)*/;
				if (!Decimal.TryParse(listOfOrderArticles[position].ArticlePrice, out articlePrice))
				{
					articlePrice = 0;
				}
				decimal articleQuantity = Convert.ToDecimal(listOfOrderArticles[position].ArticleQuantity);
				listOfOrderArticles[position].ArticleTotalPrice = Math.Round(articlePrice * articleQuantity, 2).ToString();
				vh.articleTotalPrice.Text = listOfOrderArticles[position].ArticleTotalPrice.ToString();
				//calculate total price of all articles and set it to 

				TextView totalPriceAllArticlesTextView = (TextView)((Activity)currentContext).FindViewById<TextView>(Resource.Id.totalPriceAllArticlesTextView);
				totalPriceAllArticlesTextView.Text = CaltulateTotalPriceOfAllOrderArticles().ToString();
			};

			//set limit of numbers to input after the decimal separator
			vh.articleQuantity.SetFilters(new IInputFilter[] { new DecimalFilter(3) });
			//calculate new article total price as user types value in articleQuantity
			vh.articleQuantity.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {
				listOfOrderArticles[position].ArticleQuantity = e.Text.ToString();
				decimal articlePrice = Convert.ToDecimal(listOfOrderArticles[position].ArticlePrice);
				decimal articleQuantity /*= Convert.ToDecimal(listOfOrderArticles[position].ArticleQuantity)*/;
				//if entered value is not a number we set articleQuantity to 0
				if (!Decimal.TryParse(listOfOrderArticles[position].ArticleQuantity, out articleQuantity))
				{
					articleQuantity = 0;
				};
				listOfOrderArticles[position].ArticleTotalPrice = Math.Round(articlePrice * articleQuantity, 2).ToString();
				vh.articleTotalPrice.Text = listOfOrderArticles[position].ArticleTotalPrice.ToString();
				TextView totalPriceAllArticlesTextView = (TextView)((Activity)currentContext).FindViewById<TextView>(Resource.Id.totalPriceAllArticlesTextView);
				totalPriceAllArticlesTextView.Text = CaltulateTotalPriceOfAllOrderArticles().ToString();
			};
			
		}
		
		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View itemView = LayoutInflater.From(parent.Context).
						Inflate(Resource.Layout.OrderArticleCardView, parent, false);
			// Create a ViewHolder to find and hold these view references, and 
			// register OnClick with the view holder:
			OrderArticleViewHolder vh = new OrderArticleViewHolder(itemView);
			return vh;
		}

		//calculate the total price of all articles in RecyclerView
		public decimal CaltulateTotalPriceOfAllOrderArticles()
		{
			decimal totalPriceOfAllArticles = 0;
			foreach(OrderArticleViewModel orderArticle in listOfOrderArticles)
			{
				totalPriceOfAllArticles += Convert.ToDecimal(orderArticle.ArticleTotalPrice);
			}

			totalPriceOfAllArticles = Math.Round(totalPriceOfAllArticles,2);

			return totalPriceOfAllArticles;
		}
	}
}