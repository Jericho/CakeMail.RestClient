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
