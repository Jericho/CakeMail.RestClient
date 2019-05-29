using CakeMail.RestClient.Models;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class CampaignsTests
	{
		public static async Task ExecuteAllMethods(ICakeMailRestClient client, string userKey, long clientId, TextWriter log, CancellationToken cancellationToken)
		{
			await log.WriteLineAsync("\n***** CAMPAIGNS *****").ConfigureAwait(false);

			var campaigns = await client.Campaigns.GetListAsync(userKey, CampaignStatus.Ongoing, null, CampaignsSortBy.Name, SortDirection.Ascending, null, null, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"All campaigns retrieved. Count = {campaigns.Count()}").ConfigureAwait(false);

			var campaignsCount = await client.Campaigns.GetCountAsync(userKey, CampaignStatus.Ongoing, null, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Campaigns count = {campaignsCount}").ConfigureAwait(false);

			var campaignId = await client.Campaigns.CreateAsync(userKey, "Dummy campaign", clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"New campaign created. Id: {campaignId}").ConfigureAwait(false);

			var updated = await client.Campaigns.UpdateAsync(userKey, campaignId, CampaignStatus.Ongoing, "Updated name", clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Campaign updated: {(updated ? "success" : "failed")}").ConfigureAwait(false);

			var campaign = await client.Campaigns.GetAsync(userKey, campaignId, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Campaign retrieved: Name = {campaign.Name}").ConfigureAwait(false);

			var deleted = await client.Campaigns.DeleteAsync(userKey, campaignId, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Campaign deleted: {(deleted ? "success" : "failed")}").ConfigureAwait(false);
		}
	}
}
