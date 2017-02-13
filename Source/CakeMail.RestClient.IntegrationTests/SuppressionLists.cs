using System;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class SuppressionListsTests
	{
		public static async Task ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing SUPPRESSION LISTS methods...");

			var suppressEmailsResult = await api.SuppressionLists.AddEmailAddressesAsync(userKey, new string[] { "qwerty@azerty.com", "azerty@qwerty.com" }, clientId).ConfigureAwait(false);
			Console.WriteLine("Email addresses suppressed: {0}", string.Join(", ", suppressEmailsResult.Select(r => string.Format("{0}={1}", r.Email, string.IsNullOrEmpty(r.ErrorMessage) ? "success" : r.ErrorMessage))));

			var suppressedDomainResult = await api.SuppressionLists.AddDomainsAsync(userKey, new[] { "qwerty.com", "azerty.com" }, clientId).ConfigureAwait(false);
			Console.WriteLine("Email domains suppressed: {0}", string.Join(", ", suppressedDomainResult.Select(r => string.Format("{0}={1}", r.Domain, string.IsNullOrEmpty(r.ErrorMessage) ? "success" : r.ErrorMessage))));

			var suppressedLocalPartsResult = await api.SuppressionLists.AddLocalPartsAsync(userKey, new[] { "qwerty", "azerty" }, clientId).ConfigureAwait(false);
			Console.WriteLine("Email localparts suppressed: {0}", string.Join(", ", suppressedLocalPartsResult.Select(r => string.Format("{0}={1}", r.LocalPart, string.IsNullOrEmpty(r.ErrorMessage) ? "success" : r.ErrorMessage))));

			var suppressedEmails = await api.SuppressionLists.GetEmailAddressesAsync(userKey, 0, 0, clientId).ConfigureAwait(false);
			Console.WriteLine("Retrieved suppressed email addresses: {0}", string.Join(", ", suppressedEmails.Select(r => r.Email)));

			var suppressedDomains = await api.SuppressionLists.GetDomainsAsync(userKey, 0, 0, clientId).ConfigureAwait(false);
			Console.WriteLine("Retrieved suppressed email domains: {0}", string.Join(", ", suppressedDomains));

			var suppressedLocalParts = await api.SuppressionLists.GetLocalPartsAsync(userKey, 0, 0, clientId).ConfigureAwait(false);
			Console.WriteLine("Retrieved suppressed localparts: {0}", string.Join(", ", suppressedLocalParts));

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}
