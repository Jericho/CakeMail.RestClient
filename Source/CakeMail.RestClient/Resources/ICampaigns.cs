using CakeMail.RestClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public interface ICampaigns
	{
		Task<long> CreateAsync(string userKey, string name, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> DeleteAsync(string userKey, long campaignId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Campaign> GetAsync(string userKey, long campaignId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetCountAsync(string userKey, CampaignStatus? status = default(CampaignStatus?), string name = null, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Campaign[]> GetListAsync(string userKey, CampaignStatus? status = default(CampaignStatus?), string name = null, CampaignsSortBy? sortBy = default(CampaignsSortBy?), SortDirection? sortDirection = default(SortDirection?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> UpdateAsync(string userKey, long campaignId, string name, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
	}
}
