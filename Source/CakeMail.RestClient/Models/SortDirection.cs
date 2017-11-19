using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate sort direction
	/// </summary>
	public enum SortDirection
	{
		/// <summary>
		/// Ascending
		/// </summary>
		[EnumMember(Value = "asc")]
		Ascending,

		/// <summary>
		/// Descending
		/// </summary>
		[EnumMember(Value = "desc")]
		Descending
	}
}
