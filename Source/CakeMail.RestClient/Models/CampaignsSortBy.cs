using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate how to sort campaigns.
	/// </summary>
	public enum CampaignsSortBy
	{
		/// <summary>
		/// Sort by 'Created On'
		/// </summary>
		[EnumMember(Value = "created_on")]
		CreatedOn,

		/// <summary>
		/// Sort by 'Name'
		/// </summary>
		[EnumMember(Value = "name")]
		Name
	}
}
