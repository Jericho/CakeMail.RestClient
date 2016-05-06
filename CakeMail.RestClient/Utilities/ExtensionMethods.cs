using System;
using System.Linq;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Utilities
{
	/// <summary>
	/// Various extension methods
	/// </summary>
	public static class ExtensionMethods
	{
		/// <summary>
		/// Convert a DateTime into a string that can be accepted by the CakeMail API.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ToCakeMailString(this DateTime value)
		{
			if (value == DateTime.MinValue) return Constants.EMPTY_CAKEMAIL_DATE;
			return value.ToString(Constants.CAKEMAIL_DATE_FORMAT);
		}

		/// <summary>
		/// Get the value of the 'EnumMember' attribute associated with the value.
		/// </summary>
		/// <param name="value">The enum value</param>
		/// <returns>The string value of the 'EnumMember' attribute associated with the value</returns>
		public static string GetEnumMemberValue(this Enum value)
		{
			var type = value.GetType();
			var name = Enum.GetName(type, value);
			var attrib = type.GetField(name)
				.GetCustomAttributes(false)
				.OfType<EnumMemberAttribute>()
				.SingleOrDefault();
			return (attrib == null ? "" : attrib.Value);
		}

		/// <summary>
		/// Get the enum value associated with the 'EnumMember' attribute string value
		/// </summary>
		/// <typeparam name="T">The Enum type</typeparam>
		/// <param name="enumMember">The value of the 'EnumMember' attribute</param>
		/// <returns>The Enum value associated with the 'EnumMember' attribute</returns>
		public static T GetValueFromEnumMember<T>(this string enumMember) where T : struct, IConvertible
		{
			var type = typeof(T);
			if (!type.IsEnum) throw new NotSupportedException("Type given must be an Enum");
			foreach (var field in type.GetFields())
			{
				var attribute = Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute)) as EnumMemberAttribute;
				if (attribute != null)
				{
					if (attribute.Value == enumMember) return (T)field.GetValue(null);
				}
				else
				{
					if (field.Name == enumMember) return (T)field.GetValue(null);
				}
			}
			throw new ArgumentException(string.Format("{0} does not have an element with value {1}", typeof(T), enumMember));
		}
	}
}