using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate how to sort mailings
	/// </summary>
	public enum MailingsSortBy
	{
		/// <summary>
		/// Sort by name
		/// </summary>
		[EnumMember(Value = "name")]
		Name,

		/// <summary>
		/// Sort yb 'Created On'
		/// </summary>
		[EnumMember(Value = "created_on")]
		CreatedOn,

		/// <summary>
		/// Sort by the date the mailing is scheduled to be sent on
		/// </summary>
		[EnumMember(Value = "scheduled_for")]
		ScheduledFor,

		/// <summary>
		/// Sort by the date when the mailing was scheduled
		/// </summary>
		[EnumMember(Value = "scheduled_on")]
		ScheduledOn,

		/// <summary>
		/// Sort by the number of active emails on the list associated with the mailing
		/// </summary>
		[EnumMember(Value = "active_emails")]
		ActiveEmails
	}
}
