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
	public class ArticleViewHolder : RecyclerView.ViewHolder
	{
		public TextView articleName { get; private set; }
		public TextView articlePrice { get; private set; }
		public TextView articleQuantity { get; private set; }
		public TextView sum { get; private set; }

		// Get references to the views defined in the CardView layout.
		public ArticleViewHolder(View itemView, Action<int> listener) 
            : base (itemView)
        {
			// Locate and cache view references:
			articleName = itemView.FindViewById<TextView>(Resource.Id.articleName);
			articlePrice = itemView.FindViewById<TextView>(Resource.Id.articlePrice);
			articleQuantity = itemView.FindViewById<TextView>(Resource.Id.articleQuantity);
			sum = itemView.FindViewById<TextView>(Resource.Id.sum);

			// Detect user clicks on the item view and report which item
			// was clicked (by layout position) to the listener:
			itemView.Click += (sender, e) => listener(base.LayoutPosition);
		}
	}
}