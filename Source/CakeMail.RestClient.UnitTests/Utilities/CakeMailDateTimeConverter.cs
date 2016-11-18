using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.IO;
using System.Text;

namespace CakeMail.RestClient.UnitTests.Utilities
{
	[TestClass]
	public class CakeMailDateTimeConverterTests
	{
		[TestMethod]
		public void CakeMailDateTimeConverter_ReadJson_Successfully_parses_json()
		{
			// Arrange
			var json = "\"2015-03-11 15:21:00\"";

			var converter = new CakeMailDateTimeConverter();
			var reader = new JsonTextReader(new StringReader(json));
			reader.Read();

			// Act
			var result = (DateTime)converter.ReadJson(reader, typeof(DateTime), null, null);

			// Assert
			result.Year.ShouldBe(2015);
			result.Month.ShouldBe(3);
			result.Day.ShouldBe(11);
			result.Hour.ShouldBe(15);
			result.Minute.ShouldBe(21);
			result.Second.ShouldBe(0);
		}

		[TestMethod]
		public void CakeMailDateTimeConverter_ReadJson_Successfully_parses_empty_date()
		{
			// Arrange
			var json = "\"0000-00-00 00:00:00\"";

			var converter = new CakeMailDateTimeConverter();
			var reader = new JsonTextReader(new StringReader(json));
			reader.Read();

			// Act
			var result = (DateTime)converter.ReadJson(reader, typeof(DateTime), null, null);

			// Assert
			result.ShouldBe(DateTime.MinValue);
		}

		[TestMethod]
		public void CakeMailDateTimeConverter_ReadJson_Successfully_parses_null_date()
		{
			// Arrange
			var json = "null";

			var converter = new CakeMailDateTimeConverter();
			var reader = new JsonTextReader(new StringReader(json));
			reader.Read();

			// Act
			var result = (DateTime?)converter.ReadJson(reader, typeof(DateTime), null, null);

			// Assert
			result.ShouldBeNull();
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void CakeMailDateTimeConverter_ReadJson_Throws_exception_when_content_is_not_a_string()
		{
			// Arrange
			var json = "1234";

			var converter = new CakeMailDateTimeConverter();
			var reader = new JsonTextReader(new StringReader(json));
			reader.Read();

			// Act
			var result = (DateTime)converter.ReadJson(reader, typeof(DateTime), null, null);
		}

		[TestMethod]
		public void CakeMailDateTimeConverter_CanConvert_datetime()
		{
			// Arrange
			var type = typeof(DateTime);

			var converter = new CakeMailDateTimeConverter();

			// Act
			var result = converter.CanConvert(type);

			// Assert
			result.ShouldBeTrue();
		}

		[TestMethod]
		public void CakeMailDateTimeConverter_CanConvert_nullable_datetime()
		{
			// Arrange
			var type = typeof(DateTime?);

			var converter = new CakeMailDateTimeConverter();

			// Act
			var result = converter.CanConvert(type);

			// Assert
			result.ShouldBeTrue();
		}

		[TestMethod]
		public void CakeMailDateTimeConverter_CanConvert_returns_false()
		{
			// Arrange
			var type = typeof(int);

			var converter = new CakeMailDateTimeConverter();

			// Act
			var result = converter.CanConvert(type);

			// Assert
			result.ShouldBeFalse();
		}

		[TestMethod]
		public void CakeMailDateTimeConverter_WriteJson_Successfully_writes_datetime()
		{
			// Arrange
			var converter = new CakeMailDateTimeConverter();
			var value = new DateTime(2015, 3, 24, 12, 30, 11, 99, DateTimeKind.Utc);
			var expected = "\"2015-03-24 12:30:11\"";

			var sb = new StringBuilder();

			using (var sw = new StringWriter(sb))
			{
				using (var jsonWriter = new JsonTextWriter(sw))
				{
					// Act
					converter.WriteJson(jsonWriter, value, null);

					// Assert
					sb.ToString().ShouldBe(expected);
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void CakeMailDateTimeConverter_WriteJson_Throws_exception_when_content_is_not_boolean()
		{
			// Arrange
			var converter = new CakeMailDateTimeConverter();
			var value = "This is not a DateTime value";

			var sb = new StringBuilder();

			using (var sw = new StringWriter(sb))
			{
				using (var jsonWriter = new JsonTextWriter(sw))
				{
					// Act
					converter.WriteJson(jsonWriter, value, null);

					// Assert
					// Nothing to assert, since 'WriteJson' will throw an exception
				}
			}
		}
	}
}
