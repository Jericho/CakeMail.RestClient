using CakeMail.RestClient.Resources;
using CakeMail.RestClient.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Pathoschild.Http.Client;
using Pathoschild.Http.Client.Extensibility;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace CakeMail.RestClient
{
	/// <summary>
	/// Core class for using the CakeMail Api.
	/// </summary>
	public class CakeMailRestClient : ICakeMailRestClient
	{
		#region FIELDS

		private const string CAKEMAIL_BASE_URI = "https://api.wbsrvc.com/";

		private static string _version;

		private readonly bool _mustDisposeHttpClient;
		private readonly CakeMailClientOptions _options;
		private readonly ILogger _logger;

		private HttpClient _httpClient;
		private Pathoschild.Http.Client.IClient _fluentClient;

		#endregion

		#region PROPERTIES

		/// <summary>
		/// Gets the Version.
		/// </summary>
		/// <value>
		/// The version.
		/// </value>
		public static string Version
		{
			get
			{
				if (string.IsNullOrEmpty(_version))
				{
					_version = typeof(CakeMailRestClient).GetTypeInfo().Assembly.GetName().Version.ToString(3);
#if DEBUG
					_version = "DEBUG";
#endif
				}

				return _version;
			}
		}

		/// <summary>
		/// Gets the API key provided by CakeMail.
		/// </summary>
		public string ApiKey { get; private set; }

		/// <summary>
		/// Gets the URL where all API requests are sent.
		/// </summary>
		public Uri BaseUrl { get; private set; }

		/// <summary>
		/// Gets the <see cref="Campaigns">Campaigns</see> resource.
		/// </summary>
		public ICampaigns Campaigns { get; private set; }

		/// <summary>
		/// Gets the <see cref="Clients">Clients</see> resource.
		/// </summary>
		public IClients Clients { get; private set; }

		/// <summary>
		/// Gets the <see cref="Countries">Countries</see> resource.
		/// </summary>
		public ICountries Countries { get; private set; }

		/// <summary>
		/// Gets the <see cref="Permissions">Permissions</see> resource.
		/// </summary>
		public IPermissions Permissions { get; private set; }

		/// <summary>
		/// Gets the <see cref="Lists">Lists</see> resource.
		/// </summary>
		public ILists Lists { get; private set; }

		/// <summary>
		/// Gets the <see cref="Timezones">Timezones</see> resource.
		/// </summary>
		public ITimezones Timezones { get; private set; }

		/// <summary>
		/// Gets the <see cref="Mailings">Mailings</see> resource.
		/// </summary>
		public IMailings Mailings { get; private set; }

		/// <summary>
		/// Gets the <see cref="Relays">Relays</see> resource.
		/// </summary>
		public IRelays Relays { get; private set; }

		/// <summary>
		/// Gets the <see cref="Segments">Segments</see> resource.
		/// </summary>
		public ISegments Segments { get; private set; }

		/// <summary>
		/// Gets the <see cref="Users">Users</see> resource.
		/// </summary>
		public IUsers Users { get; private set; }

		/// <summary>
		/// Gets the <see cref="SuppressionLists">SuppressionLists</see> resource.
		/// </summary>
		public ISuppressionLists SuppressionLists { get; private set; }

		/// <summary>
		/// Gets the <see cref="Templates">Templates</see> resource.
		/// </summary>
		public ITemplates Templates { get; private set; }

		/// <summary>
		/// Gets the <see cref="Triggers">Triggers</see> resource.
		/// </summary>
		public ITriggers Triggers { get; private set; }

		#endregion

		#region CONSTRUCTORS AND DESTRUCTORS

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailRestClient"/> class.
		/// </summary>
		/// <param name="apiKey">The API Key received from CakeMail.</param>
		/// <param name="options">Options for the CakeMail client.</param>
		/// <param name="logger">Logger.</param>
		public CakeMailRestClient(string apiKey, CakeMailClientOptions options = null, ILogger logger = null)
			: this(apiKey, (HttpClient)null, options, logger)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailRestClient"/> class.
		/// </summary>
		/// <param name="apiKey">The API Key received from CakeMail.</param>
		/// <param name="proxy">Allows you to specify a proxy.</param>
		/// <param name="options">Options for the CakeMail client.</param>
		/// <param name="logger">Logger.</param>
		public CakeMailRestClient(string apiKey, IWebProxy proxy, CakeMailClientOptions options = null, ILogger logger = null)
			: this(apiKey, new HttpClient(new HttpClientHandler { Proxy = proxy, UseProxy = proxy != null }), options, logger)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailRestClient"/> class.
		/// </summary>
		/// <param name="apiKey">The API Key received from CakeMail.</param>
		/// <param name="handler">The HTTP handler stack to use for sending requests.</param>
		/// <param name="options">Options for the CakeMail client.</param>
		/// <param name="logger">Logger.</param>
		public CakeMailRestClient(string apiKey, HttpMessageHandler handler, CakeMailClientOptions options = null, ILogger logger = null)
			: this(apiKey, new HttpClient(handler), true, options, logger)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailRestClient"/> class.
		/// </summary>
		/// <param name="apiKey">The API Key received from CakeMail.</param>
		/// <param name="httpClient">Allows you to inject your own HttpClient. This is useful, for example, to setup the HtppClient with a proxy.</param>
		/// <param name="options">Options for the CakeMail client.</param>
		/// <param name="logger">Logger.</param>
		public CakeMailRestClient(string apiKey, HttpClient httpClient, CakeMailClientOptions options = null, ILogger logger = null)
			: this(apiKey, httpClient, false, options, logger)
		{
		}

		private CakeMailRestClient(string apiKey, HttpClient httpClient, bool disposeClient, CakeMailClientOptions options, ILogger logger = null)
		{
			_mustDisposeHttpClient = disposeClient;
			_httpClient = httpClient;
			_options = options ?? GetDefaultOptions();
			_logger = logger ?? NullLogger.Instance;

			ApiKey = apiKey;
			BaseUrl = new Uri(CAKEMAIL_BASE_URI);

			_fluentClient = new FluentClient(this.BaseUrl, httpClient)
				.SetUserAgent($"CakeMail .NET REST Client/{Version} (+https://github.com/Jericho/CakeMail.RestClient)");

			_fluentClient.Filters.Remove<DefaultErrorFilter>();

			// Order is important: DiagnosticHandler must be first.
			_fluentClient.Filters.Add(new DiagnosticHandler(_options.LogLevelSuccessfulCalls, _options.LogLevelFailedCalls));
			_fluentClient.Filters.Add(new CakeMailErrorHandler());

			_fluentClient.BaseClient.DefaultRequestHeaders.Add("apikey", this.ApiKey);

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

		private CakeMailClientOptions GetDefaultOptions()
		{
			return new CakeMailClientOptions()
			{
				LogLevelSuccessfulCalls = LogLevel.Debug,
				LogLevelFailedCalls = LogLevel.Error
			};
		}

		#endregion
	}
}
