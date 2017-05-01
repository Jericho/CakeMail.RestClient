using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CakeMail.RestClient.Utilities
{
	public static class CakeMailMergeFieldsParser
	{
		private static readonly Regex _currentDateRegex = new Regex(@"\[(NOW|TODAY|DATE)\s*(?:\|\s*(.*?))?\]", RegexOptions.Compiled);
		private static readonly Regex _mergeFieldsRegex = new Regex(@"\[(.*?)\s*(?:\|\s*(.*?))?\]", RegexOptions.Compiled);

		public static string Parse(string content, IDictionary<string, object> data)
		{
			if (string.IsNullOrEmpty(content))
			{
				return string.Empty;
			}

			var mergedContent = _currentDateRegex.Replace(content, (Match m) => CurrentDateMatchEval(m));
			mergedContent = _mergeFieldsRegex.Replace(mergedContent, (Match m) => MatchEval(m, data));

			return mergedContent;
		}

		private static string CurrentDateMatchEval(Match m)
		{
			var format = (m.Groups.Count >= 3 ? m.Groups[2] : null)?.Value ?? string.Empty;
			var dateAsString = DateTime.UtcNow.ToString(format);

			return dateAsString;
		}

		private static string MatchEval(Match m, IDictionary<string, object> data)
		{
			var group1 = m.Groups[1].Value.Split(',');
			var group2 = (m.Groups.Count >= 3 ? m.Groups[2] : null);

			var fieldName = group1[0].Trim();
			var fallbackValue = (group1.Length >= 2 ? group1[1] : string.Empty).Trim();
			var format = group2?.Value?.Trim() ?? string.Empty;

			if (data != null && data.ContainsKey(fieldName))
			{
				if (data[fieldName] is DateTime) return ((DateTime)data[fieldName]).ToString(format);
				else if (data[fieldName] is short) return ((short)data[fieldName]).ToString(format);
				else if (data[fieldName] is int) return ((int)data[fieldName]).ToString(format);
				else if (data[fieldName] is long) return ((long)data[fieldName]).ToString(format);
				else if (data[fieldName] is decimal) return ((decimal)data[fieldName]).ToString(format);
				else if (data[fieldName] is float) return ((float)data[fieldName]).ToString(format);
				else if (data[fieldName] is double) return ((double)data[fieldName]).ToString(format);
				else return data[fieldName]?.ToString() ?? fallbackValue;
			}
			else
			{
				return fallbackValue;
			}
		}
	}
}
