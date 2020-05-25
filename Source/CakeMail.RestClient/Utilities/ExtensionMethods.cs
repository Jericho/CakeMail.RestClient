using CakeMail.RestClient.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathoschild.Http.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Utilities
{
	/// <summary>
	/// Various extension methods.
	/// </summary>
	internal static class ExtensionMethods
	{
		/// <summary>
		/// Convert a DateTime into a string that can be accepted by the CakeMail API.
		/// </summary>
		/// <param name="value">The datetime value to be converted.</param>
		/// <returns>The string representation of the value.</returns>
		public static string ToCakeMailString(this DateTime value)
		{
			if (value == DateTime.MinValue) return Constants.EMPTY_CAKEMAIL_DATE;
			return value.ToString(Constants.CAKEMAIL_DATE_FORMAT);
		}

		/// <summary>
		/// Get the value of the 'EnumMember' attribute associated with the value.
		/// </summary>
		/// <param name="value">The enum value.</param>
		/// <returns>The string value of the 'EnumMember' attribute associated with the value.</returns>
		public static string GetEnumMemberValue(this Enum value)
		{
			var fieldInfo = value.GetType().GetRuntimeField(value.ToString());
			if (fieldInfo == null) return value.ToString();

			var attribute = fieldInfo
				.GetCustomAttributes()
				.OfType<EnumMemberAttribute>()
				.FirstOrDefault();

			return attribute?.Value ?? value.ToString();
		}

		/// <summary>
		/// Get the enum value associated with the 'EnumMember' attribute string value.
		/// </summary>
		/// <typeparam name="T">The Enum type.</typeparam>
		/// <param name="enumMember">The value of the 'EnumMember' attribute.</param>
		/// <returns>The Enum value associated with the 'EnumMember' attribute.</returns>
		public static T GetValueFromEnumMember<T>(this string enumMember)
			where T : struct, IConvertible
		{
			var fields = typeof(T).GetRuntimeFields();
			foreach (var fieldInfo in fields.Where(f => f.IsPublic))
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
		/// <param name="response">The response.</param>
		/// <returns>Returns the response body, or <c>null</c> if the response has no body.</returns>
		/// <exception cref="ApiException">An error occurred processing the response.</exception>
		public static Task<T> AsCakeMailObject<T>(this IResponse response)
		{
			return response.Message.Content.AsCakeMailObjectAsync<T>();
		}

		/// <summary>Asynchronously parses the JSON response from the CakeMail API and converts the data the desired type.</summary>
		/// <typeparam name="T">The response model to deserialize into.</typeparam>
		/// <param name="request">The request.</param>
		/// <param name="propertyName">The name of the JSON property (or null if not applicable) where the desired data is stored.</param>
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
			return request.WithBody(bodyBuilder => bodyBuilder.FormUrlEncoded(parameters));
		}

		/// <summary>
		///  Converts the value of the current System.TimeSpan object to its equivalent string
		///  representation by using a human readable format.
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		/// <returns>Returns the human readable representation of the TimeSpan.</returns>
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

		/// <summary>
		/// Returns the first value for a specified header stored in the System.Net.Http.Headers.HttpHeaderscollection.
		/// </summary>
		/// <param name="headers">The HTTP headers.</param>
		/// <param name="name">The specified header to return value for.</param>
		/// <returns>A string.</returns>
		public static string GetValue(this HttpHeaders headers, string name)
		{
			if (headers == null) return null;

			if (headers.TryGetValues(name, out IEnumerable<string> values))
			{
				return values.FirstOrDefault();
			}

			return null;
		}

		/// <summary>
		/// Reads the content of the HTTP response as string asynchronously.
		/// </summary>
		/// <param name="httpContent">The content.</param>
		/// <param name="encoding">The encoding. You can leave this parameter null and the encoding will be
		/// automatically calculated based on the charset in the response. Also, UTF-8
		/// encoding will be used if the charset is absent from the response, is blank
		/// or contains an invalid value.</param>
		/// <returns>The string content of the response.</returns>
		/// <remarks>
		/// This method is an improvement over the built-in ReadAsStringAsync method
		/// because it can handle invalid charset returned in the response. For example
		/// you may be sending a request to an API that returns a blank charset or a
		/// mispelled one like 'utf8' instead of the correctly spelled 'utf-8'. The
		/// built-in method throws an exception if an invalid charset is specified
		/// while this method uses the UTF-8 encoding in that situation.
		///
		/// My motivation for writing this extension method was to work around a situation
		/// where the 3rd party API I was sending requests to would sometimes return 'utf8'
		/// as the charset and an exception would be thrown when I called the ReadAsStringAsync
		/// method to get the content of the response into a string because the .Net HttpClient
		/// would attempt to determine the proper encoding to use but it would fail due to
		/// the fact that the charset was misspelled. I contacted the vendor, asking them
		/// to either omit the charset or fix the misspelling but they didn't feel the need
		/// to fix this issue because:
		/// "in some programming languages, you can use the syntax utf8 instead of utf-8".
		/// In other words, they are happy to continue using the misspelled value which is
		/// supported by "some" programming languages instead of using the properly spelled
		/// value which is supported by all programming languages.
		/// </remarks>
		/// <example>
		/// <code>
		/// var httpRequest = new HttpRequestMessage
		/// {
		///     Method = HttpMethod.Get,
		///     RequestUri = new Uri("https://api.vendor.com/v1/endpoint")
		/// };
		/// var httpClient = new HttpClient();
		/// var response = await httpClient.SendAsync(httpRequest, CancellationToken.None).ConfigureAwait(false);
		/// var responseContent = await response.Content.ReadAsStringAsync(null).ConfigureAwait(false);
		/// </code>
		/// </example>
		public static async Task<string> ReadAsStringAsync(this HttpContent httpContent, Encoding encoding)
		{
			var content = string.Empty;

			if (httpContent != null)
			{
				var contentStream = await httpContent.ReadAsStreamAsync().ConfigureAwait(false);

				if (encoding == null) encoding = httpContent.GetEncoding(Encoding.UTF8);

				// This is important: we must make a copy of the response stream otherwise we would get an
				// exception on subsequent attempts to read the content of the stream
				using (var ms = new MemoryStream())
				{
					await contentStream.CopyToAsync(ms).ConfigureAwait(false);
					ms.Position = 0;
					using (var sr = new StreamReader(ms, encoding))
					{
						content = await sr.ReadToEndAsync().ConfigureAwait(false);
					}

					// It's important to rewind the stream
					if (contentStream.CanSeek) contentStream.Position = 0;
				}
			}

			return content;
		}

		/// <summary>
		/// Gets the encoding.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <param name="defaultEncoding">The default encoding.</param>
		/// <returns>
		/// The encoding.
		/// </returns>
		/// <remarks>
		/// This method tries to get the encoding based on the charset or uses the
		/// 'defaultEncoding' if the charset is empty or contains an invalid value.
		/// </remarks>
		/// <example>
		///   <code>
		/// var httpRequest = new HttpRequestMessage
		/// {
		/// Method = HttpMethod.Get,
		/// RequestUri = new Uri("https://my.api.com/v1/myendpoint")
		/// };
		/// var httpClient = new HttpClient();
		/// var response = await httpClient.SendAsync(httpRequest, CancellationToken.None).ConfigureAwait(false);
		/// var encoding = response.Content.GetEncoding(Encoding.UTF8);
		/// </code>
		/// </example>
		public static Encoding GetEncoding(this HttpContent content, Encoding defaultEncoding)
		{
			var encoding = defaultEncoding;
			try
			{
				var charset = content?.Headers?.ContentType?.CharSet;
				if (!string.IsNullOrEmpty(charset))
				{
					encoding = Encoding.GetEncoding(charset);
				}
			}
			catch
			{
				encoding = defaultEncoding;
			}

			return encoding;
		}

		public static async Task<TResult[]> ForEachAsync<T, TResult>(this IEnumerable<T> items, Func<T, Task<TResult>> action, int maxDegreeOfParalellism)
		{
			var allTasks = new List<Task<TResult>>();
			var throttler = new SemaphoreSlim(initialCount: maxDegreeOfParalellism);
			foreach (var item in items)
			{
				await throttler.WaitAsync();
				allTasks.Add(
					Task.Run(async () =>
					{
						try
						{
							return await action(item).ConfigureAwait(false);
						}
						finally
						{
							throttler.Release();
						}
					}));
			}

			var results = await Task.WhenAll(allTasks).ConfigureAwait(false);
			return results;
		}

		public static async Task ForEachAsync<T>(this IEnumerable<T> items, Func<T, Task> action, int maxDegreeOfParalellism)
		{
			var allTasks = new List<Task>();
			var throttler = new SemaphoreSlim(initialCount: maxDegreeOfParalellism);
			foreach (var item in items)
			{
				await throttler.WaitAsync();
				allTasks.Add(
					Task.Run(async () =>
					{
						try
						{
							await action(item).ConfigureAwait(false);
						}
						finally
						{
							throttler.Release();
						}
					}));
			}

			await Task.WhenAll(allTasks).ConfigureAwait(false);
		}

		/// <summary>Asynchronously parses the JSON response from the CakeMail API and converts the data the desired type.</summary>
		/// <typeparam name="T">The response model to deserialize into.</typeparam>
		/// <param name="httpContent">The content.</param>
		/// <param name="propertyName">The name of the JSON property (or null if not applicable) where the desired data is stored.</param>
		/// <returns>Returns the response body, or <c>null</c> if the response has no body.</returns>
		/// <exception cref="ApiException">An error occurred processing the response.</exception>
		private static async Task<T> AsCakeMailObjectAsync<T>(this HttpContent httpContent, string propertyName = null, JsonConverter jsonConverter = null)
		{
			var responseContent = await httpContent.ReadAsStringAsync().ConfigureAwait(false);

			var serializer = new JsonSerializer();
			if (jsonConverter != null) serializer.Converters.Add(jsonConverter);

			var cakeResponse = JObject.Parse(responseContent);
			var data = cakeResponse["data"];

			if (!string.IsNullOrEmpty(propertyName))
			{
				var properties = ((JObject)data).Properties().Where(p => p.Name.Equals(propertyName));
				if (!properties.Any()) throw new CakeMailException(string.Format("Json does not contain property {0}", propertyName), responseContent);
				data = properties.First().Value;
			}

			if (data is JArray) return (data as JArray).ToObject<T>(serializer);
			else if (data is JValue) return (data as JValue).ToObject<T>(serializer);
			return (data as JObject).ToObject<T>(serializer);
		}
	}
}
