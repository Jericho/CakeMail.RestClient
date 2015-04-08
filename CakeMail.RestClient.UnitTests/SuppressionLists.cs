using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class SuppressionListsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const int CLIENT_ID = 999;

		[TestMethod]
		public void AddEmailAddressesToSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var emailAddresses = new[] { "aaa@aaa.com", "bbb@bbb.com", "ccc@ccc.com" };

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email[0]" && (string)p.Value == emailAddresses[0] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email[1]" && (string)p.Value == emailAddresses[1] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email[2]" && (string)p.Value == emailAddresses[2] && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.AddEmailAddressesToSuppressionList(USER_KEY, emailAddresses);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void AddEmailAddressesToSuppressionList_with_null_array()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.AddEmailAddressesToSuppressionList(USER_KEY, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void AddEmailAddressesToSuppressionList_with_clientid()
		{
			// Arrange
			var emailAddresses = new[] { "aaa@aaa.com", "bbb@bbb.com", "ccc@ccc.com" };

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email[0]" && (string)p.Value == emailAddresses[0] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email[1]" && (string)p.Value == emailAddresses[1] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email[2]" && (string)p.Value == emailAddresses[2] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.AddEmailAddressesToSuppressionList(USER_KEY, emailAddresses, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void AddDomainsToSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var domains = new[] { "aaa.com", "bbb.com", "ccc.com" };

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "domain[0]" && (string)p.Value == domains[0] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "domain[1]" && (string)p.Value == domains[1] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "domain[2]" && (string)p.Value == domains[2] && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"domain\":\"aaa.com\"},{\"domain\":\"bbb.com\"},{\"domain\":\"ccc.com\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.AddDomainsToSuppressionList(USER_KEY, domains);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void AddDomainsToSuppressionList_with_null_array()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"domain\":\"aaa.com\"},{\"domain\":\"bbb.com\"},{\"domain\":\"ccc.com\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.AddDomainsToSuppressionList(USER_KEY, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void AddDomainsToSuppressionList_with_clientid()
		{
			// Arrange
			var domains = new[] { "aaa.com", "bbb.com", "ccc.com" };

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "domain[0]" && (string)p.Value == domains[0] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "domain[1]" && (string)p.Value == domains[1] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "domain[2]" && (string)p.Value == domains[2] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"domain\":\"aaa.com\"},{\"domain\":\"bbb.com\"},{\"domain\":\"ccc.com\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.AddDomainsToSuppressionList(USER_KEY, domains, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void AddLocalPartsToSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var localParts = new[] { "administrator", "manager", "info" };

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "localpart[0]" && (string)p.Value == localParts[0] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "localpart[1]" && (string)p.Value == localParts[1] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "localpart[2]" && (string)p.Value == localParts[2] && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"manager\"},{\"localpart\":\"info\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.AddLocalPartsToSuppressionList(USER_KEY, localParts);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void AddLocalPartsToSuppressionList_with_null_array()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"manager\"},{\"localpart\":\"info\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.AddLocalPartsToSuppressionList(USER_KEY, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void AddLocalPartsToSuppressionList_with_clientid()
		{
			// Arrange
			var localParts = new[] { "administrator", "manager", "info" };

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "localpart[0]" && (string)p.Value == localParts[0] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "localpart[1]" && (string)p.Value == localParts[1] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "localpart[2]" && (string)p.Value == localParts[2] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"manager\"},{\"localpart\":\"info\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.AddLocalPartsToSuppressionList(USER_KEY, localParts, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void RemoveEmailAddressesFromSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var emailAddresses = new[] { "aaa@aaa.com", "bbb@bbb.com", "ccc@ccc.com" };

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email[0]" && (string)p.Value == emailAddresses[0] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email[1]" && (string)p.Value == emailAddresses[1] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email[2]" && (string)p.Value == emailAddresses[2] && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.RemoveEmailAddressesFromSuppressionList(USER_KEY, emailAddresses);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void RemoveEmailAddressesFromSuppressionList_with_null_array()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.RemoveEmailAddressesFromSuppressionList(USER_KEY, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void RemoveEmailAddressesFromSuppressionList_with_clientid()
		{
			// Arrange
			var emailAddresses = new[] { "aaa@aaa.com", "bbb@bbb.com", "ccc@ccc.com" };

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email[0]" && (string)p.Value == emailAddresses[0] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email[1]" && (string)p.Value == emailAddresses[1] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email[2]" && (string)p.Value == emailAddresses[2] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.RemoveEmailAddressesFromSuppressionList(USER_KEY, emailAddresses, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void RemoveDomainsFromSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var domains = new[] { "aaa.com", "bbb.com", "ccc.com" };

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "domain[0]" && (string)p.Value == domains[0] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "domain[1]" && (string)p.Value == domains[1] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "domain[2]" && (string)p.Value == domains[2] && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"domain\":\"aaa.com\"},{\"domain\":\"bbb.com\"},{\"domain\":\"ccc.com\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.RemoveDomainsFromSuppressionList(USER_KEY, domains);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void RemoveDomainsFromSuppressionList_with_null_array()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"domain\":\"aaa.com\"},{\"domain\":\"bbb.com\"},{\"domain\":\"ccc.com\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.RemoveDomainsFromSuppressionList(USER_KEY, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void RemoveDomainsFromSuppressionList_with_clientid()
		{
			// Arrange
			var domains = new[] { "aaa.com", "bbb.com", "ccc.com" };

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "domain[0]" && (string)p.Value == domains[0] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "domain[1]" && (string)p.Value == domains[1] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "domain[2]" && (string)p.Value == domains[2] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"domain\":\"aaa.com\"},{\"domain\":\"bbb.com\"},{\"domain\":\"ccc.com\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.RemoveDomainsFromSuppressionList(USER_KEY, domains, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void RemoveLocalPartsFromSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var localParts = new[] { "administrator", "manager", "info" };

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "localpart[0]" && (string)p.Value == localParts[0] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "localpart[1]" && (string)p.Value == localParts[1] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "localpart[2]" && (string)p.Value == localParts[2] && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"manager\"},{\"localpart\":\"info\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.RemoveLocalPartsFromSuppressionList(USER_KEY, localParts);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void RemoveLocalPartsFromSuppressionList_with_null_array()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"manager\"},{\"localpart\":\"info\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.RemoveLocalPartsFromSuppressionList(USER_KEY, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void RemoveLocalPartsFromSuppressionList_with_clientid()
		{
			// Arrange
			var localParts = new[] { "administrator", "manager", "info" };

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "localpart[0]" && (string)p.Value == localParts[0] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "localpart[1]" && (string)p.Value == localParts[1] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "localpart[2]" && (string)p.Value == localParts[2] && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"manager\"},{\"localpart\":\"info\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.RemoveLocalPartsFromSuppressionList(USER_KEY, localParts, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void GetSuppressedEmailAddresses_with_minimal_parameters()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"emails\":[{\"email\":\"aaa@aaa.com\",\"source_type\":\"manual\"},{\"email\":\"bbb@bbb.com\",\"source_type\":\"manual\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedEmailAddresses(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetSuppressedEmailAddresses_with_limit()
		{
			// Arrange
			var limit = 5;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"emails\":[{\"email\":\"aaa@aaa.com\",\"source_type\":\"manual\"},{\"email\":\"bbb@bbb.com\",\"source_type\":\"manual\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedEmailAddresses(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetSuppressedEmailAddresses_with_offset()
		{
			// Arrange
			var offset = 25;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"emails\":[{\"email\":\"aaa@aaa.com\",\"source_type\":\"manual\"},{\"email\":\"bbb@bbb.com\",\"source_type\":\"manual\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedEmailAddresses(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetSuppressedEmailAddresses_with_clientid()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"emails\":[{\"email\":\"aaa@aaa.com\",\"source_type\":\"manual\"},{\"email\":\"bbb@bbb.com\",\"source_type\":\"manual\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedEmailAddresses(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetSuppressedDomains_with_minimal_parameters()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"domains\":[{\"domain\":\"aaa.org\"},{\"domain\":\"bbb.com\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedDomains(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetSuppressedDomains_with_limit()
		{
			// Arrange
			var limit = 5;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"domains\":[{\"domain\":\"aaa.org\"},{\"domain\":\"bbb.com\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedDomains(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetSuppressedDomains_with_offset()
		{
			// Arrange
			var offset = 25;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"domains\":[{\"domain\":\"aaa.org\"},{\"domain\":\"bbb.com\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedDomains(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetSuppressedDomains_with_clientid()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"domains\":[{\"domain\":\"aaa.org\"},{\"domain\":\"bbb.com\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedDomains(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetSuppressedLocalParts_with_minimal_parameters()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"info\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedLocalParts(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetSuppressedLocalParts_with_limit()
		{
			// Arrange
			var limit = 5;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"info\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedLocalParts(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetSuppressedLocalParts_with_offset()
		{
			// Arrange
			var offset = 25;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"info\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedLocalParts(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetSuppressedLocalParts_with_clientid()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"info\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedLocalParts(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetSuppressedEmailAddressesCount_with_minimal_parameters()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedEmailAddressesCount(USER_KEY);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetSuppressedEmailAddressesCount_with_clientid()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.GetSuppressedEmailAddressesCount(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetSuppressedDomainsCount_with_minimal_parameters()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedDomainsCount(USER_KEY);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetSuppressedDomainsCount_with_clientid()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.GetSuppressedDomainsCount(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetSuppressedLocalPartsCount_with_minimal_parameters()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetSuppressedLocalPartsCount(USER_KEY);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetSuppressedLocalPartsCount_with_clientid()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.GetSuppressedLocalPartsCount(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}
	}
}
