using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class SuppressEmailResult
	{
		#region Properties

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("error_code")]
		public int ErrorCode { get; set; }

		[JsonProperty("error_message")]
		public string ErrorMessage { get; set; }

		#endregion
	}
}