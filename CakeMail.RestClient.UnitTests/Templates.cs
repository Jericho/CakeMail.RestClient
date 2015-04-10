using CakeMail.RestClient.Utilities;
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
	}
}