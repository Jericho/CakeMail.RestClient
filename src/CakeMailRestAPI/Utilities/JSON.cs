using Newtonsoft.Json;
using System.Diagnostics;

namespace CakeMailRestAPI.Utilities
{
	public class JSON
	{
		#region Static Fields

		private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
		{
			DateFormatHandling = DateFormatHandling.IsoDateFormat,
			DefaultValueHandling = DefaultValueHandling.Ignore,
			NullValueHandling = NullValueHandling.Ignore
		};

		#endregion

		#region Public Methods and Operators

		public static T Parse<T>(string json) where T : new()
		{
			if (json == null) return default(T);

			try
			{
				return JsonConvert.DeserializeObject<T>(json, _settings);
			}
			catch (JsonReaderException e)
			{
				Trace.TraceWarning("Unable to parse JSON - {0}", json);
				Trace.TraceError(e.Message);
				return default(T);
			}
		}

		#endregion
	}
}