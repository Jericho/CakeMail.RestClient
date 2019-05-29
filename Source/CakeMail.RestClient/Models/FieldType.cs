using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// Enumeration to indicate the type of a custom field.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum FieldType
	{
		/// <summary>
		/// Text
		/// </summary>
		[EnumMember(Value = "text")]
		Text,

		/// <summary>
		/// Integer
		/// </summary>
		[EnumMember(Value = "integer")]
		Integer,

		/// <summary>
		/// Date and time
		/// </summary>
		[EnumMember(Value = "datetime")]
		DateTime,

		/// <summary>
		/// Timestamp
		/// </summary>
		[EnumMember(Value = "timestamp")]
		Timestamp,

		/// <summary>
		/// Memo
		/// </summary>
		[EnumMember(Value = "mediumtext")]
		Memo
	}
}
