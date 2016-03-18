using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public class Users
	{
		#region Fields

		private readonly CakeMailRestClient _cakeMailRestClient;

		#endregion

		#region Constructor

		public Users(CakeMailRestClient cakeMailRestClient)
		{
			_cakeMailRestClient = cakeMailRestClient;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Create a user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="email">Email address of the user.</param>
		/// <param name="password">Password of the user.</param>
		/// <param name="firstName">First name of the user.</param>
		/// <param name="lastName">Last name of the user.</param>
		/// <param name="title">Title of the user.</param>
		/// <param name="officePhone">Office phone number of the user.</param>
		/// <param name="mobilePhone">Mobile phone number of the user.</param>
		/// <param name="language">Language of the user. For example: 'en_US' for English (US)</param>
		/// <param name="timezoneId">ID of the timezone of the user. UTC (id 542) is the default value</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>ID of the new user</returns>
		public async Task<long> CreateAsync(string userKey, string email, string password, string firstName = null, string lastName = null, string title = null, string officePhone = null, string mobilePhone = null, string language = null, long timezoneId = 542, long? clientId = null)
		{
			string path = "/User/Create/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("email", email),
				new KeyValuePair<string, object>("password", password),
				new KeyValuePair<string, object>("password_confirmation", password),
				new KeyValuePair<string, object>("timezone_id", timezoneId)
			};
			if (firstName != null) parameters.Add(new KeyValuePair<string, object>("first_name", firstName));
			if (lastName != null) parameters.Add(new KeyValuePair<string, object>("last_name", lastName));
			if (title != null) parameters.Add(new KeyValuePair<string, object>("title", title));
			if (officePhone != null) parameters.Add(new KeyValuePair<string, object>("office_phone", officePhone));
			if (mobilePhone != null) parameters.Add(new KeyValuePair<string, object>("mobile_phone", mobilePhone));
			if (language != null) parameters.Add(new KeyValuePair<string, object>("language", language));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			// When a new user is created, the payload contains a json object with two properties: 'id' and 'key'. We only care about the ID.
			return await _cakeMailRestClient.ExecuteRequestAsync<long>(path, parameters, "id").ConfigureAwait(false);
		}

		/// <summary>
		/// Suspend a user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the user is suspended</returns>
		public async Task<bool> DeactivateAsync(string userKey, long userId, long? clientId = null)
		{
			return await UpdateAsync(userKey, userId, status: UserStatus.Suspended, clientId: clientId).ConfigureAwait(false);
		}

		/// <summary>
		/// Delete a user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the user is deleted</returns>
		public async Task<bool> DeleteAsync(string userKey, long userId, long? clientId = null)
		{
			return await UpdateAsync(userKey, userId, status: UserStatus.Deleted, clientId: clientId).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve a user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user</param>
		/// <param name="clientId">ID of the client</param>
		/// <returns>The <see cref="User">user</see></returns>
		public async Task<User> GetAsync(string userKey, long userId, long? clientId = null)
		{
			var path = "/User/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("user_id", userId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<User>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the users matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the user status. Possible values: 'active', 'suspended'</param>
		/// <param name="limit">Limit the number of resulting users.</param>
		/// <param name="offset">Offset the beginning of resulting users.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>Enumeration of <see cref="User">users</see> matching the filtering criteria</returns>
		public async Task<IEnumerable<User>> GetUsersAsync(string userKey, UserStatus? status = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/User/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<User>(path, parameters, "users").ConfigureAwait(false);
		}

		/// <summary>
		/// Get a count of users matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the user status. Possible values: 'active', 'suspended'</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The count of users matching the filtering criteria</returns>
		public async Task<long> GetCountAsync(string userKey, UserStatus? status = null, long? clientId = null)
		{
			var path = "/User/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteCountRequestAsync(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Update a user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="email">Email address of the user.</param>
		/// <param name="password">Password of the user.</param>
		/// <param name="firstName">First name of the user.</param>
		/// <param name="lastName">Last name of the user.</param>
		/// <param name="title">Title of the user.</param>
		/// <param name="officePhone">Office phone number of the user.</param>
		/// <param name="mobilePhone">Mobile phone number of the user.</param>
		/// <param name="language">Language of the user. For example: 'en_US' for English (US)</param>
		/// <param name="timezoneId">ID of the timezone of the user.</param>
		/// <param name="status">Status of the user. Possible values: 'active', 'suspended'</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the user was updated</returns>
		public async Task<bool> UpdateAsync(string userKey, long userId, string email = null, string password = null, string firstName = null, string lastName = null, string title = null, string officePhone = null, string mobilePhone = null, string language = null, long? timezoneId = null, UserStatus? status = null, long? clientId = null)
		{
			string path = "/User/SetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("user_id", userId)
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (email != null) parameters.Add(new KeyValuePair<string, object>("email", email));
			if (firstName != null) parameters.Add(new KeyValuePair<string, object>("first_name", firstName));
			if (lastName != null) parameters.Add(new KeyValuePair<string, object>("last_name", lastName));
			if (title != null) parameters.Add(new KeyValuePair<string, object>("title", title));
			if (officePhone != null) parameters.Add(new KeyValuePair<string, object>("office_phone", officePhone));
			if (mobilePhone != null) parameters.Add(new KeyValuePair<string, object>("mobile_phone", mobilePhone));
			if (language != null) parameters.Add(new KeyValuePair<string, object>("language", language));
			if (timezoneId != null) parameters.Add(new KeyValuePair<string, object>("timezone_id", timezoneId));
			if (password != null)
			{
				parameters.Add(new KeyValuePair<string, object>("password", password));
				parameters.Add(new KeyValuePair<string, object>("password_confirmation", password));
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Validate user name and password
		/// </summary>
		/// <param name="email">Email address of the user.</param>
		/// <param name="password">Password of the user.</param>
		/// <param name="clientId">ID of the client</param>
		/// <returns>The <see cref="LoginInfo">login information</see> for the user</returns>
		public async Task<LoginInfo> LoginAsync(string email, string password, long? clientId = null)
		{
			var path = "/User/Login/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("email", email),
				new KeyValuePair<string, object>("password", password)

			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<LoginInfo>(path, parameters).ConfigureAwait(false);
		}

		#endregion
	}
}
