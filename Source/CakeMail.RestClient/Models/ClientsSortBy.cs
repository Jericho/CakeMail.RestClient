using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate how to sort clients.
	/// </summary>
	public enum ClientsSortBy
	{
		/// <summary>
		/// Sort by company name
		/// </summary>
		[EnumMember(Value = "company_name")]
		CompanyName,

		/// <summary>
		/// Sort by 'Registered On'
		/// </summary>
		[EnumMember(Value = "registered_date")]
		RegisteredOn,

		/// <summary>
		/// Sort by mailing limit
		/// </summary>
		[EnumMember(Value = "mailing_limit")]
		MailingLimit,

		/// <summary>
		/// Sort by monthly limit
		/// </summary>
		[EnumMember(Value = "month_limit")]
		MonthlyLimit,

		/// <summary>
		/// Sort by contact limit
		/// </summary>
		[EnumMember(Value = "contact_limit")]
		ContactLimit,

		/// <summary>
		/// Sort by 'Last Activity On'
		/// </summary>
		[EnumMember(Value = "last_activity")]
		LastActivityOn
	}
}
