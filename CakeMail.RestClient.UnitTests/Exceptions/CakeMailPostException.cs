using CakeMail.RestClient.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CakeMail.RestClient.UnitTests.Utilities
{
	[TestClass]
	public class CakeMailPostExceptionTests
	{
		[TestMethod]
		public void CakeMailPostException_Constructor_with_message_and_postdata()
		{
			// Arrange
			var message = "This is a dummy message";
			var postData = "This is a sample";

			// Act
			var exception = new CakeMailPostException(message, postData);

			// Assert
			Assert.AreEqual(message, exception.Message);
			Assert.AreEqual(postData, exception.PostData);
			Assert.IsNull(exception.InnerException);
		}

		[TestMethod]
		public void CakeMailPostException_Constructor_with_message_and_postdata_and_innerexception()
		{
			// Arrange
			var message = "This is a dummy message";
			var postData = "This is a sample";
			var innerException = new Exception("Testing");

			// Act
			var exception = new CakeMailPostException(message, postData, innerException);

			// Assert
			Assert.AreEqual(message, exception.Message);
			Assert.AreEqual(postData, exception.PostData);
			Assert.IsNotNull(exception.InnerException);
			Assert.AreEqual(innerException.Message, exception.InnerException.Message);
		}
	}
}
