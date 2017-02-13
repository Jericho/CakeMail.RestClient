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
		/// Gets the API key provided by CakeMail
		/// </summary>
		string ApiKey { get; }

		/// <summary>
		/// Gets the user agent
		/// </summary>
		string UserAgent { get; }

		/// <summary>
		/// Gets the URL where all API requests are sent
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
