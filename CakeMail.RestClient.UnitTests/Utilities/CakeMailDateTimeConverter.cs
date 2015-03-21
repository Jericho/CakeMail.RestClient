using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CakeMail.RestClient.UnitTests.Utilities
{
	[TestClass]
	public class CakeMailDateTimeConverterTests
	{
		[TestMethod]
		public void CakeMailDateTimeConverter_Successfully_parses_json()
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
		public void CakeMailDateTimeConverter_Successfully_parses_empty_date()
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
		public void CakeMailDateTimeConverter_Throws_exception_when_content_is_not_a_string()
		{
			// Arrange
			var json = "1234";

			var converter = new CakeMailDateTimeConverter();
			var reader = new JsonTextReader(new StringReader(json));
			reader.Read();

			// Act
			var result = (DateTime)converter.ReadJson(reader, typeof(DateTime), null, null);
		}
	}
}
