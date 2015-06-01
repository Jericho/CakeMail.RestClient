using CakeMail.RestClient.Models;
using System;
using System.Linq;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class CampaignsTests
	{
		public static void ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing CAMPAIGNS methods...");

			var campaigns = api.GetCampaigns(userKey, CampaignStatus.Ongoing, null, CampaignsSortBy.Name, SortDirection.Ascending, null, null, clientId);
			Console.WriteLine("All campaigns retrieved. Count = {0}", campaigns.Count());

			var campaignsCount = api.GetCampaignsCount(userKey, CampaignStatus.Ongoing, null, clientId);
			Console.WriteLine("Campaigns count = {0}", campaignsCount);

			var campaignId = api.CreateCampaign(userKey, "Dummy campaign", clientId);
			Console.WriteLine("New campaign created. Id: {0}", campaignId);

			var updated = api.UpdateCampaign(userKey, campaignId, "Updated name", clientId);
			Console.WriteLine("Campaign updated: {0}", updated ? "success" : "failed");

			var campaign = api.GetCampaign(userKey, campaignId, clientId);
			Console.WriteLine("Campaign retrieved: Name = {0}", campaign.Name);

			var deleted = api.DeleteCampaign(userKey, campaignId, clientId);
			Console.WriteLine("Campaign deleted: {0}", deleted ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}
