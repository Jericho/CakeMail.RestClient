using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	public class TemplateCategoryVisibility
	{
		#region Properties

		[JsonProperty("client_id")]
		public long ClientId { get; set; }

		[JsonProperty("company_name")]
		public string CompanyName { get; set; }

		[JsonProperty("visible")]
		[JsonConverter(typeof(CakeMailIntegerBooleanConverter))]
		public bool Visible { get; set; }

		#endregion
	}
}