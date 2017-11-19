namespace CakeMail.RestClient.Utilities
{
	/// <summary>
	/// Various constants used in CakeMail.RestClient
	/// </summary>
	internal static class Constants
	{
		/// <summary>
		/// The string representation of a null DateTime as definied by CakeMail
		/// </summary>
		public const string EMPTY_CAKEMAIL_DATE = "0000-00-00 00:00:00";

		/// <summary>
		/// The string format of DateTime values sent to the (and received from) the CakeMail API
		/// </summary>
		public const string CAKEMAIL_DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
	}
}
