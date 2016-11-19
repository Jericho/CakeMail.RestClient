using CakeMail.RestClient.Exceptions;
using CakeMail.RestClient.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CakeMail.RestClient.Utilities;

namespace CakeMail.RestClient
{
	/// <summary>
	/// Core class for using the CakeMail Api
	/// </summary>
	public class CakeMailRestClient : ICakeMailRestClient
	{
		#region FIELDS

		private const string MEDIA_TYPE = "application/json";

		private readonly bool _mustDisposeHttpClient;

		private HttpClient _httpClient;

		private enum Methods
		{
			GET, PUT, POST, PATCH, DELETE
		}

		#endregion

		#region PROPERTIES

		/// <summary>
		/// The API key provided by CakeMail
		/// </summary>
		public string ApiKey { get; private set; }

		/// <summary>
		/// The user agent
		/// </summary>
		public string UserAgent { get; private set; }

		/// <summary>
		/// The timeout
		/// </summary>
		public int Timeout { get; private set; }

		/// <summary>
		/// The URL where all API requests are sent
		/// </summary>
		public Uri BaseUrl { get; private set; }

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

		public string Version { get; private set; }

		#endregion

		#region CONSTRUCTORS AND DESTRUCTORS

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailRestClient"/> class.
		/// </summary>
		/// <param name="apiKey">The API Key received from CakeMail</param>
		public CakeMailRestClient(string apiKey) : this(apiKey, httpClient: (HttpClient)null) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailRestClient"/> class.
		/// </summary>
		/// <param name="apiKey">The API Key received from CakeMail</param>
		/// <param name="proxy">Allows you to specify a proxy</param>
		public CakeMailRestClient(string apiKey, IWebProxy proxy = null)
			: this(apiKey, httpClient: new HttpClient(new HttpClientHandler { Proxy = proxy, UseProxy = proxy != null }))
		{
			_mustDisposeHttpClient = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailRestClient"/> class.
		/// </summary>
		/// <param name="apiKey">The API Key received from CakeMail</param>
		/// <param name="host">The host where the API is hosted. The default is api.wbsrvc.com</param>
		/// <param name="timeout">Timeout in milliseconds for connection to web service. The default is 5000.</param>
		/// <param name="httpClient">Allows you to inject your own HttpClient. This is useful, for example, to setup the HtppClient with a proxy</param>
		public CakeMailRestClient(string apiKey, string host = "api.wbsrvc.com", int timeout = 5000, HttpClient httpClient = null)
		{
			ApiKey = apiKey;
			BaseUrl = new Uri(string.Format("https://{0}", host));
			Timeout = timeout;

			Version = typeof(CakeMailRestClient).GetTypeInfo().Assembly.GetName().Version.ToString();
			UserAgent = string.Format("CakeMail .NET REST Client;{0}", Version);

			Campaigns = new Campaigns(this);
			Clients = new Clients(this);
			Countries = new Countries(this);
			Permissions = new Permissions(this);
			Lists = new Lists(this);
			Timezones = new Timezones(this);
			Mailings = new Mailings(this);
			Relays = new Relays(this);
			Segments = new Segments(this);
			Users = new Users(this);
			SuppressionLists = new SuppressionLists(this);
			Templates = new Templates(this);
			Triggers = new Triggers(this);

			_mustDisposeHttpClient = httpClient == null;
			_httpClient = httpClient ?? new HttpClient();
			_httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout);
			_httpClient.BaseAddress = BaseUrl;
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE));
			_httpClient.DefaultRequestHeaders.Add("apikey", ApiKey);
			_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", UserAgent);
		}

		~CakeMailRestClient()
		{
			// The object went out of scope and finalized is called.
			// Call 'Dispose' to release unmanaged resources
			// Managed resources will be released when GC runs the next time.
			Dispose(false);
		}

		#endregion

		#region PUBLIC METHODS

		public void Dispose()
		{
			// Call 'Dispose' to release resources
			Dispose(true);

			// Tell the GC that we have done the cleanup and there is nothing left for the Finalizer to do
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				ReleaseManagedResources();
			}
			else
			{
				// The object went out of scope and the Finalizer has been called.
				// The GC will take care of releasing managed resources, therefore there is nothing to do here.
			}

			ReleaseUnmanagedResources();
		}

		#endregion

		#region INTERNAL METHODS

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

			// Make sure response indicates success
			response.EnsureSuccess();

			// Parse the response
			var data = await ParseCakeMailResponseAsync(response).ConfigureAwait(false);

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

		internal async Task<HttpResponseMessage> ExecuteRequestAsync(string endpoint, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken = default(CancellationToken))
		{
			var paramsWithValue = parameters.Where(p => p.Value != null).Select(p => string.Concat(Uri.EscapeDataString(p.Key), "=", Uri.EscapeDataString((string)p.Value)));
			var paramsWithoutValue = parameters.Where(p => p.Value == null).Select(p => string.Concat(Uri.EscapeDataString(p.Key), "="));
			var allParams = paramsWithValue.Union(paramsWithoutValue).ToArray();
			var content = new StringContent(string.Join("&", paramsWithValue.Union(paramsWithoutValue)), Encoding.UTF8);

			var response = await RequestAsync(Methods.POST, endpoint, content, cancellationToken).ConfigureAwait(false);

#if DEBUG
			var debugRequestMsg = string.Format("Request sent to CakeMail: {0}/{1}", BaseUrl.ToString().TrimEnd('/'), endpoint.TrimStart('/'));
			var debugParametersMsg = string.Format("Request parameters: {0}", string.Join("&", parameters.Select(p => string.Concat(p.Key, "=", p.Value))));
			var debugResponseMsg = string.Format("Response received: {0}", await response.Content.ReadAsStringAsync().ConfigureAwait(false));
			Debug.WriteLine("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{0}", new string('=', 25), debugRequestMsg, debugParametersMsg, debugResponseMsg);
#endif

			return response;
		}

		#endregion

		#region PRIVATE METHODS

		/// <summary>
		///     Create a client that connects to the SendGrid Web API
		/// </summary>
		/// <param name="method">HTTP verb, case-insensitive</param>
		/// <param name="endpoint">Resource endpoint</param>
		/// <param name="content">A StringContent representing the content of the http request</param>
		/// <returns>An asyncronous task</returns>
		private async Task<HttpResponseMessage> RequestAsync(Methods method, string endpoint, StringContent content, CancellationToken cancellationToken = default(CancellationToken))
		{
			try
			{
				var methodAsString = string.Empty;
				switch (method)
				{
					case Methods.GET: methodAsString = "GET"; break;
					case Methods.PUT: methodAsString = "PUT"; break;
					case Methods.POST: methodAsString = "POST"; break;
					case Methods.PATCH: methodAsString = "PATCH"; break;
					case Methods.DELETE: methodAsString = "DELETE"; break;
					default:
						var message = "{\"errors\":[{\"message\":\"Bad method call, supported methods are GET, PUT, POST, PATCH and DELETE\"}]}";
						return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)
						{
							Content = new StringContent(message)
						};
				}

				var httpRequest = new HttpRequestMessage
				{
					Method = new HttpMethod(methodAsString),
					RequestUri = new Uri(string.Format("{0}{1}{2}", BaseUrl, endpoint.StartsWith("/", StringComparison.Ordinal) ? string.Empty : "/", endpoint)),
					Content = content
				};
				var response = await _httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
				return response;
			}
			catch (Exception ex)
			{
				var message = string.Format(".NET {0}, raw message: \n\n{1}", (ex is HttpRequestException) ? "HttpRequestException" : "Exception", ex.GetBaseException().Message);
				return new HttpResponseMessage(HttpStatusCode.BadRequest)
				{
					Content = new StringContent(message)
				};
			}
		}

		private async Task<JToken> ParseCakeMailResponseAsync(HttpResponseMessage response)
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
				var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
				var cakeResponse = JObject.Parse(responseContent);
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

		private void ReleaseManagedResources()
		{
			if (_httpClient != null && _mustDisposeHttpClient)
			{
				_httpClient.Dispose();
				_httpClient = null;
			}
		}

		private void ReleaseUnmanagedResources()
		{
			// We do not hold references to unmanaged resources
		}

		#endregion
	}
}
