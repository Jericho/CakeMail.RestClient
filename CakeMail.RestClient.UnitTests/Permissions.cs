using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class PermissionsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const int CLIENT_ID = 999;

		[TestMethod]
		public void SetPermissions_with_all_parameters()
		{
			// Arrange
			var userId = 1234;
			var permissions = new string[] { "FirstPermission", "SecondPermission", "ThirdPermission" }; ;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "user_id" && (int)p.Value == userId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "permission[0]" && (string)p.Value == "FirstPermission" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "permission[1]" && (string)p.Value == "SecondPermission" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "permission[2]" && (string)p.Value == "ThirdPermission" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.SetUserPermissions(USER_KEY, userId, permissions, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void SetPermissions_without_clientid()
		{
			// Arrange
			var userId = 1234;
			var permissions = new string[] { "FirstPermission", "SecondPermission", "ThirdPermission" }; ;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "user_id" && (int)p.Value == userId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "permission[0]" && (string)p.Value == "FirstPermission" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "permission[1]" && (string)p.Value == "SecondPermission" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "permission[2]" && (string)p.Value == "ThirdPermission" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.SetUserPermissions(USER_KEY, userId, permissions, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetPermissions_with_all_parameters()
		{
			// Arrange
			var userId = 1234;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "user_id" && (int)p.Value == userId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"permissions\":[\"FirstPermission\",\"SecondPermission\",\"ThirdPermission\"],\"user_id\":\"{0}\"}}}}", userId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetUserPermissions(USER_KEY, userId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void GetPermissions_without_clientid()
		{
			// Arrange
			var userId = 1234;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "user_id" && (int)p.Value == userId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"permissions\":[\"FirstPermission\",\"SecondPermission\",\"ThirdPermission\"],\"user_id\":\"{0}\"}}}}", userId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetUserPermissions(USER_KEY, userId, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}
	}
}
