using CakeMail.RestClient.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
			Assert.AreEqual(message, exception.Message);
			Assert.IsNull(exception.InnerException);
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
			Assert.AreEqual(message, exception.Message);
			Assert.IsNotNull(exception.InnerException);
			Assert.AreEqual(innerException.Message, exception.InnerException.Message);
		}
	}
}
