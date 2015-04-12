using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class TemplatesTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const int CLIENT_ID = 999;

		[TestMethod]
		public void CreateTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var templateId = 123;
			var labels = new Dictionary<string, string>()
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/CreateCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[en_US]" && (string)p.Value == "My Category" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[fr_FR]" && (string)p.Value == "Ma Catégorie" && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", templateId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateTemplateCategory(USER_KEY, labels);

			// Assert
			Assert.AreEqual(templateId, result);
		}

		[TestMethod]
		public void CreateTemplateCategory_with_no_labels()
		{
			// Arrange
			var templateId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/CreateCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "json",
					Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", templateId)
				});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateTemplateCategory(USER_KEY, null);

			// Assert
			Assert.AreEqual(templateId, result);
		}

		[TestMethod]
		public void CreateTemplateCategory_with_isvisiblebydefault_false()
		{
			// Arrange
			var templateId = 123;
			var labels = new Dictionary<string, string>()
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/CreateCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "0" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[en_US]" && (string)p.Value == "My Category" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[fr_FR]" && (string)p.Value == "Ma Catégorie" && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "json",
					Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", templateId)
				});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateTemplateCategory(USER_KEY, labels, isVisibleByDefault: false);

			// Assert
			Assert.AreEqual(templateId, result);
		}

		[TestMethod]
		public void CreateTemplateCategory_with_templatescanbecopied_false()
		{
			// Arrange
			var templateId = 123;
			var labels = new Dictionary<string, string>()
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/CreateCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "0" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[en_US]" && (string)p.Value == "My Category" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[fr_FR]" && (string)p.Value == "Ma Catégorie" && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "json",
					Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", templateId)
				});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateTemplateCategory(USER_KEY, labels, templatesCanBeCopied: false);

			// Assert
			Assert.AreEqual(templateId, result);
		}

		[TestMethod]
		public void CreateTemplateCategory_with_clientid()
		{
			// Arrange
			var templateId = 123;
			var labels = new Dictionary<string, string>()
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/CreateCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[en_US]" && (string)p.Value == "My Category" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[fr_FR]" && (string)p.Value == "Ma Catégorie" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "json",
					Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", templateId)
				});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateTemplateCategory(USER_KEY, labels, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(templateId, result);
		}

		[TestMethod]
		public void DeleteTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var categoryId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/DeleteCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (int)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteTemplateCategory(USER_KEY, categoryId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void DeleteTemplateCategory_with_clientid()
		{
			// Arrange
			var categoryId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/DeleteCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (int)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteTemplateCategory(USER_KEY, categoryId, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var categoryId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (int)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"owner_client_id\":\"{1}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}}}", categoryId, CLIENT_ID)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategory(USER_KEY, categoryId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(categoryId, result.Id);
		}

		[TestMethod]
		public void GetTemplateCategory_with_clientid()
		{
			// Arrange
			var categoryId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (int)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"owner_client_id\":\"{1}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}}}", categoryId, CLIENT_ID)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategory(USER_KEY, categoryId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(categoryId, result.Id);
		}

		[TestMethod]
		public void GetTemplateCategories_with_minimal_parameters()
		{
			// Arrange
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategories/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategories(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetTemplateCategories_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategories/" &&
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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategories(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetTemplateCategories_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategories/" &&
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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategories(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetTemplateCategories_with_clientid()
		{
			// Arrange
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategories/" &&
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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategories(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

	}
}