using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class CampaignsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[TestMethod]
		public async Task CreateCampaign_with_minimal_parameters()
		{
			// Arrange
			var campaignName = "My Campaign";
			var campaignId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == campaignName && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", campaignId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.CreateAsync(USER_KEY, campaignName);

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public async Task CreateCampaign_with_clientid()
		{
			// Arrange
			var campaignName = "My Campaign";
			var campaignId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == campaignName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", campaignId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.CreateAsync(USER_KEY, campaignName, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public async Task DeleteCampaign_with_minimal_parameters()
		{
			// Arrange
			var campaignId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/Delete/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (long)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.DeleteAsync(USER_KEY, campaignId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteCampaign_with_clientid()
		{
			// Arrange
			var campaignId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/Delete/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (long)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.DeleteAsync(USER_KEY, campaignId, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetCampaign_with_minimal_parameters()
		{
			// Arrange
			var campaignId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (long)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"client_id\":\"{1}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-22 04:38:46\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}}}", campaignId, CLIENT_ID)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.GetAsync(USER_KEY, campaignId, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(campaignId, result.Id);
		}

		[TestMethod]
		public async Task GetCampaign_with_clientid()
		{
			// Arrange
			var campaignId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (long)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"client_id\":\"{1}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-22 04:38:46\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}}}", campaignId, CLIENT_ID)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.GetAsync(USER_KEY, campaignId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(campaignId, result.Id);
		}

		[TestMethod]
		public async Task GetCampaigns_with_minimal_parameters()
		{
			// Arrange
			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetCampaign_with_status()
		{
			// Arrange
			var status = CampaignStatus.Ongoing;

			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetCampaign_with_name()
		{
			// Arrange
			var name = "Dummy campaign";

			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY, name: name);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetCampaign_with_sortby()
		{
			// Arrange
			var sortBy = CampaignsSortBy.Name;

			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sort_by" && (string)p.Value == sortBy.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY, sortBy: sortBy);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetCampaign_with_sortdirection()
		{
			// Arrange
			var sortDirection = SortDirection.Ascending;

			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "direction" && (string)p.Value == sortDirection.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY, sortDirection: sortDirection);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetCampaign_with_limit()
		{
			// Arrange
			var limit = 50;

			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetCampaign_with_offset()
		{
			// Arrange
			var offset = 25;

			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetCampaigns_with_clientid()
		{
			// Arrange
			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.GetListAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetCampaignCount_with_minimal_parameters()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.GetCountAsync(USER_KEY);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetCampaignCount_with_status()
		{
			// Arrange
			var status = CampaignStatus.Ongoing;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.GetCountAsync(USER_KEY, status: status);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetCampaignCount_with_name()
		{
			// Arrange
			var name = "Dummy campaign";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.GetCountAsync(USER_KEY, name: name);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetCampaignCount_with_clientid()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.GetCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task UpdateCampaign_with_minimal_parameters()
		{
			// Arrange
			var campaignId = 123;
			var name = "New name";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (long)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.UpdateAsync(USER_KEY, campaignId, name);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateCampaign_with_clientid()
		{
			// Arrange
			var campaignId = 123;
			var name = "New name";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (long)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Campaigns.UpdateAsync(USER_KEY, campaignId, name, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}
	}
}
