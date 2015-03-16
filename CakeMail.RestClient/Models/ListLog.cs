using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CakeMail.RestClient.Models
{
	public class ListLog
	{
		#region Properties

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("log_id")]
		public int LogId { get; set; }

		[JsonProperty("record_id")]
		public int RecordId { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("action")]
		public string Action { get; set; }

		[JsonProperty("total")]
		public int Total { get; set; }

		[JsonProperty("time")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime Date { get; set; }

		[JsonProperty("user_agent")]
		public string UserAgent { get; set; }

		[JsonProperty("ip")]
		public string IpAddress { get; set; }

		[JsonProperty("host")]
		public string Host { get; set; }

		[JsonProperty("extra")]
		public string Extra { get; set; }

		[JsonExtensionData]
		public IDictionary<string, object> CustomFields { get; set; }

		#endregion
	}
}