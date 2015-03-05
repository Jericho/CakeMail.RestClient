
using Newtonsoft.Json;
namespace CakeMailRestAPI.Models
{
	public class ArrayOfCampaigns
	{
		#region Properties

		[JsonProperty("campaigns")]
		public Campaign[] Campaigns { get; set; }

		#endregion
	}
}