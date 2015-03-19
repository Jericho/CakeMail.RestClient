using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace CakeMail.RestClient.Utilities
{
	public class CakeMailDateTimeConverter : DateTimeConverterBase
	{
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

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return (DateTime?)null;
			if (reader.TokenType != JsonToken.String) throw new Exception("Wrong Token Type");

			var dateAsString = (string)reader.Value;
			var date = (dateAsString == Constants.EMPTY_CAKEMAIL_DATE ? DateTime.MinValue : DateTime.Parse(dateAsString));
			return date;
		}
	}
}