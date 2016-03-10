using CakeMail.RestClient.Models;
using System;
using System.Linq;
using System.Threading;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class TriggersTests
	{
		public static void ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing TRIGGERS methods...");

			var listId = api.Lists.Create(userKey, "INTEGRATION TESTING list for trigger", "Bob Smith", "bobsmith@fictitiouscomapny.com", true, clientId);
			var listMemberId = api.Lists.Subscribe(userKey, listId, "recipient@destination.com", true, true, null, clientId);

			var campaigns = api.Campaigns.GetList(userKey, CampaignStatus.Ongoing, null, CampaignsSortBy.Name, SortDirection.Ascending, 1, 0, clientId);
			var campaign = campaigns.First();

			var triggers = api.Triggers.GetTriggers(userKey, TriggerStatus.Active, null, null, null, null, null, clientId);
			Console.WriteLine("Active triggers retrieved. Count = {0}", triggers.Count());

			var triggersCount = api.Triggers.GetCount(userKey, TriggerStatus.Active, null, null, null, clientId);
			Console.WriteLine("Active triggers count = {0}", triggersCount);

			var triggerId = api.Triggers.Create(userKey, "Integration Testing: trigger", listId, campaign.Id, null, null, clientId);
			Console.WriteLine("New trigger created. Id: {0}", triggerId);

			var trigger = api.Triggers.Get(userKey, triggerId, clientId);
			Console.WriteLine("Trigger retrieved: Name = {0}", trigger.Name);

			var updated = api.Triggers.Update(userKey, triggerId, name: "UPDATED INTEGRATION TEST: trigger", htmlContent: "<html><body>Hello World in HTML. <a href=\"http://cakemail.com\">CakeMail web site</a></body></html>", textContent: "Hello World in text", subject: "This is a test", trackClicksInHtml: true, trackClicksInText: true, trackOpens: true, clientId: clientId);
			Console.WriteLine("Trigger updated: {0}", updated ? "success" : "failed");

			var rawEmail = api.Triggers.GetRawEmailMessage(userKey, triggerId, clientId);
			Console.WriteLine("Trigger raw email: {0}", rawEmail.Message);

			var rawHtml = api.Triggers.GetRawHtml(userKey, triggerId, clientId);
			Console.WriteLine("Trigger raw html: {0}", rawHtml);

			var rawText = api.Triggers.GetRawText(userKey, triggerId, clientId);
			Console.WriteLine("Trigger raw text: {0}", rawText);

			var unleashed = api.Triggers.Unleash(userKey, triggerId, listMemberId, clientId);
			Console.WriteLine("Trigger unleashed: {0}", unleashed ? "success" : "failed");

			// Short pause to allow CakeMail to send the trigger
			Thread.Sleep(2000);

			var logs = api.Triggers.GetLogs(userKey, triggerId, null, null, false, false, null, null, null, null, clientId);
			Console.WriteLine("Trigger logs retrieved. Count = {0}", logs.Count());

			var links = api.Triggers.GetLinks(userKey, triggerId, 0, 0, clientId);
			Console.WriteLine("Trigger links retrieved. Count = {0}", links.Count());

			var linksCount = api.Triggers.GetLinksCount(userKey, triggerId, clientId);
			Console.WriteLine("Trigger links count = {0}", linksCount);

			var linksStats = api.Triggers.GetLinksWithStats(userKey, triggerId, null, null, null, null, clientId);
			Console.WriteLine("Trigger links stats retrieved. Count = {0}", linksStats.Count());

			var linksStatsCount = api.Triggers.GetLinksWithStatsCount(userKey, triggerId, null, null, clientId);
			Console.WriteLine("Trigger links stats count = {0}", linksStatsCount);

			if (links.Any())
			{
				// As of May 2015, CakeMail has not implemented despite documenting in on their web site
				//var link = api.Triggers.GetLink(userKey, links.First().Id, clientId);
				//Console.WriteLine("Trigger link retrieved. URI = {0}", link.Uri);
			}

			var deleted = api.Lists.Delete(userKey, listId, clientId);
			Console.WriteLine("List deleted: {0}", deleted ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}