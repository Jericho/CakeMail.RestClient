using CakeMail.RestClient.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class TriggersTests
	{
		public static async Task ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing TRIGGERS methods...");

			var listId = await api.Lists.CreateAsync(userKey, "INTEGRATION TESTING list for trigger", "Bob Smith", "bobsmith@fictitiouscomapny.com", true, clientId).ConfigureAwait(false);
			var listMemberId = await api.Lists.SubscribeAsync(userKey, listId, "recipient@destination.com", true, true, null, clientId).ConfigureAwait(false);

			var campaigns = await api.Campaigns.GetListAsync(userKey, CampaignStatus.Ongoing, null, CampaignsSortBy.Name, SortDirection.Ascending, 1, 0, clientId).ConfigureAwait(false);
			var campaign = campaigns.First();

			var triggers = await api.Triggers.GetTriggersAsync(userKey, TriggerStatus.Active, null, null, null, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("Active triggers retrieved. Count = {0}", triggers.Count());

			var triggersCount = await api.Triggers.GetCountAsync(userKey, TriggerStatus.Active, null, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("Active triggers count = {0}", triggersCount);

			var triggerId = await api.Triggers.CreateAsync(userKey, "Integration Testing: trigger", listId, campaign.Id, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("New trigger created. Id: {0}", triggerId);

			var trigger = await api.Triggers.GetAsync(userKey, triggerId, clientId).ConfigureAwait(false);
			Console.WriteLine("Trigger retrieved: Name = {0}", trigger.Name);

			var updated = await api.Triggers.UpdateAsync(userKey, triggerId, name: "UPDATED INTEGRATION TEST: trigger", htmlContent: "<html><body>Hello World in HTML. <a href=\"http://cakemail.com\">CakeMail web site</a></body></html>", textContent: "Hello World in text", subject: "This is a test", trackClicksInHtml: true, trackClicksInText: true, trackOpens: true, clientId: clientId).ConfigureAwait(false);
			Console.WriteLine("Trigger updated: {0}", updated ? "success" : "failed");

			var rawEmail = await api.Triggers.GetRawEmailMessageAsync(userKey, triggerId, clientId).ConfigureAwait(false);
			Console.WriteLine("Trigger raw email: {0}", rawEmail.Message);

			var rawHtml = await api.Triggers.GetRawHtmlAsync(userKey, triggerId, clientId).ConfigureAwait(false);
			Console.WriteLine("Trigger raw html: {0}", rawHtml);

			var rawText = await api.Triggers.GetRawTextAsync(userKey, triggerId, clientId).ConfigureAwait(false);
			Console.WriteLine("Trigger raw text: {0}", rawText);

			var unleashed = await api.Triggers.UnleashAsync(userKey, triggerId, listMemberId, clientId).ConfigureAwait(false);
			Console.WriteLine("Trigger unleashed: {0}", unleashed ? "success" : "failed");

			// Short pause to allow CakeMail to send the trigger
			Thread.Sleep(2000);

			var logs = await api.Triggers.GetLogsAsync(userKey, triggerId, null, null, false, false, null, null, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("Trigger logs retrieved. Count = {0}", logs.Count());

			var links = await api.Triggers.GetLinksAsync(userKey, triggerId, 0, 0, clientId).ConfigureAwait(false);
			Console.WriteLine("Trigger links retrieved. Count = {0}", links.Count());

			var linksCount = await api.Triggers.GetLinksCountAsync(userKey, triggerId, clientId).ConfigureAwait(false);
			Console.WriteLine("Trigger links count = {0}", linksCount);

			var linksStats = await api.Triggers.GetLinksWithStatsAsync(userKey, triggerId, null, null, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("Trigger links stats retrieved. Count = {0}", linksStats.Count());

			var linksStatsCount = await api.Triggers.GetLinksWithStatsCountAsync(userKey, triggerId, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("Trigger links stats count = {0}", linksStatsCount);

			if (links.Any())
			{
				// As of May 2015, CakeMail has not implemented despite documenting in on their web site
				//var link = api.Triggers.GetLink(userKey, links.First().Id, clientId);
				//Console.WriteLine("Trigger link retrieved. URI = {0}", link.Uri);
			}

			var deleted = await api.Lists.DeleteAsync(userKey, listId, clientId).ConfigureAwait(false);
			Console.WriteLine("List deleted: {0}", deleted ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}
