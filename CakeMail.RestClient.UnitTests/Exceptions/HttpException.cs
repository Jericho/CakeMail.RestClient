using CakeMail.RestClient.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
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
			exception.Message.ShouldBe(message);
			exception.HttpStatus.ShouldBe(httpStatus);
			exception.Uri.ShouldBe(uri);
			exception.InnerException.ShouldBeNull();
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
			exception.Message.ShouldBe(message);
			exception.HttpStatus.ShouldBe(httpStatus);
			exception.Uri.ShouldBe(uri);
			exception.InnerException.ShouldNotBeNull();
			exception.InnerException.Message.ShouldBe(innerException.Message);
		}
	}
}
