using Newtonsoft.Json;
using System;

namespace CakeMail.RestClient.Utilities
{
	/// <summary>
	/// The Json for an array returned by CakeMail looks like this:
	///	{
	///		name_of_array_property: [ { ... item 1 ...} { ... item 2... } ]
	///	}
	/// The goal of this JsonConverter is to ignore the "array property" and to return an array of items.
	///
	/// Also, an empty array looks like this:
	///	{
	///		[]
	///	}
	/// This JsonConverter returns an empty array in this scenario
	/// </summary>
	public class CakeMailArrayConverter : JsonConverter
	{
		private string _arrayPropertyName;

		public override bool CanRead { get { return true; } }
		public override bool CanWrite { get { return false; } }

		public CakeMailArrayConverter(string arrayPropertyName)
		{
			_arrayPropertyName = arrayPropertyName;
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			// The token type will be 'StartObject' when the items returned by CakeMail are in a "array property". 
			// In the case of an empty array, the token type will be 'StartArray' since CakeMail omits the "array property".
			if (reader.TokenType == JsonToken.StartObject)
			{
				// Read the next token, presumably the property containing the items
				reader.Read();

				// Make sure the token we just read is the expected property
				if (reader.TokenType != JsonToken.PropertyName) throw new JsonSerializationException(string.Format("Expected a property containing an array of items. Instead found a {0}", reader.TokenType));
				if (!reader.Value.Equals(_arrayPropertyName)) throw new JsonSerializationException(string.Format("Expected a property called {0}. Instead found {1}", _arrayPropertyName, reader.Value));

				// Read the next token, presumably the numerical value
				reader.Read();

				// Parse the numerical value
				var itemSerializer = new JsonSerializer();
				return itemSerializer.Deserialize(reader, objectType);
			}

			// Make sure the property contains an array of items
			if (reader.TokenType == JsonToken.StartArray)
			{
				var itemsSerializer = new JsonSerializer();
				return itemsSerializer.Deserialize(reader, objectType);
			}

			// The Json does not seem to contain a CakeMail array
			throw new JsonSerializationException("Json does not seem to contain CakeMail array");
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}