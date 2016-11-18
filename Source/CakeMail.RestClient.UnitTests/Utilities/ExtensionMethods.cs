using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Runtime.Serialization;

namespace CakeMail.RestClient.UnitTests.Utilities
{
	[TestClass]
	public class ExtensionMethodsTests
	{
		private enum UnitTestingEnum
		{
			AAA,
			[EnumMember(Value = "BBB")]
			BBB
		}

		[TestMethod]
		public void ToCakeMailString_handles_DateTime_MinValue()
		{
			// Arrange
			var date = DateTime.MinValue;

			// Act
			var result = date.ToCakeMailString();

			// Assert
			result.ShouldBe(Constants.EMPTY_CAKEMAIL_DATE);
		}

		[TestMethod]
		public void ToCakeMailString_handles_DateTime()
		{
			// Arrange
			var date = new DateTime(2015, 3, 20, 17, 41, 59, 123, DateTimeKind.Utc);

			// Act
			var result = date.ToCakeMailString();

			// Assert
			result.ShouldBe("2015-03-20 17:41:59");
		}

		[TestMethod]
		public void GetEnumMemberValue_handles_zero_attributes()
		{
			// Arrange
			var value = UnitTestingEnum.AAA;

			// Act
			var result = value.GetEnumMemberValue();

			// Assert
			result.ShouldBe(String.Empty);
		}

		[TestMethod]
		public void GetEnumMemberValue_handles_attribute()
		{
			// Arrange
			var value = UnitTestingEnum.BBB;

			// Act
			var result = value.GetEnumMemberValue();

			// Assert
			result.ShouldBe("BBB");
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void GetValueFromEnumMember_thows_exception_when_invalid_type()
		{
			// Arrange
			var enumMember = "BBB";

			// Act
			var result = enumMember.GetValueFromEnumMember<int>();

			// Assert
			// Nothing to assert, an exception will be thrown
		}

		[TestMethod]
		public void GetValueFromEnumMember_handles_value_matching_attribute()
		{
			// Arrange
			var enumMember = "BBB";

			// Act
			var result = enumMember.GetValueFromEnumMember<UnitTestingEnum>();

			// Assert
			result.ShouldBe(UnitTestingEnum.BBB);
		}

		[TestMethod]
		public void GetValueFromEnumMember_handles_value_matching_enum_name()
		{
			// Arrange
			var enumMember = "AAA";

			// Act
			var result = enumMember.GetValueFromEnumMember<UnitTestingEnum>();

			// Assert
			result.ShouldBe(UnitTestingEnum.AAA);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void GetValueFromEnumMember_thows_exception_when_no_match()
		{
			// Arrange
			var enumMember = "CCC";

			// Act
			var result = enumMember.GetValueFromEnumMember<UnitTestingEnum>();

			// Assert
			// Nothing to assert, an exception will be thrown
		}
	}
}
