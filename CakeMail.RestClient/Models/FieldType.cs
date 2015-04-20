using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum FieldType
	{
		[EnumMember(Value = "text")]
		Text,
		[EnumMember(Value = "integer")]
		Integer,
		[EnumMember(Value = "datetime")]
		DateTime,
		[EnumMember(Value = "timestamp")]
		Timestamp,
		[EnumMember(Value = "mediumtext")]
		Memo
	}
}