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

namespace MobileOrder.CustomExceptions
{
	public class InsufficientQuantityException : Exception
	{
		public InsufficientQuantityException()
		{

		}

		public InsufficientQuantityException(string quantityDesired, string articleName, decimal quantityAvailable)
        : base("Искано количество "+ quantityDesired  + " кг. за артикул " + articleName + " е недостатъчно. Наличност " + quantityAvailable + " кг.")
		{

		}
	}
}