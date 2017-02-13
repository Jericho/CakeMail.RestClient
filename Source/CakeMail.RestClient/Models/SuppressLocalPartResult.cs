using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class SuppressLocalPartResult
	{
		#region Properties

		[JsonProperty("localpart")]
		public string LocalPart { get; set; }

		[JsonProperty("error_code")]
		public long ErrorCode { get; set; }

		[JsonProperty("error_message")]
		public string ErrorMessage { get; set; }

		#endregion
	}
}
