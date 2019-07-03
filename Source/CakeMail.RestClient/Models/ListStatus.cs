using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate the status of a list.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ListStatus
	{
		/// <summary>
		/// The list is active
		/// </summary>
		[EnumMember(Value = "active")]
		Active,

		/// <summary>
		/// The list has been archived
		/// </summary>
		[EnumMember(Value = "archived")]
		Archived
	}
}
