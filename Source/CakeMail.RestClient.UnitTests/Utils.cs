using System.Linq;

namespace CakeMail.RestClient.UnitTests
{
	public static class Utils
	{
		private const string CAKEMAIL_API_BASE_URI = "https://api.wbsrvc.com";

		public static string GetCakeMailApiUri(params object[] resources)
		{
			return resources.Aggregate(CAKEMAIL_API_BASE_URI, (current, path) => $"{current.TrimEnd('/')}/{path.ToString().TrimStart('/')}");
		}
	}
}
