using System;
using System.Linq;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class SuppressionListsTests
	{
		public static void ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing SUPPRESSION LISTS methods...");

			var suppressEmailsResult = api.SuppressionLists.AddEmailAddresses(userKey, new string[] { "qwerty@azerty.com", "azerty@qwerty.com" }, clientId);
			Console.WriteLine("Email addresses suppressed: {0}", string.Join(", ", suppressEmailsResult.Select(r => string.Format("{0}={1}", r.Email, string.IsNullOrEmpty(r.ErrorMessage) ? "success" : r.ErrorMessage))));

			var suppressedDomainResult = api.SuppressionLists.AddDomains(userKey, new[] { "qwerty.com", "azerty.com" }, clientId);
			Console.WriteLine("Email domains suppressed: {0}", string.Join(", ", suppressedDomainResult.Select(r => string.Format("{0}={1}", r.Domain, string.IsNullOrEmpty(r.ErrorMessage) ? "success" : r.ErrorMessage))));

			var suppressedLocalPartsResult = api.SuppressionLists.AddLocalParts(userKey, new[] { "qwerty", "azerty" }, clientId);
			Console.WriteLine("Email localparts suppressed: {0}", string.Join(", ", suppressedLocalPartsResult.Select(r => string.Format("{0}={1}", r.LocalPart, string.IsNullOrEmpty(r.ErrorMessage) ? "success" : r.ErrorMessage))));

			var suppressedEmails = api.SuppressionLists.GetEmailAddresses(userKey, 0, 0, clientId);
			Console.WriteLine("Retrieved suppressed email addresses: {0}", string.Join(", ", suppressedEmails.Select(r => r.Email)));

			var suppressedDomains = api.SuppressionLists.GetDomains(userKey, 0, 0, clientId);
			Console.WriteLine("Retrieved suppressed email domains: {0}", string.Join(", ", suppressedDomains));

			var suppressedLocalParts = api.SuppressionLists.GetLocalParts(userKey, 0, 0, clientId);
			Console.WriteLine("Retrieved suppressed localparts: {0}", string.Join(", ", suppressedLocalParts));
			
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}