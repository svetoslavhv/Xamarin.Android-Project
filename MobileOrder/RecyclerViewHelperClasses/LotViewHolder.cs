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
	public class LotViewHolder : RecyclerView.ViewHolder
	{
		public TextView lotNameExpireDate { get; private set; }
		public TextView lotQuantity { get; private set; }
		public RadioButton lotRadioButton { get; private set; }

		// Get references to the views defined in the CardView layout.
		public LotViewHolder(View itemView) 
            : base (itemView)
        {
			// Locate and cache view references:
			lotNameExpireDate = itemView.FindViewById<TextView>(Resource.Id.lotNameExpireDate);
			lotQuantity = itemView.FindViewById<TextView>(Resource.Id.lotQuantity);
			lotRadioButton = itemView.FindViewById<RadioButton>(Resource.Id.lotRadioButton);

			// Detect user clicks on the item view and report which item
			// was clicked (by layout position) to the listener:
			//itemView.Click += (sender, e) => listener(base.LayoutPosition);
		}
	}
}