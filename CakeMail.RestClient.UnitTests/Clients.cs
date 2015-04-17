using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class ClientsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[TestMethod]
		public void CreateClient_with_minimal_parameters()
		{
			// Arrange
			string name = "Fictitious Inc";

			string confirmationCode = "... dummy confirmation code ...";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "parent_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "company_name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "contact_same_as_admin" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", confirmationCode)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateClient(CLIENT_ID, name);

			// Assert
			Assert.AreEqual(confirmationCode, result);
		}

		[TestMethod]
		public void CreateClient_with_minimal_parameters_and_contactsameasadmin_false()
		{
			// Arrange
			string name = "Fictitious Inc";

			string confirmationCode = "... dummy confirmation code ...";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "parent_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "company_name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "contact_same_as_admin" && (string)p.Value == "0" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", confirmationCode)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateClient(CLIENT_ID, name, primaryContactSameAsAdmin: false);

			// Assert
			Assert.AreEqual(confirmationCode, result);
		}

		[TestMethod]
		public void CreateClient_with_all_parameters()
		{
			// Arrange
			string name = "Fictitious Inc";
			string address1 = "123 1st Avenue";
			string address2 = "Suite 1000";
			string city = "Mock City";
			string provinceId = "FL";
			string postalCode = "12345";
			string countryId = "us";
			string website = "www.fictitiouscompany.com";
			string phone = "111-111-1111";
			string fax = "222-222-2222";
			string adminEmail = "bobsmith@fictitiouscompany.com";
			string adminFirstName = "Bob";
			string adminLastName = "Smith";
			string adminTitle = "Administrator";
			string adminOfficePhone = "333-333-3333";
			string adminMobilePhone = "444-444-4444";
			string adminLanguage = "en";
			int? adminTimezoneId = 542;
			string adminPassword = "MySecretPassword";
			string primaryContactEmail = "janedoe@fictitiouscompany.com";
			string primaryContactFirstName = "Jane";
			string primaryContactLastName = "Doe";
			string primaryContactTitle = "CEO";
			string primaryContactOfficePhone = "555-555-5555";
			string primaryContactMobilePhone = "666-666-6666";
			string primaryContactLanguage = "en";
			int? primaryContactTimezoneId = 542;
			string primaryContactPassword = "SuperSecretPassword";

			string confirmationCode = "... dummy confirmation code ...";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/Create/" &&

				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 32 &&

				r.Parameters.Count(p => p.Name == "parent_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "company_name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&

				r.Parameters.Count(p => p.Name == "address1" && (string)p.Value == address1 && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "address2" && (string)p.Value == address2 && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "city" && (string)p.Value == city && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "province_id" && (string)p.Value == provinceId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "postal_code" && (string)p.Value == postalCode && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "country_id" && (string)p.Value == countryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "website" && (string)p.Value == website && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "phone" && (string)p.Value == phone && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "fax" && (string)p.Value == fax && p.Type == ParameterType.GetOrPost) == 1 &&

				r.Parameters.Count(p => p.Name == "admin_email" && (string)p.Value == adminEmail && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_first_name" && (string)p.Value == adminFirstName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_last_name" && (string)p.Value == adminLastName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_password" && (string)p.Value == adminPassword && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_password_confirmation" && (string)p.Value == adminPassword && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_title" && (string)p.Value == adminTitle && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_office_phone" && (string)p.Value == adminOfficePhone && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_mobile_phone" && (string)p.Value == adminMobilePhone && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_language" && (string)p.Value == adminLanguage && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_timezone_id" && (long)p.Value == adminTimezoneId && p.Type == ParameterType.GetOrPost) == 1 &&

				r.Parameters.Count(p => p.Name == "contact_same_as_admin" && (string)p.Value == "0" && p.Type == ParameterType.GetOrPost) == 1 &&

				r.Parameters.Count(p => p.Name == "contact_email" && (string)p.Value == primaryContactEmail && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "contact_first_name" && (string)p.Value == primaryContactFirstName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "contact_last_name" && (string)p.Value == primaryContactLastName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "contact_password" && (string)p.Value == primaryContactPassword && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "contact_password_confirmation" && (string)p.Value == primaryContactPassword && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "contact_title" && (string)p.Value == primaryContactTitle && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "contact_office_phone" && (string)p.Value == primaryContactOfficePhone && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "contact_mobile_phone" && (string)p.Value == primaryContactMobilePhone && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "contact_language" && (string)p.Value == primaryContactLanguage && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "contact_timezone_id" && (long)p.Value == primaryContactTimezoneId && p.Type == ParameterType.GetOrPost) == 1

			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", confirmationCode)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateClient(CLIENT_ID, name, address1, address2, city, provinceId, postalCode, countryId, website, phone, fax, adminEmail, adminFirstName, adminLastName, adminTitle, adminOfficePhone, adminMobilePhone, adminLanguage, adminTimezoneId, adminPassword, false, primaryContactEmail, primaryContactFirstName, primaryContactLastName, primaryContactTitle, primaryContactOfficePhone, primaryContactMobilePhone, primaryContactLanguage, primaryContactTimezoneId, primaryContactPassword);

			// Assert
			Assert.AreEqual(confirmationCode, result);
		}

		[TestMethod]
		public void CreateClient_admin_same_as_contact()
		{
			// Arrange
			string name = "Fictitious Inc";
			string address1 = "123 1st Avenue";
			string address2 = "Suite 1000";
			string city = "Mock City";
			string provinceId = "FL";
			string postalCode = "12345";
			string countryId = "us";
			string website = "www.fictitiouscompany.com";
			string phone = "111-111-1111";
			string fax = "222-222-2222";
			string adminEmail = "bobsmith@fictitiouscompany.com";
			string adminFirstName = "Bob";
			string adminLastName = "Smith";
			string adminTitle = "Administrator";
			string adminOfficePhone = "333-333-3333";
			string adminMobilePhone = "444-444-4444";
			string adminLanguage = "en";
			long? adminTimezoneId = 542;
			string adminPassword = "MySecretPassword";
			string primaryContactEmail = "janedoe@fictitiouscompany.com";
			string primaryContactFirstName = "Jane";
			string primaryContactLastName = "Doe";
			string primaryContactTitle = "CEO";
			string primaryContactOfficePhone = "555-555-5555";
			string primaryContactMobilePhone = "666-666-6666";
			string primaryContactLanguage = "en";
			long? primaryContactTimezoneId = 542;
			string primaryContactPassword = "SuperSecretPassword";

			string confirmationCode = "... dummy confirmation code ...";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 22 &&

				r.Parameters.Count(p => p.Name == "parent_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "company_name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&

				r.Parameters.Count(p => p.Name == "address1" && (string)p.Value == address1 && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "address2" && (string)p.Value == address2 && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "city" && (string)p.Value == city && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "province_id" && (string)p.Value == provinceId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "postal_code" && (string)p.Value == postalCode && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "country_id" && (string)p.Value == countryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "website" && (string)p.Value == website && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "phone" && (string)p.Value == phone && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "fax" && (string)p.Value == fax && p.Type == ParameterType.GetOrPost) == 1 &&

				r.Parameters.Count(p => p.Name == "admin_email" && (string)p.Value == adminEmail && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_first_name" && (string)p.Value == adminFirstName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_last_name" && (string)p.Value == adminLastName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_password" && (string)p.Value == adminPassword && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_password_confirmation" && (string)p.Value == adminPassword && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_title" && (string)p.Value == adminTitle && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_office_phone" && (string)p.Value == adminOfficePhone && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_mobile_phone" && (string)p.Value == adminMobilePhone && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_language" && (string)p.Value == adminLanguage && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "admin_timezone_id" && (long)p.Value == adminTimezoneId && p.Type == ParameterType.GetOrPost) == 1 &&

				r.Parameters.Count(p => p.Name == "contact_same_as_admin" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1

			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", confirmationCode)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateClient(CLIENT_ID, name, address1, address2, city, provinceId, postalCode, countryId, website, phone, fax, adminEmail, adminFirstName, adminLastName, adminTitle, adminOfficePhone, adminMobilePhone, adminLanguage, adminTimezoneId, adminPassword, true, primaryContactEmail, primaryContactFirstName, primaryContactLastName, primaryContactTitle, primaryContactOfficePhone, primaryContactMobilePhone, primaryContactLanguage, primaryContactTimezoneId, primaryContactPassword);

			// Assert
			Assert.AreEqual(confirmationCode, result);
		}

		[TestMethod]
		public void ConfirmClient()
		{
			// Arrange
			var confirmationId = "... dummy confirmation id ...";
			var clientId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/Activate/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "confirmation" && (string)p.Value == confirmationId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"client_id\":\"{0}\",\"client_key\":\"...dummy client key...\",\"admin_id\":\"123\",\"admin_key\":\"...dummy admin key...\",\"contact_id\":\"456\",\"contact_key\":\"...dummy contact key...\"}}}}", clientId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.ConfirmClient(confirmationId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(clientId, result.ClientId);
		}

		[TestMethod]
		public void GetClient_by_confirmationcode()
		{
			// Arrange
			var confirmationCode = "...dummy confirmation code...";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "confirmation" && (string)p.Value == confirmationCode && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"city\":\"Mock City\",\"company_name\":\"Fictitious Inc\",\"confirmation\":\"{0}\",\"address1\":\"123 1st Avenue\",\"address2\":\"Suite 1000\",\"country_id\":\"us\",\"currency\":\"USD\",\"fax\":\"222-222-2222\",\"parent_id\":\"1\",\"phone\":\"111-111-1111\",\"postal_code\":\"12345\",\"province_id\":\"FL\",\"status\":\"pending\",\"contact_same_as_admin\":\"1\",\"admin_email\":\"bobsmith@fictitiouscompany.com\",\"admin_password\":\"7cad97840d5b8e175870b1245a5fe9d8\",\"admin_first_name\":\"Bob\",\"admin_last_name\":\"Smith\",\"admin_language\":\"en_US\",\"admin_mobile_phone\":\"444-444-4444\",\"admin_office_phone\":\"333-333-3333\",\"admin_timezone_id\":\"542\",\"admin_title\":\"Administrator\",\"contact_email\":null,\"contact_password\":null,\"contact_first_name\":null,\"contact_last_name\":null,\"contact_language\":\"en_US\",\"contact_mobile_phone\":null,\"contact_office_phone\":null,\"contact_timezone_id\":\"152\",\"contact_title\":null,\"time\":\"2015-03-25 17:07:03\",\"last_confirmation\":\"2015-03-25 17:07:03\",\"expire\":\"2015-04-24 17:07:03\",\"website\":\"www.fictitiouscompany.com\",\"industry_id\":\"0\"}}}}", confirmationCode)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetClient(USER_KEY, confirmationCode);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(confirmationCode, result.ConfirmationCode);
		}

		[TestMethod]
		public void GetClient_with_minimal_parameters()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lineage\":\"1-{0}\",\"address1\":\"123 1st Avenue\",\"address2\":\"Suite 1000\",\"config_id\":\"55\",\"auth_domain\":\"md02.com\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"parent_bounce_domain\":\"bounce.fictitiouscompany.com\",\"city\":\"Mock City\",\"company_name\":\"Fictitious Inc\",\"contact_id\":\"0\",\"country\":\"United States\",\"country_id\":\"us\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"dkim_domain\":\"md02.com\",\"doptin_ip\":\"192.168.77.1\",\"fax\":null,\"force_unsub\":\"false\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"parent_forward_domain\":\"http://forward.fictitiouscompany.com/\",\"forward_ip\":\"192.168.77.2\",\"id\":\"{0}\",\"key\":\"...dummy key...\",\"last_activity\":\"2015-03-25 14:20:54\",\"mailing_limit\":\"900000\",\"manager_id\":\"0\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"parent_md_domain\":\"http://link.fictitiouscompany.com/\",\"month_limit\":\"900000\",\"default_contact_limit\":\"0\",\"contact_limit\":\"0\",\"mta_id\":\"1\",\"parent_id\":\"1\",\"phone\":null,\"plan_min_vol\":null,\"plan_period\":null,\"plan_price\":null,\"plan_start_date\":null,\"plan_type\":null,\"postal_code\":null,\"pricing_plan\":\"courrielleur-demo\",\"province\":\"Florida\",\"province_id\":\"FL\",\"registered_date\":\"2015-03-01 00:00:00\",\"reseller\":\"false\",\"status\":\"active\",\"lineage_status\":\"active\",\"default_trial\":\"false\",\"default_reseller\":\"false\",\"billing_bundle_id\":\"0\",\"billing_package_id\":\"0\",\"billing_discount_percent\":\"0.000\",\"callback\":null,\"trigger_info\":\"false\",\"website\":null,\"industry_id\":null}}}}", CLIENT_ID)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetClient(USER_KEY, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(CLIENT_ID, result.Id);
		}

		[TestMethod]
		public void GetClient_with_startdate()
		{
			// Arrange
			var startDate = new DateTime(2014, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			var endDate = (DateTime?)null;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "start_date" && (string)p.Value == startDate.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lineage\":\"1-{0}\",\"address1\":\"123 1st Avenue\",\"address2\":\"Suite 1000\",\"config_id\":\"55\",\"auth_domain\":\"md02.com\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"parent_bounce_domain\":\"bounce.fictitiouscompany.com\",\"city\":\"Mock City\",\"company_name\":\"Fictitious Inc\",\"contact_id\":\"0\",\"country\":\"United States\",\"country_id\":\"us\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"dkim_domain\":\"md02.com\",\"doptin_ip\":\"192.168.77.1\",\"fax\":null,\"force_unsub\":\"false\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"parent_forward_domain\":\"http://forward.fictitiouscompany.com/\",\"forward_ip\":\"192.168.77.2\",\"id\":\"{0}\",\"key\":\"...dummy key...\",\"last_activity\":\"2015-03-25 14:20:54\",\"mailing_limit\":\"900000\",\"manager_id\":\"0\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"parent_md_domain\":\"http://link.fictitiouscompany.com/\",\"month_limit\":\"900000\",\"default_contact_limit\":\"0\",\"contact_limit\":\"0\",\"mta_id\":\"1\",\"parent_id\":\"1\",\"phone\":null,\"plan_min_vol\":null,\"plan_period\":null,\"plan_price\":null,\"plan_start_date\":null,\"plan_type\":null,\"postal_code\":null,\"pricing_plan\":\"courrielleur-demo\",\"province\":\"Florida\",\"province_id\":\"FL\",\"registered_date\":\"2015-03-01 00:00:00\",\"reseller\":\"false\",\"status\":\"active\",\"lineage_status\":\"active\",\"default_trial\":\"false\",\"default_reseller\":\"false\",\"billing_bundle_id\":\"0\",\"billing_package_id\":\"0\",\"billing_discount_percent\":\"0.000\",\"callback\":null,\"trigger_info\":\"false\",\"website\":null,\"industry_id\":null}}}}", CLIENT_ID)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetClient(USER_KEY, CLIENT_ID, startDate, endDate);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(CLIENT_ID, result.Id);
		}

		[TestMethod]
		public void GetClient_with_enddate()
		{
			// Arrange
			var startDate = (DateTime?)null;
			var endDate = new DateTime(2014, 12, 31, 23, 59, 59, 999, DateTimeKind.Utc);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "end_date" && (string)p.Value == endDate.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lineage\":\"1-{0}\",\"address1\":\"123 1st Avenue\",\"address2\":\"Suite 1000\",\"config_id\":\"55\",\"auth_domain\":\"md02.com\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"parent_bounce_domain\":\"bounce.fictitiouscompany.com\",\"city\":\"Mock City\",\"company_name\":\"Fictitious Inc\",\"contact_id\":\"0\",\"country\":\"United States\",\"country_id\":\"us\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"dkim_domain\":\"md02.com\",\"doptin_ip\":\"192.168.77.1\",\"fax\":null,\"force_unsub\":\"false\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"parent_forward_domain\":\"http://forward.fictitiouscompany.com/\",\"forward_ip\":\"192.168.77.2\",\"id\":\"{0}\",\"key\":\"...dummy key...\",\"last_activity\":\"2015-03-25 14:20:54\",\"mailing_limit\":\"900000\",\"manager_id\":\"0\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"parent_md_domain\":\"http://link.fictitiouscompany.com/\",\"month_limit\":\"900000\",\"default_contact_limit\":\"0\",\"contact_limit\":\"0\",\"mta_id\":\"1\",\"parent_id\":\"1\",\"phone\":null,\"plan_min_vol\":null,\"plan_period\":null,\"plan_price\":null,\"plan_start_date\":null,\"plan_type\":null,\"postal_code\":null,\"pricing_plan\":\"courrielleur-demo\",\"province\":\"Florida\",\"province_id\":\"FL\",\"registered_date\":\"2015-03-01 00:00:00\",\"reseller\":\"false\",\"status\":\"active\",\"lineage_status\":\"active\",\"default_trial\":\"false\",\"default_reseller\":\"false\",\"billing_bundle_id\":\"0\",\"billing_package_id\":\"0\",\"billing_discount_percent\":\"0.000\",\"callback\":null,\"trigger_info\":\"false\",\"website\":null,\"industry_id\":null}}}}", CLIENT_ID)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetClient(USER_KEY, CLIENT_ID, startDate, endDate);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(CLIENT_ID, result.Id);
		}

		[TestMethod]
		public void GetClients_with_status()
		{
			// Arrange
			var status = "active";

			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetClients(USER_KEY, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetClients_with_name()
		{
			// Arrange
			var name = "Dummy Client";

			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "company_name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetClients(USER_KEY, name: name);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetClients_with_sortBy()
		{
			// Arrange
			var sortBy = "company_name";

			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sort_by" && (string)p.Value == sortBy && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetClients(USER_KEY, sortBy: sortBy);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetClients_with_sortdirection()
		{
			// Arrange
			var sortDirection = "asc";

			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "direction" && (string)p.Value == sortDirection && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetClients(USER_KEY, sortDirection: sortDirection);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetClients_with_limit()
		{
			// Arrange
			var limit = 11;

			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetClients(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetClients_with_offset()
		{
			// Arrange
			var offset = 33;

			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetClients(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetClients_with_clientid()
		{
			// Arrange
			var jsonClient1 = "{\"company_name\":\"Dummy Client #1\",\"contact_id\":\"123\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"111\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-09 14:34:48\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";
			var jsonClient2 = "{\"company_name\":\"Dummy Client #2\",\"contact_id\":\"456\",\"currency\":\"USD\",\"default_mailing_limit\":\"25\",\"default_month_limit\":\"250\",\"default_contact_limit\":\"0\",\"id\":\"222\",\"reseller\":\"false\",\"mailing_limit\":\"25\",\"contact_limit\":\"0\",\"manager_id\":\"0\",\"month_limit\":\"250\",\"parent_id\":\"1\",\"registered_date\":\"2015-03-24 22:36:40\",\"status\":\"active\",\"md_domain\":\"http://link.fictitiouscompany.com/\",\"bounce_domain\":\"bounce.fictitiouscompany.com\",\"forward_domain\":\"http://forward.fictitiouscompany.com/\",\"lineage\":\"1\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonClient1, jsonClient2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetClients(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetClientsCount_with_status()
		{
			// Arrange
			var status = "active";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetClientsCount(USER_KEY, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetClientsCount_with_name()
		{
			// Arrange
			var name = "Dummy Client";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "company_name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetClientsCount(USER_KEY, name: name);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetClientsCount_with_clientid()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetClientsCount(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void UpdateClient_name()
		{
			// Arrange
			var name = "Fictitious Inc";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "company_name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, name: name);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_status()
		{
			// Arrange
			var status = "active";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, status: status);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_parentid()
		{
			// Arrange
			var parentId = 1;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "parent_id" && (long)p.Value == parentId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, parentId: parentId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_address1()
		{
			// Arrange
			var address1 = "123 1st Avenue";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "address1" && (string)p.Value == address1 && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, address1: address1);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_address2()
		{
			// Arrange
			var address2 = "Suite 1000";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "address2" && (string)p.Value == address2 && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, address2: address2);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_city()
		{
			// Arrange
			var city = "Mock City";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "city" && (string)p.Value == city && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, city: city);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_provinceid()
		{
			// Arrange
			var provinceId = "FL";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "province_id" && (string)p.Value == provinceId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, provinceId: provinceId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_postalcode()
		{
			// Arrange
			var postalCode = "12345";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "postal_code" && (string)p.Value == postalCode && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, postalCode: postalCode);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_countryid()
		{
			// Arrange
			var countryId = "us";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "country_id" && (string)p.Value == countryId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, countryId: countryId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_website()
		{
			// Arrange
			var website = "www.fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "website" && (string)p.Value == website && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, website: website);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_phone()
		{
			// Arrange
			var phone = "111-111-1111";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "phone" && (string)p.Value == phone && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, phone: phone);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_fax()
		{
			// Arrange
			var fax = "222-222-2222";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "fax" && (string)p.Value == fax && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, fax: fax);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_autdomain()
		{
			// Arrange
			var authDomain = "md02.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "auth_domain" && (string)p.Value == authDomain && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, authDomain: authDomain);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_bouncedomain()
		{
			// Arrange
			var bounceDomain = "bounce.fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "bounce_domain" && (string)p.Value == bounceDomain && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, bounceDomain: bounceDomain);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_dkimdomain()
		{
			// Arrange
			var dkimDomain = "md02.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "dkim_domain" && (string)p.Value == dkimDomain && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, dkimDomain: dkimDomain);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_doptinip()
		{
			// Arrange
			var doptinIp = "192.168.77.1";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "doptin_ip" && (string)p.Value == doptinIp && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, doptinIp: doptinIp);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_forwarddomain()
		{
			// Arrange
			var forwardDomain = "http://forward.fictitiouscompany.com/";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "forward_domain" && (string)p.Value == forwardDomain && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, forwardDomain: forwardDomain);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_forwardip()
		{
			// Arrange
			var forwardIp = "192.168.77.2";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "forward_ip" && (string)p.Value == forwardIp && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, forwardIp: forwardIp);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_ippool()
		{
			// Arrange
			var ipPool = "dummy";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "ip_pool" && (string)p.Value == ipPool && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, ipPool: ipPool);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_mddomain()
		{
			// Arrange
			var mdDomain = "http://link.fictitiouscompany.com/";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "md_domain" && (string)p.Value == mdDomain && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, mdDomain: mdDomain);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_isreseller_true()
		{
			// Arrange
			var isReseller = true;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "reseller" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, isReseller: isReseller);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_isreseller_false()
		{
			// Arrange
			var isReseller = false;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "reseller" && (string)p.Value == "0" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, isReseller: isReseller);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_currency()
		{
			// Arrange
			var currency = "USD";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "currency" && (string)p.Value == currency && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, currency: currency);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_plantype()
		{
			// Arrange
			var planType = "dummy plan";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "plan_type" && (string)p.Value == planType && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, planType: planType);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_mailinglimit()
		{
			// Arrange
			var mailingLimit = 10;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_limit" && (int)p.Value == mailingLimit && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, mailingLimit: mailingLimit);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_monthlimit()
		{
			// Arrange
			var monthLimit = 100;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "month_limit" && (int)p.Value == monthLimit && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, monthLimit: monthLimit);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_contactlimit()
		{
			// Arrange
			var contactLimit = 1000;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "contact_limit" && (int)p.Value == contactLimit && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, contactLimit: contactLimit);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_defaultmailinglimit()
		{
			// Arrange
			var defaultMailingLimit = 10;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default_mailing_limit" && (int)p.Value == defaultMailingLimit && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, defaultMailingLimit: defaultMailingLimit);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_defaultmonthlimit()
		{
			// Arrange
			var defaultMonthLimit = 100;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default_month_limit" && (int)p.Value == defaultMonthLimit && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, defaultMonthLimit: defaultMonthLimit);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateClient_defaultcontactlimit()
		{
			// Arrange
			var defaultContactLimit = 1000;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default_contact_limit" && (int)p.Value == defaultContactLimit && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateClient(USER_KEY, CLIENT_ID, defaultContactLimit: defaultContactLimit);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void ActivateClient()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "json",
					Content = "{\"status\":\"success\",\"data\":\"true\"}"
				});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.ActivateClient(USER_KEY, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void SuspendClient()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == "suspended_by_reseller" && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "json",
					Content = "{\"status\":\"success\",\"data\":\"true\"}"
				});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.SuspendClient(USER_KEY, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void DeleteClient()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == "deleted" && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "json",
					Content = "{\"status\":\"success\",\"data\":\"true\"}"
				});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteClient(USER_KEY, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}
	}
}
