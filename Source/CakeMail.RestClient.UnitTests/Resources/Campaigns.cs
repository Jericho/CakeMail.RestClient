using CakeMail.RestClient.Models;
using RichardSzalay.MockHttp;
using Shouldly;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Resources
{
	public class CampaignsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[Fact]
		public async Task CreateCampaign_with_minimal_parameters()
		{
			// Arrange
			var campaignName = "My Campaign";
			var campaignId = 12345L;
			var jsonResponse = $"{{\"status\":\"success\",\"data\":\"{campaignId}\"}}";

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.CreateAsync(USER_KEY, campaignName).ConfigureAwait(false);

			// Assert
			result.ShouldBe(campaignId);
		}

		[Fact]
		public async Task CreateCampaign_with_clientid()
		{
			// Arrange
			var campaignName = "My Campaign";
			var campaignId = 12345L;
			var jsonResponse = $"{{\"status\":\"success\",\"data\":\"{campaignId}\"}}";

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.CreateAsync(USER_KEY, campaignName).ConfigureAwait(false);

			// Assert
			result.ShouldBe(campaignId);
		}

		[Fact]
		public async Task DeleteCampaign_with_minimal_parameters()
		{
			// Arrange
			var campaignId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/Delete")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.DeleteAsync(USER_KEY, campaignId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteCampaign_with_clientid()
		{
			// Arrange
			var campaignId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/Delete")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.DeleteAsync(USER_KEY, campaignId, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetCampaign_with_minimal_parameters()
		{
			// Arrange
			var campaignId = 12345L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"client_id\":\"{1}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-22 04:38:46\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}}}", campaignId, CLIENT_ID);

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.GetAsync(USER_KEY, campaignId, null);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(campaignId);
		}

		[Fact]
		public async Task GetCampaign_with_clientid()
		{
			// Arrange
			var campaignId = 12345L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"client_id\":\"{1}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-22 04:38:46\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}}}", campaignId, CLIENT_ID);

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.GetAsync(USER_KEY, campaignId, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(campaignId);
		}

		[Fact]
		public async Task GetCampaigns_with_minimal_parameters()
		{
			// Arrange
			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2);

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetCampaign_with_status()
		{
			// Arrange
			var status = CampaignStatus.Ongoing;
			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2);

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY, status: status);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetCampaign_with_name()
		{
			// Arrange
			var name = "Dummy campaign";
			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2);

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY, name: name);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetCampaign_with_sortby()
		{
			// Arrange
			var sortBy = CampaignsSortBy.Name;
			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2);

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY, sortBy: sortBy);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetCampaign_with_sortdirection()
		{
			// Arrange
			var sortDirection = Models.SortDirection.Ascending;
			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2);

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY, sortDirection: sortDirection);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetCampaign_with_limit()
		{
			// Arrange
			var limit = 50;
			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2);

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetCampaign_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2);

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetCampaigns_with_clientid()
		{
			// Arrange
			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2);

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetCampaignCount_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.GetCountAsync(USER_KEY);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetCampaignCount_with_status()
		{
			// Arrange
			var status = CampaignStatus.Ongoing;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.GetCountAsync(USER_KEY, status: status);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetCampaignCount_with_name()
		{
			// Arrange
			var name = "Dummy campaign";
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.GetCountAsync(USER_KEY, name: name);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetCampaignCount_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.GetCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task UpdateCampaign_with_minimal_parameters()
		{
			// Arrange
			var campaignId = 123L;
			var name = "New name";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.UpdateAsync(USER_KEY, campaignId, name);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateCampaign_with_clientid()
		{
			// Arrange
			var campaignId = 123L;
			var name = "New name";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Campaign/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Campaigns.UpdateAsync(USER_KEY, campaignId, name, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}
	}
}
