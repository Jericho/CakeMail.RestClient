using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CakeMail.RestClient.Models
{
	public class Template
	{
		#region Properties

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("labels")]
		public KeyValuePair<string, string>[] Labels { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("category_id")]
		public long CategoryId { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("content")]
		public string Content { get; set; }

		[JsonProperty("thumbnail")]
		public string Thumbnail { get; set; }

		[JsonProperty("last_modified")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime ModifiedOn { get; set; }

		[JsonProperty("editor_version")]
		public long EditorVersion { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		#endregion
	}
}