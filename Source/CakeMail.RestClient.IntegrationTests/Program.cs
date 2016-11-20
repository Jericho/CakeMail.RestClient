using System;

namespace CakeMail.RestClient.IntegrationTests
{
	public class Program
	{
		public static void Main()
		{
			Console.WriteLine("{0} Executing all CakeMail API methods ... {0}", new string('=', 10));

			try
			{
				ExecuteAllMethods();
			}
			catch (Exception e)
			{
				Console.WriteLine("");
				Console.WriteLine("");
				Console.WriteLine("An error has occured: {0}", (e.InnerException ?? e).Message);
			}
			finally
			{
				// Clear the keyboard buffer
				while (Console.KeyAvailable)
				{
					Console.ReadKey();
				}

				Console.WriteLine("");
				Console.WriteLine("Press any key...");
				Console.ReadKey();
			}
		}

		private static void ExecuteAllMethods()
		{
			// -----------------------------------------------------------------------------

			// Do you want to proxy requests through Fiddler (useful for debugging)?
			var useFiddler = false;

			// -----------------------------------------------------------------------------


			var proxy = useFiddler ? new WebProxy("http://localhost:8888") : null;
			var apiKey = Environment.GetEnvironmentVariable("CAKEMAIL_APIKEY");
			var userName = Environment.GetEnvironmentVariable("CAKEMAIL_USERNAME");
			var password = Environment.GetEnvironmentVariable("CAKEMAIL_PASSWORD");
			var overrideClientId = Environment.GetEnvironmentVariable("CAKEMAIL_OVERRIDECLIENTID");

			var api = new CakeMailRestClient(apiKey, proxy);
			var loginInfo = api.Users.LoginAsync(userName, password).Result;
			var clientId = string.IsNullOrEmpty(overrideClientId) ? loginInfo.ClientId : long.Parse(overrideClientId);
			var userKey = loginInfo.UserKey;

			TimezonesTests.ExecuteAllMethods(api).Wait();
			CountriesTests.ExecuteAllMethods(api).Wait();
			ClientsTests.ExecuteAllMethods(api, userKey, clientId).Wait();
			UsersTests.ExecuteAllMethods(api, userKey, clientId).Wait();
			PermissionsTests.ExecuteAllMethods(api, userKey, clientId).Wait();
			CampaignsTests.ExecuteAllMethods(api, userKey, clientId).Wait();
			ListsTests.ExecuteAllMethods(api, userKey, clientId).Wait();
			TemplatesTests.ExecuteAllMethods(api, userKey, clientId).Wait();
			SuppressionListsTests.ExecuteAllMethods(api, userKey, clientId).Wait();
			RelaysTests.ExecuteAllMethods(api, userKey, clientId).Wait();
			TriggersTests.ExecuteAllMethods(api, userKey, clientId).Wait();
			MailingsTests.ExecuteAllMethods(api, userKey, clientId).Wait();
		}
	}
}
