using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class ImportResult
	{
		#region Properties

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("id")]
		public long ListMemberId { get; set; }

		[JsonProperty("error_code")]
		public long ErrorCode { get; set; }

		[JsonProperty("error_message")]
		public string ErrorMessage { get; set; }

		#endregion
	}
}