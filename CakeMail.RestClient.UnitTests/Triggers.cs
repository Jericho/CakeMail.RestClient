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
			var triggerId = 123L;
			var listId = 111L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", triggerId);
			var mockRestClient = new MockRestClient("/Trigger/Create/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var listId = 111L;
			var campaignId = 222L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "campaign_id", Value = campaignId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", triggerId);
			var mockRestClient = new MockRestClient("/Trigger/Create/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var listId = 111L;
			var encoding = MessageEncoding.Utf8;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "encoding", Value = encoding.GetEnumMemberValue() }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", triggerId);
			var mockRestClient = new MockRestClient("/Trigger/Create/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var listId = 111L;
			var transferEncoding = TransferEncoding.Base64;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "transfer_encoding", Value = transferEncoding.GetEnumMemberValue() }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", triggerId);
			var mockRestClient = new MockRestClient("/Trigger/Create/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var listId = 111L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", triggerId);
			var mockRestClient = new MockRestClient("/Trigger/Create/", parameters, jsonResponse);

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
			var triggerId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"action\":\"opt-in\",\"campaign_id\":\"0\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"{0}\",\"list_id\":\"111222333\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/8da92a42e5625ef43112c0ca4237902219935a88319a1349\",\"name\":\"this_is_a_test_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}}}}", triggerId);
			var mockRestClient = new MockRestClient("/Trigger/GetInfo/", parameters, jsonResponse);

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
			var triggerId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"action\":\"opt-in\",\"campaign_id\":\"0\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"{0}\",\"list_id\":\"111222333\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/8da92a42e5625ef43112c0ca4237902219935a88319a1349\",\"name\":\"this_is_a_test_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}}}}", triggerId);
			var mockRestClient = new MockRestClient("/Trigger/GetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var campaignId = 111L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "campaign_id", Value = campaignId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var name = "My mailing";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var action = TriggerAction.OptIn;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "action", Value = action.GetEnumMemberValue() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var encoding = MessageEncoding.Utf8;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "encoding", Value = encoding.GetEnumMemberValue() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var transferEncoding = TransferEncoding.Base64;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "transfer_encoding", Value = transferEncoding.GetEnumMemberValue() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var subject = "My mailing subject";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "subject", Value = subject }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var senderEmail = "bobsmith@fictitiouscompany.com";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = senderEmail }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var senderName = "Bob Smith";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_name", Value = senderName }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var replyTo = "marketing@fictitiouscompany.com";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "reply_to", Value = replyTo }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var htmlContent = "<html><body>Hello world</body></html>";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "html_message", Value = htmlContent }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var textContent = "Hello world";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "text_message", Value = textContent }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "opening_stats", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "opening_stats", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "clickthru_html", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "clickthru_html", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "clickthru_text", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "clickthru_text", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var trackingParameters = "param1=abc&param2=123";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "tracking_params", Value = trackingParameters }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var delay = 60;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "delay", Value = delay }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var status = TriggerStatus.Active;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var date = new DateTime(2015, 12, 1, 13, 6, 6);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "date_field", Value = date.ToCakeMailString() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SetInfo/", parameters, jsonResponse);

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

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2);
			var mockRestClient = new MockRestClient("/Trigger/GetList/", parameters, jsonResponse);

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

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2);
			var mockRestClient = new MockRestClient("/Trigger/GetList/", parameters, jsonResponse);

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

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "action", Value = action.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2);
			var mockRestClient = new MockRestClient("/Trigger/GetList/", parameters, jsonResponse);

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
			var listId = 111L;
			var jsonTrigger1 = "{\"action\":\"opt-in\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"123\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b99469dc805938e947457d91aa10c4c551\",\"name\":\"list_111_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}";
			var jsonTrigger2 = "{\"action\":\"opt-out\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"456\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b9c3ef15863200867f457d91aa10c4c551\",\"name\":\"list_111_opt_out\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Unsubscribe Confirmed.\",\"transfer_encoding\":\"quoted-printable\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2);
			var mockRestClient = new MockRestClient("/Trigger/GetList/", parameters, jsonResponse);

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
			var campaignId = 111L;
			var jsonTrigger1 = "{\"action\":\"opt-in\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"123\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b99469dc805938e947457d91aa10c4c551\",\"name\":\"list_111_opt_in\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Subscription Confirmed\",\"transfer_encoding\":\"quoted-printable\"}";
			var jsonTrigger2 = "{\"action\":\"opt-out\",\"campaign_id\":\"111\",\"delay\":\"0\",\"encoding\":\"utf-8\",\"id\":\"456\",\"list_id\":\"111\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/dff4a336984aa4b9c3ef15863200867f457d91aa10c4c551\",\"name\":\"list_111_opt_out\",\"parent_id\":\"0\",\"send_to\":\"[email]\",\"sender_email\":\"marketing@fictitiouscompany.com\",\"sender_name\":\"Marketing Group\",\"status\":\"active\",\"subject\":\"Unsubscribe Confirmed.\",\"transfer_encoding\":\"quoted-printable\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "campaign_id", Value = campaignId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2);
			var mockRestClient = new MockRestClient("/Trigger/GetList/", parameters, jsonResponse);

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

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2);
			var mockRestClient = new MockRestClient("/Trigger/GetList/", parameters, jsonResponse);

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

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2);
			var mockRestClient = new MockRestClient("/Trigger/GetList/", parameters, jsonResponse);

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

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"triggers\":[{0},{1}]}}}}", jsonTrigger1, jsonTrigger2);
			var mockRestClient = new MockRestClient("/Trigger/GetList/", parameters, jsonResponse);

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
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetList/", parameters, jsonResponse);

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

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetList/", parameters, jsonResponse);

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

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "action", Value = action.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetList/", parameters, jsonResponse);

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
			var listId = 111L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetList/", parameters, jsonResponse);

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
			var campaignId = 111L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "campaign_id", Value = campaignId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetList/", parameters, jsonResponse);

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
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetList/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "test_type", Value = "merged" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "test_email", Value = recipientEmail }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SendTestEmail/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "test_type", Value = "separated" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "test_email", Value = recipientEmail }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SendTestEmail/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "test_type", Value = "merged" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "test_email", Value = recipientEmail },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/SendTestEmail/", parameters, jsonResponse);

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
			var triggerId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"subject\":\"This is a simple message\",\"message\":\"...dummy content...\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetEmailMessage/", parameters, jsonResponse);

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
			var triggerId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"subject\":\"This is a simple message\",\"message\":\"...dummy content...\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetEmailMessage/", parameters, jsonResponse);

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
			var triggerId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"<html><body>...dummy content...</body></html>\"}";
			var mockRestClient = new MockRestClient("/Trigger/GetHtmlMessage/", parameters, jsonResponse);

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
			var triggerId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"<html><body>...dummy content...</body></html>\"}";
			var mockRestClient = new MockRestClient("/Trigger/GetHtmlMessage/", parameters, jsonResponse);

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
			var triggerId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"...dummy content...\"}";
			var mockRestClient = new MockRestClient("/Trigger/GetTextMessage/", parameters, jsonResponse);

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
			var triggerId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"...dummy content...\"}";
			var mockRestClient = new MockRestClient("/Trigger/GetTextMessage/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var listMemberId = 111L; 
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = listMemberId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/Unleash/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var listMemberId = 111L; 
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = listMemberId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Trigger/Unleash/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var logType = LogType.Sent;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "action", Value = logType.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0}]}}}}", sentLog);
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var listMemberId = 111L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = listMemberId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0}]}}}}", sentLog);
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var start = new DateTime(2015, 1, 1);

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "start_time", Value = start.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var end = new DateTime(2015, 12, 31);

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "end_time", Value = end.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var limit = 5;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var offset = 25;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var logType = LogType.Sent;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "action", Value = logType.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var listMemberId = 111L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = listMemberId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var start = new DateTime(2015, 1, 1, 0, 0, 0);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "start_time", Value = start.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var end = new DateTime(2015, 12, 31, 23, 59, 59);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "end_time", Value = end.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetLog/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockRestClient = new MockRestClient("/Trigger/GetLinks/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var limit = 5;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockRestClient = new MockRestClient("/Trigger/GetLinks/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var offset = 25;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockRestClient = new MockRestClient("/Trigger/GetLinks/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockRestClient = new MockRestClient("/Trigger/GetLinks/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetLinks/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetLinks/", parameters, jsonResponse);

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
			var linkId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "link_id", Value = linkId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}}}}", linkId);
			var mockRestClient = new MockRestClient("/Trigger/GetLinkInfo/", parameters, jsonResponse);

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
			var linkId = 12345L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "link_id", Value = linkId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}}}}", linkId);
			var mockRestClient = new MockRestClient("/Trigger/GetLinkInfo/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var jsonClickLog1 = "{\"id\":\"1111\",\"link_to\":\"http://www.fictitiouscompany.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";
			var jsonClickLog2 = "{\"id\":\"2222\",\"link_to\":\"http://www.cakemail.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonClickLog1, jsonClickLog2);
			var mockRestClient = new MockRestClient("/Trigger/GetLinksLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var start = new DateTime(2015, 1, 1);

			var jsonClickLog1 = "{\"id\":\"1111\",\"link_to\":\"http://www.fictitiouscompany.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";
			var jsonClickLog2 = "{\"id\":\"2222\",\"link_to\":\"http://www.cakemail.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "start_time", Value = start.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonClickLog1, jsonClickLog2);
			var mockRestClient = new MockRestClient("/Trigger/GetLinksLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var end = new DateTime(2015, 12, 31);

			var jsonClickLog1 = "{\"id\":\"1111\",\"link_to\":\"http://www.fictitiouscompany.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";
			var jsonClickLog2 = "{\"id\":\"2222\",\"link_to\":\"http://www.cakemail.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "end_time", Value = end.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonClickLog1, jsonClickLog2);
			var mockRestClient = new MockRestClient("/Trigger/GetLinksLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var limit = 5;

			var jsonClickLog1 = "{\"id\":\"1111\",\"link_to\":\"http://www.fictitiouscompany.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";
			var jsonClickLog2 = "{\"id\":\"2222\",\"link_to\":\"http://www.cakemail.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonClickLog1, jsonClickLog2);
			var mockRestClient = new MockRestClient("/Trigger/GetLinksLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var offset = 25;

			var jsonClickLog1 = "{\"id\":\"1111\",\"link_to\":\"http://www.fictitiouscompany.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";
			var jsonClickLog2 = "{\"id\":\"2222\",\"link_to\":\"http://www.cakemail.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonClickLog1, jsonClickLog2);
			var mockRestClient = new MockRestClient("/Trigger/GetLinksLog/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var jsonClickLog1 = "{\"id\":\"1111\",\"link_to\":\"http://www.fictitiouscompany.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";
			var jsonClickLog2 = "{\"id\":\"2222\",\"link_to\":\"http://www.cakemail.com\",\"unique\":\"1\",\"total\":\"5\",\"unique_rate\":\"1\",\"total_rate\":\"1\"}";

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonClickLog1, jsonClickLog2);
			var mockRestClient = new MockRestClient("/Trigger/GetLinksLog/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetLinksLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var start = new DateTime(2015, 1, 1, 0, 0, 0);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "start_time", Value = start.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetLinksLog/", parameters, jsonResponse);

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
			var triggerId = 123L;
			var end = new DateTime(2015, 12, 31, 23, 59, 59);

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "end_time", Value = end.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetLinksLog/", parameters, jsonResponse);

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
			var triggerId = 123L;

			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "trigger_id", Value = triggerId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/Trigger/GetLinksLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Triggers.GetLinksWithStatsCountAsync(USER_KEY, triggerId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}
	}
}
