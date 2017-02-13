using CakeMail.RestClient.Exceptions;
using Moq;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CakeMail.RestClient.UnitTests
{
	public class CakeMailRestClientTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[Fact]
		public void Version_is_not_empty()
		{
			// Arrange
			var client = new CakeMailRestClient(API_KEY);

			// Act
			var result = client.Version;

			// Assert
			result.ShouldNotBeNullOrEmpty();
		}

		[Fact]
		public void RestClient_constructor_with_ApiKey()
		{
			// Arrange

			// Act
			var client = new CakeMailRestClient(API_KEY);
			var userAgent = client.UserAgent;
			var baseUrl = client.BaseUrl;

			// Assert
			userAgent.Split(new[] { '/' })[0].ShouldBe("CakeMail .NET REST Client");
			userAgent.Split(new[] { '+' })[1].Trim(new[] { '(', ')' }).ShouldBe("https://github.com/Jericho/CakeMail.RestClient");
			baseUrl.ShouldBe(new Uri("https://api.wbsrvc.com"));
		}

		[Fact]
		public void RestClient_constructor_with_IWebProxy()
		{
			// Arrange
			var mockProxy = new Mock<IWebProxy>(MockBehavior.Strict);

			// Act
			var client = new CakeMailRestClient(API_KEY, mockProxy.Object);
			var baseUrl = client.BaseUrl;

			// Assert
			baseUrl.ShouldBe(new Uri("https://api.wbsrvc.com"));
		}

		[Fact]
		public void RestClient_constructor_with_HttpClient()
		{
			// Arrange
			var mockHost = "my.apiserver.com";

			var mockHttp = new MockHttpMessageHandler();
			var httpClient = new HttpClient(mockHttp);

			// Act
			var client = new CakeMailRestClient(API_KEY, mockHost, httpClient);
			var baseUrl = client.BaseUrl;

			// Assert
			mockHttp.VerifyNoOutstandingExpectation();
			mockHttp.VerifyNoOutstandingRequest();
			baseUrl.ShouldBe(new Uri($"https://{mockHost}"));
		}

		[Fact]
		public async Task RestClient_Throws_exception_when_responsestatus_is_error()
		{
			// Arrange
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Country/GetList")).Respond(HttpStatusCode.BadRequest);

			// Act
			var client = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			await Should.ThrowAsync<HttpException>(async () =>
			{
				var result = await client.Countries.GetListAsync().ConfigureAwait(false);
			});
		}

		[Fact]
		public async Task RestClient_Throws_exception_when_responsestatus_is_timeout()
		{
			// Arrange
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Country/GetList")).Respond(HttpStatusCode.RequestTimeout);

			// Act
			var client = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			await Should.ThrowAsync<HttpException>(async () =>
			{
				var result = await client.Countries.GetListAsync().ConfigureAwait(false);
			});
		}

		[Fact]
		public async Task RestClient_Throws_exception_when_request_is_successful_but_response_content_is_empty()
		{
			// Arrange
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Country/GetList")).Respond(HttpStatusCode.OK, new StringContent(""));

			// Act
			var client = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			await Should.ThrowAsync<JsonReaderException>(async () =>
			{
				var result = await client.Countries.GetListAsync().ConfigureAwait(false);
			});
		}

		[Fact]
		public async Task RestClient_Throws_exception_when_responsescode_is_between_400_and_499_and_content_is_empty()
		{
			// Arrange
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Country/GetList")).Respond((HttpStatusCode)450);

			// Act
			var client = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			await Should.ThrowAsync<HttpException>(async () =>
			{
				var result = await client.Countries.GetListAsync().ConfigureAwait(false);
			});
		}

		[Fact]
		public async Task RestClient_Throws_exception_when_responsescode_is_between_400_and_499_and_content_is_not_empty()
		{
			// Arrange
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Country/GetList")).Respond((HttpStatusCode)450, new StringContent("dummy content", Encoding.UTF8, "application/json"));

			// Act
			var client = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			await Should.ThrowAsync<HttpException>(async () =>
			{
				var result = await client.Countries.GetListAsync().ConfigureAwait(false);
			});
		}

		[Fact]
		public async Task RestClient_Throws_exception_when_responsescode_is_between_500_and_599()
		{
			// Arrange
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Country/GetList")).Respond((HttpStatusCode)550);

			// Act
			var client = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			await Should.ThrowAsync<HttpException>(async () =>
			{
				var result = await client.Countries.GetListAsync().ConfigureAwait(false);
			});
		}

		[Fact]
		public async Task RestClient_Throws_exception_when_responsescode_is_greater_than_599()
		{
			// Arrange
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Country/GetList")).Respond((HttpStatusCode)600);

			// Act
			var client = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			await Should.ThrowAsync<HttpException>(async () =>
			{
				var result = await client.Countries.GetListAsync().ConfigureAwait(false);
			});
		}

		[Fact]
		public async Task RestClient_Throws_exception_when_responsecode_is_greater_than_599_and_custom_errormessage()
		{
			// Arrange
			var mockHttp = new MockHttpMessageHandler();
			mockHttp
				.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Country/GetList"))
				.Respond((HttpStatusCode)600, new StringContent("This is a bogus error message", Encoding.UTF8, "application/json"));

			// Act
			var client = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			await Should.ThrowAsync<HttpException>(async () =>
			{
				var result = await client.Countries.GetListAsync().ConfigureAwait(false);
			});
		}

		[Fact]
		public async Task RestClient_Throws_exception_when_cakemail_api_returns_failure_with_post_details()
		{
			// Arrange
			var campaignId = 123L;
			var jsonResponse = string.Format("{{\"status\":\"failed\",\"data\":\"There is no campaign with the id {0} and client id {1}!\",\"post\":{{\"user_key\":\"{2}\",\"campaign_id\":\"{0}\",\"client_id\":\"{1}\"}}}}", campaignId, CLIENT_ID, USER_KEY);

			var mockHttp = new MockHttpMessageHandler();
			mockHttp
				.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/Delete"))
				.Respond((HttpStatusCode)200, new StringContent(jsonResponse, Encoding.UTF8, "application/json"));

			// Act
			var client = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			await Should.ThrowAsync<CakeMailPostException>(async () =>
			{
				var result = await client.Campaigns.DeleteAsync(USER_KEY, campaignId, CLIENT_ID);
			});
		}

		[Fact]
		public async Task RestClient_Throws_exception_when_reponse_contains_invalid_json()
		{
			// Arrange
			var campaignId = 123L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"{This content is not valid json (missing closing brackets)\"";

			var mockHttp = new MockHttpMessageHandler();
			mockHttp
				.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/Delete"))
				.Respond((HttpStatusCode)200, new StringContent(jsonResponse, Encoding.UTF8, "application/json"));

			// Act
			var client = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			await Should.ThrowAsync<JsonReaderException>(async () =>
			{
				var result = await client.Campaigns.DeleteAsync(USER_KEY, campaignId, CLIENT_ID);
			});
		}

		[Fact]
		public async Task RestClient_Throws_exception_when_reponse_does_not_contain_expected_property()
		{
			// Arrange
			var categoryId = 123;
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"we_expected_a_property_named_id_but_instead_we_received_this_bogus_property\":\"{0}\"}}}}", categoryId);

			var mockHttp = new MockHttpMessageHandler();
			mockHttp
				.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/CreateCategory"))
				.Respond((HttpStatusCode)200, new StringContent(jsonResponse, Encoding.UTF8, "application/json"));

			// Act
			var client = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			await Should.ThrowAsync<CakeMailException>(async () =>
			{
				var result = await client.Templates.CreateCategoryAsync(USER_KEY, labels);
			});
		}
	}
}
