using CakeMail.RestClient.Models;
using System;
using System.Collections.Generic;

namespace CakeMail.RestClient
{
	public interface ICakeMailRestClient
	{
		#region Methods related to CAMPAIGNS

		/// <summary>
		/// Create a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">The name of the campaign.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The ID of the newly created campaign</returns>
		int CreateCampaign(string userKey, string name, int? clientId = null);

		bool DeleteCampaign(string userKey, int campaignId, int? clientId = null);

		Campaign GetCampaign(string userKey, int campaignId, int? clientId = null);

		IEnumerable<Campaign> GetCampaigns(string userKey, string status, string name = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null);

		long GetCampaignsCount(string userKey, string status, string name = null, int? clientId = null);

		bool UpdateCampaign(string userKey, int campaignId, string name, int? clientId = null);

		#endregion

		#region Methods related to COUNTRIES

		/// <summary>
		/// Get the list of countries.
		/// </summary>
		/// <returns>An enumeration of <see cref="Country">countries</see>.</returns>
		IEnumerable<Country> GetCountries();

		/// <summary>
		/// Get the list of provinces for a given country.
		/// </summary>
		/// <param name="countryId">ID of the country.</param>
		/// <returns>An enumeration of <see cref="Province">provinces</see>.</returns>
		IEnumerable<Province> GetProvinces(string countryId);

		#endregion

		#region Methods related to RELAYS

		bool SendRelay(string userKey, string email, string senderEmail, string senderName, string html, string text, string subject, string encoding, bool trackOpens, bool trackClicksInHtml, bool trackClicksInText, int trackingId, int? clientId = null);

		IEnumerable<RelayLog> GetRelaySentLogs(string userKey, int? trackingId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		IEnumerable<RelayOpenLog> GetRelayOpenLogs(string userKey, int? trackingId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		IEnumerable<RelayClickLog> GetRelayClickLogs(string userKey, int? trackingId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		IEnumerable<RelayBounceLog> GetRelayBounceLogs(string userKey, int? trackingId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		#endregion

		#region Methods related to USERS

		/// <summary>
		/// Create a user.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="email">Email address of the user.</param>
		/// <param name="firstName">First name of the user.</param>
		/// <param name="lastName">Last name of the user.</param>
		/// <param name="title">Title of the user.</param>
		/// <param name="officePhone">Office phone number of the user.</param>
		/// <param name="mobilePhone">Mobile phone number of the user.</param>
		/// <param name="language">Language of the user. For example: 'en_US' for English (US).</param>
		/// <param name="timezoneId">ID of the timezone of the user.</param>
		/// <param name="password">Password of the user.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The ID of the newly created user</returns>
		int CreateUser(string userKey, string email, string firstName, string lastName, string title, string officePhone, string mobilePhone, string language, string password, int timezoneId = 542, int? clientId = null);

		bool DeactivateUser(string userKey, int userId, int? clientId = null);

		bool DeleteUser(string userKey, int userId, int? clientId = null);

		/// <summary>
		/// Get the information of a user.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The <see cref="User"/></returns>
		User GetUser(string userKey, int userId, int? clientId = null);

		/// <summary>
		/// Get the list of users.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the user status. Possible values: 'active', 'suspended'</param>
		/// <param name="limit">Limit the number of resulting users.</param>
		/// <param name="offset">Offset the beginning of resulting users.</param>
		/// <param name="count">Return the number of users.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>An enumeration of <see cref="User">users</see>.</returns>
		IEnumerable<User> GetUsers(string userKey, string status, int limit = 0, int offset = 0, int? clientId = null);

		/// <summary>
		/// Get the list of users.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the user status. Possible values: 'active', 'suspended'</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The count of users.</returns>
		long GetUsersCount(string userKey, string status, int? clientId = null);

		/// <summary>
		/// Update a user.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="status">Status of the user. Possible values: 'active', 'suspended'.</param>
		/// <param name="email">Email address of the user.</param>
		/// <param name="firstName">First name of the user.</param>
		/// <param name="lastName">Last name of the user.</param>
		/// <param name="title">Title of the user.</param>
		/// <param name="officePhone">Office phone number of the user.</param>
		/// <param name="mobilePhone">Mobile phone number of the user.</param>
		/// <param name="language">Language of the user. For example: 'en_US' for English (US).</param>
		/// <param name="timezoneId">ID of the timezone of the user.</param>
		/// <param name="password">Password of the user.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The <see cref="User"/></returns>
		bool UpdateUser(string userKey, int userId, string status, string email, string firstName, string lastName, string title, string officePhone, string mobilePhone, string language, string timezoneId, string password, int? clientId = null);

		/// <summary>
		/// Check the login of a user.
		/// </summary>
		/// <param name="userName">Email address of the user.</param>
		/// <param name="password">Password of the user.</param>
		/// <param name="clientId">ID of the client to check for the login.</param>
		/// <returns>The <see cref="LoginInfo"/></returns>
		LoginInfo Login(string userName, string password, int? clientId = null);

		#endregion
	}
}