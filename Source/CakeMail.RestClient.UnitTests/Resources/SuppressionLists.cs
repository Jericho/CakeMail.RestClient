using RichardSzalay.MockHttp;
using Shouldly;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Resources
{
	public class SuppressionListsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[Fact]
		public async Task AddEmailAddressesToSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var emailAddresses = new[] { "aaa@aaa.com", "bbb@bbb.com", "ccc@ccc.com" };
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ImportEmails")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.AddEmailAddressesAsync(USER_KEY, emailAddresses);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task AddEmailAddressesToSuppressionList_with_null_array()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ImportEmails")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.AddEmailAddressesAsync(USER_KEY, null);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task AddEmailAddressesToSuppressionList_with_clientid()
		{
			// Arrange
			var emailAddresses = new[] { "aaa@aaa.com", "bbb@bbb.com", "ccc@ccc.com" };
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ImportEmails")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.AddEmailAddressesAsync(USER_KEY, emailAddresses, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task AddDomainsToSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var domains = new[] { "aaa.com", "bbb.com", "ccc.com" };
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"domain\":\"aaa.com\"},{\"domain\":\"bbb.com\"},{\"domain\":\"ccc.com\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ImportDomains")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.AddDomainsAsync(USER_KEY, domains);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task AddDomainsToSuppressionList_with_null_array()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":[]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ImportDomains")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.AddDomainsAsync(USER_KEY, null);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(0);
		}

		[Fact]
		public async Task AddDomainsToSuppressionList_with_clientid()
		{
			// Arrange
			var domains = new[] { "aaa.com", "bbb.com", "ccc.com" };
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"domain\":\"aaa.com\"},{\"domain\":\"bbb.com\"},{\"domain\":\"ccc.com\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ImportDomains")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.AddDomainsAsync(USER_KEY, domains, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task AddLocalPartsToSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var localParts = new[] { "administrator", "manager", "info" };
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"localpart\":\"administrator\"},{\"localpart\":\"manager\"},{\"localpart\":\"info\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ImportLocalparts")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.AddLocalPartsAsync(USER_KEY, localParts);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task AddLocalPartsToSuppressionList_with_null_array()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":[]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ImportLocalparts")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.AddLocalPartsAsync(USER_KEY, null);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(0);
		}

		[Fact]
		public async Task AddLocalPartsToSuppressionList_with_clientid()
		{
			// Arrange
			var localParts = new[] { "administrator", "manager", "info" };
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"localpart\":\"administrator\"},{\"localpart\":\"manager\"},{\"localpart\":\"info\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ImportLocalparts")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.AddLocalPartsAsync(USER_KEY, localParts, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task RemoveEmailAddressesFromSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var emailAddresses = new[] { "aaa@aaa.com", "bbb@bbb.com", "ccc@ccc.com" };
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/DeleteEmails")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.RemoveEmailAddressesAsync(USER_KEY, emailAddresses);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task RemoveEmailAddressesFromSuppressionList_with_null_array()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":[]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/DeleteEmails")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.RemoveEmailAddressesAsync(USER_KEY, null);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(0);
		}

		[Fact]
		public async Task RemoveEmailAddressesFromSuppressionList_with_clientid()
		{
			// Arrange
			var emailAddresses = new[] { "aaa@aaa.com", "bbb@bbb.com", "ccc@ccc.com" };
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/DeleteEmails")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.RemoveEmailAddressesAsync(USER_KEY, emailAddresses, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task RemoveDomainsFromSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var domains = new[] { "aaa.com", "bbb.com", "ccc.com" };
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"domain\":\"aaa.com\"},{\"domain\":\"bbb.com\"},{\"domain\":\"ccc.com\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/DeleteDomains")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.RemoveDomainsAsync(USER_KEY, domains);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task RemoveDomainsFromSuppressionList_with_null_array()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":[]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/DeleteDomains")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.RemoveDomainsAsync(USER_KEY, null);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(0);
		}

		[Fact]
		public async Task RemoveDomainsFromSuppressionList_with_clientid()
		{
			// Arrange
			var domains = new[] { "aaa.com", "bbb.com", "ccc.com" };
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"domain\":\"aaa.com\"},{\"domain\":\"bbb.com\"},{\"domain\":\"ccc.com\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/DeleteDomains")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.RemoveDomainsAsync(USER_KEY, domains, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task RemoveLocalPartsFromSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var localParts = new[] { "administrator", "manager", "info" };
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"localpart\":\"administrator\"},{\"localpart\":\"manager\"},{\"localpart\":\"info\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/DeleteLocalparts")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.RemoveLocalPartsAsync(USER_KEY, localParts);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task RemoveLocalPartsFromSuppressionList_with_null_array()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":[]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/DeleteLocalparts")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.RemoveLocalPartsAsync(USER_KEY, null);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(0);
		}

		[Fact]
		public async Task RemoveLocalPartsFromSuppressionList_with_clientid()
		{
			// Arrange
			var localParts = new[] { "administrator", "manager", "info" };
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"localpart\":\"administrator\"},{\"localpart\":\"manager\"},{\"localpart\":\"info\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/DeleteLocalparts")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.RemoveLocalPartsAsync(USER_KEY, localParts, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task GetSuppressedEmailAddresses_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"emails\":[{\"email\":\"aaa@aaa.com\",\"source_type\":\"manual\"},{\"email\":\"bbb@bbb.com\",\"source_type\":\"manual\"}]}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportEmails")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetEmailAddressesAsync(USER_KEY);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedEmailAddresses_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"emails\":[{\"email\":\"aaa@aaa.com\",\"source_type\":\"manual\"},{\"email\":\"bbb@bbb.com\",\"source_type\":\"manual\"}]}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportEmails")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetEmailAddressesAsync(USER_KEY, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedEmailAddresses_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"emails\":[{\"email\":\"aaa@aaa.com\",\"source_type\":\"manual\"},{\"email\":\"bbb@bbb.com\",\"source_type\":\"manual\"}]}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportEmails")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetEmailAddressesAsync(USER_KEY, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedEmailAddresses_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"emails\":[{\"email\":\"aaa@aaa.com\",\"source_type\":\"manual\"},{\"email\":\"bbb@bbb.com\",\"source_type\":\"manual\"}]}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportEmails")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetEmailAddressesAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedDomains_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"domains\":[{\"domain\":\"aaa.org\"},{\"domain\":\"bbb.com\"}]}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportDomains")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetDomainsAsync(USER_KEY);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedDomains_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"domains\":[{\"domain\":\"aaa.org\"},{\"domain\":\"bbb.com\"}]}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportDomains")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetDomainsAsync(USER_KEY, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedDomains_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"domains\":[{\"domain\":\"aaa.org\"},{\"domain\":\"bbb.com\"}]}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportDomains")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetDomainsAsync(USER_KEY, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedDomains_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"domains\":[{\"domain\":\"aaa.org\"},{\"domain\":\"bbb.com\"}]}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportDomains")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetDomainsAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedLocalParts_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"info\"}]}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportLocalparts")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetLocalPartsAsync(USER_KEY);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedLocalParts_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"info\"}]}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportLocalparts")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetLocalPartsAsync(USER_KEY, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedLocalParts_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"info\"}]}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportLocalparts")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetLocalPartsAsync(USER_KEY, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedLocalParts_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"info\"}]}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportLocalparts")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetLocalPartsAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedEmailAddressesCount_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportEmails")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetEmailAddressesCountAsync(USER_KEY);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedEmailAddressesCount_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportEmails")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetEmailAddressesCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedDomainsCount_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportDomains")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetDomainsCountAsync(USER_KEY);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedDomainsCount_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportDomains")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetDomainsCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedLocalPartsCount_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportLocalparts")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetLocalPartsCountAsync(USER_KEY);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetSuppressedLocalPartsCount_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("SuppressionList/ExportLocalparts")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.SuppressionLists.GetLocalPartsCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}
	}
}
