using CakeMail.RestClient.Utilities;
using Newtonsoft.Json;
using System;

namespace CakeMail.RestClient.Models
{
	public class UnConfirmedClient
	{
		#region Properties

		[JsonProperty("company_name")]
		public string Name { get; set; }

		[JsonProperty("confirmation")]
		public string ConfirmationCode { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("country_id")]
		public string CountryId { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("fax")]
		public string Fax { get; set; }

		[JsonProperty("parent_id")]
		public long ParentId { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }

		[JsonProperty("postal_code")]
		public string PostalCode { get; set; }

		[JsonProperty("province_id")]
		public string ProvinceId { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("time")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime CreatedOn { get; set; }

		[JsonProperty("last_confirmation")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime LastConfirmation { get; set; }

		[JsonProperty("expire")]
		[JsonConverter(typeof(CakeMailDateTimeConverter))]
		public DateTime ExpiresOn { get; set; }

		[JsonProperty("website")]
		public string Website { get; set; }

		[JsonProperty("industry_id")]
		public string IndustryId { get; set; }

		[JsonProperty("contact_same_as_admin")]
		[JsonConverter(typeof(CakeMailIntegerBooleanConverter))]
		public bool ContactSameAsAdmin { get; set; }


		[JsonProperty("admin_email")]
		public string AdminEmail { get; set; }

		[JsonProperty("admin_password")]
		public string AdminPassword { get; set; }

		[JsonProperty("admin_first_name")]
		public string AdminFirstName { get; set; }

		[JsonProperty("admin_last_name")]
		public string AdminLastName { get; set; }

		[JsonProperty("admin_language")]
		public string AdminLanguage { get; set; }

		[JsonProperty("admin_mobile_phone")]
		public string AdminMobilePhone { get; set; }

		[JsonProperty("admin_office_phone")]
		public string AdminOfficePhone { get; set; }

		[JsonProperty("admin_timezone_id")]
		public string AdminTimezoneId { get; set; }

		[JsonProperty("admin_title")]
		public string AdminTitle { get; set; }


		[JsonProperty("contact_email")]
		public string PrimaryContactEmail { get; set; }

		[JsonProperty("contact_password")]
		public string PrimaryContactPassword { get; set; }

		[JsonProperty("contact_first_name")]
		public string PrimaryContactFirstName { get; set; }

		[JsonProperty("contact_last_name")]
		public string PrimaryContactLastName { get; set; }

		[JsonProperty("contact__language")]
		public string PrimaryContactLanguage { get; set; }

		[JsonProperty("contact_mobile_phone")]
		public string PrimaryContactMobilePhone { get; set; }

		[JsonProperty("contact_office_phone")]
		public string PrimaryContactOfficePhone { get; set; }

		[JsonProperty("contact_timezone_id")]
		public string PrimaryContactTimezoneId { get; set; }

		[JsonProperty("contact_title")]
		public string PrimaryContactTitle { get; set; }

		#endregion
	}
}