using CakeMail.RestClient.Exceptions;
using Shouldly;
using System;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Utilities
{
	public class CakeMailExceptionTests
	{
		[Fact]
		public void CakeMailException_Constructor_with_message()
		{
			// Arrange
			var message = "This is a dummy message";

			// Act
			var exception = new CakeMailException(message);

			// Assert
			exception.Message.ShouldBe(message);
			exception.InnerException.ShouldBeNull();
		}

		[Fact]
		public void CakeMailException_Constructor_with_message_and_innerexception()
		{
			// Arrange
			var message = "This is a dummy message";
			var innerException = new Exception("Testing");

			// Act
			var exception = new CakeMailException(message, innerException);

			// Assert
			exception.Message.ShouldBe(message);
			exception.InnerException.ShouldNotBeNull();
			exception.InnerException.Message.ShouldBe(innerException.Message);
		}
	}
}
