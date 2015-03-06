using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class RelayClickLog : RelayLog
	{
		#region Properties

		[JsonProperty("host")]
		public string Host { get; set; }

		[JsonProperty("ip")]
		public string IpAddress { get; set; }

		[JsonProperty("open_id")]
		public string Id { get; set; }

		[JsonProperty("user_agent")]
		public string UserAgent { get; set; }

		#endregion
	}
}