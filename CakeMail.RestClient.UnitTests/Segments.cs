using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class SegmentsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[TestMethod]
		public async Task CreateSegment_with_query()
		{
			// Arrange
			var name = "My Segment";
			var query = "???";
			var listId = 123L;
			var segmentId = 456L;
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "sublist_name", Value = name },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "query", Value = query }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", segmentId);
			var mockRestClient = new MockRestClient("/List/CreateSublist/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.CreateAsync(USER_KEY, listId, name, query, null);

			// Assert
			Assert.AreEqual(segmentId, result);
		}

		[TestMethod]
		public async Task CreateSegment_with_clientid()
		{
			// Arrange
			var name = "My Segment";
			var listId = 123L;
			var segmentId = 456L;
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "sublist_name", Value = name },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", segmentId);
			var mockRestClient = new MockRestClient("/List/CreateSublist/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.CreateAsync(USER_KEY, listId, name, null, CLIENT_ID);

			// Assert
			Assert.AreEqual(segmentId, result);
		}

		[TestMethod]
		public async Task UpdateSegment_name()
		{
			// Arrange
			var listId = 123L;
			var segmentId = 456L;
			var name = "My list";
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "sublist_id", Value = segmentId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "name", Value = name }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.UpdateAsync(USER_KEY, segmentId, listId, name: name);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateSegment_query()
		{
			// Arrange
			var listId = 123L;
			var segmentId = 456L;
			var query = "???";
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "sublist_id", Value = segmentId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "query", Value = query }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.UpdateAsync(USER_KEY, segmentId, listId, query: query);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateSegment_clientid()
		{
			// Arrange
			var listId = 123L;
			var segmentId = 456L;
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "sublist_id", Value = segmentId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.UpdateAsync(USER_KEY, segmentId, listId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteSegment_wit_minimal_parameters()
		{
			// Arrange
			var segmentId = 456L;
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "sublist_id", Value = segmentId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/DeleteSublist/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.DeleteAsync(USER_KEY, segmentId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteSegment_with_clientid()
		{
			// Arrange
			var segmentId = 456L;
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "sublist_id", Value = segmentId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/DeleteSublist/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.DeleteAsync(USER_KEY, segmentId, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetSegment_with_minimal_parameters()
		{
			// Arrange
			var segmentId = 456L;
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "sublist_id", Value = segmentId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "no_details", Value = "false" },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "with_engagement", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"list_id\":\"111\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}}}", segmentId);
			var mockRestClient = new MockRestClient("/List/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.GetAsync(USER_KEY, segmentId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(segmentId, result.Id);
		}

		[TestMethod]
		public async Task GetSegment_with_includestatistics_true()
		{
			// Arrange
			var segmentId = 456L;
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "sublist_id", Value = segmentId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "no_details", Value = "false" },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "with_engagement", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"list_id\":\"111\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}}}", segmentId);
			var mockRestClient = new MockRestClient("/List/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.GetAsync(USER_KEY, segmentId, includeStatistics: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(segmentId, result.Id);
		}

		[TestMethod]
		public async Task GetSegment_with_includestatistics_false()
		{
			// Arrange
			var segmentId = 456L;
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "sublist_id", Value = segmentId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "no_details", Value = "true" },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "with_engagement", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"list_id\":\"111\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}}}", segmentId);
			var mockRestClient = new MockRestClient("/List/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.GetAsync(USER_KEY, segmentId, includeStatistics: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(segmentId, result.Id);
		}

		[TestMethod]
		public async Task GetSegment_with_calculateengagement_true()
		{
			// Arrange
			var segmentId = 456L;
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "sublist_id", Value = segmentId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "no_details", Value = "false" },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "with_engagement", Value = "true" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"list_id\":\"111\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}}}", segmentId);
			var mockRestClient = new MockRestClient("/List/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.GetAsync(USER_KEY, segmentId, calculateEngagement: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(segmentId, result.Id);
		}

		[TestMethod]
		public async Task GetSegment_with_calculateengagement_false()
		{
			// Arrange
			var segmentId = 456L;
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "sublist_id", Value = segmentId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "no_details", Value = "false" },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "with_engagement", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"list_id\":\"111\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}}}", segmentId);
			var mockRestClient = new MockRestClient("/List/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.GetAsync(USER_KEY, segmentId, calculateEngagement: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(segmentId, result.Id);
		}

		[TestMethod]
		public async Task GetSegment_with_clientid()
		{
			// Arrange
			var segmentId = 456L;
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "sublist_id", Value = segmentId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "no_details", Value = "false" },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "with_engagement", Value = "false" },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"list_id\":\"111\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}}}", segmentId);
			var mockRestClient = new MockRestClient("/List/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.GetAsync(USER_KEY, segmentId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(segmentId, result.Id);
		}

		[TestMethod]
		public async Task GetSegments_with_details_false()
		{
			// Arrange
			var listId = 123L;
			var jsonSegment1 = string.Format("{{\"id\":\"123\",\"list_id\":\"{0}\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var jsonSegment2 = string.Format("{{\"id\":\"456\",\"list_id\":\"{0}\",\"name\":\"Second Segment\",\"query\":\"(`email` LIKE \\\"%bb%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "no_details", Value = "true" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sublists\":[{0},{1}]}}}}", jsonSegment1, jsonSegment2);
			var mockRestClient = new MockRestClient("/List/GetSublists/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.GetSegmentsAsync(USER_KEY, listId, includeDetails: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetSegments_with_limit()
		{
			// Arrange
			var listId = 123L;
			var limit = 5;
			var jsonSegment1 = string.Format("{{\"id\":\"123\",\"list_id\":\"{0}\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var jsonSegment2 = string.Format("{{\"id\":\"456\",\"list_id\":\"{0}\",\"name\":\"Second Segment\",\"query\":\"(`email` LIKE \\\"%bb%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "no_details", Value = "false" },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "limit", Value = limit }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sublists\":[{0},{1}]}}}}", jsonSegment1, jsonSegment2);
			var mockRestClient = new MockRestClient("/List/GetSublists/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.GetSegmentsAsync(USER_KEY, listId, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetSegments_with_offset()
		{
			// Arrange
			var listId = 123L;
			var offset = 5;
			var jsonSegment1 = string.Format("{{\"id\":\"123\",\"list_id\":\"{0}\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var jsonSegment2 = string.Format("{{\"id\":\"456\",\"list_id\":\"{0}\",\"name\":\"Second Segment\",\"query\":\"(`email` LIKE \\\"%bb%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "no_details", Value = "false" },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "offset", Value = offset }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sublists\":[{0},{1}]}}}}", jsonSegment1, jsonSegment2);
			var mockRestClient = new MockRestClient("/List/GetSublists/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.GetSegmentsAsync(USER_KEY, listId, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetSegments_with_clientid()
		{
			// Arrange
			var listId = 123L;
			var jsonSegment1 = string.Format("{{\"id\":\"123\",\"list_id\":\"{0}\",\"name\":\"First Segment\",\"query\":\"(`email` LIKE \\\"%aa%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var jsonSegment2 = string.Format("{{\"id\":\"456\",\"list_id\":\"{0}\",\"name\":\"Second Segment\",\"query\":\"(`email` LIKE \\\"%bb%\\\")\",\"mailings_count\":\"0\",\"last_used\":\"0000-00-00 00:00:00\",\"created_on\":\"2015-03-27 21:15:19\",\"engagement\":null,\"count\":\"0\"}}", listId);
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "no_details", Value = "false" },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sublists\":[{0},{1}]}}}}", jsonSegment1, jsonSegment2);
			var mockRestClient = new MockRestClient("/List/GetSublists/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.GetSegmentsAsync(USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetSegmentsCount_with_minimal_parameters()
		{
			// Arrange
			var listId = 123L;
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.GetCountAsync(USER_KEY, listId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetSegmentsCount_with_clientid()
		{
			// Arrange
			var listId = 123L;
			var parameters = new[]
			{
				new Parameter {  Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter {  Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Segments.GetCountAsync(USER_KEY, listId, CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}
	}
}
