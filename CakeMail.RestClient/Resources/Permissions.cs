using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public class Permissions
	{
		#region Fields

		private readonly CakeMailRestClient _cakeMailRestClient;

		#endregion

		#region Constructor

		public Permissions(CakeMailRestClient cakeMailRestClient)
		{
			_cakeMailRestClient = cakeMailRestClient;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Retrieve the list of permissions for a given user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of permissions</returns>
		public async Task<IEnumerable<string>> GetUserPermissionsAsync(string userKey, long userId, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/Permission/GetPermissions/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("user_id", userId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<string>(path, parameters, "permissions", cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Set the permissions granted to a given user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="permissions">Enumeration of permissions</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the operation succeeded</returns>
		public async Task<bool> SetUserPermissionsAsync(string userKey, long userId, IEnumerable<string> permissions, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/Permission/SetPermissions/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("user_id", userId)
			};
			foreach (var item in permissions.Select((name, i) => new { Index = i, Name = name }))
			{
				parameters.Add(new KeyValuePair<string, object>(string.Format("permission[{0}]", item.Index), item.Name));
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		#endregion
	}
}
