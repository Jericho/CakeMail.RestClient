using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class SuppressDomainResult
	{
		#region Properties

		[JsonProperty("domain")]
		public string Domain { get; set; }

		[JsonProperty("error_code")]
		public long ErrorCode { get; set; }

		[JsonProperty("error_message")]
		public string ErrorMessage { get; set; }

		#endregion
	}
}