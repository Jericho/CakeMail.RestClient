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
		private const int CLIENT_ID = 999;

		[TestMethod]
		public void CreateClient_with_all_parameters()
		{
			// Arrange
			string name = "The Client";
			string address1 = "123 1st Avenue";
			string address2 = "Suite 1000";
			string city = "Mock City";
			string provinceId = "fl";
			string postalCode = "12345";
			string countryId = "us";
			string website = "www.ficticiouscompany.com";
			string phone = "111-111-1111";
			string fax = "222-222-2222";
			string adminEmail = "bobsmith@ficticiouscompany.com";
			string adminFirstName = "Bob";
			string adminLastName = "Smith";
			string adminTitle = "Administrator";
			string adminOfficePhone = "333-333-3333";
			string adminMobilePhone = "444-444-4444";
			string adminLanguage = "en";
			int? adminTimezoneId = 542;
			string adminPassword = "MySecretPassword";
			string primaryContactEmail = "janedoe@ficticiouscompany.com";
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
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 32 &&

				r.Parameters.Count(p => p.Name == "parent_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
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
				r.Parameters.Count(p => p.Name == "admin_timezone_id" && (int)p.Value == adminTimezoneId && p.Type == ParameterType.GetOrPost) == 1 &&

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
				r.Parameters.Count(p => p.Name == "contact_timezone_id" && (int)p.Value == primaryContactTimezoneId && p.Type == ParameterType.GetOrPost) == 1

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
		public void CreateClient_with_minimal_parameters()
		{
			// Arrange
			string name = "The Client";
			string address1 = null;
			string address2 = null;
			string city = null;
			string provinceId = null;
			string postalCode = null;
			string countryId = null;
			string website = null;
			string phone = null;
			string fax = null;
			string adminEmail = null;
			string adminFirstName = null;
			string adminLastName = null;
			string adminTitle = null;
			string adminOfficePhone = null;
			string adminMobilePhone = null;
			string adminLanguage = null;
			int? adminTimezoneId = null;
			string adminPassword = null;
			string primaryContactEmail = null;
			string primaryContactFirstName = null;
			string primaryContactLastName = null;
			string primaryContactTitle = null;
			string primaryContactOfficePhone = null;
			string primaryContactMobilePhone = null;
			string primaryContactLanguage = null;
			int? primaryContactTimezoneId = null;
			string primaryContactPassword = null;

			string confirmationCode = "... dummy confirmation code ...";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "parent_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.CreateClient(CLIENT_ID, name, address1, address2, city, provinceId, postalCode, countryId, website, phone, fax, adminEmail, adminFirstName, adminLastName, adminTitle, adminOfficePhone, adminMobilePhone, adminLanguage, adminTimezoneId, adminPassword, false, primaryContactEmail, primaryContactFirstName, primaryContactLastName, primaryContactTitle, primaryContactOfficePhone, primaryContactMobilePhone, primaryContactLanguage, primaryContactTimezoneId, primaryContactPassword);

			// Assert
			Assert.AreEqual(confirmationCode, result);
		}

		[TestMethod]
		public void CreateClient_admin_same_as_contact()
		{
			// Arrange
			string name = "The Client";
			string address1 = "123 1st Avenue";
			string address2 = "Suite 1000";
			string city = "Mock City";
			string provinceId = "fl";
			string postalCode = "12345";
			string countryId = "us";
			string website = "www.ficticiouscompany.com";
			string phone = "111-111-1111";
			string fax = "222-222-2222";
			string adminEmail = "bobsmith@ficticiouscompany.com";
			string adminFirstName = "Bob";
			string adminLastName = "Smith";
			string adminTitle = "Administrator";
			string adminOfficePhone = "333-333-3333";
			string adminMobilePhone = "444-444-4444";
			string adminLanguage = "en";
			int? adminTimezoneId = 542;
			string adminPassword = "MySecretPassword";
			string primaryContactEmail = "janedoe@ficticiouscompany.com";
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
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 22 &&

				r.Parameters.Count(p => p.Name == "parent_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
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
				r.Parameters.Count(p => p.Name == "admin_timezone_id" && (int)p.Value == adminTimezoneId && p.Type == ParameterType.GetOrPost) == 1 &&

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
		public void ActivateClient()
		{
			// Arrange
			var confirmationId = "... dummy confirmation id ...";
			var clientId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
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
			var result = apiClient.ActivateClient(confirmationId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(clientId, result.ClientId);
		}
	}
}
