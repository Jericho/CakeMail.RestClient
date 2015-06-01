using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MessageEncoding
	{
		[EnumMember(Value = "utf-8")]
		Utf8,
		[EnumMember(Value = "iso-8859-x")]
		Iso8859
	}
}