using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate how to sort list members
	/// </summary>
	public enum ListMembersSortBy
	{
		/// <summary>
		/// Sort by Id
		/// </summary>
		[EnumMember(Value = "id")]
		Id,

		/// <summary>
		/// Sort by email address
		/// </summary>
		[EnumMember(Value = "email")]
		EmailAddress
	}
}
