using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using System;

namespace CakeMail.RestClient.Models
{
	public class Trigger
	{
		#region Properties

		[JsonProperty("action")]
		public string Action { get; set; }

		[JsonProperty("campaign_id")]
		public long CampaignId { get; set; }

		[JsonProperty("clickthru_html")]
		public bool TrackClicksInHtml { get; set; }

		[JsonProperty("clickthru_text")]
		public bool TrackClicksInText { get; set; }

		[JsonProperty("delay")]
		public long Delay { get; set; }

		[JsonProperty("encoding")]
		public string Encoding { get; set; }

		[JsonProperty("html_message")]
		public string HtmlContent { get; set; }

		[JsonProperty("show_email_link")]
		public string ShowEmailLink { get; set; }

		[JsonProperty("share_email_link")]
		public string ShareEmailLink { get; set; }

		[JsonProperty("created_on")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime CreatedOn { get; set; }

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("list_id")]
		public long ListId { get; set; }

		[JsonProperty("list_name")]
		public string ListName { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("opening_stats")]
		public bool TrackOpens { get; set; }

		[JsonProperty("parent_id")]
		public long ParentId { get; set; }

		[JsonProperty("send_to")]
		public string SendTo { get; set; }

		[JsonProperty("sender_email")]
		public string SenderEmail { get; set; }

		[JsonProperty("sender_name")]
		public string SenderName { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("subject")]
		public string Subject { get; set; }

		[JsonProperty("text_message")]
		public string TextContent { get; set; }

		[JsonProperty("transfer_encoding")]
		public string TransferEncoding { get; set; }

		[JsonProperty("tracking_params")]
		public string TrackingParameters { get; set; }

		[JsonProperty("content_last_updated")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime ModifiedOn { get; set; }

		[JsonProperty("date_field")]
		public string DateField { get; set; }

		[JsonProperty("send_at")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime? SendOn { get; set; }

		[JsonProperty("bcc")]
		public string Bcc { get; set; }

		[JsonProperty("cc")]
		public string Cc { get; set; }

		[JsonProperty("reply_to")]
		public string ReplyTo { get; set; }

		[JsonProperty("social_bar")]
		public bool DisplaySocialSharingToolbarBar { get; set; }

		[JsonProperty("footer")]
		public string Footer { get; set; }

		[JsonProperty("unsub_bottom_link")]
		public bool IncludeUnsubscribeLink { get; set; }

		#endregion
	}
}