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

namespace MobileOrder.Events
{
	class ImportFinishedEventHandler
	{
		public event EventHandler<EventArgs> OnImportFinished;
		public void FireEvent()
		{
			if (OnImportFinished != null)
			{
				OnImportFinished(this, new EventArgs());
			}
		}
	}
}