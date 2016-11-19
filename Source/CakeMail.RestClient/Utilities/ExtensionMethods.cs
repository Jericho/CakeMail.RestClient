using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
			var fieldInfo = value.GetType().GetField(value.ToString());
			if (fieldInfo == null) return value.ToString();

			var attributes = fieldInfo.GetCustomAttributes(typeof(EnumMemberAttribute), false).ToArray();
			if (attributes == null || attributes.Length == 0) return value.ToString();

			var descriptionAttribute = attributes[0] as EnumMemberAttribute;
			return (descriptionAttribute == null ? value.ToString() : descriptionAttribute.Value);
		}

		/// <summary>
		/// Get the enum value associated with the 'EnumMember' attribute string value
		/// </summary>
		/// <typeparam name="T">The Enum type</typeparam>
		/// <param name="enumMember">The value of the 'EnumMember' attribute</param>
		/// <returns>The Enum value associated with the 'EnumMember' attribute</returns>
		public static T GetValueFromEnumMember<T>(this string enumMember) where T : struct, IConvertible
		{
			var fields = typeof(T).GetFields();
			foreach (var fieldInfo in fields)
			{
				var attributes = fieldInfo.GetCustomAttributes(typeof(EnumMemberAttribute), false).OfType<EnumMemberAttribute>();
				if (attributes.Any(a => a.Value == enumMember))
				{
					return (T)Enum.Parse(typeof(T), fieldInfo.Name, true);
				}
			}

			var message = string.Format("'{0}' is not a valid enumeration of '{1}'", enumMember, typeof(T).Name);
			throw new Exception(message);
		}

		public static void EnsureSuccess(this HttpResponseMessage response)
		{
			if (response.IsSuccessStatusCode) return;

			var content = string.Empty;
			if (response.Content != null)
			{
				content = response.Content.ReadAsStringAsync().Result;
				response.Content.Dispose();
			}
			else
			{
				content = string.Format("StatusCode: {0}", response.StatusCode);
			}

			throw new Exception(content);
		}
	}
}
