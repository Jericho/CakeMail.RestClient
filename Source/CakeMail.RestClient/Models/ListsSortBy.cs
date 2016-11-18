using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	public enum ListsSortBy
	{
		[EnumMember(Value = "name")]
		Name,
		[EnumMember(Value = "created_on")]
		CreatedOn,
		[EnumMember(Value = "active_members_count")]
		ActiveMembersCount
	}
}