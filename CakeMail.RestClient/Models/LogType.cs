using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum LogType
	{
		[EnumMember(Value = "subscribe")]
		Subscribe,
		[EnumMember(Value = "in_queue")]
		Sent,
		[EnumMember(Value = "opened")]
		Open,
		[EnumMember(Value = "opened_forward")]
		OpenForward,
		[EnumMember(Value = "clickthru")]
		Click,
		[EnumMember(Value = "forward")]
		Forward,
		[EnumMember(Value = "unsubscribe")]
		Unsubscribe,
		[EnumMember(Value = "view")]
		View,
		[EnumMember(Value = "spam")]
		Spam,
		[EnumMember(Value = "skipped")]
		Skipped,
		[EnumMember(Value = "implied_open")]
		ImpliedOpen,

		[EnumMember(Value = "bounce_sb")]
		SoftBounce,
		[EnumMember(Value = "bounce_ac")]
		AddressChange,
		[EnumMember(Value = "bounce_ar")]
		AutoReply,
		[EnumMember(Value = "bounce_cr")]
		ChallengeResponse,
		[EnumMember(Value = "bounce_mb")]
		MailBlock,
		[EnumMember(Value = "bounce_fm")]
		FullMailbox,
		[EnumMember(Value = "bounce_tr")]
		TransientBounce,

		[EnumMember(Value = "bounce_hb")]
		HardBounce,
		[EnumMember(Value = "bounce_df")]
		DnsFailure
	}
}