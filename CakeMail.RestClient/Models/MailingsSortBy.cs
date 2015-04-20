using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	public enum MailingsSortBy
	{
		[EnumMember(Value = "name")]
		Name,
		[EnumMember(Value = "created_on")]
		CreatedOn,
		[EnumMember(Value = "scheduled_for")]
		ScheduledFor,
		[EnumMember(Value = "scheduled_on")]
		ScheduledOn,
		[EnumMember(Value = "active_emails")]
		ActiveEmails
	}
}