using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate the status of a client
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ClientStatus
	{
		/// <summary>
		/// Pending
		/// </summary>
		[EnumMember(Value = "pending")]
		Pending,

		/// <summary>
		/// Trial
		/// </summary>
		[EnumMember(Value = "trial")]
		Trial,

		/// <summary>
		/// Active
		/// </summary>
		[EnumMember(Value = "active")]
		Active,

		/// <summary>
		/// Suspended by reseller
		/// </summary>
		[EnumMember(Value = "suspended_by_reseller")]
		SuspendedByReseller,

		/// <summary>
		/// Deleted
		/// </summary>
		[EnumMember(Value = "deleted")]
		Deleted
	}
}
