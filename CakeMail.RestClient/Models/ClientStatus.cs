using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ClientStatus
	{
		[EnumMember(Value = "pending")]
		Pending,
		[EnumMember(Value = "trial")]
		Trial,
		[EnumMember(Value = "active")]
		Active,
		[EnumMember(Value = "suspended_by_reseller")]
		SuspendedByReseller,
		[EnumMember(Value = "deleted")]
		Deleted
	}
}