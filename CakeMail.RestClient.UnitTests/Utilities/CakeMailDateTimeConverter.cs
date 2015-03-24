using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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
			Assert.AreEqual(2015, result.Year);
			Assert.AreEqual(3, result.Month);
			Assert.AreEqual(11, result.Day);
			Assert.AreEqual(15, result.Hour);
			Assert.AreEqual(21, result.Minute);
			Assert.AreEqual(0, result.Second);
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
			Assert.AreEqual(DateTime.MinValue, result);
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
			Assert.IsTrue(result);
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
			Assert.IsTrue(result);
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
			Assert.IsFalse(result);
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
			using (var jsonWriter = new JsonTextWriter(sw))
			{
				// Act
				converter.WriteJson(jsonWriter, value, null);

				// Assert
				Assert.AreEqual(expected, sb.ToString());
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
