using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	public enum CampaignsSortBy
	{
		[EnumMember(Value = "created_on")]
		CreatedOn,
		[EnumMember(Value = "name")]
		Name
	}
}
