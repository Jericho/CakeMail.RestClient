using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum CampaignStatus
	{
		[EnumMember(Value = "ongoing")]
		Ongoing,
		[EnumMember(Value = "closed")]
		Closed
	}
}
