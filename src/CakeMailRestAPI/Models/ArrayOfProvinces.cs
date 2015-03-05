using Newtonsoft.Json;

namespace CakeMailRestAPI.Models
{
	public class ArrayOfProvinces
	{
		#region Properties

		[JsonProperty("provinces")]
		public Province[] Provinces { get; set; }

		#endregion
	}
}