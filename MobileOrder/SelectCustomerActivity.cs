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
using MobileOrder.RequestsToDatabaseMethods;
using Android.Support.V7.Widget;
using MobileOrder.RecyclerViewHelperClasses;
using MobileOrder.Data;

namespace MobileOrder
{
	[Activity(Label = "SelectCustomerActivity", WindowSoftInputMode = SoftInput.AdjustNothing)]
	public class SelectCustomerActivity : Activity
	{
		List<Customer> customers = new List<Customer>();
		
		// RecyclerView instance that displays the customers:
		RecyclerView customersRecyclerView;

		// Layout manager that lays out each card in the RecyclerView:
		RecyclerView.LayoutManager customersLayoutManager;

		// Adapter that accesses the data set (customers):
		CustomersAdapter customersAdapter;
		
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.SelectCustomer);
			
			Android.Widget.SearchView searchView = FindViewById<Android.Widget.SearchView>(Resource.Id.searchViewCustomers);

			//get all customers from database
			customers = DatabaseRequest.GetAllFromTable<Customer>().OrderBy(x => x.Company).ToList();

			//populate RecyclerView with all customers
			customersRecyclerView = FindViewById<RecyclerView>(Resource.Id.customersRecyclerView);
			customersLayoutManager = new LinearLayoutManager(this);
			customersRecyclerView.SetLayoutManager(customersLayoutManager);
			customersAdapter = new CustomersAdapter(customers);
			customersAdapter.ItemClick += OnItemClick;
			customersRecyclerView.SetAdapter(customersAdapter);

			searchView.QueryTextChange += delegate
			{
				string currentSearchViewValue = searchView.Query;
				var currentCustomersSearched = customers.Where(x => x.Company.ToLower().Contains(currentSearchViewValue.ToLower())).ToList();
				//repopulate RecyclerView with currentCustomersSearched
				customersAdapter = new CustomersAdapter(currentCustomersSearched);
				customersAdapter.ItemClick += OnItemClick;
				customersRecyclerView.SetAdapter(customersAdapter);
			};
		}

		void OnItemClick(object sender, Customer customer)
		{
			var ordersActivity = new Intent(this, typeof(OrdersActivity));
			//set selected customer name and id to global variables CurrentOrder.orderCustomerName and CurrentOrder.orderCustomerId
			CurrentOrder.orderCustomerId = customer.Id;
			CurrentOrder.orderCustomerName = customer.Company;
			StartActivity(ordersActivity);
			Finish();
		}

		public override void OnBackPressed()
		{
			StartActivity(typeof(OrdersActivity));
		}

	}
}