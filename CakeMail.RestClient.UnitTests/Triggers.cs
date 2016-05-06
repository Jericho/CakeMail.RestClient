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
	public class TriggersTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[TestMethod]
		public async Task CreateTrigger_with_minimal_parameters()
		{
			// Arrange
			var name = "My trigger";
			var triggerId = 123;
			var listId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", triggerId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.CreateAsync(USER_KEY, name, listId);

			// Assert
			Assert.AreEqual(triggerId, result);
		}

		[TestMethod]
		public async Task CreateTrigger_with_campaignid()
		{
			// Arrange
			var name = "My trigger";
			var triggerId = 123;
			var listId = 111;
			var campaignId = 222;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (long)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", triggerId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.CreateAsync(USER_KEY, name, listId, campaignId: campaignId);

			// Assert
			Assert.AreEqual(triggerId, result);
		}

		[TestMethod]
		public async Task CreateTrigger_with_encoding()
		{
			// Arrange
			var name = "My trigger";
			var triggerId = 123;
			var listId = 111;
			var encoding = MessageEncoding.Utf8;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "encoding" && (string)p.Value == encoding.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", triggerId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.CreateAsync(USER_KEY, name, listId, encoding: encoding);

			// Assert
			Assert.AreEqual(triggerId, result);
		}

		[TestMethod]
		public async Task CreateTrigger_with_transferencoding()
		{
			// Arrange
			var name = "My trigger";
			var triggerId = 123;
			var listId = 111;
			var transferEncoding = TransferEncoding.Base64;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "transfer_encoding" && (string)p.Value == transferEncoding.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", triggerId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.CreateAsync(USER_KEY, name, listId, transferEncoding: transferEncoding);

			// Assert
			Assert.AreEqual(triggerId, result);
		}

		[TestMethod]
		public async Task CreateTrigger_with_clientid()
		{
			// Arrange
			var name = "My trigger";
			var triggerId = 123;
			var listId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", triggerId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.CreateAsync(USER_KEY, name, listId, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(triggerId, result);
		}

		[TestMethod]
		public async Task GetTrigger_with_minimal_parameters()
		{
			// Arrange
			var triggerId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"action\":\"opt-in\",\"campaign_id\":\"0\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"{0}\",\"list_id\":\"111222333\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/8da92a42e5625ef43112c0ca4237902219935a88319a1349\",\"name\":\"this_is_a_test_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}}}}", triggerId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetAsync(USER_KEY, triggerId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(triggerId, result.Id);
		}

		[TestMethod]
		public async Task GetTrigger_with_clientid()
		{
			// Arrange
			var triggerId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"action\":\"opt-in\",\"campaign_id\":\"0\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"{0}\",\"list_id\":\"111222333\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/8da92a42e5625ef43112c0ca4237902219935a88319a1349\",\"name\":\"this_is_a_test_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}}}}", triggerId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetAsync(USER_KEY, triggerId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(triggerId, result.Id);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_minimal_parameters()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_campaignid()
		{
			// Arrange
			var triggerId = 123;
			var campaignId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (long)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, campaignId: campaignId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_name()
		{
			// Arrange
			var triggerId = 123;
			var name = "My mailing";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, name: name);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_action()
		{
			// Arrange
			var triggerId = 123;
			var action = TriggerAction.OptIn;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == action.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, action: action);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_encoding()
		{
			// Arrange
			var triggerId = 123;
			var encoding = MessageEncoding.Utf8;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "encoding" && (string)p.Value == encoding.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, encoding: encoding);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_transferencoding()
		{
			// Arrange
			var triggerId = 123;
			var transferEncoding = TransferEncoding.Base64;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "transfer_encoding" && (string)p.Value == transferEncoding.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, transferEncoding: transferEncoding);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_subject()
		{
			// Arrange
			var triggerId = 123;
			var subject = "My mailing subject";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "subject" && (string)p.Value == subject && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, subject: subject);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_senderemail()
		{
			// Arrange
			var triggerId = 123;
			var senderEmail = "bobsmith@fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_email" && (string)p.Value == senderEmail && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, senderEmail: senderEmail);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_sendername()
		{
			// Arrange
			var triggerId = 123;
			var senderName = "Bob Smith";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_name" && (string)p.Value == senderName && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, senderName: senderName);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_replyto()
		{
			// Arrange
			var triggerId = 123;
			var replyTo = "marketing@fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "reply_to" && (string)p.Value == replyTo && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, replyTo: replyTo);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_htmlcontent()
		{
			// Arrange
			var triggerId = 123;
			var htmlContent = "<html><body>Hello world</body></html>";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "html_message" && (string)p.Value == htmlContent && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, htmlContent: htmlContent);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_textcontent()
		{
			// Arrange
			var triggerId = 123;
			var textContent = "Hello world";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "text_message" && (string)p.Value == textContent && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, textContent: textContent);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_trackopens_true()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "opening_stats" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, trackOpens: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_trackopens_false()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "opening_stats" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, trackOpens: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_trackclicksinhtml_true()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "clickthru_html" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, trackClicksInHtml: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_trackclicksinhtml_false()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "clickthru_html" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, trackClicksInHtml: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_trackclicksintext_true()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "clickthru_text" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, trackClicksInText: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_trackclicksintext_false()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "clickthru_text" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, trackClicksInText: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_trackingparameters()
		{
			// Arrange
			var triggerId = 123;
			var trackingParameters = "param1=abc&param2=123";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "tracking_params" && (string)p.Value == trackingParameters && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, trackingParameters: trackingParameters);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_delay()
		{
			// Arrange
			var triggerId = 123;
			var delay = 60;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "delay" && (int)p.Value == delay && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, delay: delay);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_status()
		{
			// Arrange
			var triggerId = 123;
			var status = TriggerStatus.Active;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, status: status);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_datefield()
		{
			// Arrange
			var triggerId = 123;
			var date = new DateTime(2015, 12, 1, 13, 6, 6);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "date_field" && (string)p.Value == date.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, date: date);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTrigger_with_clientid()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UpdateAsync(USER_KEY, triggerId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetTriggers_with_minimal_parameters()
		{
			// Arrange
			var jsonTrigger1 = "{\"action\":\"opt-in\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"123\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b99469dc805938e947457d91aa10c4c551\",\"name\":\"list_111_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}";
			var jsonTrigger2 = "{\"action\":\"opt-out\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"456\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b9c3ef15863200867f457d91aa10c4c551\",\"name\":\"list_111_opt_out\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Unsubscribe Confirmed.\",\"transfer_encoding\":\"quoted-printable\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetTriggersAsync(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTriggers_with_status()
		{
			// Arrange
			var status = TriggerStatus.Active;
			var jsonTrigger1 = "{\"action\":\"opt-in\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"123\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b99469dc805938e947457d91aa10c4c551\",\"name\":\"list_111_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}";
			var jsonTrigger2 = "{\"action\":\"opt-out\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"456\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b9c3ef15863200867f457d91aa10c4c551\",\"name\":\"list_111_opt_out\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Unsubscribe Confirmed.\",\"transfer_encoding\":\"quoted-printable\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetList/" &&
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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetTriggersAsync(USER_KEY, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTriggers_with_action()
		{
			// Arrange
			var action = TriggerAction.OptIn;
			var jsonTrigger1 = "{\"action\":\"opt-in\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"123\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b99469dc805938e947457d91aa10c4c551\",\"name\":\"list_111_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}";
			var jsonTrigger2 = "{\"action\":\"opt-in\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"456\",\"list_id\":\"222\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b9c3ef15863200867f457d91aa10c4c551\",\"name\":\"list_222_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == action.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetTriggersAsync(USER_KEY, action: action);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTriggers_with_listid()
		{
			// Arrange
			var listId = 111;
			var jsonTrigger1 = "{\"action\":\"opt-in\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"123\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b99469dc805938e947457d91aa10c4c551\",\"name\":\"list_111_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}";
			var jsonTrigger2 = "{\"action\":\"opt-out\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"456\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b9c3ef15863200867f457d91aa10c4c551\",\"name\":\"list_111_opt_out\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Unsubscribe Confirmed.\",\"transfer_encoding\":\"quoted-printable\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetTriggersAsync(USER_KEY, listId: listId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTriggers_with_campaignid()
		{
			// Arrange
			var campaignId = 111;
			var jsonTrigger1 = "{\"action\":\"opt-in\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"123\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b99469dc805938e947457d91aa10c4c551\",\"name\":\"list_111_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}";
			var jsonTrigger2 = "{\"action\":\"opt-out\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"456\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b9c3ef15863200867f457d91aa10c4c551\",\"name\":\"list_111_opt_out\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Unsubscribe Confirmed.\",\"transfer_encoding\":\"quoted-printable\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (long)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetTriggersAsync(USER_KEY, campaignId: campaignId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTriggers_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonTrigger1 = "{\"action\":\"opt-in\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"123\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b99469dc805938e947457d91aa10c4c551\",\"name\":\"list_111_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}";
			var jsonTrigger2 = "{\"action\":\"opt-out\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"456\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b9c3ef15863200867f457d91aa10c4c551\",\"name\":\"list_111_opt_out\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Unsubscribe Confirmed.\",\"transfer_encoding\":\"quoted-printable\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetList/" &&
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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetTriggersAsync(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTriggers_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonTrigger1 = "{\"action\":\"opt-in\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"123\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b99469dc805938e947457d91aa10c4c551\",\"name\":\"list_111_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}";
			var jsonTrigger2 = "{\"action\":\"opt-out\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"456\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b9c3ef15863200867f457d91aa10c4c551\",\"name\":\"list_111_opt_out\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Unsubscribe Confirmed.\",\"transfer_encoding\":\"quoted-printable\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetList/" &&
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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetTriggersAsync(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTriggers_with_clientid()
		{
			// Arrange
			var jsonTrigger1 = "{\"action\":\"opt-in\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"123\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b99469dc805938e947457d91aa10c4c551\",\"name\":\"list_111_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}";
			var jsonTrigger2 = "{\"action\":\"opt-out\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"456\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b9c3ef15863200867f457d91aa10c4c551\",\"name\":\"list_111_opt_out\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Unsubscribe Confirmed.\",\"transfer_encoding\":\"quoted-printable\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetList/" &&
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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetTriggersAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTriggersCount_with_minimal_parameters()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetList/" &&
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
			var result = await apiClient.Triggers.GetCountAsync(USER_KEY);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggersCount_with_status()
		{
			// Arrange
			var status = TriggerStatus.Active;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetList/" &&
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
			var result = await apiClient.Triggers.GetCountAsync(USER_KEY, status: status);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggersCount_with_action()
		{
			// Arrange
			var action = TriggerAction.OptIn;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == action.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetCountAsync(USER_KEY, action: action);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggersCount_with_listid()
		{
			// Arrange
			var listId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetCountAsync(USER_KEY, listId: listId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggersCount_with_campaignid()
		{
			// Arrange
			var campaignId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (long)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetCountAsync(USER_KEY, campaignId: campaignId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggersCount_with_clientid()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetList/" &&
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
			var result = await apiClient.Triggers.GetCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task SendTriggerTestEmail_with_minimal_parameters()
		{
			// Arrange
			var triggerId = 123;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SendTestEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "test_type" && (string)p.Value == "merged" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "test_email" && (string)p.Value == recipientEmail && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.SendTestEmailAsync(USER_KEY, triggerId, recipientEmail);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SendTriggerTestEmail_with_separated_true()
		{
			// Arrange
			var triggerId = 123;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SendTestEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "test_type" && (string)p.Value == "separated" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "test_email" && (string)p.Value == recipientEmail && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.SendTestEmailAsync(USER_KEY, triggerId, recipientEmail, separated: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SendTriggerTestEmail_with_clientid()
		{
			// Arrange
			var triggerId = 123;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/SendTestEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "test_type" && (string)p.Value == "merged" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "test_email" && (string)p.Value == recipientEmail && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.SendTestEmailAsync(USER_KEY, triggerId, recipientEmail, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetTriggerRawEmailMessage_with_minimal_parameters()
		{
			// Arrange
			var triggerId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetEmailMessage/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"subject\":\"This is a simple message\",\"message\":\"...dummy content...\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetRawEmailMessageAsync(USER_KEY, triggerId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("This is a simple message", result.Subject);
		}

		[TestMethod]
		public async Task GetTriggerRawEmailMessage_with_clientid()
		{
			// Arrange
			var triggerId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetEmailMessage/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"subject\":\"This is a simple message\",\"message\":\"...dummy content...\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetRawEmailMessageAsync(USER_KEY, triggerId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("This is a simple message", result.Subject);
		}

		[TestMethod]
		public async Task GetTriggerRawHtml_with_minimal_parameters()
		{
			// Arrange
			var triggerId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetHtmlMessage/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"<html><body>...dummy content...</body></html>\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetRawHtmlAsync(USER_KEY, triggerId);

			// Assert
			Assert.AreEqual("<html><body>...dummy content...</body></html>", result);
		}

		[TestMethod]
		public async Task GetTriggerRawHtml_with_clientid()
		{
			// Arrange
			var triggerId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetHtmlMessage/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"<html><body>...dummy content...</body></html>\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetRawHtmlAsync(USER_KEY, triggerId, CLIENT_ID);

			// Assert
			Assert.AreEqual("<html><body>...dummy content...</body></html>", result);
		}

		[TestMethod]
		public async Task GetTriggerRawText_with_minimal_parameters()
		{
			// Arrange
			var triggerId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetTextMessage/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"...dummy content...\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetRawTextAsync(USER_KEY, triggerId);

			// Assert
			Assert.AreEqual("...dummy content...", result);
		}

		[TestMethod]
		public async Task GetTriggerRawText_with_clientid()
		{
			// Arrange
			var triggerId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetTextMessage/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"...dummy content...\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetRawTextAsync(USER_KEY, triggerId, CLIENT_ID);

			// Assert
			Assert.AreEqual("...dummy content...", result);
		}

		[TestMethod]
		public async Task UnleashTrigger_with_minimal_parameters()
		{
			// Arrange
			var triggerId = 123;
			var listMemberId = 111; 
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/Unleash/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (long)p.Value == listMemberId && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UnleashAsync(USER_KEY, triggerId, listMemberId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UnleashTrigger_with_clientid()
		{
			// Arrange
			var triggerId = 123;
			var listMemberId = 111; 
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/Unleash/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (long)p.Value == listMemberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.UnleashAsync(USER_KEY, triggerId, listMemberId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetTriggerLogs_with_minimal_parameters()
		{
			// Arrange
			var triggerId = 123;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsAsync(USER_KEY, triggerId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLogs_with_logtype()
		{
			// Arrange
			var triggerId = 123;
			var logType = LogType.Sent;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == logType.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0}]}}}}", sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsAsync(USER_KEY, triggerId, logType: logType);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLogs_with_listmemberid()
		{
			// Arrange
			var triggerId = 123;
			var listMemberId = 111;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (long)p.Value == listMemberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0}]}}}}", sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsAsync(USER_KEY, triggerId, listMemberId: listMemberId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLogs_with_startdate()
		{
			// Arrange
			var triggerId = 123;
			var start = new DateTime(2015, 1, 1);

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "start_time" && (string)p.Value == start.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsAsync(USER_KEY, triggerId, start: start);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLogs_with_enddate()
		{
			// Arrange
			var triggerId = 123;
			var end = new DateTime(2015, 12, 31);

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "end_time" && (string)p.Value == end.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsAsync(USER_KEY, triggerId, uniques: false, totals: false, end: end);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLogs_with_limit()
		{
			// Arrange
			var triggerId = 123;
			var limit = 5;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsAsync(USER_KEY, triggerId, uniques: false, totals: false, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLogs_with_offset()
		{
			// Arrange
			var triggerId = 123;
			var offset = 25;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsAsync(USER_KEY, triggerId, uniques: false, totals: false, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLogs_with_clientid()
		{
			// Arrange
			var triggerId = 123;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsAsync(USER_KEY, triggerId, uniques: false, totals: false, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLogs_with_uniques_true()
		{
			// Arrange
			var triggerId = 123;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsAsync(USER_KEY, triggerId, uniques: true, totals: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLogs_with_totals_true()
		{
			// Arrange
			var triggerId = 123;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsAsync(USER_KEY, triggerId, uniques: false, totals: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLogsCount_with_minimal_parameters()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsCountAsync(USER_KEY, triggerId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggerLogsCount_with_logtype()
		{
			// Arrange
			var triggerId = 123;
			var logType = LogType.Sent;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == logType.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsCountAsync(USER_KEY, triggerId, logType: logType);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggerLogsCount_with_listmemberid()
		{
			// Arrange
			var triggerId = 123;
			var listMemberId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (long)p.Value == listMemberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsCountAsync(USER_KEY, triggerId, listMemberId: listMemberId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggerLogsCount_with_uniques_true()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsCountAsync(USER_KEY, triggerId, uniques: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggerLogsCount_with_totals_true()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsCountAsync(USER_KEY, triggerId, totals: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggerLogsCount_with_startdate()
		{
			// Arrange
			var triggerId = 123;
			var start = new DateTime(2015, 1, 1, 0, 0, 0);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "start_time" && (string)p.Value == start.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsCountAsync(USER_KEY, triggerId, start: start);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggerLogsCount_with_enddate()
		{
			// Arrange
			var triggerId = 123;
			var end = new DateTime(2015, 12, 31, 23, 59, 59);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "end_time" && (string)p.Value == end.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsCountAsync(USER_KEY, triggerId, end: end);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggerLogsCount_with_clientid()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLogsCountAsync(USER_KEY, triggerId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggerLinks_with_minimal_parameters()
		{
			// Arrange
			var triggerId = 123;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinks/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksAsync(USER_KEY, triggerId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLinks_with_limit()
		{
			// Arrange
			var triggerId = 123;
			var limit = 5;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinks/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksAsync(USER_KEY, triggerId, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLinks_with_offset()
		{
			// Arrange
			var triggerId = 123;
			var offset = 25;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinks/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksAsync(USER_KEY, triggerId, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLinks_with_clientid()
		{
			// Arrange
			var triggerId = 123;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinks/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksAsync(USER_KEY, triggerId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLinksCount_with_minimal_parameters()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinks/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksCountAsync(USER_KEY, triggerId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggerLinksCount_with_clientid()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinks/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = await apiClient.Triggers.GetLinksCountAsync(USER_KEY, triggerId, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggerLink_with_minimal_parameters()
		{
			// Arrange
			var linkId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinkInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "link_id" && (long)p.Value == linkId && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}}}}", linkId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinkAsync(USER_KEY, linkId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(linkId, result.Id);
		}

		[TestMethod]
		public async Task GetTriggerLink_with_clientid()
		{
			// Arrange
			var linkId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinkInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "link_id" && (long)p.Value == linkId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}}}}", linkId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinkAsync(USER_KEY, linkId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(linkId, result.Id);
		}

		[TestMethod]
		public async Task GetTriggerLinksWithStats_with_minimal_parameters()
		{
			// Arrange
			var triggerId = 123;

			var jsonClickLog1 = "{\"id\":\"1111\",\"link_to\":\"http://www.fictitiouscompany.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";
			var jsonClickLog2 = "{\"id\":\"2222\",\"link_to\":\"http://www.cakemail.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinksLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonClickLog1, jsonClickLog2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksWithStatsAsync(USER_KEY, triggerId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLinksWithStats_with_startdate()
		{
			// Arrange
			var triggerId = 123;
			var start = new DateTime(2015, 1, 1);

			var jsonClickLog1 = "{\"id\":\"1111\",\"link_to\":\"http://www.fictitiouscompany.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";
			var jsonClickLog2 = "{\"id\":\"2222\",\"link_to\":\"http://www.cakemail.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinksLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "start_time" && (string)p.Value == start.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonClickLog1, jsonClickLog2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksWithStatsAsync(USER_KEY, triggerId, start: start);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLinksWithStats_with_enddate()
		{
			// Arrange
			var triggerId = 123;
			var end = new DateTime(2015, 12, 31);

			var jsonClickLog1 = "{\"id\":\"1111\",\"link_to\":\"http://www.fictitiouscompany.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";
			var jsonClickLog2 = "{\"id\":\"2222\",\"link_to\":\"http://www.cakemail.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinksLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "end_time" && (string)p.Value == end.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonClickLog1, jsonClickLog2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksWithStatsAsync(USER_KEY, triggerId, end: end);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLinksWithStats_with_limit()
		{
			// Arrange
			var triggerId = 123;
			var limit = 5;

			var jsonClickLog1 = "{\"id\":\"1111\",\"link_to\":\"http://www.fictitiouscompany.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";
			var jsonClickLog2 = "{\"id\":\"2222\",\"link_to\":\"http://www.cakemail.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinksLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonClickLog1, jsonClickLog2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksWithStatsAsync(USER_KEY, triggerId, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLinksWithStats_with_offset()
		{
			// Arrange
			var triggerId = 123;
			var offset = 25;

			var jsonClickLog1 = "{\"id\":\"1111\",\"link_to\":\"http://www.fictitiouscompany.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";
			var jsonClickLog2 = "{\"id\":\"2222\",\"link_to\":\"http://www.cakemail.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinksLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonClickLog1, jsonClickLog2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksWithStatsAsync(USER_KEY, triggerId, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTriggerLinksWithStats_with_clientid()
		{
			// Arrange
			var triggerId = 123;

			var jsonClickLog1 = "{\"id\":\"1111\",\"link_to\":\"http://www.fictitiouscompany.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";
			var jsonClickLog2 = "{\"id\":\"2222\",\"link_to\":\"http://www.cakemail.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinksLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonClickLog1, jsonClickLog2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksWithStatsAsync(USER_KEY, triggerId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}
		
		[TestMethod]
		public async Task GetTriggerLinksWithStatsCount_with_minimal_parameters()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinksLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksWithStatsCountAsync(USER_KEY, triggerId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggerLinksWithStatsCount_with_startdate()
		{
			// Arrange
			var triggerId = 123;
			var start = new DateTime(2015, 1, 1, 0, 0, 0);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinksLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "start_time" && (string)p.Value == start.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksWithStatsCountAsync(USER_KEY, triggerId, start: start);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggerLinksWithStatsCount_with_enddate()
		{
			// Arrange
			var triggerId = 123;
			var end = new DateTime(2015, 12, 31, 23, 59, 59);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinksLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "end_time" && (string)p.Value == end.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksWithStatsCountAsync(USER_KEY, triggerId, end: end);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTriggerLinksWithStatsCount_with_clientid()
		{
			// Arrange
			var triggerId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Trigger/GetLinksLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "trigger_id" && (long)p.Value == triggerId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = await apiClient.Triggers.GetLinksWithStatsCountAsync(USER_KEY, triggerId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}
	}
}
