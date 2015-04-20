using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	public enum LogType
	{
		[EnumMember(Value = "subscribe")]
		Subscribe,
		[EnumMember(Value = "in_queue")]
		Sent,
		[EnumMember(Value = "opened")]
		Open,
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
		Skipped
	}
}