using CakeMail.RestClient.Utilities;
using Shouldly;
using System;
using System.Runtime.Serialization;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Utilities
{
	public class ExtensionMethodsTests
	{
		private enum UnitTestingEnum
		{
			AAA,
			[EnumMember(Value = "BBB")]
			BBB
		}

		[Fact]
		public void ToCakeMailString_handles_DateTime_MinValue()
		{
			// Arrange
			var date = DateTime.MinValue;

			// Act
			var result = date.ToCakeMailString();

			// Assert
			result.ShouldBe(Constants.EMPTY_CAKEMAIL_DATE);
		}

		[Fact]
		public void ToCakeMailString_handles_DateTime()
		{
			// Arrange
			var date = new DateTime(2015, 3, 20, 17, 41, 59, 123, DateTimeKind.Utc);

			// Act
			var result = date.ToCakeMailString();

			// Assert
			result.ShouldBe("2015-03-20 17:41:59");
		}

		[Fact]
		public void GetEnumMemberValue_handles_zero_attributes()
		{
			// Arrange
			var value = UnitTestingEnum.AAA;

			// Act
			var result = value.GetEnumMemberValue();

			// Assert
			result.ShouldBe("AAA");
		}

		[Fact]
		public void GetEnumMemberValue_handles_attribute()
		{
			// Arrange
			var value = UnitTestingEnum.BBB;

			// Act
			var result = value.GetEnumMemberValue();

			// Assert
			result.ShouldBe("BBB");
		}

		[Fact]
		public void GetValueFromEnumMember_thows_exception_when_invalid_type()
		{
			// Arrange
			var enumMember = "BBB";

			// Act
			Should.Throw<Exception>(() =>
			{
				var result = enumMember.GetValueFromEnumMember<int>();
			});

			// Assert
			// Nothing to assert, an exception will be thrown
		}

		[Fact]
		public void GetValueFromEnumMember_handles_value_matching_attribute()
		{
			// Arrange
			var enumMember = "BBB";

			// Act
			var result = enumMember.GetValueFromEnumMember<UnitTestingEnum>();

			// Assert
			result.ShouldBe(UnitTestingEnum.BBB);
		}

		[Fact]
		public void GetValueFromEnumMember_handles_value_matching_enum_name()
		{
			// Arrange
			var enumMember = "AAA";

			// Act
			var result = enumMember.GetValueFromEnumMember<UnitTestingEnum>();

			// Assert
			result.ShouldBe(UnitTestingEnum.AAA);
		}

		[Fact]
		public void GetValueFromEnumMember_thows_exception_when_no_match()
		{
			// Arrange
			var enumMember = "CCC";

			// Act
			Should.Throw<Exception>(() =>
			{
				var result = enumMember.GetValueFromEnumMember<UnitTestingEnum>();
			});

			// Assert
			// Nothing to assert, an exception will be thrown
		}
	}
}
