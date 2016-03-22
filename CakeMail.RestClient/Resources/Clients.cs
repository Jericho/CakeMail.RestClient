using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public class Clients
	{
		#region Fields

		private readonly CakeMailRestClient _cakeMailRestClient;

		#endregion

		#region Constructor

		public Clients(CakeMailRestClient cakeMailRestClient)
		{
			_cakeMailRestClient = cakeMailRestClient;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Create a client
		/// </summary>
		/// <param name="parentId">ID of the parent client.</param>
		/// <param name="name">Name of the client</param>
		/// <param name="address1">Address of the client</param>
		/// <param name="address2">Address of the client</param>
		/// <param name="city">City of the client</param>
		/// <param name="provinceId">ID of the province of the client</param>
		/// <param name="postalCode">Postal Code of the client</param>
		/// <param name="countryId">ID or the country of the client</param>
		/// <param name="website">Website URL of the client</param>
		/// <param name="phone">Phone number of the client</param>
		/// <param name="fax">Fax number of the client</param>
		/// <param name="adminEmail">Email address of the admin user</param>
		/// <param name="adminFirstName">First name of the admin user</param>
		/// <param name="adminLastName">Last name of the admin user</param>
		/// <param name="adminTitle">Title of the admin user</param>
		/// <param name="adminOfficePhone">Office phone of the admin user</param>
		/// <param name="adminMobilePhone">Mobile phone of the admin user</param>
		/// <param name="adminLanguage">Language of the admin user. e.g.: 'en-US' for English (US)</param>
		/// <param name="adminTimezoneId">ID of the timezone of the admin user</param>
		/// <param name="adminPassword">Password of the admin user</param>
		/// <param name="primaryContactSameAsAdmin">Is the primary contact the same person as the admin user?</param>
		/// <param name="primaryContactEmail">Email address of the primary contact</param>
		/// <param name="primaryContactFirstName">First name of the primary contact</param>
		/// <param name="primaryContactLastName">Last name of the primary contact</param>
		/// <param name="primaryContactTitle">Title of the primary contact</param>
		/// <param name="primaryContactOfficePhone">Office phone of the primary contact</param>
		/// <param name="primaryContactMobilePhone">Mobile phone of the primary contact</param>
		/// <param name="primaryContactLanguage">Language of the primary contact. e.g.: 'en-US' for English (US)</param>
		/// <param name="primaryContactTimezoneId">ID of the timezone of the primary contact</param>
		/// <param name="primaryContactPassword">Password of the primary contact</param>
		/// <returns>A confirmation code which must be used subsequently to 'activate' the client</returns>
		public async Task<string> CreateAsync(long parentId, string name, string address1 = null, string address2 = null, string city = null, string provinceId = null, string postalCode = null, string countryId = null, string website = null, string phone = null, string fax = null, string adminEmail = null, string adminFirstName = null, string adminLastName = null, string adminTitle = null, string adminOfficePhone = null, string adminMobilePhone = null, string adminLanguage = null, long? adminTimezoneId = null, string adminPassword = null, bool primaryContactSameAsAdmin = true, string primaryContactEmail = null, string primaryContactFirstName = null, string primaryContactLastName = null, string primaryContactTitle = null, string primaryContactOfficePhone = null, string primaryContactMobilePhone = null, string primaryContactLanguage = null, long? primaryContactTimezoneId = null, string primaryContactPassword = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/Client/Create/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("parent_id", parentId),
				new KeyValuePair<string, object>("company_name", name)
			};

			// Address and phone
			if (address1 != null) parameters.Add(new KeyValuePair<string, object>("address1", address1));
			if (address2 != null) parameters.Add(new KeyValuePair<string, object>("address2", address2));
			if (city != null) parameters.Add(new KeyValuePair<string, object>("city", city));
			if (provinceId != null) parameters.Add(new KeyValuePair<string, object>("province_id", provinceId));
			if (postalCode != null) parameters.Add(new KeyValuePair<string, object>("postal_code", postalCode));
			if (countryId != null) parameters.Add(new KeyValuePair<string, object>("country_id", countryId));
			if (website != null) parameters.Add(new KeyValuePair<string, object>("website", website));
			if (phone != null) parameters.Add(new KeyValuePair<string, object>("phone", phone));
			if (fax != null) parameters.Add(new KeyValuePair<string, object>("fax", fax));

			// Admin
			if (adminEmail != null) parameters.Add(new KeyValuePair<string, object>("admin_email", adminEmail));
			if (adminFirstName != null) parameters.Add(new KeyValuePair<string, object>("admin_first_name", adminFirstName));
			if (adminLastName != null) parameters.Add(new KeyValuePair<string, object>("admin_last_name", adminLastName));
			if (adminPassword != null)
			{
				parameters.Add(new KeyValuePair<string, object>("admin_password", adminPassword));
				parameters.Add(new KeyValuePair<string, object>("admin_password_confirmation", adminPassword));
			}
			if (adminTitle != null) parameters.Add(new KeyValuePair<string, object>("admin_title", adminTitle));
			if (adminOfficePhone != null) parameters.Add(new KeyValuePair<string, object>("admin_office_phone", adminOfficePhone));
			if (adminMobilePhone != null) parameters.Add(new KeyValuePair<string, object>("admin_mobile_phone", adminMobilePhone));
			if (adminLanguage != null) parameters.Add(new KeyValuePair<string, object>("admin_language", adminLanguage));
			if (adminTimezoneId.HasValue) parameters.Add(new KeyValuePair<string, object>("admin_timezone_id", adminTimezoneId.Value));

			// Contact
			parameters.Add(new KeyValuePair<string, object>("contact_same_as_admin", primaryContactSameAsAdmin ? "1" : "0"));
			if (!primaryContactSameAsAdmin)
			{
				if (primaryContactEmail != null) parameters.Add(new KeyValuePair<string, object>("contact_email", primaryContactEmail));
				if (primaryContactFirstName != null) parameters.Add(new KeyValuePair<string, object>("contact_first_name", primaryContactFirstName));
				if (primaryContactLastName != null) parameters.Add(new KeyValuePair<string, object>("contact_last_name", primaryContactLastName));
				if (primaryContactPassword != null)
				{
					parameters.Add(new KeyValuePair<string, object>("contact_password", primaryContactPassword));
					parameters.Add(new KeyValuePair<string, object>("contact_password_confirmation", primaryContactPassword));
				}
				if (primaryContactTitle != null) parameters.Add(new KeyValuePair<string, object>("contact_title", primaryContactTitle));
				if (primaryContactLanguage != null) parameters.Add(new KeyValuePair<string, object>("contact_language", primaryContactLanguage));
				if (primaryContactTimezoneId.HasValue) parameters.Add(new KeyValuePair<string, object>("contact_timezone_id", primaryContactTimezoneId.Value));
				if (primaryContactOfficePhone != null) parameters.Add(new KeyValuePair<string, object>("contact_office_phone", primaryContactOfficePhone));
				if (primaryContactMobilePhone != null) parameters.Add(new KeyValuePair<string, object>("contact_mobile_phone", primaryContactMobilePhone));
			}

			return await _cakeMailRestClient.ExecuteRequestAsync<string>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Activate a pending client
		/// </summary>
		/// <param name="confirmationCode">Confirmation code returned by the Create method.</param>
		/// <returns><see cref="ClientRegistrationInfo">Information</see> about the activated client</returns>
		public async Task<ClientRegistrationInfo> ConfirmAsync(string confirmationCode, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/Client/Activate/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("confirmation", confirmationCode)
			};

			return await _cakeMailRestClient.ExecuteRequestAsync<ClientRegistrationInfo>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="startDate">Start date to return stats about the client.</param>
		/// <param name="endDate">End date to return stats about the client.</param>
		/// <returns>The <see cref="Client">client</see></returns>
		public async Task<Client> GetAsync(string userKey, long clientId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/Client/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("client_id", clientId)
			};
			if (startDate.HasValue) parameters.Add(new KeyValuePair<string, object>("start_date", startDate.Value.ToCakeMailString()));
			if (endDate.HasValue) parameters.Add(new KeyValuePair<string, object>("end_date", endDate.Value.ToCakeMailString()));

			return await _cakeMailRestClient.ExecuteRequestAsync<Client>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve a pending client.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="confirmationCode">Confirmation code to get the information of a pending client.</param>
		/// <returns>The <see cref="UnConfirmedClient">client</see></returns>
		/// <remarks>Pending clients must be activated before they can start using the CakeMail service.</remarks>
		public async Task<UnConfirmedClient> GetAsync(string userKey, string confirmationCode, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/Client/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("confirmation", confirmationCode)
			};

			return await _cakeMailRestClient.ExecuteRequestAsync<UnConfirmedClient>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

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
		/// <returns>Enumeration of <see cref="Client">clients</see> matching the filtering criteria</returns>
		public async Task<IEnumerable<Client>> GetListAsync(string userKey, ClientStatus? status = null, string name = null, ClientsSortBy? sortBy = null, SortDirection? sortDirection = null, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/Client/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("company_name", name));
			if (sortBy.HasValue) parameters.Add(new KeyValuePair<string, object>("sort_by", sortBy.Value.GetEnumMemberValue()));
			if (sortDirection.HasValue) parameters.Add(new KeyValuePair<string, object>("direction", sortDirection.Value.GetEnumMemberValue()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<Client>(path, parameters, "clients", cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Get a count of clients matching the filtering criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the client status. Possible values: 'all', 'pending', 'trial', 'active', 'suspended_all'</param>
		/// <param name="name">Filter using the client name.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The number of clients matching the filtering criteria</returns>
		public async Task<long> GetCountAsync(string userKey, ClientStatus? status = null, string name = null, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/Client/GetList/";
			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("company_name", name));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteCountRequestAsync(path, parameters, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Update a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="name">Name of the client</param>
		/// <param name="status">Status of the client. Possible values: 'trial', 'active', 'suspended_by_reseller', 'deleted'</param>
		/// <param name="parentId">ID of the parent client</param>
		/// <param name="address1">Address of the client</param>
		/// <param name="address2">Address of the client</param>
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
		/// <returns>True if the record was updated.</returns>
		public async Task<bool> UpdateAsync(string userKey, long clientId, string name = null, ClientStatus? status = null, long? parentId = null, string address1 = null, string address2 = null, string city = null, string provinceId = null, string postalCode = null, string countryId = null, string website = null, string phone = null, string fax = null, string authDomain = null, string bounceDomain = null, string dkimDomain = null, string doptinIp = null, string forwardDomain = null, string forwardIp = null, string ipPool = null, string mdDomain = null, bool? isReseller = null, string currency = null, string planType = null, int? mailingLimit = null, int? monthLimit = null, int? contactLimit = null, int? defaultMailingLimit = null, int? defaultMonthLimit = null, int? defaultContactLimit = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/Client/SetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("client_id", clientId)
			};
			if (name != null) parameters.Add(new KeyValuePair<string, object>("company_name", name));
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (parentId.HasValue) parameters.Add(new KeyValuePair<string, object>("parent_id", parentId.Value));
			if (address1 != null) parameters.Add(new KeyValuePair<string, object>("address1", address1));
			if (address2 != null) parameters.Add(new KeyValuePair<string, object>("address2", address2));
			if (city != null) parameters.Add(new KeyValuePair<string, object>("city", city));
			if (provinceId != null) parameters.Add(new KeyValuePair<string, object>("province_id", provinceId));
			if (postalCode != null) parameters.Add(new KeyValuePair<string, object>("postal_code", postalCode));
			if (countryId != null) parameters.Add(new KeyValuePair<string, object>("country_id", countryId));
			if (website != null) parameters.Add(new KeyValuePair<string, object>("website", website));
			if (phone != null) parameters.Add(new KeyValuePair<string, object>("phone", phone));
			if (fax != null) parameters.Add(new KeyValuePair<string, object>("fax", fax));
			if (authDomain != null) parameters.Add(new KeyValuePair<string, object>("auth_domain", authDomain));
			if (bounceDomain != null) parameters.Add(new KeyValuePair<string, object>("bounce_domain", bounceDomain));
			if (dkimDomain != null) parameters.Add(new KeyValuePair<string, object>("dkim_domain", dkimDomain));
			if (doptinIp != null) parameters.Add(new KeyValuePair<string, object>("doptin_ip", doptinIp));
			if (forwardDomain != null) parameters.Add(new KeyValuePair<string, object>("forward_domain", forwardDomain));
			if (forwardIp != null) parameters.Add(new KeyValuePair<string, object>("forward_ip", forwardIp));
			if (ipPool != null) parameters.Add(new KeyValuePair<string, object>("ip_pool", ipPool));
			if (mdDomain != null) parameters.Add(new KeyValuePair<string, object>("md_domain", mdDomain));
			if (isReseller.HasValue) parameters.Add(new KeyValuePair<string, object>("reseller", isReseller.Value ? "1" : "0"));
			if (currency != null) parameters.Add(new KeyValuePair<string, object>("currency", currency));
			if (planType != null) parameters.Add(new KeyValuePair<string, object>("plan_type", planType));
			if (mailingLimit.HasValue) parameters.Add(new KeyValuePair<string, object>("mailing_limit", mailingLimit.Value));
			if (monthLimit.HasValue) parameters.Add(new KeyValuePair<string, object>("month_limit", monthLimit.Value));
			if (contactLimit.HasValue) parameters.Add(new KeyValuePair<string, object>("contact_limit", contactLimit.Value));
			if (defaultMailingLimit.HasValue) parameters.Add(new KeyValuePair<string, object>("default_mailing_limit", defaultMailingLimit.Value));
			if (defaultMonthLimit.HasValue) parameters.Add(new KeyValuePair<string, object>("default_month_limit", defaultMonthLimit.Value));
			if (defaultContactLimit.HasValue) parameters.Add(new KeyValuePair<string, object>("default_contact_limit", defaultContactLimit.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Activate a client which has been previously suspended
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the client was activated</returns>
		/// <remarks>This method is simply a shortcut for: UpdateClient(userKey, clientId, status: "active")</remarks>
		public async Task<bool> ActivateAsync(string userKey, long clientId, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await UpdateAsync(userKey, clientId, status: ClientStatus.Active, cancellationToken: cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Suspend a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the client was suspended</returns>
		/// <remarks>This method is simply a shortcut for: UpdateClient(userKey, clientId, status: "suspended_by_reseller")</remarks>
		public async Task<bool> SuspendAsync(string userKey, long clientId, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await UpdateAsync(userKey, clientId, status: ClientStatus.SuspendedByReseller, cancellationToken: cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Delete a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the client was deleted</returns>
		/// <remarks>This method is simply a shortcut for: UpdateClient(userKey, clientId, status: "deleted")</remarks>
		public async Task<bool> DeleteAsync(string userKey, long clientId, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await UpdateAsync(userKey, clientId, status: ClientStatus.Deleted, cancellationToken: cancellationToken).ConfigureAwait(false);
		}

		#endregion
	}
}
