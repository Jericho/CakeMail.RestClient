using CakeMail.RestClient.Exceptions;
using Shouldly;
using System;
using System.Net;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Exceptions
{
	public class HttpExceptionTests
	{
		[Fact]
		public void HttpException_Constructor_with_message_and_status_and_uri()
		{
			// Arrange
			var message = "This is a dummy message";
			var httpStatus = HttpStatusCode.Ambiguous;
			var uri = new Uri("http://unittesting.com", UriKind.Absolute);
			var diagnosticLog = "This is the diagnostic log";

			// Act
			var exception = new HttpException(message, httpStatus, uri, diagnosticLog);

			// Assert
			exception.Message.ShouldBe(message);
			exception.HttpStatus.ShouldBe(httpStatus);
			exception.Uri.ShouldBe(uri);
			exception.DiagnosticLog.ShouldBe(diagnosticLog);
			exception.InnerException.ShouldBeNull();
		}

		[Fact]
		public void HttpException_Constructor_with_message_and_status_and_uri_and_innerexception()
		{
			// Arrange
			var message = "This is a dummy message";
			var httpStatus = HttpStatusCode.Ambiguous;
			var uri = new Uri("http://unittesting.com", UriKind.Absolute);
			var diagnosticLog = "This is the diagnostic log";
			var innerException = new Exception("Testing");

			// Act
			var exception = new HttpException(message, httpStatus, uri, diagnosticLog, innerException);

			// Assert
			exception.Message.ShouldBe(message);
			exception.HttpStatus.ShouldBe(httpStatus);
			exception.Uri.ShouldBe(uri);
			exception.DiagnosticLog.ShouldBe(diagnosticLog);
			exception.InnerException.ShouldNotBeNull();
			exception.InnerException.Message.ShouldBe(innerException.Message);
		}
	}
}
