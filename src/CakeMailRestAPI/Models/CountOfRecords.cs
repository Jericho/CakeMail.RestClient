using Newtonsoft.Json;

namespace CakeMailRestAPI.Models
{
	public class CountOfRecords
	{
		#region Properties

		[JsonProperty("count")]
		public long Count { get; set; }

		#endregion
	}
}