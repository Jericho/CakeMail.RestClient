using CakeMail.RestClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage segments
	/// </summary>
	public interface ISegments
	{
		/// <summary>
		/// Create a list segment
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="name">Name of the segment.</param>
		/// <param name="query">Rules for the segment.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>ID of the new segment</returns>
		/// <remarks>
		/// Here is what I have discovered about the query:
		///     1) the entire query must be surrounded by parenthesis: (...your query...)
		///     2) field names must be surrounded with the 'special' quote: `yourfieldname`. On my US english keyboard, this 'special' quote is the key directly above the 'Tab' and to the left of the '1'.
		///     3) The percent sign is the wilcard
		///     Here's an example: (`email` LIKE "a%")
		/// </remarks>
		Task<long> CreateAsync(string userKey, long listId, string name, string query = null, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete a segment
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="segmentId">ID of the segment</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the segment was deleted</returns>
		Task<bool> DeleteAsync(string userKey, long segmentId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve a segment
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="segmentId">ID of the segment</param>
		/// <param name="includeStatistics">True if you want the statistics</param>
		/// <param name="calculateEngagement">True if you want the engagement information to be calculated</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="Segment">segment</see></returns>
		Task<Segment> GetAsync(string userKey, long segmentId, bool includeStatistics = true, bool calculateEngagement = false, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of segments matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The count of campaigns matching the filtering criteria</returns>
		Task<long> GetCountAsync(string userKey, long listId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the segments matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="limit">Limit the number of resulting segments.</param>
		/// <param name="offset">Offset the beginning of resulting segments.</param>
		/// <param name="includeDetails">Retrieve all the stats for the segment</param>
		/// <param name="clientId">ID of the client</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>Enumeration of <see cref="Segment">segments</see> matching the filtering criteria</returns>
		Task<Segment[]> GetSegmentsAsync(string userKey, long listId, int? limit = 0, int? offset = 0, bool includeDetails = true, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Update a segment (AKA sublist)
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="segmentId">ID of the segment</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="name">Name of the segment.</param>
		/// <param name="query">Rules for the segment.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the segment was updated</returns>
		/// <remarks>A segment is sometimes referred to as a 'sub-list'</remarks>
		Task<bool> UpdateAsync(string userKey, long segmentId, long listId, string name = null, string query = null, long? clientId = default, CancellationToken cancellationToken = default);
	}
}
