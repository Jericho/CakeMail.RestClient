using CakeMail.RestClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage users
	/// </summary>
	public interface IUsers
	{
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
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>ID of the new user</returns>
		Task<long> CreateAsync(string userKey, string email, string password, string firstName = null, string lastName = null, string title = null, string officePhone = null, string mobilePhone = null, string language = null, long timezoneId = 542, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Suspend a user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the user is suspended</returns>
		Task<bool> DeactivateAsync(string userKey, long userId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete a user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the user is deleted</returns>
		Task<bool> DeleteAsync(string userKey, long userId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve a user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user</param>
		/// <param name="clientId">ID of the client</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="User">user</see></returns>
		Task<User> GetAsync(string userKey, long userId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of users matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the user status. Possible values: 'active', 'suspended'</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The count of users matching the filtering criteria</returns>
		Task<long> GetCountAsync(string userKey, UserStatus? status = default(UserStatus?), long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the users matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the user status. Possible values: 'active', 'suspended'</param>
		/// <param name="limit">Limit the number of resulting users.</param>
		/// <param name="offset">Offset the beginning of resulting users.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>Enumeration of <see cref="User">users</see> matching the filtering criteria</returns>
		Task<User[]> GetUsersAsync(string userKey, UserStatus? status = default(UserStatus?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Validate user name and password
		/// </summary>
		/// <param name="email">Email address of the user.</param>
		/// <param name="password">Password of the user.</param>
		/// <param name="clientId">ID of the client</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="LoginInfo">login information</see> for the user</returns>
		Task<LoginInfo> LoginAsync(string email, string password, long? clientId = default(long?), CancellationToken cancellationToken = default);

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
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the user was updated</returns>
		Task<bool> UpdateAsync(string userKey, long userId, string email = null, string password = null, string firstName = null, string lastName = null, string title = null, string officePhone = null, string mobilePhone = null, string language = null, long? timezoneId = default(long?), UserStatus? status = default(UserStatus?), long? clientId = default(long?), CancellationToken cancellationToken = default);
	}
}
