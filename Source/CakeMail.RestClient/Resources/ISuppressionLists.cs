using CakeMail.RestClient.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	///Allows you to manage suppression lists
	/// </summary>
	public interface ISuppressionLists
	{
		/// <summary>
		/// Add domains to the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="domains">The domains to add to the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="SuppressDomainResult">results</see>. Each item in this enumeration indicates the result of adding a domain to the suppression list.</returns>
		Task<SuppressDomainResult[]> AddDomainsAsync(string userKey, IEnumerable<string> domains, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Add email addresses to the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="emailAddresses">The email addresses to add to the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="SuppressEmailResult">results</see>. Each item in this enumeration indicates the result of adding an email address to the suppression list.</returns>
		Task<SuppressEmailResult[]> AddEmailAddressesAsync(string userKey, IEnumerable<string> emailAddresses, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Add localparts to the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="localParts">The localparts to add to the suppression list</param>
		/// <param name="clientId">Client ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="SuppressLocalPartResult">results</see>. Each item in this enumeration indicates the result of adding a localpart to the suppression list.</returns>
		Task<SuppressLocalPartResult[]> AddLocalPartsAsync(string userKey, IEnumerable<string> localParts, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Retrieve the domains on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="limit">Limit the number of resulting domains.</param>
		/// <param name="offset">Offset the beginning of resulting domains.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of domains.</returns>
		Task<string[]> GetDomainsAsync(string userKey, int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Get a count of domains on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of domains on the suppresssion list</returns>
		Task<long> GetDomainsCountAsync(string userKey, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Retrieve the email addresses on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="limit">Limit the number of resulting email addresses.</param>
		/// <param name="offset">Offset the beginning of resulting email addresses.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="SuppressedEmail">addresses</see>. The result also indicates how each email address ended up on the suppression list.</returns>
		Task<SuppressedEmail[]> GetEmailAddressesAsync(string userKey, int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Get a count of email addresses on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of email addresses on the suppresssion list</returns>
		Task<long> GetEmailAddressesCountAsync(string userKey, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Retrieve the localparts on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="limit">Limit the number of resulting localparts.</param>
		/// <param name="offset">Offset the beginning of resulting localparts.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of localparts.</returns>
		Task<string[]> GetLocalPartsAsync(string userKey, int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Get a count of localparts on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of localparts on the suppresssion list</returns>
		Task<long> GetLocalPartsCountAsync(string userKey, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Remove domains from the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="domains">The domains to remove from the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="SuppressDomainResult">results</see>. Each item in this enumeration indicates the result of removing a domain from the suppression list.</returns>
		Task<SuppressDomainResult[]> RemoveDomainsAsync(string userKey, IEnumerable<string> domains, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Remove email addresses from the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="emailAddresses">The email addresses to remove from the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="SuppressEmailResult">results</see>. Each item in this enumeration indicates the result of removing an email address from the suppression list.</returns>
		Task<SuppressEmailResult[]> RemoveEmailAddressesAsync(string userKey, IEnumerable<string> emailAddresses, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Remove localparts from the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="localParts">The localparts to remove from the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="SuppressLocalPartResult">results</see>. Each item in this enumeration indicates the result of removing a localpart from the suppression list.</returns>
		Task<SuppressLocalPartResult[]> RemoveLocalPartsAsync(string userKey, IEnumerable<string> localParts, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
	}
}
