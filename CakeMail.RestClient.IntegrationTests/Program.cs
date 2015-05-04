using System;
using System.Configuration;

namespace CakeMail.RestClient.IntegrationTests
{
	class Program
	{
		static void Main(string[] args)
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
				Console.WriteLine("An error has occured: {0}", e.Message);
			}
			finally
			{
				// Clear the keyboard buffer
				while (Console.KeyAvailable) { Console.ReadKey(); }

				Console.WriteLine("");
				Console.WriteLine("Press any key...");
				Console.ReadKey();
			}
		}

		static void ExecuteAllMethods()
		{
			var apiKey = ConfigurationManager.AppSettings["ApiKey"];
			var userName = ConfigurationManager.AppSettings["UserName"];
			var password = ConfigurationManager.AppSettings["Password"];
			var overrideClientId = ConfigurationManager.AppSettings["OverrideClientId"];

			var api = new CakeMailRestClient(apiKey);
			var loginInfo = api.Login(userName, password);
			var clientId = string.IsNullOrEmpty(overrideClientId) ? loginInfo.ClientId : long.Parse(overrideClientId);
			var userKey = loginInfo.UserKey;

			TimezonesTests.ExecuteAllMethods(api);
			CountriesTests.ExecuteAllMethods(api);
			ClientsTests.ExecuteAllMethods(api, userKey, clientId);
			UsersTests.ExecuteAllMethods(api, userKey, clientId);
			PermissionsTests.ExecuteAllMethods(api, userKey, clientId);
			CampaignsTests.ExecuteAllMethods(api, userKey, clientId);
			ListsTests.ExecuteAllMethods(api, userKey, clientId);
			TemplatesTests.ExecuteAllMethods(api, userKey, clientId);
			SuppressionListsTests.ExecuteAllMethods(api, userKey, clientId);
			RelaysTests.ExecuteAllMethods(api, userKey, clientId);
			TriggersTests.ExecuteAllMethods(api, userKey, clientId);
		}
	}
}
