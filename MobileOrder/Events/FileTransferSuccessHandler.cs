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
	class FileTransferSuccessHandler
	{
		public event EventHandler<EventArgs> OnFileTransferSuccess;
		public void FireEvent()
		{
			if (OnFileTransferSuccess != null)
			{
				OnFileTransferSuccess(this, new EventArgs());
			}
		}
	}
}