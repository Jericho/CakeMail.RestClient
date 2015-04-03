using CakeMail.RestClient.Models;
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
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "mailing_id" && (int)p.Value == mailingId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"id\":\"{0}\",\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}}}}", mailingId)
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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"id\":\"{0}\",\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}}}}", mailingId)
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
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

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
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
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
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
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
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
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
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
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
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
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
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
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
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
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
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
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
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
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
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
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
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

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
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

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
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.ficticiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.ficticiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

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
		public void GetListsCount_with_name()
		{
			// Arrange
			var name = "Dummy List";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
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
			var result = apiClient.GetListsCount(USER_KEY, name: name);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetListsCount_with_clientid()
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
			var result = apiClient.GetListsCount(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void UpdateList_name()
		{
			// Arrange
			var listId = 123;
			var name = "My list";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateList(USER_KEY, listId, name: name);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateList_language()
		{
			// Arrange
			var listId = 123;
			var language = "fr-FR";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "language" && (string)p.Value == language && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateList(USER_KEY, listId, language: language);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateList_spampolicy_accepted()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_policy" && (string)p.Value == "accepted" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateList(USER_KEY, listId, spamPolicyAccepted: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateList_spampolicy_declined()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_policy" && (string)p.Value == "declined" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateList(USER_KEY, listId, spamPolicyAccepted: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateList_status()
		{
			// Arrange
			var listId = 123;
			var status = "archived";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateList(USER_KEY, listId, status: status);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateList_sendername()
		{
			// Arrange
			var listId = 123;
			var senderName = "Bob Smith";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_name" && (string)p.Value == senderName && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateList(USER_KEY, listId, senderName: senderName);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateList_senderemail()
		{
			// Arrange
			var listId = 123;
			var senderEmail = "bobsmith@fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_email" && (string)p.Value == senderEmail && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateList(USER_KEY, listId, senderEmail: senderEmail);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateList_gotooi()
		{
			// Arrange
			var listId = 123;
			var goto_oi = "???";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "goto_oi" && (string)p.Value == goto_oi && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateList(USER_KEY, listId, goto_oi: goto_oi);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateList_gotodi()
		{
			// Arrange
			var listId = 123;
			var goto_di = "???";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "goto_di" && (string)p.Value == goto_di && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateList(USER_KEY, listId, goto_di: goto_di);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateList_gotooo()
		{
			// Arrange
			var listId = 123;
			var goto_oo = "???";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "goto_oo" && (string)p.Value == goto_oo && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateList(USER_KEY, listId, goto_oo: goto_oo);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateList_webhook()
		{
			// Arrange
			var listId = 123;
			var webhook = "???";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "webhook" && (string)p.Value == webhook && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateList(USER_KEY, listId, webhook: webhook);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateList_clientid()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateList(USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void AddListField_with_all_parameters()
		{
			// Arrange
			var listId = 123;
			var name = "My field";
			var type = "text";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "field" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "type" && (string)p.Value == type && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == "add" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.AddListField(USER_KEY, listId, name, type, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void AddListField_without_clientid()
		{
			// Arrange
			var listId = 123;
			var name = "My field";
			var type = "text";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "field" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "type" && (string)p.Value == type && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == "add" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.AddListField(USER_KEY, listId, name, type, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void DeleteListField_with_all_parameters()
		{
			// Arrange
			var listId = 123;
			var name = "My field";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "field" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == "delete" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteListField(USER_KEY, listId, name, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void DeleteListField_without_clientid()
		{
			// Arrange
			var listId = 123;
			var name = "My field";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "field" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == "delete" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteListField(USER_KEY, listId, name, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetListFields_with_all_parameters()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"id\":\"integer\",\"email\":\"text\",\"registered\":\"timestamp\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListFields(USER_KEY, listId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void GetListFields_without_clientid()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"id\":\"integer\",\"email\":\"text\",\"registered\":\"timestamp\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListFields(USER_KEY, listId, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public void GetListFields_returns_null()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":null}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListFields(USER_KEY, listId, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(0, result.Count());
		}

		[TestMethod]
		public void AddTestEmail_with_all_parameters()
		{
			// Arrange
			var listId = 123;
			var email = "test@test.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.AddTestEmail(USER_KEY, listId, email, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void AddTestEmail_without_clientid()
		{
			// Arrange
			var listId = 123;
			var email = "test@test.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.AddTestEmail(USER_KEY, listId, email, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void DeleteTestEmail_with_all_parameters()
		{
			// Arrange
			var listId = 123;
			var email = "test@test.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteTestEmail(USER_KEY, listId, email, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void DeleteTestEmail_without_clientid()
		{
			// Arrange
			var listId = 123;
			var email = "test@test.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteTestEmail(USER_KEY, listId, email, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetTestEmails_with_all_parameters()
		{
			// Arrange
			var listId = 123;

			var testEmail1 = "aaa@aaa.com";
			var testEmail2 = "bbb@bbb.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"testemails\":[\"{0}\",\"{1}\"]}}}}", testEmail1, testEmail2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTestEmails(USER_KEY, listId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(testEmail1, result.ToArray()[0]);
			Assert.AreEqual(testEmail2, result.ToArray()[1]);
		}

		[TestMethod]
		public void GetTestEmails_without_clientid()
		{
			// Arrange
			var listId = 123;

			var testEmail1 = "aaa@aaa.com";
			var testEmail2 = "bbb@bbb.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"testemails\":[\"{0}\",\"{1}\"]}}}}", testEmail1, testEmail2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTestEmails(USER_KEY, listId, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(testEmail1, result.ToArray()[0]);
			Assert.AreEqual(testEmail2, result.ToArray()[1]);
		}

		[TestMethod]
		public void Subscribe_with_autoresponders_true()
		{
			// Arrange
			var listId = 123;
			var email = "aaa@aaa.com";
			var subscriberId = 777;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Subscribe(USER_KEY, listId, email, autoResponders: true);

			// Assert
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public void Subscribe_with_autoresponders_false()
		{
			// Arrange
			var listId = 123;
			var email = "aaa@aaa.com";
			var subscriberId = 777;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Subscribe(USER_KEY, listId, email, autoResponders: false);

			// Assert
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public void Subscribe_with_triggers_true()
		{
			// Arrange
			var listId = 123;
			var email = "aaa@aaa.com";
			var subscriberId = 777;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Subscribe(USER_KEY, listId, email, autoResponders: true);

			// Assert
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public void Subscribe_with_triggers_false()
		{
			// Arrange
			var listId = 123;
			var email = "aaa@aaa.com";
			var subscriberId = 777;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Subscribe(USER_KEY, listId, email, triggers: false);

			// Assert
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public void Subscribe_with_customfields()
		{
			// Arrange
			var listId = 123;
			var email = "aaa@aaa.com";
			var subscriberId = 777;
			var firstName = "Bob";
			var lastName = "Smith";

			var customFields = new[]
			{
				new KeyValuePair<string, object>("firstname", firstName),
				new KeyValuePair<string, object>("lastname", lastName),
				new KeyValuePair<string, object>("birthday", new DateTime(1973, 1, 1))
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 8 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "data[firstname]" && (string)p.Value == firstName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "data[lastname]" && (string)p.Value == lastName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "data[birthday]" && (string)p.Value == "1973-01-01 00:00:00" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Subscribe(USER_KEY, listId, email, customFields: customFields);

			// Assert
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public void Subscribe_with_clientid()
		{
			// Arrange
			var listId = 123;
			var email = "aaa@aaa.com";
			var subscriberId = 777;
			var firstName = "Bob";
			var lastName = "Smith";

			var customFields = new[]
			{
				new KeyValuePair<string, object>("firstname", firstName),
				new KeyValuePair<string, object>("lastname", lastName),
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Subscribe(USER_KEY, listId, email, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public void Import_with_autoresponders_true()
		{
			// Arrange
			var listId = 123;
			var listMembers = new[]
			{
				new ListMember() { Email = "aaa@aaa.com" },
				new ListMember() { Email = "bbb@bbb.com" },
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 7 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "import_to" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][email]" && (string)p.Value == "aaa@aaa.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][email]" && (string)p.Value == "bbb@bbb.com" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aa@aa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Import(USER_KEY, listId, listMembers, autoResponders: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void Import_with_autoresponders_false()
		{
			// Arrange
			var listId = 123;
			var listMembers = new[]
			{
				new ListMember() { Email = "aaa@aaa.com" },
				new ListMember() { Email = "bbb@bbb.com" },
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 7 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "import_to" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][email]" && (string)p.Value == "aaa@aaa.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][email]" && (string)p.Value == "bbb@bbb.com" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aa@aa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Import(USER_KEY, listId, listMembers, autoResponders: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void Import_with_triggers_true()
		{
			// Arrange
			var listId = 123;
			var listMembers = new[]
			{
				new ListMember() { Email = "aaa@aaa.com" },
				new ListMember() { Email = "bbb@bbb.com" },
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 7 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "import_to" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][email]" && (string)p.Value == "aaa@aaa.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][email]" && (string)p.Value == "bbb@bbb.com" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aa@aa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Import(USER_KEY, listId, listMembers, triggers: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void Import_with_triggers_false()
		{
			// Arrange
			var listId = 123;
			var listMembers = new[]
			{
				new ListMember() { Email = "aaa@aaa.com" },
				new ListMember() { Email = "bbb@bbb.com" },
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 7 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "import_to" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][email]" && (string)p.Value == "aaa@aaa.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][email]" && (string)p.Value == "bbb@bbb.com" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aa@aa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Import(USER_KEY, listId, listMembers, triggers: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void Import_with_customfields()
		{
			// Arrange
			var listId = 123;
			var listMembers = new[]
			{
				new ListMember() { Email = "aaa@aaa.com", CustomFields = new Dictionary<string, object>() { { "firstname", "Bob" }, { "lastname", "Smith" }, { "age", 41 }, { "birthday", new DateTime(1973, 1, 1) } } },
				new ListMember() { Email = "bbb@bbb.com", CustomFields = new Dictionary<string, object>() { { "firstname", "Jane" }, { "lastname", "Doe" }, { "age", 50 }, { "birthday", new DateTime(1964, 1, 1) } } }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 15 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "import_to" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][email]" && (string)p.Value == "aaa@aaa.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][firstname]" && (string)p.Value == "Bob" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][lastname]" && (string)p.Value == "Smith" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][age]" && (int)p.Value == 41 && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][birthday]" && (string)p.Value == "1973-01-01 00:00:00" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][email]" && (string)p.Value == "bbb@bbb.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][firstname]" && (string)p.Value == "Jane" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][lastname]" && (string)p.Value == "Doe" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][age]" && (int)p.Value == 50 && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][birthday]" && (string)p.Value == "1964-01-01 00:00:00" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aa@aa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Import(USER_KEY, listId, listMembers, autoResponders: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void Import_with_clientid()
		{
			// Arrange
			var listId = 123;
			var listMembers = new[]
			{
				new ListMember() { Email = "aaa@aaa.com" },
				new ListMember() { Email = "bbb@bbb.com" },
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 8 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "import_to" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][email]" && (string)p.Value == "aaa@aaa.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][email]" && (string)p.Value == "bbb@bbb.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aa@aa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Import(USER_KEY, listId, listMembers, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void Import_zero_subscribers()
		{
			// Arrange
			var listId = 123;
			var listMembers = (ListMember[])null;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "import_to" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aa@aa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Import(USER_KEY, listId, listMembers);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void Unsubscribe_by_email()
		{
			// Arrange
			var listId = 123;
			var email = "test@test.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Unsubscribe(USER_KEY, listId, email, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void Unsubscribe_by_email_with_clientid()
		{
			// Arrange
			var listId = 123;
			var email = "test@test.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Unsubscribe(USER_KEY, listId, email, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void Unsubscribe_by_memberid()
		{
			// Arrange
			var listId = 123;
			var memberId = 555;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (int)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Unsubscribe(USER_KEY, listId, memberId, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void Unsubscribe_by_memberid_with_clientid()
		{
			// Arrange
			var listId = 123;
			var memberId = 555;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (int)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Unsubscribe(USER_KEY, listId, memberId, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void DeleteListMember()
		{
			// Arrange
			var listId = 123;
			var memberId = 555;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (int)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteListMember(USER_KEY, listId, memberId, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void DeleteListMember_with_clientid()
		{
			// Arrange
			var listId = 123;
			var memberId = 555;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (int)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteListMember(USER_KEY, listId, memberId, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetListMember()
		{
			// Arrange
			var listId = 123;
			var memberId = 555;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (int)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}}}}", memberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListMember(USER_KEY, listId, memberId, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(memberId, result.Id);
		}

		[TestMethod]
		public void GetListMember_with_clientid()
		{
			// Arrange
			var listId = 123;
			var memberId = 555;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (int)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}}}}", memberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListMember(USER_KEY, listId, memberId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(memberId, result.Id);
		}

		[TestMethod]
		public void GetListMembers_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;

			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"members\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListMembers(USER_KEY, listId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetListMembers_with_status()
		{
			// Arrange
			var listId = 123;
			var status = "active";

			var jsonMember1 = string.Format("{{\"id\":\"1\",\"status\":\"{0}\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}}", status);
			var jsonMember2 = string.Format("{{\"id\":\"2\",\"status\":\"{0}\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}}", status);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"members\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListMembers(USER_KEY, listId, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetListMembers_with_query()
		{
			// Arrange
			var listId = 123;
			var query = "(... this is a bogus query ...)";

			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "query" && (string)p.Value == query && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"members\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListMembers(USER_KEY, listId, query: query);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetListMembers_with_sortby()
		{
			// Arrange
			var listId = 123;
			var sortBy = "email";

			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sort_by" && (string)p.Value == sortBy && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"members\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListMembers(USER_KEY, listId, sortBy: sortBy);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetListMembers_with_sortdirection()
		{
			// Arrange
			var listId = 123;
			var sortDirection = "asc";

			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "direction" && (string)p.Value == sortDirection && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"members\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListMembers(USER_KEY, listId, sortDirection: sortDirection);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetListMembers_with_limit()
		{
			// Arrange
			var listId = 123;
			var limit = 5;

			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"members\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListMembers(USER_KEY, listId, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetListMembers_with_offset()
		{
			// Arrange
			var listId = 123;
			var offset = 25;

			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"members\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListMembers(USER_KEY, listId, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetListMembers_with_clientid()
		{
			// Arrange
			var listId = 123;

			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"members\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListMembers(USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetListMembersCount_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
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
			var result = apiClient.GetListMembersCount(USER_KEY, listId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetListMembersCount_with_status()
		{
			// Arrange
			var listId = 123;
			var status = "active";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.GetListMembersCount(USER_KEY, listId, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetListMembersCount_with_clientid()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.GetListMembersCount(USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void UpdateMember_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;
			var memberId = 456;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (int)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateListMember(USER_KEY, listId, memberId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMember_with_customfields()
		{
			// Arrange
			var listId = 123;
			var memberId = 456;
			var customFields = new[]
			{
				new KeyValuePair<string, object>("fullname", "Bob Smith"),
				new KeyValuePair<string, object>("birthday", new DateTime(1973, 1, 1))
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (int)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "data[fullname]" && (string)p.Value == "Bob Smith" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "data[birthday]" && (string)p.Value == "1973-01-01 00:00:00" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateListMember(USER_KEY, listId, memberId, customFields: customFields);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateMember_with_clientid()
		{
			// Arrange
			var listId = 123;
			var memberId = 456;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (int)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateListMember(USER_KEY, listId, memberId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetListLogs_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListLogs(USER_KEY, listId, uniques: false, totals: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetListLogs_with_logtype()
		{
			// Arrange
			var listId = 123;
			var logType = "subscribe";

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == logType && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0}]}}}}", subscribeLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListLogs(USER_KEY, listId, uniques: false, totals: false, logType: logType);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public void GetListLogs_with_startdate()
		{
			// Arrange
			var listId = 123;
			var start = new DateTime(2015, 1, 1);

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "start_time" && (string)p.Value == "2015-01-01 00:00:00" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListLogs(USER_KEY, listId, uniques: false, totals: false, start: start);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetListLogs_with_enddate()
		{
			// Arrange
			var listId = 123;
			var end = new DateTime(2015, 12, 31);

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "end_time" && (string)p.Value == "2015-12-31 00:00:00" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListLogs(USER_KEY, listId, uniques: false, totals: false, end: end);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetListLogs_with_limit()
		{
			// Arrange
			var listId = 123;
			var limit = 5;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListLogs(USER_KEY, listId, uniques: false, totals: false, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetListLogs_with_offset()
		{
			// Arrange
			var listId = 123;
			var offset = 25;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListLogs(USER_KEY, listId, uniques: false, totals: false, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetListLogs_with_clientid()
		{
			// Arrange
			var listId = 123;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListLogs(USER_KEY, listId, uniques: false, totals: false, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetListLogs_with_uniques_true()
		{
			// Arrange
			var listId = 123;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListLogs(USER_KEY, listId, uniques: true, totals: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetListLogs_with_totals_true()
		{
			// Arrange
			var listId = 123;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetListLogs(USER_KEY, listId, uniques: false, totals: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetListLogsCount_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.GetListLogsCount(USER_KEY, listId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetListLogsCount_with_logtype()
		{
			// Arrange
			var listId = 123;
			var logType = "clickthru";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.GetListLogsCount(USER_KEY, listId, logType: logType);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetListLogsCount_with_startdate()
		{
			// Arrange
			var listId = 123;
			var start = new DateTime(2015, 1, 1);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.GetListLogsCount(USER_KEY, listId, uniques: false, totals: false, start: start);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetListLogsCount_with_enddate()
		{
			// Arrange
			var listId = 123;
			var end = new DateTime(2015, 12, 31);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "end_time" && (string)p.Value == "2015-12-31 00:00:00" && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.GetListLogsCount(USER_KEY, listId, uniques: false, totals: false, end: end);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetListLogsCount_with_clientid()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.GetListLogsCount(USER_KEY, listId, uniques: false, totals: false, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetListLogsCount_with_uniques_true()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.GetListLogsCount(USER_KEY, listId, uniques: true, totals: false);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetListLogsCount_with_totals_true()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.GetListLogsCount(USER_KEY, listId, uniques: false, totals: true);

			// Assert
			Assert.AreEqual(2, result);
		}
	}
}
