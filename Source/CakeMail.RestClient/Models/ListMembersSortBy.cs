using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	public enum ListMembersSortBy
	{
		[EnumMember(Value = "id")]
		Id,
		[EnumMember(Value = "email")]
		EmailAddress
	}
}
