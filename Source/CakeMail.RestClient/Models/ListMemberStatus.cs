using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ListMemberStatus
	{
		[EnumMember(Value = "active")]
		Active,
		[EnumMember(Value = "unsubscribed")]
		Unsubscribed,
		[EnumMember(Value = "deleted")]
		Deleted,
		[EnumMember(Value = "inactive_bounced")]
		InactiveBounced,
		[EnumMember(Value = "active_bounced")]
		ActiveBounced,
		[EnumMember(Value = "spam")]
		Spam
	}
}
