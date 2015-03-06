using Newtonsoft.Json;
using System;

namespace CakeMail.RestClient.Utilities
{
	class CakeMailCountOfRecordsConverter : JsonConverter
	{
		public override bool CanRead { get { return true; } }
		public override bool CanWrite { get { return false; } }

		public override bool CanConvert(Type objectType)
		{
			return true;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			// The Json returned by CakeMail when counting the number of recods looks like this:
			//	{
			//		count: 1234
			//	}
			// The goal of this JsonConverter is to ignore the "count" property and to return the numerical value.

			if (reader.TokenType == JsonToken.StartObject)
			{
				// Read the next token, presumably the 'count' property
				reader.Read();

				// Make sure the token we just read is the expected property
				if (reader.TokenType != JsonToken.PropertyName) throw new JsonSerializationException(string.Format("Expected a property containing a numerical value. Instead found a {0}", reader.TokenType));
				if (!reader.Value.Equals("count")) throw new JsonSerializationException(string.Format("Expected a property called 'count'. Instead found {0}", reader.Value));

				// Read the next token, presumably the start of the array
				reader.Read();

				// Parse the numerical value
				var itemsSerializer = new JsonSerializer();
				return itemsSerializer.Deserialize(reader, objectType);
			}

			// The Json does not seem to contain the 'count' property
			throw new JsonSerializationException("Json does not seem to contain the 'count' property");
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}