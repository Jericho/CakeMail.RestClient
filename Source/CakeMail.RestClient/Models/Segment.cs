using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using System;

namespace CakeMail.RestClient.Models
{
	public class Segment
	{
		#region Properties

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("list_id")]
		public long ListId { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("query")]
		public string Query { get; set; }

		[JsonProperty("mailings_count")]
		public long MailingsCount { get; set; }

		[JsonProperty("last_used")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime LastUsedOn { get; set; }

		[JsonProperty("created_on")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime CreatedOn { get; set; }

		[JsonProperty("engagement")]
		public int? Engagement { get; set; }

		[JsonProperty("count")]
		public long Count { get; set; }

		#endregion
	}
}
