using CakeMail.RestClient.Utilities;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Utilities
{
	public class CakeMailContentParserTests
	{
		[Fact]
		public void Returns_empty_string_when_content_is_null()
		{
			// Arrange
			var content = (string)null;

			// Act
			var result = CakeMailContentParser.Parse(content, null);

			// Assert
			result.ShouldBeEmpty();
		}

		[Fact]
		public void Returns_empty_string_when_content_is_empty_string()
		{
			// Arrange
			var content = string.Empty;

			// Act
			var result = CakeMailContentParser.Parse(content, null);

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
			var result = CakeMailContentParser.Parse(content, data);

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
			var result = CakeMailContentParser.Parse(content, data);

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
			var result = CakeMailContentParser.Parse(content, data);

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
			var result = CakeMailContentParser.Parse(content, data);

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
			var result = CakeMailContentParser.Parse(content, data);

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
			var result = CakeMailContentParser.Parse(content, data);

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
			var result = CakeMailContentParser.Parse(content, data);

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
			var result = CakeMailContentParser.Parse(content, data);

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
			var result = CakeMailContentParser.Parse(content, data);

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
			var result = CakeMailContentParser.Parse(content, data);

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
			var result = CakeMailContentParser.Parse(content, data);

			// Assert
			result.ShouldBe("We drove 12.35 miles today");
		}

		[Fact]
		public void DATE_without_format()
		{
			// Arrange
			var content = "The current date is: [DATE]";

			// Act
			var result = CakeMailContentParser.Parse(content, null);

			// Assert
			Assert.StartsWith("The current date is: ", content);
		}

		[Fact]
		public void NOW_without_format()
		{
			// Arrange
			var content = "The current date is: [NOW]";

			// Act
			var result = CakeMailContentParser.Parse(content, null);

			// Assert
			Assert.StartsWith("The current date is: ", content);
		}

		[Fact]
		public void TODAY_without_format()
		{
			// Arrange
			var content = "The current date is: [TODAY]";

			// Act
			var result = CakeMailContentParser.Parse(content, null);

			// Assert
			Assert.StartsWith("The current date is: ", content);
		}

		[Fact]
		public void DATE_with_format()
		{
			// Arrange
			var content = "The current year is: [DATE | yyyy]";

			// Act
			var result = CakeMailContentParser.Parse(content, null);

			// Assert
			result.ShouldBe($"The current year is: {DateTime.UtcNow.Year}");
		}

		[Fact]
		public void NOW_with_format()
		{
			// Arrange
			var content = "The current year is: [NOW|yyyy]";

			// Act
			var result = CakeMailContentParser.Parse(content, null);

			// Assert
			result.ShouldBe($"The current year is: {DateTime.UtcNow.Year}");
		}

		[Fact]
		public void TODAY_with_format()
		{
			// Arrange
			var content = "The current year is: [TODAY |yyyy]";

			// Act
			var result = CakeMailContentParser.Parse(content, null);

			// Assert
			result.ShouldBe($"The current year is: {DateTime.UtcNow.Year}");
		}

		[Fact]
		public void IF_true()
		{
			// Arrange
			var content = "[IF `firstname` = \"Bob\"]Yes[ELSE]No[ENDIF]";
			var data = new Dictionary<string, object>
			{
				{ "firstname", "Bob" }
			};

			// Act
			var result = CakeMailContentParser.Parse(content, data);

			// Assert
			result.ShouldBe("Yes");
		}

		[Fact]
		public void IF_false_with_ELSE()
		{
			// Arrange
			var content = "[IF `firstname` = \"Robert\"]Yes[ELSE]No[ENDIF]";
			var data = new Dictionary<string, object>
			{
				{ "firstname", "Bob" }
			};

			// Act
			var result = CakeMailContentParser.Parse(content, data);

			// Assert
			result.ShouldBe("No");
		}

		[Fact]
		public void IF_false_without_ELSE()
		{
			// Arrange
			var content = "[IF `firstname` = \"Robert\"]Yes[ENDIF]";
			var data = new Dictionary<string, object>
			{
				{ "firstname", "Bob" }
			};

			// Act
			var result = CakeMailContentParser.Parse(content, data);

			// Assert
			result.ShouldBeEmpty();
		}

		[Fact]
		public void IF_false_with_ELSEIF_true()
		{
			// Arrange
			var content = "[IF `firstname` = \"Robert\"]Yes - Robert[ELSEIF `firstname` = \"Bob\"]Yes - Bob[ELSE]No[ENDIF]";
			var data = new Dictionary<string, object>
			{
				{ "firstname", "Bob" }
			};

			// Act
			var result = CakeMailContentParser.Parse(content, data);

			// Assert
			result.ShouldBe("Yes - Bob");
		}

		[Fact]
		public void IF_false_with_ELSEIF_false()
		{
			// Arrange
			var content = "[IF `firstname` = \"Robert\"]Yes - Robert[ELSEIF `firstname` = \"Bob\"]Yes - Bob[ELSE]No[ENDIF]";
			var data = new Dictionary<string, object>
			{
				{ "firstname", "Jim" }
			};

			// Act
			var result = CakeMailContentParser.Parse(content, data);

			// Assert
			result.ShouldBe("No");
		}

		[Fact]
		public void Two_conditions_both_true()
		{
			// Arrange
			var content = "[IF `firstname` = \"Jim\" AND `lastname` = \"Halpert\"]Yes[ELSE]No[ENDIF]";
			var data = new Dictionary<string, object>
			{
				{ "firstname", "Jim" },
				{ "lastname", "Halpert" }
			};

			// Act
			var result = CakeMailContentParser.Parse(content, data);

			// Assert
			result.ShouldBe("Yes");
		}

		[Fact]
		public void Two_conditions_one_true_one_false_AND()
		{
			// Arrange
			var content = "[IF `firstname` = \"Jim\" AND `lastname` = \"Halpert\"]Yes[ELSE]No[ENDIF]";
			var data = new Dictionary<string, object>
			{
				{ "firstname", "John" },
				{ "lastname", "Halpert" }
			};

			// Act
			var result = CakeMailContentParser.Parse(content, data);

			// Assert
			result.ShouldBe("No");
		}

		[Fact]
		public void Two_conditions_one_true_one_false_OR()
		{
			// Arrange
			var content = "[IF `firstname` = \"Jim\" OR `lastname` = \"Halpert\"]Yes[ELSE]No[ENDIF]";
			var data = new Dictionary<string, object>
			{
				{ "firstname", "John" },
				{ "lastname", "Halpert" }
			};

			// Act
			var result = CakeMailContentParser.Parse(content, data);

			// Assert
			result.ShouldBe("Yes");
		}

		[Fact]
		public void Numeric_true()
		{
			// Arrange
			var content = "[IF `age` > \"18\"]Yes[ELSE]No[ENDIF]";
			var data = new Dictionary<string, object>
			{
				{ "age", 25 }
			};

			// Act
			var result = CakeMailContentParser.Parse(content, data);

			// Assert
			result.ShouldBe("Yes");
		}

		[Fact]
		public void Numeric_CAKEMAIL_BUG()
		{
			// CakeMail only allows comparing merge fields with string values (e.g.:[ IF `age` >= "18"]content for adults[ELSE]content for minors[ENDIF])
			// which does not work properly in some cases. Let's say we want to display display content for adults aged 18 or over and different content
			// for minors and let's say you have a recipient age 9. We will treat 9 as a string (per the CakeMail logic) and compare it to the string "18".
			// Well... guess what: the character "9" is greater than the character "1" (which is the first character in the string "18") and therefore this
			// person who is clearly underage would receive an email with content intended to adults. Conversely, if you have a recipient age 100, this
			// person would receive an email with the content for minors because "0" (the second character in the string "100") is smaller than "8" (the
			// second character in the string "18").

			// Arrange
			var content = "[IF `age` > \"18\"]Adult[ELSE]Minor[ENDIF]";
			var data = new Dictionary<string, object>
			{
				{ "age", 9 }
			};

			// Act
			var result = CakeMailContentParser.Parse(content, data);

			// Assert
			result.ShouldBe("Adult");
		}

		[Fact]
		public void Numeric_CAKEMAIL_BUG_Fix_false()
		{
			// The CakeMail.RestClient allows comparing merge fields with numeric values (e.g.: [ IF `age` >= 18]content for adults[ELSE]content for minors[ENDIF])
			// which solves the problem I describe in the Numeric_CAKEMAIL_BUG unit test.

			// Arrange
			var content = "[IF `age` >= 18]Adult[ELSE]Minor[ENDIF]";
			var data = new Dictionary<string, object>
			{
				{ "age", 9 }
			};

			// Act
			var result = CakeMailContentParser.Parse(content, data);

			// Assert
			result.ShouldBe("Minor");
		}

		[Fact]
		public void Numeric_CAKEMAIL_BUG_FIX_true()
		{
			// Arrange
			var content = "[IF `age` >= 18]Adult[ELSE]Minor[ENDIF]";
			var data = new Dictionary<string, object>
			{
				{ "age", 18 }
			};

			// Act
			var result = CakeMailContentParser.Parse(content, data);

			// Assert
			result.ShouldBe("Adult");
		}

		[Fact]
		public void Numeric_CAKEMAIL_double()
		{
			// Arrange
			var content = "[IF `amount` == 2.0]Yes[ELSE]No[ENDIF]";
			var data = new Dictionary<string, object>
			{
				{ "amount", Math.Sqrt(2) * Math.Sqrt(2) }
			};

			// Act
			var result = CakeMailContentParser.Parse(content, data);

			// Assert
			result.ShouldBe("Yes");
		}
	}
}
