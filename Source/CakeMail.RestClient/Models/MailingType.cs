using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate the type of mailing
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MailingType
	{
		/// <summary>
		/// Standard
		/// </summary>
		[EnumMember(Value = "standard")]
		Standard,

		/// <summary>
		/// Recurring
		/// </summary>
		[EnumMember(Value = "recurring")]
		Recurring,

		/// <summary>
		/// A/B split
		/// </summary>
		[EnumMember(Value = "absplit")]
		ABSplit
	}
}
