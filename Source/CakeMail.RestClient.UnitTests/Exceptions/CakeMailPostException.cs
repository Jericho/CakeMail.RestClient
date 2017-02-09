using CakeMail.RestClient.Exceptions;
using Shouldly;
using System;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Utilities
{
	public class CakeMailPostExceptionTests
	{
		[Fact]
		public void CakeMailPostException_Constructor_with_message_and_postdata()
		{
			// Arrange
			var message = "This is a dummy message";
			var postData = "This is a sample";

			// Act
			var exception = new CakeMailPostException(message, postData);

			// Assert
			exception.Message.ShouldBe(message);
			exception.PostData.ShouldBe(postData);
			exception.InnerException.ShouldBeNull();
		}

		[Fact]
		public void CakeMailPostException_Constructor_with_message_and_postdata_and_innerexception()
		{
			// Arrange
			var message = "This is a dummy message";
			var postData = "This is a sample";
			var innerException = new Exception("Testing");

			// Act
			var exception = new CakeMailPostException(message, postData, innerException);

			// Assert
			exception.Message.ShouldBe(message);
			exception.PostData.ShouldBe(postData);
			exception.InnerException.ShouldNotBeNull();
			exception.InnerException.Message.ShouldBe(innerException.Message);
		}
	}
}
