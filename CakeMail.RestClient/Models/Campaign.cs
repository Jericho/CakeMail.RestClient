using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using System;

namespace CakeMail.RestClient.Models
{
	public class Campaign
	{
		#region Properties

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("client_id")]
		public long ClientId { get; set; }

		[JsonProperty("status")]
		public CampaignStatus? Status { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("created_on")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime CreatedOn { get; set; }

		[JsonProperty("closed_on")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime ClosedOn { get; set; }

		#endregion
	}
}