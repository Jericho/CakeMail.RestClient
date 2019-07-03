using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	///  Enumeration to indicate the type of log.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum LogType
	{
		/// <summary>
		/// Subscribe
		/// </summary>
		[EnumMember(Value = "subscribe")]
		Subscribe,

		/// <summary>
		/// Sent
		/// </summary>
		[EnumMember(Value = "in_queue")]
		Sent,

		/// <summary>
		/// Open
		/// </summary>
		[EnumMember(Value = "opened")]
		Open,

		/// <summary>
		/// Opened forward
		/// </summary>
		[EnumMember(Value = "opened_forward")]
		OpenForward,

		/// <summary>
		/// Click
		/// </summary>
		[EnumMember(Value = "clickthru")]
		Click,

		/// <summary>
		/// Forward
		/// </summary>
		[EnumMember(Value = "forward")]
		Forward,

		/// <summary>
		/// Unsubscribe
		/// </summary>
		[EnumMember(Value = "unsubscribe")]
		Unsubscribe,

		/// <summary>
		/// View
		/// </summary>
		[EnumMember(Value = "view")]
		View,

		/// <summary>
		/// Spam
		/// </summary>
		[EnumMember(Value = "spam")]
		Spam,

		/// <summary>
		/// Skipped
		/// </summary>
		[EnumMember(Value = "skipped")]
		Skipped,

		/// <summary>
		/// Implied open
		/// </summary>
		[EnumMember(Value = "implied_open")]
		ImpliedOpen,

		/// <summary>
		/// Soft bounce
		/// </summary>
		[EnumMember(Value = "bounce_sb")]
		SoftBounce,

		/// <summary>
		/// Address change
		/// </summary>
		[EnumMember(Value = "bounce_ac")]
		AddressChange,

		/// <summary>
		/// Automatic reply
		/// </summary>
		[EnumMember(Value = "bounce_ar")]
		AutoReply,

		/// <summary>
		/// Challenge response
		/// </summary>
		[EnumMember(Value = "bounce_cr")]
		ChallengeResponse,

		/// <summary>
		/// Mail block
		/// </summary>
		[EnumMember(Value = "bounce_mb")]
		MailBlock,

		/// <summary>
		/// Full mailbox
		/// </summary>
		[EnumMember(Value = "bounce_fm")]
		FullMailbox,

		/// <summary>
		/// Transient bounce
		/// </summary>
		[EnumMember(Value = "bounce_tr")]
		TransientBounce,

		/// <summary>
		/// Hard bounce
		/// </summary>
		[EnumMember(Value = "bounce_hb")]
		HardBounce,

		/// <summary>
		/// DNS failure
		/// </summary>
		[EnumMember(Value = "bounce_df")]
		DnsFailure,

		/// <summary>
		/// Received
		/// </summary>
		[EnumMember(Value = "received")]
		Received,

		/// <summary>
		/// Schedule
		/// </summary>
		[EnumMember(Value = "schedule")]
		Schedule
	}
}
