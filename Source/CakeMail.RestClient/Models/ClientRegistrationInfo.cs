using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class ClientRegistrationInfo
	{
		#region Properties

		[JsonProperty("client_id")]
		public long ClientId { get; set; }

		[JsonProperty("client_key")]
		public string ClientKey { get; set; }

		[JsonProperty("admin_id")]
		public long AdminId { get; set; }

		[JsonProperty("admin_key")]
		public string AdminKey { get; set; }

		[JsonProperty("contact_id")]
		public long PrimaryContactId { get; set; }

		[JsonProperty("contact_key")]
		public string PrimaryContactKey { get; set; }

		#endregion
	}
}
