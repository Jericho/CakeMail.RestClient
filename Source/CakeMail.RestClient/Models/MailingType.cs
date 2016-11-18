using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MailingType
	{
		[EnumMember(Value = "standard")]
		Standard,
		[EnumMember(Value = "recurring")]
		Recurring,
		[EnumMember(Value = "absplit")]
		ABSplit
	}
}