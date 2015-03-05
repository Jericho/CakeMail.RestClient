using CakeMailRestAPI.Utilities;
using Newtonsoft.Json;
using System;

namespace CakeMailRestAPI.Models
{
	public class LoginInfo
	{
		#region Properties

		[JsonProperty("client_id")]
		public int ClientId { get; set; }

		[JsonProperty("client_key")]
		public string ClientKey { get; set; }

		[JsonProperty("user_id")]
		public int UserId { get; set; }

		[JsonProperty("user_key")]
		public string UserKey { get; set; }

		[JsonProperty("first_name")]
		public string FirstName { get; set; }

		[JsonProperty("last_name")]
		public string LastName { get; set; }

		[JsonProperty("language")]
		public string Language { get; set; }

		[JsonProperty("timezone")]
		public string TimeZone { get; set; }

		[JsonProperty("last_activity")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime LastActivity { get; set; }

		[JsonProperty("client_lineage")]
		public string Lineage { get; set; }

		#endregion
	}
}