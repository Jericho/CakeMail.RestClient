using CakeMail.RestClient.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class MailingsTests
	{
		public static async Task ExecuteAllMethods(ICakeMailRestClient client, string userKey, long clientId, TextWriter log, CancellationToken cancellationToken)
		{
			await log.WriteLineAsync("\n***** MAILINGS *****").ConfigureAwait(false);

			var mailings = await client.Mailings.GetMailingsAsync(userKey, null, MailingType.Standard, null, null, null, null, null, null, MailingsSortBy.Name, SortDirection.Descending, null, null, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"All mailings retrieved. Count = {mailings.Count()}").ConfigureAwait(false);

			var mailingsCount = await client.Mailings.GetCountAsync(userKey, null, MailingType.Standard, null, null, null, null, null, null, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Mailings count = {mailingsCount}").ConfigureAwait(false);

			var mailingId = await client.Mailings.CreateAsync(userKey, "Integration Testing", clientId: clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"New mailing created. Id: {mailingId}").ConfigureAwait(false);

			var lists = await client.Lists.GetListsAsync(userKey, ListStatus.Active, null, ListsSortBy.Name, SortDirection.Descending, 1, 0, clientId).ConfigureAwait(false);
			var list = lists.First();
			var updated = await client.Mailings.UpdateAsync(userKey, mailingId, name: "UPDATED Integration Test", listId: list.Id, htmlContent: "<html><body>Hello World in HTML.  <a href=\"http://cakemail.com\">CakeMail web site</a></body></html>", textContent: "Hello World in text", subject: "This is a test", clientId: clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Mailing updated: {(updated ? "success" : "failed")}").ConfigureAwait(false);

			var mailing = await client.Mailings.GetAsync(userKey, mailingId, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Mailing retrieved: Name = {mailing.Name}").ConfigureAwait(false);

			var testSent = await client.Mailings.SendTestEmailAsync(userKey, mailingId, "integration@testing.com", true, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Test sent: {(testSent ? "success" : "failed")}").ConfigureAwait(false);

			var rawEmail = await client.Mailings.GetRawEmailMessageAsync(userKey, mailingId, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Mailing raw email: {rawEmail.Message}").ConfigureAwait(false);

			var rawHtml = await client.Mailings.GetRawHtmlAsync(userKey, mailingId, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Mailing raw html: {rawHtml}").ConfigureAwait(false);

			var rawText = await client.Mailings.GetRawTextAsync(userKey, mailingId, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Mailing raw text: {rawText}").ConfigureAwait(false);

			var scheduled = await client.Mailings.ScheduleAsync(userKey, mailingId, DateTime.UtcNow.AddDays(2), clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Mailing scheduled: {(scheduled ? "success" : "failed")}").ConfigureAwait(false);

			var unscheduled = await client.Mailings.UnscheduleAsync(userKey, mailingId, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Mailing unscheduled: {(unscheduled ? "success" : "failed")}").ConfigureAwait(false);

			var deleted = await client.Mailings.DeleteAsync(userKey, mailingId, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Mailing deleted: {(deleted ? "success" : "failed")}").ConfigureAwait(false);

			var sentMailings = mailings.Where(m => m.Status == MailingStatus.Delivered);
			if (sentMailings.Any())
			{
				var sentMailingId = sentMailings.First().Id;

				var logs = await client.Mailings.GetLogsAsync(userKey, sentMailingId, null, null, false, false, null, null, 25, 0, clientId).ConfigureAwait(false);
				await log.WriteLineAsync($"Mailing logs retrieved. Count = {logs.Count()}").ConfigureAwait(false);

				var links = await client.Mailings.GetLinksAsync(userKey, sentMailingId, null, null, clientId).ConfigureAwait(false);
				await log.WriteLineAsync($"Mailing links retrieved. Count = {links.Count()}").ConfigureAwait(false);

				var linksCount = await client.Mailings.GetLinksCountAsync(userKey, sentMailingId, clientId).ConfigureAwait(false);
				await log.WriteLineAsync($"Mailing links count = {linksCount}").ConfigureAwait(false);

				var linksStats = await client.Mailings.GetLinksWithStatsAsync(userKey, sentMailingId, null, null, null, null, clientId).ConfigureAwait(false);
				await log.WriteLineAsync($"Mailing links stats retrieved. Count = {linksStats}").ConfigureAwait(false);

				var linksStatsCount = await client.Mailings.GetLinksWithStatsCountAsync(userKey, sentMailingId, null, null, null, null, clientId).ConfigureAwait(false);
				await log.WriteLineAsync($"Mailing links stats count = {linksStatsCount}").ConfigureAwait(false);
			}
		}
	}
}
