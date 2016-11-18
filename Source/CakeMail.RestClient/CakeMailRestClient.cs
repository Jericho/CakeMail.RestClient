using CakeMail.RestClient.Exceptions;
using CakeMail.RestClient.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

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

		/// <summary>
		/// The <see cref="Campaigns">Campaigns</see> resource
		/// </summary>
		public Campaigns Campaigns { get; private set; }

		/// <summary>
		/// The <see cref="Clients">Clients</see> resource
		/// </summary>
		public Clients Clients { get; private set; }

		/// <summary>
		/// The <see cref="Countries">Countries</see> resource
		/// </summary>
		public Countries Countries { get; private set; }

		/// <summary>
		/// The <see cref="Permissions">Permissions</see> resource
		/// </summary>
		public Permissions Permissions { get; private set; }

		/// <summary>
		/// The <see cref="Lists">Lists</see> resource
		/// </summary>
		public Lists Lists { get; private set; }

		/// <summary>
		/// The <see cref="Timezones">Timezones</see> resource
		/// </summary>
		public Timezones Timezones { get; private set; }

		/// <summary>
		/// The <see cref="Mailings">Mailings</see> resource
		/// </summary>
		public Mailings Mailings { get; private set; }

		/// <summary>
		/// The <see cref="Relays">Relays</see> resource
		/// </summary>
		public Relays Relays { get; private set; }

		/// <summary>
		/// The <see cref="Segments">Segments</see> resource
		/// </summary>
		public Segments Segments { get; private set; }

		/// <summary>
		/// The <see cref="Users">Users</see> resource
		/// </summary>
		public Users Users { get; private set; }

		/// <summary>
		/// The <see cref="SuppressionLists">SuppressionLists</see> resource
		/// </summary>
		public SuppressionLists SuppressionLists { get; private set; }

		/// <summary>
		/// The <see cref="Templates">Templates</see> resource
		/// </summary>
		public Templates Templates { get; private set; }

		/// <summary>
		/// The <see cref="Triggers">Triggers</see> resource
		/// </summary>
		public Triggers Triggers { get; private set; }

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

			InitializeResources();
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
				UserAgent = string.Format("CakeMail .NET REST Client;{0}", _version),
				Proxy = webProxy
			};

			InitializeResources();
		}

		#endregion

		#region Internal Methods

		internal Task<long> ExecuteCountRequestAsync(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ExecuteRequestAsync<long>(urlPath, parameters, "count", cancellationToken);
		}

		internal Task<IEnumerable<T>> ExecuteArrayRequestAsync<T>(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters, string propertyName = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ExecuteRequestAsync<IEnumerable<T>>(urlPath, parameters, propertyName, cancellationToken);
		}

		internal async Task<T> ExecuteRequestAsync<T>(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters, string propertyName = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			// Execute the API call
			var response = await ExecuteRequestAsync(urlPath, parameters, cancellationToken).ConfigureAwait(false);

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

		internal async Task<IRestResponse> ExecuteRequestAsync(string urlPath, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken = default(CancellationToken))
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

			var response = await _client.ExecuteTaskAsync(request, cancellationToken).ConfigureAwait(false);
			ValidateResponse(request, response);

#if DEBUG
			var debugRequestMsg = string.Format("Request sent to CakeMail: {0}/{1}", _client.BaseUrl.ToString().TrimEnd('/'), urlPath.TrimStart('/'));
			var debugHeadersMsg = string.Format("Request headers: {0}", string.Join("&", request.Parameters.Where(p => p.Type == ParameterType.HttpHeader).Select(p => string.Concat(p.Name, "=", p.Value))));
			var debugParametersMsg = string.Format("Request parameters: {0}", string.Join("&", request.Parameters.Where(p => p.Type != ParameterType.HttpHeader).Select(p => string.Concat(p.Name, "=", p.Value))));
			var debugResponseMsg = string.Format("Response received from CakeMail: {0}", response.Content);
			Debug.WriteLine("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}\r\n{0}", new string('=', 25), debugRequestMsg, debugHeadersMsg, debugParametersMsg, debugResponseMsg);
#endif

			return response;
		}

		#endregion

		#region Private Methods

		private void InitializeResources()
		{
			this.Campaigns = new Campaigns(this);
			this.Clients = new Clients(this);
			this.Countries = new Countries(this);
			this.Permissions = new Permissions(this);
			this.Lists = new Lists(this);
			this.Timezones = new Timezones(this);
			this.Mailings = new Mailings(this);
			this.Relays = new Relays(this);
			this.Segments = new Segments(this);
			this.Users = new Users(this);
			this.SuppressionLists = new SuppressionLists(this);
			this.Templates = new Templates(this);
			this.Triggers = new Triggers(this);
		}

		private static string GetVersion()
		{
			try
			{
				// The following may throw 'System.Security.Permissions.FileIOPermission' under some circumpstances
				//var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;

				// Here's an alternative suggested by Phil Haack: http://haacked.com/archive/2010/11/04/assembly-location-and-medium-trust.aspx
				var assemblyVersion = new AssemblyName(typeof(CakeMailRestClient).Assembly.FullName).Version;
				var version = string.Format("{0}.{1}.{2}", assemblyVersion.Major, assemblyVersion.Minor, assemblyVersion.Build);

				return version;
			}
			catch
			{
				return "0.0.0";
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

		private void ValidateResponse(IRestRequest request, IRestResponse response)
		{
			var responseUri = GetResponseUri(request, response);
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
				Validate200Response(request, response);
			}
			else if (statusCode >= 400 && statusCode < 500)
			{
				Validate400Response(request, response);
			}
			else if (statusCode >= 500 && statusCode < 600)
			{
				Validate500Response(request, response);
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

		private void Validate200Response(IRestRequest request, IRestResponse response)
		{
			var responseUri = GetResponseUri(request, response);
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
		}

		private void Validate400Response(IRestRequest request, IRestResponse response)
		{
			var responseUri = GetResponseUri(request, response);
			if (string.IsNullOrEmpty(response.Content))
			{
				var missingBodyMessage = string.Format("Received a {0} error from {1} with no body", response.StatusCode, request.Resource);
				throw new HttpException(missingBodyMessage, response.StatusCode, responseUri);
			}

			var errorMessage = string.Format("Received a {0} error from {1} with the following content: {2}", response.StatusCode, request.Resource, response.Content);
			throw new HttpException(errorMessage, response.StatusCode, responseUri);
		}

		private void Validate500Response(IRestRequest request, IRestResponse response)
		{
			var responseUri = GetResponseUri(request, response);
			var errorMessage = string.Format("Received a server ({0}) error from {1}", (int)response.StatusCode, request.Resource);
			throw new HttpException(errorMessage, response.StatusCode, responseUri);
		}

		private Uri GetResponseUri(IRestRequest request, IRestResponse response)
		{
			var responseUri = response.ResponseUri ?? new Uri(string.Format("{0}/{1}", _client.BaseUrl.ToString().TrimEnd('/'), request.Resource.TrimStart('/')));
			return responseUri;
		}

		#endregion
	}
}
