using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage permissions
	/// </summary>
	public interface IPermissions
	{
		/// <summary>
		/// Retrieve the list of permissions for a given user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of permissions</returns>
		Task<string[]> GetUserPermissionsAsync(string userKey, long userId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Set the permissions granted to a given user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="permissions">Enumeration of permissions</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the operation succeeded</returns>
		Task<bool> SetUserPermissionsAsync(string userKey, long userId, IEnumerable<string> permissions, long? clientId = default(long?), CancellationToken cancellationToken = default);
	}
}
