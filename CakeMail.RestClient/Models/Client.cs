using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using System;

namespace CakeMail.RestClient.Models
{
	public class Client
	{
		#region Properties

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("company_name")]
		public string Name { get; set; }

		[JsonProperty("contact_id")]
		public int ContactId { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("default_mailing_limit")]
		public int DefaultMailingLimit { get; set; }

		[JsonProperty("default_month_limit")]
		public int DefaultMonthLimit { get; set; }

		[JsonProperty("default_contact_limit")]
		public int DefaultContactLimit { get; set; }

		[JsonProperty("reseller")]
		public bool IsReseller { get; set; }

		[JsonProperty("mailing_limit")]
		public int MailingLimit { get; set; }

		[JsonProperty("month_limit")]
		public int MonthLimit { get; set; }

		[JsonProperty("contact_limit")]
		public int ContactLimit { get; set; }

		[JsonProperty("manager_id")]
		public int ManagerId { get; set; }

		[JsonProperty("parent_id")]
		public int ParentId { get; set; }

		[JsonProperty("registered_date")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime RegisteredOn { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("md_domain")]
		public string MdDomain { get; set; }

		[JsonProperty("bounce_domain")]
		public string BounceDomain { get; set; }

		[JsonProperty("forward_domain")]
		public string ForwardDomain { get; set; }

		[JsonProperty("office_phone")]
		public string OfficePhone { get; set; }

		[JsonProperty("mobile_phone")]
		public string MobilePhone { get; set; }

		[JsonProperty("lineage")]
		public string lineage { get; set; }

		#endregion
	}
}