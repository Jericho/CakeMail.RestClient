using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class RawEmailMessage
	{
		#region Properties

		[JsonProperty("subject")]
		public string Subject { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

		#endregion
	}
}
