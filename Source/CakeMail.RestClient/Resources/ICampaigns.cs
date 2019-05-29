using CakeMail.RestClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage Campaigns
	/// </summary>
	public interface ICampaigns
	{
		/// <summary>
		/// Create a new campaign
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the campaign.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is created.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>ID of the new campaign</returns>
		Task<long> CreateAsync(string userKey, string name, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign to delete.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the campaign is deleted</returns>
		Task<bool> DeleteAsync(string userKey, long campaignId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="Campaign">campaign</see></returns>
		Task<Campaign> GetAsync(string userKey, long campaignId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of campaigns matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the campaign status. Possible value 'ongoing', 'closed'</param>
		/// <param name="name">Filter using the campaign name.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The count of campaigns matching the filtering criteria</returns>
		Task<long> GetCountAsync(string userKey, CampaignStatus? status = default(CampaignStatus?), string name = null, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the campaigns matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the campaign status. Possible value 'ongoing', 'closed'</param>
		/// <param name="name">Filter using the campaign name.</param>
		/// <param name="sortBy">Sort resulting campaigns. Possible value 'created_on', 'name'</param>
		/// <param name="sortDirection">Direction of the sorting. Possible value 'asc', 'desc'</param>
		/// <param name="limit">Limit the number of resulting campaigns.</param>
		/// <param name="offset">Offset the beginning of resulting campaigns.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>Array of <see cref="Campaign">campaigns</see> matching the filtering criteria</returns>
		Task<Campaign[]> GetListAsync(string userKey, CampaignStatus? status = default(CampaignStatus?), string name = null, CampaignsSortBy? sortBy = default(CampaignsSortBy?), SortDirection? sortDirection = default(SortDirection?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Update a campaign
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign.</param>
		/// <param name="status">The status of the campaign. Possible value 'ongoing', 'closed'</param>
		/// <param name="name">The name of the campaign</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the record was updated.</returns>
		Task<bool> UpdateAsync(string userKey, long campaignId, CampaignStatus? status = default(CampaignStatus?), string name = null, long? clientId = default(long?), CancellationToken cancellationToken = default);
	}
}
