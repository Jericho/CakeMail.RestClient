using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace CakeMail.RestClient.Utilities
{
	public class CakeMailIntegerBooleanConverter : DateTimeConverterBase
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value is bool)
			{
				var val = (bool)value;
				writer.WriteValue(val ? 1 : 0);
			}
			else
			{
				throw new Exception("Expected boolean object value.");
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.String) throw new Exception("Wrong Token Type");

			var booleanAsString = (string)reader.Value;
			var boolean = (booleanAsString == "1");

			return boolean;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(bool);
		}
	}
}