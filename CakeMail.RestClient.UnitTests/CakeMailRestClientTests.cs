using CakeMail.RestClient.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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
			var userAgentParts = userAgent.Split(new[] { ';' });
			var baseUrl = apiClient.BaseUrl;
			var timeout = apiClient.Timeout;
			var proxy = apiClient.Proxy;

			// Assert
			Assert.AreEqual("CakeMail .NET REST Client", userAgentParts[0]);
			Assert.AreEqual(new Uri($"https://{mockHost}"), baseUrl);
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
		public async Task RestClient_Throws_exception_when_responsestatus_is_error()
		{
			// Arrange
			var mockRestClient = new MockRestClient("/Country/GetList/", Enumerable.Empty<Parameter>(), ResponseStatus.Error, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Countries.GetListAsync();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public async Task RestClient_Throws_exception_when_responsestatus_is_timeout()
		{
			// Arrange
			var mockRestClient = new MockRestClient("/Country/GetList/", Enumerable.Empty<Parameter>(), ResponseStatus.TimedOut, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Countries.GetListAsync();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public async Task RestClient_Throws_exception_when_request_is_successful_but_response_content_is_empty()
		{
			// Arrange
			var mockRestClient = new MockRestClient("/Country/GetList/", Enumerable.Empty<Parameter>(), "", false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Countries.GetListAsync();
		}

		[TestMethod]
		[ExpectedException(typeof(CakeMailException))]
		public async Task RestClient_Throws_exception_when_request_is_successful_but_response_contenttype_is_not_specified()
		{
			// Arrange
			var mockRestClient = new MockRestClient("/Country/GetList/", Enumerable.Empty<Parameter>(), "dummy content", false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Countries.GetListAsync();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public async Task RestClient_Throws_exception_when_responsescode_is_between_400_and_499_and_content_is_empty()
		{
			// Arrange
			var response = new RestResponse
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = HttpStatusCode.Forbidden,
				Content = ""
			};
			var mockRestClient = new MockRestClient("/Country/GetList/", Enumerable.Empty<Parameter>(), response, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Countries.GetListAsync();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public async Task RestClient_Throws_exception_when_responsescode_is_between_400_and_499_and_content_is_not_empty()
		{
			// Arrange
			var response = new RestResponse
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = HttpStatusCode.Forbidden,
				Content = "dummy content"
			};
			var mockRestClient = new MockRestClient("/Country/GetList/", Enumerable.Empty<Parameter>(), response, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Countries.GetListAsync();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public async Task RestClient_Throws_exception_when_responsescode_is_between_500_and_599()
		{
			// Arrange
			var response = new RestResponse
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = HttpStatusCode.InternalServerError
			};
			var mockRestClient = new MockRestClient("/Country/GetList/", Enumerable.Empty<Parameter>(), response, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Countries.GetListAsync();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public async Task RestClient_Throws_exception_when_responsescode_is_greater_than_599()
		{
			// Arrange
			var response = new RestResponse
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = (HttpStatusCode)600
			};
			var mockRestClient = new MockRestClient("/Country/GetList/", Enumerable.Empty<Parameter>(), response, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Countries.GetListAsync();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public async Task RestClient_Throws_exception_when_responsecode_is_greater_than_599_and_custom_errormessage()
		{
			// Arrange
			var response = new RestResponse
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = (HttpStatusCode)600,
				ErrorMessage = "This is a bogus error message"
			};
			var mockRestClient = new MockRestClient("/Country/GetList/", Enumerable.Empty<Parameter>(), response, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Countries.GetListAsync();
		}

		[TestMethod]
		[ExpectedException(typeof(CakeMailException))]
		public async Task RestClient_Throws_exception_when_cakemail_api_returns_failure()
		{
			// Arrange
			var response = new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"failure\",\"data\":\"An error has occured\"}"
			};
			var mockRestClient = new MockRestClient("/Country/GetList/", Enumerable.Empty<Parameter>(), response, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Countries.GetListAsync().ConfigureAwait(false);
		}

		[TestMethod]
		[ExpectedException(typeof(CakeMailPostException))]
		public async Task RestClient_Throws_exception_when_cakemail_api_returns_failure_with_post_details()
		{
			// Arrange
			var campaignId = 123L;
			var jsonResponse = string.Format("{{\"status\":\"failed\",\"data\":\"There is no campaign with the id {0} and client id {1}!\",\"post\":{{\"user_key\":\"{2}\",\"campaign_id\":\"{0}\",\"client_id\":\"{1}\"}}}}", campaignId, CLIENT_ID, USER_KEY);
			var parameters = new Parameter[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "campaign_id", Value = campaignId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var mockRestClient = new MockRestClient("/Campaign/Delete/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.DeleteAsync(USER_KEY, campaignId, CLIENT_ID);
		}

		[TestMethod]
		[ExpectedException(typeof(CakeMailException))]
		public async Task RestClient_Throws_exception_when_reponse_contains_invalid_json()
		{
			// Arrange
			var campaignId = 123L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"{This content is not valid json (missing closing brackets)\"";
			var parameters = new Parameter[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "campaign_id", Value = campaignId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var mockRestClient = new MockRestClient("/Campaign/Delete/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.DeleteAsync(USER_KEY, campaignId, CLIENT_ID);
		}

		[TestMethod]
		[ExpectedException(typeof(CakeMailException))]
		public async Task RestClient_Throws_exception_when_reponse_does_not_contain_expected_property()
		{
			// Arrange
			var categoryId = 123;
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"we_expected_a_property_named_id_but_instead_we_received_this_bogus_property\":\"{0}\"}}}}", categoryId);
			var parameters = new Parameter[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "default", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "templates_copyable", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[0][language]", Value = "en_US" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[0][name]", Value = "My Category" }
			};
			var mockRestClient = new MockRestClient("/TemplateV2/CreateCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.CreateCategoryAsync(USER_KEY, labels);
		}
	}
}
