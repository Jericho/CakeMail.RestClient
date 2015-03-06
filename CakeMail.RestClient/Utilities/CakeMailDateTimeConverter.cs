using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Utilities
{
	[CLSCompliant(false)]
	public class CakeMailDateTimeConverter : DateTimeConverterBase
	{
		private const string EMPTY_DATE = "0000-00-00 00:00:00";

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value is DateTime)
			{
				var val = (DateTime)value;
				if (val == DateTime.MinValue) writer.WriteValue(EMPTY_DATE);
				else writer.WriteValue(val.ToString("yyyy-MM-dd HH:mm:ss"));
			}
			else
			{
				throw new Exception("Expected date object value.");
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.String)
				throw new Exception("Wrong Token Type");

			var dateAsString = (string)reader.Value;
			var date = (dateAsString == EMPTY_DATE ? DateTime.MinValue : DateTime.Parse(dateAsString));
			return date;
		}
	}
}