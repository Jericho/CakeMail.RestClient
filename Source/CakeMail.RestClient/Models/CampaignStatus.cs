using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate the status of a campaign.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum CampaignStatus
	{
		/// <summary>
		/// The campaign is ongoing
		/// </summary>
		[EnumMember(Value = "ongoing")]
		Ongoing,

		/// <summary>
		/// The campaign is closed
		/// </summary>
		[EnumMember(Value = "closed")]
		Closed
	}
}
