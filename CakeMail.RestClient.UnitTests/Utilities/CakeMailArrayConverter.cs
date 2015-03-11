using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace CakeMail.RestCLient.UnitTests.Utilities
{
	[TestClass]
	public class CakeMailArrayConverterTests
	{
		[TestMethod]
		public void Successfully_parse_json_containing_array()
		{
			// Arrange
			var json = "{ \"tests\": [ { \"Name\" : \"First\", \"Value\" : \"123\" }, { \"FirstName\" : \"Bob\", \"LastName\" : \"Smith\", \"Phone\" : \"888-555-1212\" } ] }";

			var converter = new CakeMailArrayConverter("tests");
			var reader = new JsonTextReader(new StringReader(json));
			reader.Read();

			// Act
			var result = (ExpandoObject[])converter.ReadJson(reader, typeof(ExpandoObject[]), null, null);

			// Assert
			Assert.AreEqual(2, result.Length);

			CollectionAssert.AreEqual(new[] { "Name", "Value" }, result[0].Select(x => x.Key).ToArray());
			CollectionAssert.AreEqual(new[] { "First", "123" }, result[0].Select(x => x.Value).ToArray());

			CollectionAssert.AreEqual(new[] { "FirstName", "LastName", "Phone" }, result[1].Select(x => x.Key).ToArray());
			CollectionAssert.AreEqual(new[] { "Bob", "Smith", "888-555-1212" }, result[1].Select(x => x.Value).ToArray());
		}

		[TestMethod]
		public void Successfully_parse_json_containing_empty_array()
		{
			// Arrange
			var json = "{ \"tests\": [ ] }";

			var converter = new CakeMailArrayConverter("tests");
			var reader = new JsonTextReader(new StringReader(json));
			reader.Read();

			// Act
			var result = (ExpandoObject[])converter.ReadJson(reader, typeof(ExpandoObject[]), null, null);

			// Assert
			Assert.AreEqual(0, result.Length);
		}

		[TestMethod]
		public void Successfully_parse_json_containing_empty_cakemail_array()
		{
			// Arrange
			var json = "{[]}";

			var converter = new CakeMailArrayConverter("tests");
			var reader = new JsonTextReader(new StringReader(json));
			reader.Read();

			// Act
			var result = (ExpandoObject[])converter.ReadJson(reader, typeof(ExpandoObject[]), null, null);

			// Assert
			Assert.AreEqual(0, result.Length);
		}
	}
}
