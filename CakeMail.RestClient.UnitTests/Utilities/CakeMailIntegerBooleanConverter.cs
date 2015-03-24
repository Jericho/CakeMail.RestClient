using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace CakeMail.RestClient.UnitTests.Utilities
{
	[TestClass]
	public class CakeMailIntegerBooleanConverterTests
	{
		[TestMethod]
		public void CakeMailIntegerBooleanConverter_ReadJson_Successfully_parses_value_representing_true()
		{
			// Arrange
			var json = "\"1\"";

			var converter = new CakeMailIntegerBooleanConverter();
			var reader = new JsonTextReader(new StringReader(json));
			reader.Read();

			// Act
			var result = (bool)converter.ReadJson(reader, typeof(bool), null, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void CakeMailIntegerBooleanConverter_ReadJson_Successfully_parses_value_representing_false()
		{
			// Arrange
			var json = "\"this value is not '1' therefore it should be interpreted as false\"";

			var converter = new CakeMailIntegerBooleanConverter();
			var reader = new JsonTextReader(new StringReader(json));
			reader.Read();

			// Act
			var result = (bool)converter.ReadJson(reader, typeof(bool), null, null);

			// Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void CakeMailIntegerBooleanConverter_ReadJson_Throws_exception_when_content_is_not_a_string()
		{
			// Arrange
			var json = "1234";

			var converter = new CakeMailIntegerBooleanConverter();
			var reader = new JsonTextReader(new StringReader(json));
			reader.Read();

			// Act
			var result = (bool)converter.ReadJson(reader, typeof(DateTime), null, null);
		}

		[TestMethod]
		public void CakeMailIntegerBooleanConverter_CanConvert_returns_true()
		{
			// Arrange
			var type = typeof(bool);

			var converter = new CakeMailIntegerBooleanConverter();

			// Act
			var result = converter.CanConvert(type);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void CakeMailIntegerBooleanConverter_CanConvert_returns_false()
		{
			// Arrange
			var type = typeof(string);

			var converter = new CakeMailIntegerBooleanConverter();

			// Act
			var result = converter.CanConvert(type);

			// Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void CakeMailIntegerBooleanConverter_WriteJson_Successfully_writes_value_representing_true()
		{
			// Arrange
			var converter = new CakeMailIntegerBooleanConverter();
			var value = true;
			var expected = "1";

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
		public void CakeMailIntegerBooleanConverter_WriteJson_Successfully_writes_value_representing_false()
		{
			// Arrange
			var converter = new CakeMailIntegerBooleanConverter();
			var value = false;
			var expected = "0";

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
		public void CakeMailIntegerBooleanConverter_WriteJson_Throws_exception_when_content_is_not_boolean()
		{
			// Arrange
			var converter = new CakeMailIntegerBooleanConverter();
			var value = "This is not a boolean value";

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
