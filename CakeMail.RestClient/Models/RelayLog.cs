using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class RelayLog
	{
		#region Properties

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("relay_id")]
		public string RelayId { get; set; }

		[JsonProperty("sent_id")]
		public string RelayLogId { get; set; }

		[JsonProperty("time")]
		public string Date { get; set; }

		[JsonProperty("tracking_id")]
		public string TrackingId { get; set; }

		#endregion
	}
}