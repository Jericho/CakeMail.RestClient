using CakeMail.RestClient.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace CakeMail.RestClient.UnitTests.Utilities
{
	[TestClass]
	public class HttpExceptionTests
	{
		[TestMethod]
		public void HttpException_Constructor_with_message_and_status_and_uri()
		{
			// Arrange
			var message = "This is a dummy message";
			var httpStatus = HttpStatusCode.Ambiguous;
			var uri = new Uri("http://unittesting.com", UriKind.Absolute);

			// Act
			var exception = new HttpException(message, httpStatus, uri);

			// Assert
			Assert.AreEqual(message, exception.Message);
			Assert.AreEqual(httpStatus, exception.HttpStatus);
			Assert.AreEqual(uri, exception.Uri);
			Assert.IsNull(exception.InnerException);
		}

		[TestMethod]
		public void HttpException_Constructor_with_message_and_status_and_uri_and_innerexception()
		{
			// Arrange
			var message = "This is a dummy message";
			var httpStatus = HttpStatusCode.Ambiguous;
			var uri = new Uri("http://unittesting.com", UriKind.Absolute);
			var innerException = new Exception("Testing");

			// Act
			var exception = new HttpException(message, httpStatus, uri, innerException);

			// Assert
			Assert.AreEqual(message, exception.Message);
			Assert.AreEqual(httpStatus, exception.HttpStatus);
			Assert.AreEqual(uri, exception.Uri);
			Assert.IsNotNull(exception.InnerException);
			Assert.AreEqual(innerException.Message, exception.InnerException.Message);
		}
	}
}
