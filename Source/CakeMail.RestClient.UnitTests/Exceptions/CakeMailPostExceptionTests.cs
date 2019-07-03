using CakeMail.RestClient.Exceptions;
using Shouldly;
using System;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Exceptions
{
	public class CakeMailPostExceptionTests
	{
		[Fact]
		public void CakeMailPostException_Constructor_with_message_and_postdata_and_diagnostic()
		{
			// Arrange
			var message = "This is a dummy message";
			var postData = "This is a sample";
			var diagnosticLog = "This is the diagnostic log";

			// Act
			var exception = new CakeMailPostException(message, postData, diagnosticLog);

			// Assert
			exception.Message.ShouldBe(message);
			exception.PostData.ShouldBe(postData);
			exception.DiagnosticLog.ShouldBe(diagnosticLog);
			exception.InnerException.ShouldBeNull();
		}

		[Fact]
		public void CakeMailPostException_Constructor_with_message_and_postdata_and_diagnostic_and_innerexception()
		{
			// Arrange
			var message = "This is a dummy message";
			var postData = "This is a sample";
			var diagnosticLog = "This is the diagnostic log";
			var innerException = new Exception("Testing");

			// Act
			var exception = new CakeMailPostException(message, postData, diagnosticLog, innerException);

			// Assert
			exception.Message.ShouldBe(message);
			exception.PostData.ShouldBe(postData);
			exception.DiagnosticLog.ShouldBe(diagnosticLog);
			exception.InnerException.ShouldNotBeNull();
			exception.InnerException.Message.ShouldBe(innerException.Message);
		}
	}
}
