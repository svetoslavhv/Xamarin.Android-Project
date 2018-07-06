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
using MobileOrder.Model;
using Android.Support.V7.Widget;

namespace MobileOrder.RecyclerViewHelperClasses
{
	public class CustomersAdapter : RecyclerView.Adapter
	{
		public event EventHandler<Customer> ItemClick;
		public /*List<ArticleViewModel>*/ List<Customer> listOfCustomers;
		public CustomersAdapter(/*List<ArticleViewModel>*/List<Customer> customers)
		{
			listOfCustomers = customers;
		}

		public override int ItemCount
		{
			get { return listOfCustomers.Count; }
		}

		void OnClick(int position)
		{
			if (ItemClick != null)
				ItemClick(this, listOfCustomers[position]);
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			CustomerViewHolder vh = holder as CustomerViewHolder;

			// Set the TextViews in this ViewHolder's CardView 
			// from this position in the list of customers:
			vh.customerName.Text = listOfCustomers[position].Company;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View itemView = LayoutInflater.From(parent.Context).
						Inflate(Resource.Layout.CustomerCardView, parent, false);

			// Create a ViewHolder to find and hold these view references, and 
			// register OnClick with the view holder:
			CustomerViewHolder vh = new CustomerViewHolder(itemView, OnClick);
			return vh;
		}

		//private void OnClick(int obj)
		//{
		//	throw new NotImplementedException();
		//}
	}
}