using CakeMail.RestClient.Resources;
using CakeMail.RestClient.Utilities;
using Pathoschild.Http.Client;
using Pathoschild.Http.Client.Extensibility;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace CakeMail.RestClient
{
	/// <summary>
	/// Core class for using the CakeMail Api
	/// </summary>
	public class CakeMailRestClient : ICakeMailRestClient
	{
		#region FIELDS

		private const string DEFAULT_HOST = "api.wbsrvc.com";

		private readonly bool _mustDisposeHttpClient;

		private HttpClient _httpClient;
		private Pathoschild.Http.Client.IClient _fluentClient;

		#endregion

		#region PROPERTIES

		/// <summary>
		/// Gets the API key provided by CakeMail
		/// </summary>
		public string ApiKey { get; private set; }

		/// <summary>
		/// Gets the user agent
		/// </summary>
		public string UserAgent { get; private set; }

		/// <summary>
		/// Gets the URL where all API requests are sent
		/// </summary>
		public Uri BaseUrl { get; private set; }

		/// <summary>
		/// Gets the <see cref="Campaigns">Campaigns</see> resource
		/// </summary>
		public Campaigns Campaigns { get; private set; }

		/// <summary>
		/// Gets the <see cref="Clients">Clients</see> resource
		/// </summary>
		public Clients Clients { get; private set; }

		/// <summary>
		/// Gets the <see cref="Countries">Countries</see> resource
		/// </summary>
		public Countries Countries { get; private set; }

		/// <summary>
		/// Gets the <see cref="Permissions">Permissions</see> resource
		/// </summary>
		public Permissions Permissions { get; private set; }

		/// <summary>
		/// Gets the <see cref="Lists">Lists</see> resource
		/// </summary>
		public Lists Lists { get; private set; }

		/// <summary>
		/// Gets the <see cref="Timezones">Timezones</see> resource
		/// </summary>
		public Timezones Timezones { get; private set; }

		/// <summary>
		/// Gets the <see cref="Mailings">Mailings</see> resource
		/// </summary>
		public Mailings Mailings { get; private set; }

		/// <summary>
		/// Gets the <see cref="Relays">Relays</see> resource
		/// </summary>
		public Relays Relays { get; private set; }

		/// <summary>
		/// Gets the <see cref="Segments">Segments</see> resource
		/// </summary>
		public Segments Segments { get; private set; }

		/// <summary>
		/// Gets the <see cref="Users">Users</see> resource
		/// </summary>
		public Users Users { get; private set; }

		/// <summary>
		/// Gets the <see cref="SuppressionLists">SuppressionLists</see> resource
		/// </summary>
		public SuppressionLists SuppressionLists { get; private set; }

		/// <summary>
		/// Gets the <see cref="Templates">Templates</see> resource
		/// </summary>
		public Templates Templates { get; private set; }

		/// <summary>
		/// Gets the <see cref="Triggers">Triggers</see> resource
		/// </summary>
		public Triggers Triggers { get; private set; }

		/// <summary>
		/// Gets the Version.
		/// </summary>
		/// <value>
		/// Gets the version.
		/// </value>
		public string Version { get; private set; }

		#endregion

		#region CONSTRUCTORS AND DESTRUCTORS

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailRestClient"/> class.
		/// </summary>
		/// <param name="apiKey">The API Key received from CakeMail</param>
		public CakeMailRestClient(string apiKey)
			: this(apiKey, httpClient: (HttpClient)null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailRestClient"/> class.
		/// </summary>
		/// <param name="apiKey">The API Key received from CakeMail</param>
		/// <param name="proxy">Allows you to specify a proxy</param>
		public CakeMailRestClient(string apiKey, IWebProxy proxy)
			: this(apiKey, httpClient: new HttpClient(new HttpClientHandler { Proxy = proxy, UseProxy = proxy != null }))
		{
			_mustDisposeHttpClient = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailRestClient"/> class.
		/// </summary>
		/// <param name="apiKey">The API Key received from CakeMail</param>
		/// <param name="host">The host where the API is hosted. The default is api.wbsrvc.com</param>
		/// <param name="httpClient">Allows you to inject your own HttpClient. This is useful, for example, to setup the HtppClient with a proxy</param>
		public CakeMailRestClient(string apiKey, string host = DEFAULT_HOST, HttpClient httpClient = null)
		{
			_mustDisposeHttpClient = httpClient == null;
			_httpClient = httpClient;

			ApiKey = apiKey;
			BaseUrl = new Uri($"https://{host.TrimEnd('/')}/");
			Version = typeof(CakeMailRestClient).GetTypeInfo().Assembly.GetName().Version.ToString();
			UserAgent = $"CakeMail .NET REST Client/{Version} (+https://github.com/Jericho/CakeMail.RestClient)";

			_fluentClient = new FluentClient(this.BaseUrl, httpClient)
				.SetUserAgent(this.UserAgent);

			_fluentClient.BaseClient.DefaultRequestHeaders.Add("apikey", this.ApiKey);

			_fluentClient.Filters.Remove<DefaultErrorFilter>();
			_fluentClient.Filters.Add(new DiagnosticHandler());
			_fluentClient.Filters.Add(new CakeMailErrorHandler());

			Campaigns = new Campaigns(_fluentClient);
			Clients = new Clients(_fluentClient);
			Countries = new Countries(_fluentClient);
			Permissions = new Permissions(_fluentClient);
			Lists = new Lists(_fluentClient);
			Timezones = new Timezones(_fluentClient);
			Mailings = new Mailings(_fluentClient);
			Relays = new Relays(_fluentClient);
			Segments = new Segments(_fluentClient);
			Users = new Users(_fluentClient);
			SuppressionLists = new SuppressionLists(_fluentClient);
			Templates = new Templates(_fluentClient);
			Triggers = new Triggers(_fluentClient);
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="CakeMailRestClient"/> class.
		/// </summary>
		~CakeMailRestClient()
		{
			// The object went out of scope and finalized is called.
			// Call 'Dispose' to release unmanaged resources
			// Managed resources will be released when GC runs the next time.
			Dispose(false);
		}

		#endregion

		#region PUBLIC METHODS

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			// Call 'Dispose' to release resources
			Dispose(true);

			// Tell the GC that we have done the cleanup and there is nothing left for the Finalizer to do
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
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

		#region PRIVATE METHODS

		private void ReleaseManagedResources()
		{
			if (_fluentClient != null)
			{
				_fluentClient.Dispose();
				_fluentClient = null;
			}

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
