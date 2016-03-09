using CakeMail.RestClient.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class CakeMailRestClientTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[TestMethod]
		public void RestClient_constructor()
		{
			// Arrange
			var mockHost = "myhost";
			var mockProxyHost = "localhost";
			var mockProxyPort = 8888;
			var mockTimeout = 777;

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockHost, mockTimeout, new WebProxy(mockProxyHost, mockProxyPort));
			var userAgent = apiClient.UserAgent;
			var baseUrl = apiClient.BaseUrl;
			var timeout = apiClient.Timeout;
			var proxy = apiClient.Proxy;

			// Assert
			Assert.AreEqual("CakeMail .NET REST Client 2.0.0.0", userAgent);
			Assert.AreEqual(new Uri(string.Format("https://{0}", mockHost)), baseUrl);
			Assert.AreEqual(mockTimeout, timeout);
			Assert.AreEqual(new Uri(string.Format("http://{0}:{1}", mockProxyHost, mockProxyPort)), ((WebProxy)proxy).Address);
		}

		[TestMethod]
		public void RestClient_constructor_with_IRestClient()
		{
			// Arrange
			var mockUserAgent = "... this is a mock user agent string ...";
			var mockProxyHost = "localhost";
			var mockProxyPort = 8888;
			var mockBaseUrl = new Uri("http://127.0.0.1");
			var mockTimeout = 777;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.UserAgent).Returns(mockUserAgent);
			mockRestClient.Setup(m => m.Proxy).Returns(new WebProxy(mockProxyHost, mockProxyPort));
			mockRestClient.Setup(m => m.BaseUrl).Returns(mockBaseUrl);
			mockRestClient.Setup(m => m.Timeout).Returns(mockTimeout);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var userAgent = apiClient.UserAgent;
			var baseUrl = apiClient.BaseUrl;
			var timeout = apiClient.Timeout;
			var proxy = apiClient.Proxy;

			// Assert
			Assert.AreEqual(mockUserAgent, userAgent);
			Assert.AreEqual(mockBaseUrl, baseUrl);
			Assert.AreEqual(mockTimeout, timeout);
			Assert.AreEqual(new Uri(string.Format("http://{0}:{1}", mockProxyHost, mockProxyPort)), ((WebProxy)proxy).Address);
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_responsestatus_is_error()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Country/GetList/" &&
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Error
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_responsestatus_is_timeout()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Country/GetList/" &&
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.TimedOut
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_request_is_successful_but_response_content_is_empty()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Country/GetList/" &&
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = HttpStatusCode.OK,
				Content = ""
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(CakeMailException))]
		public void RestClient_Throws_exception_when_request_is_successful_but_response_contenttype_is_not_specified()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Country/GetList/" &&
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = HttpStatusCode.OK,
				Content = "dummy content"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_responsescode_is_between_400_and_499_and_content_is_empty()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Country/GetList/" &&
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = HttpStatusCode.Forbidden,
				Content = ""
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_responsescode_is_between_400_and_499_and_content_is_not_empty()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Country/GetList/" &&
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = HttpStatusCode.Forbidden,
				Content = "dummy content"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_responsescode_is_between_500_and_599()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Country/GetList/" &&
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = HttpStatusCode.InternalServerError
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_responsescode_is_greater_than_599()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Country/GetList/" &&
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = (HttpStatusCode)600
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_responsecode_is_greater_than_599_and_custom_errormessage()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Country/GetList/" &&
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = (HttpStatusCode)600,
				ErrorMessage = "This is a bogus error message"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(CakeMailException))]
		public void RestClient_Throws_exception_when_cakemail_api_returns_failure()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Country/GetList/" &&
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"failure\",\"data\":\"An error has occured\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(CakeMailPostException))]
		public void RestClient_Throws_exception_when_cakemail_api_returns_failure_with_post_details()
		{
			// Arrange
			var campaignId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/Delete/" &&
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"failed\",\"data\":\"There is no campaign with the id {0} and client id {1}!\",\"post\":{{\"user_key\":\"{2}\",\"campaign_id\":\"{0}\",\"client_id\":\"{1}\"}}}}", campaignId, CLIENT_ID, USER_KEY)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Campaigns.Delete(USER_KEY, campaignId, CLIENT_ID);
		}

		[TestMethod]
		[ExpectedException(typeof(CakeMailException))]
		public void RestClient_Throws_exception_when_reponse_contains_invalid_json()
		{
			// Arrange
			var campaignId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/Delete/" &&
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"{This content is not valid json (missing closing brackets)\""
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Campaigns.Delete(USER_KEY, campaignId, CLIENT_ID);
		}

		[TestMethod]
		[ExpectedException(typeof(CakeMailException))]
		public void RestClient_Throws_exception_when_reponse_does_not_contain_expected_property()
		{
			// Arrange
			var categoryId = 123;
			var labels = new Dictionary<string, string>()
			{
				{ "en_US", "My Category" }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/CreateCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[0][language]" && (string)p.Value == "en_US" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[0][name]" && (string)p.Value == "My Category" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"we_expected_a_property_named_id_but_instead_we_received_this_bogus_property\":\"{0}\"}}}}", categoryId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateTemplateCategory(USER_KEY, labels);
		}
	}
}
