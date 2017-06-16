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

		ICampaigns Campaigns { get; }

		IClients Clients { get; }

		ICountries Countries { get; }

		IPermissions Permissions { get; }

		ILists Lists { get; }

		ITimezones Timezones { get; }

		IMailings Mailings { get; }

		IRelays Relays { get; }

		ISegments Segments { get; }

		IUsers Users { get; }

		ISuppressionLists SuppressionLists { get; }

		ITemplates Templates { get; }

		ITriggers Triggers { get; }
	}
}
