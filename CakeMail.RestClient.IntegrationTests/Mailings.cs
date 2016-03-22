using CakeMail.RestClient.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class MailingsTests
	{
		public static async Task ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing MAILINGS methods...");

			var mailings = await api.Mailings.GetMailingsAsync(userKey, null, MailingType.Standard, null, null, null, null, null, null, MailingsSortBy.Name, SortDirection.Descending, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("All mailings retrieved. Count = {0}", mailings.Count());

			var mailingsCount = await api.Mailings.GetCountAsync(userKey, null, MailingType.Standard, null, null, null, null, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("Mailings count = {0}", mailingsCount);

			var mailingId = await api.Mailings.CreateAsync(userKey, "Integration Testing", null, MailingType.Standard, null, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("New mailing created. Id: {0}", mailingId);

			var lists = await api.Lists.GetListsAsync(userKey, ListStatus.Active, null, ListsSortBy.Name, SortDirection.Descending, 1, 0, clientId).ConfigureAwait(false);
			var list = lists.First();
			var updated = await api.Mailings.UpdateAsync(userKey, mailingId, name: "UPDATED Integration Test", listId: list.Id, htmlContent: "<html><body>Hello World in HTML.  <a href=\"http://cakemail.com\">CakeMail web site</a></body></html>", textContent: "Hello World in text", subject: "This is a test", clientId: clientId).ConfigureAwait(false);
			Console.WriteLine("Mailing updated: {0}", updated ? "success" : "failed");

			var mailing = await api.Mailings.GetAsync(userKey, mailingId, clientId).ConfigureAwait(false);
			Console.WriteLine("Mailing retrieved: Name = {0}", mailing.Name);

			var testSent = await api.Mailings.SendTestEmailAsync(userKey, mailingId, "integration@testing.com", true, clientId).ConfigureAwait(false);
			Console.WriteLine("Test sent: {0}", testSent ? "success" : "failed");

			var rawEmail = await api.Mailings.GetRawEmailMessageAsync(userKey, mailingId, clientId).ConfigureAwait(false);
			Console.WriteLine("Mailing raw email: {0}", rawEmail.Message);

			var rawHtml = await api.Mailings.GetRawHtmlAsync(userKey, mailingId, clientId).ConfigureAwait(false);
			Console.WriteLine("Mailing raw html: {0}", rawHtml);

			var rawText = await api.Mailings.GetRawTextAsync(userKey, mailingId, clientId).ConfigureAwait(false);
			Console.WriteLine("Mailing raw text: {0}", rawText);

			var scheduled = await api.Mailings.ScheduleAsync(userKey, mailingId, DateTime.UtcNow.AddDays(2), clientId).ConfigureAwait(false);
			Console.WriteLine("Mailing scheduled: {0}", scheduled ? "success" : "failed");

			var unscheduled = await api.Mailings.UnscheduleAsync(userKey, mailingId, clientId).ConfigureAwait(false);
			Console.WriteLine("Mailing unscheduled: {0}", unscheduled ? "success" : "failed");

			var deleted = await api.Mailings.DeleteAsync(userKey, mailingId, clientId).ConfigureAwait(false);
			Console.WriteLine("Mailing deleted: {0}", deleted ? "success" : "failed");

			var sentMailings = mailings.Where(m => m.Status == MailingStatus.Delivered);
			if (sentMailings.Any())
			{
				var sentMailingId = sentMailings.First().Id;

				var logs = await api.Mailings.GetLogsAsync(userKey, sentMailingId, null, null, false, false, null, null, 25, 0, clientId).ConfigureAwait(false);
				Console.WriteLine("Mailing logs retrieved. Count = {0}", logs.Count());

				var links = await api.Mailings.GetLinksAsync(userKey, sentMailingId, null, null, clientId).ConfigureAwait(false);
				Console.WriteLine("Mailing links retrieved. Count = {0}", links.Count());

				var linksCount = await api.Mailings.GetLinksCountAsync(userKey, sentMailingId, clientId).ConfigureAwait(false);
				Console.WriteLine("Mailing links count = {0}", linksCount);

				var linksStats = await api.Mailings.GetLinksWithStatsAsync(userKey, sentMailingId, null, null, null, null, clientId).ConfigureAwait(false);
				Console.WriteLine("Mailing links stats retrieved. Count = {0}", linksStats);

				var linksStatsCount = await api.Mailings.GetLinksWithStatsCountAsync(userKey, sentMailingId, null, null, null, null, clientId).ConfigureAwait(false);
				Console.WriteLine("Mailing links stats count = {0}", linksStatsCount);
			}

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}
