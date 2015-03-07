using CakeMail.RestClient.Exceptions;
using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;

namespace CakeMail.RestClient
{
	/// <summary>
	/// Core class for using the CakeMail Api
	/// </summary>
	public class Client : IClient
	{
		#region Fields

		private static readonly string _version = Client.GetVersion();
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

		/// <summary>
		/// Initializes a new instance of the <see cref="Client"/> class.
		/// </summary>
		/// <param name="apiKey">The API Key received from CakeMail</param>
		/// <param name="host">The host where the API is hosted. The default is api.wbsrvc.com</param>
		/// <param name="timeout">Timeout in milliseconds for connection to web service. The default is 3000.</param>
		public Client(string apiKey, string host = "api.wbsrvc.com", int timeout = 3000)
		{
			this.ApiKey = apiKey;

			_client = Client.CreateClient(host, timeout);
		}

		#endregion

		#region Methods related to CAMPAIGNS

		public int CreateCampaign(string userKey, string name, int? clientId = null)
		{
			string path = "/Campaign/Create/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("name", name),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return Execute<int>(path, parameters);
		}

		public bool DeleteCampaign(string userKey, int campaignId, int? clientId = null)
		{
			string path = "/Campaign/Delete/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("campaign_id", campaignId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return Execute<bool>(path, parameters);
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

			return Execute<Campaign>(path, parameters);
		}

		public IEnumerable<Campaign> GetCampaigns(string userKey, string status, string name = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			var path = "/Campaign/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status != null) parameters.Add(new KeyValuePair<string, object>("status", status));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			var items = ExecuteArray<Campaign>(path, parameters, "campaigns");
			return (items ?? Enumerable.Empty<Campaign>());
		}

		public long GetCampaignsCount(string userKey, string status, string name = null, int? clientId = null)
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

			return ExecuteCount(path, parameters);
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

			return Execute<bool>(path, parameters);
		}

		#endregion

		#region Methods related to COUNTRIES

		public IEnumerable<Country> GetCountries()
		{
			var path = "/Country/GetList/";

			var parameters = new List<KeyValuePair<string, object>>();

			var items = ExecuteArray<Country>(path, parameters, "countries");
			return (items ?? Enumerable.Empty<Country>());
		}

		public IEnumerable<Province> GetProvinces(string countryId)
		{
			var path = "/Country/GetProvinces/";

			var parameters = new List<KeyValuePair<string, object>>();
			parameters.Add(new KeyValuePair<string, object>("country_id", countryId));

			var items = ExecuteArray<Province>(path, parameters, "provinces");
			return (items ?? Enumerable.Empty<Province>());
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

			return Execute<bool>(path, parameters);
		}

		public IEnumerable<RelayLog> GetRelaySentLogs(string userKey, int? trackingId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			return GetRelayLogs<RelayLog>(userKey, "sent", "sent_logs", trackingId, start, end, limit, offset, clientId);
		}

		public IEnumerable<RelayOpenLog> GetRelayOpenLogs(string userKey, int? trackingId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			return GetRelayLogs<RelayOpenLog>(userKey, "open", "open_logs", trackingId, start, end, limit, offset, clientId);
		}

		public IEnumerable<RelayClickLog> GetRelayClickLogs(string userKey, int? trackingId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			return GetRelayLogs<RelayClickLog>(userKey, "clickthru", "click_logs", trackingId, start, end, limit, offset, clientId);
		}

		public IEnumerable<RelayBounceLog> GetRelayBounceLogs(string userKey, int? trackingId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			return GetRelayLogs<RelayBounceLog>(userKey, "bounce", "bounce_logs", trackingId, start, end, limit, offset, clientId);
		}

