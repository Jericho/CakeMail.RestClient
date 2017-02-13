using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum UserStatus
	{
		[EnumMember(Value = "active")]
		Active,
		[EnumMember(Value = "suspended")]
		Suspended,
		[EnumMember(Value = "deleted")]
		Deleted
	}
}
