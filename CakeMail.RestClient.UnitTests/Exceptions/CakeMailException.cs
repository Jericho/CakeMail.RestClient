using CakeMail.RestClient.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Shouldly;

namespace CakeMail.RestClient.UnitTests.Utilities
{
	[TestClass]
	public class CakeMailExceptionTests
	{
		[TestMethod]
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

		[TestMethod]
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
