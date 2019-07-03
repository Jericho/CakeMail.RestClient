using CakeMail.RestClient.Exceptions;
using Shouldly;
using System;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Exceptions
{
	public class CakeMailExceptionTests
	{
		[Fact]
		public void CakeMailException_Constructor_with_message()
		{
			// Arrange
			var message = "This is a dummy message";
			var diagnosticLog = "This is the diagnostic log";

			// Act
			var exception = new CakeMailException(message, diagnosticLog);

			// Assert
			exception.Message.ShouldBe(message);
			exception.DiagnosticLog.ShouldBe(diagnosticLog);
			exception.InnerException.ShouldBeNull();
		}

		[Fact]
		public void CakeMailException_Constructor_with_message_and_innerexception()
		{
			// Arrange
			var message = "This is a dummy message";
			var diagnosticLog = "This is the diagnostic log";
			var innerException = new Exception("Testing");

			// Act
			var exception = new CakeMailException(message, diagnosticLog, innerException);

			// Assert
			exception.Message.ShouldBe(message);
			exception.DiagnosticLog.ShouldBe(diagnosticLog);
			exception.InnerException.ShouldNotBeNull();
			exception.InnerException.Message.ShouldBe(innerException.Message);
		}
	}
}
