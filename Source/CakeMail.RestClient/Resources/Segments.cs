using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using Pathoschild.Http.Client;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage segments.
	/// </summary>
	/// <seealso cref="CakeMail.RestClient.Resources.ISegments" />
	public class Segments : ISegments
	{
		#region Fields

		private readonly IClient _client;

		#endregion

		#region Constructor

		internal Segments(IClient client)
		{
			_client = client;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Create a list segment.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="name">Name of the segment.</param>
		/// <param name="query">Rules for the segment.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>ID of the new segment.</returns>
		/// <remarks>
		/// Here is what I have discovered about the query:
		///     1) the entire query must be surrounded by parenthesis: (...your query...)
		///     2) field names must be surrounded with the 'special' quote: `yourfieldname`. On my US english keyboard, this 'special' quote is the key directly above the 'Tab' and to the left of the '1'.
		///     3) The percent sign is the wilcard
		///     Here's an example: (`email` LIKE "a%").
		/// </remarks>
		public Task<long> CreateAsync(string userKey, long listId, string name, string query = null, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("sublist_name", name)
			};
			if (query != null) parameters.Add(new KeyValuePair<string, object>("query", query));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("List/CreateSublist")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<long>();
		}

		/// <summary>
		/// Retrieve a segment.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="segmentId">ID of the segment.</param>
		/// <param name="includeStatistics">True if you want the statistics.</param>
		/// <param name="calculateEngagement">True if you want the engagement information to be calculated.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The <see cref="Segment">segment</see>.</returns>
		public Task<Segment> GetAsync(string userKey, long segmentId, bool includeStatistics = true, bool calculateEngagement = false, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("sublist_id", segmentId),
				new KeyValuePair<string, object>("no_details", includeStatistics ? "false" : "true"), // CakeMail expects 'false' if you want to include details
				new KeyValuePair<string, object>("with_engagement", calculateEngagement ? "true" : "false")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("List/GetInfo")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<Segment>();
		}

		/// <summary>
		/// Update a segment (AKA sublist).
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="segmentId">ID of the segment.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="name">Name of the segment.</param>
		/// <param name="query">Rules for the segment.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the segment was updated.</returns>
		/// <remarks>A segment is sometimes referred to as a 'sub-list'.</remarks>
		public Task<bool> UpdateAsync(string userKey, long segmentId, long listId, string name = null, string query = null, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("sublist_id", segmentId),
				new KeyValuePair<string, object>("list_id", listId)
			};
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (query != null) parameters.Add(new KeyValuePair<string, object>("query", query));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("List/SetInfo")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<bool>();
		}

		/// <summary>
		/// Delete a segment.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="segmentId">ID of the segment.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the segment was deleted.</returns>
		public Task<bool> DeleteAsync(string userKey, long segmentId, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("sublist_id", segmentId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("List/DeleteSublist")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<bool>();
		}

		/// <summary>
		/// Retrieve the segments matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="limit">Limit the number of resulting segments.</param>
		/// <param name="offset">Offset the beginning of resulting segments.</param>
		/// <param name="includeDetails">Retrieve all the stats for the segment.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>Enumeration of <see cref="Segment">segments</see> matching the filtering criteria.</returns>
		public Task<Segment[]> GetSegmentsAsync(string userKey, long listId, int? limit = 0, int? offset = 0, bool includeDetails = true, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("count", "false"),
				new KeyValuePair<string, object>("no_details", includeDetails ? "false" : "true") // CakeMail expects 'false' if you want to include details
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("List/GetSublists")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<Segment[]>("sublists");
		}

		/// <summary>
		/// Get a count of segments matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The count of campaigns matching the filtering criteria.</returns>
		public Task<long> GetCountAsync(string userKey, long listId, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("List/GetList")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<long>("count");
		}

		#endregion
	}
}
