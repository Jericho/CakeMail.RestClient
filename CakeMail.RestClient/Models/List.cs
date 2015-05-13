using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using System;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// A List is a collection of subscribers (or List Members, or Records). 
	/// Each subscriber or List Member is uniquely identified by their email address, and may include an limited amount of Fields containing demographic information associated to each email address.
	/// </summary>
	public class List
	{
		#region Properties

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("status")]
		public ListStatus Status { get; set; }

		[JsonProperty("policy")]
		public string Policy { get; set; }

		[JsonProperty("language")]
		public string Language { get; set; }

		[JsonProperty("created_on")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime CreatedOn { get; set; }

		[JsonProperty("sender_name")]
		public string SenderName { get; set; }

		[JsonProperty("sender_email")]
		public string SenderEmail { get; set; }

		[JsonProperty("forward_page")]
		public string ForwardPage { get; set; }

		[JsonProperty("goto_oi")]
		public string goto_oi { get; set; }

		[JsonProperty("goto_di")]
		public string goto_di { get; set; }

		[JsonProperty("goto_oo")]
		public string goto_oo { get; set; }

		[JsonProperty("b_ac_limit")]
		public long b_ac_limit { get; set; }

		[JsonProperty("b_cr_limit")]
		public long b_cr_limit { get; set; }

		[JsonProperty("b_df_limit")]
		public long b_df_limit { get; set; }

		[JsonProperty("b_fm_limit")]
		public long b_fm_limit { get; set; }

		[JsonProperty("b_hb_limit")]
		public long b_hb_limit { get; set; }

		[JsonProperty("b_mb_limit")]
		public long b_mb_limit { get; set; }

		[JsonProperty("b_sb_limit")]
		public long b_sb_limit { get; set; }

		[JsonProperty("b_tr_limit")]
		public long b_tr_limit { get; set; }

		[JsonProperty("di_trig_cnt")]
		public long di_trig_cnt { get; set; }

		[JsonProperty("oi_trig_cnt")]
		public long oi_trig_cnt { get; set; }

		[JsonProperty("oo_trig_cnt")]
		public long oo_trig_cnt { get; set; }

		[JsonProperty("oi_url")]
		public string oi_url { get; set; }

		[JsonProperty("subscribe_url")]
		public string subscribe_url { get; set; }

		[JsonProperty("oo_url")]
		public string oo_url { get; set; }

		[JsonProperty("webhook")]
		public string webhook { get; set; }

		/// <remarks>This value is calculated when retrieving a list only if the 'calculateEngagement' parameter is set to true. Otherwise this value is null.</remarks>
		[JsonProperty("engagement")]
		public int? Engagement { get; set; }

		/// <remarks>This value is retrieved only if the 'includeDetails' parameter is set to true.</remarks>
		[JsonProperty("pending")]
		public int? PendingCount { get; set; }

		/// <remarks>This value is retrieved only if the 'includeDetails' parameter is set to true.</remarks>
		[JsonProperty("active")]
		public int? ActiveCount { get; set; }

		/// <remarks>This value is retrieved only if the 'includeDetails' parameter is set to true.</remarks>
		[JsonProperty("bounced")]
		public int? BouncedCount { get; set; }

		/// <remarks>This value is retrieved only if the 'includeDetails' parameter is set to true.</remarks>
		[JsonProperty("invalid")]
		public int? InvalidCount { get; set; }

		/// <remarks>This value is retrieved only if the 'includeDetails' parameter is set to true.</remarks>
		[JsonProperty("unsubscribed")]
		public int? UnsubscribedCount { get; set; }

		/// <remarks>This value is retrieved only if the 'includeDetails' parameter is set to true.</remarks>
		[JsonProperty("spam")]
		public int? SpamCount { get; set; }

		/// <remarks>This value is retrieved only if the 'includeDetails' parameter is set to true.</remarks>
		[JsonProperty("deleted")]
		public int? DeletedCount { get; set; }

		#endregion
	}
}