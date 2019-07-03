using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate how to sort lists.
	/// </summary>
	public enum ListsSortBy
	{
		/// <summary>
		/// Sort by Name
		/// </summary>
		[EnumMember(Value = "name")]
		Name,

		/// <summary>
		/// Sort by 'Created On'
		/// </summary>
		[EnumMember(Value = "created_on")]
		CreatedOn,

		/// <summary>
		/// Sort by the number of active members
		/// </summary>
		[EnumMember(Value = "active_members_count")]
		ActiveMembersCount
	}
}
