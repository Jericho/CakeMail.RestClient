using System;
using System.Linq;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.Utilities
{
	public static class ExtensionMethods
	{
		public static string ToCakeMailString(this DateTime value)
		{
			if (value == DateTime.MinValue) return Constants.EMPTY_CAKEMAIL_DATE;
			return value.ToString(Constants.CAKEMAIL_DATE_FORMAT);
		}

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
			throw new ArgumentException("Not found.", "description");
		}
	}
}