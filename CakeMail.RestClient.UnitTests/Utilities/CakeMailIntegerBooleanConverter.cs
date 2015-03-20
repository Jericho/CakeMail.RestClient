using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CakeMail.RestCLient.UnitTests.Utilities
{
	[TestClass]
	public class CakeMailIntegerBooleanConverterTests
	{
		[TestMethod]
		public void CakeMailIntegerBooleanConverter_Successfully_parses_value_representing_true()
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
		public void CakeMailIntegerBooleanConverter_Successfully_parses_value_representing_false()
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
		public void CakeMailIntegerBooleanConverter_Throws_exception_when_content_is_not_a_string()
		{
			// Arrange
			var json = "1234";

			var converter = new CakeMailIntegerBooleanConverter();
			var reader = new JsonTextReader(new StringReader(json));
			reader.Read();

			// Act
			var result = (bool)converter.ReadJson(reader, typeof(DateTime), null, null);
		}
	}
}
