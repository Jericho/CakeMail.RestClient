using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using System;

namespace CakeMail.RestClient.Models
{
	public class Mailing
	{
		#region Properties

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("campaign_id")]
		public int CampaignId { get; set; }

		[JsonProperty("list_id")]
		public int ListId { get; set; }

		[JsonProperty("list_name")]
		public string ListName { get; set; }

		[JsonProperty("sublist_id")]
		public int SublistId { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("next_step")]
		public string NextStep { get; set; }

		[JsonProperty("transfer_encoding")]
		public string TransferEncoding { get; set; }

		[JsonProperty("encoding")]
		public string Encoding { get; set; }

		[JsonProperty("suspended")]
		public bool IsSuspended { get; set; }

		[JsonProperty("clickthru_html")]
		public bool TrackClicksInHtml { get; set; }

		[JsonProperty("clickthru_text")]
		public bool TrackClicksInText { get; set; }

		[JsonProperty("opening_stats")]
		public bool TrackOpens { get; set; }

		[JsonProperty("unsub_bottom_link")]
		public bool IncludeUnsubscribeLink { get; set; }

		[JsonProperty("created_on")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime CreatedOn { get; set; }

		[JsonProperty("scheduled_on")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime ScheduledOn { get; set; }

		[JsonProperty("scheduled_for")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime ScheduledFor { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("subject")]
		public string Subject { get; set; }

		[JsonProperty("sender_name")]
		public string SenderName { get; set; }

		[JsonProperty("sender_email")]
		public string SenderEmail { get; set; }

		[JsonProperty("html_message")]
		public string HtmlContent { get; set; }

		[JsonProperty("text_message")]
		public string TextContent { get; set; }

		[JsonProperty("tracking_params")]
		public string TrackingParameters { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("ending_on")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime EndingOn { get; set; }

		[JsonProperty("max_recurrences")]
		public int MaxRecurrences { get; set; }

		[JsonProperty("recurring_conditions")]
		public string RecurringConditions { get; set; }

		[JsonProperty("bcc")]
		public string Bcc { get; set; }

		[JsonProperty("cc")]
		public string Cc { get; set; }

		[JsonProperty("reply_to")]
		public string ReplyTo { get; set; }

		[JsonProperty("footer")]
		public string Footer { get; set; }

		[JsonProperty("social_bar")]
		public bool DisplaySocialSharingToolbarBar { get; set; }

		[JsonProperty("show_email_link")]
		public string ShowEmailLink { get; set; }

		[JsonProperty("share_email_link")]
		public string ShareEmailLink { get; set; }

		[JsonProperty("content_last_updated")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime ModifiedOn { get; set; }

		[JsonProperty("active_emails")]
		public int ActiveEmails { get; set; }

		[JsonProperty("bounce_ar")]
		public int Bounce_AR { get; set; }

		[JsonProperty("bounce_cr")]
		public int Bounce_CR { get; set; }

		[JsonProperty("bounce_df")]
		public int Bounce_DF { get; set; }

		[JsonProperty("bounce_fm")]
		public int Bounce_FM { get; set; }

		[JsonProperty("bounce_hb")]
		public int Bounce_HB { get; set; }

		[JsonProperty("bounce_mb")]
		public int Bounce_MB { get; set; }

		[JsonProperty("bounce_sb")]
		public int Bounce_SB { get; set; }

		[JsonProperty("bounce_tr")]
		public int Bounce_TR { get; set; }

		[JsonProperty("forwards")]
		public int Forwards { get; set; }

		[JsonProperty("opened_forwards")]
		public int OpenedForwards { get; set; }

		[JsonProperty("sent")]
		public int Sent { get; set; }

		[JsonProperty("skipped")]
		public int Skipped { get; set; }

		[JsonProperty("total_clicks")]
		public int TotalClicks { get; set; }

		[JsonProperty("total_opens")]
		public int TotalOpens { get; set; }

		[JsonProperty("unique_clicks")]
		public int UniqueClicks { get; set; }

		[JsonProperty("unique_opens")]
		public int UniqueOpens { get; set; }

		[JsonProperty("unsubscribes")]
		public int? unsubscribes { get; set; }

		#endregion
	}
}