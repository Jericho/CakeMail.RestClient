using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class CampaignsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const int CLIENT_ID = 999;

		[TestMethod]
		public void CreateCampaign_with_all_parameters()
		{
			// Arrange
			var campaignName = "My Campaign";
			var campaignId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == campaignName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", campaignId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateCampaign(USER_KEY, campaignName, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void CreateCampaign_without_clientid()
		{
			// Arrange
			var campaignName = "My Campaign";
			var campaignId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == campaignName && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", campaignId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateCampaign(USER_KEY, campaignName, null);

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void DeleteCampaign_with_all_parameters()
		{
			// Arrange
			var campaignId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/Delete/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (int)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteCampaign(USER_KEY, campaignId, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void DeleteCampaign_without_clientid()
		{
			// Arrange
			var campaignId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/Delete/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (int)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteCampaign(USER_KEY, campaignId, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetCampaign_with_all_parameters()
		{
			// Arrange
			var campaignId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (int)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"client_id\":\"{1}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-22 04:38:46\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}}}", campaignId, CLIENT_ID)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCampaign(USER_KEY, campaignId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(campaignId, result.Id);
		}

		[TestMethod]
		public void GetCampaign_without_clientid()
		{
			// Arrange
			var campaignId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (int)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"client_id\":\"{1}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-22 04:38:46\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}}}", campaignId, CLIENT_ID)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCampaign(USER_KEY, campaignId, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(campaignId, result.Id);
		}

		[TestMethod]
		public void GetCampaigns_with_all_parameters()
		{
			// Arrange
			var status = "ongoing";
			var name = "Dummy campaign";
			var sortBy = "asc";
			var sortDirection = "name";
			var limit = 50;
			var offset = 25;

			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 9 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sort_by" && (string)p.Value == sortBy && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "direction" && (string)p.Value == sortDirection && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCampaigns(USER_KEY, status, name, sortBy, sortDirection, limit, offset, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetCampaigns_without_clientid()
		{
			// Arrange
			var status = "ongoing";
			var name = "Dummy campaign";
			var sortBy = "asc";
			var sortDirection = "name";
			var limit = 50;
			var offset = 25;

			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 8 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sort_by" && (string)p.Value == sortBy && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "direction" && (string)p.Value == sortDirection && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCampaigns(USER_KEY, status, name, sortBy, sortDirection, limit, offset, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetCampaigns_without_offset()
		{
			// Arrange
			var status = "ongoing";
			var name = "Dummy campaign";
			var sortBy = "asc";
			var sortDirection = "name";
			var limit = 50;
			var offset = 0;

			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 8 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sort_by" && (string)p.Value == sortBy && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "direction" && (string)p.Value == sortDirection && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCampaigns(USER_KEY, status, name, sortBy, sortDirection, limit, offset, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetCampaigns_without_limit()
		{
			// Arrange
			var status = "ongoing";
			var name = "Dummy campaign";
			var sortBy = "asc";
			var sortDirection = "name";
			var limit = 0;
			var offset = 25;

			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 8 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sort_by" && (string)p.Value == sortBy && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "direction" && (string)p.Value == sortDirection && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCampaigns(USER_KEY, status, name, sortBy, sortDirection, limit, offset, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetCampaigns_without_sortdirecton()
		{
			// Arrange
			var status = "ongoing";
			var name = "Dummy campaign";
			var sortBy = "asc";
			var sortDirection = (string)null;
			var limit = 50;
			var offset = 25;

			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 8 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sort_by" && (string)p.Value == sortBy && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCampaigns(USER_KEY, status, name, sortBy, sortDirection, limit, offset, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetCampaigns_without_sortby()
		{
			// Arrange
			var status = "ongoing";
			var name = "Dummy campaign";
			var sortBy = (string)null;
			var sortDirection = "name";
			var limit = 50;
			var offset = 25;

			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 8 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "direction" && (string)p.Value == sortDirection && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCampaigns(USER_KEY, status, name, sortBy, sortDirection, limit, offset, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetCampaigns_without_name()
		{
			// Arrange
			var status = "ongoing";
			var name = (string)null;
			var sortBy = "asc";
			var sortDirection = "name";
			var limit = 50;
			var offset = 25;

			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 8 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sort_by" && (string)p.Value == sortBy && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "direction" && (string)p.Value == sortDirection && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCampaigns(USER_KEY, status, name, sortBy, sortDirection, limit, offset, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetCampaigns_without_status()
		{
			// Arrange
			var status = (string)null;
			var name = "Dummy campaign";
			var sortBy = "asc";
			var sortDirection = "name";
			var limit = 50;
			var offset = 25;

			var jsonCampaign1 = string.Format("{{\"id\":\"123\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);
			var jsonCampaign2 = string.Format("{{\"id\":\"456\",\"client_id\":\"{0}\",\"name\":\"Dummy campaign\",\"status\":\"ongoing\",\"created_on\":\"2015-03-23 13:29:45\",\"closed_on\":\"0000-00-00 00:00:00\",\"sent\":\"0\",\"open_pct\":\"0.0000\",\"click_pct\":\"0.0000\",\"bounce_pct\":\"0.0000\",\"unsubscribes_pct\":\"0.0000\",\"fbl_pct\":\"0.0000\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 8 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sort_by" && (string)p.Value == sortBy && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "direction" && (string)p.Value == sortDirection && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"campaigns\":[{0},{1}]}}}}", jsonCampaign1, jsonCampaign2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCampaigns(USER_KEY, status, name, sortBy, sortDirection, limit, offset, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetCampaignsCount_with_all_parameters()
		{
			// Arrange
			var status = "ongoing";
			var name = "Dummy campaign";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCampaignsCount(USER_KEY, status, name, CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetCampaignsCount_without_clientid()
		{
			// Arrange
			var status = "ongoing";
			var name = "Dummy campaign";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCampaignsCount(USER_KEY, status, name, null);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetCampaignsCount_without_name()
		{
			// Arrange
			var status = "ongoing";
			var name = (string)null;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCampaignsCount(USER_KEY, status, name, CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetCampaignsCount_without_status()
		{
			// Arrange
			var status = (string)null;
			var name = "Dummy campaign";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCampaignsCount(USER_KEY, status, name, CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void UpdateCampaign_with_all_parameters()
		{
			// Arrange
			var campaignId = 123;
			var name = "New name";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (int)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateCampaign(USER_KEY, campaignId, name, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateCampaign_without_clientid()
		{
			// Arrange
			var campaignId = 123;
			var name = "New name";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (int)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateCampaign(USER_KEY, campaignId, name, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateCampaign_without_name()
		{
			// Arrange
			var campaignId = 123;
			var name = (string)null;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Campaign/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (int)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateCampaign(USER_KEY, campaignId, name, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}
	}
}
