using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	public enum ClientsSortBy
	{
		[EnumMember(Value = "company_name")]
		CompanyName,
		[EnumMember(Value = "registered_date")]
		RegisteredOn,
		[EnumMember(Value = "mailing_limit")]
		MailingLimit,
		[EnumMember(Value = "month_limit")]
		MonthlyLimit,
		[EnumMember(Value = "contact_limit")]
		ContactLimit,
		[EnumMember(Value = "last_activity")]
		LastActivityOn
	}
}
