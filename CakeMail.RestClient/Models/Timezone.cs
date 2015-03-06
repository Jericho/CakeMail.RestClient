using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class Timezone
	{
		#region Properties

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		#endregion
	}
}