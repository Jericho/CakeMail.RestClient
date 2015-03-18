using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class MailingLink
	{
		#region Properties

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("link_to")]
		public string Uri { get; set; }

		#endregion
	}
}