		private IEnumerable<T> GetRelayLogs<T>(string userKey, string logType, string arrayPropertyName, int? trackingId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null) where T : RelayLog, new()
		{
			string path = "/Relay/GetLogs/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("log_type", logType)
			};
			if (trackingId.HasValue) parameters.Add(new KeyValuePair<string, object>("tracking_id", trackingId.Value));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			var items = ExecuteArray<T>(path, parameters, arrayPropertyName);
			return (items ?? Enumerable.Empty<T>());
		}

		#endregion

		#region Methods related to TIMEZONES

		public IEnumerable<Timezone> GetTimezones()
		{
			var path = "/Client/GetTimezones/";

			var parameters = new List<KeyValuePair<string, object>>();

			var items = ExecuteArray<Timezone>(path, parameters, "timezones");
			return (items ?? Enumerable.Empty<Timezone>());
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
			var user = Execute<User>(path, parameters);
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

			return Execute<User>(path, parameters);
		}

		public IEnumerable<User> GetUsers(string userKey, string status, int limit = 0, int offset = 0, int? clientId = null)
		{
			var path = "/User/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("status", status),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			var items = ExecuteArray<User>(path, parameters, "users");
			return (items ?? Enumerable.Empty<User>());
		}

		public long GetUsersCount(string userKey, string status, int? clientId = null)
		{
			var path = "/User/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("status", status),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return ExecuteCount(path, parameters);
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

			return Execute<bool>(path, parameters);
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

			return Execute<LoginInfo>(path, parameters);
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
				var assemblyVersion = new AssemblyName(typeof(Client).Assembly.FullName).Version;
				var version = string.Format("{0}.{1}.{2}.{3}", assemblyVersion.Major, assemblyVersion.Minor, assemblyVersion.Build, assemblyVersion.Revision);

				return version;
			}
			catch
			{
				return "0.0.0.0";
			}
		}

		private static IRestClient CreateClient(string host, int timeout)
		{
			var restClient = new RestSharp.RestClient("https://" + host);
			restClient.Timeout = timeout;
			restClient.UserAgent = String.Format("CakeMail .NET REST Client {0}", _version);

			return restClient;
		}

		private T Execute<T>(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters) where T : new()
		{
			var response = ExecuteRequest(urlPath, parameters);
			var data = ParseCakeMailResponse(response);

			var dataObject = data.ToObject<T>();
			return dataObject;
		}

		private T[] ExecuteArray<T>(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters, string arrayPropertyName) where T : new()
		{
			var response = ExecuteRequest(urlPath, parameters);
			var data = ParseCakeMailResponse(response);

			var serializer = new JsonSerializer();
			serializer.Converters.Add(new CakeMailArrayConverter(arrayPropertyName));
			var dataObject = data.ToObject<T[]>(serializer);
			return dataObject;
		}

		private long ExecuteCount(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters)
		{
			var response = ExecuteRequest(urlPath, parameters);
			var data = ParseCakeMailResponse(response);

			var serializer = new JsonSerializer();
			serializer.Converters.Add(new CakeMailCountOfRecordsConverter());
			var dataObject = data.ToObject<long>(serializer);
			return dataObject;
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
			if (response.ResponseStatus == ResponseStatus.Error)
			{
				throw new HttpException(string.Format("Error received while making request: {0}", response.ErrorMessage), response.StatusCode, response.ResponseUri, response.ErrorException);
			}

			var statusCode = (int)response.StatusCode;
			if (statusCode == 200)
			{
				if (string.IsNullOrEmpty(response.Content))
					throw new HttpException(string.Format("Received a 200 response for {0} but there was no message body.", response.ResponseUri), response.StatusCode, response.ResponseUri);

				if (response.ContentType == null || !response.ContentType.Contains("json"))
					throw new CakeMailException(string.Format("Received a 200 response for {0} but it does not appear to be JSON:\n", response.ContentType));

				// Request was successful
				return response;
			}
			else if (statusCode >= 400 && statusCode < 500)
			{
				if (string.IsNullOrEmpty(response.Content))
				{
					throw new HttpException(string.Format("Received a {0} error for {1} with no body", response.StatusCode, response.ResponseUri), response.StatusCode, response.ResponseUri);
				}

				throw new HttpException(string.Format("Received a {0} error for {1} with the following content: {2}", response.StatusCode, response.ResponseUri, response.Content), response.StatusCode, response.ResponseUri);
			}
			else if (statusCode >= 500 && statusCode < 600)
			{
				throw new HttpException(string.Format("Received a server ({0}) error for {1}", (int)response.StatusCode, response.ResponseUri), response.StatusCode, response.ResponseUri);
			}

			var errorMessage = string.Format("Received an unexpected response for {0} (status code: {1})", response.ResponseUri, (int)response.StatusCode);
			throw new HttpException(errorMessage, response.StatusCode, response.ResponseUri);
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