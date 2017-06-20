using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate the status of a list member
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ListMemberStatus
	{
		/// <summary>
		/// List member is active
		/// </summary>
		[EnumMember(Value = "active")]
		Active,

		/// <summary>
		/// List member has unsubscribed from the list
		/// </summary>
		[EnumMember(Value = "unsubscribed")]
		Unsubscribed,

		/// <summary>
		/// List member has been deleted from the list
		/// </summary>
		[EnumMember(Value = "deleted")]
		Deleted,

		/// <summary>
		/// List member is inactive due to the fact that a previous email bounced
		/// </summary>
		[EnumMember(Value = "inactive_bounced")]
		InactiveBounced,

		/// <summary>
		/// List member is active despite the fact that a previous email bounced
		/// </summary>
		[EnumMember(Value = "active_bounced")]
		ActiveBounced,

		/// <summary>
		/// List member has complained that a prior email was SPAM
		/// </summary>
		[EnumMember(Value = "spam")]
		Spam
	}
}
