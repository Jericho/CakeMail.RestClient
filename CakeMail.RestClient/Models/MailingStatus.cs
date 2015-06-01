using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MailingStatus
	{
		[EnumMember(Value = "incomplete")]
		Incomplete,
		[EnumMember(Value = "scheduled")]
		Scheduled,
		[EnumMember(Value = "delivering")]
		Delivering,
		[EnumMember(Value = "delivered")]
		Delivered,
		[EnumMember(Value = "archived")]
		Archived
	}
}