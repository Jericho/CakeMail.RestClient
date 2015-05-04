using System;
using System.Linq;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class RelaysTests
	{
		public static void ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing RELAYS methods...");

			var sent = api.SendRelay(userKey, "integration@testing.com", "Sent from integration test", "<html><body>Sent from integration test (HTML)</body></html>", "Sent from integration test (TEXT)", "sender@integrationtesting.com", "Integration Testing", null, clientId);
			Console.WriteLine("Relay sent: {0}", sent ? "success" : "failed");

			var sentLogs = api.GetRelaySentLogs(userKey, null, null, null, null, null, clientId);
			Console.WriteLine("Sent Logs: {0}", sentLogs.Count());

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}