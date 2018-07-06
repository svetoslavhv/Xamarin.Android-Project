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
using MobileOrder.Model;
using MobileOrder.ViewModels;

namespace MobileOrder.RecyclerViewHelperClasses
{
	public class ArticlesAdapter : RecyclerView.Adapter
	{
		public event EventHandler<string> ItemClick;
		public /*List<ArticleViewModel>*/ List<ArticleViewModel> listOfArticles;
		public decimal totalSumAllArticles;
		public ArticlesAdapter(/*List<ArticleViewModel>*/List<ArticleViewModel> articles)
		{
			listOfArticles = articles;
		}

		public override int ItemCount
		{
			get { return listOfArticles.Count; }
		}

		void OnClick(int position)
		{
			if (ItemClick != null)
				ItemClick(this, listOfArticles[position].Id);
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			ArticleViewHolder vh = holder as ArticleViewHolder;

			// Set the TextViews in this ViewHolder's CardView 
			// from this position in the photo album:
			vh.articleName.Text = listOfArticles[position].Name;
			vh.articlePrice.Text = listOfArticles[position].SellPriceDisplay;
			vh.articleQuantity.Text = listOfArticles[position].QuantityDisplay;
			vh.sum.Text = listOfArticles[position].SumDisplay;
			totalSumAllArticles += listOfArticles[position].Sum;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View itemView = LayoutInflater.From(parent.Context).
						Inflate(Resource.Layout.ArticleCardView, parent, false);

			// Create a ViewHolder to find and hold these view references, and 
			// register OnClick with the view holder:
			ArticleViewHolder vh = new ArticleViewHolder(itemView, OnClick);
			return vh;
		}
		
	}
}