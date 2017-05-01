﻿using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Utilities
{
	public class CakeMailMergeFieldsParserTests
	{
		[Fact]
		public void Returns_empty_string_when_content_is_null()
		{
			// Arrange
			var content = (string)null;

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, null);

			// Assert
			result.ShouldBeEmpty();
		}

		[Fact]
		public void Returns_empty_string_when_content_is_empty_string()
		{
			// Arrange
			var content = string.Empty;

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, null);

			// Assert
			result.ShouldBeEmpty();
		}

		[Fact]
		public void Field_without_fallback_is_successfully_merged_when_data_provided()
		{
			// Arrange
			var content = "Dear [firstname]";
			var data = new Dictionary<string, object>
			{
				{ "firstname", "Bob" }
			};

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, data);

			// Assert
			result.ShouldBe("Dear Bob");
		}

		[Fact]
		public void Field_without_fallback_is_merged_with_empty_string_when_data_is_omitted()
		{
			// Arrange
			var content = "Dear [firstname]";
			var data = new Dictionary<string, object>
			{
				{ "lastname", "Smith" }
			};

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, data);

			// Assert
			result.ShouldBe("Dear ");
		}

		[Fact]
		public void Fallback_is_merged_when_data_is_omitted()
		{
			// Arrange
			var content = "Dear [firstname,friend]";
			var data = new Dictionary<string, object>
			{
				{ "lastname", "Smith" }
			};

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, data);

			// Assert
			result.ShouldBe("Dear friend");
		}

		[Fact]
		public void Fallback_is_merged_when_null_value_is_specified()
		{
			// Arrange
			var content = "Dear [firstname,friend]";
			var data = new Dictionary<string, object>
			{
				{ "firstname", null }
			};

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, data);

			// Assert
			result.ShouldBe("Dear friend");
		}

		[Fact]
		public void Datetime_with_format()
		{
			// Arrange
			var content = "Thank you for being a customer since [customer_since | yyyy]";
			var data = new Dictionary<string, object>
			{
				{ "customer_since", new DateTime(2017, 4, 30, 4, 57, 0) }
			};

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, data);

			// Assert
			result.ShouldBe("Thank you for being a customer since 2017");
		}

		[Fact]
		public void Short_with_format()
		{
			// Arrange
			var content = "We drove [miles_driven|#,#] miles during this trip";
			var data = new Dictionary<string, object>
			{
				{ "miles_driven", (short)12345 }
			};

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, data);

			// Assert
			result.ShouldBe("We drove 12,345 miles during this trip");
		}

		[Fact]
		public void Int_with_format()
		{
			// Arrange
			var content = "We drove [miles_driven|#,#] miles during this trip";
			var data = new Dictionary<string, object>
			{
				{ "miles_driven", (int)12345 }
			};

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, data);

			// Assert
			result.ShouldBe("We drove 12,345 miles during this trip");
		}

		[Fact]
		public void Long_with_format()
		{
			// Arrange
			var content = "We drove [miles_driven|#,#] miles during this trip";
			var data = new Dictionary<string, object>
			{
				{ "miles_driven", (long)12345 }
			};

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, data);

			// Assert
			result.ShouldBe("We drove 12,345 miles during this trip");
		}

		[Fact]
		public void Decimal_with_format()
		{
			// Arrange
			var content = "We drove [miles_driven|0.00] miles today";
			var data = new Dictionary<string, object>
			{
				{ "miles_driven", (decimal)12.345 }
			};

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, data);

			// Assert
			result.ShouldBe("We drove 12.35 miles today");
		}

		[Fact]
		public void Float_with_format()
		{
			// Arrange
			var content = "We drove [miles_driven|0.00] miles today";
			var data = new Dictionary<string, object>
			{
				{ "miles_driven", (float)12.345 }
			};

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, data);

			// Assert
			result.ShouldBe("We drove 12.35 miles today");
		}

		[Fact]
		public void Double_with_format()
		{
			// Arrange
			var content = "We drove [miles_driven|0.00] miles today";
			var data = new Dictionary<string, object>
			{
				{ "miles_driven", (double)12.345 }
			};

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, data);

			// Assert
			result.ShouldBe("We drove 12.35 miles today");
		}

		[Fact]
		public void DATE_without_format()
		{
			// Arrange
			var content = "The current date is: [DATE]";

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, null);

			// Assert
			Assert.StartsWith("The current date is: ", content);
		}

		[Fact]
		public void NOW_without_format()
		{
			// Arrange
			var content = "The current date is: [NOW]";

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, null);

			// Assert
			Assert.StartsWith("The current date is: ", content);
		}

		[Fact]
		public void TODAY_without_format()
		{
			// Arrange
			var content = "The current date is: [TODAY]";

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, null);

			// Assert
			Assert.StartsWith("The current date is: ", content);
		}

		[Fact]
		public void DATE_with_format()
		{
			// Arrange
			var content = "The current year is: [DATE | yyyy]";

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, null);

			// Assert
			result.ShouldBe($"The current year is: {DateTime.UtcNow.Year}");
		}

		[Fact]
		public void NOW_with_format()
		{
			// Arrange
			var content = "The current year is: [NOW|yyyy]";

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, null);

			// Assert
			result.ShouldBe($"The current year is: {DateTime.UtcNow.Year}");
		}

		[Fact]
		public void TODAY_with_format()
		{
			// Arrange
			var content = "The current year is: [TODAY |yyyy]";

			// Act
			var result = CakeMailMergeFieldsParser.Parse(content, null);

			// Assert
			result.ShouldBe($"The current year is: {DateTime.UtcNow.Year}");
		}
	}
}