using CakeMail.RestClient.Models;
using System;
using System.Linq;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class MailingsTests
	{
		public static void ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing MAILINGS methods...");

			var mailings = api.Mailings.GetMailings(userKey, null, MailingType.Standard, null, null, null, null, null, null, MailingsSortBy.Name, SortDirection.Descending, null, null, clientId);
			Console.WriteLine("All mailings retrieved. Count = {0}", mailings.Count());

			var mailingsCount = api.Mailings.GetCount(userKey, null, MailingType.Standard, null, null, null, null, null, null, clientId);
			Console.WriteLine("Mailings count = {0}", mailingsCount);

			var mailingId = api.Mailings.Create(userKey, "Integration Testing", null, MailingType.Standard, null, null, null, clientId);
			Console.WriteLine("New mailing created. Id: {0}", mailingId);

			var list = api.Lists.GetLists(userKey, ListStatus.Active, null, ListsSortBy.Name, SortDirection.Descending, 1, 0, clientId).First();
			var updated = api.Mailings.Update(userKey, mailingId, name: "UPDATED Integration Test", listId: list.Id, htmlContent: "<html><body>Hello World in HTML.  <a href=\"http://cakemail.com\">CakeMail web site</a></body></html>", textContent: "Hello World in text", subject: "This is a test", clientId: clientId);
			Console.WriteLine("Mailing updated: {0}", updated ? "success" : "failed");

			var mailing = api.Mailings.Get(userKey, mailingId, clientId);
			Console.WriteLine("Mailing retrieved: Name = {0}", mailing.Name);

			var testSent = api.Mailings.SendTestEmail(userKey, mailingId, "integration@testing.com", true, clientId);
			Console.WriteLine("Test sent: {0}", testSent ? "success" : "failed");

			var rawEmail = api.Mailings.GetRawEmailMessage(userKey, mailingId, clientId);
			Console.WriteLine("Mailing raw email: {0}", rawEmail.Message);

			var rawHtml = api.Mailings.GetRawHtml(userKey, mailingId, clientId);
			Console.WriteLine("Mailing raw html: {0}", rawHtml);

			var rawText = api.Mailings.GetRawText(userKey, mailingId, clientId);
			Console.WriteLine("Mailing raw text: {0}", rawText);

			var scheduled = api.Mailings.Schedule(userKey, mailingId, DateTime.UtcNow.AddDays(2), clientId);
			Console.WriteLine("Mailing scheduled: {0}", scheduled ? "success" : "failed");

			var unscheduled = api.Mailings.Unschedule(userKey, mailingId, clientId);
			Console.WriteLine("Mailing unscheduled: {0}", unscheduled ? "success" : "failed");

			var deleted = api.Mailings.Delete(userKey, mailingId, clientId);
			Console.WriteLine("Mailing deleted: {0}", deleted ? "success" : "failed");

			var sentMailings = mailings.Where(m => m.Status == MailingStatus.Delivered);
			if (sentMailings.Any())
			{
				var sentMailingId = sentMailings.First().Id;

				var logs = api.Mailings.GetLogs(userKey, sentMailingId, null, null, false, false, null, null, 25, 0, clientId);
				Console.WriteLine("Mailing logs retrieved. Count = {0}", logs.Count());

				var links = api.Mailings.GetLinks(userKey, sentMailingId, null, null, clientId);
				Console.WriteLine("Mailing links retrieved. Count = {0}", links.Count());

				var linksCount = api.Mailings.GetLinksCount(userKey, sentMailingId, clientId);
				Console.WriteLine("Mailing links count = {0}", linksCount);

				var linksStats = api.Mailings.GetLinksWithStats(userKey, sentMailingId, null, null, null, null, clientId);
				Console.WriteLine("Mailing links stats retrieved. Count = {0}", linksStats);

				var linksStatsCount = api.Mailings.GetLinksWithStatsCount(userKey, sentMailingId, null, null, null, null, clientId);
				Console.WriteLine("Mailing links stats count = {0}", linksStatsCount);
			}

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}