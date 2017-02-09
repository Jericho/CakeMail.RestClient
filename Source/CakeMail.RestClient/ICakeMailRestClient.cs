using CakeMail.RestClient.Resources;
using System;

namespace CakeMail.RestClient
{
	/// <summary>
	/// Provides the base interface for implementation of access to the CakeMail API
	/// </summary>
	public interface ICakeMailRestClient
	{
		/// <summary>
		/// The API key provided by CakeMail
		/// </summary>
		string ApiKey { get; }

		/// <summary>
		/// The user agent
		/// </summary>
		string UserAgent { get; }

		/// <summary>
		/// The timeout
		/// </summary>
		int Timeout { get; }

		/// <summary>
		/// The URL where all API requests are sent
		/// </summary>
		Uri BaseUrl { get; }

		Campaigns Campaigns { get; }
		Clients Clients { get; }
		Countries Countries { get; }
		Permissions Permissions { get; }
		Lists Lists { get; }
		Timezones Timezones { get; }
		Mailings Mailings { get; }
		Relays Relays { get; }
		Segments Segments { get; }
		Users Users { get; }
		SuppressionLists SuppressionLists { get; }
		Templates Templates { get; }
		Triggers Triggers { get; }
	}
}
