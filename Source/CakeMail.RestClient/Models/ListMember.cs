using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CakeMail.RestClient.Models
{
	public class ListMember
	{
		#region Properties

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>
		/// The id.
		/// </value>
		[JsonProperty("id")]
		public long Id { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		[JsonProperty("status")]
		public ListMemberStatus Status { get; set; }

		/// <summary>
		/// Gets or sets the bounce_type.
		/// </summary>
		/// <value>
		/// The bounce_type.
		/// </value>
		[JsonProperty("bounce_type")]
		public string BounceType { get; set; }

		/// <summary>
		/// Gets or sets the bounce_count.
		/// </summary>
		/// <value>
		/// The bounce_count.
		/// </value>
		[JsonProperty("bounce_count")]
		public long BounceCount { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		[JsonProperty("email")]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the registered.
		/// </summary>
		/// <value>
		/// The registered.
		/// </value>
		[JsonProperty("registered")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime RegisteredOn { get; set; }

		[JsonExtensionData]
		public IDictionary<string, object> CustomFields { get; set; }

		#endregion
	}
}