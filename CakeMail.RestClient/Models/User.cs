using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using System;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// A user who is authorized to log in the UI.
	/// </summary>
	/// <remarks>The Id and UserId properties are interchangable. They alway contain the same value</remarks>
	public class User
	{
		#region Properties

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("user_id")]
		public int UserId { get; set; }

		[JsonProperty("user_key")]
		public string UserKey { get; set; }

		[JsonProperty("client_id")]
		public int ClientId { get; set; }

		[JsonProperty("email")]
		public string EmailAddress { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("created_on")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime CreatedOn { get; set; }

		[JsonProperty("last_activity")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime LastActivity { get; set; }

		[JsonProperty("first_name")]
		public string FirstName { get; set; }

		[JsonProperty("last_name")]
		public string LastName { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("language")]
		public string Language { get; set; }

		[JsonProperty("timezone")]
		public string Timezone { get; set; }

		[JsonProperty("timezone_id")]
		public int timezone_id { get; set; }

		[JsonProperty("office_phone")]
		public string OfficePhone { get; set; }

		[JsonProperty("mobile_phone")]
		public string MobilePhone { get; set; }

		[JsonProperty("wysiwyg")]
		public int Wysiwyg { get; set; }

		[JsonProperty("help_visible")]
		public bool HelpVisible { get; set; }

		[JsonProperty("openid")]
		public string Openid { get; set; }

		[JsonProperty("cpanel")]
		public string CPanel { get; set; }

		[JsonProperty("permissions")]
		public string[] permissions { get; set; }

		#endregion
	}
}