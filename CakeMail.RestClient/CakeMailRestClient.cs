using CakeMail.RestClient.Exceptions;
using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace CakeMail.RestClient
{
	/// <summary>
	/// Core class for using the CakeMail Api
	/// </summary>
	public class CakeMailRestClient : ICakeMailRestClient
	{
		#region Fields

		private static readonly string _version = GetVersion();
		private readonly IRestClient _client;

		#endregion

		#region Properties

		/// <summary>
		/// The API key provided by CakeMail
		/// </summary>
		public string ApiKey { get; private set; }

		/// <summary>
		/// The web proxy
		/// </summary>
		public IWebProxy Proxy
		{
			get { return _client.Proxy; }
		}

		/// <summary>
		/// The user agent
		/// </summary>
		public string UserAgent
		{
			get { return _client.UserAgent; }
		}

		/// <summary>
		/// The timeout
		/// </summary>
		public int Timeout
		{
			get { return _client.Timeout; }
		}

		/// <summary>
		/// The URL where all API requests are sent
		/// </summary>
		public Uri BaseUrl
		{
			get { return _client.BaseUrl; }
		}

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailRestClient"/> class.
		/// </summary>
		/// <param name="apiKey">The API Key received from CakeMail</param>
		/// <param name="restClient">The rest client</param>
		public CakeMailRestClient(string apiKey, IRestClient restClient)
		{
			this.ApiKey = apiKey;
			_client = restClient;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailRestClient"/> class.
		/// </summary>
		/// <param name="apiKey">The API Key received from CakeMail</param>
		/// <param name="host">The host where the API is hosted. The default is api.wbsrvc.com</param>
		/// <param name="timeout">Timeout in milliseconds for connection to web service. The default is 5000.</param>
		/// <param name="webProxy">The web proxy</param>
		public CakeMailRestClient(string apiKey, string host = "api.wbsrvc.com", int timeout = 5000, IWebProxy webProxy = null)
		{
			this.ApiKey = apiKey;

			_client = new RestSharp.RestClient("https://" + host)
			{
				Timeout = timeout,
				UserAgent = string.Format("CakeMail .NET REST Client {0}", _version),
				Proxy = webProxy
			};
		}

		#endregion

		#region Methods related to CAMPAIGNS

		/// <summary>
		/// Create a new campaign
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the campaign.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is created.</param>
		/// <returns>ID of the new campaign</returns>
		public long CreateCampaign(string userKey, string name, long? clientId = null)
		{
			string path = "/Campaign/Create/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("name", name)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<long>(path, parameters);
		}

		/// <summary>
		/// Delete a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign to delete.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <returns>True if the campaign is deleted</returns>
		public bool DeleteCampaign(string userKey, long campaignId, long? clientId = null)
		{
			string path = "/Campaign/Delete/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("campaign_id", campaignId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Retrieve a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <returns>The <see cref="Campaign">campaign</see></returns>
		public Campaign GetCampaign(string userKey, long campaignId, long? clientId = null)
		{
			var path = "/Campaign/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("campaign_id", campaignId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<Campaign>(path, parameters);
		}

		/// <summary>
		/// Retrieve the campaigns matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the campaign status. Possible value 'ongoing', 'closed'</param>
		/// <param name="name">Filter using the campaign name.</param>
		/// <param name="sortBy">Sort resulting campaigns. Possible value 'created_on', 'name'</param>
		/// <param name="sortDirection">Direction of the sorting. Possible value 'asc', 'desc'</param>
		/// <param name="limit">Limit the number of resulting campaigns.</param>
		/// <param name="offset">Offset the beginning of resulting campaigns.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <returns>Enumeration of <see cref="Campaign">campaigns</see> matching the filtering criteria</returns>
		public IEnumerable<Campaign> GetCampaigns(string userKey, CampaignStatus? status = null, string name = null, CampaignsSortBy? sortBy = null, SortDirection? sortDirection = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/Campaign/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (sortBy.HasValue) parameters.Add(new KeyValuePair<string, object>("sort_by", sortBy.Value.GetEnumMemberValue()));
			if (sortDirection.HasValue) parameters.Add(new KeyValuePair<string, object>("direction", sortDirection.Value.GetEnumMemberValue()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<Campaign[]>(path, parameters, "campaigns");
		}

		/// <summary>
		/// Get a count of campaigns matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the campaign status. Possible value 'ongoing', 'closed'</param>
		/// <param name="name">Filter using the campaign name.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <returns>The count of campaigns matching the filtering criteria</returns>
		public long GetCampaignsCount(string userKey, CampaignStatus? status = null, string name = null, long? clientId = null)
		{
			var path = "/Campaign/GetList/";
			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Update a campaign
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign.</param>
		/// <param name="name">The name of the campaign</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <returns>True if the record was updated.</returns>
		public bool UpdateCampaign(string userKey, long campaignId, string name, long? clientId = null)
		{
			string path = "/Campaign/SetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("campaign_id", campaignId),
				new KeyValuePair<string, object>("name", name)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		#endregion

		#region Methods related to CLIENTS

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
		public string CreateClient(long parentId, string name, string address1 = null, string address2 = null, string city = null, string provinceId = null, string postalCode = null, string countryId = null, string website = null, string phone = null, string fax = null, string adminEmail = null, string adminFirstName = null, string adminLastName = null, string adminTitle = null, string adminOfficePhone = null, string adminMobilePhone = null, string adminLanguage = null, long? adminTimezoneId = null, string adminPassword = null, bool primaryContactSameAsAdmin = true, string primaryContactEmail = null, string primaryContactFirstName = null, string primaryContactLastName = null, string primaryContactTitle = null, string primaryContactOfficePhone = null, string primaryContactMobilePhone = null, string primaryContactLanguage = null, long? primaryContactTimezoneId = null, string primaryContactPassword = null)
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

			return ExecuteRequest<string>(path, parameters);
		}

		/// <summary>
		/// Activate a pending client
		/// </summary>
		/// <param name="confirmationCode">Confirmation code returned by the Create method.</param>
		/// <returns><see cref="ClientRegistrationInfo">Information</see> about the activated client</returns>
		public ClientRegistrationInfo ConfirmClient(string confirmationCode)
		{
			string path = "/Client/Activate/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("confirmation", confirmationCode)
			};

			return ExecuteRequest<ClientRegistrationInfo>(path, parameters);
		}

		/// <summary>
		/// Retrieve a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="startDate">Start date to return stats about the client.</param>
		/// <param name="endDate">End date to return stats about the client.</param>
		/// <returns>The <see cref="Client">client</see></returns>
		public Client GetClient(string userKey, long clientId, DateTime? startDate = null, DateTime? endDate = null)
		{
			var path = "/Client/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("client_id", clientId)
			};
			if (startDate.HasValue) parameters.Add(new KeyValuePair<string, object>("start_date", startDate.Value.ToCakeMailString()));
			if (endDate.HasValue) parameters.Add(new KeyValuePair<string, object>("end_date", endDate.Value.ToCakeMailString()));

			return ExecuteRequest<Client>(path, parameters);
		}

		/// <summary>
		/// Retrieve a pending client.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="confirmationCode">Confirmation code to get the information of a pending client.</param>
		/// <returns>The <see cref="UnConfirmedClient">client</see></returns>
		/// <remarks>Pending clients must be activated before they can start using the CakeMail service.</remarks>
		public UnConfirmedClient GetClient(string userKey, string confirmationCode)
		{
			var path = "/Client/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("confirmation", confirmationCode)
			};

			return ExecuteRequest<UnConfirmedClient>(path, parameters);
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
		public IEnumerable<Client> GetClients(string userKey, ClientStatus? status = null, string name = null, ClientsSortBy? sortBy = null, SortDirection? sortDirection = null, int? limit = 0, int? offset = 0, long? clientId = null)
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

			return ExecuteArrayRequest<Client>(path, parameters, "clients");
		}

		/// <summary>
		/// Get a count of clients matching the filtering criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the client status. Possible values: 'all', 'pending', 'trial', 'active', 'suspended_all'</param>
		/// <param name="name">Filter using the client name.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The number of clients matching the filtering criteria</returns>
		public long GetClientsCount(string userKey, ClientStatus? status = null, string name = null, long? clientId = null)
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

			return ExecuteCountRequest(path, parameters);
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
		public bool UpdateClient(string userKey, long clientId, string name = null, ClientStatus? status = null, long? parentId = null, string address1 = null, string address2 = null, string city = null, string provinceId = null, string postalCode = null, string countryId = null, string website = null, string phone = null, string fax = null, string authDomain = null, string bounceDomain = null, string dkimDomain = null, string doptinIp = null, string forwardDomain = null, string forwardIp = null, string ipPool = null, string mdDomain = null, bool? isReseller = null, string currency = null, string planType = null, int? mailingLimit = null, int? monthLimit = null, int? contactLimit = null, int? defaultMailingLimit = null, int? defaultMonthLimit = null, int? defaultContactLimit = null)
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

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Activate a client which has been previously suspended
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the client was activated</returns>
		/// <remarks>This method is simply a shortcut for: UpdateClient(userKey, clientId, status: "active")</remarks>
		public bool ActivateClient(string userKey, long clientId)
		{
			return UpdateClient(userKey, clientId, status: ClientStatus.Active);
		}

		/// <summary>
		/// Suspend a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the client was suspended</returns>
		/// <remarks>This method is simply a shortcut for: UpdateClient(userKey, clientId, status: "suspended_by_reseller")</remarks>
		public bool SuspendClient(string userKey, long clientId)
		{
			return UpdateClient(userKey, clientId, status: ClientStatus.SuspendedByReseller);
		}

		/// <summary>
		/// Delete a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the client was deleted</returns>
		/// <remarks>This method is simply a shortcut for: UpdateClient(userKey, clientId, status: "deleted")</remarks>
		public bool DeleteClient(string userKey, long clientId)
		{
			return UpdateClient(userKey, clientId, status: ClientStatus.Deleted);
		}

		#endregion

		#region Methods related to PERMISSIONS

		/// <summary>
		/// Retrieve the list of permissions for a given user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>An enumeration of permissions</returns>
		public IEnumerable<string> GetUserPermissions(string userKey, long userId, long? clientId = null)
		{
			var path = "/Permission/GetPermissions/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("user_id", userId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<string>(path, parameters, "permissions");
		}

		/// <summary>
		/// Set the permissions granted to a given user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="permissions">Enumeration of permissions</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the operation succeeded</returns>
		public bool SetUserPermissions(string userKey, long userId, IEnumerable<string> permissions, long? clientId = null)
		{
			var path = "/Permission/SetPermissions/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("user_id", userId)
			};
			foreach (var item in permissions.Select((name, i) => new { Index = i, Name = name }))
			{
				parameters.Add(new KeyValuePair<string, object>(string.Format("permission[{0}]", item.Index), item.Name));
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		#endregion

		#region Methods related to COUNTRIES

		/// <summary>
		/// Get the list of countries
		/// </summary>
		/// <returns>An enumeration of <see cref="Country">countries</see></returns>
		public IEnumerable<Country> GetCountries()
		{
			var path = "/Country/GetList/";

			return ExecuteArrayRequest<Country>(path, null, "countries");
		}

		/// <summary>
		/// Get the list of state/provinces for a given country
		/// </summary>
		/// <param name="countryId">ID of the country.</param>
		/// <returns>An enumeration of <see cref="Province">privinces</see></returns>
		public IEnumerable<Province> GetProvinces(string countryId)
		{
			var path = "/Country/GetProvinces/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("country_id", countryId)
			};

			return ExecuteArrayRequest<Province>(path, parameters, "provinces");
		}

		#endregion

		#region Methods related to LISTS

		/// <summary>
		/// Create a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the list.</param>
		/// <param name="defaultSenderName">Name of the default sender of the list.</param>
		/// <param name="defaultSenderEmailAddress">Email of the default sender of the list.</param>
		/// <param name="spamPolicyAccepted">Indicates if the anti-spam policy has been accepted</param>
		/// <param name="clientId">Client ID of the client in which the list is created.</param>
		/// <returns>ID of the new list</returns>
		public long CreateList(string userKey, string name, string defaultSenderName, string defaultSenderEmailAddress, bool spamPolicyAccepted = false, long? clientId = null)
		{
			string path = "/List/Create/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("name", name),
				new KeyValuePair<string, object>("sender_name", defaultSenderName),
				new KeyValuePair<string, object>("sender_email", defaultSenderEmailAddress)
			};
			if (spamPolicyAccepted) parameters.Add(new KeyValuePair<string, object>("list_policy", "accepted"));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<long>(path, parameters);
		}

		/// <summary>
		/// Delete a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>True if the list is deleted</returns>
		public bool DeleteList(string userKey, long listId, long? clientId = null)
		{
			string path = "/List/Delete/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Retrieve a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="includeStatistics">True if you want the statistics</param>
		/// <param name="calculateEngagement">True if you want the engagement information to be calculated</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>The <see cref="List">list</see></returns>
		public List GetList(string userKey, long listId, bool includeStatistics = true, bool calculateEngagement = false, long? clientId = null)
		{
			var path = "/List/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("no_details", includeStatistics ? "false" : "true"),	// CakeMail expects 'false' if you want to include details
				new KeyValuePair<string, object>("with_engagement", calculateEngagement ? "true" : "false")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<List>(path, parameters);
		}

		/// <summary>
		/// Retrieve the lists matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the list status. Possible values: 'active', 'archived'</param>
		/// <param name="name">Filter using the list name.</param>
		/// <param name="sortBy">Sort resulting lists. Possible values: 'name', 'created_on', 'active_members_count'</param>
		/// <param name="sortDirection">Direction of the sorting. Possible values: 'asc', 'desc'</param>
		/// <param name="limit">Limit the number of resulting lists.</param>
		/// <param name="offset">Offset the beginning of resulting lists.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>Enumeration of <see cref="List">lists</see> matching the filtering criteria</returns>
		public IEnumerable<List> GetLists(string userKey, ListStatus? status = null, string name = null, ListsSortBy? sortBy = null, SortDirection? sortDirection = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/List/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (sortBy.HasValue) parameters.Add(new KeyValuePair<string, object>("sort_by", sortBy.Value.GetEnumMemberValue()));
			if (sortDirection.HasValue) parameters.Add(new KeyValuePair<string, object>("direction", sortDirection.Value.GetEnumMemberValue()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<List>(path, parameters, "lists");
		}

		/// <summary>
		/// Get a count of lists matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the list status. Possible value 'active', 'archived'</param>
		/// <param name="name">Filter using the list name.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>The count of lists matching the filtering criteria</returns>
		public long GetListsCount(string userKey, ListStatus? status = null, string name = null, long? clientId = null)
		{
			var path = "/List/GetList/";
			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Update a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="name">Name of the list.</param>
		/// <param name="language">Language of the list. e.g.: 'en_US' for English (US)</param>
		/// <param name="spamPolicyAccepted">Indicates if the anti-spam policy has been accepted</param>
		/// <param name="status">Status of the list. Possible values: 'active', 'archived', 'deleted'</param>
		/// <param name="senderName">Name of the default sender of the list.</param>
		/// <param name="senderEmail">Email of the default sender of the list.</param>
		/// <param name="goto_oi">Redirection URL on subscribing to the list.</param>
		/// <param name="goto_di">Redirection URL on confirming the subscription to the list.</param>
		/// <param name="goto_oo">Redirection URL on unsubscribing to the list.</param>
		/// <param name="webhook">Webhook URL for the list.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>True if the list was updated</returns>
		public bool UpdateList(string userKey, long listId, string name = null, string language = null, bool? spamPolicyAccepted = null, ListStatus? status = null, string senderName = null, string senderEmail = null, string goto_oi = null, string goto_di = null, string goto_oo = null, string webhook = null, long? clientId = null)
		{
			string path = "/List/SetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId)
			};
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (language != null) parameters.Add(new KeyValuePair<string, object>("language", language));
			if (spamPolicyAccepted.HasValue) parameters.Add(new KeyValuePair<string, object>("list_policy", spamPolicyAccepted.Value ? "accepted" : "declined"));
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (senderName != null) parameters.Add(new KeyValuePair<string, object>("sender_name", senderName));
			if (senderEmail != null) parameters.Add(new KeyValuePair<string, object>("sender_email", senderEmail));
			if (goto_oi != null) parameters.Add(new KeyValuePair<string, object>("goto_oi", goto_oi));
			if (goto_di != null) parameters.Add(new KeyValuePair<string, object>("goto_di", goto_di));
			if (goto_oo != null) parameters.Add(new KeyValuePair<string, object>("goto_oo", goto_oo));
			if (webhook != null) parameters.Add(new KeyValuePair<string, object>("webhook", webhook));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Add a new field to a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="name">Name of the field.</param>
		/// <param name="type">Type of the field. Possible values: 'text', 'integer', 'datetime' or 'mediumtext'</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>True if the field was added to the list</returns>
		public bool AddListField(string userKey, long listId, string name, FieldType type, long? clientId = null)
		{
			string path = "/List/EditStructure/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("action", "add"),
				new KeyValuePair<string, object>("field", name),
				new KeyValuePair<string, object>("type", type.GetEnumMemberValue())
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Remove a field from a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="name">Name of the field.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>True if the field was removed from the list</returns>
		public bool DeleteListField(string userKey, long listId, string name, long? clientId = null)
		{
			string path = "/List/EditStructure/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("action", "delete"),
				new KeyValuePair<string, object>("field", name)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Retrieve the list of fields of a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>An enumeration of <see cref="ListField">fields</see></returns>
		public IEnumerable<ListField> GetListFields(string userKey, long listId, long? clientId = null)
		{
			var path = "/List/GetFields/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			var fieldsStructure = ExecuteRequest<ExpandoObject>(path, parameters);
			if (fieldsStructure == null) return Enumerable.Empty<ListField>();

			var fields = fieldsStructure.Select(x => new ListField() { Name = x.Key, Type = x.Value.ToString().GetValueFromEnumMember<FieldType>() });
			return fields;
		}

		/// <summary>
		/// Add a test email to the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="email">The email address</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>True if the email address was added</returns>
		public bool AddTestEmail(string userKey, long listId, string email, long? clientId = null)
		{
			string path = "/List/AddTestEmail/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("email", email)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Delete a test email from a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="email">The email address</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>True if the email address was deleted</returns>
		public bool DeleteTestEmail(string userKey, long listId, string email, long? clientId = null)
		{
			string path = "/List/DeleteTestEmail/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("email", email)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Retrieve the lists of test email addresses for a given list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>Enumeration of <see cref="string">test email addresses</see></returns>
		public IEnumerable<string> GetTestEmails(string userKey, long listId, long? clientId = null)
		{
			var path = "/List/GetTestEmails/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<string>(path, parameters, "testemails");
		}

		/// <summary>
		/// Add a subscriber to a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="email">Email address of the subscriber.</param>
		/// <param name="autoResponders">Trigger the autoresponders.</param>
		/// <param name="triggers">Trigger the welcome email.</param>
		/// <param name="customFields">Additional data for the subscriber.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>ID of the new subscriber</returns>
		public long Subscribe(string userKey, long listId, string email, bool autoResponders = true, bool triggers = true, IEnumerable<KeyValuePair<string, object>> customFields = null, long? clientId = null)
		{
			string path = "/List/SubscribeEmail/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("autoresponders", autoResponders ? "true" : "false"),
				new KeyValuePair<string, object>("triggers", triggers ? "true" : "false"),
				new KeyValuePair<string, object>("email", email)
			};
			if (customFields != null)
			{
				foreach (var customField in customFields)
				{
					if (customField.Value is DateTime) parameters.Add(new KeyValuePair<string, object>(string.Format("data[{0}]", customField.Key), ((DateTime)customField.Value).ToCakeMailString()));
					else parameters.Add(new KeyValuePair<string, object>(string.Format("data[{0}]", customField.Key), customField.Value));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<long>(path, parameters);
		}

		/// <summary>
		/// Add multiple subscribers to a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="autoResponders">Trigger the autoresponders.</param>
		/// <param name="triggers">Trigger the welcome email.</param>
		/// <param name="listMembers">Subscribers.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>An enumeration of <see cref="ImportResult">results</see></returns>
		public IEnumerable<ImportResult> Import(string userKey, long listId, IEnumerable<ListMember> listMembers, bool autoResponders = true, bool triggers = true, long? clientId = null)
		{
			string path = "/List/Import/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("import_to", "active"),
				new KeyValuePair<string, object>("autoresponders", autoResponders ? "true" : "false"),
				new KeyValuePair<string, object>("triggers", triggers ? "true" : "false")
			};
			if (listMembers != null)
			{
				foreach (var item in listMembers.Select((member, i) => new { Index = i, Email = member.Email, CustomFields = member.CustomFields }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("record[{0}][email]", item.Index), item.Email));
					if (item.CustomFields != null)
					{
						foreach (var customField in item.CustomFields)
						{
							if (customField.Value is DateTime) parameters.Add(new KeyValuePair<string, object>(string.Format("record[{0}][{1}]", item.Index, customField.Key), ((DateTime)customField.Value).ToCakeMailString()));
							else parameters.Add(new KeyValuePair<string, object>(string.Format("record[{0}][{1}]", item.Index, customField.Key), customField.Value));
						}
					}
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<ImportResult>(path, parameters, null);
		}

		/// <summary>
		/// Unsubscribe a member from the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="email">Email address of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>True if the member was unsubscribed</returns>
		public bool Unsubscribe(string userKey, long listId, string email, long? clientId = null)
		{
			string path = "/List/UnsubscribeEmail/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("email", email)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Unsubscribe a member from the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>True if the member was unsubscribed</returns>
		public bool Unsubscribe(string userKey, long listId, long listMemberId, long? clientId = null)
		{
			string path = "/List/UnsubscribeEmail/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("record_id", listMemberId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Delete a member from the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>True if the member was deleted</returns>
		public bool DeleteListMember(string userKey, long listId, long listMemberId, long? clientId = null)
		{
			string path = "/List/DeleteRecord/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("record_id", listMemberId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Retrieve the information about a list member
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>The <see cref=" ListMember">list mamber</see></returns>
		public ListMember GetListMember(string userKey, long listId, long listMemberId, long? clientId = null)
		{
			var path = "/List/GetRecord/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("record_id", listMemberId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			var listMember = ExecuteRequest<ListMember>(path, parameters);
			return listMember;
		}

		/// <summary>
		/// Retrieve the list members matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="status">Filter using the member status. Possible values: 'active', 'unsubscribed', 'deleted', 'inactive_bounced', 'spam'</param>
		/// <param name="query">Query to retrieve members from a segment.</param>
		/// <param name="sortBy">Sort resulting members. Possible values: 'id', 'email'</param>
		/// <param name="sortDirection">Direction of the sorting. Possible values: 'asc', 'desc'</param>
		/// <param name="limit">Limit the number of resulting members.</param>
		/// <param name="offset">Offset the beginning of resulting members.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>Enumeration of <see cref="List">lists</see> matching the filtering criteria</returns>
		public IEnumerable<ListMember> GetListMembers(string userKey, long listId, ListMemberStatus? status = null, string query = null, ListMembersSortBy? sortBy = null, SortDirection? sortDirection = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/List/Show/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (query != null) parameters.Add(new KeyValuePair<string, object>("query", query));
			if (sortBy.HasValue) parameters.Add(new KeyValuePair<string, object>("sort_by", sortBy.Value.GetEnumMemberValue()));
			if (sortDirection.HasValue) parameters.Add(new KeyValuePair<string, object>("direction", sortDirection.Value.GetEnumMemberValue()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<ListMember>(path, parameters, "records");
		}

		/// <summary>
		/// Get a count of list members matching the filtering criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="status">Filter using the member status. Possible values: 'active', 'unsubscribed', 'deleted', 'inactive_bounced', 'spam'</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The number of list members matching the filtering criteria</returns>
		public long GetListMembersCount(string userKey, long listId, ListMemberStatus? status = null, long? clientId = null)
		{
			var path = "/List/Show/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Update a list member
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the list member</param>
		/// <param name="customFields">Additional data for the member</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the member was updated</returns>
		public bool UpdateListMember(string userKey, long listId, long listMemberId, IEnumerable<KeyValuePair<string, object>> customFields = null, long? clientId = null)
		{
			string path = "/List/UpdateRecord/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("record_id", listMemberId),
			};
			if (customFields != null)
			{
				foreach (var customField in customFields)
				{
					if (customField.Value is DateTime) parameters.Add(new KeyValuePair<string, object>(string.Format("data[{0}]", customField.Key), ((DateTime)customField.Value).ToCakeMailString()));
					else parameters.Add(new KeyValuePair<string, object>(string.Format("data[{0}]", customField.Key), customField.Value));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Retrieve the log items for a given list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="logType">Filter using the log action. Possible values: "subscribe", "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped"</param>
		/// <param name="uniques">Return unique log items per member</param>
		/// <param name="totals">Return all the log items</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>An enumeration of <see cref="LogItem">log items</see> matching the filter criteria</returns>
		public IEnumerable<LogItem> GetListLogs(string userKey, long listId, LogType? logType = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			string path = "/List/GetLog/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("totals", totals ? "true" : "false"),
				new KeyValuePair<string, object>("uniques", uniques ? "true" : "false"),
				new KeyValuePair<string, object>("count", "false")
			};
			if (logType.HasValue) parameters.Add(new KeyValuePair<string, object>("action", logType.Value.GetEnumMemberValue()));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<LogItem>(path, parameters, "logs");
		}

		/// <summary>
		/// Get a count of log items matching the filter criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="logType">Filter using the log action. Possible values: "subscribe", "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped"</param>
		/// <param name="uniques">Return unique log items per member</param>
		/// <param name="totals">Return all the log items</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>The number of log items matching the filtering criteria</returns>
		public long GetListLogsCount(string userKey, long listId, LogType? logType = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, long? clientId = null)
		{
			string path = "/List/GetLog/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("totals", totals ? "true" : "false"),
				new KeyValuePair<string, object>("uniques", uniques ? "true" : "false"),
				new KeyValuePair<string, object>("count", "true")
			};
			if (logType.HasValue) parameters.Add(new KeyValuePair<string, object>("action", logType.Value.GetEnumMemberValue()));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		#endregion

		#region Methods related to SEGMENTS

		/// <summary>
		/// Create a list segment
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="name">Name of the segment.</param>
		/// <param name="query">Rules for the segment.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>ID of the new segment</returns>
		/// <remarks>
		/// Here is what I have discovered about the query: 
		///		1) the entire query must be surrounded by parenthesis: (...your query...)
		///		2) field names must be surrounded with the 'special' quote: `yourfieldname`. On my US english keyboard, this 'special quote is the key directly above the 'Tab' and to the left of the '1'.
		///		3) The percent sign is the wilcard
		///		Here's an example: (`email` LIKE "a%")
		///	</remarks>
		public long CreateSegment(string userKey, long listId, string name, string query = null, long? clientId = null)
		{
			string path = "/List/CreateSublist/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("sublist_name", name)
			};
			if (query != null) parameters.Add(new KeyValuePair<string, object>("query", query));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<long>(path, parameters);
		}

		/// <summary>
		/// Retrieve a segment
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="segmentId">ID of the segment</param>
		/// <param name="includeStatistics">True if you want the statistics</param>
		/// <param name="calculateEngagement">True if you want the engagement information to be calculated</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>The <see cref="Segment">segment</see></returns>
		public Segment GetSegment(string userKey, long segmentId, bool includeStatistics = true, bool calculateEngagement = false, long? clientId = null)
		{
			var path = "/List/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("sublist_id", segmentId),
				new KeyValuePair<string, object>("no_details", includeStatistics ? "false" : "true"),	// CakeMail expects 'false' if you want to include details
				new KeyValuePair<string, object>("with_engagement", calculateEngagement ? "true" : "false")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<Segment>(path, parameters);
		}

		/// <summary>
		/// Update a segment (AKA sublist)
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="segmentId">ID of the segment</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="name">Name of the segment.</param>
		/// <param name="query">Rules for the segment.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>True if the segment was updated</returns>
		/// <remarks>A segment is sometimes referred to as a 'sub-list'</remarks>
		public bool UpdateSegment(string userKey, long segmentId, long listId, string name = null, string query = null, long? clientId = null)
		{
			string path = "/List/SetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("sublist_id", segmentId),
				new KeyValuePair<string, object>("list_id", listId)
			};
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (query != null) parameters.Add(new KeyValuePair<string, object>("query", query));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Delete a segment
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="segmentId">ID of the segment</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>True if the segment was deleted</returns>
		public bool DeleteSegment(string userKey, long segmentId, long? clientId = null)
		{
			string path = "/List/DeleteSublist/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("sublist_id", segmentId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Retrieve the segments matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="limit">Limit the number of resulting segments.</param>
		/// <param name="offset">Offset the beginning of resulting segments.</param>
		/// <param name="includeDetails">Retrieve all the stats for the segment</param>
		/// <param name="clientId">ID of the client</param>
		/// <returns>Enumeration of <see cref="Segment">segments</see> matching the filtering criteria</returns>
		public IEnumerable<Segment> GetSegments(string userKey, long listId, int? limit = 0, int? offset = 0, bool includeDetails = true, long? clientId = null)
		{
			var path = "/List/GetSublists/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("count", "false"),
				new KeyValuePair<string, object>("no_details", includeDetails ? "false" : "true")	// CakeMail expects 'false' if you want to include details
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<Segment>(path, parameters, "sublists");
		}

		/// <summary>
		/// Get a count of segments matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>The count of campaigns matching the filtering criteria</returns>
		public long GetSegmentsCount(string userKey, long listId, long? clientId = null)
		{
			var path = "/List/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		#endregion

		#region Methods related to MAILINGS

		/// <summary>
		/// Create a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the mailing.</param>
		/// <param name="campaignId">ID of the campaign you want to associate the mailing with.</param>
		/// <param name="type">Type of the mailing. Possible values: 'standard', 'recurring', 'absplit'</param>
		/// <param name="recurringId">ID of the first recurrence in case of a 'recurring' or 'absplit' mailing.</param>
		/// <param name="encoding">Encoding to be used for the mailing. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="transferEncoding">Transfer encoding to be used for the mailing. Possible values: 'quoted-printable', 'base64'</param>
		/// <param name="clientId">Client ID of the client in which the mailing is created.</param>
		/// <returns>ID of the new mailing</returns>
		public long CreateMailing(string userKey, string name, long? campaignId = null, MailingType? type = MailingType.Standard, long? recurringId = null, MessageEncoding? encoding = null, TransferEncoding? transferEncoding = null, long? clientId = null)
		{
			string path = "/Mailing/Create/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("name", name)
			};
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (type.HasValue) parameters.Add(new KeyValuePair<string, object>("type", type.Value.GetEnumMemberValue()));
			if (recurringId.HasValue) parameters.Add(new KeyValuePair<string, object>("recurring_id", recurringId.Value));
			if (encoding.HasValue) parameters.Add(new KeyValuePair<string, object>("encoding", encoding.Value.GetEnumMemberValue()));
			if (transferEncoding.HasValue) parameters.Add(new KeyValuePair<string, object>("transfer_encoding", transferEncoding.Value.GetEnumMemberValue()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<long>(path, parameters);
		}

		/// <summary>
		/// Delete a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is deleted</returns>
		public bool DeleteMailing(string userKey, long mailingId, long? clientId = null)
		{
			string path = "/Mailing/Delete/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Retrieve a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The <see cref="Mailing">mailing</see></returns>
		public Mailing GetMailing(string userKey, long mailingId, long? clientId = null)
		{
			var path = "/Mailing/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<Mailing>(path, parameters);
		}

		/// <summary>
		/// Retrieve the mailings matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the mailing status. Possible values: 'incomplete', 'scheduled', 'delivering', 'delivered'</param>
		/// <param name="type">Filter using the mailing type. Possible values: 'standard', 'recurring', 'absplit'</param>
		/// <param name="name">Filter using the mailing name.</param>
		/// <param name="listId">Filter using the ID of the mailing list.</param>
		/// <param name="campaignId">Filter using the ID of the mailing campaign.</param>
		/// <param name="recurringId">Filter using the ID of the mailing recurrence.</param>
		/// <param name="start">Filter using a start date.</param>
		/// <param name="end">Filter using a end date.</param>
		/// <param name="sortBy">Sort resulting mailings. Possible values: 'name', 'created_on', 'scheduled_for', 'scheduled_on', 'active_emails'</param>
		/// <param name="sortDirection">Direction of the sorting. Possible values: 'asc', 'desc'</param>
		/// <param name="limit">Limit the number of resulting mailings.</param>
		/// <param name="offset">Offset the beginning of resulting mailings.</param>
		/// <param name="clientId">Client ID of the client in which the mailings are located.</param>
		/// <returns>Enumeration of <see cref="Mailing">mailings</see> matching the filtering criteria</returns>
		public IEnumerable<Mailing> GetMailings(string userKey, MailingStatus? status = null, MailingType? type = null, string name = null, long? listId = null, long? campaignId = null, long? recurringId = null, DateTime? start = null, DateTime? end = null, MailingsSortBy? sortBy = null, SortDirection? sortDirection = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/Mailing/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (type.HasValue) parameters.Add(new KeyValuePair<string, object>("type", type.Value.GetEnumMemberValue()));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (listId.HasValue) parameters.Add(new KeyValuePair<string, object>("list_id", listId.Value));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (recurringId.HasValue) parameters.Add(new KeyValuePair<string, object>("recurring_id", recurringId.Value));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_date", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_date", end.Value.ToCakeMailString()));
			if (sortBy.HasValue) parameters.Add(new KeyValuePair<string, object>("sort_by", sortBy.Value.GetEnumMemberValue()));
			if (sortDirection.HasValue) parameters.Add(new KeyValuePair<string, object>("direction", sortDirection.Value.GetEnumMemberValue()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<Mailing>(path, parameters, "mailings");
		}

		/// <summary>
		/// Get a count of mailings matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the mailing status. Possible values: 'incomplete', 'scheduled', 'delivering', 'delivered'</param>
		/// <param name="type">Filter using the mailing type. Possible values: 'standard', 'recurring', 'absplit'</param>
		/// <param name="name">Filter using the mailing name.</param>
		/// <param name="listId">Filter using the ID of the mailing list.</param>
		/// <param name="campaignId">Filter using the ID of the mailing campaign.</param>
		/// <param name="recurringId">Filter using the ID of the mailing recurrence.</param>
		/// <param name="start">Filter using a start date.</param>
		/// <param name="end">Filter using a end date.</param>
		/// <param name="clientId">Client ID of the client in which the mailings are located.</param>
		/// <returns>The count of mailings matching the filtering criteria</returns>
		public long GetMailingsCount(string userKey, MailingStatus? status = null, MailingType? type = null, string name = null, long? listId = null, long? campaignId = null, long? recurringId = null, DateTime? start = null, DateTime? end = null, long? clientId = null)
		{
			var path = "/Mailing/GetList/";
			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (type.HasValue) parameters.Add(new KeyValuePair<string, object>("type", type.Value.GetEnumMemberValue()));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (listId.HasValue) parameters.Add(new KeyValuePair<string, object>("list_id", listId.Value));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (recurringId.HasValue) parameters.Add(new KeyValuePair<string, object>("recurring_id", recurringId.Value));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_date", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_date", end.Value.ToCakeMailString()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Update a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="campaignId">ID of the campaign you want to associate the mailing with</param>
		/// <param name="listId">ID of the list you want to associate the mailing with.</param>
		/// <param name="sublistId">ID of the segment you want to associate the mailing with.</param>
		/// <param name="name">Name of the mailing.</param>
		/// <param name="type">Type of the mailing. Possible values: 'standard', 'recurring', 'absplit'</param>
		/// <param name="encoding">Encoding to be used for the mailing. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="transferEncoding">Transfer encoding to be used for the mailing. Possible values: 'quoted-printable', 'base64'</param>
		/// <param name="subject">Subject of the mailing.</param>
		/// <param name="senderEmail">Email address of the sender of the mailing.</param>
		/// <param name="senderName">Name of the sender of the mailing.</param>
		/// <param name="replyTo">Email address of the reply-to of the mailing.</param>
		/// <param name="htmlContent">HTML content of the mailing.</param>
		/// <param name="textContent">Text content of the mailing.</param>
		/// <param name="trackOpens">Indicates if opens in the HTML version are tracked.</param>
		/// <param name="trackClicksInHtml">Indicates if clicks in the HTML version are tracked.</param>
		/// <param name="trackClicksInText">Indicates if clicks in the text version are tracked.</param>
		/// <param name="trackingParameters">Additional tracking parameters for links.</param>
		/// <param name="endingOn">The date to end a 'recurring' mailing.</param>
		/// <param name="maxRecurrences">The number of recurrences for a 'recurring' mailing.</param>
		/// <param name="recurringConditions">The recurring conditions for a 'recurring' mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing was updated</returns>
		public bool UpdateMailing(string userKey, long mailingId, long? campaignId = null, long? listId = null, long? sublistId = null, string name = null, MailingType? type = null, MessageEncoding? encoding = null, TransferEncoding? transferEncoding = null, string subject = null, string senderEmail = null, string senderName = null, string replyTo = null, string htmlContent = null, string textContent = null, bool? trackOpens = null, bool? trackClicksInHtml = null, bool? trackClicksInText = null, string trackingParameters = null, DateTime? endingOn = null, int? maxRecurrences = null, string recurringConditions = null, long? clientId = null)
		{
			var path = "/Mailing/SetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (listId.HasValue) parameters.Add(new KeyValuePair<string, object>("list_id", listId.Value));
			if (sublistId.HasValue) parameters.Add(new KeyValuePair<string, object>("sublist_id", sublistId.Value));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (type.HasValue) parameters.Add(new KeyValuePair<string, object>("type", type.Value.GetEnumMemberValue()));
			if (encoding.HasValue) parameters.Add(new KeyValuePair<string, object>("encoding", encoding.Value.GetEnumMemberValue()));
			if (transferEncoding.HasValue) parameters.Add(new KeyValuePair<string, object>("transfer_encoding", transferEncoding.Value.GetEnumMemberValue()));
			if (subject != null) parameters.Add(new KeyValuePair<string, object>("subject", subject));
			if (senderEmail != null) parameters.Add(new KeyValuePair<string, object>("sender_email", senderEmail));
			if (senderName != null) parameters.Add(new KeyValuePair<string, object>("sender_name", senderName));
			if (replyTo != null) parameters.Add(new KeyValuePair<string, object>("reply_to", replyTo));
			if (htmlContent != null) parameters.Add(new KeyValuePair<string, object>("html_message", htmlContent));
			if (textContent != null) parameters.Add(new KeyValuePair<string, object>("text_message", textContent));
			if (trackOpens.HasValue) parameters.Add(new KeyValuePair<string, object>("opening_stats", trackOpens.Value ? "true" : "false"));
			if (trackClicksInHtml.HasValue) parameters.Add(new KeyValuePair<string, object>("clickthru_html", trackClicksInHtml.Value ? "true" : "false"));
			if (trackClicksInText.HasValue) parameters.Add(new KeyValuePair<string, object>("clickthru_text", trackClicksInText.Value ? "true" : "false"));
			if (trackingParameters != null) parameters.Add(new KeyValuePair<string, object>("tracking_params", trackingParameters));
			if (endingOn.HasValue) parameters.Add(new KeyValuePair<string, object>("ending_on", endingOn.Value.ToCakeMailString()));
			if (maxRecurrences.HasValue) parameters.Add(new KeyValuePair<string, object>("max_recurrences", maxRecurrences.Value));
			if (recurringConditions != null) parameters.Add(new KeyValuePair<string, object>("recurring_conditions", recurringConditions));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Send a test of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="recipientEmail">Email address where the test will be sent.</param>
		/// <param name="separated">True if you want the HTML and the text to be sent seperatly, false if you want to combine the HTML and the text in a multi-part email.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the test email was sent</returns>
		public bool SendMailingTestEmail(string userKey, long mailingId, string recipientEmail, bool separated = false, long? clientId = null)
		{
			var path = "/Mailing/SendTestEmail/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
				new KeyValuePair<string, object>("test_email", recipientEmail),
				new KeyValuePair<string, object>("test_type", separated ? "separated" : "merged")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Get the multi-part version of a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The <see cref="RawEmailMessage">multi-part message</see></returns>
		public RawEmailMessage GetMailingRawEmailMessage(string userKey, long mailingId, long? clientId = null)
		{
			var path = "/Mailing/GetEmailMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<RawEmailMessage>(path, parameters);
		}

		/// <summary>
		/// Get the rendered HTML version of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The rendered HTML</returns>
		public string GetMailingRawHtml(string userKey, long mailingId, long? clientId = null)
		{
			var path = "/Mailing/GetHtmlMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<string>(path, parameters);
		}

		/// <summary>
		/// Get the rendered text version of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The rendered text</returns>
		public string GetMailingRawText(string userKey, long mailingId, long? clientId = null)
		{
			var path = "/Mailing/GetTextMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<string>(path, parameters);
		}

		/// <summary>
		/// Schedule a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="date">Date when the mailing is scheduled. If not provided, the mailing will be sent right away.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is scheduled</returns>
		public bool ScheduleMailing(string userKey, long mailingId, DateTime? date = null, long? clientId = null)
		{
			var path = "/Mailing/Schedule/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (date.HasValue) parameters.Add(new KeyValuePair<string, object>("date", date.Value.ToCakeMailString()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Unschedule a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is unscheduled</returns>
		public bool UnscheduleMailing(string userKey, long mailingId, long? clientId = null)
		{
			var path = "/Mailing/Unschedule/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Suspend a delivering mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is suspended</returns>
		public bool SuspendMailing(string userKey, long mailingId, long? clientId = null)
		{
			var path = "/Mailing/Suspend/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Resume a suspended mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is resumed</returns>
		public bool ResumeMailing(string userKey, long mailingId, long? clientId = null)
		{
			var path = "/Mailing/Resume/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Retrieve the log items for a given mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="logType">Filter using the log action. Possible values: "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped"</param>
		/// <param name="listMemberId">Filter using the ID of the member.</param>
		/// <param name="uniques">Return unique log item per member.</param>
		/// <param name="totals">Return all the log items.</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="LogItem">log items</see> matching the filter criteria</returns>
		public IEnumerable<LogItem> GetMailingLogs(string userKey, long mailingId, LogType? logType = null, long? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			string path = "/Mailing/GetLog/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
				new KeyValuePair<string, object>("totals", totals ? "true" : "false"),
				new KeyValuePair<string, object>("uniques", uniques ? "true" : "false"),
				new KeyValuePair<string, object>("count", "false")
			};
			if (logType.HasValue) parameters.Add(new KeyValuePair<string, object>("action", logType.Value.GetEnumMemberValue()));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (listMemberId.HasValue) parameters.Add(new KeyValuePair<string, object>("record_id", listMemberId.Value));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<LogItem>(path, parameters, "logs");
		}

		/// <summary>
		/// Get a count of log items matching the filter criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="logType">Filter using the log action. Possible values: "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped"</param>
		/// <param name="listMemberId">Filter using the ID of the member.</param>
		/// <param name="uniques">Return unique log item per member.</param>
		/// <param name="totals">Return all the log items.</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The number of log items matching the filtering criteria</returns>
		public long GetMailingLogsCount(string userKey, long mailingId, LogType? logType = null, long? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, long? clientId = null)
		{
			string path = "/Mailing/GetLog/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
				new KeyValuePair<string, object>("totals", totals ? "true" : "false"),
				new KeyValuePair<string, object>("uniques", uniques ? "true" : "false"),
				new KeyValuePair<string, object>("count", "true")
			};
			if (logType.HasValue) parameters.Add(new KeyValuePair<string, object>("action", logType.Value.GetEnumMemberValue()));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (listMemberId.HasValue) parameters.Add(new KeyValuePair<string, object>("record_id", listMemberId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Retrieve the links for a given mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="Link">links</see> matching the filter criteria</returns>
		public IEnumerable<Link> GetMailingLinks(string userKey, long mailingId, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			string path = "/Mailing/GetLinks/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<Link>(path, parameters, "links");
		}

		/// <summary>
		/// Get a count of links matching the filter criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The number of links matching the filtering criteria</returns>
		public long GetMailingLinksCount(string userKey, long mailingId, long? clientId = null)
		{
			string path = "/Mailing/GetLinks/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Retrieve a link
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="linkId">ID of the link.</param>
		/// <param name="clientId">Client ID of the client in which the link is located.</param>
		/// <returns>The <see cref="Link">link</see></returns>
		public Link GetMailingLink(string userKey, long linkId, long? clientId = null)
		{
			string path = "/Mailing/GetLinkInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("link_id", linkId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<Link>(path, parameters);
		}

		public IEnumerable<LogItem> GetMailingLinksLogs(string userKey, long mailingId, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			string path = "/Mailing/GetLinksLog/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<LogItem>(path, parameters, "logs");
		}

		#endregion

		#region Methods related to RELAYS

		/// <summary>
		/// Send a one-off email without tracking opens and clicks
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="recipientEmailAddress">The email address of the recipient.</param>
		/// <param name="subject">Subject of the relay</param>
		/// <param name="html">HTML content of the relay.</param>
		/// <param name="text">Text content of the relay.</param>
		/// <param name="senderEmail">Email address of the sender of the relay.</param>
		/// <param name="senderName">Name of the sender of the relay.</param>
		/// <param name="encoding">Encoding to be used for the relay. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="clientId">Client ID of the client in which the relay is located.</param>
		/// <returns>True if the email is sent</returns>
		public bool SendRelay(string userKey, string recipientEmailAddress, string subject, string html, string text, string senderEmail, string senderName = null, MessageEncoding? encoding = null, long? clientId = null)
		{
			string path = "/Relay/Send/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("email", recipientEmailAddress),
				new KeyValuePair<string, object>("subject", subject),
				new KeyValuePair<string, object>("html_message", html),
				new KeyValuePair<string, object>("text_message", text),
				new KeyValuePair<string, object>("sender_email", senderEmail),
				new KeyValuePair<string, object>("track_opening", "false"),
				new KeyValuePair<string, object>("track_clicks_in_html", "false"),
				new KeyValuePair<string, object>("track_clicks_in_text", "false")
			};
			if (senderName != null) parameters.Add(new KeyValuePair<string, object>("sender_name", senderName));
			if (encoding.HasValue) parameters.Add(new KeyValuePair<string, object>("encoding", encoding.Value.GetEnumMemberValue()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Send a one-off email. Track opens and clicks.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="trackingId">ID for tracking purposes.</param>
		/// <param name="recipientEmailAddress">The email address of the recipient.</param>
		/// <param name="subject">Subject of the relay</param>
		/// <param name="html">HTML content of the relay.</param>
		/// <param name="text">Text content of the relay.</param>
		/// <param name="senderEmail">Email address of the sender of the relay.</param>
		/// <param name="senderName">Name of the sender of the relay.</param>
		/// <param name="encoding">Encoding to be used for the relay. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="clientId">Client ID of the client in which the relay is located.</param>
		/// <returns>True if the email is sent</returns>
		public bool SendTrackedRelay(string userKey, long trackingId, string recipientEmailAddress, string subject, string html, string text, string senderEmail, string senderName = null, MessageEncoding? encoding = null, long? clientId = null)
		{
			string path = "/Relay/Send/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("tracking_id", trackingId),
				new KeyValuePair<string, object>("email", recipientEmailAddress),
				new KeyValuePair<string, object>("subject", subject),
				new KeyValuePair<string, object>("html_message", html),
				new KeyValuePair<string, object>("text_message", text),
				new KeyValuePair<string, object>("sender_email", senderEmail),
				new KeyValuePair<string, object>("track_opening", "true"),
				new KeyValuePair<string, object>("track_clicks_in_html", "true"),
				new KeyValuePair<string, object>("track_clicks_in_text", "true")
			};
			if (senderName != null) parameters.Add(new KeyValuePair<string, object>("sender_name", senderName));
			if (encoding.HasValue) parameters.Add(new KeyValuePair<string, object>("encoding", encoding.Value.GetEnumMemberValue()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Retrieve the log items for either a given tracked relay or for a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="trackingId">ID of the tracked relay. If this value is omitted, all log items for the client will be returned</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="RelayLog">log items</see> matching the filter criteria</returns>
		public IEnumerable<RelayLog> GetRelaySentLogs(string userKey, long? trackingId = null, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			return GetRelayLogs<RelayLog>(userKey, "sent", "sent_logs", trackingId, start, end, limit, offset, clientId);
		}

		/// <summary>
		/// Retrieve the log items for either a given tracked relay or for a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="trackingId">ID of the tracked relay. If this value is omitted, all log items for the client will be returned</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="RelayOpenLog">log items</see> matching the filter criteria</returns>
		public IEnumerable<RelayOpenLog> GetRelayOpenLogs(string userKey, long? trackingId = null, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			return GetRelayLogs<RelayOpenLog>(userKey, "open", "open_logs", trackingId, start, end, limit, offset, clientId);
		}

		/// <summary>
		/// Retrieve the log items for either a given tracked relay or for a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="trackingId">ID of the tracked relay. If this value is omitted, all log items for the client will be returned</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="RelayClickLog">log items</see> matching the filter criteria</returns>
		public IEnumerable<RelayClickLog> GetRelayClickLogs(string userKey, long? trackingId = null, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			return GetRelayLogs<RelayClickLog>(userKey, "clickthru", "clickthru_logs", trackingId, start, end, limit, offset, clientId);
		}

		/// <summary>
		/// Retrieve the log items for either a given tracked relay or for a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="trackingId">ID of the tracked relay. If this value is omitted, all log items for the client will be returned</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="RelayBounceLog">log items</see> matching the filter criteria</returns>
		public IEnumerable<RelayBounceLog> GetRelayBounceLogs(string userKey, long? trackingId = null, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			return GetRelayLogs<RelayBounceLog>(userKey, "bounce", "bounce_logs", trackingId, start, end, limit, offset, clientId);
		}

		private IEnumerable<T> GetRelayLogs<T>(string userKey, string logType, string arrayPropertyName, long? trackingId = null, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null) where T : RelayLog, new()
		{
			string path = "/Relay/GetLogs/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("log_type", logType)
			};
			if (trackingId.HasValue) parameters.Add(new KeyValuePair<string, object>("tracking_id", trackingId.Value));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<T>(path, parameters, arrayPropertyName);
		}

		#endregion

		#region Methods related to SUPPRESSION LISTS

		/// <summary>
		/// Add email addresses to the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="emailAddresses">The email addresses to add to the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>An enumeration of <see cref="SuppressEmailResult">results</see>. Each item in this enumeration indicates the result of adding an email address to the suppression list.</returns>
		public IEnumerable<SuppressEmailResult> AddEmailAddressesToSuppressionList(string userKey, IEnumerable<string> emailAddresses, long? clientId = null)
		{
			string path = "/SuppressionList/ImportEmails/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (emailAddresses != null)
			{
				foreach (var item in emailAddresses.Select((email, i) => new { Index = i, Email = email }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("email[{0}]", item.Index), item.Email));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<SuppressEmailResult>(path, parameters, null);
		}

		/// <summary>
		/// Add domains to the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="domains">The domains to add to the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>An enumeration of <see cref="SuppressDomainResult">results</see>. Each item in this enumeration indicates the result of adding a domain to the suppression list.</returns>
		public IEnumerable<SuppressDomainResult> AddDomainsToSuppressionList(string userKey, IEnumerable<string> domains, long? clientId = null)
		{
			string path = "/SuppressionList/ImportDomains/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (domains != null)
			{
				foreach (var item in domains.Select((domain, i) => new { Index = i, Domain = domain }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("domain[{0}]", item.Index), item.Domain));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<SuppressDomainResult>(path, parameters, null);
		}

		/// <summary>
		/// Add localparts to the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="localParts">The localparts to add to the suppression list</param>
		/// <param name="clientId">Client ID of the client.</param>
		/// <returns>An enumeration of <see cref="SuppressLocalPartResult">results</see>. Each item in this enumeration indicates the result of adding a localpart to the suppression list.</returns>
		public IEnumerable<SuppressLocalPartResult> AddLocalPartsToSuppressionList(string userKey, IEnumerable<string> localParts, long? clientId = null)
		{
			string path = "/SuppressionList/ImportLocalparts/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (localParts != null)
			{
				foreach (var item in localParts.Select((localPart, i) => new { Index = i, LocalPart = localPart }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("localpart[{0}]", item.Index), item.LocalPart));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<SuppressLocalPartResult>(path, parameters, "localparts");
		}

		/// <summary>
		/// Remove email addresses from the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="emailAddresses">The email addresses to remove from the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>An enumeration of <see cref="SuppressEmailResult">results</see>. Each item in this enumeration indicates the result of removing an email address from the suppression list.</returns>
		public IEnumerable<SuppressEmailResult> RemoveEmailAddressesFromSuppressionList(string userKey, IEnumerable<string> emailAddresses, long? clientId = null)
		{
			string path = "/SuppressionList/DeleteEmails/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (emailAddresses != null)
			{
				foreach (var item in emailAddresses.Select((email, i) => new { Index = i, Email = email }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("email[{0}]", item.Index), item.Email));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<SuppressEmailResult>(path, parameters, null);
		}

		/// <summary>
		/// Remove domains from the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="domains">The domains to remove from the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>An enumeration of <see cref="SuppressDomainResult">results</see>. Each item in this enumeration indicates the result of removing a domain from the suppression list.</returns>
		public IEnumerable<SuppressDomainResult> RemoveDomainsFromSuppressionList(string userKey, IEnumerable<string> domains, long? clientId = null)
		{
			string path = "/SuppressionList/DeleteDomains/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (domains != null)
			{
				foreach (var item in domains.Select((domain, i) => new { Index = i, Domain = domain }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("domain[{0}]", item.Index), item.Domain));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<SuppressDomainResult>(path, parameters, null);
		}

		/// <summary>
		/// Remove localparts from the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="localParts">The localparts to remove from the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>An enumeration of <see cref="SuppressLocalPartResult">results</see>. Each item in this enumeration indicates the result of removing a localpart from the suppression list.</returns>
		public IEnumerable<SuppressLocalPartResult> RemoveLocalPartsFromSuppressionList(string userKey, IEnumerable<string> localParts, long? clientId = null)
		{
			string path = "/SuppressionList/DeleteLocalparts/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (localParts != null)
			{
				foreach (var item in localParts.Select((localPart, i) => new { Index = i, LocalPart = localPart }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("localpart[{0}]", item.Index), item.LocalPart));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<SuppressLocalPartResult>(path, parameters, "localparts");
		}

		/// <summary>
		/// Retrieve the email addresses on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="limit">Limit the number of resulting email addresses.</param>
		/// <param name="offset">Offset the beginning of resulting email addresses.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>An enumeration of <see cref="SuppressedEmail">addresses</see>. The result also indicates how each email address ended up on the suppression list.</returns>
		public IEnumerable<SuppressedEmail> GetSuppressedEmailAddresses(string userKey, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/SuppressionList/ExportEmails/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<SuppressedEmail>(path, parameters, "emails");
		}

		/// <summary>
		/// Retrieve the domains on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="limit">Limit the number of resulting domains.</param>
		/// <param name="offset">Offset the beginning of resulting domains.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>An enumeration of domains.</returns>
		public IEnumerable<string> GetSuppressedDomains(string userKey, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/SuppressionList/ExportDomains/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			var result = ExecuteArrayRequest<ExpandoObject>(path, parameters, "domains");

			var domains = (from r in result select r.Single(p => p.Key == "domain").Value.ToString()).ToArray();
			return domains;
		}

		/// <summary>
		/// Retrieve the localparts on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="limit">Limit the number of resulting localparts.</param>
		/// <param name="offset">Offset the beginning of resulting localparts.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>An enumeration of localparts.</returns>
		public IEnumerable<string> GetSuppressedLocalParts(string userKey, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/SuppressionList/ExportLocalparts/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			var result = ExecuteArrayRequest<ExpandoObject>(path, parameters, "localparts");

			var localParts = (from r in result select r.Single(p => p.Key == "localpart").Value.ToString()).ToArray();
			return localParts;
		}

		/// <summary>
		/// Get a count of email addresses on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The number of email addresses on the suppresssion list</returns>
		public long GetSuppressedEmailAddressesCount(string userKey, long? clientId = null)
		{
			var path = "/SuppressionList/ExportEmails/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Get a count of domains on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The number of domains on the suppresssion list</returns>
		public long GetSuppressedDomainsCount(string userKey, long? clientId = null)
		{
			var path = "/SuppressionList/ExportDomains/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Get a count of localparts on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The number of localparts on the suppresssion list</returns>
		public long GetSuppressedLocalPartsCount(string userKey, long? clientId = null)
		{
			var path = "/SuppressionList/ExportLocalparts/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		#endregion

		#region Methods related to TIMEZONES

		/// <summary>
		/// Retrieve the list of all timezones known to the CakeMail system
		/// </summary>
		/// <returns>An enumeration of all <see cref="Timezone">timezones</see>.</returns>
		public IEnumerable<Timezone> GetTimezones()
		{
			var path = "/Client/GetTimezones/";

			return ExecuteArrayRequest<Timezone>(path, null, "timezones");
		}

		#endregion

		#region Methods related to TRIGGERS

		/// <summary>
		/// Create a trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the trigger.</param>
		/// <param name="listId">ID of the list you want to associate the trigger with.</param>
		/// <param name="campaignId">ID of the campaign you want to associate the trigger with.</param>
		/// <param name="encoding">Encoding to be used for the trigger. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="transferEncoding">Transfer encoding to be used for the trigger. Possible values: 'quoted-printable', 'base64'</param>
		/// <param name="clientId">Client ID of the client in which the mailing is created.</param>
		/// <returns>ID of the new trigger</returns>
		public long CreateTrigger(string userKey, string name, long listId, long? campaignId = null, MessageEncoding? encoding = null, TransferEncoding? transferEncoding = null, long? clientId = null)
		{
			string path = "/Trigger/Create/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("name", name),
				new KeyValuePair<string, object>("list_id", listId)
			};
			if (encoding.HasValue) parameters.Add(new KeyValuePair<string, object>("encoding", encoding.Value.GetEnumMemberValue()));
			if (transferEncoding.HasValue) parameters.Add(new KeyValuePair<string, object>("transfer_encoding", transferEncoding.Value.GetEnumMemberValue()));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<long>(path, parameters);
		}

		//public bool DeleteTrigger(string userKey, long triggerId, long? clientId = null)
		//{
		//	string path = "/Trigger/Delete/";

		//	var parameters = new List<KeyValuePair<string, object>>()
		//	{
		//		new KeyValuePair<string, object>("user_key", userKey),
		//		new KeyValuePair<string, object>("trigger_id", triggerId),
		//	};
		//	if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

		//	return ExecuteRequest<bool>(path, parameters);
		//}

		/// <summary>
		/// Retrieve a trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <returns>The <see cref="Trigger">trigger</see></returns>
		public Trigger GetTrigger(string userKey, long triggerId, long? clientId = null)
		{
			var path = "/Trigger/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<Trigger>(path, parameters);
		}

		/// <summary>
		/// Update a trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="campaignId">ID of the campaign you want to associate the trigger with.</param>
		/// <param name="name">Name of the trigger</param>
		/// <param name="action">Action of the trigger. Possible values: 'opt-in', 'douopt-in', 'opt-out', 'specific', 'annual'</param>
		/// <param name="encoding">Encoding to be used for the trigger. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="transferEncoding">Transfer encoding to be used for the trigger. Possible values: 'quoted-printable', 'base64'</param>
		/// <param name="subject">Subject of the trigger.</param>
		/// <param name="senderEmail">Email address of the sender of the trigger.</param>
		/// <param name="senderName">Name of the sender of the trigger.</param>
		/// <param name="replyTo">Email address of the reply-to of the trigger.</param>
		/// <param name="htmlContent">HTML content of the trigger.</param>
		/// <param name="textContent">Text content of the trigger.</param>
		/// <param name="trackOpens">Track the opens in the HTML version.</param>
		/// <param name="trackClicksInHtml">Track the clicks in the HTML version.</param>
		/// <param name="trackClicksInText">Track the clicks in the text version.</param>
		/// <param name="trackingParameters">Additional tracking parameters for links.</param>
		/// <param name="delay">Delay (in seconds) to be used when the trigger is unleashed.</param>
		/// <param name="status">Status of the trigger. Possible values: 'active', 'inactive'</param>
		/// <param name="date">DateTime to be used for trigger with action 'specific' or 'annual'.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <returns>True if the trigger was updated</returns>
		public bool UpdateTrigger(string userKey, long triggerId, long? campaignId = null, string name = null, TriggerAction? action = null, MessageEncoding? encoding = null, TransferEncoding? transferEncoding = null, string subject = null, string senderEmail = null, string senderName = null, string replyTo = null, string htmlContent = null, string textContent = null, bool? trackOpens = null, bool? trackClicksInHtml = null, bool? trackClicksInText = null, string trackingParameters = null, int? delay = null, TriggerStatus? status = null, DateTime? date = null, long? clientId = null)
		{
			var path = "/Trigger/SetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (action.HasValue) parameters.Add(new KeyValuePair<string, object>("action", action.Value.GetEnumMemberValue()));
			if (encoding.HasValue) parameters.Add(new KeyValuePair<string, object>("encoding", encoding.Value.GetEnumMemberValue()));
			if (transferEncoding.HasValue) parameters.Add(new KeyValuePair<string, object>("transfer_encoding", transferEncoding.Value.GetEnumMemberValue()));
			if (subject != null) parameters.Add(new KeyValuePair<string, object>("subject", subject));
			if (senderEmail != null) parameters.Add(new KeyValuePair<string, object>("sender_email", senderEmail));
			if (senderName != null) parameters.Add(new KeyValuePair<string, object>("sender_name", senderName));
			if (replyTo != null) parameters.Add(new KeyValuePair<string, object>("reply_to", replyTo));
			if (htmlContent != null) parameters.Add(new KeyValuePair<string, object>("html_message", htmlContent));
			if (textContent != null) parameters.Add(new KeyValuePair<string, object>("text_message", textContent));
			if (trackOpens.HasValue) parameters.Add(new KeyValuePair<string, object>("opening_stats", trackOpens.Value ? "true" : "false"));
			if (trackClicksInHtml.HasValue) parameters.Add(new KeyValuePair<string, object>("clickthru_html", trackClicksInHtml.Value ? "true" : "false"));
			if (trackClicksInText.HasValue) parameters.Add(new KeyValuePair<string, object>("clickthru_text", trackClicksInText.Value ? "true" : "false"));
			if (trackingParameters != null) parameters.Add(new KeyValuePair<string, object>("tracking_params", trackingParameters));
			if (delay != null) parameters.Add(new KeyValuePair<string, object>("delay", delay));
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (date.HasValue) parameters.Add(new KeyValuePair<string, object>("date_field", date.Value.ToCakeMailString()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Retrieve the triggers matching the filtering criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the trigger status. Possible values: 'active', 'inactive'</param>
		/// <param name="action">Filter using the trigger action. Possible values: 'opt-in', 'douopt-in', 'opt-out', 'specific', 'annual'</param>
		/// <param name="listId">Filter using the ID of the trigger list.</param>
		/// <param name="campaignId">Filter using the ID of the trigger campaign.</param>
		/// <param name="limit">Limit the number of resulting triggers.</param>
		/// <param name="offset">Offset the beginning of resulting triggers.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <returns>An enumeration of <see cref="Trigger">triggers</see> matching the filter criteria</returns>
		public IEnumerable<Trigger> GetTriggers(string userKey, TriggerStatus? status = null, TriggerAction? action = null, long? listId = null, long? campaignId = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/Trigger/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (action.HasValue) parameters.Add(new KeyValuePair<string, object>("action", action.Value.GetEnumMemberValue()));
			if (listId.HasValue) parameters.Add(new KeyValuePair<string, object>("list_id", listId.Value));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<Trigger>(path, parameters, "triggers");
		}

		/// <summary>
		/// Get a count of triggers matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the trigger status. Possible values: 'active', 'inactive'</param>
		/// <param name="action">Filter using the trigger action. Possible values: 'opt-in', 'douopt-in', 'opt-out', 'specific', 'annual'</param>
		/// <param name="listId">Filter using the ID of the trigger list.</param>
		/// <param name="campaignId">Filter using the ID of the trigger campaign.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <returns>The count of triggers matching the filtering criteria</returns>
		public long GetTriggersCount(string userKey, TriggerStatus? status = null, TriggerAction? action = null, long? listId = null, long? campaignId = null, long? clientId = null)
		{
			var path = "/Trigger/GetList/";
			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (action.HasValue) parameters.Add(new KeyValuePair<string, object>("action", action.Value.GetEnumMemberValue()));
			if (listId.HasValue) parameters.Add(new KeyValuePair<string, object>("list_id", listId.Value));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Send a test of a trigger.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="recipientEmail">Email address where the test will be sent.</param>
		/// <param name="separated">True if you want the HTML and the text to be sent seperatly, false if you want to combine the HTML and the text in a multi-part email.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <returns>True if the test email was sent</returns>
		public bool SendTriggerTestEmail(string userKey, long triggerId, string recipientEmail, bool separated = false, long? clientId = null)
		{
			var path = "/Trigger/SendTestEmail/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("test_email", recipientEmail),
				new KeyValuePair<string, object>("test_type", separated ? "separated" : "merged")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Get the multi-part version of a trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <returns>The <see cref="RawEmailMessage">multi-part message</see></returns>
		public RawEmailMessage GetTriggerRawEmailMessage(string userKey, long triggerId, long? clientId = null)
		{
			var path = "/Trigger/GetEmailMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<RawEmailMessage>(path, parameters);
		}

		/// <summary>
		/// Get the rendered HTML version of a trigger.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <returns>The rendered HTML</returns>
		public string GetTriggerRawHtml(string userKey, long triggerId, long? clientId = null)
		{
			var path = "/Trigger/GetHtmlMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<string>(path, parameters);
		}

		/// <summary>
		/// Get the rendered text version of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <returns>The rendered text</returns>
		public string GetTriggerRawText(string userKey, long triggerId, long? clientId = null)
		{
			var path = "/Trigger/GetTextMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<string>(path, parameters);
		}

		/// <summary>
		/// Unleash a trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="listMemberId">ID of the member to unleash the trigger to.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <returns>True is the trigger is unleashed</returns>
		public bool UnleashTrigger(string userKey, long triggerId, long listMemberId, long? clientId = null)
		{
			var path = "/Trigger/Unleash/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("record_id", listMemberId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Retrieve the log items for a given trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger.</param>
		/// <param name="logType">Filter using the log action.</param>
		/// <param name="listMemberId">Filter using the ID of the member.</param>
		/// <param name="uniques">Return unique log item per member.</param>
		/// <param name="totals">Return all the log items.</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="LogItem">log items</see> matching the filter criteria</returns>
		public IEnumerable<LogItem> GetTriggerLogs(string userKey, long triggerId, LogType? logType = null, long? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			string path = "/Trigger/GetLog/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("totals", totals ? "true" : "false"),
				new KeyValuePair<string, object>("uniques", uniques ? "true" : "false"),
				new KeyValuePair<string, object>("count", "false")
			};
			if (logType.HasValue) parameters.Add(new KeyValuePair<string, object>("action", logType.Value.GetEnumMemberValue()));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (listMemberId.HasValue) parameters.Add(new KeyValuePair<string, object>("record_id", listMemberId.Value));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<LogItem>(path, parameters, "logs");
		}

		/// <summary>
		/// Get a count of log items matching the filter criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger.</param>
		/// <param name="logType">Filter using the log action.</param>
		/// <param name="listMemberId">Filter using the ID of the member.</param>
		/// <param name="uniques">Return unique log item per member.</param>
		/// <param name="totals">Return all the log items.</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <returns>The number of log items matching the filtering criteria</returns>
		public long GetTriggerLogsCount(string userKey, long triggerId, LogType? logType = null, long? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, long? clientId = null)
		{
			string path = "/Trigger/GetLog/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("totals", totals ? "true" : "false"),
				new KeyValuePair<string, object>("uniques", uniques ? "true" : "false"),
				new KeyValuePair<string, object>("count", "true")
			};
			if (logType.HasValue) parameters.Add(new KeyValuePair<string, object>("action", logType.Value.GetEnumMemberValue()));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (listMemberId.HasValue) parameters.Add(new KeyValuePair<string, object>("record_id", listMemberId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Retrieve the links for a given trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger.</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <returns>An enumeration of <see cref="Link">links</see> matching the filter criteria</returns>
		public IEnumerable<Link> GetTriggerLinks(string userKey, long triggerId, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			string path = "/Trigger/GetLinks/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<Link>(path, parameters, "links");
		}

		/// <summary>
		/// Get a count of links matching the filter criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <returns>The number of links matching the filtering criteria</returns>
		public long GetTriggerLinksCount(string userKey, long triggerId, long? clientId = null)
		{
			string path = "/Trigger/GetLinks/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Retrieve a link
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="linkId">ID of the link.</param>
		/// <param name="clientId">Client ID of the client in which the link is located.</param>
		/// <returns>The <see cref="Link">link</see></returns>
		public Link GetTriggerLink(string userKey, long linkId, long? clientId = null)
		{
			string path = "/Trigger/GetLinkInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("link_id", linkId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<Link>(path, parameters);
		}

		public IEnumerable<LogItem> GetTriggerLinksLogs(string userKey, long triggerId, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			string path = "/Trigger/GetLog/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<LogItem>(path, parameters, "logs");
		}

		#endregion

		#region Methods related to TEMPLATES

		/// <summary>
		/// Create a template category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="labels">Name of the category.</param>
		/// <param name="isVisibleByDefault">Is the category visible by default.</param>
		/// <param name="templatesCanBeCopied">Are the templates in the category copyable.</param>
		/// <param name="clientId">Client ID of the client in which the category is created.</param>
		/// <returns>ID of the new template category</returns>
		public long CreateTemplateCategory(string userKey, IDictionary<string, string> labels, bool isVisibleByDefault = true, bool templatesCanBeCopied = true, long? clientId = null)
		{
			string path = "/TemplateV2/CreateCategory/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("default", isVisibleByDefault ? "1" : "0"),
				new KeyValuePair<string, object>("templates_copyable", templatesCanBeCopied ? "1" : "0")
			};
			if (labels != null)
			{
				foreach (var item in labels.Select((label, i) => new { Index = i, Language = label.Key, Name = label.Value }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("label[{0}][language]", item.Index), item.Language));
					parameters.Add(new KeyValuePair<string, object>(string.Format("label[{0}][name]", item.Index), item.Name));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			// The data returned when creating a new category is a little bit unusual
			// Instead of simply returning the unique identifier of the new record like all the other 'Create' methods, for example: {"status":"success","data":"4593766"}
			// this method return an object with a single property called 'id' containing the unique identifier of the new record, like this: {"status":"success","data":{"id":"14052"}}
			return ExecuteRequest<long>(path, parameters, "id");
		}

		/// <summary>
		/// Delete a template category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the template category</param>
		/// <param name="clientId">Client ID of the client in which the template category is located.</param>
		/// <returns>True if the template category is deleted</returns>
		public bool DeleteTemplateCategory(string userKey, long categoryId, long? clientId = null)
		{
			string path = "/TemplateV2/DeleteCategory/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			// The data returned when deleting a category is a little bit unusual
			// Instead of returning a boolean value to indicate success, it returns an empty array!!!
			// For example:  {"status":"success","data":[]}
			ExecuteRequest(path, parameters);
			return true;
		}

		/// <summary>
		/// Retrieve a template category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientId">Client ID of the client in which the category is located.</param>
		/// <returns>The <see cref="TemplateCategory">category</see></returns>
		public TemplateCategory GetTemplateCategory(string userKey, long categoryId, long? clientId = null)
		{
			var path = "/TemplateV2/GetCategory/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<TemplateCategory>(path, parameters);
		}

		/// <summary>
		/// Retrieve the template categories matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="limit">Limit the number of resulting categories.</param>
		/// <param name="offset">Offset the beginning of resulting categories.</param>
		/// <param name="clientId">Client ID of the client in which the categories are located.</param>
		/// <returns>Enumeration of <see cref="TemplateCategory">categories</see> matching the filtering criteria</returns>
		public IEnumerable<TemplateCategory> GetTemplateCategories(string userKey, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/TemplateV2/GetCategories/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<TemplateCategory>(path, parameters, "categories");
		}

		/// <summary>
		/// Get a count of template categories matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">Client ID of the client in which the categories are located.</param>
		/// <returns>The count of categories matching the filtering criteria</returns>
		public long GetTemplateCategoriesCount(string userKey, long? clientId = null)
		{
			var path = "/TemplateV2/GetCategories/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Update a template category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="labels">Name of the category.</param>
		/// <param name="isVisibleByDefault">Is the category visible by default.</param>
		/// <param name="templatesCanBeCopied">Are the templates in the category copyable.</param>
		/// <param name="clientId">Client ID of the client in which the category is located.</param>
		/// <returns>True if the category was updated</returns>
		public bool UpdateTemplateCategory(string userKey, long categoryId, IDictionary<string, string> labels, bool isVisibleByDefault = true, bool templatesCanBeCopied = true, long? clientId = null)
		{
			string path = "/TemplateV2/SetCategory/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
				new KeyValuePair<string, object>("default", isVisibleByDefault ? "1" : "0"),
				new KeyValuePair<string, object>("templates_copyable", templatesCanBeCopied ? "1" : "0"),
			};
			if (labels != null)
			{
				foreach (var label in labels)
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("label[{0}]", label.Key), label.Value));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Retrieve the list of permissions for a category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="limit">Limit the number of resulting permissions.</param>
		/// <param name="offset">Offset the beginning of resulting permissions.</param>
		/// <param name="clientId">ID of the client in which the category is located.</param>
		/// <returns>An enumeration of permissions</returns>
		public IEnumerable<TemplateCategoryVisibility> GetTemplateCategoryVisibility(string userKey, long categoryId, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/TemplateV2/GetCategoryVisibility/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<TemplateCategoryVisibility>(path, parameters, "clients");
		}

		/// <summary>
		/// Get a count of permissions for a given template category.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientId">ID of the client</param>
		/// <returns>The count of permissions matching the filtering criteria</returns>
		public long GetTemplateCategoryVisibilityCount(string userKey, long categoryId, long? clientId = null)
		{
			var path = "/TemplateV2/GetCategoryVisibility/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Set the permissions for a category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientVisibility">The list of clients and their associated boolean that indicates if they have access to the category</param>
		/// <param name="clientId">ID of the client in which the category is located.</param>
		/// <returns>True if the permissions are successfully updated</returns>
		public bool SetTemplateCategoryVisibility(string userKey, long categoryId, IDictionary<long, bool> clientVisibility, long? clientId = null)
		{
			var path = "/TemplateV2/SetCategoryVisibility/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
			};
			if (clientVisibility != null)
			{
				foreach (var item in clientVisibility.Select((visibility, i) => new { Index = i, ClientId = visibility.Key, Visible = visibility.Value }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("client[{0}][client_id]", item.Index), item.ClientId));
					parameters.Add(new KeyValuePair<string, object>(string.Format("client[{0}][visible]", item.Index), item.Visible ? "true" : "false"));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Create a template
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="labels">Name of the template.</param>
		/// <param name="content">Content of the template.</param>
		/// <param name="categoryId">ID of the category.</param>
		/// <param name="clientId">Client ID of the client in which the template is created.</param>
		/// <returns>ID of the new template</returns>
		public long CreateTemplate(string userKey, IDictionary<string, string> labels, string content, long categoryId, long? clientId = null)
		{
			string path = "/TemplateV2/CreateTemplate/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("content", content),
				new KeyValuePair<string, object>("category_id", categoryId)
			};
			if (labels != null)
			{
				foreach (var item in labels.Select((label, i) => new { Index = i, Language = label.Key, Name = label.Value }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("label[{0}][language]", item.Index), item.Language));
					parameters.Add(new KeyValuePair<string, object>(string.Format("label[{0}][name]", item.Index), item.Name));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			// The data returned when creating a new template is a little bit unusual
			// Instead of simply returning the unique identifier of the new record like all the other 'Create' methods, for example: {"status":"success","data":"4593766"}
			// this method return an object with a single property called 'id' containing the unique identifier of the new record, like this: {"status":"success","data":{"id":"14052"}}
			return ExecuteRequest<long>(path, parameters, "id");
		}

		/// <summary>
		/// Delete a template
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="templateId">ID of the template</param>
		/// <param name="clientId">Client ID of the client in which the template is located.</param>
		/// <returns>True if the template is deleted</returns>
		public bool DeleteTemplate(string userKey, long templateId, long? clientId = null)
		{
			string path = "/TemplateV2/DeleteTemplate/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("template_id", templateId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			// The data returned when deleting a template is a little bit unusual
			// Instead of returning a boolean value to indicate success, it returns an empty array!!!
			// For example:  {"status":"success","data":[]}
			ExecuteRequest(path, parameters);
			return true;
		}

		/// <summary>
		/// Retrieve a template
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="templateId">ID of the template</param>
		/// <param name="clientId">Client ID of the client in which the template is located.</param>
		/// <returns>The <see cref="Template">template</see></returns>
		public Template GetTemplate(string userKey, long templateId, long? clientId = null)
		{
			var path = "/TemplateV2/GetTemplate/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("template_id", templateId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<Template>(path, parameters);
		}

		/// <summary>
		/// Retrieve the templates matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="limit">Limit the number of resulting templates.</param>
		/// <param name="offset">Offset the beginning of resulting templates.</param>
		/// <param name="clientId">Client ID of the client in which the templates are located.</param>
		/// <returns>Enumeration of <see cref="Template">templates</see> matching the filtering criteria</returns>
		public IEnumerable<Template> GetTemplates(string userKey, long? categoryId = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/TemplateV2/GetTemplates/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (categoryId.HasValue) parameters.Add(new KeyValuePair<string, object>("category_id", categoryId.Value));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<Template>(path, parameters, "templates");
		}

		/// <summary>
		/// Get a count of templates matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientId">Client ID of the client in which the templates are located.</param>
		/// <returns>The count of templates matching the filtering criteria</returns>
		public long GetTemplatesCount(string userKey, long? categoryId = null, long? clientId = null)
		{
			var path = "/TemplateV2/GetTemplates/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (categoryId.HasValue) parameters.Add(new KeyValuePair<string, object>("category_id", categoryId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Update a template
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="templateId">ID of the template</param>
		/// <param name="labels">Name of the template.</param>
		/// <param name="content">Content of the template.</param>
		/// <param name="categoryId">ID of the category.</param>
		/// <param name="clientId">Client ID of the client in which the template is located.</param>
		/// <returns>True if the category was updated</returns>
		public bool UpdateTemplate(string userKey, long templateId, IDictionary<string, string> labels, string content = null, long? categoryId = null, long? clientId = null)
		{
			string path = "/TemplateV2/SetTemplate/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("template_id", templateId)
			};
			if (labels != null)
			{
				foreach (var label in labels)
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("label[{0}]", label.Key), label.Value));
				}
			}
			if (content != null) parameters.Add(new KeyValuePair<string, object>("content", content));
			if (categoryId.HasValue) parameters.Add(new KeyValuePair<string, object>("category_id", categoryId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<bool>(path, parameters);
		}

		#endregion

		#region Methods related to USERS

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
		public long CreateUser(string userKey, string email, string password, string firstName = null, string lastName = null, string title = null, string officePhone = null, string mobilePhone = null, string language = null, long timezoneId = 542, long? clientId = null)
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
			return ExecuteRequest<long>(path, parameters, "id");
		}

		/// <summary>
		/// Suspend a user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the user is suspended</returns>
		public bool DeactivateUser(string userKey, long userId, long? clientId = null)
		{
			return UpdateUser(userKey, userId, status: UserStatus.Suspended, clientId: clientId);
		}

		/// <summary>
		/// Delete a user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the user is deleted</returns>
		public bool DeleteUser(string userKey, long userId, long? clientId = null)
		{
			return UpdateUser(userKey, userId, status: UserStatus.Deleted, clientId: clientId);
		}

		/// <summary>
		/// Retrieve a user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user</param>
		/// <param name="clientId">ID of the client</param>
		/// <returns>The <see cref="User">user</see></returns>
		public User GetUser(string userKey, long userId, long? clientId = null)
		{
			var path = "/User/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("user_id", userId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<User>(path, parameters);
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
		public IEnumerable<User> GetUsers(string userKey, UserStatus? status = null, int? limit = 0, int? offset = 0, long? clientId = null)
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

			return ExecuteArrayRequest<User>(path, parameters, "users");
		}

		/// <summary>
		/// Get a count of users matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the user status. Possible values: 'active', 'suspended'</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The count of users matching the filtering criteria</returns>
		public long GetUsersCount(string userKey, UserStatus? status = null, long? clientId = null)
		{
			var path = "/User/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
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
		public bool UpdateUser(string userKey, long userId, string email = null, string password = null, string firstName = null, string lastName = null, string title = null, string officePhone = null, string mobilePhone = null, string language = null, long? timezoneId = null, UserStatus? status = null, long? clientId = null)
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

			return ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Validate user name and password
		/// </summary>
		/// <param name="email">Email address of the user.</param>
		/// <param name="password">Password of the user.</param>
		/// <param name="clientId">ID of the client</param>
		/// <returns>The <see cref="LoginInfo">login information</see> for the user</returns>
		public LoginInfo Login(string email, string password, long? clientId = null)
		{
			var path = "/User/Login/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("email", email),
				new KeyValuePair<string, object>("password", password)

			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteRequest<LoginInfo>(path, parameters);
		}

		#endregion

		#region Private Methods

		private static string GetVersion()
		{
			try
			{
				// The following may throw 'System.Security.Permissions.FileIOPermission' under some circumpstances
				//var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;

				// Here's an alternative suggested by Phil Haack: http://haacked.com/archive/2010/11/04/assembly-location-and-medium-trust.aspx
				var assemblyVersion = new AssemblyName(typeof(CakeMailRestClient).Assembly.FullName).Version;
				var version = string.Format("{0}.{1}.{2}.{3}", assemblyVersion.Major, assemblyVersion.Minor, assemblyVersion.Build, assemblyVersion.Revision);

				return version;
			}
			catch
			{
				return "0.0.0.0";
			}
		}

		private long ExecuteCountRequest(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters)
		{
			return ExecuteRequest<long>(urlPath, parameters, "count");
		}

		private T[] ExecuteArrayRequest<T>(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters, string propertyName = null)
		{
			return ExecuteRequest<T[]>(urlPath, parameters, propertyName);
		}

		private T ExecuteRequest<T>(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters, string propertyName = null)
		{
			// Execute the API call
			var response = ExecuteRequest(urlPath, parameters);

			// Parse the response
			var data = ParseCakeMailResponse(response);

			// Check if the response is a well-known object type (JArray, of JValue)
			if (data is JArray) return (data as JArray).ToObject<T>();
			else if (data is JValue) return (data as JValue).ToObject<T>();

			// The response contains a JObject which we can return is a specific property was not requested
			if (string.IsNullOrEmpty(propertyName)) return (data as JObject).ToObject<T>();

			// The response contains a JObject but we only want a specific property. We must ensure the desired property is present
			var properties = (data as JObject).Properties().Where(p => p.Name.Equals(propertyName));
			if (!properties.Any()) throw new CakeMailException(string.Format("Json does not contain property {0}", propertyName));

			// Convert the property to the appropriate object type (JArray, JValue or JObject)
			var property = properties.First();
			if (property.Value is JArray) return (property.Value as JArray).ToObject<T>();
			else if (property.Value is JValue) return (property.Value as JValue).ToObject<T>();
			return (property.Value as JObject).ToObject<T>();
		}

		private IRestResponse ExecuteRequest(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters)
		{
			var request = new RestRequest(urlPath, Method.POST) { RequestFormat = DataFormat.Json };

			request.AddHeader("apikey", this.ApiKey);

			if (parameters != null)
			{
				foreach (var parameter in parameters)
				{
					request.AddParameter(parameter.Key, parameter.Value);
				}
			}

			var response = _client.Execute(request);
			var responseUri = response.ResponseUri ?? new Uri(string.Format("{0}/{1}", _client.BaseUrl.ToString().TrimEnd('/'), request.Resource.TrimStart('/')));

			if (response.ResponseStatus == ResponseStatus.Error)
			{
				var errorMessage = string.Format("Error received while making request: {0}", response.ErrorMessage);
				throw new HttpException(errorMessage, response.StatusCode, responseUri);
			}
			else if (response.ResponseStatus == ResponseStatus.TimedOut)
			{
				throw new HttpException("Request timed out", response.StatusCode, responseUri, response.ErrorException);
			}

			var statusCode = (int)response.StatusCode;
			if (statusCode == 200)
			{
				if (string.IsNullOrEmpty(response.Content))
				{
					var missingBodyMessage = string.Format("Received a 200 response from {0} but there was no message body.", request.Resource);
					throw new HttpException(missingBodyMessage, response.StatusCode, responseUri);
				}
				else if (response.ContentType == null || !response.ContentType.Contains("json"))
				{
					var unsupportedContentTypeMessage = string.Format("Received a 200 response from {0} but the content type is not JSON: {1}", request.Resource, response.ContentType ?? "NULL");
					throw new CakeMailException(unsupportedContentTypeMessage);
				}

				#region DEBUGGING
#if DEBUG
				var debugRequestMsg = string.Format("Request sent to CakeMail: {0}/{1}", _client.BaseUrl.ToString().TrimEnd('/'), urlPath.TrimStart('/'));
				var debugHeadersMsg = string.Format("Request headers: {0}", string.Join("&", request.Parameters.Where(p => p.Type == ParameterType.HttpHeader).Select(p => string.Concat(p.Name, "=", p.Value))));
				var debugParametersMsg = string.Format("Request parameters: {0}", string.Join("&", request.Parameters.Where(p => p.Type != ParameterType.HttpHeader).Select(p => string.Concat(p.Name, "=", p.Value))));
				var debugResponseMsg = string.Format("Response received from CakeMail: {0}", response.Content);
				Debug.WriteLine("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}\r\n{0}", new string('=', 25), debugRequestMsg, debugHeadersMsg, debugParametersMsg, debugResponseMsg);
#endif
				#endregion

				// Request was successful
				return response;
			}
			else if (statusCode >= 400 && statusCode < 500)
			{
				if (string.IsNullOrEmpty(response.Content))
				{
					var missingBodyMessage = string.Format("Received a {0} error from {1} with no body", response.StatusCode, request.Resource);
					throw new HttpException(missingBodyMessage, response.StatusCode, responseUri);
				}

				var errorMessage = string.Format("Received a {0} error from {1} with the following content: {2}", response.StatusCode, request.Resource, response.Content);
				throw new HttpException(errorMessage, response.StatusCode, responseUri);
			}
			else if (statusCode >= 500 && statusCode < 600)
			{
				var errorMessage = string.Format("Received a server ({0}) error from {1}", (int)response.StatusCode, request.Resource);
				throw new HttpException(errorMessage, response.StatusCode, responseUri);
			}
			else if (!string.IsNullOrEmpty(response.ErrorMessage))
			{
				var errorMessage = string.Format("Received an error message from {0} (status code: {1}) (error message: {2})", request.Resource, (int)response.StatusCode, response.ErrorMessage);
				throw new HttpException(errorMessage, response.StatusCode, responseUri);
			}
			else
			{
				var errorMessage = string.Format("Received an unexpected response from {0} (status code: {1})", request.Resource, (int)response.StatusCode);
				throw new HttpException(errorMessage, response.StatusCode, responseUri);
			}
		}

		private JToken ParseCakeMailResponse(IRestResponse response)
		{
			try
			{
				/* A typical response from the CakeMail API looks like this:
				 *	{
				 *		"status" : "success",
				 *		"data" : { ... data for the API call ... }
				 *	}
				 *	
				 * In case of an error, the response looks like this:
				 *	{
				 *		"status" : "failed",
				 *		"data" : "An error has occured"
				 *	}
				 */
				var cakeResponse = JObject.Parse(response.Content);
				var status = cakeResponse["status"].ToString();
				var data = cakeResponse["data"];
				var postData = cakeResponse["post"];

				if (status != "success")
				{
					if (postData != null) throw new CakeMailPostException(data.ToString(), postData.ToString());
					else throw new CakeMailException(data.ToString());
				}

				return data;
			}
			catch (JsonReaderException ex)
			{
				throw new CakeMailException(string.Format("Unable to decode response from CakeMail as JSON: {0}", response.Content), ex);
			}
		}

		#endregion
	}
}