using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// A timezone.
	/// </summary>
	public class Timezone
	{
		#region Properties

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[JsonProperty("id")]
		public long Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[JsonProperty("name")]
		public string Name { get; set; }

		#endregion
	}
}
