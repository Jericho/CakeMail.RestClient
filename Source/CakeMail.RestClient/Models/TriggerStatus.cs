using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TriggerStatus
	{
		[EnumMember(Value = "active")]
		Active,
		[EnumMember(Value = "inactive")]
		Inactive
	}
}
