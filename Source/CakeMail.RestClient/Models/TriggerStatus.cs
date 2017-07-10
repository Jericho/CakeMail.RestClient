using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate the status of a trigger
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TriggerStatus
	{
		/// <summary>
		/// Active
		/// </summary>
		[EnumMember(Value = "active")]
		Active,

		/// <summary>
		/// Inactive
		/// </summary>
		[EnumMember(Value = "inactive")]
		Inactive
	}
}
