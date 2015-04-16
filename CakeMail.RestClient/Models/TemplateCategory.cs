using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class TemplateCategory
	{
		#region Properties

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("owner_client_id")]
		public long OwnerClientId { get; set; }

		[JsonProperty("templates_copyable")]
		[JsonConverter(typeof(CakeMailIntegerBooleanConverter))]
		public bool TemplatesAreCopyable { get; set; }

		[JsonProperty("default")]
		[JsonConverter(typeof(CakeMailIntegerBooleanConverter))]
		public bool IsVisibleByDefault { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("amount_templates")]
		public long AmountTemplates { get; set; }

		[JsonProperty("amount_clients")]
		public long AmountCLients { get; set; }

		[JsonProperty("level")]
		public long Level { get; set; }

		#endregion
	}
}