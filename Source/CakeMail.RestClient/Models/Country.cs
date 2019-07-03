using Newtonsoft.Json;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// A country.
	/// </summary>
	public class Country
	{
		#region Properties

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the french name.
		/// </summary>
		/// <value>
		/// The name of the french.
		/// </value>
		[JsonProperty("fr_name")]
		public string FrenchName { get; set; }

		/// <summary>
		/// Gets or sets the english name.
		/// </summary>
		/// <value>
		/// The name of the english.
		/// </value>
		[JsonProperty("en_name")]
		public string EnglishName { get; set; }

		#endregion
	}
}
