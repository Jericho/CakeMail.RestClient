using System;

namespace CakeMail.RestClient.Utilities
{
	public static class ExtensionMethods
	{
		public static string ToCakeMailString(this DateTime value)
		{
			if (value == DateTime.MinValue) return Constants.EMPTY_CAKEMAIL_DATE;
			return value.ToString(Constants.CAKEMAIL_DATE_FORMAT);
		}
	}
}