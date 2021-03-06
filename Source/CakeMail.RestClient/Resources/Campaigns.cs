using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using Pathoschild.Http.Client;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage Campaigns.
	/// </summary>
	/// <seealso cref="CakeMail.RestClient.Resources.ICampaigns" />
	public class Campaigns : ICampaigns
	{
		#region Fields

		private readonly IClient _client;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="Campaigns" /> class.
		/// </summary>
		/// <param name="client">The HTTP client.</param>
		internal Campaigns(IClient client)
		{
			_client = client;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Create a new campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the campaign.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is created.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>ID of the new campaign.</returns>
		public Task<long> CreateAsync(string userKey, string name, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("name", name)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Campaign/Create")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<long>();
		}

		/// <summary>
		/// Delete a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign to delete.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the campaign is deleted.</returns>
		public Task<bool> DeleteAsync(string userKey, long campaignId, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("campaign_id", campaignId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Campaign/Delete")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<bool>();
		}

		/// <summary>
		/// Retrieve a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The <see cref="Campaign">campaign</see>.</returns>
		public Task<Campaign> GetAsync(string userKey, long campaignId, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("campaign_id", campaignId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Campaign/GetInfo")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<Campaign>();
		}

		/// <summary>
		/// Retrieve the campaigns matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the campaign status. Possible value 'ongoing', 'closed'.</param>
		/// <param name="name">Filter using the campaign name.</param>
		/// <param name="sortBy">Sort resulting campaigns. Possible value 'created_on', 'name'.</param>
		/// <param name="sortDirection">Direction of the sorting. Possible value 'asc', 'desc'.</param>
		/// <param name="limit">Limit the number of resulting campaigns.</param>
		/// <param name="offset">Offset the beginning of resulting campaigns.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>Array of <see cref="Campaign">campaigns</see> matching the filtering criteria.</returns>
		public Task<Campaign[]> GetListAsync(string userKey, CampaignStatus? status = null, string name = null, CampaignsSortBy? sortBy = null, SortDirection? sortDirection = null, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (sortBy.HasValue) parameters.Add(new KeyValuePair<string, object>("sort_by", sortBy.Value.GetEnumMemberValue()));
			if (sortDirection.HasValue) parameters.Add(new KeyValuePair<string, object>("direction", sortDirection.Value.GetEnumMemberValue()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Campaign/GetList")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<Campaign[]>("campaigns");
		}

		/// <summary>
		/// Get a count of campaigns matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the campaign status. Possible value 'ongoing', 'closed'.</param>
		/// <param name="name">Filter using the campaign name.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The count of campaigns matching the filtering criteria.</returns>
		public Task<long> GetCountAsync(string userKey, CampaignStatus? status = null, string name = null, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Campaign/GetList")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<long>("count");
		}

		/// <summary>
		/// Update a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign.</param>
		/// <param name="status">The status of the campaign. Possible value 'ongoing', 'closed'.</param>
		/// <param name="name">The name of the campaign.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the record was updated.</returns>
		public Task<bool> UpdateAsync(string userKey, long campaignId, CampaignStatus? status = null, string name = null, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("campaign_id", campaignId),
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (!string.IsNullOrEmpty(name)) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Campaign/SetInfo")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<bool>();
		}

		#endregion
	}
}
