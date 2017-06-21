using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using System;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// A Campaign is a group of Mailings and can be seen as a folder.
	/// </summary>
	public class Campaign
	{
		#region Properties

		/// <summary>
		/// Gets or sets the identifier of the campaign.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[JsonProperty("id")]
		public long Id { get; set; }

		/// <summary>
		/// Gets or sets the identifier of the client to which the campaign belongs.
		/// </summary>
		/// <value>
		/// The client identifier.
		/// </value>
		[JsonProperty("client_id")]
		public long ClientId { get; set; }

		/// <summary>
		/// Gets or sets the status of the campaign.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		[JsonProperty("status")]
		public CampaignStatus? Status { get; set; }

		/// <summary>
		/// Gets or sets the name of the campaign.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the date the campaign was created on.
		/// </summary>
		/// <value>
		/// The created on.
		/// </value>
		[JsonProperty("created_on")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime CreatedOn { get; set; }

		/// <summary>
		/// Gets or sets the date the campaign was closed on.
		/// </summary>
		/// <value>
		/// The closed on.
		/// </value>
		[JsonProperty("closed_on")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime ClosedOn { get; set; }

		#endregion
	}
}
