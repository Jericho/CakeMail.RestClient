using System;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class RelaysTests
	{
		public static async Task ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing RELAYS methods...");

			var sent = await api.Relays.SendWithoutTrackingAsync(userKey, "integration@testing.com", "Sent from integration test", "<html><body>Sent from integration test (HTML)</body></html>", "Sent from integration test (TEXT)", "sender@integrationtesting.com", "Integration Testing", null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("Relay sent: {0}", sent ? "success" : "failed");

			var sentLogs = await api.Relays.GetSentLogsAsync(userKey, null, null, null, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("Sent Logs: {0}", sentLogs.Count());

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}
