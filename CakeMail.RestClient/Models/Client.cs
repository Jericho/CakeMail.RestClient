using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using System;

namespace CakeMail.RestClient.Models
{
	/// <summary>
	/// A Client is basically an account. A client can have multiple users.
	/// </summary>
	public class Client
	{
		#region Properties

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("lineage")]
		public string Lineage { get; set; }

		[JsonProperty("address1")]
		public string Address1 { get; set; }

		[JsonProperty("address2")]
		public string Address2 { get; set; }

		[JsonProperty("config_id")]
		public string ConfigId { get; set; }

		[JsonProperty("auth_domain")]
		public string AuthDomain { get; set; }

		[JsonProperty("bounce_domain")]
		public string BounceDomain { get; set; }

		[JsonProperty("parent_bounce_domain")]
		public string ParentBounceDomain { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("company_name")]
		public string Name { get; set; }

		[JsonProperty("contact_id")]
		public long ContactId { get; set; }

		[JsonProperty("country")]
		public string CountryName { get; set; }

		[JsonProperty("country_id")]
		public string CountryId { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("default_mailing_limit")]
		public long DefaultMailingLimit { get; set; }

		[JsonProperty("default_month_limit")]
		public long DefaultMonthLimit { get; set; }

		[JsonProperty("dkim_domain")]
		public string DkimDomain { get; set; }

		[JsonProperty("doptin_ip")]
		public string DoptinIp { get; set; }

		[JsonProperty("fax")]
		public string Fax { get; set; }

		[JsonProperty("force_unsub")]
		public bool ForceUnsubscribe { get; set; }

		[JsonProperty("forward_domain")]
		public string ForwardDomain { get; set; }

		[JsonProperty("parent_forward_domain")]
		public string ParentForwardDomain { get; set; }

		[JsonProperty("forward_ip")]
		public string ForwardIp { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("last_activity")]
		public DateTime LastActivityOn { get; set; }

		[JsonProperty("mailing_limit")]
		public long MailingLimit { get; set; }

		[JsonProperty("manager_id")]
		public long ManagerId { get; set; }

		[JsonProperty("md_domain")]
		public string MdDomain { get; set; }

		[JsonProperty("parent_md_domain")]
		public string ParentMdDomain { get; set; }

		[JsonProperty("month_limit")]
		public long MonthLimit { get; set; }

		[JsonProperty("default_contact_limit")]
		public long DefaultContactLimit { get; set; }

		[JsonProperty("contact_limit")]
		public long ContactLimit { get; set; }

		[JsonProperty("mta_id")]
		public string MtaId { get; set; }

		[JsonProperty("parent_id")]
		public long ParentId { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }

		[JsonProperty("plan_min_vol")]
		public string PlanMinVol { get; set; }

		[JsonProperty("plan_period")]
		public string PlanPeriod { get; set; }

		[JsonProperty("plan_price")]
		public string PlanPrice { get; set; }

		[JsonProperty("plan_start_date")]
		public string PlanStartDate { get; set; }

		[JsonProperty("plan_type")]
		public string PlanType { get; set; }

		[JsonProperty("postal_code")]
		public string PostalCode { get; set; }

		[JsonProperty("pricing_plan")]
		public string PricingPlan { get; set; }

		[JsonProperty("province")]
		public string ProvinceName { get; set; }

		[JsonProperty("province_id")]
		public string ProvinceId { get; set; }

		[JsonProperty("registered_date")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime RegisteredOn { get; set; }

		[JsonProperty("reseller")]
		public bool IsReseller { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("lineage_status")]
		public string LineageStatus { get; set; }

		[JsonProperty("default_trial")]
		public string DefaultTrial { get; set; }

		[JsonProperty("default_reseller")]
		public string DefaultReseller { get; set; }

		[JsonProperty("billing_bundle_id")]
		public string BillingBundleId { get; set; }

		[JsonProperty("billing_package_id")]
		public string BillingPackageId { get; set; }

		[JsonProperty("billing_discount_percent")]
		public string BillingDiscountPercent { get; set; }

		[JsonProperty("callback")]
		public string Callback { get; set; }

		[JsonProperty("trigger_info")]
		public string TriggerInfo { get; set; }

		[JsonProperty("website")]
		public string Website { get; set; }

		[JsonProperty("industry_id")]
		public string IndustryId { get; set; }

		#endregion
	}
}