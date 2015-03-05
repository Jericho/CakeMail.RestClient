using Newtonsoft.Json;

namespace CakeMailRestAPI.Models
{
	public class ArrayOfCountries
	{
		#region Properties

		[JsonProperty("countries")]
		public Country[] Countries { get; set; }

		#endregion
	}
}