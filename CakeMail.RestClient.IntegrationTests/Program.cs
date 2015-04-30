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
				Console.WriteLine("An error has occured: {0}", e.Message);
			}
			finally
			{
				Console.WriteLine("");
				Console.WriteLine("Press any key to close...");
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

			CountriesTests.ExecuteAllMethods(api);
			ClientsTests.ExecuteAllMethods(api, userKey, clientId);
			UsersTests.ExecuteAllMethods(api, userKey, clientId);
		}
	}
}
