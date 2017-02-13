using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class RelayBounceLog : RelayLog
	{
		#region Properties

		[JsonProperty("bounce_type")]
		public string BounceType { get; set; }

		#endregion
	}
}
