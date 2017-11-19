using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate the status of the mailing
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MailingStatus
	{
		/// <summary>
		/// The mailing is incomplete
		/// </summary>
		[EnumMember(Value = "incomplete")]
		Incomplete,

		/// <summary>
		/// The mailing has been scheduled
		/// </summary>
		[EnumMember(Value = "scheduled")]
		Scheduled,

		/// <summary>
		/// The mailing is beeing delivered
		/// </summary>
		[EnumMember(Value = "delivering")]
		Delivering,

		/// <summary>
		/// The mailing has been delivered
		/// </summary>
		[EnumMember(Value = "delivered")]
		Delivered,

		/// <summary>
		/// The mailing has been archived
		/// </summary>
		[EnumMember(Value = "archived")]
		Archived
	}
}
