using CakeMail.RestClient.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class CampaignsTests
	{
		public static async Task ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing CAMPAIGNS methods...");

			var campaigns = await api.Campaigns.GetListAsync(userKey, CampaignStatus.Ongoing, null, CampaignsSortBy.Name, SortDirection.Ascending, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("All campaigns retrieved. Count = {0}", campaigns.Count());

			var campaignsCount = await api.Campaigns.GetCountAsync(userKey, CampaignStatus.Ongoing, null, clientId).ConfigureAwait(false);
			Console.WriteLine("Campaigns count = {0}", campaignsCount);

			var campaignId = await api.Campaigns.CreateAsync(userKey, "Dummy campaign", clientId).ConfigureAwait(false);
			Console.WriteLine("New campaign created. Id: {0}", campaignId);

			var updated = await api.Campaigns.UpdateAsync(userKey, campaignId, "Updated name", clientId).ConfigureAwait(false);
			Console.WriteLine("Campaign updated: {0}", updated ? "success" : "failed");

			var campaign = await api.Campaigns.GetAsync(userKey, campaignId, clientId).ConfigureAwait(false);
			Console.WriteLine("Campaign retrieved: Name = {0}", campaign.Name);

			var deleted = await api.Campaigns.DeleteAsync(userKey, campaignId, clientId).ConfigureAwait(false);
			Console.WriteLine("Campaign deleted: {0}", deleted ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}
