using CakeMail.RestClient.Models;
using RichardSzalay.MockHttp;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Resources
{
	public class ListsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[Fact]
		public async Task CreateList_with_minimal_parameters()
		{
			// Arrange
			var name = "My new list";
			var defaultSenderName = "Bob Smith";
			var defaultSenderAddress = "bobsmith@fictitiouscompany.com";
			var listId = 12345L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", listId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.CreateAsync(USER_KEY, name, defaultSenderName, defaultSenderAddress);

			// Assert
			result.ShouldBe(listId);
		}

		[Fact]
		public async Task CreateList_with_spampolicyaccepted()
		{
			// Arrange
			var name = "My new list";
			var defaultSenderName = "Bob Smith";
			var defaultSenderAddress = "bobsmith@fictitiouscompany.com";
			var listId = 12345L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", listId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.CreateAsync(USER_KEY, name, defaultSenderName, defaultSenderAddress, spamPolicyAccepted: true);

			// Assert
			result.ShouldBe(listId);
		}

		[Fact]
		public async Task CreateList_with_clientid()
		{
			// Arrange
			var name = "My new list";
			var defaultSenderName = "Bob Smith";
			var defaultSenderAddress = "bobsmith@fictitiouscompany.com";
			var listId = 12345L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", listId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.CreateAsync(USER_KEY, name, defaultSenderName, defaultSenderAddress, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(listId);
		}

		[Fact]
		public async Task DeleteList_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Delete")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.DeleteAsync(USER_KEY, listId, null);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteList_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Delete")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.DeleteAsync(USER_KEY, listId, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetList_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetInfo")).Respond("application/json", jsonResponse);

			// Actt
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetAsync(USER_KEY, listId);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(listId);
		}

		[Fact]
		public async Task GetList_with_includestatistics_true()
		{
			await GetList_with_includestatistics(true).ConfigureAwait(false);
		}

		[Fact]
		public async Task GetList_with_includestatistics_false()
		{
			await GetList_with_includestatistics(false).ConfigureAwait(false);
		}

		private async Task GetList_with_includestatistics(bool includeStatistics)
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetAsync(USER_KEY, listId, includeStatistics: includeStatistics);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(listId);
		}

		[Fact]
		public async Task GetList_with_calculateengagement_true()
		{
			await GetList_with_calculateengagement(true).ConfigureAwait(false);
		}

		[Fact]
		public async Task GetList_with_calculateengagement_false()
		{
			await GetList_with_calculateengagement(false).ConfigureAwait(false);
		}

		private async Task GetList_with_calculateengagement(bool calculateEngagement)
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetAsync(USER_KEY, listId, calculateEngagement: calculateEngagement);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(listId);
		}

		[Fact]
		public async Task GetList_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetAsync(USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(listId);
		}

		[Fact]
		public async Task GetLists_with_status()
		{
			// Arrange
			var status = ListStatus.Active;
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetListsAsync(USER_KEY, status: status);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetLists_with_name()
		{
			// Arrange
			var name = "Dummy List";
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetListsAsync(USER_KEY, name: name);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetLists_with_sortby()
		{
			// Arrange
			var sortBy = ListsSortBy.Name;
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetListsAsync(USER_KEY, sortBy: sortBy);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetLists_with_sortdirection()
		{
			// Arrange
			var sortDirection = Models.SortDirection.Ascending;
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetListsAsync(USER_KEY, sortDirection: sortDirection);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetLists_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetListsAsync(USER_KEY, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetLists_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetListsAsync(USER_KEY, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetLists_with_clientid()
		{
			// Arrange
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetListsAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetListsCount_with_status()
		{
			// Arrange
			var status = ListStatus.Active;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetCountAsync(USER_KEY, status: status);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetListsCount_with_name()
		{
			// Arrange
			var name = "Dummy List";
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetCountAsync(USER_KEY, name: name);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetListsCount_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task UpdateList_name()
		{
			// Arrange
			var listId = 12345L;
			var name = "My list";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, name: name);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateList_language()
		{
			// Arrange
			var listId = 12345L;
			var language = "fr-FR";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, language: language);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateList_spampolicy_accepted()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, spamPolicyAccepted: true);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateList_spampolicy_declined()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, spamPolicyAccepted: false);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateList_status()
		{
			// Arrange
			var listId = 12345L;
			var status = ListStatus.Archived;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, status: status);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateList_sendername()
		{
			// Arrange
			var listId = 12345L;
			var senderName = "Bob Smith";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, senderName: senderName);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateList_senderemail()
		{
			// Arrange
			var listId = 12345L;
			var senderEmail = "bobsmith@fictitiouscompany.com";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, senderEmail: senderEmail);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateList_gotooi()
		{
			// Arrange
			var listId = 12345L;
			var goto_oi = "???";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, goto_oi: goto_oi);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateList_gotodi()
		{
			// Arrange
			var listId = 12345L;
			var goto_di = "???";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, goto_di: goto_di);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateList_gotooo()
		{
			// Arrange
			var listId = 12345L;
			var goto_oo = "???";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, goto_oo: goto_oo);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateList_webhook()
		{
			// Arrange
			var listId = 12345L;
			var webhook = "???";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, webhook: webhook);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateList_clientid()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task AddListField_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var name = "My field";
			var fieldType = FieldType.Text;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/EditStructure")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.AddFieldAsync(USER_KEY, listId, name, fieldType);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task AddListField_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var name = "My field";
			var fieldType = FieldType.Integer;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/EditStructure")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.AddFieldAsync(USER_KEY, listId, name, fieldType, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteListField_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var name = "My field";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/EditStructure")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.DeleteFieldAsync(USER_KEY, listId, name);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteListField_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var name = "My field";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/EditStructure")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.DeleteFieldAsync(USER_KEY, listId, name, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetListFields_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"id\":\"integer\",\"email\":\"text\",\"registered\":\"timestamp\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetFields")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetFieldsAsync(USER_KEY, listId);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task GetListFields_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"id\":\"integer\",\"email\":\"text\",\"registered\":\"timestamp\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetFields")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetFieldsAsync(USER_KEY, listId, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task GetListFields_returns_null()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":null}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetFields")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetFieldsAsync(USER_KEY, listId, null);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(0);
		}

		[Fact]
		public async Task AddTestEmail_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var email = "test@test.com";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/AddTestEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.AddTestEmailAsync(USER_KEY, listId, email);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task AddTestEmail_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var email = "test@test.com";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/AddTestEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.AddTestEmailAsync(USER_KEY, listId, email, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteTestEmail_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var email = "test@test.com";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/DeleteTestEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.DeleteTestEmailAsync(USER_KEY, listId, email);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteTestEmail_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var email = "test@test.com";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/DeleteTestEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.DeleteTestEmailAsync(USER_KEY, listId, email, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetTestEmails_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var testEmail1 = "aaa@aaa.com";
			var testEmail2 = "bbb@bbb.com";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"testemails\":[\"{0}\",\"{1}\"]}}}}", testEmail1, testEmail2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetTestEmails")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetTestEmailsAsync(USER_KEY, listId);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].ShouldBe(testEmail1);
			result.ToArray()[1].ShouldBe(testEmail2);
		}

		[Fact]
		public async Task GetTestEmails_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var testEmail1 = "aaa@aaa.com";
			var testEmail2 = "bbb@bbb.com";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"testemails\":[\"{0}\",\"{1}\"]}}}}", testEmail1, testEmail2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetTestEmails")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetTestEmailsAsync(USER_KEY, listId, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].ShouldBe(testEmail1);
			result.ToArray()[1].ShouldBe(testEmail2);
		}

		[Fact]
		public async Task Subscribe_with_autoresponders_true()
		{
			await Subscribe_with_autoresponders(true).ConfigureAwait(false);
		}

		[Fact]
		public async Task Subscribe_with_autoresponders_false()
		{
			await Subscribe_with_autoresponders(false).ConfigureAwait(false);
		}

		private async Task Subscribe_with_autoresponders(bool autoResponder)
		{
			// Arrange
			var listId = 12345L;
			var email = "aaa@aaa.com";
			var subscriberId = 777L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SubscribeEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.SubscribeAsync(USER_KEY, listId, email, autoResponders: autoResponder);

			// Assert
			result.ShouldBe(subscriberId);
		}

		[Fact]
		public async Task Subscribe_with_triggers_true()
		{
			await Subscribe_with_triggers(true).ConfigureAwait(false);
		}

		[Fact]
		public async Task Subscribe_with_triggers_false()
		{
			await Subscribe_with_triggers(false).ConfigureAwait(false);
		}

		private async Task Subscribe_with_triggers(bool triggers)
		{
			// Arrange
			var listId = 12345L;
			var email = "aaa@aaa.com";
			var subscriberId = 777;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SubscribeEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.SubscribeAsync(USER_KEY, listId, email, triggers: triggers);

			// Assert
			result.ShouldBe(subscriberId);
		}

		[Fact]
		public async Task Subscribe_with_customfields()
		{
			// Arrange
			var listId = 12345L;
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
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SubscribeEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.SubscribeAsync(USER_KEY, listId, email, customFields: customFields);

			// Assert
			result.ShouldBe(subscriberId);
		}

		[Fact]
		public async Task Subscribe_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var email = "aaa@aaa.com";
			var subscriberId = 777;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/SubscribeEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.SubscribeAsync(USER_KEY, listId, email, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(subscriberId);
		}

		[Fact]
		public async Task Import_with_autoresponders_true()
		{
			await Import_with_autoresponders(true).ConfigureAwait(false);
		}

		[Fact]
		public async Task Import_with_autoresponders_false()
		{
			await Import_with_autoresponders(false).ConfigureAwait(false);
		}

		private async Task Import_with_autoresponders(bool autoResponder)
		{
			// Arrange
			var listId = 12345L;
			var listMembers = new[]
			{
				new ListMember { Email = "aaa@aaa.com" },
				new ListMember { Email = "bbb@bbb.com" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Import")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.ImportAsync(USER_KEY, listId, listMembers, autoResponders: autoResponder);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task Import_with_triggers_true()
		{
			await Import_with_triggers(true).ConfigureAwait(false);
		}

		[Fact]
		public async Task Import_with_triggers_false()
		{
			await Import_with_triggers(false).ConfigureAwait(false);
		}

		private async Task Import_with_triggers(bool triggers)
		{
			// Arrange
			var listId = 12345L;
			var listMembers = new[]
			{
				new ListMember { Email = "aaa@aaa.com" },
				new ListMember { Email = "bbb@bbb.com" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Import")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.ImportAsync(USER_KEY, listId, listMembers, triggers: triggers);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task Import_with_customfields()
		{
			// Arrange
			var listId = 12345L;
			var listMembers = new[]
			{
				new ListMember { Email = "aaa@aaa.com", CustomFields = new Dictionary<string, object> { { "firstname", "Bob" }, { "lastname", "Smith" }, { "age", 41 }, { "birthday", new DateTime(1973, 1, 1) } } },
				new ListMember { Email = "bbb@bbb.com", CustomFields = new Dictionary<string, object> { { "firstname", "Jane" }, { "lastname", "Doe" }, { "age", 50 }, { "birthday", new DateTime(1964, 1, 1) } } }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Import")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.ImportAsync(USER_KEY, listId, listMembers, autoResponders: true);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task Import_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var listMembers = new[]
			{
				new ListMember { Email = "aaa@aaa.com" },
				new ListMember { Email = "bbb@bbb.com" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Import")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.ImportAsync(USER_KEY, listId, listMembers, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task Import_zero_subscribers()
		{
			// Arrange
			var listId = 12345L;
			var listMembers = (ListMember[])null;
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Import")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.ImportAsync(USER_KEY, listId, listMembers);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task Unsubscribe_by_email()
		{
			// Arrange
			var listId = 12345L;
			var email = "test@test.com";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/UnsubscribeEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UnsubscribeAsync(USER_KEY, listId, email, null);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task Unsubscribe_by_email_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var email = "test@test.com";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/UnsubscribeEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UnsubscribeAsync(USER_KEY, listId, email, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task Unsubscribe_by_memberid()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 555L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/UnsubscribeEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UnsubscribeAsync(USER_KEY, listId, memberId, null);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task Unsubscribe_by_memberid_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 555L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/UnsubscribeEmail")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UnsubscribeAsync(USER_KEY, listId, memberId, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteListMember()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 555L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/DeleteRecord")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.DeleteMemberAsync(USER_KEY, listId, memberId, null);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteListMember_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 555L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/DeleteRecord")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.DeleteMemberAsync(USER_KEY, listId, memberId, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetListMember()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 555L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}}}}", memberId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetRecord")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetMemberAsync(USER_KEY, listId, memberId, null);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(memberId);
		}

		[Fact]
		public async Task GetListMember_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 555L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}}}}", memberId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetRecord")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetMemberAsync(USER_KEY, listId, memberId, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(memberId);
		}

		[Fact]
		public async Task GetListMembers_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Show")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(1);
			result.ToArray()[1].Id.ShouldBe(2);
		}

		[Fact]
		public async Task GetListMembers_with_status()
		{
			// Arrange
			var listId = 12345L;
			var status = ListMemberStatus.Active;
			var jsonMember1 = string.Format("{{\"id\":\"1\",\"status\":\"{0}\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}}", status);
			var jsonMember2 = string.Format("{{\"id\":\"2\",\"status\":\"{0}\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}}", status);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Show")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId, status: status);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(1);
			result.ToArray()[1].Id.ShouldBe(2);
		}

		[Fact]
		public async Task GetListMembers_with_query()
		{
			// Arrange
			var listId = 12345L;
			var query = "(... this is a bogus query ...)";
			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Show")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId, query: query);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(1);
			result.ToArray()[1].Id.ShouldBe(2);
		}

		[Fact]
		public async Task GetListMembers_with_sortby()
		{
			// Arrange
			var listId = 12345L;
			var sortBy = ListMembersSortBy.EmailAddress;
			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Show")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId, sortBy: sortBy);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(1);
			result.ToArray()[1].Id.ShouldBe(2);
		}

		[Fact]
		public async Task GetListMembers_with_sortdirection()
		{
			// Arrange
			var listId = 12345L;
			var sortDirection = Models.SortDirection.Ascending;
			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Show")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId, sortDirection: sortDirection);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(1);
			result.ToArray()[1].Id.ShouldBe(2);
		}

		[Fact]
		public async Task GetListMembers_with_limit()
		{
			// Arrange
			var listId = 12345L;
			var limit = 5;
			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Show")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(1);
			result.ToArray()[1].Id.ShouldBe(2);
		}

		[Fact]
		public async Task GetListMembers_with_offset()
		{
			// Arrange
			var listId = 12345L;
			var offset = 25;
			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Show")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(1);
			result.ToArray()[1].Id.ShouldBe(2);
		}

		[Fact]
		public async Task GetListMembers_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Show")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(1);
			result.ToArray()[1].Id.ShouldBe(2);
		}

		[Fact]
		public async Task GetListMembersCount_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Show")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetMembersCountAsync(USER_KEY, listId);

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetListMembersCount_with_status()
		{
			// Arrange
			var listId = 12345L;
			var status = ListMemberStatus.Active;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Show")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetMembersCountAsync(USER_KEY, listId, status: status);

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetListMembersCount_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/Show")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetMembersCountAsync(USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.ShouldBe(2);
		}

		[Fact]
		public async Task UpdateMember_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 456L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/UpdateRecord")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateMemberAsync(USER_KEY, listId, memberId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMember_with_customfields()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 456L;
			var customFields = new[]
			{
				new KeyValuePair<string, object>("fullname", "Bob Smith"),
				new KeyValuePair<string, object>("birthday", new DateTime(1973, 1, 1))
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/UpdateRecord")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateMemberAsync(USER_KEY, listId, memberId, customFields: customFields);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateMember_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 456L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/UpdateRecord")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.UpdateMemberAsync(USER_KEY, listId, memberId, clientId: CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetListLogs_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: false);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetListLogs_with_logtype()
		{
			// Arrange
			var listId = 12345L;
			var logType = LogType.Subscribe;
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0}]}}}}", subscribeLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: false, logType: logType);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(1);
		}

		[Fact]
		public async Task GetListLogs_with_startdate()
		{
			// Arrange
			var listId = 12345L;
			var start = new DateTime(2015, 1, 1);
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: false, start: start);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetListLogs_with_enddate()
		{
			// Arrange
			var listId = 12345L;
			var end = new DateTime(2015, 12, 31);
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: false, end: end);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetListLogs_with_limit()
		{
			// Arrange
			var listId = 12345L;
			var limit = 5;
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: false, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetListLogs_with_offset()
		{
			// Arrange
			var listId = 12345L;
			var offset = 25;
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: false, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetListLogs_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: false, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetListLogs_with_uniques_true()
		{
			// Arrange
			var listId = 12345L;
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: true, totals: false);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetListLogs_with_totals_true()
		{
			// Arrange
			var listId = 12345L;
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: true);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetListLogsCount_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsCountAsync(USER_KEY, listId);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetListLogsCount_with_logtype()
		{
			// Arrange
			var listId = 12345L;
			var logType = LogType.Click;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsCountAsync(USER_KEY, listId, logType: logType);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetListLogsCount_with_startdate()
		{
			// Arrange
			var listId = 12345L;
			var start = new DateTime(2015, 1, 1);
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsCountAsync(USER_KEY, listId, uniques: false, totals: false, start: start);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetListLogsCount_with_enddate()
		{
			// Arrange
			var listId = 12345L;
			var end = new DateTime(2015, 12, 31);
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsCountAsync(USER_KEY, listId, uniques: false, totals: false, end: end);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetListLogsCount_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsCountAsync(USER_KEY, listId, uniques: false, totals: false, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetListLogsCount_with_uniques_true()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsCountAsync(USER_KEY, listId, uniques: true, totals: false);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetListLogsCount_with_totals_true()
		{
			// Arrange
			var listId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("List/GetLog")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Lists.GetLogsCountAsync(USER_KEY, listId, uniques: false, totals: true);

			// Assert
			result.ShouldBe(2);
		}
	}
}
