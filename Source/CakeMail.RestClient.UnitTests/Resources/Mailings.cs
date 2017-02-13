using CakeMail.RestClient.Models;
using RichardSzalay.MockHttp;
using Shouldly;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Resources
{
	public class MailingsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[Fact]
		public async Task CreateMailing_with_minimal_parameters()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name);

			// Assert
			result.ShouldBe(mailingId);
		}

		[Fact]
		public async Task CreateMailing_without_type()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name, type: null);

			// Assert
			result.ShouldBe(mailingId);
		}

		[Fact]
		public async Task CreateMailing_with_campaignid()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;
			var campaignId = 111L;

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name, campaignId: campaignId);

			// Assert
			result.ShouldBe(mailingId);
		}

		[Fact]
		public async Task CreateMailing_with_type()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;
			var type = MailingType.Recurring;

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name, type: type);

			// Assert
			result.ShouldBe(mailingId);
		}

		[Fact]
		public async Task CreateMailing_with_recurringid()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;
			var recurringId = 222L;

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name, recurringId: recurringId);

			// Assert
			result.ShouldBe(mailingId);
		}

		[Fact]
		public async Task CreateMailing_with_encoding()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;
			var encoding = MessageEncoding.Iso8859;

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name, encoding: encoding);

			// Assert
			result.ShouldBe(mailingId);
		}

		[Fact]
		public async Task CreateMailing_with_trasnferencoding()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;
			var transferEncoding = TransferEncoding.Base64;

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name, transferEncoding: transferEncoding);

			// Assert
			result.ShouldBe(mailingId);
		}

		[Fact]
		public async Task CreateMailing_with_clientid()
		{
			// Arrange
			var name = "My new mailing";
			var mailingId = 123L;

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", mailingId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.CreateAsync(USER_KEY, name, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(mailingId);
		}

		[Fact]
		public async Task DeleteMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Delete")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.DeleteAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Delete")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.DeleteAsync(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"id\":\"{0}\",\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}}}}", mailingId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(mailingId);
		}

		[Fact]
		public async Task GetMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"id\":\"{0}\",\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}}}}", mailingId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetAsync(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(mailingId);
		}

		[Fact]
		public async Task GetMailings_with_minimal_parameters()
		{
			// Arrange
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetMailings_with_status()
		{
			// Arrange
			var status = MailingStatus.Incomplete;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, status: status);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetMailings_with_type()
		{
			// Arrange
			var type = MailingType.Standard;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, type: type);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetMailings_with_name()
		{
			// Arrange
			var name = "My mailing";
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, name: name);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetMailings_with_listid()
		{
			// Arrange
			var listId = 111L;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, listId: listId);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetMailings_with_campaignid()
		{
			// Arrange
			var campaignId = 111L;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, campaignId: campaignId);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetMailings_with_recurringid()
		{
			// Arrange
			var recurringId = 111L;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, recurringId: recurringId);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetMailings_with_start()
		{
			// Arrange
			var start = new DateTime(2015, 1, 1);
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, start: start);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetMailings_with_end()
		{
			// Arrange
			var end = new DateTime(2015, 12, 31, 23, 59, 59);
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, end: end);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetMailings_with_sortby()
		{
			// Arrange
			var sortBy = MailingsSortBy.CreatedOn;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, sortBy: sortBy);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetMailings_with_sortdirection()
		{
			// Arrange
			var sortDirection = SortDirection.Descending;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, sortDirection: sortDirection);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetMailings_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetMailings_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetMailings_with_clientid()
		{
			// Arrange
			var jsonMailing1 = "{\"id\":\"123\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";
			var jsonMailing2 = "{\"id\":\"456\",\"active_emails\":\"0\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/b020262b548e2ea3bdb540616ca2503cc19cbe3b3585224b\",\"share_email_link\":\"http://link.fictitiouscompany.com/share/443/b020262b548e2ea3af1ffc84f9135bef\",\"campaign_id\":\"0\",\"clickthru_html\":\"true\",\"clickthru_text\":\"true\",\"created_on\":\"2015-04-03 16:01:30\",\"encoding\":\"utf-8\",\"content_last_updated\":\"0000-00-00 00:00:00\",\"content_hash\":null,\"html_message\":null,\"list_id\":\"0\",\"name\":\"My mailing\",\"next_step\":\"recipients\",\"list_name\":null,\"opening_stats\":\"true\",\"scheduled_for\":\"0000-00-00 00:00:00\",\"scheduled_on\":\"0000-00-00 00:00:00\",\"sender_email\":null,\"sender_name\":null,\"bcc\":null,\"cc\":null,\"reply_to\":null,\"social_bar\":\"false\",\"footer\":null,\"status\":\"incomplete\",\"subject\":null,\"sublist_id\":\"0\",\"suspended\":\"false\",\"text_message\":null,\"tracking_params\":null,\"transfer_encoding\":\"quoted-printable\",\"type\":\"standard\",\"unsub_bottom_link\":\"true\",\"unsubscribes\":null}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"mailings\":[{0},{1}]}}}}", jsonMailing1, jsonMailing2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetMailingsAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetMailingsCount_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingsCount_with_status()
		{
			// Arrange
			var status = MailingStatus.Scheduled;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, status: status);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingsCount_with_type()
		{
			// Arrange
			var type = MailingType.Standard;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, type: type);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingsCount_with_name()
		{
			// Arrange
			var name = "My mailing";

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, name: name);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingsCount_with_listid()
		{
			// Arrange
			var listId = 111L;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, listId: listId);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingsCount_with_campaignid()
		{
			// Arrange
			var campaignId = 111L;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, campaignId: campaignId);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingsCount_with_recurringid()
		{
			// Arrange
			var recurringId = 111L;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, recurringId: recurringId);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingsCount_with_start()
		{
			// Arrange
			var start = new DateTime(2015, 1, 1);

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, start: start);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingsCount_with_end()
		{
			// Arrange
			var end = new DateTime(2015, 12, 31, 23, 59, 59);

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, end: end);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingsCount_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task UpdateMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_campaignid()
		{
			// Arrange
			var mailingId = 123L;
			var campaignId = 111L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, campaignId: campaignId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_listid()
		{
			// Arrange
			var mailingId = 123L;
			var listId = 111L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, listId: listId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_sublistid()
		{
			// Arrange
			var mailingId = 123L;
			var sublistId = 111L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, sublistId: sublistId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_name()
		{
			// Arrange
			var mailingId = 123L;
			var name = "My mailing";

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, name: name);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_type()
		{
			// Arrange
			var mailingId = 123L;
			var type = MailingType.Standard;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, type: type);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_encoding()
		{
			// Arrange
			var mailingId = 123L;
			var encoding = MessageEncoding.Utf8;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, encoding: encoding);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_transferencoding()
		{
			// Arrange
			var mailingId = 123L;
			var transferEncoding = TransferEncoding.Base64;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, transferEncoding: transferEncoding);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_subject()
		{
			// Arrange
			var mailingId = 123L;
			var subject = "My mailing subject";

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, subject: subject);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_senderemail()
		{
			// Arrange
			var mailingId = 123L;
			var senderEmail = "bobsmith@fictitiouscompany.com";

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, senderEmail: senderEmail);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_sendername()
		{
			// Arrange
			var mailingId = 123L;
			var senderName = "Bob Smith";

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, senderName: senderName);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_replyto()
		{
			// Arrange
			var mailingId = 123L;
			var replyTo = "marketing@fictitiouscompany.com";

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, replyTo: replyTo);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_htmlcontent()
		{
			// Arrange
			var mailingId = 123L;
			var htmlContent = "<html><body>Hello world</body></html>";

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, htmlContent: htmlContent);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_textcontent()
		{
			// Arrange
			var mailingId = 123L;
			var textContent = "Hello world";

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, textContent: textContent);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_trackopens_true()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, trackOpens: true);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_trackopens_false()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, trackOpens: false);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_trackclicksinhtml_true()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, trackClicksInHtml: true);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_trackclicksinhtml_false()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, trackClicksInHtml: false);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_trackclicksintext_true()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, trackClicksInText: true);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_trackclicksintext_false()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, trackClicksInText: false);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_trackingparameters()
		{
			// Arrange
			var mailingId = 123L;
			var trackingParameters = "param1=abc&param2=123";

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, trackingParameters: trackingParameters);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_endingon()
		{
			// Arrange
			var mailingId = 123L;
			var endingOn = new DateTime(2015, 7, 10);

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, endingOn: endingOn);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_maxrecurrences()
		{
			// Arrange
			var mailingId = 123L;
			var maxRecurrences = 99;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, maxRecurrences: maxRecurrences);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_recurringconditions()
		{
			// Arrange
			var mailingId = 123L;
			var recurringConditions = "???";

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, recurringConditions: recurringConditions);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMailing_with_clientid()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UpdateAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task SendMailingTestEmail_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SendTestEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.SendTestEmailAsync(USER_KEY, mailingId, recipientEmail);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task SendMailingTestEmail_with_separated_true()
		{
			// Arrange
			var mailingId = 123L;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SendTestEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.SendTestEmailAsync(USER_KEY, mailingId, recipientEmail, separated: true);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task SendMailingTestEmail_with_clientid()
		{
			// Arrange
			var mailingId = 123L;
			var recipientEmail = "bobsmith@fictitiouscompany.com";

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/SendTestEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.SendTestEmailAsync(USER_KEY, mailingId, recipientEmail, clientId: CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetMailingRawEmailMessage_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"subject\":\"This is a simple message\",\"message\":\"...dummy content...\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetEmailMessage")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetRawEmailMessageAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldNotBeNull();
			result.Subject.ShouldBe("This is a simple message");
		}

		[Fact]
		public async Task GetMailingRawEmailMessage_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"subject\":\"This is a simple message\",\"message\":\"...dummy content...\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetEmailMessage")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetRawEmailMessageAsync(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Subject.ShouldBe("This is a simple message");
		}

		[Fact]
		public async Task GetMailingRawHtml_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"<html><body>...dummy content...</body></html>\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetHtmlMessage")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetRawHtmlAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldBe("<html><body>...dummy content...</body></html>");
		}

		[Fact]
		public async Task GetMailingRawHtml_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"<html><body>...dummy content...</body></html>\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetHtmlMessage")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetRawHtmlAsync(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			result.ShouldBe("<html><body>...dummy content...</body></html>");
		}

		[Fact]
		public async Task GetMailingRawText_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"...dummy content...\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetTextMessage")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetRawTextAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldBe("...dummy content...");
		}

		[Fact]
		public async Task GetMailingRawText_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"...dummy content...\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetTextMessage")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetRawTextAsync(USER_KEY, mailingId, CLIENT_ID);

			// Assert
			result.ShouldBe("...dummy content...");
		}

		[Fact]
		public async Task ScheduleMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Schedule")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.ScheduleAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task ScheduleMailing_with_date()
		{
			// Arrange
			var mailingId = 12345L;
			var date = new DateTime(2015, 4, 3, 17, 0, 0);

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Schedule")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.ScheduleAsync(USER_KEY, mailingId, date: date);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task ScheduleMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Schedule")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.ScheduleAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UnscheduleMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Unschedule")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UnscheduleAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UnscheduleMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Unschedule")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.UnscheduleAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task SuspendMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Suspend")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.SuspendAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task SuspendMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Suspend")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.SuspendAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task ResumeMailing_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Resume")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.ResumeAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task ResumeMailing_with_clientid()
		{
			// Arrange
			var mailingId = 12345L;

			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/Resume")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.ResumeAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetMailingLogs_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogs_with_logtype()
		{
			// Arrange
			var mailingId = 123L;
			var logType = LogType.Sent;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0}]}}}}", sentLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, logType: logType);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(1);
		}

		[Fact]
		public async Task GetMailingLogs_with_listmemberid()
		{
			// Arrange
			var mailingId = 123L;
			var listMemberId = 111L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0}]}}}}", sentLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, listMemberId: listMemberId);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(1);
		}

		[Fact]
		public async Task GetMailingLogs_with_startdate()
		{
			// Arrange
			var mailingId = 123L;
			var start = new DateTime(2015, 1, 1);

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, start: start);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogs_with_enddate()
		{
			// Arrange
			var mailingId = 123L;
			var end = new DateTime(2015, 12, 31);

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, uniques: false, totals: false, end: end);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogs_with_limit()
		{
			// Arrange
			var mailingId = 123L;
			var limit = 5;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, uniques: false, totals: false, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogs_with_offset()
		{
			// Arrange
			var mailingId = 123L;
			var offset = 25;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, uniques: false, totals: false, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogs_with_clientid()
		{
			// Arrange
			var mailingId = 123L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, uniques: false, totals: false, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogs_with_uniques_true()
		{
			// Arrange
			var mailingId = 123L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, uniques: true, totals: false);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogs_with_totals_true()
		{
			// Arrange
			var mailingId = 123L;

			var sentLog = "{\"id\":\"1\",\"record_id\":\"1\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-18 15:27:01\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":null,\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403e139cc9ce93542dea11dc1da82aac3298\",\"l_registered\":\"2015-03-18 15:23:31\"}";
			var hardbounceLog = "{\"id\":\"312\",\"record_id\":\"11\",\"email\":\"aaa@aaa.com\",\"action\":\"bounce_hb\",\"total\":\"1\",\"time\":\"2015-03-18 15:29:36\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"{dsn} smtp;550 5.1.1 RESOLVER.ADR.RecipNotFound; not found\",\"show_email_link\":\"http://link.fictitiouscompany.com/v/443/f6f02aba584b403ec23295741ba72edcbcfd4b9079f78d8d\",\"l_registered\":\"2015-03-18 15:24:37\"}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", sentLog, hardbounceLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsAsync(USER_KEY, mailingId, uniques: false, totals: true);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogsCount_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogsCount_with_logtype()
		{
			// Arrange
			var mailingId = 123L;
			var logType = LogType.Sent;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId, logType: logType);

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogsCount_with_listmemberid()
		{
			// Arrange
			var mailingId = 123L;
			var listMemberId = 111L;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId, listMemberId: listMemberId);

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogsCount_with_uniques_true()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId, uniques: true);

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogsCount_with_totals_true()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId, totals: true);

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogsCount_with_startdate()
		{
			// Arrange
			var mailingId = 123L;
			var start = new DateTime(2015, 1, 1, 0, 0, 0);

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId, start: start);

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogsCount_with_enddate()
		{
			// Arrange
			var mailingId = 123L;
			var end = new DateTime(2015, 12, 31, 23, 59, 59);

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId, end: end);

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLogsCount_with_clientid()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLogsCountAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLinks_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinks")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLinks_with_limit()
		{
			// Arrange
			var mailingId = 123L;
			var limit = 5;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinks")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksAsync(USER_KEY, mailingId, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLinks_with_offset()
		{
			// Arrange
			var mailingId = 123L;
			var offset = 25;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinks")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksAsync(USER_KEY, mailingId, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLinks_with_clientid()
		{
			// Arrange
			var mailingId = 123L;

			var jsonLink1 = "{\"id\":\"2002673788\",\"status\":\"active\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\"}";
			var jsonLink2 = "{\"id\":\"2002673787\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}";

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinks")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLinksCount_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinks")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksCountAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLinksCount_with_clientid()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinks")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksCountAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLink_with_minimal_parameters()
		{
			// Arrange
			var linkId = 12345L;

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}}}}", linkId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinkInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinkAsync(USER_KEY, linkId);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(linkId);
		}

		[Fact]
		public async Task GetMailingLink_with_clientid()
		{
			// Arrange
			var linkId = 12345L;

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"link_to\":\"http://www.fictitiouscompany.com.com/\"}}}}", linkId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinkInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinkAsync(USER_KEY, linkId, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(linkId);
		}

		[Fact]
		public async Task GetMailingLinksWithStats_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;

			var jsonLink1 = string.Format("{{\"id\":\"111111\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\",\"mailing_id\":\"{0}\",\"unique\":\"3\",\"total\":\"10\",\"unique_rate\":\"2\",\"total_rate\":\"1\"}}", mailingId);
			var jsonLink2 = string.Format("{{\"id\":\"222222\",\"link_to\":\"http://www.fictitiouscompany.com.com/\",\"mailing_id\":\"{0}\",\"unique\":\"1\",\"total\":\"2\",\"unique_rate\":\"1\",\"total_rate\":\"2\"}}", mailingId);

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinksLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksWithStatsAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLinksWithStats_with_start()
		{
			// Arrange
			var mailingId = 123L;
			var start = new DateTime(2015, 1, 1);

			var jsonLink1 = string.Format("{{\"id\":\"111111\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\",\"mailing_id\":\"{0}\",\"unique\":\"3\",\"total\":\"10\",\"unique_rate\":\"2\",\"total_rate\":\"1\"}}", mailingId);
			var jsonLink2 = string.Format("{{\"id\":\"222222\",\"link_to\":\"http://www.fictitiouscompany.com.com/\",\"mailing_id\":\"{0}\",\"unique\":\"1\",\"total\":\"2\",\"unique_rate\":\"1\",\"total_rate\":\"2\"}}", mailingId);

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinksLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksWithStatsAsync(USER_KEY, mailingId, start: start);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLinksWithStats_with_end()
		{
			// Arrange
			var mailingId = 123L;
			var end = new DateTime(2015, 12, 31);

			var jsonLink1 = string.Format("{{\"id\":\"111111\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\",\"mailing_id\":\"{0}\",\"unique\":\"3\",\"total\":\"10\",\"unique_rate\":\"2\",\"total_rate\":\"1\"}}", mailingId);
			var jsonLink2 = string.Format("{{\"id\":\"222222\",\"link_to\":\"http://www.fictitiouscompany.com.com/\",\"mailing_id\":\"{0}\",\"unique\":\"1\",\"total\":\"2\",\"unique_rate\":\"1\",\"total_rate\":\"2\"}}", mailingId);

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinksLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksWithStatsAsync(USER_KEY, mailingId, end: end);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLinksWithStats_with_limit()
		{
			// Arrange
			var mailingId = 123L;
			var limit = 5;

			var jsonLink1 = string.Format("{{\"id\":\"111111\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\",\"mailing_id\":\"{0}\",\"unique\":\"3\",\"total\":\"10\",\"unique_rate\":\"2\",\"total_rate\":\"1\"}}", mailingId);
			var jsonLink2 = string.Format("{{\"id\":\"222222\",\"link_to\":\"http://www.fictitiouscompany.com.com/\",\"mailing_id\":\"{0}\",\"unique\":\"1\",\"total\":\"2\",\"unique_rate\":\"1\",\"total_rate\":\"2\"}}", mailingId);

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinksLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksWithStatsAsync(USER_KEY, mailingId, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLinksWithStats_with_offset()
		{
			// Arrange
			var mailingId = 123L;
			var offset = 25;

			var jsonLink1 = string.Format("{{\"id\":\"111111\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\",\"mailing_id\":\"{0}\",\"unique\":\"3\",\"total\":\"10\",\"unique_rate\":\"2\",\"total_rate\":\"1\"}}", mailingId);
			var jsonLink2 = string.Format("{{\"id\":\"222222\",\"link_to\":\"http://www.fictitiouscompany.com.com/\",\"mailing_id\":\"{0}\",\"unique\":\"1\",\"total\":\"2\",\"unique_rate\":\"1\",\"total_rate\":\"2\"}}", mailingId);

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinksLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksWithStatsAsync(USER_KEY, mailingId, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLinksWithStats_with_clientid()
		{
			// Arrange
			var mailingId = 123L;

			var jsonLink1 = string.Format("{{\"id\":\"111111\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\",\"mailing_id\":\"{0}\",\"unique\":\"3\",\"total\":\"10\",\"unique_rate\":\"2\",\"total_rate\":\"1\"}}", mailingId);
			var jsonLink2 = string.Format("{{\"id\":\"222222\",\"link_to\":\"http://www.fictitiouscompany.com.com/\",\"mailing_id\":\"{0}\",\"unique\":\"1\",\"total\":\"2\",\"unique_rate\":\"1\",\"total_rate\":\"2\"}}", mailingId);

			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"links\":[{0},{1}]}}}}", jsonLink1, jsonLink2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinksLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksWithStatsAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetMailingLinksWithStatsCount_with_minimal_parameters()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"links\":[],\"count\":\"3\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinksLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksWithStatsCountAsync(USER_KEY, mailingId);

			// Assert
			result.ShouldBe(3);
		}

		[Fact]
		public async Task GetMailingLinksWithStatsCount_with_start()
		{
			// Arrange
			var mailingId = 123L;
			var start = new DateTime(2015, 1, 1);

			var jsonLink1 = string.Format("{{\"id\":\"111111\",\"link_to\":\"http://fictitiouscompany.com/hello_world.aspx\",\"mailing_id\":\"{0}\",\"unique\":\"3\",\"total\":\"10\",\"unique_rate\":\"2\",\"total_rate\":\"1\"}}", mailingId);
			var jsonLink2 = string.Format("{{\"id\":\"222222\",\"link_to\":\"http://www.fictitiouscompany.com.com/\",\"mailing_id\":\"{0}\",\"unique\":\"1\",\"total\":\"2\",\"unique_rate\":\"1\",\"total_rate\":\"2\"}}", mailingId);

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"links\":[],\"count\":\"3\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinksLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksWithStatsCountAsync(USER_KEY, mailingId, start: start);

			// Assert
			result.ShouldBe(3);
		}

		[Fact]
		public async Task GetMailingLinksWithStatsCount_with_end()
		{
			// Arrange
			var mailingId = 123L;
			var end = new DateTime(2015, 12, 31);

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"links\":[],\"count\":\"3\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinksLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksWithStatsCountAsync(USER_KEY, mailingId, end: end);

			// Assert
			result.ShouldBe(3);
		}

		[Fact]
		public async Task GetMailingLinksWithStatsCount_with_limit()
		{
			// Arrange
			var mailingId = 123L;
			var limit = 5;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"links\":[],\"count\":\"3\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinksLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksWithStatsCountAsync(USER_KEY, mailingId, limit: limit);

			// Assert
			result.ShouldBe(3);
		}

		[Fact]
		public async Task GetMailingLinksWithStatsCount_with_offset()
		{
			// Arrange
			var mailingId = 123L;
			var offset = 25;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"links\":[],\"count\":\"3\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinksLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksWithStatsCountAsync(USER_KEY, mailingId, offset: offset);

			// Assert
			result.ShouldBe(3);
		}

		[Fact]
		public async Task GetMailingLinksWithStatsCount_with_clientid()
		{
			// Arrange
			var mailingId = 123L;

			var jsonResponse = "{\"status\":\"success\",\"data\":{\"links\":[],\"count\":\"3\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Mailing/GetLinksLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Mailings.GetLinksWithStatsCountAsync(USER_KEY, mailingId, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(3);
		}
	}
}
