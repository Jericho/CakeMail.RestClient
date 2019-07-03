using CakeMail.RestClient.Resources;
using System;

namespace CakeMail.RestClient
{
	/// <summary>
	/// Provides the base interface for implementation of access to the CakeMail API.
	/// </summary>
	public interface ICakeMailRestClient
	{
		/// <summary>
		/// Gets the API key provided by CakeMail.
		/// </summary>
		string ApiKey { get; }

		/// <summary>
		/// Gets the URL where all API requests are sent.
		/// </summary>
		Uri BaseUrl { get; }

		/// <summary>
		/// Gets the <see cref="Campaigns">Campaigns</see> resource.
		/// </summary>
		ICampaigns Campaigns { get; }

		/// <summary>
		/// Gets the <see cref="Clients">Clients</see> resource.
		/// </summary>
		IClients Clients { get; }

		/// <summary>
		/// Gets the <see cref="Countries">Countries</see> resource.
		/// </summary>
		ICountries Countries { get; }

		/// <summary>
		/// Gets the <see cref="Permissions">Permissions</see> resource.
		/// </summary>
		IPermissions Permissions { get; }

		/// <summary>
		/// Gets the <see cref="Lists">Lists</see> resource.
		/// </summary>
		ILists Lists { get; }

		/// <summary>
		/// Gets the <see cref="Timezones">Timezones</see> resource.
		/// </summary>
		ITimezones Timezones { get; }

		/// <summary>
		/// Gets the <see cref="Mailings">Mailings</see> resource.
		/// </summary>
		IMailings Mailings { get; }

		/// <summary>
		/// Gets the <see cref="Relays">Relays</see> resource.
		/// </summary>
		IRelays Relays { get; }

		/// <summary>
		/// Gets the <see cref="Segments">Segments</see> resource.
		/// </summary>
		ISegments Segments { get; }

		/// <summary>
		/// Gets the <see cref="Users">Users</see> resource.
		/// </summary>
		IUsers Users { get; }

		/// <summary>
		/// Gets the <see cref="SuppressionLists">SuppressionLists</see> resource.
		/// </summary>
		ISuppressionLists SuppressionLists { get; }

		/// <summary>
		/// Gets the <see cref="Templates">Templates</see> resource.
		/// </summary>
		ITemplates Templates { get; }

		/// <summary>
		/// Gets the <see cref="Triggers">Triggers</see> resource.
		/// </summary>
		ITriggers Triggers { get; }
	}
}
