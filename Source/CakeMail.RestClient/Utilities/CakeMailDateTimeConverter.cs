using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.InteropServices;

namespace CakeMail.RestClient.Utilities
{
	/// <summary>
	/// Converter class used in conjuction with JSON.NET to convert a DateTime into a string format acceptable to CakeMail
	/// </summary>
	[ComVisible(false)]
	internal class CakeMailDateTimeConverter : DateTimeConverterBase
	{
		/// <summary>
		/// Writes the JSON representation of the DateTime value
		/// </summary>
		/// <param name="writer">The Newtonsoft.Json.JsonWriter to write to</param>
		/// <param name="value">The value</param>
		/// <param name="serializer">The calling serializer</param>
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value is DateTime)
			{
				var val = (DateTime)value;
				writer.WriteValue(val.ToCakeMailString());
			}
			else
			{
				throw new Exception("Expected date object value.");
			}
		}

		/// <summary>
		/// Reads the JSON representation of the CakeMail DateTime
		/// </summary>
		/// <param name="reader">The Newtonsoft.Json.JsonReader to read from</param>
		/// <param name="objectType">Type of the object</param>
		/// <param name="existingValue">The existing value of object being read</param>
		/// <param name="serializer">The calling serializer</param>
		/// <returns>The DateTime value</returns>
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return (DateTime?)null;
			if (reader.TokenType != JsonToken.String) throw new Exception("Wrong Token Type");

			var dateAsString = (string)reader.Value;
			var date = dateAsString == Constants.EMPTY_CAKEMAIL_DATE ? DateTime.MinValue : DateTime.Parse(dateAsString);

			return date;
		}

		/// <summary>
		/// Determines whether this instance can convert the specified object type.
		/// </summary>
		/// <param name="objectType">The type of object</param>
		/// <returns>true if this instance can convert the specified object type; otherwise, false.</returns>
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
		}
	}
}
