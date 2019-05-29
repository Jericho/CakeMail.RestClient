using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests.Tests
{
	public static class RelaysTests
	{
		public static async Task ExecuteAllMethods(ICakeMailRestClient client, string userKey, long clientId, TextWriter log, CancellationToken cancellationToken)
		{
			await log.WriteLineAsync("\n***** RELAYS *****").ConfigureAwait(false);

			var sent = await client.Relays.SendWithoutTrackingAsync(userKey, "integration@testing.com", "Sent from integration test", "<html><body>Sent from integration test (HTML)</body></html>", "Sent from integration test (TEXT)", "sender@integrationtesting.com", "Integration Testing", null, null, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Relay sent: {(sent ? "success" : "failed")}").ConfigureAwait(false);

			var sentLogs = await client.Relays.GetSentLogsAsync(userKey, null, null, null, null, null, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Sent Logs: {sentLogs.Count()}").ConfigureAwait(false);
		}
	}
}
