using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate the type of encoding.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MessageEncoding
	{
		/// <summary>
		/// UTF8
		/// </summary>
		[EnumMember(Value = "utf-8")]
		Utf8,

		/// <summary>
		/// ISO8859
		/// </summary>
		[EnumMember(Value = "iso-8859-x")]
		Iso8859
	}
}
