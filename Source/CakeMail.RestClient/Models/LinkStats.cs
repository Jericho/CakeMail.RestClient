using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class LinkStats
	{
		#region Properties

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("link_to")]
		public string Uri { get; set; }

		[JsonProperty("unique")]
		public long UniqueClicks { get; set; }

		[JsonProperty("total")]
		public long TotalClicks { get; set; }

		[JsonProperty("unique_rate")]
		public long UniqueClicksRate { get; set; }

		[JsonProperty("total_rate")]
		public long TotalClicksRate { get; set; }

		#endregion
	}
}
