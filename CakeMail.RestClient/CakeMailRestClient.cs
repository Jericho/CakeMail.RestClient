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
using System.Runtime.Serialization;

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

		public string ApiKey { get; private set; }

		public IWebProxy Proxy
		{
			get { return _client.Proxy; }
			set { _client.Proxy = value; }
		}

		/// <summary>
		///     UserAgent to use for requests.
		/// </summary>
		public string UserAgent
		{
			get { return _client.UserAgent; }
			set { _client.UserAgent = value; }
		}

		#endregion

		#region Constructors and Destructors

		public CakeMailRestClient(string apiKey, IRestClient restClient)
		{
			this.ApiKey = apiKey;
			_client = restClient;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ApiClient"/> class.
		/// </summary>
		/// <param name="apiKey">The API Key received from CakeMail</param>
		/// <param name="host">The host where the API is hosted. The default is api.wbsrvc.com</param>
		/// <param name="timeout">Timeout in milliseconds for connection to web service. The default is 5000.</param>
		public CakeMailRestClient(string apiKey, string host = "api.wbsrvc.com", int timeout = 5000)
		{
			this.ApiKey = apiKey;

			_client = new RestSharp.RestClient("https://" + host)
			{
				Timeout = timeout,
				UserAgent = string.Format("CakeMail .NET REST Client {0}", _version)
			};
		}

		#endregion

		#region Methods related to CAMPAIGNS

		/// <summary>
		/// Method to create a new campaign
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the campaign.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is created.</param>
		/// <returns>ID of the new campaign</returns>
		public int CreateCampaign(string userKey, string name, int? clientId = null)
		{
			string path = "/Campaign/Create/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("name", name)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<int>(path, parameters);
		}

		/// <summary>
		/// Method to delete a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign to delete.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <returns></returns>
		public bool DeleteCampaign(string userKey, int campaignId, int? clientId = null)
		{
			string path = "/Campaign/Delete/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("campaign_id", campaignId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public Campaign GetCampaign(string userKey, int campaignId, int? clientId = null)
		{
			var path = "/Campaign/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("campaign_id", campaignId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<Campaign>(path, parameters);
		}

		public IEnumerable<Campaign> GetCampaigns(string userKey, string status = null, string name = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			var path = "/Campaign/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (sortBy != null) parameters.Add(new KeyValuePair<string, object>("sort_by", sortBy));
			if (sortDirection != null) parameters.Add(new KeyValuePair<string, object>("direction", sortDirection));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<Campaign>(path, parameters, "campaigns");
		}

		public long GetCampaignsCount(string userKey, string status = null, string name = null, int? clientId = null)
		{
			var path = "/Campaign/GetList/";
			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		public bool UpdateCampaign(string userKey, int campaignId, string name, int? clientId = null)
		{
			string path = "/Campaign/SetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("campaign_id", campaignId)
			};
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		#endregion

		#region Methods related to CLIENTS

		public string CreateClient(int parentId, string name, string address1 = null, string address2 = null, string city = null, string provinceId = null, string postalCode = null, string countryId = null, string website = null, string phone = null, string fax = null, string adminEmail = null, string adminFirstName = null, string adminLastName = null, string adminTitle = null, string adminOfficePhone = null, string adminMobilePhone = null, string adminLanguage = null, int? adminTimezoneId = null, string adminPassword = null, bool primaryContactSameAsAdmin = true, string primaryContactEmail = null, string primaryContactFirstName = null, string primaryContactLastName = null, string primaryContactTitle = null, string primaryContactOfficePhone = null, string primaryContactMobilePhone = null, string primaryContactLanguage = null, int? primaryContactTimezoneId = null, string primaryContactPassword = null)
		{
			string path = "/Client/Create/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("parent_id", parentId),
				new KeyValuePair<string, object>("company_name", name)
			};
			if (!string.IsNullOrEmpty(adminEmail))
			{
				parameters.Add(new KeyValuePair<string, object>("admin_email", adminEmail));
				parameters.Add(new KeyValuePair<string, object>("admin_first_name", adminFirstName));
				parameters.Add(new KeyValuePair<string, object>("admin_last_name", adminLastName));
				parameters.Add(new KeyValuePair<string, object>("admin_password", adminPassword));
				parameters.Add(new KeyValuePair<string, object>("admin_password_confirmation", adminPassword));
			}
			if (!primaryContactSameAsAdmin && !string.IsNullOrEmpty(primaryContactEmail))
			{
				parameters.Add(new KeyValuePair<string, object>("contact_email", primaryContactEmail));
				parameters.Add(new KeyValuePair<string, object>("contact_first_name", primaryContactFirstName));
				parameters.Add(new KeyValuePair<string, object>("contact_last_name", primaryContactLastName));
				parameters.Add(new KeyValuePair<string, object>("contact_password", primaryContactPassword));
				parameters.Add(new KeyValuePair<string, object>("contact_password_confirmation", primaryContactPassword));
			}
			parameters.Add(new KeyValuePair<string, object>("contact_same_as_admin", primaryContactSameAsAdmin ? "1" : "0"));
			if (address1 != null) parameters.Add(new KeyValuePair<string, object>("address1", address1));
			if (address2 != null) parameters.Add(new KeyValuePair<string, object>("address2", address2));
			if (city != null) parameters.Add(new KeyValuePair<string, object>("city", city));
			if (provinceId != null) parameters.Add(new KeyValuePair<string, object>("province_id", provinceId));
			if (postalCode != null) parameters.Add(new KeyValuePair<string, object>("postal_code", postalCode));
			if (countryId != null) parameters.Add(new KeyValuePair<string, object>("country_id", countryId));
			if (website != null) parameters.Add(new KeyValuePair<string, object>("website", website));
			if (phone != null) parameters.Add(new KeyValuePair<string, object>("phone", phone));
			if (fax != null) parameters.Add(new KeyValuePair<string, object>("fax", fax));
			if (primaryContactSameAsAdmin) parameters.Add(new KeyValuePair<string, object>("contact_same_as_admin", "1"));
			if (adminTitle != null) parameters.Add(new KeyValuePair<string, object>("admin_title", adminTitle));
			if (adminOfficePhone != null) parameters.Add(new KeyValuePair<string, object>("admin_office_phone", adminOfficePhone));
			if (adminMobilePhone != null) parameters.Add(new KeyValuePair<string, object>("admin_mobile_phone", adminMobilePhone));
			if (adminLanguage != null) parameters.Add(new KeyValuePair<string, object>("admin_language", adminLanguage));
			if (adminTimezoneId.HasValue) parameters.Add(new KeyValuePair<string, object>("admin_timezone_id", adminTimezoneId.Value));
			if (primaryContactTitle != null) parameters.Add(new KeyValuePair<string, object>("contact_title", primaryContactTitle));
			if (primaryContactLanguage != null) parameters.Add(new KeyValuePair<string, object>("contact_language", primaryContactLanguage));
			if (primaryContactTimezoneId.HasValue) parameters.Add(new KeyValuePair<string, object>("contact_timezone_id", primaryContactTimezoneId.Value));
			if (primaryContactOfficePhone != null) parameters.Add(new KeyValuePair<string, object>("contact_office_phone", primaryContactOfficePhone));
			if (primaryContactMobilePhone != null) parameters.Add(new KeyValuePair<string, object>("contact_mobile_phone", primaryContactMobilePhone));

			return ExecuteObjectRequest<string>(path, parameters);
		}

		public ActivationInfo ActivateClient(string confirmation)
		{
			string path = "/Client/Activate/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("confirmation", confirmation)
			};

			return ExecuteObjectRequest<ActivationInfo>(path, parameters);
		}

		public Client GetClient(string userKey, int clientId, DateTime? startDate = null, DateTime? endDate = null)
		{
			var path = "/Client/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("client_id", clientId)
			};
			if (startDate.HasValue) parameters.Add(new KeyValuePair<string, object>("start_date", ((DateTime)startDate.Value).ToCakeMailString()));
			if (endDate.HasValue) parameters.Add(new KeyValuePair<string, object>("end_date", ((DateTime)endDate.Value).ToCakeMailString()));

			return ExecuteObjectRequest<Client>(path, parameters);
		}

		public IEnumerable<Client> GetClients(string userKey, string status = null, string name = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			var path = "/Client/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("company_name", name));
			if (sortBy != null) parameters.Add(new KeyValuePair<string, object>("sort_by", sortBy));
			if (sortDirection != null) parameters.Add(new KeyValuePair<string, object>("direction", sortDirection));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<Client>(path, parameters, "clients");
		}

		public long GetClientsCount(string userKey, string status = null, string name = null, int? clientId = null)
		{
			var path = "/Client/GetList/";
			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		public bool UpdateClient(string userKey, int clientId, string name = null, string status = null, int? parentId = null, string address1 = null, string address2 = null, string city = null, string provinceId = null, string postalCode = null, string countryId = null, string website = null, string phone = null, string fax = null, string authDomain = null, string bounceDomain = null, string dkimDomain = null, string doptinIp = null, string forwardDomain = null, string forwardIp = null, string ipPool = null, string mdDomain = null, bool? isReseller = null, string currency = null, string planType = null, string mailingLimit = null, string monthLimit = null, string defaultMailingLimit = null, string defaultMonthLimit = null)
		{
			string path = "/Client/SetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("client_id", clientId)
			};
			if (name != null) parameters.Add(new KeyValuePair<string, object>("company_name", name));
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
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
			if (isReseller.HasValue) parameters.Add(new KeyValuePair<string, object>("reseller", isReseller.Value));
			if (currency != null) parameters.Add(new KeyValuePair<string, object>("currency", currency));
			if (planType != null) parameters.Add(new KeyValuePair<string, object>("plan_type", planType));
			if (mailingLimit != null) parameters.Add(new KeyValuePair<string, object>("mailing_limit", mailingLimit));
			if (monthLimit != null) parameters.Add(new KeyValuePair<string, object>("month_limit", monthLimit));
			if (defaultMailingLimit != null) parameters.Add(new KeyValuePair<string, object>("default_mailing_limit", defaultMailingLimit));
			if (defaultMonthLimit != null) parameters.Add(new KeyValuePair<string, object>("default_monthLimit", defaultMonthLimit));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		#endregion

		#region Methods related to PERMISSIONS

		public IEnumerable<string> GetUserPermissions(string userKey, int userId, int? clientId = null)
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

		public bool SetUserPermissions(string userKey, int userId, IEnumerable<string> permissions, int? clientId = null)
		{
			var path = "/Permission/SetPermissions/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("user_id", userId)
			};
			foreach (var permission in permissions)
			{
				parameters.Add(new KeyValuePair<string, object>("permission", permission));
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		#endregion

		#region Methods related to COUNTRIES

		public IEnumerable<Country> GetCountries()
		{
			var path = "/Country/GetList/";

			var parameters = new List<KeyValuePair<string, object>>();

			return ExecuteArrayRequest<Country>(path, parameters, "countries");
		}

		public IEnumerable<Province> GetProvinces(string countryId)
		{
			var path = "/Country/GetProvinces/";

			var parameters = new List<KeyValuePair<string, object>>();
			parameters.Add(new KeyValuePair<string, object>("country_id", countryId));

			return ExecuteArrayRequest<Province>(path, parameters, "provinces");
		}

		#endregion

		#region Methods related to LISTS

		public int CreateList(string userKey, string name, string defaultSenderName, string defaultSenderEmailAddress, int? clientId = null)
		{
			string path = "/List/Create/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("name", name)
			};
			if (defaultSenderName != null) parameters.Add(new KeyValuePair<string, object>("sender_name", defaultSenderName));
			if (defaultSenderEmailAddress != null) parameters.Add(new KeyValuePair<string, object>("sender_email", defaultSenderEmailAddress));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<int>(path, parameters);
		}

		public bool DeleteList(string userKey, int listId, int? clientId = null)
		{
			string path = "/List/Delete/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public List GetList(string userKey, int listId, int? subListId = null, bool includeDetails = true, bool calculateEngagement = false, int? clientId = null)
		{
			var path = "/List/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId)
			};
			if (subListId.HasValue) parameters.Add(new KeyValuePair<string, object>("sublist_id", subListId.Value));
			parameters.Add(new KeyValuePair<string, object>("no_details", includeDetails ? "false" : "true"));	// CakeMail expects 'false' if you want to include details
			parameters.Add(new KeyValuePair<string, object>("with_engagement", calculateEngagement ? "true" : "false"));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<List>(path, parameters);
		}

		public IEnumerable<List> GetLists(string userKey, string name = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			var path = "/List/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (sortBy != null) parameters.Add(new KeyValuePair<string, object>("sort_by", sortBy));
			if (sortDirection != null) parameters.Add(new KeyValuePair<string, object>("direction", sortDirection));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<List>(path, parameters, "lists");
		}

		public long GetListsCount(string userKey, string name = null, int? clientId = null)
		{
			var path = "/List/GetList/";
			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		public bool UpdateList(string userKey, int listId, int? subListId = null, string name = null, string language = null, string policy = null, string status = null, string senderName = null, string senderEmail = null, string goto_oi = null, string goto_di = null, string goto_oo = null, string webhook = null, string query = null, int? clientId = null)
		{
			string path = "/List/SetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId)
			};
			if (subListId.HasValue) parameters.Add(new KeyValuePair<string, object>("sublist_id", subListId));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (language != null) parameters.Add(new KeyValuePair<string, object>("language", language));
			if (policy != null) parameters.Add(new KeyValuePair<string, object>("policy", policy));
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (senderName != null) parameters.Add(new KeyValuePair<string, object>("sender_name", senderName));
			if (senderEmail != null) parameters.Add(new KeyValuePair<string, object>("sender_email", senderEmail));
			if (goto_oi != null) parameters.Add(new KeyValuePair<string, object>("goto_oi", goto_oi));
			if (goto_di != null) parameters.Add(new KeyValuePair<string, object>("goto_di", goto_di));
			if (goto_oo != null) parameters.Add(new KeyValuePair<string, object>("goto_oo", goto_oo));
			if (webhook != null) parameters.Add(new KeyValuePair<string, object>("webhook", webhook));
			if (query != null) parameters.Add(new KeyValuePair<string, object>("query", query));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userKey"></param>
		/// <param name="listId"></param>
		/// <param name="fieldName"></param>
		/// <param name="fieldType">Possible values: 'text', 'integer', 'datetime' or 'mediumtext'</param>
		/// <param name="clientId"></param>
		/// <returns></returns>
		public bool AddListField(string userKey, int listId, string fieldName, string fieldType, int? clientId = null)
		{
			string path = "/List/EditStructure/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("action", "add"),
				new KeyValuePair<string, object>("field", fieldName),
				new KeyValuePair<string, object>("type", fieldType)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public bool DeleteListField(string userKey, int listId, string fieldName, int? clientId = null)
		{
			string path = "/List/EditStructure/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("action", "delete"),
				new KeyValuePair<string, object>("field", fieldName)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public IEnumerable<ListField> GetListFields(string userKey, int listId, int? clientId = null)
		{
			var path = "/List/GetFields/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			var fieldsStructure = ExecuteObjectRequest<ExpandoObject>(path, parameters);
			if (fieldsStructure == null) return Enumerable.Empty<ListField>();

			var fields = fieldsStructure.Select(x => new ListField() { Name = x.Key, Type = x.Value.ToString() });
			return fields;
		}

		public int CreateSublist(string userKey, int listId, string name, string query = null, int? clientId = null)
		{
			string path = "/List/CreateSublist/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("sublist_name", name),
				new KeyValuePair<string, object>("query", query)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<int>(path, parameters);
		}

		public bool DeleteSublist(string userKey, int sublistId, int? clientId = null)
		{
			string path = "/List/DeleteSublist/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("sublist_id", sublistId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public IEnumerable<List> GetSublists(string userKey, int listId, int limit = 0, int offset = 0, bool includeDetails = true, int? clientId = null)
		{
			var path = "/List/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("count", "false")
			};
			parameters.Add(new KeyValuePair<string, object>("no_details", includeDetails ? "false" : "true"));	// CakeMail expects 'false' if you want to include details
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<List>(path, parameters, "lists");
		}

		public long GetSublistsCount(string userKey, int listId, int? clientId = null)
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

		public bool AddTestEmail(string userKey, int listId, string email, int? clientId = null)
		{
			string path = "/List/AddTestEmail/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("email", email)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public bool DeleteTestEmail(string userKey, int listId, string email, int? clientId = null)
		{
			string path = "/List/DeleteTestEmail/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("email", email)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public IEnumerable<string> GetTestEmails(string userKey, int listId, int? clientId = null)
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

		public int Subscribe(string userKey, int listId, string email, bool autoResponders = true, bool triggers = true, IEnumerable<KeyValuePair<string, object>> customFields = null, int? clientId = null)
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

			return ExecuteObjectRequest<int>(path, parameters);
		}

		public IEnumerable<ImportResult> Import(string userKey, int listId, bool autoResponders = true, bool triggers = true, IEnumerable<ListMember> listMembers = null, int? clientId = null)
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
				var recordCount = 0;
				foreach (var listMember in listMembers)
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("record[{0}][email]", recordCount), listMember.Email));
					foreach (var customField in listMember.CustomFields)
					{
						if (customField.Value is DateTime) parameters.Add(new KeyValuePair<string, object>(string.Format("record[{0}][{1}]", recordCount, customField.Key), ((DateTime)customField.Value).ToCakeMailString()));
						else parameters.Add(new KeyValuePair<string, object>(string.Format("record[{0}][{1}]", recordCount, customField.Key), customField.Value));
					}
					recordCount++;
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<ImportResult>(path, parameters, null);
		}

		public bool Unsubscribe(string userKey, int listId, string email, int? clientId = null)
		{
			string path = "/List/UnsubscribeEmail/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("email", email)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public bool Unsubscribe(string userKey, int listId, int listMemberId, int? clientId = null)
		{
			string path = "/List/UnsubscribeEmail/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("record_id", listMemberId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public bool DeleteListMember(string userKey, int listId, int listMemberId, int? clientId = null)
		{
			string path = "/List/DeleteRecord/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("record_id", listMemberId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public ListMember GetListMember(string userKey, int listId, int listMemberId, int? clientId = null)
		{
			var path = "/List/GetRecord/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("record_id", listMemberId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			var listMember = ExecuteObjectRequest<ListMember>(path, parameters);
			return listMember;
		}

		public IEnumerable<ListMember> GetListMembers(string userKey, int listId, string status = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			var path = "/List/Show/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (sortBy != null) parameters.Add(new KeyValuePair<string, object>("sort_by", sortBy));
			if (sortDirection != null) parameters.Add(new KeyValuePair<string, object>("direction", sortDirection));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<ListMember>(path, parameters, "members");
		}

		public long GetListMembersCount(string userKey, int listId, string status = null, int? clientId = null)
		{
			var path = "/List/Show/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		public bool UpdateListMember(string userKey, int listId, int listMemberId, IEnumerable<KeyValuePair<string, object>> customFields = null, int? clientId = null)
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

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userKey"></param>
		/// <param name="listId"></param>
		/// <param name="logType">Possible values: "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped"</param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="limit"></param>
		/// <param name="offset"></param>
		/// <param name="clientId"></param>
		/// <returns></returns>
		public IEnumerable<LogItem> GetListLogs(string userKey, int listId, string logType = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null)
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
			if (logType != null) parameters.Add(new KeyValuePair<string, object>("action", logType));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", ((DateTime)start.Value).ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", ((DateTime)end.Value).ToCakeMailString()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<LogItem>(path, parameters, "logs");
		}

		public long GetListLogsCount(string userKey, int listId, string logType = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int? clientId = null)
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
			if (logType != null) parameters.Add(new KeyValuePair<string, object>("action", logType));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", ((DateTime)start.Value).ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", ((DateTime)end.Value).ToCakeMailString()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		#endregion

		#region Methods related to MAILINGS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userKey"></param>
		/// <param name="name"></param>
		/// <param name="campaignId"></param>
		/// <param name="type">Type of the mailing. Possible value 'standard', 'recurring', 'absplit'</param>
		/// <param name="recurringId"></param>
		/// <param name="encoding">Encoding to be used for the mailing. Possible value 'utf-8', 'iso-8859-x'</param>
		/// <param name="transferEncoding">Transfer encoding to be used for the mailing. Possible value 'quoted-printable', 'base64'</param>
		/// <param name="clientId"></param>
		/// <returns></returns>
		public int CreateMailing(string userKey, string name, int? campaignId = null, string type = "standard", int? recurringId = null, string encoding = null, string transferEncoding = null, int? clientId = null)
		{
			string path = "/Mailing/Create/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("name", name)
			};
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (type != null) parameters.Add(new KeyValuePair<string, object>("type", type));
			if (recurringId.HasValue) parameters.Add(new KeyValuePair<string, object>("recurring_id", recurringId.Value));
			if (encoding != null) parameters.Add(new KeyValuePair<string, object>("encoding", encoding));
			if (transferEncoding != null) parameters.Add(new KeyValuePair<string, object>("transfer_encoding", transferEncoding));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<int>(path, parameters);
		}

		public bool DeleteMailing(string userKey, int mailingId, int? clientId = null)
		{
			string path = "/Mailing/Delete/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public Mailing GetMailing(string userKey, int mailingId, int? clientId = null)
		{
			var path = "/Mailing/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<Mailing>(path, parameters);
		}

		public IEnumerable<Mailing> GetMailings(string userKey, string status = null, string type = null, string name = null, int? listId = null, int? campaignId = null, int? recurringId = null, DateTime? start = null, DateTime? end = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			var path = "/Mailing/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (type != null) parameters.Add(new KeyValuePair<string, object>("type", type));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (listId.HasValue) parameters.Add(new KeyValuePair<string, object>("list_id", listId.Value));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (recurringId.HasValue) parameters.Add(new KeyValuePair<string, object>("recurring_id", recurringId.Value));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_date", ((DateTime)start.Value).ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_date", ((DateTime)end.Value).ToCakeMailString()));
			if (sortBy != null) parameters.Add(new KeyValuePair<string, object>("sort_by", sortBy));
			if (sortDirection != null) parameters.Add(new KeyValuePair<string, object>("direction", sortDirection));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<Mailing>(path, parameters, "mailings");
		}

		public long GetMailingsCount(string userKey, string status = null, string type = null, string name = null, int? listId = null, int? campaignId = null, int? recurringId = null, DateTime? start = null, DateTime? end = null, int? clientId = null)
		{
			var path = "/Mailing/GetList/";
			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (type != null) parameters.Add(new KeyValuePair<string, object>("type", type));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (listId.HasValue) parameters.Add(new KeyValuePair<string, object>("list_id", listId.Value));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (recurringId.HasValue) parameters.Add(new KeyValuePair<string, object>("recurring_id", recurringId.Value));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_date", ((DateTime)start.Value).ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_date", ((DateTime)end.Value).ToCakeMailString()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		public bool UpdateMailing(string userKey, int mailingId, int? campaignId = null, int? listId = null, int? sublistId = null, string name = null, string type = null, string encoding = null, string transferEncoding = null, string subject = null, string senderEmail = null, string senderName = null, string replyTo = null, string htmlContent = null, string textContent = null, bool? trackOpens = null, bool? trackClicksInHtml = null, bool? trackClicksInText = null, string trackingParameters = null, DateTime? endingOn = null, int? maxRecurrences = null, string recurringConditions = null, int? clientId = null)
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
			if (type != null) parameters.Add(new KeyValuePair<string, object>("type", type));
			if (encoding != null) parameters.Add(new KeyValuePair<string, object>("encoding", encoding));
			if (transferEncoding != null) parameters.Add(new KeyValuePair<string, object>("transfer_encoding", transferEncoding));
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
			if (endingOn.HasValue) parameters.Add(new KeyValuePair<string, object>("ending_on", ((DateTime)endingOn.Value).ToCakeMailString()));
			if (maxRecurrences.HasValue) parameters.Add(new KeyValuePair<string, object>("max_recurrences", maxRecurrences.Value));
			if (recurringConditions != null) parameters.Add(new KeyValuePair<string, object>("recurring_conditions", recurringConditions));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public bool SendMailingTestEmail(string userKey, int mailingId, string recipientEmail, bool separated = false, int? clientId = null)
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

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public RawEmailMessage GetMailingRawEmailMessage(string userKey, int mailingId, int? clientId = null)
		{
			var path = "/Mailing/GetEmailMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<RawEmailMessage>(path, parameters);
		}

		public string GetMailingRawHtml(string userKey, int mailingId, int? clientId = null)
		{
			var path = "/Mailing/GetHtmlMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<string>(path, parameters);
		}

		public string GetMailingRawText(string userKey, int mailingId, int? clientId = null)
		{
			var path = "/Mailing/GetTextMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<string>(path, parameters);
		}

		public bool ScheduleMailing(string userKey, int mailingId, DateTime? date = null, int? clientId = null)
		{
			var path = "/Mailing/Schedule/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (date.HasValue) parameters.Add(new KeyValuePair<string, object>("date", ((DateTime)date.Value).ToCakeMailString()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public bool UnscheduleMailing(string userKey, int mailingId, int? clientId = null)
		{
			var path = "/Mailing/Unschedule/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public bool SuspendMailing(string userKey, int mailingId, int? clientId = null)
		{
			var path = "/Mailing/Suspend/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public bool ResumeMailing(string userKey, int mailingId, int? clientId = null)
		{
			var path = "/Mailing/Resume/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userKey"></param>
		/// <param name="listId"></param>
		/// <param name="logType">Possible values: "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped"</param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="limit"></param>
		/// <param name="offset"></param>
		/// <param name="clientId"></param>
		/// <returns></returns>
		public IEnumerable<LogItem> GetMailingLogs(string userKey, int mailingId, string logType = null, int? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null)
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
			if (logType != null) parameters.Add(new KeyValuePair<string, object>("action", logType));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", ((DateTime)start.Value).ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", ((DateTime)end.Value).ToCakeMailString()));
			if (listMemberId.HasValue) parameters.Add(new KeyValuePair<string, object>("record_id", listMemberId.Value));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<LogItem>(path, parameters, "logs");
		}

		public long GetMailingLogsCount(string userKey, int mailingId, string logType = null, int? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int? clientId = null)
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
			if (logType != null) parameters.Add(new KeyValuePair<string, object>("action", logType));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", ((DateTime)start.Value).ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", ((DateTime)end.Value).ToCakeMailString()));
			if (listMemberId.HasValue) parameters.Add(new KeyValuePair<string, object>("record_id", listMemberId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		public IEnumerable<Link> GetMailingLinks(string userKey, int mailingId, int limit = 0, int offset = 0, int? clientId = null)
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

		public long GetMailingLinksCount(string userKey, int mailingId, int? clientId = null)
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

		public Link GetMailingLink(string userKey, int linkId, int? clientId = null)
		{
			string path = "/Mailing/GetLinkInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("link_id", linkId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<Link>(path, parameters);
		}

		public IEnumerable<LogItem> GetMailingLinksLogs(string userKey, int mailingId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			string path = "/Mailing/GetLog/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", ((DateTime)start.Value).ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", ((DateTime)end.Value).ToCakeMailString()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<LogItem>(path, parameters, "logs");
		}

		#endregion

		#region Methods related to RELAYS

		public bool SendRelay(string userKey, string email, string senderEmail, string senderName, string html, string text, string subject, string encoding, bool trackOpens, bool trackClicksInHtml, bool trackClicksInText, int trackingId, int? clientId = null)
		{
			string path = "/Relay/Send/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("email", email),
				new KeyValuePair<string, object>("subject", subject)
			};
			if (senderName != null) parameters.Add(new KeyValuePair<string, object>("sender_name", senderName));
			if (senderEmail != null) parameters.Add(new KeyValuePair<string, object>("sender_email", senderEmail));
			if (html != null) parameters.Add(new KeyValuePair<string, object>("html_message", html));
			if (text != null) parameters.Add(new KeyValuePair<string, object>("text_message", text));
			if (encoding != null) parameters.Add(new KeyValuePair<string, object>("encoding", encoding));
			parameters.Add(new KeyValuePair<string, object>("track_opening", trackOpens ? "true" : "false"));
			parameters.Add(new KeyValuePair<string, object>("track_clicks_in_html", trackClicksInHtml ? "true" : "false"));
			parameters.Add(new KeyValuePair<string, object>("track_clicks_in_text", trackClicksInText ? "true" : "false"));
			parameters.Add(new KeyValuePair<string, object>("tracking_id", trackingId));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public IEnumerable<RelayLog> GetRelaySentLogs(string userKey, int? trackingId = null, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			return GetRelayLogs<RelayLog>(userKey, "sent", "sent_logs", trackingId, start, end, limit, offset, clientId);
		}

		public IEnumerable<RelayOpenLog> GetRelayOpenLogs(string userKey, int? trackingId = null, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			return GetRelayLogs<RelayOpenLog>(userKey, "open", "open_logs", trackingId, start, end, limit, offset, clientId);
		}

		public IEnumerable<RelayClickLog> GetRelayClickLogs(string userKey, int? trackingId = null, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			return GetRelayLogs<RelayClickLog>(userKey, "clickthru", "click_logs", trackingId, start, end, limit, offset, clientId);
		}

		public IEnumerable<RelayBounceLog> GetRelayBounceLogs(string userKey, int? trackingId = null, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			return GetRelayLogs<RelayBounceLog>(userKey, "bounce", "bounce_logs", trackingId, start, end, limit, offset, clientId);
		}

		private IEnumerable<T> GetRelayLogs<T>(string userKey, string logType, string arrayPropertyName, int? trackingId = null, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null) where T : RelayLog, new()
		{
			string path = "/Relay/GetLogs/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("log_type", logType)
			};
			if (trackingId.HasValue) parameters.Add(new KeyValuePair<string, object>("tracking_id", trackingId.Value));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", ((DateTime)start.Value).ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", ((DateTime)end.Value).ToCakeMailString()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<T>(path, parameters, arrayPropertyName);
		}

		#endregion

		#region Methods related to SUPPRESSION LISTS

		public IEnumerable<SuppressEmailResult> AddEmailAddressesToSuppressionList(string userKey, IEnumerable<string> emailAddresses, int? clientId = null)
		{
			string path = "/SuppressionList/ImportEmails/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (emailAddresses != null)
			{
				var recordCount = 0;
				foreach (var emailAddress in emailAddresses)
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("email[{0}]", recordCount), emailAddress));
					recordCount++;
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<SuppressEmailResult>(path, parameters, null);
		}

		public IEnumerable<SuppressDomainResult> AddDomainsToSuppressionList(string userKey, IEnumerable<string> domains, int? clientId = null)
		{
			string path = "/SuppressionList/ImportDomains/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (domains != null)
			{
				var recordCount = 0;
				foreach (var domain in domains)
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("domain[{0}]", recordCount), domain));
					recordCount++;
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<SuppressDomainResult>(path, parameters, null);
		}

		public IEnumerable<SuppressLocalPartResult> AddLocalPartsToSuppressionList(string userKey, IEnumerable<string> localParts, int? clientId = null)
		{
			string path = "/SuppressionList/ImportLocalparts/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (localParts != null)
			{
				var recordCount = 0;
				foreach (var localPart in localParts)
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("localpart[{0}]", recordCount), localPart));
					recordCount++;
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<SuppressLocalPartResult>(path, parameters, null);
		}

		public IEnumerable<SuppressEmailResult> RemoveEmailAddressesFromSuppressionList(string userKey, IEnumerable<string> emailAddresses, int? clientId = null)
		{
			string path = "/SuppressionList/DeleteEmails/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (emailAddresses != null)
			{
				var recordCount = 0;
				foreach (var emailAddress in emailAddresses)
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("email[{0}]", recordCount), emailAddress));
					recordCount++;
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<SuppressEmailResult>(path, parameters, null);
		}

		public IEnumerable<SuppressDomainResult> RemoveDomainsFromSuppressionList(string userKey, IEnumerable<string> domains, int? clientId = null)
		{
			string path = "/SuppressionList/DeleteDomains/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (domains != null)
			{
				var recordCount = 0;
				foreach (var domain in domains)
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("domain[{0}]", recordCount), domain));
					recordCount++;
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<SuppressDomainResult>(path, parameters, null);
		}

		public IEnumerable<SuppressLocalPartResult> RemoveLocalPartsFromSuppressionList(string userKey, IEnumerable<string> localParts, int? clientId = null)
		{
			string path = "/SuppressionList/DeleteLocalparts/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (localParts != null)
			{
				var recordCount = 0;
				foreach (var localPart in localParts)
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("localpart[{0}]", recordCount), localPart));
					recordCount++;
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<SuppressLocalPartResult>(path, parameters, null);
		}

		public IEnumerable<SuppressedEmail> GetSuppressedEmailAddresses(string userKey, int limit = 0, int offset = 0, int? clientId = null)
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

		public IEnumerable<string> GetSuppressedDomains(string userKey, int limit = 0, int offset = 0, int? clientId = null)
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
			if (result == null) return Enumerable.Empty<string>();

			var domains = (from r in result select r.Single(p => p.Key == "domain").Value.ToString()).ToArray();
			return domains;
		}

		public IEnumerable<string> GetSuppressedLocalParts(string userKey, int limit = 0, int offset = 0, int? clientId = null)
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
			if (result == null) return Enumerable.Empty<string>();

			var localParts = (from r in result select r.Single(p => p.Key == "localpart").Value.ToString()).ToArray();
			return localParts;
		}

		#endregion

		#region Methods related to TIMEZONES

		public IEnumerable<Timezone> GetTimezones()
		{
			var path = "/Client/GetTimezones/";

			var parameters = new List<KeyValuePair<string, object>>();

			return ExecuteArrayRequest<Timezone>(path, parameters, "timezones");
		}

		#endregion

		#region Methods related to TRIGGERS

		public int CreateTrigger(string userKey, string name, int listId, string encoding = null, string transferEncoding = null, int? campaignId = null, int? clientId = null)
		{
			string path = "/Trigger/Create/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("name", name),
				new KeyValuePair<string, object>("list_id", listId)
			};
			if (encoding != null) parameters.Add(new KeyValuePair<string, object>("encoding", encoding));
			if (transferEncoding != null) parameters.Add(new KeyValuePair<string, object>("transfer_encoding", transferEncoding));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<int>(path, parameters);
		}

		//public bool DeleteTrigger(string userKey, int triggerId, int? clientId = null)
		//{
		//	string path = "/Trigger/Delete/";

		//	var parameters = new List<KeyValuePair<string, object>>()
		//	{
		//		new KeyValuePair<string, object>("user_key", userKey),
		//		new KeyValuePair<string, object>("trigger_id", triggerId),
		//	};
		//	if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

		//	return ExecuteObjectRequest<bool>(path, parameters);
		//}

		public Trigger GetTrigger(string userKey, int triggerId, int? clientId = null)
		{
			var path = "/Trigger/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<Trigger>(path, parameters);
		}

		public bool UpdateTrigger(string userKey, int triggerId, int? campaignId = null, string name = null, string action = null, string encoding = null, string transferEncoding = null, string subject = null, string senderEmail = null, string senderName = null, string replyTo = null, string htmlContent = null, string textContent = null, bool? trackOpens = null, bool? trackClicksInHtml = null, bool? trackClicksInText = null, string trackingParameters = null, int? delay = null, string status = null, string dateField = null, int? clientId = null)
		{
			var path = "/Trigger/SetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (action != null) parameters.Add(new KeyValuePair<string, object>("action", action));
			if (encoding != null) parameters.Add(new KeyValuePair<string, object>("encoding", encoding));
			if (transferEncoding != null) parameters.Add(new KeyValuePair<string, object>("transfer_encoding", transferEncoding));
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
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (dateField != null) parameters.Add(new KeyValuePair<string, object>("date_field", dateField));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public IEnumerable<Trigger> GetTriggers(string userKey, string status = null, string action = null, int? listId = null, int? campaignId = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			var path = "/Trigger/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (action != null) parameters.Add(new KeyValuePair<string, object>("action", action));
			if (listId.HasValue) parameters.Add(new KeyValuePair<string, object>("list_id", listId.Value));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<Trigger>(path, parameters, "triggers");
		}

		public long GetTriggersCount(string userKey, string status = null, string action = null, int? listId = null, int? campaignId = null, int? clientId = null)
		{
			var path = "/Trigger/GetList/";
			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (action != null) parameters.Add(new KeyValuePair<string, object>("action", action));
			if (listId.HasValue) parameters.Add(new KeyValuePair<string, object>("list_id", listId.Value));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		public bool SendTriggerTestEmail(string userKey, int triggerId, string recipientEmail, bool separated = false, int? clientId = null)
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

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public RawEmailMessage GetTriggerRawEmailMessage(string userKey, int triggerId, int? clientId = null)
		{
			var path = "/Trigger/GetEmailMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<RawEmailMessage>(path, parameters);
		}

		public string GetTriggerRawHtml(string userKey, int triggerId, int? clientId = null)
		{
			var path = "/Trigger/GetHtmlMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<string>(path, parameters);
		}

		public string GetTriggerRawText(string userKey, int triggerId, int? clientId = null)
		{
			var path = "/Trigger/GetTextMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<string>(path, parameters);
		}

		public bool UnleashTrigger(string userKey, int triggerId, int listMemberId, int? clientId = null)
		{
			var path = "/Trigger/Unleash/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("record_id", listMemberId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public IEnumerable<LogItem> GetTriggerLogs(string userKey, int triggerId, string logType = null, int? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null)
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
			if (logType != null) parameters.Add(new KeyValuePair<string, object>("action", logType));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", ((DateTime)start.Value).ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", ((DateTime)end.Value).ToCakeMailString()));
			if (listMemberId.HasValue) parameters.Add(new KeyValuePair<string, object>("record_id", listMemberId.Value));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<LogItem>(path, parameters, "logs");
		}

		public long GetTriggerLogsCount(string userKey, int triggerId, string logType = null, int? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int? clientId = null)
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
			if (logType != null) parameters.Add(new KeyValuePair<string, object>("action", logType));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", ((DateTime)start.Value).ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", ((DateTime)end.Value).ToCakeMailString()));
			if (listMemberId.HasValue) parameters.Add(new KeyValuePair<string, object>("record_id", listMemberId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		public IEnumerable<Link> GetTriggerLinks(string userKey, int triggerId, int limit = 0, int offset = 0, int? clientId = null)
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

		public long GetTriggerLinksCount(string userKey, int triggerId, int? clientId = null)
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

		public Link GeTriggerLink(string userKey, int linkId, int? clientId = null)
		{
			string path = "/Trigger/GetLinkInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("link_id", linkId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<Link>(path, parameters);
		}

		public IEnumerable<LogItem> GetTriggerLinksLogs(string userKey, int triggerId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			string path = "/Trigger/GetLog/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", ((DateTime)start.Value).ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", ((DateTime)end.Value).ToCakeMailString()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<LogItem>(path, parameters, "logs");
		}

		#endregion

		#region Methods related to TEMPLATES

		public int CreateTemplateCategory(string userKey, IDictionary<string, string> labels, bool isVisibleByDefault = true, bool templatesCanBeCopied = true, int? clientId = null)
		{
			string path = "/TemplateV2/CreateCategory/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
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

			return ExecuteObjectRequest<int>(path, parameters);
		}

		public bool DeleteTemplateCategory(string userKey, int categoryId, int? clientId = null)
		{
			string path = "/TemplateV2/DeleteCategory/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public TemplateCategory GetTemplateCategory(string userKey, int categoryId, int? clientId = null)
		{
			var path = "/TemplateV2/GetCategory/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<TemplateCategory>(path, parameters);
		}

		public IEnumerable<TemplateCategory> GetTemplateCategories(string userKey, int limit = 0, int offset = 0, int? clientId = null)
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

		public long GetTemplateCategoriesCount(string userKey, int? clientId = null)
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

		public bool UpdateTemplateCategory(string userKey, int categoryId, IDictionary<string, string> labels, bool isVisibleByDefault = true, bool templatesCanBeCopied = true, int? clientId = null)
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

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public IEnumerable<string> GetTemplateCategoryVisibility(string userKey, int categoryId, int limit = 0, int offset = 0, int? clientId = null)
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

			return ExecuteArrayRequest<string>(path, parameters, "categories");
		}

		public long SetTemplateCategoryVisibility(string userKey, int categoryId, IDictionary<int, bool> clientVisibility, int? clientId = null)
		{
			var path = "/TemplateV2/SetCategoryVisibility/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
			};
			if (clientVisibility != null)
			{
				foreach (var visibility in clientVisibility)
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("client[{0}][visible]", visibility.Key), visibility.Value ? "true" : "false"));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		public int CreateTemplate(string userKey, IDictionary<string, string> labels, string content = null, int? categoryId = null, int? clientId = null)
		{
			string path = "/TemplateV2/CreateTemplate/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey)
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

			return ExecuteObjectRequest<int>(path, parameters);
		}

		public bool DeleteTemplate(string userKey, int templateId, int? clientId = null)
		{
			string path = "/TemplateV2/DeleteTemplate/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("template_id", templateId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public TemplateCategory GetTemplate(string userKey, int templateId, int? clientId = null)
		{
			var path = "/TemplateV2/GetTemplate/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("template_id", templateId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<TemplateCategory>(path, parameters);
		}

		public IEnumerable<TemplateCategory> GetTemplates(string userKey, int? categoryId = null, int limit = 0, int offset = 0, int? clientId = null)
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

			return ExecuteArrayRequest<TemplateCategory>(path, parameters, "templates");
		}

		public long GetTemplatesCount(string userKey, int? categoryId = null, int? clientId = null)
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

		public bool UpdateTemplate(string userKey, int templateId, IDictionary<string, string> labels, string content = null, int? categoryId = null, int? clientId = null)
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

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		#endregion

		#region Methods related to USERS

		public int CreateUser(string userKey, string email, string firstName, string lastName, string title, string officePhone, string mobilePhone, string language, string password, int timezoneId = 542, int? clientId = null)
		{
			string path = "/User/Create/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("email", email),
				new KeyValuePair<string, object>("password", password),
				new KeyValuePair<string, object>("password_confirmation", password)
			};
			if (firstName != null) parameters.Add(new KeyValuePair<string, object>("first_name", firstName));
			if (lastName != null) parameters.Add(new KeyValuePair<string, object>("last_name", lastName));
			if (title != null) parameters.Add(new KeyValuePair<string, object>("title", title));
			if (officePhone != null) parameters.Add(new KeyValuePair<string, object>("office_phone", officePhone));
			if (mobilePhone != null) parameters.Add(new KeyValuePair<string, object>("mobile_phone", mobilePhone));
			if (language != null) parameters.Add(new KeyValuePair<string, object>("language", language));
			if (timezoneId > 0) parameters.Add(new KeyValuePair<string, object>("timezone_id", timezoneId));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			// When a new user is created, the json payload only contains the 'id'
			var user = ExecuteObjectRequest<User>(path, parameters);
			return user.Id;
		}

		public bool DeactivateUser(string userKey, int userId, int? clientId = null)
		{
			return UpdateUser(userKey, userId, "suspended", null, null, null, null, null, null, null, null, null, clientId);
		}

		public bool DeleteUser(string userKey, int userId, int? clientId = null)
		{
			return UpdateUser(userKey, userId, "deleted", null, null, null, null, null, null, null, null, null, clientId);
		}

		public User GetUser(string userKey, int userId, int? clientId = null)
		{
			var path = "/User/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("user_id", userId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<User>(path, parameters);
		}

		public IEnumerable<User> GetUsers(string userKey, string status = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			var path = "/User/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteArrayRequest<User>(path, parameters, "users");
		}

		public long GetUsersCount(string userKey, string status = null, int? clientId = null)
		{
			var path = "/User/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCountRequest(path, parameters);
		}

		public bool UpdateUser(string userKey, int userId, string status, string email, string firstName, string lastName, string title, string officePhone, string mobilePhone, string language, string timezoneId, string password, int? clientId = null)
		{
			string path = "/User/SetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("user_id", userId)
			};
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (email != null) parameters.Add(new KeyValuePair<string, object>("email", email));
			if (firstName != null) parameters.Add(new KeyValuePair<string, object>("first_name", firstName));
			if (lastName != null) parameters.Add(new KeyValuePair<string, object>("last_name", lastName));
			if (title != null) parameters.Add(new KeyValuePair<string, object>("title", title));
			if (officePhone != null) parameters.Add(new KeyValuePair<string, object>("office_phone", officePhone));
			if (mobilePhone != null) parameters.Add(new KeyValuePair<string, object>("mobile_phone", mobilePhone));
			if (language != null) parameters.Add(new KeyValuePair<string, object>("language", language));
			if (timezoneId != null) parameters.Add(new KeyValuePair<string, object>("timezoneId", timezoneId));
			if (password != null)
			{
				parameters.Add(new KeyValuePair<string, object>("password", password));
				parameters.Add(new KeyValuePair<string, object>("password_confirmation", password));
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<bool>(path, parameters);
		}

		public LoginInfo Login(string userName, string password, int? clientId = null)
		{
			var path = "/User/Login/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("email", userName),
				new KeyValuePair<string, object>("password", password)

			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteObjectRequest<LoginInfo>(path, parameters);
		}

		#endregion

		#region Private Methods

		private static string GetVersion()
		{
			try
			{
				// The following may throw 'System.Security.Permissions.FileIOPermission' under some circumpstances
				//var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;

				// Here's an alternative suggected by Phil Haack: http://haacked.com/archive/2010/11/04/assembly-location-and-medium-trust.aspx
				var assemblyVersion = new AssemblyName(typeof(CakeMailRestClient).Assembly.FullName).Version;
				var version = string.Format("{0}.{1}.{2}.{3}", assemblyVersion.Major, assemblyVersion.Minor, assemblyVersion.Build, assemblyVersion.Revision);

				return version;
			}
			catch
			{
				return "0.0.0.0";
			}
		}

		private void ExecuteRequest(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters, JsonSerializer jsonSerializer = null)
		{
			var response = ExecuteRequest(urlPath, parameters);
			ParseCakeMailResponse(response);
		}

		private T ExecuteObjectRequest<T>(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters, JsonSerializer jsonSerializer = null)
		{
			var response = ExecuteRequest(urlPath, parameters);
			var data = ParseCakeMailResponse(response);

			var dataObject = (jsonSerializer == null ? data.ToObject<T>() : data.ToObject<T>(jsonSerializer));
			return dataObject;
		}

		private IEnumerable<T> ExecuteArrayRequest<T>(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters, string arrayPropertyName)
		{
			var response = ExecuteRequest(urlPath, parameters);
			var data = ParseCakeMailResponse(response);

			if (data is JArray)
			{
				return (data as JArray).ToObject<T[]>();
			}
			else if (data is JObject)
			{
				var property = (data as JObject).Properties().SingleOrDefault(p => p.Name.Equals(arrayPropertyName));
				if (property == null) return Enumerable.Empty<T>();
				else if (property.Value is JArray) return (property.Value as JArray).ToObject<T[]>();
				else if (property.Value is JObject) return new T[] { (property.Value as JObject).ToObject<T>() };
				else throw new CakeMailException(string.Format("Json contains CakeMail array {0} but the data is not valid", arrayPropertyName));
			}
			else
			{
				throw new CakeMailException(string.Format("Json does not contain CakeMail array {0}", arrayPropertyName));
			}
		}

		private long ExecuteCountRequest(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters)
		{
			var response = ExecuteRequest(urlPath, parameters);
			var data = ParseCakeMailResponse(response);

			if (data is JObject)
			{
				var property = (data as JObject).Properties().SingleOrDefault(p => p.Name.Equals("count"));
				if (property == null) throw new CakeMailException("Json does not contain 'count' property");
				else if (property.Value is JValue) return (property.Value as JValue).ToObject<long>();
				else throw new CakeMailException("Json contains 'count' propertyt but the data is not valid");
			}
			else
			{
				throw new CakeMailException("Json does not contain 'count' property");
			}
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
				Debug.WriteLine("============================\r\n{0}\r\n{1}\r\n{2}\r\n{3}\r\n============================", debugRequestMsg, debugHeadersMsg, debugParametersMsg, debugResponseMsg);
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
			catch (SerializationException ex)
			{
				throw new CakeMailException(string.Format("Received a 200 response but could not decode it as JSON: {0}", response.Content), ex);
			}
		}

		#endregion
	}
}