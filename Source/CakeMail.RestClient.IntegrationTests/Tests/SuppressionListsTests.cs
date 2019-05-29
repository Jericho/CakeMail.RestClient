using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests.Tests
{
	public static class SuppressionListsTests
	{
		public static async Task ExecuteAllMethods(ICakeMailRestClient client, string userKey, long clientId, TextWriter log, CancellationToken cancellationToken)
		{
			await log.WriteLineAsync("\n***** SUPPRESSION LISTS *****").ConfigureAwait(false);

			var suppressEmailsResult = await client.SuppressionLists.AddEmailAddressesAsync(userKey, new string[] { "qwerty@azerty.com", "azerty@qwerty.com" }, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Email addresses suppressed: {string.Join(", ", suppressEmailsResult.Select(r => string.Format("{0}={1}", r.Email, string.IsNullOrEmpty(r.ErrorMessage) ? "success" : r.ErrorMessage)))}").ConfigureAwait(false);

			var suppressedDomainResult = await client.SuppressionLists.AddDomainsAsync(userKey, new[] { "qwerty.com", "azerty.com" }, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Email domains suppressed: {string.Join(", ", suppressedDomainResult.Select(r => string.Format("{0}={1}", r.Domain, string.IsNullOrEmpty(r.ErrorMessage) ? "success" : r.ErrorMessage)))}").ConfigureAwait(false);

			var suppressedLocalPartsResult = await client.SuppressionLists.AddLocalPartsAsync(userKey, new[] { "qwerty", "azerty" }, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Email localparts suppressed: {string.Join(", ", suppressedLocalPartsResult.Select(r => string.Format("{0}={1}", r.LocalPart, string.IsNullOrEmpty(r.ErrorMessage) ? "success" : r.ErrorMessage)))}").ConfigureAwait(false);

			var suppressedEmails = await client.SuppressionLists.GetEmailAddressesAsync(userKey, 0, 0, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Retrieved suppressed email addresses: {string.Join(", ", suppressedEmails.Select(r => r.Email))}").ConfigureAwait(false);

			var suppressedDomains = await client.SuppressionLists.GetDomainsAsync(userKey, 0, 0, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Retrieved suppressed email domains: {string.Join(", ", suppressedDomains)}").ConfigureAwait(false);

			var suppressedLocalParts = await client.SuppressionLists.GetLocalPartsAsync(userKey, 0, 0, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Retrieved suppressed localparts: {string.Join(", ", suppressedLocalParts)}").ConfigureAwait(false);
		}
	}
}
