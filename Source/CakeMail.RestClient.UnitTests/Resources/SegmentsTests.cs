using RichardSzalay.MockHttp;
using Shouldly;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Resources
{
	public class SegmentsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[Fact]
		public async Task CreateSegment_with_query()
		{
			// Arrange
			var name = "My Segment";
			var query = "???";
			var listId = 123L;
			var segmentId = 456L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", segmentId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/CreateSublist")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.CreateAsync(USER_KEY, listId, name, query, null);

			// Assert
			result.ShouldBe(segmentId);
		}

		[Fact]
		public async Task CreateSegment_with_clientid()
		{
			// Arrange
			var name = "My Segment";
			var listId = 123L;
			var segmentId = 456L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", segmentId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/CreateSublist")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.CreateAsync(USER_KEY, listId, name, null, CLIENT_ID);

			// Assert
			result.ShouldBe(segmentId);
		}

		[Fact]
		public async Task UpdateSegment_name()
		{
			// Arrange
			var listId = 123L;
			var segmentId = 456L;
			var name = "My list";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.UpdateAsync(USER_KEY, segmentId, listId, name: name);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateSegment_query()
		{
			// Arrange
			var listId = 123L;
			var segmentId = 456L;
			var query = "???";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.UpdateAsync(USER_KEY, segmentId, listId, query: query);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateSegment_clientid()
		{
			// Arrange
			var listId = 123L;
			var segmentId = 456L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.UpdateAsync(USER_KEY, segmentId, listId, clientId: CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteSegment_wit_minimal_parameters()
		{
			// Arrange
			var segmentId = 456L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/DeleteSublist")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.DeleteAsync(USER_KEY, segmentId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteSegment_with_clientid()
		{
			// Arrange
			var segmentId = 456L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/DeleteSublist")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.DeleteAsync(USER_KEY, segmentId, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetSegment_with_minimal_parameters()
		{
			// Arrange
			var segmentId = 456L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"list_id\":\"111\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}}}", segmentId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.GetAsync(USER_KEY, segmentId);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(segmentId);
		}

		[Fact]
		public async Task GetSegment_with_includestatistics_true()
		{
			// Arrange
			var segmentId = 456L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"list_id\":\"111\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}}}", segmentId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.GetAsync(USER_KEY, segmentId, includeStatistics: true);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(segmentId);
		}

		[Fact]
		public async Task GetSegment_with_includestatistics_false()
		{
			// Arrange
			var segmentId = 456L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"list_id\":\"111\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}}}", segmentId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.GetAsync(USER_KEY, segmentId, includeStatistics: false);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(segmentId);
		}

		[Fact]
		public async Task GetSegment_with_calculateengagement_true()
		{
			// Arrange
			var segmentId = 456L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"list_id\":\"111\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}}}", segmentId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.GetAsync(USER_KEY, segmentId, calculateEngagement: true);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(segmentId);
		}

		[Fact]
		public async Task GetSegment_with_calculateengagement_false()
		{
			// Arrange
			var segmentId = 456L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"list_id\":\"111\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}}}", segmentId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.GetAsync(USER_KEY, segmentId, calculateEngagement: false);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(segmentId);
		}

		[Fact]
		public async Task GetSegment_with_clientid()
		{
			// Arrange
			var segmentId = 456L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"list_id\":\"111\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}}}", segmentId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.GetAsync(USER_KEY, segmentId, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(segmentId);
		}

		[Fact]
		public async Task GetSegments_with_details_false()
		{
			// Arrange
			var listId = 123L;
			var jsonSegment1 = string.Format("{{\"id\":\"123\",\"list_id\":\"{0}\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var jsonSegment2 = string.Format("{{\"id\":\"456\",\"list_id\":\"{0}\",\"name\":\"Second Segment\",\"query\":\"(`email` LIKE \\\"%bb%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sublists\":[{0},{1}]}}}}", jsonSegment1, jsonSegment2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetSublists")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.GetSegmentsAsync(USER_KEY, listId, includeDetails: false);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSegments_with_limit()
		{
			// Arrange
			var listId = 123L;
			var limit = 5;
			var jsonSegment1 = string.Format("{{\"id\":\"123\",\"list_id\":\"{0}\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var jsonSegment2 = string.Format("{{\"id\":\"456\",\"list_id\":\"{0}\",\"name\":\"Second Segment\",\"query\":\"(`email` LIKE \\\"%bb%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sublists\":[{0},{1}]}}}}", jsonSegment1, jsonSegment2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetSublists")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.GetSegmentsAsync(USER_KEY, listId, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSegments_with_offset()
		{
			// Arrange
			var listId = 123L;
			var offset = 5;
			var jsonSegment1 = string.Format("{{\"id\":\"123\",\"list_id\":\"{0}\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var jsonSegment2 = string.Format("{{\"id\":\"456\",\"list_id\":\"{0}\",\"name\":\"Second Segment\",\"query\":\"(`email` LIKE \\\"%bb%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sublists\":[{0},{1}]}}}}", jsonSegment1, jsonSegment2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetSublists")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.GetSegmentsAsync(USER_KEY, listId, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSegments_with_clientid()
		{
			// Arrange
			var listId = 123L;
			var jsonSegment1 = string.Format("{{\"id\":\"123\",\"list_id\":\"{0}\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var jsonSegment2 = string.Format("{{\"id\":\"456\",\"list_id\":\"{0}\",\"name\":\"Second Segment\",\"query\":\"(`email` LIKE \\\"%bb%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sublists\":[{0},{1}]}}}}", jsonSegment1, jsonSegment2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetSublists")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.GetSegmentsAsync(USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetSegmentsCount_with_minimal_parameters()
		{
			// Arrange
			var listId = 123L;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.GetCountAsync(USER_KEY, listId);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetSegmentsCount_with_clientid()
		{
			// Arrange
			var listId = 123L;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Segments.GetCountAsync(USER_KEY, listId, CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}
	}
}
