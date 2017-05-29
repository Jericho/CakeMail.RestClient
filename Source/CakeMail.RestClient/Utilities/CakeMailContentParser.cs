using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CakeMail.RestClient.Utilities
{
	public static class CakeMailContentParser
	{
		private const string DYNAMIC_CONTENT_START_TAG = @"\[IF (.*?)\]";
		private const string DYNAMIC_CONTENT_END_TAG = @"\[ENDIF\]";
		private const string DYNAMIC_CONTENT_ELSEIF_TAG = "\\[ELSEIF (.*?)\\]";
		private const string DYNAMIC_CONTENT_ELSE_TAG = "\\[ELSE\\]";
		private const string DYNAMIC_CONTENT_CONDITION_STRING = "`(.*?)` (<=|>=|<|>|!=|==|=) \"(.*?)\"";
		private const string DYNAMIC_CONTENT_CONDITION_NUMERIC = "`(.*?)` (<=|>=|<|>|!=|==|=) (.*?)$";

		private static readonly Regex _currentDateRegex = new Regex(@"\[(NOW|TODAY|DATE)\s*(?:\|\s*(.*?))?\]", RegexOptions.Compiled);
		private static readonly Regex _mergeFieldsRegex = new Regex(@"\[(.*?)\s*(?:\|\s*(.*?))?\]", RegexOptions.Compiled);

		public static string Parse(string content, IDictionary<string, object> data)
		{
			if (string.IsNullOrEmpty(content))
			{
				return string.Empty;
			}

			var mergedContent = ParseDynamicContent(content, data);
			mergedContent = _currentDateRegex.Replace(mergedContent, (Match m) => CurrentDateMatchEval(m));
			mergedContent = _mergeFieldsRegex.Replace(mergedContent, (Match m) => MergeFieldMatchEval(m, data));
			return mergedContent;
		}

		private static string CurrentDateMatchEval(Match m)
		{
			var format = (m.Groups.Count >= 3 ? m.Groups[2] : null)?.Value ?? string.Empty;
			var dateAsString = DateTime.UtcNow.ToString(format);

			return dateAsString;
		}

		private static string MergeFieldMatchEval(Match m, IDictionary<string, object> data)
		{
			var group1 = m.Groups[1].Value.Split(',');
			var group2 = m.Groups.Count >= 3 ? m.Groups[2] : null;

			var fieldName = group1[0].Trim();
			var fallbackValue = (group1.Length >= 2 ? group1[1] : string.Empty).Trim();
			var format = group2?.Value?.Trim() ?? string.Empty;

			var returnValue = FieldDataAsString(fieldName, data, format, fallbackValue);
			return returnValue;
		}

		private static string ParseDynamicContent(string content, IDictionary<string, object> data)
		{
			if (string.IsNullOrEmpty(content)) return string.Empty;

			var matchStartCondition = Regex.Match(content, DYNAMIC_CONTENT_START_TAG);
			var matchEndCondition = Regex.Match(content, DYNAMIC_CONTENT_END_TAG);

			// Make sure there is at least one "IF" statement
			if (!matchStartCondition.Success) return content;

			// Make sure the number of "IF" match the number of "ENDIF"
			if (matchStartCondition.Success != matchEndCondition.Success || matchStartCondition.Captures.Count != matchEndCondition.Captures.Count)
			{
				throw new Exception("Dynamic content does not seem valid. Typically this is caused by a \"IF\" condition missing the corresponding \"ENDIF\"");
			}

			// Make sure the "ENDIF" does not preceed the "IF"
			if (matchEndCondition.Index < matchStartCondition.Index + matchStartCondition.Length) return content;

			var condition = matchStartCondition.Groups[1].Value;
			var conditionalContent = content.Substring(matchStartCondition.Index + matchStartCondition.Length, matchEndCondition.Index - matchStartCondition.Index - matchStartCondition.Length);

			var isTrue = EvaluateCondition(condition, data);
			var parsedConditionalContent = ParseConditionalContent(isTrue, conditionalContent, data);

			var parsedContent = new StringBuilder();
			parsedContent.Append(content.Substring(0, matchStartCondition.Index));
			parsedContent.Append(parsedConditionalContent);
			parsedContent.Append(content.Substring(matchEndCondition.Index + matchEndCondition.Length));

			return ParseDynamicContent(parsedContent.ToString(), data);
		}

		private static string ParseConditionalContent(bool condition, string content, IDictionary<string, object> data)
		{
			if (string.IsNullOrEmpty(content)) return content;

			var matchCondition = Regex.Match(content, DYNAMIC_CONTENT_ELSEIF_TAG);
			if (matchCondition.Success && condition) return content.Substring(0, matchCondition.Index);

			if (!matchCondition.Success)
			{
				matchCondition = Regex.Match(content, DYNAMIC_CONTENT_ELSE_TAG);
				if (matchCondition.Success)
				{
					if (condition) return content.Substring(0, matchCondition.Index);
					else return content.Substring(matchCondition.Index + matchCondition.Length);
				}
				else if (condition)
				{
					return content;
				}
				else
				{
					return string.Empty;
				}
			}

			var isTrue = EvaluateCondition(matchCondition.Groups[1].Value, data);
			var conditionalContent = ParseConditionalContent(isTrue, content.Substring(matchCondition.Index + matchCondition.Length), data);

			return conditionalContent;
		}

		private static bool EvaluateCondition(string condition, IDictionary<string, object> data)
		{
			return EvaluateCondition(condition, " AND ", data);
		}

		private static bool EvaluateCondition(string condition, string logicalOperator, IDictionary<string, object> data)
		{
			if (string.IsNullOrEmpty(condition)) return false;

			var subConditions = Regex.Split(condition, logicalOperator);
			if (subConditions == null || subConditions.Length == 0)
			{
				return false;
			}
			else if (subConditions.Length > 1)
			{
				foreach (var subCondition in subConditions)
				{
					var result = EvaluateCondition(subCondition, data);
					if (logicalOperator == " AND " && !result) return false;
					else if (logicalOperator == " OR " && result) return true;
				}

				return logicalOperator == " AND ";
			}
			else if (logicalOperator == " AND ")
			{
				return EvaluateCondition(condition, " OR ", data);
			}
			else
			{
				return EvaluateSingleCondition(condition, data);
			}
		}

		private static bool EvaluateSingleCondition(string condition, IDictionary<string, object> data)
		{
			var matches = Regex.Match(condition, DYNAMIC_CONTENT_CONDITION_STRING);
			if (matches.Success)
			{
				switch (matches.Groups[2].Value)
				{
					case "=":
					case "==":
						return IsEqual(matches.Groups[1].Value, matches.Groups[3].Value, data);
					case "!=":
						return !IsEqual(matches.Groups[1].Value, matches.Groups[3].Value, data);
					case "LIKE":
						return IsLike(matches.Groups[1].Value, matches.Groups[3].Value, data);
					case "NOT LIKE":
						return !IsLike(matches.Groups[1].Value, matches.Groups[3].Value, data);
					case "<=":
						return IsSmaller(matches.Groups[1].Value, matches.Groups[3].Value, data) || IsEqual(matches.Groups[1].Value, matches.Groups[3].Value, data);
					case ">=":
						return IsGreater(matches.Groups[1].Value, matches.Groups[3].Value, data) || IsEqual(matches.Groups[1].Value, matches.Groups[3].Value, data);
					case "<":
						return IsSmaller(matches.Groups[1].Value, matches.Groups[3].Value, data);
					case ">":
						return IsGreater(matches.Groups[1].Value, matches.Groups[3].Value, data);
				}
			}

			matches = Regex.Match(condition, DYNAMIC_CONTENT_CONDITION_NUMERIC);
			if (matches.Success)
			{
				switch (matches.Groups[2].Value)
				{
					case "=":
					case "==":
						return IsEqualNumeric(matches.Groups[1].Value, matches.Groups[3].Value, data);
					case "!=":
						return !IsEqualNumeric(matches.Groups[1].Value, matches.Groups[3].Value, data);
					case "<=":
						return IsSmallerNumeric(matches.Groups[1].Value, matches.Groups[3].Value, data) || IsEqualNumeric(matches.Groups[1].Value, matches.Groups[3].Value, data);
					case ">=":
						return IsGreaterNumeric(matches.Groups[1].Value, matches.Groups[3].Value, data) || IsEqualNumeric(matches.Groups[1].Value, matches.Groups[3].Value, data);
					case "<":
						return IsSmallerNumeric(matches.Groups[1].Value, matches.Groups[3].Value, data);
					case ">":
						return IsGreaterNumeric(matches.Groups[1].Value, matches.Groups[3].Value, data);
				}
			}

			return false;
		}

		private static bool IsEqual(string fieldName, string value, IDictionary<string, object> data)
		{
			value = value.TrimStart(new char[] { '"', '\'' }).TrimEnd(new char[] { '"', '\'' });
			if (!data.ContainsKey(fieldName)) return string.IsNullOrEmpty(value);
			return FieldDataAsString(fieldName, data, null, null) == value;
		}

		private static bool IsEqualNumeric(string fieldName, string value, IDictionary<string, object> data)
		{
			var fieldData = (object)null;
			data.TryGetValue(fieldName, out fieldData);

			if (fieldData is short) return (short)fieldData == Convert.ToInt16(value);
			else if (fieldData is int) return (int)fieldData == Convert.ToInt32(value);
			else if (fieldData is long) return (long)fieldData == Convert.ToInt64(value);
			else if (fieldData is decimal) return (decimal)fieldData == Convert.ToDecimal(value);

			// Cannot compare float/double because they are stored as binary fractions, not decimal fractions.
			// Explanation: https://stackoverflow.com/questions/21895756/why-are-floating-point-numbers-inaccurate
			// A solution described in this blog post (https://csharp.2000things.com/2011/09/21/416-use-an-epsilon-to-compare-two-floating-point-numbers/) suggests checking if the two values are "nearly" identical
			else if (fieldData is float) return Math.Abs((float)fieldData - Convert.ToSingle(value)) < 0.00001;
			else if (fieldData is double) return Math.Abs((double)fieldData - Convert.ToDouble(value)) < 0.00001;

			return false;
		}

		private static bool IsSmaller(string fieldName, string value, IDictionary<string, object> data)
		{
			if (!data.ContainsKey(fieldName)) return false;
			value = value.TrimStart(new char[] { '"', '\'' }).TrimEnd(new char[] { '"', '\'' });
			return string.Compare(data[fieldName].ToString(), value, StringComparison.Ordinal) < 0;
		}

		private static bool IsSmallerNumeric(string fieldName, string value, IDictionary<string, object> data)
		{
			var fieldData = (object)null;
			data.TryGetValue(fieldName, out fieldData);

			if (fieldData is short) return (short)fieldData < Convert.ToInt16(value);
			else if (fieldData is int) return (int)fieldData < Convert.ToInt32(value);
			else if (fieldData is long) return (long)fieldData < Convert.ToInt64(value);
			else if (fieldData is decimal) return (decimal)fieldData < Convert.ToDecimal(value);

			// When checking if a float/double is smaller than another, we must make sure the two values are not "nearly" identical
			else if (fieldData is float) return (float)fieldData < Convert.ToSingle(value) && Math.Abs((float)fieldData - Convert.ToSingle(value)) > 0.00001;
			else if (fieldData is double) return (double)fieldData < Convert.ToDouble(value) && Math.Abs((double)fieldData - Convert.ToDouble(value)) > 0.00001;

			return false;
		}

		private static bool IsGreater(string fieldName, string value, IDictionary<string, object> data)
		{
			if (!data.ContainsKey(fieldName)) return false;
			value = value.TrimStart(new char[] { '"', '\'' }).TrimEnd(new char[] { '"', '\'' });
			return string.Compare(data[fieldName].ToString(), value, StringComparison.Ordinal) > 0;
		}

		private static bool IsGreaterNumeric(string fieldName, string value, IDictionary<string, object> data)
		{
			var fieldData = (object)null;
			data.TryGetValue(fieldName, out fieldData);

			if (fieldData is short) return (short)fieldData > Convert.ToInt16(value);
			else if (fieldData is int) return (int)fieldData > Convert.ToInt32(value);
			else if (fieldData is long) return (long)fieldData > Convert.ToInt64(value);
			else if (fieldData is decimal) return (decimal)fieldData > Convert.ToDecimal(value);

			// When checking if a float/double is greater than another, we must make sure the two values are not "nearly" identical
			else if (fieldData is float) return (float)fieldData > Convert.ToSingle(value) && Math.Abs((float)fieldData - Convert.ToSingle(value)) > 0.00001;
			else if (fieldData is double) return (double)fieldData > Convert.ToDouble(value) && Math.Abs((double)fieldData - Convert.ToDouble(value)) > 0.00001;

			return false;
		}

		private static bool IsLike(string fieldName, string value, IDictionary<string, object> data)
		{
			if (!data.ContainsKey(fieldName)) return false;
			value = value.TrimStart(new char[] { '"', '\'' }).TrimEnd(new char[] { '"', '\'' });
			var a = Regex.IsMatch(data[fieldName].ToString(), value.Replace("%", "(.*?)"));
			return a;
		}

		private static string FieldDataAsString(string fieldName, IDictionary<string, object> data, string format = null, string fallbackValue = null)
		{
			var fieldData = (object)null;
			data.TryGetValue(fieldName, out fieldData);

			var returnValue = (string)null;
			if (fieldData != null)
			{
				if (fieldData is DateTime) returnValue = ((DateTime)fieldData).ToString(format ?? "yyyy-MM-dd HH-mm-ss");
				else if (fieldData is short) returnValue = ((short)fieldData).ToString(format);
				else if (fieldData is int) returnValue = ((int)fieldData).ToString(format);
				else if (fieldData is long) returnValue = ((long)fieldData).ToString(format);
				else if (fieldData is decimal) returnValue = ((decimal)fieldData).ToString(format);
				else if (fieldData is float) returnValue = ((float)fieldData).ToString(format);
				else if (fieldData is double) returnValue = ((double)fieldData).ToString(format);
				else returnValue = fieldData.ToString();
			}

			return returnValue ?? fallbackValue ?? string.Empty;
		}
	}
}
