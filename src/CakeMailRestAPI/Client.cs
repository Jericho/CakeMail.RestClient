using CakeMailRestAPI.Exceptions;
using CakeMailRestAPI.Models;
using CakeMailRestAPI.Utilities;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;

namespace CakeMailRestAPI
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

		/// <summary>
		/// Create a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">The name of the campaign.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The <see cref="Campaign"/></returns>
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

		/// <summary>
		/// Get the information about a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The <see cref="User"/></returns>
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

		/// <summary>
		/// Get the list of campaigns.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the campaign status. Possible values: 'ongoing', 'closed'.</param>
		/// <param name="name">Filter using the campaign name.</param>
		/// <param name="sortBy">Sort resulting campaigns. Possible values: 'created_on', 'name'.</param>
		/// <param name="limit">Limit the number of resulting campaigns.</param>
		/// <param name="offset">Offset the beginning of resulting campaigns.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>An enumeration of <see cref="User">campaigns</see>.</returns>
		public IEnumerable<Campaign> GetCampaigns(string userKey, string status, string name = null, int limit = 0, int offset = 0, int? clientId = null)
		{
			var path = "/Campaign/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("status", status),
				new KeyValuePair<string, object>("count", "false")
			};
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return Execute<ArrayOfCampaigns>(path, parameters).Campaigns;
		}

		#endregion

		#region Methods related to COUNTRIES

		/// <summary>
		/// Get the list of countries.
		/// </summary>
		/// <returns>An enumeration of <see cref="Country">countries</see>.</returns>
		public IEnumerable<Country> GetCountries()
		{
			var path = "/Country/GetList/";

			var parameters = new List<KeyValuePair<string, object>>();

			return Execute<ArrayOfCountries>(path, parameters).Countries;
		}

		/// <summary>
		/// Get the list of provinces for a given country.
		/// </summary>
		/// <param name="countryId">ID if the country.</param>
		/// <returns>An enumeration of <see cref="Province">provinces</see>.</returns>
		public IEnumerable<Province> GetProvinces(string countryId)
		{
			var path = "/Country/GetProvinces/";

			var parameters = new List<KeyValuePair<string, object>>();
			parameters.Add(new KeyValuePair<string, object>("country_id", countryId));

			return Execute<ArrayOfProvinces>(path, parameters).Provinces;
		}

		#endregion

		#region Methods related to RELAYS

		public bool SendRelay(string userKey, string email, string senderEmail, string senderName, string html, string text, string subject, string encoding, bool trackOpens, bool trackClicksInHtml, bool trackClicksInText, string trackingId, int? clientId = null)
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
			if (trackingId != null) parameters.Add(new KeyValuePair<string, object>("tracking_id", trackingId));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return Execute<bool>(path, parameters);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clientId"></param>
		/// <param name="logType"></param>
		/// <param name="trackingId">Per my discussion with Vincent on 8/20/2013, omiting the tracking id will return all relay logs for a given customer</param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="limit"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		/// <remarks>If start time and end time parameters are null then only current 24 hour period is retrieved</remarks>
		//public IEnumerable<RelayLog> GetRelayLogs(IDictionary<string, string> connectionSettings, int clientId, string logType, int? trackingId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0)
		//{
		//	string path = "/Relay/GetLogs/";

		//	var parameters = new List<KeyValuePair<string, object>>()
		//	{
		//		new KeyValuePair<string, object>("user_key", userKey),
		//		new KeyValuePair<string, object>("email", email),
		//		new KeyValuePair<string, object>("password", password),
		//		new KeyValuePair<string, object>("password_confirmation", password)
		//	};
		//	if (firstName != null) parameters.Add(new KeyValuePair<string, object>("first_name", firstName));
		//	if (lastName != null) parameters.Add(new KeyValuePair<string, object>("last_name", lastName));
		//	if (title != null) parameters.Add(new KeyValuePair<string, object>("title", title));
		//	if (officePhone != null) parameters.Add(new KeyValuePair<string, object>("office_phone", officePhone));
		//	if (mobilePhone != null) parameters.Add(new KeyValuePair<string, object>("mobile_phone", mobilePhone));
		//	if (language != null) parameters.Add(new KeyValuePair<string, object>("language", language));
		//	if (timezoneId != null) parameters.Add(new KeyValuePair<string, object>("timezoneId", timezoneId));
		//	if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

		//	Task<IRestResponse> post = this.PostAsync(path, parameters);
		//	return
		//		post.ContinueWith(
		//			p => { return JSON.Parse<bool>(p.Result.Content); },
		//			TaskContinuationOptions.ExecuteSynchronously).Result;
		//}

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
		public int CreateUser(string userKey, string email, string firstName, string lastName, string title, string officePhone, string mobilePhone, string language, string timezoneId, string password, int? clientId = null)
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
			if (timezoneId != null) parameters.Add(new KeyValuePair<string, object>("timezoneId", timezoneId));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return Execute<int>(path, parameters);
		}

		/// <summary>
		/// Get the information of a user.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The <see cref="User"/></returns>
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

		/// <summary>
		/// Get the list of users.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the user status. Possible values: 'active', 'suspended'</param>
		/// <param name="limit">Limit the number of resulting users.</param>
		/// <param name="offset">Offset the beginning of resulting users.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>An enumeration of <see cref="User">Users</see>.</returns>
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

			return Execute<ArrayOfUsers>(path, parameters).Users;
		}

		/// <summary>
		/// Get the list of users.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the user status. Possible values: 'active', 'suspended'</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The count of users.</returns>
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

			return Execute<CountOfRecords>(path, parameters).Count;
		}

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
		public void UpdateUser(string userKey, int userId, string status, string email, string firstName, string lastName, string title, string officePhone, string mobilePhone, string language, string timezoneId, string password, int? clientId = null)
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

			Execute<bool>(path, parameters);
		}

		/// <summary>
		/// Check the login of a user.
		/// </summary>
		/// <param name="userName">Email address of the user.</param>
		/// <param name="password">Password of the user.</param>
		/// <param name="clientId">ID of the client to check for the login.</param>
		/// <returns>The <see cref="LoginInfo"/></returns>
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
			var restClient = new RestClient("https://" + host);
			restClient.AddHandler("application/vnd.maxmind.com-insights+json", new JsonDeserializer());
			restClient.AddHandler("application/vnd.maxmind.com-country+json", new JsonDeserializer());
			restClient.AddHandler("application/vnd.maxmind.com-city+json", new JsonDeserializer());
			restClient.Timeout = timeout;
			restClient.UserAgent = String.Format("CakeMail .NET Client {0}", _version);

			return restClient;
		}

		private T Execute<T>(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters) where T : new()
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
				if (response.ContentLength <= 0)
					throw new HttpException(string.Format("Received a 200 response for {0} but there was no message body.", response.ResponseUri), response.StatusCode, response.ResponseUri);

				if (response.ContentType == null || !response.ContentType.Contains("json"))
					throw new CakeMailException(string.Format("Received a 200 response for {0} but it does not appear to be JSON:\n", response.ContentType));

				return ProcessResponse<T>(response);
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

		private T ProcessResponse<T>(IRestResponse response) where T : new()
		{
			try
			{
				/* Typical responses from the CakeMail API look like this:
				 *	{
				 *		"status" : "success",
				 *		"data" : { ... data for the API call ... }
				 *	}
				 *	
				 *  in case of an error, it will look like this:
				 *	{
				 *		"status" : "failed",
				 *		"data" : "An error has occured"
				 *	}
				 */
				var cakeResponse = JObject.Parse(response.Content);
				var status = cakeResponse["status"].ToString();
				var data = cakeResponse["data"].ToString();

				if (status != "success") throw new CakeMailException(data);

				var dataObject = JSON.Parse<T>(data);
				return dataObject;
			}
			catch (SerializationException ex)
			{
				throw new CakeMailException(string.Format("Received a 200 response but could not decode it as JSON: {0}", response.Content), ex);
			}
		}

		#endregion
	}
}