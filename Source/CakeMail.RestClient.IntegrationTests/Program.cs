using CakeMail.RestClient.Logging;
using System;

namespace CakeMail.RestClient.IntegrationTests
{
	public class Program
	{
		public static void Main()
		{
			// -----------------------------------------------------------------------------

			// Do you want to proxy requests through Fiddler (useful for debugging)?
			var useFiddler = false;

			// As an alternative to Fiddler, you can display debug information about
			// every HTTP request/response in the console. This is useful for debugging
			// purposes but the amount of information can be overwhelming.
			var debugHttpMessagesToConsole = true;
			// -----------------------------------------------------------------------------

			var proxy = useFiddler ? new WebProxy("http://localhost:8888") : null;
			var apiKey = Environment.GetEnvironmentVariable("CAKEMAIL_APIKEY");
			var userName = Environment.GetEnvironmentVariable("CAKEMAIL_USERNAME");
			var password = Environment.GetEnvironmentVariable("CAKEMAIL_PASSWORD");
			var overrideClientId = Environment.GetEnvironmentVariable("CAKEMAIL_OVERRIDECLIENTID");

			if (debugHttpMessagesToConsole)
			{
				LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
			}

			try
			{
				var client = new CakeMailRestClient(apiKey, proxy);
				var loginInfo = client.Users.LoginAsync(userName, password).Result;
				var clientId = string.IsNullOrEmpty(overrideClientId) ? loginInfo.ClientId : long.Parse(overrideClientId);
				var userKey = loginInfo.UserKey;

				TimezonesTests.ExecuteAllMethods(client).Wait();
				CountriesTests.ExecuteAllMethods(client).Wait();
				ClientsTests.ExecuteAllMethods(client, userKey, clientId).Wait();
				UsersTests.ExecuteAllMethods(client, userKey, clientId).Wait();
				PermissionsTests.ExecuteAllMethods(client, userKey, clientId).Wait();
				CampaignsTests.ExecuteAllMethods(client, userKey, clientId).Wait();
				ListsTests.ExecuteAllMethods(client, userKey, clientId).Wait();
				TemplatesTests.ExecuteAllMethods(client, userKey, clientId).Wait();
				SuppressionListsTests.ExecuteAllMethods(client, userKey, clientId).Wait();
				RelaysTests.ExecuteAllMethods(client, userKey, clientId).Wait();
				TriggersTests.ExecuteAllMethods(client, userKey, clientId).Wait();
				MailingsTests.ExecuteAllMethods(client, userKey, clientId).Wait();
			}
			catch (Exception e)
			{
				Console.WriteLine("\n\n**************************************************");
				Console.WriteLine("**************************************************");
				Console.WriteLine($"AN EXCEPTION OCCURED: {(e.InnerException ?? e).Message}");
				Console.WriteLine("**************************************************");
				Console.WriteLine("**************************************************");
			}
			finally
			{
				// Clear the keyboard buffer
				while (Console.KeyAvailable)
				{
					Console.ReadKey();
				}
				Console.WriteLine("\n\n*************************");
				Console.WriteLine("All tests completed");
				Console.WriteLine("Press any key to exit");
				Console.ReadKey();
			}
		}
	}
}
