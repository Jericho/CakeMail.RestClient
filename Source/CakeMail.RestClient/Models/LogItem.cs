using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CakeMail.RestClient.Models
{
	public class LogItem
	{
		#region Properties

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("log_id")]
		public long LogId { get; set; }

		[JsonProperty("record_id")]
		public long ListMemberId { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("action")]
		public LogType LogType { get; set; }

		[JsonProperty("total")]
		public long Total { get; set; }

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

		[JsonProperty("show_email_link")]
		public string ShowEmailLink { get; set; }

		[JsonExtensionData]
		public IDictionary<string, object> CustomFields { get; set; }

		#endregion
	}
}
