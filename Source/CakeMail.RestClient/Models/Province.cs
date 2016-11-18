using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class Province
	{
		#region Properties

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("fr_name")]
		public string FrenchName { get; set; }

		[JsonProperty("en_name")]
		public string EnglishName { get; set; }

		#endregion
	}
}