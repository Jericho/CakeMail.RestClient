using CakeMailRestAPI.Utilities;
using Newtonsoft.Json;
using System;

namespace CakeMailRestAPI.Models
{
	public class Campaign
	{
		#region Properties

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("client_id")]
		public int ClientId { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

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