using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate transfer encoding.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TransferEncoding
	{
		/// <summary>
		/// Quoted-printable
		/// </summary>
		[EnumMember(Value = "quoted-printable")]
		QuotedPrintable,

		/// <summary>
		/// Base64
		/// </summary>
		[EnumMember(Value = "base64")]
		Base64
	}
}
