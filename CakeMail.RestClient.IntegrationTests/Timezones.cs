using System;
using System.Linq;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class TimezonesTests
	{
		public static void ExecuteAllMethods(CakeMailRestClient api)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing TIMEZONES methods...");

			var timezones = api.Timezones.GetAll();
			Console.WriteLine("Retrieved all timezones. There are {0} timezones.", timezones.Count());

			var utcTimezones = timezones.Where(tz => tz.Name.Contains("UTC")).ToArray();
			Console.WriteLine("The following timezones contain the word UTC in their name:");
			Console.WriteLine(string.Join(", ", utcTimezones.Select(tz => string.Format("{0} ({1})", tz.Name, tz.Id))));

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}