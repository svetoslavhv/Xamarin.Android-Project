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
	public class CustomerViewHolder : RecyclerView.ViewHolder
	{
		public TextView customerName { get; private set; }

		// Get references to the views defined in the CardView layout.
		public CustomerViewHolder(View itemView, Action<int> listener) 
            : base (itemView)
        {
			// Locate and cache view references:
			customerName = itemView.FindViewById<TextView>(Resource.Id.customerName);

			// Detect user clicks on the item view and report which item
			// was clicked (by layout position) to the listener:
			itemView.Click += (sender, e) => listener(base.LayoutPosition);
		}
	}
}