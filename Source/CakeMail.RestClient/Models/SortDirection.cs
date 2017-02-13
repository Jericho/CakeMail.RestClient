using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	public enum SortDirection
	{
		[EnumMember(Value = "asc")]
		Ascending,
		[EnumMember(Value = "desc")]
		Descending
	}
}
