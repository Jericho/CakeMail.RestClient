using CakeMail.RestClient.Exceptions;
using Newtonsoft.Json.Linq;
using Pathoschild.Http.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
			return descriptionAttribute == null ? value.ToString() : descriptionAttribute.Value;
		}

		/// <summary>
		/// Get the enum value associated with the 'EnumMember' attribute string value
		/// </summary>
		/// <typeparam name="T">The Enum type</typeparam>
		/// <param name="enumMember">The value of the 'EnumMember' attribute</param>
		/// <returns>The Enum value associated with the 'EnumMember' attribute</returns>
		public static T GetValueFromEnumMember<T>(this string enumMember)
			where T : struct, IConvertible
		{
			var fields = typeof(T).GetFields();
			foreach (var fieldInfo in fields)
			{
				var attributes = fieldInfo.GetCustomAttributes(typeof(EnumMemberAttribute), false).OfType<EnumMemberAttribute>();
				if (attributes.Any(a => a.Value == enumMember) || fieldInfo.Name == enumMember)
				{
					return (T)Enum.Parse(typeof(T), fieldInfo.Name, true);
				}
			}

			var message = string.Format("'{0}' is not a valid enumeration of '{1}'", enumMember, typeof(T).Name);
			throw new Exception(message);
		}

		/// <summary>Asynchronously parses the JSON response from the CakeMail API and converts the data the desired type.</summary>
		/// <typeparam name="T">The response model to deserialize into.</typeparam>
		/// <param name="response">The response</param>
		/// <returns>Returns the response body, or <c>null</c> if the response has no body.</returns>
		/// <exception cref="ApiException">An error occurred processing the response.</exception>
		public static Task<T> AsCakeMailObject<T>(this IResponse response)
		{
			return response.Message.Content.AsCakeMailObjectAsync<T>();
		}

		/// <summary>Asynchronously parses the JSON response from the CakeMail API and converts the data the desired type.</summary>
		/// <typeparam name="T">The response model to deserialize into.</typeparam>
		/// <param name="request">The request</param>
		/// <param name="propertyName">The name of the JSON property (or null if not applicable) where the desired data is stored</param>
		/// <returns>Returns the response body, or <c>null</c> if the response has no body.</returns>
		/// <exception cref="ApiException">An error occurred processing the response.</exception>
		public static async Task<T> AsCakeMailObject<T>(this IRequest request, string propertyName = null)
		{
			var response = await request.AsMessage().ConfigureAwait(false);
			return await response.Content.AsCakeMailObjectAsync<T>(propertyName).ConfigureAwait(false);
		}

		/// <summary>Set the body content of the HTTP request.</summary>
		/// <param name="request">The request.</param>
		/// <param name="parameters">The parameters to serialize into the HTTP body content.</param>
		/// <returns>Returns the request builder for chaining.</returns>
		public static IRequest WithFormUrlEncodedBody(this IRequest request, IEnumerable<KeyValuePair<string, object>> parameters)
		{
			var body = (FormUrlEncodedContent)null;
			if (parameters != null)
			{
				var paramsWithValue = parameters.Where(p => p.Value != null).Select(p => new KeyValuePair<string, string>(p.Key, p.Value.ToString()));
				var paramsWithoutValue = parameters.Where(p => p.Value == null).Select(p => new KeyValuePair<string, string>(p.Key, null));
				var allParams = paramsWithValue.Union(paramsWithoutValue).ToArray();
				body = new FormUrlEncodedContent(allParams);
			}

			return request.WithBodyContent(body);
		}

		/// <summary>
		///  Converts the value of the current System.TimeSpan object to its equivalent string
		///  representation by using a human readable format.
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		/// <returns>Returns the human readable representation of the TimeSpan</returns>
		public static string ToDurationString(this TimeSpan timeSpan)
		{
			// In case the TimeSpan is extremely short
			if (timeSpan.TotalMilliseconds <= 1) return "1 millisecond";

			var result = new StringBuilder();

			if (timeSpan.Days == 1) result.Append(" 1 day");
			else if (timeSpan.Days > 1) result.AppendFormat(" {0} days", timeSpan.Days);

			if (timeSpan.Hours == 1) result.Append(" 1 hour");
			else if (timeSpan.Hours > 1) result.AppendFormat(" {0} hours", timeSpan.Hours);

			if (timeSpan.Minutes == 1) result.Append(" 1 minute");
			else if (timeSpan.Minutes > 1) result.AppendFormat(" {0} minutes", timeSpan.Minutes);

			if (timeSpan.Seconds == 1) result.Append(" 1 second");
			else if (timeSpan.Seconds > 1) result.AppendFormat(" {0} seconds", timeSpan.Seconds);

			if (timeSpan.Milliseconds == 1) result.Append(" 1 millisecond");
			else if (timeSpan.Milliseconds > 1) result.AppendFormat(" {0} milliseconds", timeSpan.Milliseconds);

			return result.ToString().Trim();
		}

		/// <summary>Asynchronously parses the JSON response from the CakeMail API and converts the data the desired type.</summary>
		/// <typeparam name="T">The response model to deserialize into.</typeparam>
		/// <param name="httpContent">The content</param>
		/// <param name="propertyName">The name of the JSON property (or null if not applicable) where the desired data is stored</param>
		/// <returns>Returns the response body, or <c>null</c> if the response has no body.</returns>
		/// <exception cref="ApiException">An error occurred processing the response.</exception>
		private static async Task<T> AsCakeMailObjectAsync<T>(this HttpContent httpContent, string propertyName = null)
		{
			var responseContent = await httpContent.ReadAsStringAsync().ConfigureAwait(false);
			var cakeResponse = JObject.Parse(responseContent);
			var data = cakeResponse["data"];

			if (!string.IsNullOrEmpty(propertyName))
			{
				var properties = ((JObject)data).Properties().Where(p => p.Name.Equals(propertyName));
				if (!properties.Any()) throw new CakeMailException(string.Format("Json does not contain property {0}", propertyName));
				data = properties.First().Value;
			}

			if (data is JArray) return (data as JArray).ToObject<T>();
			else if (data is JValue) return (data as JValue).ToObject<T>();
			return (data as JObject).ToObject<T>();
		}
	}
}
