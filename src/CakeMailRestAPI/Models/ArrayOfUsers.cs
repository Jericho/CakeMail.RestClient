using Newtonsoft.Json;

namespace CakeMailRestAPI.Models
{
	public class ArrayOfUsers
	{
		#region Properties

		[JsonProperty("users")]
		public User[] Users { get; set; }

		#endregion
	}
}