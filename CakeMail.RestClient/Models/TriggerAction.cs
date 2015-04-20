using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TriggerAction
	{
		[EnumMember(Value = "opt-in")]
		OptIn,
		[EnumMember(Value = "douopt-in")]
		DoubleOptIn,
		[EnumMember(Value = "opt-out")]
		OptOut,
		[EnumMember(Value = "specific")]
		Specific,
		[EnumMember(Value = "annual")]
		Annual
	}
}