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
	public class MailingsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[TestMethod]
		public async Task CreateMailing_with_minimal_parameters()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "type", Value = MailingType.Standard.GetEnumMemberValue() }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockRestClient = new MockRestClient("/Mailing/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public async Task CreateMailing_without_type()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockRestClient = new MockRestClient("/Mailing/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name, type: null);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public async Task CreateMailing_with_campaignid()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;
			var campaignId = 111L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "type", Value = MailingType.Standard.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "campaign_id", Value = campaignId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockRestClient = new MockRestClient("/Mailing/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name, campaignId: campaignId);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public async Task CreateMailing_with_type()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;
			var type = MailingType.Recurring;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "type", Value = type.GetEnumMemberValue() }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockRestClient = new MockRestClient("/Mailing/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name, type: type);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public async Task CreateMailing_with_recurringid()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;
			var recurringId = 222L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "type", Value = MailingType.Standard.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "recurring_id", Value = recurringId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockRestClient = new MockRestClient("/Mailing/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name, recurringId: recurringId);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public async Task CreateMailing_with_encoding()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;
			var encoding = MessageEncoding.Iso8859;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "type", Value = MailingType.Standard.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "encoding", Value = encoding.GetEnumMemberValue() }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockRestClient = new MockRestClient("/Mailing/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name, encoding: encoding);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public async Task CreateMailing_with_trasnferencoding()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;
			var transferEncoding = TransferEncoding.Base64;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "type", Value = MailingType.Standard.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "transfer_encoding", Value = transferEncoding.GetEnumMemberValue() }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockRestClient = new MockRestClient("/Mailing/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name, transferEncoding: transferEncoding);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public async Task CreateMailing_with_clientid()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "type", Value = MailingType.Standard.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockRestClient = new MockRestClient("/Mailing/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(mailingId, result);
		}

		[TestMethod]
		public async Task DeleteMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/Delete/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.DeleteAsync(USER_KEY, mailingId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/Delete/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.DeleteAsync(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"id\":\"{0}\",\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}}}}", mailingId);
			var mockRestClient = new MockRestClient("/Mailing/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetAsync(USER_KEY, mailingId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(mailingId, result.Id);
		}

		[TestMethod]
		public async Task GetMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"id\":\"{0}\",\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}}}}", mailingId);
			var mockRestClient = new MockRestClient("/Mailing/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetAsync(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(mailingId, result.Id);
		}

		[TestMethod]
		public async Task GetMailings_with_minimal_parameters()
		{
			// Arrange
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetMailings_with_status()
		{
			// Arrange
			var status = MailingStatus.Incomplete;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetMailings_with_type()
		{
			// Arrange
			var type = MailingType.Standard;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "type", Value = type.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, type: type);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetMailings_with_name()
		{
			// Arrange
			var name = "My mailing";
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, name: name);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetMailings_with_listid()
		{
			// Arrange
			var listId = 111L;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, listId: listId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetMailings_with_campaignid()
		{
			// Arrange
			var campaignId = 111L;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "campaign_id", Value = campaignId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, campaignId: campaignId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetMailings_with_recurringid()
		{
			// Arrange
			var recurringId = 111L;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "recurring_id", Value = recurringId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, recurringId: recurringId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetMailings_with_start()
		{
			// Arrange
			var start = new DateTime(2015, 1, 1);
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "start_date", Value = start.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, start: start);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetMailings_with_end()
		{
			// Arrange
			var end = new DateTime(2015, 12, 31, 23, 59, 59);
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "end_date", Value = end.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, end: end);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetMailings_with_sortby()
		{
			// Arrange
			var sortBy = MailingsSortBy.CreatedOn;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "sort_by", Value = sortBy.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, sortBy: sortBy);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetMailings_with_sortdirection()
		{
			// Arrange
			var sortDirection = SortDirection.Descending;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "direction", Value = sortDirection.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, sortDirection: sortDirection);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetMailings_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetMailings_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetMailings_with_clientid()
		{
			// Arrange
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetMailingsCount_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingsCount_with_status()
		{
			// Arrange
			var status = MailingStatus.Scheduled;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, status: status);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingsCount_with_type()
		{
			// Arrange
			var type = MailingType.Standard;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "type", Value = type.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, type: type);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingsCount_with_name()
		{
			// Arrange
			var name = "My mailing";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, name: name);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingsCount_with_listid()
		{
			// Arrange
			var listId = 111L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, listId: listId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingsCount_with_campaignid()
		{
			// Arrange
			var campaignId = 111L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "campaign_id", Value = campaignId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, campaignId: campaignId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingsCount_with_recurringid()
		{
			// Arrange
			var recurringId = 111L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "recurring_id", Value = recurringId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, recurringId: recurringId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingsCount_with_start()
		{
			// Arrange
			var start = new DateTime(2015, 1, 1);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "start_date", Value = start.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, start: start);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingsCount_with_end()
		{
			// Arrange
			var end = new DateTime(2015, 12, 31, 23, 59, 59);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "end_date", Value = end.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, end: end);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingsCount_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_campaignid()
		{
			// Arrange
			var mailingId = 123L;
			var campaignId = 111L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "campaign_id", Value = campaignId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, campaignId: campaignId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_listid()
		{
			// Arrange
			var mailingId = 123L;
			var listId = 111L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, listId: listId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_sublistid()
		{
			// Arrange
			var mailingId = 123L;
			var sublistId = 111L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sublist_id", Value = sublistId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, sublistId: sublistId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_name()
		{
			// Arrange
			var mailingId = 123L;
			var name = "My mailing";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, name: name);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_type()
		{
			// Arrange
			var mailingId = 123L;
			var type = MailingType.Standard;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "type", Value = type.GetEnumMemberValue() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, type: type);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_encoding()
		{
			// Arrange
			var mailingId = 123L;
			var encoding = MessageEncoding.Utf8;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "encoding", Value = encoding.GetEnumMemberValue() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, encoding: encoding);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_transferencoding()
		{
			// Arrange
			var mailingId = 123L;
			var transferEncoding = TransferEncoding.Base64;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "transfer_encoding", Value = transferEncoding.GetEnumMemberValue() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, transferEncoding: transferEncoding);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_subject()
		{
			// Arrange
			var mailingId = 123L;
			var subject = "My mailing subject";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "subject", Value = subject }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, subject: subject);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_senderemail()
		{
			// Arrange
			var mailingId = 123L;
			var senderEmail = "bobsmith@fictitiouscompany.com";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = senderEmail }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, senderEmail: senderEmail);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_sendername()
		{
			// Arrange
			var mailingId = 123L;
			var senderName = "Bob Smith";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_name", Value = senderName }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, senderName: senderName);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_replyto()
		{
			// Arrange
			var mailingId = 123L;
			var replyTo = "marketing@fictitiouscompany.com";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "reply_to", Value = replyTo }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, replyTo: replyTo);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_htmlcontent()
		{
			// Arrange
			var mailingId = 123L;
			var htmlContent = "<html><body>Hello world</body></html>";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "html_message", Value = htmlContent }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, htmlContent: htmlContent);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_textcontent()
		{
			// Arrange
			var mailingId = 123L;
			var textContent = "Hello world";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "text_message", Value = textContent }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, textContent: textContent);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_trackopens_true()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "opening_stats", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, trackOpens: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_trackopens_false()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "opening_stats", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, trackOpens: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_trackclicksinhtml_true()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "clickthru_html", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, trackClicksInHtml: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_trackclicksinhtml_false()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "clickthru_html", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, trackClicksInHtml: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_trackclicksintext_true()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "clickthru_text", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, trackClicksInText: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_trackclicksintext_false()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "clickthru_text", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, trackClicksInText: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_trackingparameters()
		{
			// Arrange
			var mailingId = 123L;
			var trackingParameters = "param1=abc&param2=123";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "tracking_params", Value = trackingParameters }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, trackingParameters: trackingParameters);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_endingon()
		{
			// Arrange
			var mailingId = 123L;
			var endingOn = new DateTime(2015, 7, 10);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "ending_on", Value = endingOn.ToCakeMailString() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, endingOn: endingOn);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_maxrecurrences()
		{
			// Arrange
			var mailingId = 123L;
			var maxRecurrences = 99;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "max_recurrences", Value = maxRecurrences }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, maxRecurrences: maxRecurrences);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_recurringconditions()
		{
			// Arrange
			var mailingId = 123L;
			var recurringConditions = "???";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "recurring_conditions", Value = recurringConditions }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, recurringConditions: recurringConditions);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMailing_with_clientid()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SendMailingTestEmail_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "test_type", Value = "merged" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "test_email", Value = recipientEmail }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SendTestEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.SendTestEmailAsync(USER_KEY, mailingId, recipientEmail);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SendMailingTestEmail_with_separated_true()
		{
			// Arrange
			var mailingId = 123L;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "test_type", Value = "separated" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "test_email", Value = recipientEmail }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SendTestEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.SendTestEmailAsync(USER_KEY, mailingId, recipientEmail, separated: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SendMailingTestEmail_with_clientid()
		{
			// Arrange
			var mailingId = 123L;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "test_type", Value = "merged" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "test_email", Value = recipientEmail },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/SendTestEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.SendTestEmailAsync(USER_KEY, mailingId, recipientEmail, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetMailingRawEmailMessage_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"subject\":\"This is a simple message\",\"message\":\"...dummy content...\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetEmailMessage/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetRawEmailMessageAsync(USER_KEY, mailingId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("This is a simple message", result.Subject);
		}

		[TestMethod]
		public async Task GetMailingRawEmailMessage_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"subject\":\"This is a simple message\",\"message\":\"...dummy content...\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetEmailMessage/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetRawEmailMessageAsync(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("This is a simple message", result.Subject);
		}

		[TestMethod]
		public async Task GetMailingRawHtml_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"<html><body>...dummy content...</body></html>\"}";
			var mockRestClient = new MockRestClient("/Mailing/GetHtmlMessage/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetRawHtmlAsync(USER_KEY, mailingId);

			// Assert
			Assert.AreEqual("<html><body>...dummy content...</body></html>", result);
		}

		[TestMethod]
		public async Task GetMailingRawHtml_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"<html><body>...dummy content...</body></html>\"}";
			var mockRestClient = new MockRestClient("/Mailing/GetHtmlMessage/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetRawHtmlAsync(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			Assert.AreEqual("<html><body>...dummy content...</body></html>", result);
		}

		[TestMethod]
		public async Task GetMailingRawText_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"...dummy content...\"}";
			var mockRestClient = new MockRestClient("/Mailing/GetTextMessage/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetRawTextAsync(USER_KEY, mailingId);

			// Assert
			Assert.AreEqual("...dummy content...", result);
		}

		[TestMethod]
		public async Task GetMailingRawText_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"...dummy content...\"}";
			var mockRestClient = new MockRestClient("/Mailing/GetTextMessage/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetRawTextAsync(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			Assert.AreEqual("...dummy content...", result);
		}

		[TestMethod]
		public async Task ScheduleMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/Schedule/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.ScheduleAsync(USER_KEY, mailingId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task ScheduleMailing_with_date()
		{
			// Arrange
			var mailingId = 12345L;
			var date = new DateTime(2015, 4, 3, 17, 0, 0);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "date", Value = date.ToCakeMailString() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/Schedule/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.ScheduleAsync(USER_KEY, mailingId, date: date);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task ScheduleMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/Schedule/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.ScheduleAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UnscheduleMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/Unschedule/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UnscheduleAsync(USER_KEY, mailingId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UnscheduleMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/Unschedule/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.UnscheduleAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SuspendMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/Suspend/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.SuspendAsync(USER_KEY, mailingId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SuspendMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/Suspend/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.SuspendAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task ResumeMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/Resume/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.ResumeAsync(USER_KEY, mailingId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task ResumeMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Mailing/Resume/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.ResumeAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetMailingLogs_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLogs_with_logtype()
		{
			// Arrange
			var mailingId = 123L;
			var logType = LogType.Sent;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "action", Value = logType.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0}]}}}}", sentLog);
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, logType: logType);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLogs_with_listmemberid()
		{
			// Arrange
			var mailingId = 123L;
			var listMemberId = 111L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = listMemberId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0}]}}}}", sentLog);
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, listMemberId: listMemberId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLogs_with_startdate()
		{
			// Arrange
			var mailingId = 123L;
			var start = new DateTime(2015, 1, 1);

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "start_time", Value = start.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, start: start);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLogs_with_enddate()
		{
			// Arrange
			var mailingId = 123L;
			var end = new DateTime(2015, 12, 31);

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "end_time", Value = end.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, uniques: false, totals: false, end: end);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLogs_with_limit()
		{
			// Arrange
			var mailingId = 123L;
			var limit = 5;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, uniques: false, totals: false, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLogs_with_offset()
		{
			// Arrange
			var mailingId = 123L;
			var offset = 25;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, uniques: false, totals: false, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLogs_with_clientid()
		{
			// Arrange
			var mailingId = 123L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, uniques: false, totals: false, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLogs_with_uniques_true()
		{
			// Arrange
			var mailingId = 123L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, uniques: true, totals: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLogs_with_totals_true()
		{
			// Arrange
			var mailingId = 123L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, uniques: false, totals: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLogsCount_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingLogsCount_with_logtype()
		{
			// Arrange
			var mailingId = 123L;
			var logType = LogType.Sent;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "action", Value = logType.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId, logType: logType);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingLogsCount_with_listmemberid()
		{
			// Arrange
			var mailingId = 123L;
			var listMemberId = 111L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = listMemberId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId, listMemberId: listMemberId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingLogsCount_with_uniques_true()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId, uniques: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingLogsCount_with_totals_true()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId, totals: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingLogsCount_with_startdate()
		{
			// Arrange
			var mailingId = 123L;
			var start = new DateTime(2015, 1, 1, 0, 0, 0);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "start_time", Value = start.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId, start: start);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingLogsCount_with_enddate()
		{
			// Arrange
			var mailingId = 123L;
			var end = new DateTime(2015, 12, 31, 23, 59, 59);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "end_time", Value = end.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId, end: end);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingLogsCount_with_clientid()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingLinks_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockRestClient = new MockRestClient("/Mailing/GetLinks/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksAsync(USER_KEY, mailingId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLinks_with_limit()
		{
			// Arrange
			var mailingId = 123L;
			var limit = 5;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockRestClient = new MockRestClient("/Mailing/GetLinks/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksAsync(USER_KEY, mailingId, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLinks_with_offset()
		{
			// Arrange
			var mailingId = 123L;
			var offset = 25;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockRestClient = new MockRestClient("/Mailing/GetLinks/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksAsync(USER_KEY, mailingId, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLinks_with_clientid()
		{
			// Arrange
			var mailingId = 123L;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockRestClient = new MockRestClient("/Mailing/GetLinks/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLinksCount_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLinks/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksCountAsync(USER_KEY, mailingId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingLinksCount_with_clientid()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLinks/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksCountAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetMailingLink_with_minimal_parameters()
		{
			// Arrange
			var linkId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "link_id", Value = linkId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}}}}", linkId);
			var mockRestClient = new MockRestClient("/Mailing/GetLinkInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinkAsync(USER_KEY, linkId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(linkId, result.Id);
		}

		[TestMethod]
		public async Task GetMailingLink_with_clientid()
		{
			// Arrange
			var linkId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "link_id", Value = linkId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}}}}", linkId);
			var mockRestClient = new MockRestClient("/Mailing/GetLinkInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinkAsync(USER_KEY, linkId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(linkId, result.Id);
		}

		[TestMethod]
		public async Task GetMailingLinksWithStats_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;

			var jsonLink1 = string.Format("{{\"id\":\"111111\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\",\"mailing_id\":\"{0}\",\"unique\":\"3\",\"total\":\"10\",\"unique_rate\":\"2\",\"total_rate\":\"1\"}}", mailingId);
			var jsonLink2 = string.Format("{{\"id\":\"222222\",\"link_to\":\"http://www.fictitiouscompany.com.com/\",\"mailing_id\":\"{0}\",\"unique\":\"1\",\"total\":\"2\",\"unique_rate\":\"1\",\"total_rate\":\"2\"}}", mailingId);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockRestClient = new MockRestClient("/Mailing/GetLinksLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksWithStatsAsync(USER_KEY, mailingId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLinksWithStats_with_start()
		{
			// Arrange
			var mailingId = 123L;
			var start = new DateTime(2015, 1, 1);

			var jsonLink1 = string.Format("{{\"id\":\"111111\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\",\"mailing_id\":\"{0}\",\"unique\":\"3\",\"total\":\"10\",\"unique_rate\":\"2\",\"total_rate\":\"1\"}}", mailingId);
			var jsonLink2 = string.Format("{{\"id\":\"222222\",\"link_to\":\"http://www.fictitiouscompany.com.com/\",\"mailing_id\":\"{0}\",\"unique\":\"1\",\"total\":\"2\",\"unique_rate\":\"1\",\"total_rate\":\"2\"}}", mailingId);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "start_time", Value = start.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockRestClient = new MockRestClient("/Mailing/GetLinksLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksWithStatsAsync(USER_KEY, mailingId, start: start);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLinksWithStats_with_end()
		{
			// Arrange
			var mailingId = 123L;
			var end = new DateTime(2015, 12, 31);

			var jsonLink1 = string.Format("{{\"id\":\"111111\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\",\"mailing_id\":\"{0}\",\"unique\":\"3\",\"total\":\"10\",\"unique_rate\":\"2\",\"total_rate\":\"1\"}}", mailingId);
			var jsonLink2 = string.Format("{{\"id\":\"222222\",\"link_to\":\"http://www.fictitiouscompany.com.com/\",\"mailing_id\":\"{0}\",\"unique\":\"1\",\"total\":\"2\",\"unique_rate\":\"1\",\"total_rate\":\"2\"}}", mailingId);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "end_time", Value = end.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockRestClient = new MockRestClient("/Mailing/GetLinksLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksWithStatsAsync(USER_KEY, mailingId, end: end);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLinksWithStats_with_limit()
		{
			// Arrange
			var mailingId = 123L;
			var limit = 5;

			var jsonLink1 = string.Format("{{\"id\":\"111111\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\",\"mailing_id\":\"{0}\",\"unique\":\"3\",\"total\":\"10\",\"unique_rate\":\"2\",\"total_rate\":\"1\"}}", mailingId);
			var jsonLink2 = string.Format("{{\"id\":\"222222\",\"link_to\":\"http://www.fictitiouscompany.com.com/\",\"mailing_id\":\"{0}\",\"unique\":\"1\",\"total\":\"2\",\"unique_rate\":\"1\",\"total_rate\":\"2\"}}", mailingId);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockRestClient = new MockRestClient("/Mailing/GetLinksLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksWithStatsAsync(USER_KEY, mailingId, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLinksWithStats_with_offset()
		{
			// Arrange
			var mailingId = 123L;
			var offset = 25;

			var jsonLink1 = string.Format("{{\"id\":\"111111\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\",\"mailing_id\":\"{0}\",\"unique\":\"3\",\"total\":\"10\",\"unique_rate\":\"2\",\"total_rate\":\"1\"}}", mailingId);
			var jsonLink2 = string.Format("{{\"id\":\"222222\",\"link_to\":\"http://www.fictitiouscompany.com.com/\",\"mailing_id\":\"{0}\",\"unique\":\"1\",\"total\":\"2\",\"unique_rate\":\"1\",\"total_rate\":\"2\"}}", mailingId);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockRestClient = new MockRestClient("/Mailing/GetLinksLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksWithStatsAsync(USER_KEY, mailingId, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLinksWithStats_with_clientid()
		{
			// Arrange
			var mailingId = 123L;

			var jsonLink1 = string.Format("{{\"id\":\"111111\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\",\"mailing_id\":\"{0}\",\"unique\":\"3\",\"total\":\"10\",\"unique_rate\":\"2\",\"total_rate\":\"1\"}}", mailingId);
			var jsonLink2 = string.Format("{{\"id\":\"222222\",\"link_to\":\"http://www.fictitiouscompany.com.com/\",\"mailing_id\":\"{0}\",\"unique\":\"1\",\"total\":\"2\",\"unique_rate\":\"1\",\"total_rate\":\"2\"}}", mailingId);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockRestClient = new MockRestClient("/Mailing/GetLinksLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksWithStatsAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetMailingLinksWithStatsCount_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"links\":[],\"count\":\"3\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLinksLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksWithStatsCountAsync(USER_KEY, mailingId);

			// Assert
			Assert.AreEqual(3, result);
		}

		[TestMethod]
		public async Task GetMailingLinksWithStatsCount_with_start()
		{
			// Arrange
			var mailingId = 123L;
			var start = new DateTime(2015, 1, 1);

			var jsonLink1 = string.Format("{{\"id\":\"111111\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\",\"mailing_id\":\"{0}\",\"unique\":\"3\",\"total\":\"10\",\"unique_rate\":\"2\",\"total_rate\":\"1\"}}", mailingId);
			var jsonLink2 = string.Format("{{\"id\":\"222222\",\"link_to\":\"http://www.fictitiouscompany.com.com/\",\"mailing_id\":\"{0}\",\"unique\":\"1\",\"total\":\"2\",\"unique_rate\":\"1\",\"total_rate\":\"2\"}}", mailingId);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "start_time", Value = start.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"links\":[],\"count\":\"3\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLinksLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksWithStatsCountAsync(USER_KEY, mailingId, start: start);

			// Assert
			Assert.AreEqual(3, result);
		}

		[TestMethod]
		public async Task GetMailingLinksWithStatsCount_with_end()
		{
			// Arrange
			var mailingId = 123L;
			var end = new DateTime(2015, 12, 31);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "end_time", Value = end.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"links\":[],\"count\":\"3\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLinksLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksWithStatsCountAsync(USER_KEY, mailingId, end: end);

			// Assert
			Assert.AreEqual(3, result);
		}

		[TestMethod]
		public async Task GetMailingLinksWithStatsCount_with_limit()
		{
			// Arrange
			var mailingId = 123L;
			var limit = 5;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"links\":[],\"count\":\"3\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLinksLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksWithStatsCountAsync(USER_KEY, mailingId, limit: limit);

			// Assert
			Assert.AreEqual(3, result);
		}

		[TestMethod]
		public async Task GetMailingLinksWithStatsCount_with_offset()
		{
			// Arrange
			var mailingId = 123L;
			var offset = 25;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"links\":[],\"count\":\"3\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLinksLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksWithStatsCountAsync(USER_KEY, mailingId, offset: offset);

			// Assert
			Assert.AreEqual(3, result);
		}

		[TestMethod]
		public async Task GetMailingLinksWithStatsCount_with_clientid()
		{
			// Arrange
			var mailingId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "mailing_id", Value = mailingId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"links\":[],\"count\":\"3\"}}";
			var mockRestClient = new MockRestClient("/Mailing/GetLinksLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Mailings.GetLinksWithStatsCountAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(3, result);
		}
	}
}
