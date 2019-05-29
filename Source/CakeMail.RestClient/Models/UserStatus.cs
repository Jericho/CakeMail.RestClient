using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate the status of a user.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum UserStatus
	{
		/// <summary>
		/// Active
		/// </summary>
		[EnumMember(Value = "active")]
		Active,

		/// <summary>
		/// Suspended
		/// </summary>
		[EnumMember(Value = "suspended")]
		Suspended,

		/// <summary>
		/// Deleted
		/// </summary>
		[EnumMember(Value = "deleted")]
		Deleted
	}
}
