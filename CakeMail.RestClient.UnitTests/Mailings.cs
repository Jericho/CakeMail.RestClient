using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class MailingsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const int CLIENT_ID = 999;

		[TestMethod]
		public void CreateMailing_with_minimal_parameters()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "type" && (string)p.Value == "standard" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateMailing(USER_KEY, name);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public void CreateMailing_without_type()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateMailing(USER_KEY, name, type: null);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public void CreateMailing_with_campaignid()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123;
			var campaignId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "type" && (string)p.Value == "standard" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (int)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateMailing(USER_KEY, name, campaignId: campaignId);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public void CreateMailing_with_type()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123;
			var type = "recurring";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "type" && (string)p.Value == type && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateMailing(USER_KEY, name, type: type);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public void CreateMailing_with_recurringid()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123;
			var recurringId = 222;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "type" && (string)p.Value == "standard" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "recurring_id" && (int)p.Value == recurringId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateMailing(USER_KEY, name, recurringId: recurringId);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public void CreateMailing_with_encoding()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123;
			var encoding = "iso-8859-x";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "type" && (string)p.Value == "standard" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "encoding" && (string)p.Value == encoding && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateMailing(USER_KEY, name, encoding: encoding);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public void CreateMailing_with_trasnferencoding()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123;
			var transferEncoding = "base64";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "type" && (string)p.Value == "standard" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "transfer_encoding" && (string)p.Value == transferEncoding && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateMailing(USER_KEY, name, transferEncoding: transferEncoding);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public void CreateMailing_with_clientid()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "type" && (string)p.Value == "standard" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateMailing(USER_KEY, name, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public void DeleteMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteMailing(USER_KEY, mailingId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void DeleteMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteMailing(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"id\":\"{0}\",\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}}}}", mailingId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailing(USER_KEY, mailingId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(mailingId, result.Id);
		}

		[TestMethod]
		public void GetMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"id\":\"{0}\",\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}}}}", mailingId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailing(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(mailingId, result.Id);
		}

		[TestMethod]
		public void GetMailings_with_minimal_parameters()
		{
			// Arrange
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailings(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetMailings_with_status()
		{
			// Arrange
			var status = "active";
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailings(USER_KEY, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetMailings_with_type()
		{
			// Arrange
			var type = "standard";
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "type" && (string)p.Value == type && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailings(USER_KEY, type: type);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetMailings_with_name()
		{
			// Arrange
			var name = "My mailing";
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailings(USER_KEY, name: name);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetMailings_with_listid()
		{
			// Arrange
			var listId = 111;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailings(USER_KEY, listId: listId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetMailings_with_campaignid()
		{
			// Arrange
			var campaignId = 111;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (int)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailings(USER_KEY, campaignId: campaignId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetMailings_with_recurringid()
		{
			// Arrange
			var recurringId = 111;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "recurring_id" && (int)p.Value == recurringId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailings(USER_KEY, recurringId: recurringId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetMailings_with_start()
		{
			// Arrange
			var start = new DateTime(2015, 1, 1);
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "start_date" && (string)p.Value == "2015-01-01 00:00:00" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailings(USER_KEY, start: start);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetMailings_with_end()
		{
			// Arrange
			var end = new DateTime(2015, 12, 31, 23, 59, 59);
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "end_date" && (string)p.Value == "2015-12-31 23:59:59" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailings(USER_KEY, end: end);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetMailings_with_sortby()
		{
			// Arrange
			var sortBy = "name";
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sort_by" && (string)p.Value == sortBy && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailings(USER_KEY, sortBy: sortBy);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetMailings_with_sortdirection()
		{
			// Arrange
			var sortDirection = "desc";
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "direction" && (string)p.Value == sortDirection && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailings(USER_KEY, sortDirection: sortDirection);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetMailings_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailings(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetMailings_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailings(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetMailings_with_clientid()
		{
			// Arrange
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailings(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetMailingsCount_with_minimal_parameters()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
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
			var result = apiClient.GetMailingsCount(USER_KEY);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingsCount_with_status()
		{
			// Arrange
			var status = "active";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingsCount(USER_KEY, status: status);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingsCount_with_type()
		{
			// Arrange
			var type = "standard";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "type" && (string)p.Value == type && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingsCount(USER_KEY, type: type);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingsCount_with_name()
		{
			// Arrange
			var name = "My mailing";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.GetMailingsCount(USER_KEY, name: name);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingsCount_with_listid()
		{
			// Arrange
			var listId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingsCount(USER_KEY, listId: listId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingsCount_with_campaignid()
		{
			// Arrange
			var campaignId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (int)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingsCount(USER_KEY, campaignId: campaignId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingsCount_with_recurringid()
		{
			// Arrange
			var recurringId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "recurring_id" && (int)p.Value == recurringId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingsCount(USER_KEY, recurringId: recurringId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingsCount_with_start()
		{
			// Arrange
			var start = new DateTime(2015, 1, 1);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "start_date" && (string)p.Value == "2015-01-01 00:00:00" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingsCount(USER_KEY, start: start);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingsCount_with_end()
		{
			// Arrange
			var end = new DateTime(2015, 12, 31, 23, 59, 59);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "end_date" && (string)p.Value == "2015-12-31 23:59:59" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingsCount(USER_KEY, end: end);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingsCount_with_clientid()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
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
			var result = apiClient.GetMailingsCount(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void UpdateMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_campaignid()
		{
			// Arrange
			var mailingId = 123;
			var campaignId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "campaign_id" && (int)p.Value == campaignId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, campaignId: campaignId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_listid()
		{
			// Arrange
			var mailingId = 123;
			var listId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, listId: listId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_sublistid()
		{
			// Arrange
			var mailingId = 123;
			var sublistId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == sublistId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, sublistId: sublistId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_name()
		{
			// Arrange
			var mailingId = 123;
			var name = "My mailing";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, name: name);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_type()
		{
			// Arrange
			var mailingId = 123;
			var type = "standard";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "type" && (string)p.Value == type && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, type: type);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_encoding()
		{
			// Arrange
			var mailingId = 123;
			var encoding = "utf-8";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "encoding" && (string)p.Value == encoding && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, encoding: encoding);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_transferencoding()
		{
			// Arrange
			var mailingId = 123;
			var transferEncoding = "base64";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "transfer_encoding" && (string)p.Value == transferEncoding && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, transferEncoding: transferEncoding);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_subject()
		{
			// Arrange
			var mailingId = 123;
			var subject = "My mailing subject";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "subject" && (string)p.Value == subject && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, subject: subject);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_senderemail()
		{
			// Arrange
			var mailingId = 123;
			var senderEmail = "bobsmith@fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_email" && (string)p.Value == senderEmail && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, senderEmail: senderEmail);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_sendername()
		{
			// Arrange
			var mailingId = 123;
			var senderName = "Bob Smith";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_name" && (string)p.Value == senderName && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, senderName: senderName);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_replyto()
		{
			// Arrange
			var mailingId = 123;
			var replyTo = "marketing@fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "reply_to" && (string)p.Value == replyTo && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, replyTo: replyTo);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_htmlcontent()
		{
			// Arrange
			var mailingId = 123;
			var htmlContent = "<html><body>Hello world</body></html>";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "html_message" && (string)p.Value == htmlContent && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, htmlContent: htmlContent);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_textcontent()
		{
			// Arrange
			var mailingId = 123;
			var textContent = "Hello world";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "text_message" && (string)p.Value == textContent && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, textContent: textContent);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_trackopens_true()
		{
			// Arrange
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "opening_stats" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, trackOpens: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_trackopens_false()
		{
			// Arrange
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "opening_stats" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, trackOpens: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_trackclicksinhtml_true()
		{
			// Arrange
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "clickthru_html" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, trackClicksInHtml: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_trackclicksinhtml_false()
		{
			// Arrange
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "clickthru_html" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, trackClicksInHtml: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_trackclicksintext_true()
		{
			// Arrange
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "clickthru_text" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, trackClicksInText: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_trackclicksintext_false()
		{
			// Arrange
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "clickthru_text" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, trackClicksInText: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_trackingparameters()
		{
			// Arrange
			var mailingId = 123;
			var trackingParameters = "param1=abc&param2=123";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "tracking_params" && (string)p.Value == trackingParameters && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, trackingParameters: trackingParameters);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_endingon()
		{
			// Arrange
			var mailingId = 123;
			var endingOn = new DateTime(2015, 7, 10);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "ending_on" && (string)p.Value == "2015-07-10 00:00:00" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, endingOn: endingOn);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_maxrecurrences()
		{
			// Arrange
			var mailingId = 123;
			var maxRecurrences = 99;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "max_recurrences" && (int)p.Value == maxRecurrences && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, maxRecurrences: maxRecurrences);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_recurringconditions()
		{
			// Arrange
			var mailingId = 123;
			var recurringConditions = "???";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "recurring_conditions" && (string)p.Value == recurringConditions && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, recurringConditions: recurringConditions);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMailing_with_clientid()
		{
			// Arrange
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateMailing(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void SendMailingTestEmail_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "test_type" && (string)p.Value == "merged" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "test_email" && (string)p.Value == recipientEmail && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.SendMailingTestEmail(USER_KEY, mailingId, recipientEmail);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void SendMailingTestEmail_with_separated_true()
		{
			// Arrange
			var mailingId = 123;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "test_type" && (string)p.Value == "separated" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "test_email" && (string)p.Value == recipientEmail && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.SendMailingTestEmail(USER_KEY, mailingId, recipientEmail, separated: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void SendMailingTestEmail_with_clientid()
		{
			// Arrange
			var mailingId = 123;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "test_type" && (string)p.Value == "merged" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "test_email" && (string)p.Value == recipientEmail && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.SendMailingTestEmail(USER_KEY, mailingId, recipientEmail, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetMailingRawEmailMessage_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"subject\":\"This is a simple message\",\"message\":\"...dummy content...\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingRawEmailMessage(USER_KEY, mailingId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("This is a simple message", result.Subject);
		}

		[TestMethod]
		public void GetMailingRawEmailMessage_with_clientid()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"subject\":\"This is a simple message\",\"message\":\"...dummy content...\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingRawEmailMessage(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("This is a simple message", result.Subject);
		}

		[TestMethod]
		public void GetMailingRawHtml_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"<html><body>...dummy content...</body></html>\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingRawHtml(USER_KEY, mailingId);

			// Assert
			Assert.AreEqual("<html><body>...dummy content...</body></html>", result);
		}

		[TestMethod]
		public void GetMailingRawHtml_with_clientid()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"<html><body>...dummy content...</body></html>\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingRawHtml(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			Assert.AreEqual("<html><body>...dummy content...</body></html>", result);
		}

		[TestMethod]
		public void GetMailingRawText_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"...dummy content...\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingRawText(USER_KEY, mailingId);

			// Assert
			Assert.AreEqual("...dummy content...", result);
		}

		[TestMethod]
		public void GetMailingRawText_with_clientid()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"...dummy content...\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingRawText(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			Assert.AreEqual("...dummy content...", result);
		}

		[TestMethod]
		public void ScheduleMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.ScheduleMailing(USER_KEY, mailingId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void ScheduleMailing_with_date()
		{
			// Arrange
			var mailingId = 12345;
			var date = new DateTime(2015, 4, 3, 17, 0, 0);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "date" && (string)p.Value == "2015-04-03 17:00:00" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.ScheduleMailing(USER_KEY, mailingId, date: date);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void ScheduleMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.ScheduleMailing(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UnscheduleMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UnscheduleMailing(USER_KEY, mailingId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UnscheduleMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UnscheduleMailing(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void SuspendMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.SuspendMailing(USER_KEY, mailingId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void SuspendMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.SuspendMailing(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void ResumeMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.ResumeMailing(USER_KEY, mailingId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void ResumeMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.ResumeMailing(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetMailingLogs_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogs(USER_KEY, mailingId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetMailingLogs_with_logtype()
		{
			// Arrange
			var mailingId = 123;
			var logType = "in_queue";

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == logType && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0}]}}}}", sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogs(USER_KEY, mailingId, logType: logType);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public void GetMailingLogs_with_listmemberid()
		{
			// Arrange
			var mailingId = 123;
			var listMemberId = 111;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (int)p.Value == listMemberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0}]}}}}", sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogs(USER_KEY, mailingId, listMemberId: listMemberId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public void GetMailingLogs_with_startdate()
		{
			// Arrange
			var mailingId = 123;
			var start = new DateTime(2015, 1, 1);

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "start_time" && (string)p.Value == "2015-01-01 00:00:00" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogs(USER_KEY, mailingId, start: start);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetMailingLogs_with_enddate()
		{
			// Arrange
			var mailingId = 123;
			var end = new DateTime(2015, 12, 31);

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "end_time" && (string)p.Value == "2015-12-31 00:00:00" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogs(USER_KEY, mailingId, uniques: false, totals: false, end: end);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetMailingLogs_with_limit()
		{
			// Arrange
			var mailingId = 123;
			var limit = 5;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogs(USER_KEY, mailingId, uniques: false, totals: false, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetMailingLogs_with_offset()
		{
			// Arrange
			var mailingId = 123;
			var offset = 25;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogs(USER_KEY, mailingId, uniques: false, totals: false, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetMailingLogs_with_clientid()
		{
			// Arrange
			var mailingId = 123;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogs(USER_KEY, mailingId, uniques: false, totals: false, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetMailingLogs_with_uniques_true()
		{
			// Arrange
			var mailingId = 123;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogs(USER_KEY, mailingId, uniques: true, totals: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetMailingLogs_with_totals_true()
		{
			// Arrange
			var mailingId = 123;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogs(USER_KEY, mailingId, uniques: false, totals: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetMailingLogsCount_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogsCount(USER_KEY, mailingId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingLogsCount_with_logtype()
		{
			// Arrange
			var mailingId = 123;
			var logType = "in_queue";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == logType && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogsCount(USER_KEY, mailingId, logType: logType);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingLogsCount_with_listmemberid()
		{
			// Arrange
			var mailingId = 123;
			var listMemberId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (int)p.Value == listMemberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogsCount(USER_KEY, mailingId, listMemberId: listMemberId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingLogsCount_with_uniques_true()
		{
			// Arrange
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogsCount(USER_KEY, mailingId, uniques: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingLogsCount_with_totals_true()
		{
			// Arrange
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogsCount(USER_KEY, mailingId, totals: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingLogsCount_with_startdate()
		{
			// Arrange
			var mailingId = 123;
			var start = new DateTime(2015, 1, 1, 0, 0, 0);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "start_time" && (string)p.Value == "2015-01-01 00:00:00" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogsCount(USER_KEY, mailingId, start: start);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingLogsCount_with_enddate()
		{
			// Arrange
			var mailingId = 123;
			var end = new DateTime(2015, 12, 31, 23, 59, 59);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "end_time" && (string)p.Value == end.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogsCount(USER_KEY, mailingId, end: end);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingLogsCount_with_clientid()
		{
			// Arrange
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLogsCount(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingLinks_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLinks(USER_KEY, mailingId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetMailingLinks_with_limit()
		{
			// Arrange
			var mailingId = 123;
			var limit = 5;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLinks(USER_KEY, mailingId, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetMailingLinks_with_offset()
		{
			// Arrange
			var mailingId = 123;
			var offset = 25;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLinks(USER_KEY, mailingId, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetMailingLinks_with_clientid()
		{
			// Arrange
			var mailingId = 123;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLinks(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetMailingLinksCount_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLinksCount(USER_KEY, mailingId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingLinksCount_with_clientid()
		{
			// Arrange
			var mailingId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.GetMailingLinksCount(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetMailingLink_with_minimal_parameters()
		{
			// Arrange
			var linkId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "link_id" && (int)p.Value == linkId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}}}}", linkId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLink(USER_KEY, linkId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(linkId, result.Id);
		}

		[TestMethod]
		public void GetMailingLink_with_clientid()
		{
			// Arrange
			var linkId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "link_id" && (int)p.Value == linkId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}}}}", linkId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetMailingLink(USER_KEY, linkId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(linkId, result.Id);
		}
	}
}
