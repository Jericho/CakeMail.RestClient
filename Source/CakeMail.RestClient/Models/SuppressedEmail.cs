using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class SuppressedEmail
	{
		#region Properties

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("source_type")]
		public string Source { get; set; }

		#endregion
	}
}