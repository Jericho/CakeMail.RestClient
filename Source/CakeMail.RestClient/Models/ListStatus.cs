using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ListStatus
	{
		[EnumMember(Value = "active")]
		Active,
		[EnumMember(Value = "archived")]
		Archived
	}
}
