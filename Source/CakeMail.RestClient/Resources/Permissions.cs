using CakeMail.RestClient.Utilities;
using Pathoschild.Http.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage permissions
	/// </summary>
	/// <seealso cref="CakeMail.RestClient.Resources.IPermissions" />
	public class Permissions : IPermissions
	{
		#region Fields

		private readonly IClient _client;

		#endregion

		#region Constructor

		internal Permissions(IClient client)
		{
			_client = client;
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
		public Task<string[]> GetUserPermissionsAsync(string userKey, long userId, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("user_id", userId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Permission/GetPermissions")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<string[]>("permissions");
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
		public Task<bool> SetUserPermissionsAsync(string userKey, long userId, IEnumerable<string> permissions, long? clientId = null, CancellationToken cancellationToken = default)
		{
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

			return _client
				.PostAsync("Permission/SetPermissions")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<bool>();
		}

		#endregion
	}
}
