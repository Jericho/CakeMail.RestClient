using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TransferEncoding
	{
		[EnumMember(Value = "quoted-printable")]
		QuotedPrintable,
		[EnumMember(Value = "base64")]
		Base64
	}
}
