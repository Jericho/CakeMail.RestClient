using CakeMail.RestClient.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage Clients
	/// </summary>
	public interface IClients
	{
		/// <summary>
		/// Activates client that was previously suspended.
		/// </summary>
		/// <param name="userKey">The user key.</param>
		/// <param name="clientId">The client identifier.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the client was activated</returns>
		/// <remarks>This method is simply a shortcut for: UpdateClient(userKey, clientId, status: "active")</remarks>
		Task<bool> ActivateAsync(string userKey, long clientId, CancellationToken cancellationToken = default);

		/// <summary>
		/// Confirms a client.
		/// </summary>
		/// <param name="confirmationCode">The confirmation code.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns><see cref="ClientRegistrationInfo">Information</see> about the activated client</returns>
		Task<ClientRegistrationInfo> ConfirmAsync(string confirmationCode, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new client.
		/// </summary>
		/// <param name="parentId">The parent identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="address1">The address1.</param>
		/// <param name="address2">The address2.</param>
		/// <param name="city">The city.</param>
		/// <param name="provinceId">The province identifier.</param>
		/// <param name="postalCode">The postal code.</param>
		/// <param name="countryId">The country identifier.</param>
		/// <param name="website">The website.</param>
		/// <param name="phone">The phone.</param>
		/// <param name="fax">The fax.</param>
		/// <param name="adminEmail">The admin email.</param>
		/// <param name="adminFirstName">First name of the admin.</param>
		/// <param name="adminLastName">Last name of the admin.</param>
		/// <param name="adminTitle">The admin title.</param>
		/// <param name="adminOfficePhone">The admin office phone.</param>
		/// <param name="adminMobilePhone">The admin mobile phone.</param>
		/// <param name="adminLanguage">The admin language.</param>
		/// <param name="adminTimezoneId">The admin timezone identifier.</param>
		/// <param name="adminPassword">The admin password.</param>
		/// <param name="primaryContactSameAsAdmin">if set to <c>true</c> [primary contact same as admin].</param>
		/// <param name="primaryContactEmail">The primary contact email.</param>
		/// <param name="primaryContactFirstName">First name of the primary contact.</param>
		/// <param name="primaryContactLastName">Last name of the primary contact.</param>
		/// <param name="primaryContactTitle">The primary contact title.</param>
		/// <param name="primaryContactOfficePhone">The primary contact office phone.</param>
		/// <param name="primaryContactMobilePhone">The primary contact mobile phone.</param>
		/// <param name="primaryContactLanguage">The primary contact language.</param>
		/// <param name="primaryContactTimezoneId">The primary contact timezone identifier.</param>
		/// <param name="primaryContactPassword">The primary contact password.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <remarks>After the client is created, it needs to be <see cref="ConfirmAsync(string, CancellationToken)">confirmed</see> using the confirmation code returned by this method.</remarks>
		/// <returns>The confirmation code</returns>
		Task<string> CreateAsync(long parentId, string name, string address1 = null, string address2 = null, string city = null, string provinceId = null, string postalCode = null, string countryId = null, string website = null, string phone = null, string fax = null, string adminEmail = null, string adminFirstName = null, string adminLastName = null, string adminTitle = null, string adminOfficePhone = null, string adminMobilePhone = null, string adminLanguage = null, long? adminTimezoneId = default, string adminPassword = null, bool primaryContactSameAsAdmin = true, string primaryContactEmail = null, string primaryContactFirstName = null, string primaryContactLastName = null, string primaryContactTitle = null, string primaryContactOfficePhone = null, string primaryContactMobilePhone = null, string primaryContactLanguage = null, long? primaryContactTimezoneId = default, string primaryContactPassword = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// Deletes a client.
		/// </summary>
		/// <param name="userKey">The user key.</param>
		/// <param name="clientId">The client identifier.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the client was deleted</returns>
		/// <remarks>This method is simply a shortcut for: UpdateClient(userKey, clientId, status: "deleted")</remarks>
		Task<bool> DeleteAsync(string userKey, long clientId, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve a pending client.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="confirmationCode">Confirmation code to get the information of a pending client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="UnConfirmedClient">client</see></returns>
		/// <remarks>Pending clients must be activated before they can start using the CakeMail service.</remarks>
		Task<UnConfirmedClient> GetAsync(string userKey, string confirmationCode, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="startDate">Start date to return stats about the client.</param>
		/// <param name="endDate">End date to return stats about the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="Client">client</see></returns>
		Task<Client> GetAsync(string userKey, long clientId, DateTime? startDate = default, DateTime? endDate = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of clients matching the filtering criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the client status. Possible values: 'all', 'pending', 'trial', 'active', 'suspended_all'</param>
		/// <param name="name">Filter using the client name.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of clients matching the filtering criteria</returns>
		Task<long> GetCountAsync(string userKey, ClientStatus? status = default, string name = null, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the clients matching the filtering criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the client status. Possible values: 'all', 'pending', 'trial', 'active', 'suspended_all'</param>
		/// <param name="name">Filter using the client name.</param>
		/// <param name="sortBy">Sort resulting campaigns. Possible values: 'company_name', 'registered_date', 'mailing_limit', 'month_limit', 'contact_limit', 'last_activity'</param>
		/// <param name="sortDirection">Direction of the sorting. Possible value 'asc', 'desc'</param>
		/// <param name="limit">Limit the number of resulting clients.</param>
		/// <param name="offset">Offset the beginning of resulting clients.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>Enumeration of <see cref="Client">clients</see> matching the filtering criteria</returns>
		Task<Client[]> GetListAsync(string userKey, ClientStatus? status = default, string name = null, ClientsSortBy? sortBy = default, SortDirection? sortDirection = default, int? limit = 0, int? offset = 0, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Suspend a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the client was suspended</returns>
		/// <remarks>This method is simply a shortcut for: UpdateClient(userKey, clientId, status: "suspended_by_reseller")</remarks>
		Task<bool> SuspendAsync(string userKey, long clientId, CancellationToken cancellationToken = default);

		/// <summary>
		/// Update a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="name">Name of the client</param>
		/// <param name="status">Status of the client. Possible values: 'trial', 'active', 'suspended_by_reseller', 'deleted'</param>
		/// <param name="parentId">ID of the parent client</param>
		/// <param name="address1">Address line 1 of the client</param>
		/// <param name="address2">Address line 2 of the client</param>
		/// <param name="city">City of the client</param>
		/// <param name="provinceId">ID of the province of the client</param>
		/// <param name="postalCode">Postal Code of the client</param>
		/// <param name="countryId">ID or the country of the client</param>
		/// <param name="website">Website URL of the client</param>
		/// <param name="phone">Phone number of the client</param>
		/// <param name="fax">Fax number of the client</param>
		/// <param name="authDomain"></param>
		/// <param name="bounceDomain"></param>
		/// <param name="dkimDomain"></param>
		/// <param name="doptinIp"></param>
		/// <param name="forwardDomain"></param>
		/// <param name="forwardIp"></param>
		/// <param name="ipPool"></param>
		/// <param name="mdDomain"></param>
		/// <param name="isReseller">Is the client a reseller?</param>
		/// <param name="currency"></param>
		/// <param name="planType"></param>
		/// <param name="mailingLimit">Per campaign limit of the client.</param>
		/// <param name="monthLimit">Monthly emails sent limit of the client.</param>
		/// <param name="contactLimit">Number of contacts limit of the client.</param>
		/// <param name="defaultMailingLimit"></param>
		/// <param name="defaultMonthLimit"></param>
		/// <param name="defaultContactLimit"></param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the record was updated.</returns>
		Task<bool> UpdateAsync(string userKey, long clientId, string name = null, ClientStatus? status = default, long? parentId = default, string address1 = null, string address2 = null, string city = null, string provinceId = null, string postalCode = null, string countryId = null, string website = null, string phone = null, string fax = null, string authDomain = null, string bounceDomain = null, string dkimDomain = null, string doptinIp = null, string forwardDomain = null, string forwardIp = null, string ipPool = null, string mdDomain = null, bool? isReseller = default, string currency = null, string planType = null, int? mailingLimit = default, int? monthLimit = default, int? contactLimit = default, int? defaultMailingLimit = default, int? defaultMonthLimit = default, int? defaultContactLimit = default, CancellationToken cancellationToken = default);
	}
}
