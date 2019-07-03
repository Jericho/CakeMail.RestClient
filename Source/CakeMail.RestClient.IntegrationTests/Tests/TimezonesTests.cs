using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests.Tests
{
	public class TimezonesTests : IIntegrationTest
	{
		public async Task Execute(ICakeMailRestClient client, string userKey, long clientId, TextWriter log, CancellationToken cancellationToken)
		{
			await log.WriteLineAsync("\n***** TIMEZONES *****").ConfigureAwait(false);

			var timezones = await client.Timezones.GetAllAsync(cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Retrieved all timezones. There are {timezones.Count()} timezones.").ConfigureAwait(false);

			var utcTimezones = timezones.Where(tz => tz.Name.Contains("UTC")).ToArray();
			await log.WriteLineAsync("The following timezones contain the word UTC in their name:").ConfigureAwait(false);
			await log.WriteLineAsync(string.Join(", ", utcTimezones.Select(tz => $"{tz.Name} ({tz.Id})"))).ConfigureAwait(false);
		}
	}
}
