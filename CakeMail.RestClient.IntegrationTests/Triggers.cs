using CakeMail.RestClient.Models;
using System;
using System.Linq;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class TriggersTests
	{
		public static void ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing TRIGGERS methods...");

			var list = api.GetLists(userKey, ListStatus.Active, null, ListsSortBy.Name, SortDirection.Ascending, 1, 0, clientId).First();
			var campaign = api.GetCampaigns(userKey, CampaignStatus.Ongoing, null, CampaignsSortBy.Name, SortDirection.Ascending, 1, 0, clientId).First();

			var triggers = api.GetTriggers(userKey, TriggerStatus.Active, null, null, null, null, null, clientId);
			Console.WriteLine("All triggers retrieved. Count = {0}", triggers.Count());

			var triggersCount = api.GetTriggersCount(userKey, TriggerStatus.Active, null, null, null, clientId);
			Console.WriteLine("Triggers count = {0}", triggersCount);

			var triggerId = api.CreateTrigger(userKey, "Integration Testing: trigger", list.Id, campaign.Id, null, null, clientId);
			Console.WriteLine("New trigger created. Id: {0}", triggerId);

			var trigger = api.GetTrigger(userKey, triggerId, clientId);
			Console.WriteLine("Trigger retrieved: Name = {0}", trigger.Name);

			var updated = api.UpdateTrigger(userKey, triggerId, name: "UPDATED INTEGRATION TEST: trigger", htmlContent: "<html><body>Hello World in HTML. <a href=\"http://cakemail.com\">CakeMail web site</a></body</html>", textContent: "Hello World in text", subject: "This is a test", clientId: clientId);
			Console.WriteLine("Trigger updated: {0}", updated ? "success" : "failed");

			var rawEmail = api.GetTriggerRawEmailMessage(userKey, triggerId, clientId);
			Console.WriteLine("Trigger raw email: {0}", rawEmail.Message);

			var rawHtml = api.GetTriggerRawHtml(userKey, triggerId, clientId);
			Console.WriteLine("Trigger raw html: {0}", rawHtml);

			var rawText = api.GetTriggerRawText(userKey, triggerId, clientId);
			Console.WriteLine("Trigger raw text: {0}", rawText);

			var logs = api.GetTriggerLogs(userKey, triggerId, null, null, false, false, null, null, null, null, clientId);
			Console.WriteLine("All trigger logs retrieved. Count = {0}", logs.Count());

			var links = api.GetTriggerLinks(userKey, triggerId, 0, 0, clientId);
			Console.WriteLine("All trigger links retrieved. Count = {0}", links.Count());

			var linksCount = api.GetTriggerLinksCount(userKey, triggerId, clientId);
			Console.WriteLine("Trigger links count = {0}", linksCount);

			var link = api.GetTriggerLink(userKey, links.First().Id, clientId);
			Console.WriteLine("Trigger link retrieved. URI = {0}", link.Uri);

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}