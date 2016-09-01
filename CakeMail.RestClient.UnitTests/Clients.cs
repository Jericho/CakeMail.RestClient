using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class ClientsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;
		private const string NAME = "Fictitious Inc";
		private const string ADDRESS1 = "123 1st Avenue";
		private const string ADDRESS2 = "Suite 1000";
		private const string CITY = "Mock City";
		private const string PROVINCE = "FL";
		private const string POSTALCODE = "12345";
		private const string COUNTRY_ID = "us";
		private const string WEBSITE = "www.fictitiouscompany.com";
		private const string PHONE = "111-111-1111";
		private const string FAX = "222-222-2222";
		private const string ADMIN_EMAIL = "bobsmith@fictitiouscompany.com";
		private const string ADMIN_FIRST_NAME = "Bob";
		private const string ADMIN_LAST_NAME = "Smith";
		private const string ADMIN_TITLE = "Administrator";
		private const string ADMIN_OFFICE_PHONE = "333-333-3333";
		private const string ADMIN_MOBILE_PHONE = "444-444-4444";
		private const string ADMIN_LANGUAGE = "en";
		private const long ADMIN_TIMEZONE_ID = 542;
		private const string ADMIN_PASSWORD = "MySecretPassword";
		private const string PRIMARY_CONTACT_EMAIL = "janedoe@fictitiouscompany.com";
		private const string PRIMARY_CONTACT_FIRST_NAME = "Jane";
		private const string PRIMARE_CONTACT_LAST_NAME = "Doe";
		private const string PRIMARY_CONTACT_TITLE = "CEO";
		private const string PRIMARY_CONTACT_OFFICE_PHONE = "555-555-5555";
		private const string PRIMARY_CONTACT_MOBILE_PHONE = "666-666-6666";
		private const string PRIMARY_CONTACT_LANGUAGE = "en";
		private const long PRIMARY_CONTACT_TIMEZONE_ID = 542;
		private const string PRIMARY_CONTACT_PASSOWRD = "SuperSecretPassword";
		private const string CONFIRMATION_CODE = "... dummy confirmation code ...";

		[TestMethod]
		public async Task CreateClient_with_minimal_parameters()
		{
			// Arrange
			string name = "Fictitious Inc";
			string confirmationCode = "... dummy confirmation code ...";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "parent_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "company_name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_same_as_admin", Value = "1" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", confirmationCode);
			var mockRestClient = new MockRestClient("/Client/Create/", parameters, jsonResponse, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.CreateAsync(CLIENT_ID, name);

			// Assert
			Assert.AreEqual(confirmationCode, result);
		}

		[TestMethod]
		public async Task CreateClient_with_minimal_parameters_and_contactsameasadmin_false()
		{
			// Arrange
			string name = "Fictitious Inc";
			string confirmationCode = "... dummy confirmation code ...";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "parent_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "company_name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_same_as_admin", Value = "0" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", confirmationCode);
			var mockRestClient = new MockRestClient("/Client/Create/", parameters, jsonResponse, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.CreateAsync(CLIENT_ID, name, primaryContactSameAsAdmin: false);

			// Assert
			Assert.AreEqual(confirmationCode, result);
		}

		[TestMethod]
		public async Task CreateClient_with_all_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "parent_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "company_name", Value = NAME },

				new Parameter { Type = ParameterType.GetOrPost, Name = "address1", Value = ADDRESS1 },
				new Parameter { Type = ParameterType.GetOrPost, Name = "address2", Value = ADDRESS2 },
				new Parameter { Type = ParameterType.GetOrPost, Name = "city", Value = CITY },
				new Parameter { Type = ParameterType.GetOrPost, Name = "province_id", Value = PROVINCE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "postal_code", Value = POSTALCODE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "country_id", Value = COUNTRY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "website", Value = WEBSITE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "phone", Value = PHONE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "fax", Value = FAX },

				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_email", Value = ADMIN_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_first_name", Value = ADMIN_FIRST_NAME },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_last_name", Value = ADMIN_LAST_NAME },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_password", Value = ADMIN_PASSWORD },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_password_confirmation", Value = ADMIN_PASSWORD },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_title", Value = ADMIN_TITLE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_office_phone", Value = ADMIN_OFFICE_PHONE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_mobile_phone", Value = ADMIN_MOBILE_PHONE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_language", Value = ADMIN_LANGUAGE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_timezone_id", Value = ADMIN_TIMEZONE_ID },

				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_same_as_admin", Value = "0" },

				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_email", Value = PRIMARY_CONTACT_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_first_name", Value = PRIMARY_CONTACT_FIRST_NAME },
				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_last_name", Value = PRIMARE_CONTACT_LAST_NAME },
				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_password", Value = PRIMARY_CONTACT_PASSOWRD },
				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_password_confirmation", Value = PRIMARY_CONTACT_PASSOWRD },
				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_title", Value = PRIMARY_CONTACT_TITLE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_office_phone", Value = PRIMARY_CONTACT_OFFICE_PHONE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_mobile_phone", Value = PRIMARY_CONTACT_MOBILE_PHONE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_language", Value = PRIMARY_CONTACT_LANGUAGE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_timezone_id", Value = PRIMARY_CONTACT_TIMEZONE_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", CONFIRMATION_CODE);
			var mockRestClient = new MockRestClient("/Client/Create/", parameters, jsonResponse, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.CreateAsync(CLIENT_ID, NAME, ADDRESS1, ADDRESS2, CITY, PROVINCE, POSTALCODE, COUNTRY_ID, WEBSITE, PHONE, FAX, ADMIN_EMAIL, ADMIN_FIRST_NAME, ADMIN_LAST_NAME, ADMIN_TITLE, ADMIN_OFFICE_PHONE, ADMIN_MOBILE_PHONE, ADMIN_LANGUAGE, ADMIN_TIMEZONE_ID, ADMIN_PASSWORD, false, PRIMARY_CONTACT_EMAIL, PRIMARY_CONTACT_FIRST_NAME, PRIMARE_CONTACT_LAST_NAME, PRIMARY_CONTACT_TITLE, PRIMARY_CONTACT_OFFICE_PHONE, PRIMARY_CONTACT_MOBILE_PHONE, PRIMARY_CONTACT_LANGUAGE, PRIMARY_CONTACT_TIMEZONE_ID, PRIMARY_CONTACT_PASSOWRD);

			// Assert
			Assert.AreEqual(CONFIRMATION_CODE, result);
		}

		[TestMethod]
		public async Task CreateClient_admin_same_as_contact()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "parent_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "company_name", Value = NAME },

				new Parameter { Type = ParameterType.GetOrPost, Name = "address1", Value = ADDRESS1 },
				new Parameter { Type = ParameterType.GetOrPost, Name = "address2", Value = ADDRESS2 },
				new Parameter { Type = ParameterType.GetOrPost, Name = "city", Value = CITY },
				new Parameter { Type = ParameterType.GetOrPost, Name = "province_id", Value = PROVINCE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "postal_code", Value = POSTALCODE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "country_id", Value = COUNTRY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "website", Value = WEBSITE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "phone", Value = PHONE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "fax", Value = FAX },

				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_email", Value = ADMIN_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_first_name", Value = ADMIN_FIRST_NAME },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_last_name", Value = ADMIN_LAST_NAME },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_password", Value = ADMIN_PASSWORD },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_password_confirmation", Value = ADMIN_PASSWORD },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_title", Value = ADMIN_TITLE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_office_phone", Value = ADMIN_OFFICE_PHONE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_mobile_phone", Value = ADMIN_MOBILE_PHONE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_language", Value = ADMIN_LANGUAGE },
				new Parameter { Type = ParameterType.GetOrPost, Name = "admin_timezone_id", Value = ADMIN_TIMEZONE_ID },

				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_same_as_admin", Value = "1" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", CONFIRMATION_CODE);
			var mockRestClient = new MockRestClient("/Client/Create/", parameters, jsonResponse, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.CreateAsync(CLIENT_ID, NAME, ADDRESS1, ADDRESS2, CITY, PROVINCE, POSTALCODE, COUNTRY_ID, WEBSITE, PHONE, FAX, ADMIN_EMAIL, ADMIN_FIRST_NAME, ADMIN_LAST_NAME, ADMIN_TITLE, ADMIN_OFFICE_PHONE, ADMIN_MOBILE_PHONE, ADMIN_LANGUAGE, ADMIN_TIMEZONE_ID, ADMIN_PASSWORD, true, PRIMARY_CONTACT_EMAIL, PRIMARY_CONTACT_FIRST_NAME, PRIMARE_CONTACT_LAST_NAME, PRIMARY_CONTACT_TITLE, PRIMARY_CONTACT_OFFICE_PHONE, PRIMARY_CONTACT_MOBILE_PHONE, PRIMARY_CONTACT_LANGUAGE, PRIMARY_CONTACT_TIMEZONE_ID, PRIMARY_CONTACT_PASSOWRD);

			// Assert
			Assert.AreEqual(CONFIRMATION_CODE, result);
		}

		[TestMethod]
		public async Task ConfirmClient()
		{
			// Arrange
			var confirmationId = "... dummy confirmation id ...";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "confirmation", Value = confirmationId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"client_id\":\"{0}\",\"client_key\":\"...dummy client key...\",\"admin_id\":\"123\",\"admin_key\":\"...dummy admin key...\",\"contact_id\":\"456\",\"contact_key\":\"...dummy contact key...\"}}}}", CLIENT_ID);
			var mockRestClient = new MockRestClient("/Client/Activate/", parameters, jsonResponse, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.ConfirmAsync(confirmationId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(CLIENT_ID, result.ClientId);
		}

		[TestMethod]
		public async Task GetClient_by_confirmationcode()
		{
			// Arrange
			var confirmationCode = "...dummy confirmation code...";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "confirmation", Value = confirmationCode }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"city\":\"Mock City\",\"company_name\":\"Fictitious Inc\",\"confirmation\":\"{0}\",\"address1\":\"123 1st Avenue\",\"address2\":\"Suite 1000\",\"country_id\":\"us\",\"currency\":\"USD\",\"fax\":\"222-222-2222\",\"parent_id\":\"1\",\"phone\":\"111-111-1111\",\"postal_code\":\"12345\",\"province_id\":\"FL\",\"status\":\"pending\",\"contact_same_as_admin\":\"1\",\"admin_email\":\"bobsmith@fictitiouscompany.com\",\"admin_password\":\"7cad97840d5b8e175870b1245a5fe9d8\",\"admin_first_name\":\"Bob\",\"admin_last_name\":\"Smith\",\"admin_language\":\"en_US\",\"admin_mobile_phone\":\"444-444-4444\",\"admin_office_phone\":\"333-333-3333\",\"admin_timezone_id\":\"542\",\"admin_title\":\"Administrator\",\"contact_email\":null,\"contact_password\":null,\"contact_first_name\":null,\"contact_last_name\":null,\"contact_language\":\"en_US\",\"contact_mobile_phone\":null,\"contact_office_phone\":null,\"contact_timezone_id\":\"152\",\"contact_title\":null,\"time\":\"2015-03-25 17:07:03\",\"last_confirmation\":\"2015-03-25 17:07:03\",\"expire\":\"2015-04-24 17:07:03\",\"website\":\"www.fictitiouscompany.com\",\"industry_id\":\"0\"}}}}", confirmationCode);
			var mockRestClient = new MockRestClient("/Client/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.GetAsync(USER_KEY, confirmationCode);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(confirmationCode, result.ConfirmationCode);
		}

		[TestMethod]
		public async Task GetClient_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lineage\":\"1-{0}\",\"address1\":\"123 1st Avenue\",\"address2\":\"Suite 1000\",\"config_id\":\"55\",\"auth_domain\":\"md02.com\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"parent_bounce_domain\":\"bounce.fictitiouscompany.com\",\"city\":\"Mock City\",\"company_name\":\"Fictitious Inc\",\"contact_id\":\"0\",\"country\":\"United States\",\"country_id\":\"us\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"dkim_domain\":\"md02.com\",\"doptin_ip\":\"192.168.77.1\",\"fax\":null,\"force_unsub\":\"false\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"parent_forward_domain\":\"http://forward.fictitiouscompany.com/\",\"forward_ip\":\"192.168.77.2\",\"id\":\"{0}\",\"key\":\"...dummy key...\",\"last_activity\":\"2015-03-25 14:20:54\",\"mailing_limit\":\"900000\",\"manager_id\":\"0\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"parent_md_domain\":\"http://link.fictitiouscompany.com/\",\"month_limit\":\"900000\",\"default_contact_limit\":\"0\",\"contact_limit\":\"0\",\"mta_id\":\"1\",\"parent_id\":\"1\",\"phone\":null,\"plan_min_vol\":null,\"plan_period\":null,\"plan_price\":null,\"plan_start_date\":null,\"plan_type\":null,\"postal_code\":null,\"pricing_plan\":\"courrielleur-demo\",\"province\":\"Florida\",\"province_id\":\"FL\",\"registered_date\":\"2015-03-01 00:00:00\",\"reseller\":\"false\",\"status\":\"active\",\"lineage_status\":\"active\",\"default_trial\":\"false\",\"default_reseller\":\"false\",\"billing_bundle_id\":\"0\",\"billing_package_id\":\"0\",\"billing_discount_percent\":\"0.000\",\"callback\":null,\"trigger_info\":\"false\",\"website\":null,\"industry_id\":null}}}}", CLIENT_ID);
			var mockRestClient = new MockRestClient("/Client/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.GetAsync(USER_KEY, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(CLIENT_ID, result.Id);
		}

		[TestMethod]
		public async Task GetClient_with_startdate()
		{
			// Arrange
			var startDate = new DateTime(2014, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			var endDate = (DateTime?)null;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "start_date", Value = startDate.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lineage\":\"1-{0}\",\"address1\":\"123 1st Avenue\",\"address2\":\"Suite 1000\",\"config_id\":\"55\",\"auth_domain\":\"md02.com\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"parent_bounce_domain\":\"bounce.fictitiouscompany.com\",\"city\":\"Mock City\",\"company_name\":\"Fictitious Inc\",\"contact_id\":\"0\",\"country\":\"United States\",\"country_id\":\"us\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"dkim_domain\":\"md02.com\",\"doptin_ip\":\"192.168.77.1\",\"fax\":null,\"force_unsub\":\"false\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"parent_forward_domain\":\"http://forward.fictitiouscompany.com/\",\"forward_ip\":\"192.168.77.2\",\"id\":\"{0}\",\"key\":\"...dummy key...\",\"last_activity\":\"2015-03-25 14:20:54\",\"mailing_limit\":\"900000\",\"manager_id\":\"0\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"parent_md_domain\":\"http://link.fictitiouscompany.com/\",\"month_limit\":\"900000\",\"default_contact_limit\":\"0\",\"contact_limit\":\"0\",\"mta_id\":\"1\",\"parent_id\":\"1\",\"phone\":null,\"plan_min_vol\":null,\"plan_period\":null,\"plan_price\":null,\"plan_start_date\":null,\"plan_type\":null,\"postal_code\":null,\"pricing_plan\":\"courrielleur-demo\",\"province\":\"Florida\",\"province_id\":\"FL\",\"registered_date\":\"2015-03-01 00:00:00\",\"reseller\":\"false\",\"status\":\"active\",\"lineage_status\":\"active\",\"default_trial\":\"false\",\"default_reseller\":\"false\",\"billing_bundle_id\":\"0\",\"billing_package_id\":\"0\",\"billing_discount_percent\":\"0.000\",\"callback\":null,\"trigger_info\":\"false\",\"website\":null,\"industry_id\":null}}}}", CLIENT_ID);
			var mockRestClient = new MockRestClient("/Client/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.GetAsync(USER_KEY, CLIENT_ID, startDate, endDate);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(CLIENT_ID, result.Id);
		}

		[TestMethod]
		public async Task GetClient_with_enddate()
		{
			// Arrange
			var startDate = (DateTime?)null;
			var endDate = new DateTime(2014, 12, 31, 23, 59, 59, 999, DateTimeKind.Utc);
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "end_date", Value = endDate.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lineage\":\"1-{0}\",\"address1\":\"123 1st Avenue\",\"address2\":\"Suite 1000\",\"config_id\":\"55\",\"auth_domain\":\"md02.com\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"parent_bounce_domain\":\"bounce.fictitiouscompany.com\",\"city\":\"Mock City\",\"company_name\":\"Fictitious Inc\",\"contact_id\":\"0\",\"country\":\"United States\",\"country_id\":\"us\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"dkim_domain\":\"md02.com\",\"doptin_ip\":\"192.168.77.1\",\"fax\":null,\"force_unsub\":\"false\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"parent_forward_domain\":\"http://forward.fictitiouscompany.com/\",\"forward_ip\":\"192.168.77.2\",\"id\":\"{0}\",\"key\":\"...dummy key...\",\"last_activity\":\"2015-03-25 14:20:54\",\"mailing_limit\":\"900000\",\"manager_id\":\"0\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"parent_md_domain\":\"http://link.fictitiouscompany.com/\",\"month_limit\":\"900000\",\"default_contact_limit\":\"0\",\"contact_limit\":\"0\",\"mta_id\":\"1\",\"parent_id\":\"1\",\"phone\":null,\"plan_min_vol\":null,\"plan_period\":null,\"plan_price\":null,\"plan_start_date\":null,\"plan_type\":null,\"postal_code\":null,\"pricing_plan\":\"courrielleur-demo\",\"province\":\"Florida\",\"province_id\":\"FL\",\"registered_date\":\"2015-03-01 00:00:00\",\"reseller\":\"false\",\"status\":\"active\",\"lineage_status\":\"active\",\"default_trial\":\"false\",\"default_reseller\":\"false\",\"billing_bundle_id\":\"0\",\"billing_package_id\":\"0\",\"billing_discount_percent\":\"0.000\",\"callback\":null,\"trigger_info\":\"false\",\"website\":null,\"industry_id\":null}}}}", CLIENT_ID);
			var mockRestClient = new MockRestClient("/Client/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.GetAsync(USER_KEY, CLIENT_ID, startDate, endDate);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(CLIENT_ID, result.Id);
		}

		[TestMethod]
		public async Task GetClients_with_status()
		{
			// Arrange
			var status = ClientStatus.Active;
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2);
			var mockRestClient = new MockRestClient("/Client/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.GetListAsync(USER_KEY, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetClients_with_name()
		{
			// Arrange
			var name = "Dummy Client";
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "company_name", Value = name }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2);
			var mockRestClient = new MockRestClient("/Client/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.GetListAsync(USER_KEY, name: name);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetClients_with_sortBy()
		{
			// Arrange
			var sortBy = ClientsSortBy.CompanyName;
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sort_by", Value = sortBy.GetEnumMemberValue() }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2);
			var mockRestClient = new MockRestClient("/Client/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.GetListAsync(USER_KEY, sortBy: sortBy);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetClients_with_sortdirection()
		{
			// Arrange
			var sortDirection = SortDirection.Ascending;
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "direction", Value = sortDirection.GetEnumMemberValue() }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2);
			var mockRestClient = new MockRestClient("/Client/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.GetListAsync(USER_KEY, sortDirection: sortDirection);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetClients_with_limit()
		{
			// Arrange
			var limit = 11;
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2);
			var mockRestClient = new MockRestClient("/Client/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.GetListAsync(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetClients_with_offset()
		{
			// Arrange
			var offset = 33;
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2);
			var mockRestClient = new MockRestClient("/Client/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.GetListAsync(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetClients_with_clientid()
		{
			// Arrange
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2);
			var mockRestClient = new MockRestClient("/Client/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.GetListAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetClientsCount_with_status()
		{
			// Arrange
			var status = ClientStatus.Active;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Client/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.GetCountAsync(USER_KEY, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetClientsCount_with_name()
		{
			// Arrange
			var name = "Dummy Client";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "company_name", Value = name }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Client/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.GetCountAsync(USER_KEY, name: name);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetClientsCount_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Client/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.GetCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task UpdateClient_name()
		{
			// Arrange
			var name = "Fictitious Inc";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "company_name", Value = name }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, name: name);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_status()
		{
			// Arrange
			var status = ClientStatus.Active;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, status: status);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_parentid()
		{
			// Arrange
			var parentId = 1L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "parent_id", Value = parentId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, parentId: parentId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_address1()
		{
			// Arrange
			var address1 = "123 1st Avenue";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "address1", Value = address1 }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, address1: address1);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_address2()
		{
			// Arrange
			var address2 = "Suite 1000";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "address2", Value = address2 }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, address2: address2);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_city()
		{
			// Arrange
			var city = "Mock City";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "city", Value = city }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, city: city);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_provinceid()
		{
			// Arrange
			var provinceId = "FL";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "province_id", Value = provinceId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, provinceId: provinceId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_postalcode()
		{
			// Arrange
			var postalCode = "12345";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "postal_code", Value = postalCode }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, postalCode: postalCode);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_countryid()
		{
			// Arrange
			var countryId = "us";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "country_id", Value = countryId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, countryId: countryId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_website()
		{
			// Arrange
			var website = "www.fictitiouscompany.com";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "website", Value = website }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, website: website);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_phone()
		{
			// Arrange
			var phone = "111-111-1111";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "phone", Value = phone }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, phone: phone);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_fax()
		{
			// Arrange
			var fax = "222-222-2222";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "fax", Value = fax }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, fax: fax);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_autdomain()
		{
			// Arrange
			var authDomain = "md02.com";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "auth_domain", Value = authDomain }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, authDomain: authDomain);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_bouncedomain()
		{
			// Arrange
			var bounceDomain = "bounce.fictitiouscompany.com";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "bounce_domain", Value = bounceDomain }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, bounceDomain: bounceDomain);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_dkimdomain()
		{
			// Arrange
			var dkimDomain = "md02.com";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "dkim_domain", Value = dkimDomain }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, dkimDomain: dkimDomain);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_doptinip()
		{
			// Arrange
			var doptinIp = "192.168.77.1";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "doptin_ip", Value = doptinIp }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, doptinIp: doptinIp);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_forwarddomain()
		{
			// Arrange
			var forwardDomain = "http://forward.fictitiouscompany.com/";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "forward_domain", Value = forwardDomain }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, forwardDomain: forwardDomain);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_forwardip()
		{
			// Arrange
			var forwardIp = "192.168.77.2";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "forward_ip", Value = forwardIp }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, forwardIp: forwardIp);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_ippool()
		{
			// Arrange
			var ipPool = "dummy";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "ip_pool", Value = ipPool }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, ipPool: ipPool);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_mddomain()
		{
			// Arrange
			var mdDomain = "http://link.fictitiouscompany.com/";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "md_domain", Value = mdDomain }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, mdDomain: mdDomain);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_isreseller_true()
		{
			// Arrange
			var isReseller = true;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "reseller", Value = "1" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, isReseller: isReseller);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_isreseller_false()
		{
			// Arrange
			var isReseller = false;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "reseller", Value = "0" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, isReseller: isReseller);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_currency()
		{
			// Arrange
			var currency = "USD";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "currency", Value = currency }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, currency: currency);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_plantype()
		{
			// Arrange
			var planType = "dummy plan";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "plan_type", Value = planType }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, planType: planType);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_mailinglimit()
		{
			// Arrange
			var mailingLimit = 10;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_limit", Value = mailingLimit }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, mailingLimit: mailingLimit);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_monthlimit()
		{
			// Arrange
			var monthLimit = 100;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "month_limit", Value = monthLimit }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, monthLimit: monthLimit);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_contactlimit()
		{
			// Arrange
			var contactLimit = 1000;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "contact_limit", Value = contactLimit }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, contactLimit: contactLimit);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_defaultmailinglimit()
		{
			// Arrange
			var defaultMailingLimit = 10;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "default_mailing_limit", Value = defaultMailingLimit }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, defaultMailingLimit: defaultMailingLimit);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_defaultmonthlimit()
		{
			// Arrange
			var defaultMonthLimit = 100;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "default_month_limit", Value = defaultMonthLimit }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, defaultMonthLimit: defaultMonthLimit);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateClient_defaultcontactlimit()
		{
			// Arrange
			var defaultContactLimit = 1000;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "default_contact_limit", Value = defaultContactLimit }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, defaultContactLimit: defaultContactLimit);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task ActivateClient()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = "active" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.ActivateAsync(USER_KEY, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SuspendClient()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = "suspended_by_reseller" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.SuspendAsync(USER_KEY, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteClient()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = "deleted" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Client/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Clients.DeleteAsync(USER_KEY, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}
	}
}
