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

			var mailings = api.GetMailings(userKey, null, MailingType.Standard, null, null, null, null, null, null, MailingsSortBy.Name, SortDirection.Descending, null, null, clientId);
			Console.WriteLine("All mailings retrieved. Count = {0}", mailings.Count());

			var mailingsCount = api.GetMailingsCount(userKey, null, MailingType.Standard, null, null, null, null, null, null, clientId);
			Console.WriteLine("Mailings count = {0}", mailingsCount);

			var mailingId = api.CreateMailing(userKey, "Integration Testing", null, MailingType.Standard, null, null, null, clientId);
			Console.WriteLine("New mailing created. Id: {0}", mailingId);

			var list = api.GetLists(userKey, ListStatus.Active, null, ListsSortBy.Name, SortDirection.Descending, 1, 0, clientId).First();
			var updated = api.UpdateMailing(userKey, mailingId, name: "UPDATED Integration Test", listId: list.Id, htmlContent: "<html><body>Hello World in HTML.  <a href=\"http://cakemail.com\">CakeMail web site</a></body></html>", textContent: "Hello World in text", subject: "This is a test", clientId: clientId);
			Console.WriteLine("Mailing updated: {0}", updated ? "success" : "failed");

			var mailing = api.GetMailing(userKey, mailingId, clientId);
			Console.WriteLine("Mailing retrieved: Name = {0}", mailing.Name);

			var testSent = api.SendMailingTestEmail(userKey, mailingId, "integration@testing.com", true, clientId);
			Console.WriteLine("Test sent: {0}", testSent ? "success" : "failed");

			var rawEmail = api.GetMailingRawEmailMessage(userKey, mailingId, clientId);
			Console.WriteLine("Mailing raw email: {0}", rawEmail.Message);

			var rawHtml = api.GetMailingRawHtml(userKey, mailingId, clientId);
			Console.WriteLine("Mailing raw html: {0}", rawHtml);

			var rawText = api.GetMailingRawText(userKey, mailingId, clientId);
			Console.WriteLine("Mailing raw text: {0}", rawText);

			var scheduled = api.ScheduleMailing(userKey, mailingId, DateTime.UtcNow.AddDays(2), clientId);
			Console.WriteLine("Mailing scheduled: {0}", scheduled ? "success" : "failed");

			var unscheduled = api.UnscheduleMailing(userKey, mailingId, clientId);
			Console.WriteLine("Mailing unscheduled: {0}", unscheduled ? "success" : "failed");

			var sentMailings = mailings.Where(m => m.Status == MailingStatus.Delivered);
			if (sentMailings.Any())
			{
				var logs = api.GetMailingLogs(userKey, sentMailings.First().Id, null, null, false, false, null, null, 25, 0, clientId);
				Console.WriteLine("Mailing logs retrieved. Count = {0}", logs.Count());
			}

			var links = api.GetMailingLinks(userKey, mailingId, null, null, clientId);
			Console.WriteLine("Mailing links retrieved. Count = {0}", links.Count());

			var linksCount = api.GetMailingLinksCount(userKey, mailingId, clientId);
			Console.WriteLine("Mailing links count = {0}", linksCount);

			var deleted = api.DeleteMailing(userKey, mailingId, clientId);
			Console.WriteLine("Mailing deleted: {0}", deleted ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}