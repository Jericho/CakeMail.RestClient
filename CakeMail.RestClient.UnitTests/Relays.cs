using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class RelaysTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;
		private const long TRACKING_ID = 123;
		private const string RECIPIENT_EMAIL = "bobsmith@fictitiouscompany.com";
		private const string SENDER_EMAIL = "marketing@marketingcompany.com";
		private const string SENDER_NAME = "The Marketing Group";
		private const string HTML_CONTENT = "<HTML><body>Hello World</body></HTML>";
		private const string TEXT_CONTENT = "Hello Wolrd";
		private const string SUBJECT = "Hello!";

		[TestMethod]
		public async Task SendRelay_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = RECIPIENT_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "subject", Value = SUBJECT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "html_message", Value = HTML_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "text_message", Value = TEXT_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = SENDER_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_opening", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_html", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_text", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Relay/Send/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.SendWithoutTrackingAsync(USER_KEY, RECIPIENT_EMAIL, SUBJECT, HTML_CONTENT, TEXT_CONTENT, SENDER_EMAIL);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SendRelay_with_SENDER_NAME()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = RECIPIENT_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "subject", Value = SUBJECT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "html_message", Value = HTML_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "text_message", Value = TEXT_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = SENDER_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_name", Value = SENDER_NAME },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_opening", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_html", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_text", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Relay/Send/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.SendWithoutTrackingAsync(USER_KEY, RECIPIENT_EMAIL, SUBJECT, HTML_CONTENT, TEXT_CONTENT, SENDER_EMAIL, SENDER_NAME);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SendRelay_with_encoding()
		{
			// Arrange
			var encoding = MessageEncoding.Utf8;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = RECIPIENT_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "subject", Value = SUBJECT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "html_message", Value = HTML_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "text_message", Value = TEXT_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = SENDER_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "encoding", Value = encoding.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_opening", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_html", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_text", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Relay/Send/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.SendWithoutTrackingAsync(USER_KEY, RECIPIENT_EMAIL, SUBJECT, HTML_CONTENT, TEXT_CONTENT, SENDER_EMAIL, encoding: encoding);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SendRelay_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = RECIPIENT_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "subject", Value = SUBJECT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "html_message", Value = HTML_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "text_message", Value = TEXT_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = SENDER_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_opening", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_html", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_text", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Relay/Send/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.SendWithoutTrackingAsync(USER_KEY, RECIPIENT_EMAIL, SUBJECT, HTML_CONTENT, TEXT_CONTENT, SENDER_EMAIL, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SendTrackedRelay_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "tracking_id", Value = TRACKING_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = RECIPIENT_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "subject", Value = SUBJECT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "html_message", Value = HTML_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "text_message", Value = TEXT_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = SENDER_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_opening", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_html", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_text", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Relay/Send/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.SendWithTrackingAsync(USER_KEY, TRACKING_ID, RECIPIENT_EMAIL, SUBJECT, HTML_CONTENT, TEXT_CONTENT, SENDER_EMAIL);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SendTrackedRelay_with_SENDER_NAME()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "tracking_id", Value = TRACKING_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = RECIPIENT_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "subject", Value = SUBJECT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "html_message", Value = HTML_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "text_message", Value = TEXT_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = SENDER_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_name", Value = SENDER_NAME },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_opening", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_html", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_text", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Relay/Send/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.SendWithTrackingAsync(USER_KEY, TRACKING_ID, RECIPIENT_EMAIL, SUBJECT, HTML_CONTENT, TEXT_CONTENT, SENDER_EMAIL, SENDER_NAME);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SendTrackedRelay_with_encoding()
		{
			// Arrange
			var encoding = MessageEncoding.Utf8;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "tracking_id", Value = TRACKING_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = RECIPIENT_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "subject", Value = SUBJECT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "html_message", Value = HTML_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "text_message", Value = TEXT_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = SENDER_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "encoding", Value = encoding.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_opening", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_html", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_text", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Relay/Send/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.SendWithTrackingAsync(USER_KEY, TRACKING_ID, RECIPIENT_EMAIL, SUBJECT, HTML_CONTENT, TEXT_CONTENT, SENDER_EMAIL, encoding: encoding);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SendTrackedRelay_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "tracking_id", Value = TRACKING_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = RECIPIENT_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "subject", Value = SUBJECT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "html_message", Value = HTML_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "text_message", Value = TEXT_CONTENT },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = SENDER_EMAIL },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_opening", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_html", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "track_clicks_in_text", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Relay/Send/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.SendWithTrackingAsync(USER_KEY, TRACKING_ID, RECIPIENT_EMAIL, SUBJECT, HTML_CONTENT, TEXT_CONTENT, SENDER_EMAIL, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetRelaySentLogs_with_minimal_parameters()
		{
			// Arrange
			var logType = "sent";
			var jsonSentLog1 = "{\"email\":\"aaa@aaa.com\",\"relay_id\":\"88934439\",\"sent_id\":\"8398053\",\"time\":\"2015-04-06 08:02:10\",\"tracking_id\":\"1912196542\"}";
			var jsonSentLog2 = "{\"email\":\"bbb@bbb.com\",\"relay_id\":\"88934440\",\"sent_id\":\"8398054\",\"time\":\"2015-04-06 08:02:10\",\"tracking_id\":\"1289082963\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "log_type", Value = logType }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sent_logs\":[{0},{1}]}}}}", jsonSentLog1, jsonSentLog2);
			var mockRestClient = new MockRestClient("/Relay/GetLogs/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.GetSentLogsAsync(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetRelayOpenLogs_with_minimal_parameters()
		{
			// Arrange
			var logType = "open";
			var jsonOpenLog1 = "{\"email\":\"aaa@aaa.com\",\"host\":null,\"ip\":\"127.0.0.1\",\"relay_id\":\"88258598\",\"sent_id\":\"8280816\",\"time\":\"2015-04-06 00:06:17\",\"tracking_id\":\"722823822\",\"user_agent\":\"Mozilla/5.0 (Windows NT 5.1; rv:11.0) Gecko Firefox/11.0 (via ggpht.com GoogleImageProxy)\"}";
			var jsonOpenLog2 = "{\"email\":\"bbb@bbb.com\",\"host\":null,\"ip\":\"127.0.0.1\",\"relay_id\":\"88249402\",\"sent_id\":\"8271692\",\"time\":\"2015-04-06 00:13:41\",\"tracking_id\":\"1457837189\",\"user_agent\":\"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_2) AppleWebKit/600.3.18 (KHTML, like Gecko)\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "log_type", Value = logType }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"open_logs\":[{0},{1}]}}}}", jsonOpenLog1, jsonOpenLog2);
			var mockRestClient = new MockRestClient("/Relay/GetLogs/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.GetOpenLogsAsync(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetRelayClickLogs_with_minimal_parameters()
		{
			// Arrange
			var logType = "clickthru";
			var jsonClickLog1 = "{\"email\":\"aaa@aaa.com\",\"host\":null,\"ip\":\"127.0.0.1\",\"link_to\":\"http://www.fictitiouscompany.com\",\"relay_id\":\"88750676\",\"sent_id\":\"8356550\",\"time\":\"2015-04-06 00:17:52\",\"tracking_id\":\"1744041192\",\"user_agent\":\"Mozilla/5.0 (iPhone; CPU iPhone OS 7_1_2 like Mac OS X) AppleWebKit/537.51.2 (KHTML, like Gecko) Version/7.0 Mobile/11D257 Safari/9537.53\"}";
			var jsonClickLog2 = "{\"email\":\"bbb@bbb.com\",\"host\":null,\"ip\":\"127.0.0.1\",\"link_to\":\"http://www.fictitiouscompany.com\",\"relay_id\":\"86668371\",\"sent_id\":\"8013252\",\"time\":\"2015-04-06 00:47:14\",\"tracking_id\":\"1558835933\",\"user_agent\":\"Mozilla/5.0 (iPad; CPU OS 8_1_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12B440 Safari/600.1.4\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "log_type", Value = logType }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clickthru_logs\":[{0},{1}]}}}}", jsonClickLog1, jsonClickLog2);
			var mockRestClient = new MockRestClient("/Relay/GetLogs/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.GetClickLogsAsync(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetRelayBounceLogs_with_minimal_parameters()
		{
			// Arrange
			var logType = "bounce";
			var jsonBounceLog1 = "{\"bounce_type\":\"bounce_sb\",\"email\":\"aaa@aaa.com\",\"relay_id\":\"88935339\",\"sent_id\":\"0\",\"time\":\"2015-04-06 08:08:25\",\"tracking_id\":\"251364339\"}";
			var jsonBounceLog2 = "{\"bounce_type\":\"bounce_fm\",\"email\":\"bbb@bbb.com\",\"relay_id\":\"88240001\",\"sent_id\":\"0\",\"time\":\"2015-04-06 09:27:13\",\"tracking_id\":\"412676746\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "log_type", Value = logType }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"bounce_logs\":[{0},{1}]}}}}", jsonBounceLog1, jsonBounceLog2);
			var mockRestClient = new MockRestClient("/Relay/GetLogs/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.GetBounceLogsAsync(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetRelayLogs_with_TRACKING_ID()
		{
			// Arrange
			var logType = "sent";
			var jsonSentLog1 = "{\"email\":\"aaa@aaa.com\",\"relay_id\":\"88934439\",\"sent_id\":\"8398053\",\"time\":\"2015-04-06 08:02:10\",\"tracking_id\":\"1912196542\"}";
			var jsonSentLog2 = "{\"email\":\"bbb@bbb.com\",\"relay_id\":\"88934440\",\"sent_id\":\"8398054\",\"time\":\"2015-04-06 08:02:10\",\"tracking_id\":\"1289082963\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "tracking_id", Value = TRACKING_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "log_type", Value = logType }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sent_logs\":[{0},{1}]}}}}", jsonSentLog1, jsonSentLog2);
			var mockRestClient = new MockRestClient("/Relay/GetLogs/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.GetSentLogsAsync(USER_KEY, TRACKING_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetRelayLogs_with_startdate()
		{
			// Arrange
			var start = new DateTime(2015, 1, 1, 0, 0, 0);
			var logType = "sent";
			var jsonSentLog1 = "{\"email\":\"aaa@aaa.com\",\"relay_id\":\"88934439\",\"sent_id\":\"8398053\",\"time\":\"2015-04-06 08:02:10\",\"tracking_id\":\"1912196542\"}";
			var jsonSentLog2 = "{\"email\":\"bbb@bbb.com\",\"relay_id\":\"88934440\",\"sent_id\":\"8398054\",\"time\":\"2015-04-06 08:02:10\",\"tracking_id\":\"1289082963\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "start_time", Value = start.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "log_type", Value = logType }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sent_logs\":[{0},{1}]}}}}", jsonSentLog1, jsonSentLog2);
			var mockRestClient = new MockRestClient("/Relay/GetLogs/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.GetSentLogsAsync(USER_KEY, start: start);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetRelayLogs_with_enddate()
		{
			// Arrange
			var end = new DateTime(2015, 1, 1, 0, 0, 0);
			var logType = "sent";
			var jsonSentLog1 = "{\"email\":\"aaa@aaa.com\",\"relay_id\":\"88934439\",\"sent_id\":\"8398053\",\"time\":\"2015-04-06 08:02:10\",\"tracking_id\":\"1912196542\"}";
			var jsonSentLog2 = "{\"email\":\"bbb@bbb.com\",\"relay_id\":\"88934440\",\"sent_id\":\"8398054\",\"time\":\"2015-04-06 08:02:10\",\"tracking_id\":\"1289082963\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "end_time", Value = end.ToCakeMailString() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "log_type", Value = logType }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sent_logs\":[{0},{1}]}}}}", jsonSentLog1, jsonSentLog2);
			var mockRestClient = new MockRestClient("/Relay/GetLogs/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.GetSentLogsAsync(USER_KEY, end: end);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetRelayLogs_with_limit()
		{
			// Arrange
			var limit = 5;
			var logType = "sent";
			var jsonSentLog1 = "{\"email\":\"aaa@aaa.com\",\"relay_id\":\"88934439\",\"sent_id\":\"8398053\",\"time\":\"2015-04-06 08:02:10\",\"tracking_id\":\"1912196542\"}";
			var jsonSentLog2 = "{\"email\":\"bbb@bbb.com\",\"relay_id\":\"88934440\",\"sent_id\":\"8398054\",\"time\":\"2015-04-06 08:02:10\",\"tracking_id\":\"1289082963\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "log_type", Value = logType }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sent_logs\":[{0},{1}]}}}}", jsonSentLog1, jsonSentLog2);
			var mockRestClient = new MockRestClient("/Relay/GetLogs/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.GetSentLogsAsync(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetRelayLogs_with_offset()
		{
			// Arrange
			var offset = 25;
			var logType = "sent";
			var jsonSentLog1 = "{\"email\":\"aaa@aaa.com\",\"relay_id\":\"88934439\",\"sent_id\":\"8398053\",\"time\":\"2015-04-06 08:02:10\",\"tracking_id\":\"1912196542\"}";
			var jsonSentLog2 = "{\"email\":\"bbb@bbb.com\",\"relay_id\":\"88934440\",\"sent_id\":\"8398054\",\"time\":\"2015-04-06 08:02:10\",\"tracking_id\":\"1289082963\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "log_type", Value = logType }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sent_logs\":[{0},{1}]}}}}", jsonSentLog1, jsonSentLog2);
			var mockRestClient = new MockRestClient("/Relay/GetLogs/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.GetSentLogsAsync(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetRelayLogs_with_clientid()
		{
			// Arrange
			var logType = "sent";
			var jsonSentLog1 = "{\"email\":\"aaa@aaa.com\",\"relay_id\":\"88934439\",\"sent_id\":\"8398053\",\"time\":\"2015-04-06 08:02:10\",\"tracking_id\":\"1912196542\"}";
			var jsonSentLog2 = "{\"email\":\"bbb@bbb.com\",\"relay_id\":\"88934440\",\"sent_id\":\"8398054\",\"time\":\"2015-04-06 08:02:10\",\"tracking_id\":\"1289082963\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "log_type", Value = logType }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"sent_logs\":[{0},{1}]}}}}", jsonSentLog1, jsonSentLog2);
			var mockRestClient = new MockRestClient("/Relay/GetLogs/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Relays.GetSentLogsAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}
	}
}
