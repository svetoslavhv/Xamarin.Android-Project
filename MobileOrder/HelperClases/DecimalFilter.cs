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
using Android.Text;

namespace MobileOrder.HelperClases
{
	public class DecimalFilter : Java.Lang.Object, IInputFilter
	{
		String regex = "[0-9]+((\\.[0-9]{0," + (2 - 1) + "})?)||(\\.)?";
		public DecimalFilter(int digitsAfterZero)
		{
			//mPattern = Pattern.compile("[0-9]+((\\.[0-9]{0," + (digitsAfterZero - 1) + "})?)||(\\.)?"); 
			regex = "^[0-9]+(.[0-9]{0," + (digitsAfterZero - 1) + "})?$";

		}

		public Java.Lang.ICharSequence FilterFormatted(Java.Lang.ICharSequence source, int start, int end, ISpanned dest, int dstart, int dend)
		{

			if (System.Text.RegularExpressions.Regex.IsMatch(dest.ToString(), regex) || dest.ToString().Equals(""))
			{
				return new Java.Lang.String(source.ToString());
			}

			return new Java.Lang.String(string.Empty);
		}
	}
}