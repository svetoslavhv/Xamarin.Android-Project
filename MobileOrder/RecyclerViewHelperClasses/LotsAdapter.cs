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
	public class LotsAdapter : RecyclerView.Adapter
	{
		//public event EventHandler<string> ItemClick;
		public /*List<ArticleViewModel>*/ List<LotViewModel> listOfLots;
		private RadioButton lastCheckedRB = null;
		public string currentLotIdChecked;
		public LotsAdapter(/*List<ArticleViewModel>*/List<LotViewModel> lots)
		{
			this.listOfLots = lots;
		}

		public override int ItemCount
		{
			get { return listOfLots.Count; }
		}

		//void OnClick(int position)
		//{
		//	if (ItemClick != null)
		//		ItemClick(this, listOfArticles[position].Id);
		//}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			LotViewHolder vh = holder as LotViewHolder;

			// Set the TextViews in this ViewHolder's CardView 
			// from this position in the photo album:
			vh.lotNameExpireDate.Text = listOfLots[position].Name +"/"+ listOfLots[position].ExpireDate;
			vh.lotQuantity.Text = listOfLots[position].QuantityDisplay;
			vh.lotRadioButton.Text = listOfLots[position].LotId;
			
			vh.lotRadioButton.CheckedChange += LotRadioButton_CheckedChange;
			vh.lotRadioButton.Tag = position;
		}

		private void LotRadioButton_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			int tag = (int)radioButton.Tag;
			if (lastCheckedRB == null)
			{
				lastCheckedRB = radioButton;
			}
			else if (tag != (int)lastCheckedRB.Tag)
			{
				lastCheckedRB.Checked = false;
				lastCheckedRB = radioButton;
			}

			currentLotIdChecked = lastCheckedRB.Text;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View itemView = LayoutInflater.From(parent.Context).
						Inflate(Resource.Layout.LotCardView, parent, false);

			// Create a ViewHolder to find and hold these view references, and 
			// register OnClick with the view holder:
			LotViewHolder vh = new LotViewHolder(itemView);
			return vh;
		}
	}
}