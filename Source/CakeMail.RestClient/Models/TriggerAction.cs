using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	///  Enumeration to indicate the action to kick off a trigger.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TriggerAction
	{
		/// <summary>
		/// Opt in
		/// </summary>
		[EnumMember(Value = "opt-in")]
		OptIn,

		/// <summary>
		/// Double opt in
		/// </summary>
		[EnumMember(Value = "douopt-in")]
		DoubleOptIn,

		/// <summary>
		/// Opt out
		/// </summary>
		[EnumMember(Value = "opt-out")]
		OptOut,

		/// <summary>
		/// Specific
		/// </summary>
		[EnumMember(Value = "specific")]
		Specific,

		/// <summary>
		/// Annual
		/// </summary>
		[EnumMember(Value = "annual")]
		Annual
	}
}
