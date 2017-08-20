using CakeMail.RestClient.Models;
using RichardSzalay.MockHttp;
using Shouldly;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Resources
{
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

		[Fact]
		public async Task CreateClient_with_minimal_parameters()
		{
			// Arrange
			string name = "Fictitious Inc";
			string confirmationCode = "... dummy confirmation code ...";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", confirmationCode);

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.CreateAsync(CLIENT_ID, name);

			// Assert
			result.ShouldBe(confirmationCode);
		}

		[Fact]
		public async Task CreateClient_with_minimal_parameters_and_contactsameasadmin_false()
		{
			// Arrange
			string name = "Fictitious Inc";
			string confirmationCode = "... dummy confirmation code ...";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", confirmationCode);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.CreateAsync(CLIENT_ID, name, primaryContactSameAsAdmin: false);

			// Assert
			result.ShouldBe(confirmationCode);
		}

		[Fact]
		public async Task CreateClient_with_all_parameters()
		{
			// Arrange
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", CONFIRMATION_CODE);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.CreateAsync(CLIENT_ID, NAME, ADDRESS1, ADDRESS2, CITY, PROVINCE, POSTALCODE, COUNTRY_ID, WEBSITE, PHONE, FAX, ADMIN_EMAIL, ADMIN_FIRST_NAME, ADMIN_LAST_NAME, ADMIN_TITLE, ADMIN_OFFICE_PHONE, ADMIN_MOBILE_PHONE, ADMIN_LANGUAGE, ADMIN_TIMEZONE_ID, ADMIN_PASSWORD, false, PRIMARY_CONTACT_EMAIL, PRIMARY_CONTACT_FIRST_NAME, PRIMARE_CONTACT_LAST_NAME, PRIMARY_CONTACT_TITLE, PRIMARY_CONTACT_OFFICE_PHONE, PRIMARY_CONTACT_MOBILE_PHONE, PRIMARY_CONTACT_LANGUAGE, PRIMARY_CONTACT_TIMEZONE_ID, PRIMARY_CONTACT_PASSOWRD);

			// Assert
			result.ShouldBe(CONFIRMATION_CODE);
		}

		[Fact]
		public async Task CreateClient_admin_same_as_contact()
		{
			// Arrange
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", CONFIRMATION_CODE);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.CreateAsync(CLIENT_ID, NAME, ADDRESS1, ADDRESS2, CITY, PROVINCE, POSTALCODE, COUNTRY_ID, WEBSITE, PHONE, FAX, ADMIN_EMAIL, ADMIN_FIRST_NAME, ADMIN_LAST_NAME, ADMIN_TITLE, ADMIN_OFFICE_PHONE, ADMIN_MOBILE_PHONE, ADMIN_LANGUAGE, ADMIN_TIMEZONE_ID, ADMIN_PASSWORD, true, PRIMARY_CONTACT_EMAIL, PRIMARY_CONTACT_FIRST_NAME, PRIMARE_CONTACT_LAST_NAME, PRIMARY_CONTACT_TITLE, PRIMARY_CONTACT_OFFICE_PHONE, PRIMARY_CONTACT_MOBILE_PHONE, PRIMARY_CONTACT_LANGUAGE, PRIMARY_CONTACT_TIMEZONE_ID, PRIMARY_CONTACT_PASSOWRD);

			// Assert
			result.ShouldBe(CONFIRMATION_CODE);
		}

		[Fact]
		public async Task ConfirmClient()
		{
			// Arrange
			var confirmationId = "... dummy confirmation id ...";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"client_id\":\"{0}\",\"client_key\":\"...dummy client key...\",\"admin_id\":\"123\",\"admin_key\":\"...dummy admin key...\",\"contact_id\":\"456\",\"contact_key\":\"...dummy contact key...\"}}}}", CLIENT_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/Activate")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.ConfirmAsync(confirmationId);

			// Assert
			result.ShouldNotBeNull();
			result.ClientId.ShouldBe(CLIENT_ID);
		}

		[Fact]
		public async Task GetClient_by_confirmationcode()
		{
			// Arrange
			var confirmationCode = "...dummy confirmation code...";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"city\":\"Mock City\",\"company_name\":\"Fictitious Inc\",\"confirmation\":\"{0}\",\"address1\":\"123 1st Avenue\",\"address2\":\"Suite 1000\",\"country_id\":\"us\",\"currency\":\"USD\",\"fax\":\"222-222-2222\",\"parent_id\":\"1\",\"phone\":\"111-111-1111\",\"postal_code\":\"12345\",\"province_id\":\"FL\",\"status\":\"pending\",\"contact_same_as_admin\":\"1\",\"admin_email\":\"bobsmith@fictitiouscompany.com\",\"admin_password\":\"7cad97840d5b8e175870b1245a5fe9d8\",\"admin_first_name\":\"Bob\",\"admin_last_name\":\"Smith\",\"admin_language\":\"en_US\",\"admin_mobile_phone\":\"444-444-4444\",\"admin_office_phone\":\"333-333-3333\",\"admin_timezone_id\":\"542\",\"admin_title\":\"Administrator\",\"contact_email\":null,\"contact_password\":null,\"contact_first_name\":null,\"contact_last_name\":null,\"contact_language\":\"en_US\",\"contact_mobile_phone\":null,\"contact_office_phone\":null,\"contact_timezone_id\":\"152\",\"contact_title\":null,\"time\":\"2015-03-25 17:07:03\",\"last_confirmation\":\"2015-03-25 17:07:03\",\"expire\":\"2015-04-24 17:07:03\",\"website\":\"www.fictitiouscompany.com\",\"industry_id\":\"0\"}}}}", confirmationCode);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.GetAsync(USER_KEY, confirmationCode);

			// Assert
			result.ShouldNotBeNull();
			result.ConfirmationCode.ShouldBe(confirmationCode);
		}

		[Fact]
		public async Task GetClient_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lineage\":\"1-{0}\",\"address1\":\"123 1st Avenue\",\"address2\":\"Suite 1000\",\"config_id\":\"55\",\"auth_domain\":\"md02.com\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"parent_bounce_domain\":\"bounce.fictitiouscompany.com\",\"city\":\"Mock City\",\"company_name\":\"Fictitious Inc\",\"contact_id\":\"0\",\"country\":\"United States\",\"country_id\":\"us\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"dkim_domain\":\"md02.com\",\"doptin_ip\":\"192.168.77.1\",\"fax\":null,\"force_unsub\":\"false\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"parent_forward_domain\":\"http://forward.fictitiouscompany.com/\",\"forward_ip\":\"192.168.77.2\",\"id\":\"{0}\",\"key\":\"...dummy key...\",\"last_activity\":\"2015-03-25 14:20:54\",\"mailing_limit\":\"900000\",\"manager_id\":\"0\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"parent_md_domain\":\"http://link.fictitiouscompany.com/\",\"month_limit\":\"900000\",\"default_contact_limit\":\"0\",\"contact_limit\":\"0\",\"mta_id\":\"1\",\"parent_id\":\"1\",\"phone\":null,\"plan_min_vol\":null,\"plan_period\":null,\"plan_price\":null,\"plan_start_date\":null,\"plan_type\":null,\"postal_code\":null,\"pricing_plan\":\"courrielleur-demo\",\"province\":\"Florida\",\"province_id\":\"FL\",\"registered_date\":\"2015-03-01 00:00:00\",\"reseller\":\"false\",\"status\":\"active\",\"lineage_status\":\"active\",\"default_trial\":\"false\",\"default_reseller\":\"false\",\"billing_bundle_id\":\"0\",\"billing_package_id\":\"0\",\"billing_discount_percent\":\"0.000\",\"callback\":null,\"trigger_info\":\"false\",\"website\":null,\"industry_id\":null}}}}", CLIENT_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.GetAsync(USER_KEY, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(CLIENT_ID);
		}

		[Fact]
		public async Task GetClient_with_startdate()
		{
			// Arrange
			var startDate = new DateTime(2014, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			var endDate = (DateTime?)null;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lineage\":\"1-{0}\",\"address1\":\"123 1st Avenue\",\"address2\":\"Suite 1000\",\"config_id\":\"55\",\"auth_domain\":\"md02.com\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"parent_bounce_domain\":\"bounce.fictitiouscompany.com\",\"city\":\"Mock City\",\"company_name\":\"Fictitious Inc\",\"contact_id\":\"0\",\"country\":\"United States\",\"country_id\":\"us\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"dkim_domain\":\"md02.com\",\"doptin_ip\":\"192.168.77.1\",\"fax\":null,\"force_unsub\":\"false\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"parent_forward_domain\":\"http://forward.fictitiouscompany.com/\",\"forward_ip\":\"192.168.77.2\",\"id\":\"{0}\",\"key\":\"...dummy key...\",\"last_activity\":\"2015-03-25 14:20:54\",\"mailing_limit\":\"900000\",\"manager_id\":\"0\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"parent_md_domain\":\"http://link.fictitiouscompany.com/\",\"month_limit\":\"900000\",\"default_contact_limit\":\"0\",\"contact_limit\":\"0\",\"mta_id\":\"1\",\"parent_id\":\"1\",\"phone\":null,\"plan_min_vol\":null,\"plan_period\":null,\"plan_price\":null,\"plan_start_date\":null,\"plan_type\":null,\"postal_code\":null,\"pricing_plan\":\"courrielleur-demo\",\"province\":\"Florida\",\"province_id\":\"FL\",\"registered_date\":\"2015-03-01 00:00:00\",\"reseller\":\"false\",\"status\":\"active\",\"lineage_status\":\"active\",\"default_trial\":\"false\",\"default_reseller\":\"false\",\"billing_bundle_id\":\"0\",\"billing_package_id\":\"0\",\"billing_discount_percent\":\"0.000\",\"callback\":null,\"trigger_info\":\"false\",\"website\":null,\"industry_id\":null}}}}", CLIENT_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.GetAsync(USER_KEY, CLIENT_ID, startDate, endDate);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(CLIENT_ID);
		}

		[Fact]
		public async Task GetClient_with_enddate()
		{
			// Arrange
			var startDate = (DateTime?)null;
			var endDate = new DateTime(2014, 12, 31, 23, 59, 59, 999, DateTimeKind.Utc);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lineage\":\"1-{0}\",\"address1\":\"123 1st Avenue\",\"address2\":\"Suite 1000\",\"config_id\":\"55\",\"auth_domain\":\"md02.com\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"parent_bounce_domain\":\"bounce.fictitiouscompany.com\",\"city\":\"Mock City\",\"company_name\":\"Fictitious Inc\",\"contact_id\":\"0\",\"country\":\"United States\",\"country_id\":\"us\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"dkim_domain\":\"md02.com\",\"doptin_ip\":\"192.168.77.1\",\"fax\":null,\"force_unsub\":\"false\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"parent_forward_domain\":\"http://forward.fictitiouscompany.com/\",\"forward_ip\":\"192.168.77.2\",\"id\":\"{0}\",\"key\":\"...dummy key...\",\"last_activity\":\"2015-03-25 14:20:54\",\"mailing_limit\":\"900000\",\"manager_id\":\"0\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"parent_md_domain\":\"http://link.fictitiouscompany.com/\",\"month_limit\":\"900000\",\"default_contact_limit\":\"0\",\"contact_limit\":\"0\",\"mta_id\":\"1\",\"parent_id\":\"1\",\"phone\":null,\"plan_min_vol\":null,\"plan_period\":null,\"plan_price\":null,\"plan_start_date\":null,\"plan_type\":null,\"postal_code\":null,\"pricing_plan\":\"courrielleur-demo\",\"province\":\"Florida\",\"province_id\":\"FL\",\"registered_date\":\"2015-03-01 00:00:00\",\"reseller\":\"false\",\"status\":\"active\",\"lineage_status\":\"active\",\"default_trial\":\"false\",\"default_reseller\":\"false\",\"billing_bundle_id\":\"0\",\"billing_package_id\":\"0\",\"billing_discount_percent\":\"0.000\",\"callback\":null,\"trigger_info\":\"false\",\"website\":null,\"industry_id\":null}}}}", CLIENT_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.GetAsync(USER_KEY, CLIENT_ID, startDate, endDate);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(CLIENT_ID);
		}

		[Fact]
		public async Task GetClients_with_status()
		{
			// Arrange
			var status = ClientStatus.Active;
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.GetListAsync(USER_KEY, status: status);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetClients_with_name()
		{
			// Arrange
			var name = "Dummy Client";
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.GetListAsync(USER_KEY, name: name);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetClients_with_sortBy()
		{
			// Arrange
			var sortBy = ClientsSortBy.CompanyName;
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.GetListAsync(USER_KEY, sortBy: sortBy);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetClients_with_sortdirection()
		{
			// Arrange
			var sortDirection = Models.SortDirection.Ascending;
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.GetListAsync(USER_KEY, sortDirection: sortDirection);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetClients_with_limit()
		{
			// Arrange
			var limit = 11;
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.GetListAsync(USER_KEY, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetClients_with_offset()
		{
			// Arrange
			var offset = 33;
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.GetListAsync(USER_KEY, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetClients_with_clientid()
		{
			// Arrange
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.GetListAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetClientsCount_with_status()
		{
			// Arrange
			var status = ClientStatus.Active;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.GetCountAsync(USER_KEY, status: status);

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetClientsCount_with_name()
		{
			// Arrange
			var name = "Dummy Client";
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.GetCountAsync(USER_KEY, name: name);

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetClientsCount_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.GetCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBe(2);
		}

		[Fact]
		public async Task UpdateClient_name()
		{
			// Arrange
			var name = "Fictitious Inc";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, name: name);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_status()
		{
			// Arrange
			var status = ClientStatus.Active;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, status: status);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_parentid()
		{
			// Arrange
			var parentId = 1L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, parentId: parentId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_address1()
		{
			// Arrange
			var address1 = "123 1st Avenue";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, address1: address1);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_address2()
		{
			// Arrange
			var address2 = "Suite 1000";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, address2: address2);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_city()
		{
			// Arrange
			var city = "Mock City";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, city: city);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_provinceid()
		{
			// Arrange
			var provinceId = "FL";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, provinceId: provinceId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_postalcode()
		{
			// Arrange
			var postalCode = "12345";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, postalCode: postalCode);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_countryid()
		{
			// Arrange
			var countryId = "us";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, countryId: countryId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_website()
		{
			// Arrange
			var website = "www.fictitiouscompany.com";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, website: website);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_phone()
		{
			// Arrange
			var phone = "111-111-1111";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, phone: phone);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_fax()
		{
			// Arrange
			var fax = "222-222-2222";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, fax: fax);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_autdomain()
		{
			// Arrange
			var authDomain = "md02.com";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, authDomain: authDomain);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_bouncedomain()
		{
			// Arrange
			var bounceDomain = "bounce.fictitiouscompany.com";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, bounceDomain: bounceDomain);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_dkimdomain()
		{
			// Arrange
			var dkimDomain = "md02.com";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, dkimDomain: dkimDomain);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_doptinip()
		{
			// Arrange
			var doptinIp = "192.168.77.1";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, doptinIp: doptinIp);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_forwarddomain()
		{
			// Arrange
			var forwardDomain = "http://forward.fictitiouscompany.com/";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, forwardDomain: forwardDomain);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_forwardip()
		{
			// Arrange
			var forwardIp = "192.168.77.2";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, forwardIp: forwardIp);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_ippool()
		{
			// Arrange
			var ipPool = "dummy";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, ipPool: ipPool);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_mddomain()
		{
			// Arrange
			var mdDomain = "http://link.fictitiouscompany.com/";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, mdDomain: mdDomain);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_isreseller_true()
		{
			// Arrange
			var isReseller = true;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, isReseller: isReseller);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_isreseller_false()
		{
			// Arrange
			var isReseller = false;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, isReseller: isReseller);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_currency()
		{
			// Arrange
			var currency = "USD";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, currency: currency);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_plantype()
		{
			// Arrange
			var planType = "dummy plan";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, planType: planType);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_mailinglimit()
		{
			// Arrange
			var mailingLimit = 10;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, mailingLimit: mailingLimit);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_monthlimit()
		{
			// Arrange
			var monthLimit = 100;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, monthLimit: monthLimit);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_contactlimit()
		{
			// Arrange
			var contactLimit = 1000;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, contactLimit: contactLimit);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_defaultmailinglimit()
		{
			// Arrange
			var defaultMailingLimit = 10;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, defaultMailingLimit: defaultMailingLimit);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_defaultmonthlimit()
		{
			// Arrange
			var defaultMonthLimit = 100;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, defaultMonthLimit: defaultMonthLimit);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateClient_defaultcontactlimit()
		{
			// Arrange
			var defaultContactLimit = 1000;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.UpdateAsync(USER_KEY, CLIENT_ID, defaultContactLimit: defaultContactLimit);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task ActivateClient()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.ActivateAsync(USER_KEY, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task SuspendClient()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.SuspendAsync(USER_KEY, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteClient()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Clients.DeleteAsync(USER_KEY, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}
	}
}
