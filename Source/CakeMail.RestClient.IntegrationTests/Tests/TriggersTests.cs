using CakeMail.RestClient.Models;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests.Tests
{
	public static class TriggersTests
	{
		public static async Task ExecuteAllMethods(ICakeMailRestClient client, string userKey, long clientId, TextWriter log, CancellationToken cancellationToken)
		{
			await log.WriteLineAsync("\n***** TRIGGERS *****").ConfigureAwait(false);

			var listId = await client.Lists.CreateAsync(userKey, "INTEGRATION TESTING list for trigger", "Bob Smith", "bobsmith@fictitiouscomapny.com", true, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"New list created. Id: {listId}").ConfigureAwait(false);

			var listMemberId = await client.Lists.SubscribeAsync(userKey, listId, "recipient@destination.com", true, true, null, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"New member subscribed to the list. Id: {listMemberId}").ConfigureAwait(false);

			var campaignId = await client.Campaigns.CreateAsync(userKey, "INTEGRATION TESTING campaign for trigger", clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"New campaign created. Id: {campaignId}").ConfigureAwait(false);

			var triggers = await client.Triggers.GetTriggersAsync(userKey, TriggerStatus.Active, null, null, null, null, null, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Active triggers retrieved. Count = {triggers.Count()}").ConfigureAwait(false);

			var triggersCount = await client.Triggers.GetCountAsync(userKey, TriggerStatus.Active, null, null, null, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Active triggers count = {triggersCount}").ConfigureAwait(false);

			var triggerId = await client.Triggers.CreateAsync(userKey, "Integration Testing: trigger", listId, campaignId, null, null, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"New trigger created. Id: {triggerId}").ConfigureAwait(false);

			var trigger = await client.Triggers.GetAsync(userKey, triggerId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Trigger retrieved: Name = {trigger.Name}").ConfigureAwait(false);

			var updated = await client.Triggers.UpdateAsync(userKey, triggerId, action: TriggerAction.OptIn, name: "UPDATED INTEGRATION TEST: trigger", htmlContent: "<html><body>Hello World in HTML. <a href=\"http://cakemail.com\">CakeMail web site</a></body></html>", textContent: "Hello World in text", subject: "This is a test", trackClicksInHtml: true, trackClicksInText: true, trackOpens: true, clientId: clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Trigger updated: {(updated ? "success" : "failed")}").ConfigureAwait(false);

			var rawEmail = await client.Triggers.GetRawEmailMessageAsync(userKey, triggerId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Trigger raw email: {rawEmail.Message}").ConfigureAwait(false);

			var rawHtml = await client.Triggers.GetRawHtmlAsync(userKey, triggerId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Trigger raw html: {rawHtml}").ConfigureAwait(false);

			var rawText = await client.Triggers.GetRawTextAsync(userKey, triggerId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Trigger raw text: {rawText}").ConfigureAwait(false);

			var unleashed = await client.Triggers.UnleashAsync(userKey, triggerId, listMemberId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Trigger unleashed: {(unleashed ? "success" : "failed")}").ConfigureAwait(false);

			// Short pause to allow CakeMail to send the trigger
			await Task.Delay(2000).ConfigureAwait(false);

			var logs = await client.Triggers.GetLogsAsync(userKey, triggerId, null, null, false, false, null, null, null, null, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Trigger logs retrieved. Count = {logs.Count()}").ConfigureAwait(false);

			var links = await client.Triggers.GetLinksAsync(userKey, triggerId, 0, 0, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Trigger links retrieved. Count = {links.Count()}").ConfigureAwait(false);

			var linksCount = await client.Triggers.GetLinksCountAsync(userKey, triggerId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Trigger links count = {linksCount}").ConfigureAwait(false);

			var linksStats = await client.Triggers.GetLinksWithStatsAsync(userKey, triggerId, null, null, null, null, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Trigger links stats retrieved. Count = {linksStats.Count()}").ConfigureAwait(false);

			var linksStatsCount = await client.Triggers.GetLinksWithStatsCountAsync(userKey, triggerId, null, null, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Trigger links stats count = {linksStatsCount}").ConfigureAwait(false);

			if (links.Any())
			{
				// As of May 2015, CakeMail has not implemented despite documenting in on their web site
				//var link = client.Triggers.GetLink(userKey, links.First().Id, clientId, cancellationToken);
				//await log.WriteLineAsync($"Trigger link retrieved. URI = {0}", link.Uri).ConfigureAwait(false);
			}

			var listDeleted = await client.Lists.DeleteAsync(userKey, listId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"List deleted: {(listDeleted ? "success" : "failed")}").ConfigureAwait(false);

			var campaignDeleted = await client.Campaigns.DeleteAsync(userKey, campaignId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"List deleted: {(campaignDeleted ? "success" : "failed")}").ConfigureAwait(false);
		}
	}
}
