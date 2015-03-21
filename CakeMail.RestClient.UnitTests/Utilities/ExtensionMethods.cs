using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CakeMail.RestClient.UnitTests.Utilities
{
	[TestClass]
	public class ExtensionMethodsTests
	{
		[TestMethod]
		public void ToCakeMailString_handles_DateTime_MinValue()
		{
			// Arrange
			var date = DateTime.MinValue;

			// Act
			var result = date.ToCakeMailString();

			// Assert
			Assert.AreEqual(Constants.EMPTY_CAKEMAIL_DATE, result);
		}

		[TestMethod]
		public void ToCakeMailString_handles_DateTime()
		{
			// Arrange
			var date = new DateTime(2015, 3, 20, 17, 41, 59, 123, DateTimeKind.Utc);

			// Act
			var result = date.ToCakeMailString();

			// Assert
			Assert.AreEqual("2015-03-20 17:41:59", result);
		}
	}
}
