using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class Link
	{
		#region Properties

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("link_to")]
		public string Uri { get; set; }

		#endregion
	}
}