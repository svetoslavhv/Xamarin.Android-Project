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

namespace MobileOrder.RecyclerViewHelperClasses
{
	public class OrderArticleViewHolder : RecyclerView.ViewHolder
	{
		public TextView articleName { get; private set; }
		public EditText articlePrice { get; private set; }
		public EditText articleQuantity { get; private set; }
		public TextView articleTotalPrice { get; private set; }
		public TextView totalPriceAllArticles { get; private set; }

		// Get references to the views defined in the CardView layout.
		public OrderArticleViewHolder(View itemView)
			: base(itemView)
		{
			// Locate and cache view references:
			articleName = itemView.FindViewById<TextView>(Resource.Id.articleName);
			articlePrice = itemView.FindViewById<EditText>(Resource.Id.articlePrice);
			articleQuantity = itemView.FindViewById<EditText>(Resource.Id.articleQuantity);
			articleTotalPrice = itemView.FindViewById<TextView>(Resource.Id.articleTotalPrice);
			totalPriceAllArticles = itemView.FindViewById<TextView>(Resource.Id.totalPriceAllArticlesTextView);
		}
		
	}
}