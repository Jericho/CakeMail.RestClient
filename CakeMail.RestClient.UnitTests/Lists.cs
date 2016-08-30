using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class ListsTests
	{
		private const long CLIENT_ID = 999;

		[TestMethod]
		public async Task CreateList_with_minimal_parameters()
		{
			// Arrange
			var name = "My new list";
			var defaultSenderName = "Bob Smith";
			var defaultSenderAddress = "bobsmith@fictitiouscompany.com";
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_name", Value = defaultSenderName },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = defaultSenderAddress }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", listId);
			var mockRestClient = new MockRestClient("/List/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.CreateAsync(MockRestClient.USER_KEY, name, defaultSenderName, defaultSenderAddress);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(listId, result);
		}

		[TestMethod]
		public async Task CreateList_with_spampolicyaccepted_false()
		{
			// Arrange
			var name = "My new list";
			var defaultSenderName = "Bob Smith";
			var defaultSenderAddress = "bobsmith@fictitiouscompany.com";
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_name", Value = defaultSenderName },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = defaultSenderAddress }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", listId);
			var mockRestClient = new MockRestClient("/List/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.CreateAsync(MockRestClient.USER_KEY, name, defaultSenderName, defaultSenderAddress, spamPolicyAccepted: false);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(listId, result);
		}

		[TestMethod]
		public async Task CreateList_with_spampolicyaccepted_true()
		{
			// Arrange
			var name = "My new list";
			var defaultSenderName = "Bob Smith";
			var defaultSenderAddress = "bobsmith@fictitiouscompany.com";
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_name", Value = defaultSenderName },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = defaultSenderAddress },
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_policy", Value = "accepted" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", listId);
			var mockRestClient = new MockRestClient("/List/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.CreateAsync(MockRestClient.USER_KEY, name, defaultSenderName, defaultSenderAddress, spamPolicyAccepted: true);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(listId, result);
		}

		[TestMethod]
		public async Task CreateList_with_clientid()
		{
			// Arrange
			var name = "My new list";
			var defaultSenderName = "Bob Smith";
			var defaultSenderAddress = "bobsmith@fictitiouscompany.com";
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_name", Value = defaultSenderName },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = defaultSenderAddress },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", listId);
			var mockRestClient = new MockRestClient("/List/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.CreateAsync(MockRestClient.USER_KEY, name, defaultSenderName, defaultSenderAddress, clientId: CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(listId, result);
		}

		[TestMethod]
		public async Task DeleteList_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/Delete/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteAsync(MockRestClient.USER_KEY, listId, null);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteList_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/Delete/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteAsync(MockRestClient.USER_KEY, listId, CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetList_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "no_details", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "with_engagement", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId);
			var mockRestClient = new MockRestClient("/List/GetInfo/", parameters, jsonResponse);

			// Actt
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetAsync(MockRestClient.USER_KEY, listId);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public async Task GetList_with_includestatistics_true()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "no_details", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "with_engagement", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId);
			var mockRestClient = new MockRestClient("/List/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetAsync(MockRestClient.USER_KEY, listId, includeStatistics: true);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public async Task GetList_with_includestatistics_false()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "no_details", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "with_engagement", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId);
			var mockRestClient = new MockRestClient("/List/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetAsync(MockRestClient.USER_KEY, listId, includeStatistics: false);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public async Task GetList_with_calculateengagement_true()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "no_details", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "with_engagement", Value = "true" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId);
			var mockRestClient = new MockRestClient("/List/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetAsync(MockRestClient.USER_KEY, listId, calculateEngagement: true);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public async Task GetList_with_calculateengagement_false()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "no_details", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "with_engagement", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId);
			var mockRestClient = new MockRestClient("/List/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetAsync(MockRestClient.USER_KEY, listId, calculateEngagement: false);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public async Task GetList_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "no_details", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "with_engagement", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId);
			var mockRestClient = new MockRestClient("/List/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetAsync(MockRestClient.USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public async Task GetLists_with_status()
		{
			// Arrange
			var status = ListStatus.Active;
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2);
			var mockRestClient = new MockRestClient("/List/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetListsAsync(MockRestClient.USER_KEY, status: status);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetLists_with_name()
		{
			// Arrange
			var name = "Dummy List";
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2);
			var mockRestClient = new MockRestClient("/List/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetListsAsync(MockRestClient.USER_KEY, name: name);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetLists_with_sortby()
		{
			// Arrange
			var sortBy = ListsSortBy.Name;
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "sort_by", Value = sortBy.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2);
			var mockRestClient = new MockRestClient("/List/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetListsAsync(MockRestClient.USER_KEY, sortBy: sortBy);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetLists_with_sortdirection()
		{
			// Arrange
			var sortDirection = SortDirection.Ascending;
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "direction", Value = sortDirection.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2);
			var mockRestClient = new MockRestClient("/List/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetListsAsync(MockRestClient.USER_KEY, sortDirection: sortDirection);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetLists_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2);
			var mockRestClient = new MockRestClient("/List/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetListsAsync(MockRestClient.USER_KEY, limit: limit);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetLists_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2);
			var mockRestClient = new MockRestClient("/List/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetListsAsync(MockRestClient.USER_KEY, offset: offset);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetLists_with_clientid()
		{
			// Arrange
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2);
			var mockRestClient = new MockRestClient("/List/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetListsAsync(MockRestClient.USER_KEY, clientId: CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListsCount_with_status()
		{
			// Arrange
			var status = ListStatus.Active;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetCountAsync(MockRestClient.USER_KEY, status: status);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListsCount_with_name()
		{
			// Arrange
			var name = "Dummy List";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetCountAsync(MockRestClient.USER_KEY, name: name);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListsCount_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetCountAsync(MockRestClient.USER_KEY, clientId: CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task UpdateList_name()
		{
			// Arrange
			var listId = 12345L;
			var name = "My list";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "name", Value = name }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(MockRestClient.USER_KEY, listId, name: name);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_language()
		{
			// Arrange
			var listId = 12345L;
			var language = "fr-FR";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "language", Value = language }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(MockRestClient.USER_KEY, listId, language: language);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_spampolicy_accepted()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_policy", Value = "accepted" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(MockRestClient.USER_KEY, listId, spamPolicyAccepted: true);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_spampolicy_declined()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_policy", Value = "declined" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(MockRestClient.USER_KEY, listId, spamPolicyAccepted: false);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_status()
		{
			// Arrange
			var listId = 12345L;
			var status = ListStatus.Archived;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(MockRestClient.USER_KEY, listId, status: status);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_sendername()
		{
			// Arrange
			var listId = 12345L;
			var senderName = "Bob Smith";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_name", Value = senderName }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(MockRestClient.USER_KEY, listId, senderName: senderName);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_senderemail()
		{
			// Arrange
			var listId = 12345L;
			var senderEmail = "bobsmith@fictitiouscompany.com";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sender_email", Value = senderEmail }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(MockRestClient.USER_KEY, listId, senderEmail: senderEmail);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_gotooi()
		{
			// Arrange
			var listId = 12345L;
			var goto_oi = "???";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "goto_oi", Value = goto_oi }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(MockRestClient.USER_KEY, listId, goto_oi: goto_oi);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_gotodi()
		{
			// Arrange
			var listId = 12345L;
			var goto_di = "???";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "goto_di", Value = goto_di }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(MockRestClient.USER_KEY, listId, goto_di: goto_di);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_gotooo()
		{
			// Arrange
			var listId = 12345L;
			var goto_oo = "???";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "goto_oo", Value = goto_oo }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(MockRestClient.USER_KEY, listId, goto_oo: goto_oo);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_webhook()
		{
			// Arrange
			var listId = 12345L;
			var webhook = "???";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "webhook", Value = webhook }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(MockRestClient.USER_KEY, listId, webhook: webhook);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_clientid()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(MockRestClient.USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task AddListField_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var name = "My field";
			var fieldType = FieldType.Text;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "field", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "type", Value = fieldType.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "action", Value = "add" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/EditStructure/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.AddFieldAsync(MockRestClient.USER_KEY, listId, name, fieldType);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task AddListField_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var name = "My field";
			var fieldType = FieldType.Integer;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "field", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "type", Value = fieldType.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "action", Value = "add" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/EditStructure/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.AddFieldAsync(MockRestClient.USER_KEY, listId, name, fieldType, CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteListField_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var name = "My field";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "field", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "action", Value = "delete" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/EditStructure/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteFieldAsync(MockRestClient.USER_KEY, listId, name);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteListField_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var name = "My field";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "field", Value = name },
				new Parameter { Type = ParameterType.GetOrPost, Name = "action", Value = "delete" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/EditStructure/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteFieldAsync(MockRestClient.USER_KEY, listId, name, CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetListFields_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"id\":\"integer\",\"email\":\"text\",\"registered\":\"timestamp\"}}";
			var mockRestClient = new MockRestClient("/List/GetFields/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetFieldsAsync(MockRestClient.USER_KEY, listId);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public async Task GetListFields_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"id\":\"integer\",\"email\":\"text\",\"registered\":\"timestamp\"}}";
			var mockRestClient = new MockRestClient("/List/GetFields/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetFieldsAsync(MockRestClient.USER_KEY, listId, CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public async Task GetListFields_returns_null()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":null}";
			var mockRestClient = new MockRestClient("/List/GetFields/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetFieldsAsync(MockRestClient.USER_KEY, listId, null);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(0, result.Count());
		}

		[TestMethod]
		public async Task AddTestEmail_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var email = "test@test.com";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/AddTestEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.AddTestEmailAsync(MockRestClient.USER_KEY, listId, email);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task AddTestEmail_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var email = "test@test.com";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/AddTestEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.AddTestEmailAsync(MockRestClient.USER_KEY, listId, email, CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteTestEmail_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var email = "test@test.com";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/DeleteTestEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteTestEmailAsync(MockRestClient.USER_KEY, listId, email);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteTestEmail_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var email = "test@test.com";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/DeleteTestEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteTestEmailAsync(MockRestClient.USER_KEY, listId, email, CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetTestEmails_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var testEmail1 = "aaa@aaa.com";
			var testEmail2 = "bbb@bbb.com";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"testemails\":[\"{0}\",\"{1}\"]}}}}", testEmail1, testEmail2);
			var mockRestClient = new MockRestClient("/List/GetTestEmails/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetTestEmailsAsync(MockRestClient.USER_KEY, listId);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(testEmail1, result.ToArray()[0]);
			Assert.AreEqual(testEmail2, result.ToArray()[1]);
		}

		[TestMethod]
		public async Task GetTestEmails_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var testEmail1 = "aaa@aaa.com";
			var testEmail2 = "bbb@bbb.com";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"testemails\":[\"{0}\",\"{1}\"]}}}}", testEmail1, testEmail2);
			var mockRestClient = new MockRestClient("/List/GetTestEmails/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetTestEmailsAsync(MockRestClient.USER_KEY, listId, CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(testEmail1, result.ToArray()[0]);
			Assert.AreEqual(testEmail2, result.ToArray()[1]);
		}

		[TestMethod]
		public async Task Subscribe_with_autoresponders_true()
		{
			// Arrange
			var listId = 12345L;
			var email = "aaa@aaa.com";
			var subscriberId = 777;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "autoresponders", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "triggers", Value = "true" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId);
			var mockRestClient = new MockRestClient("/List/SubscribeEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.SubscribeAsync(MockRestClient.USER_KEY, listId, email, autoResponders: true);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public async Task Subscribe_with_autoresponders_false()
		{
			// Arrange
			var listId = 12345L;
			var email = "aaa@aaa.com";
			var subscriberId = 777;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "autoresponders", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "triggers", Value = "true" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId);
			var mockRestClient = new MockRestClient("/List/SubscribeEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.SubscribeAsync(MockRestClient.USER_KEY, listId, email, autoResponders: false);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public async Task Subscribe_with_triggers_true()
		{
			// Arrange
			var listId = 12345L;
			var email = "aaa@aaa.com";
			var subscriberId = 777;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "autoresponders", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "triggers", Value = "true" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId);
			var mockRestClient = new MockRestClient("/List/SubscribeEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.SubscribeAsync(MockRestClient.USER_KEY, listId, email, autoResponders: true);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public async Task Subscribe_with_triggers_false()
		{
			// Arrange
			var listId = 12345L;
			var email = "aaa@aaa.com";
			var subscriberId = 777;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "autoresponders", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "triggers", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId);
			var mockRestClient = new MockRestClient("/List/SubscribeEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.SubscribeAsync(MockRestClient.USER_KEY, listId, email, triggers: false);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
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
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "autoresponders", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "triggers", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "data[firstname]", Value = firstName },
				new Parameter { Type = ParameterType.GetOrPost, Name = "data[lastname]", Value = lastName },
				new Parameter { Type = ParameterType.GetOrPost, Name = "data[birthday]", Value = "1973-01-01 00:00:00" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId);
			var mockRestClient = new MockRestClient("/List/SubscribeEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.SubscribeAsync(MockRestClient.USER_KEY, listId, email, customFields: customFields);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public async Task Subscribe_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var email = "aaa@aaa.com";
			var subscriberId = 777;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "autoresponders", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "triggers", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId);
			var mockRestClient = new MockRestClient("/List/SubscribeEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.SubscribeAsync(MockRestClient.USER_KEY, listId, email, clientId: CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public async Task Import_with_autoresponders_true()
		{
			// Arrange
			var listId = 12345L;
			var listMembers = new[]
			{
				new ListMember { Email = "aaa@aaa.com" },
				new ListMember { Email = "bbb@bbb.com" }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "import_to", Value = "active" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "autoresponders", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "triggers", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[0][email]", Value = "aaa@aaa.com" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[1][email]", Value = "bbb@bbb.com" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}";
			var mockRestClient = new MockRestClient("/List/Import/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.ImportAsync(MockRestClient.USER_KEY, listId, listMembers, autoResponders: true);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task Import_with_autoresponders_false()
		{
			// Arrange
			var listId = 12345L;
			var listMembers = new[]
			{
				new ListMember { Email = "aaa@aaa.com" },
				new ListMember { Email = "bbb@bbb.com" }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "import_to", Value = "active" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "autoresponders", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "triggers", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[0][email]", Value = "aaa@aaa.com" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[1][email]", Value = "bbb@bbb.com" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}";
			var mockRestClient = new MockRestClient("/List/Import/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.ImportAsync(MockRestClient.USER_KEY, listId, listMembers, autoResponders: false);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task Import_with_triggers_true()
		{
			// Arrange
			var listId = 12345L;
			var listMembers = new[]
			{
				new ListMember { Email = "aaa@aaa.com" },
				new ListMember { Email = "bbb@bbb.com" }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "import_to", Value = "active" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "autoresponders", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "triggers", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[0][email]", Value = "aaa@aaa.com" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[1][email]", Value = "bbb@bbb.com" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}";
			var mockRestClient = new MockRestClient("/List/Import/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.ImportAsync(MockRestClient.USER_KEY, listId, listMembers, triggers: true);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task Import_with_triggers_false()
		{
			// Arrange
			var listId = 12345L;
			var listMembers = new[]
			{
				new ListMember { Email = "aaa@aaa.com" },
				new ListMember { Email = "bbb@bbb.com" }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "import_to", Value = "active" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "autoresponders", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "triggers", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[0][email]", Value = "aaa@aaa.com" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[1][email]", Value = "bbb@bbb.com" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}";
			var mockRestClient = new MockRestClient("/List/Import/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.ImportAsync(MockRestClient.USER_KEY, listId, listMembers, triggers: false);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task Import_with_customfields()
		{
			// Arrange
			var listId = 12345L;
			var listMembers = new[]
			{
				new ListMember { Email = "aaa@aaa.com", CustomFields = new Dictionary<string, object> { { "firstname", "Bob" }, { "lastname", "Smith" }, { "age", 41 }, { "birthday", new DateTime(1973, 1, 1) } } },
				new ListMember { Email = "bbb@bbb.com", CustomFields = new Dictionary<string, object> { { "firstname", "Jane" }, { "lastname", "Doe" }, { "age", 50 }, { "birthday", new DateTime(1964, 1, 1) } } }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "import_to", Value = "active" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "autoresponders", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "triggers", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[0][email]", Value = "aaa@aaa.com" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[0][firstname]", Value = "Bob" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[0][lastname]", Value = "Smith" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[0][age]", Value = 41 },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[0][birthday]", Value = "1973-01-01 00:00:00" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[1][email]", Value = "bbb@bbb.com" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[1][firstname]", Value = "Jane" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[1][lastname]", Value = "Doe" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[1][age]", Value = 50 },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[1][birthday]", Value = "1964-01-01 00:00:00" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}";
			var mockRestClient = new MockRestClient("/List/Import/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.ImportAsync(MockRestClient.USER_KEY, listId, listMembers, autoResponders: true);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task Import_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var listMembers = new[]
			{
				new ListMember { Email = "aaa@aaa.com" },
				new ListMember { Email = "bbb@bbb.com" }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "import_to", Value = "active" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "autoresponders", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "triggers", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[0][email]", Value = "aaa@aaa.com" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record[1][email]", Value = "bbb@bbb.com" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}";
			var mockRestClient = new MockRestClient("/List/Import/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.ImportAsync(MockRestClient.USER_KEY, listId, listMembers, clientId: CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task Import_zero_subscribers()
		{
			// Arrange
			var listId = 12345L;
			var listMembers = (ListMember[])null;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "import_to", Value = "active" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "autoresponders", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "triggers", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}";
			var mockRestClient = new MockRestClient("/List/Import/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.ImportAsync(MockRestClient.USER_KEY, listId, listMembers);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task Unsubscribe_by_email()
		{
			// Arrange
			var listId = 12345L;
			var email = "test@test.com";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/UnsubscribeEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UnsubscribeAsync(MockRestClient.USER_KEY, listId, email, null);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task Unsubscribe_by_email_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var email = "test@test.com";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/UnsubscribeEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UnsubscribeAsync(MockRestClient.USER_KEY, listId, email, CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task Unsubscribe_by_memberid()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 555L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = memberId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/UnsubscribeEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UnsubscribeAsync(MockRestClient.USER_KEY, listId, memberId, null);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task Unsubscribe_by_memberid_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 555L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = memberId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/UnsubscribeEmail/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UnsubscribeAsync(MockRestClient.USER_KEY, listId, memberId, CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteListMember()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 555L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = memberId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/DeleteRecord/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteMemberAsync(MockRestClient.USER_KEY, listId, memberId, null);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteListMember_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 555L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = memberId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/DeleteRecord/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteMemberAsync(MockRestClient.USER_KEY, listId, memberId, CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetListMember()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 555L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = memberId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}}}}", memberId);
			var mockRestClient = new MockRestClient("/List/GetRecord/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMemberAsync(MockRestClient.USER_KEY, listId, memberId, null);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(memberId, result.Id);
		}

		[TestMethod]
		public async Task GetListMember_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 555L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = memberId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}}}}", memberId);
			var mockRestClient = new MockRestClient("/List/GetRecord/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMemberAsync(MockRestClient.USER_KEY, listId, memberId, CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(memberId, result.Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockRestClient = new MockRestClient("/List/Show/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(MockRestClient.USER_KEY, listId);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_status()
		{
			// Arrange
			var listId = 12345L;
			var status = ListMemberStatus.Active;
			var jsonMember1 = string.Format("{{\"id\":\"1\",\"status\":\"{0}\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}}", status);
			var jsonMember2 = string.Format("{{\"id\":\"2\",\"status\":\"{0}\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}}", status);
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockRestClient = new MockRestClient("/List/Show/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(MockRestClient.USER_KEY, listId, status: status);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_query()
		{
			// Arrange
			var listId = 12345L;
			var query = "(... this is a bogus query ...)";
			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "query", Value = query }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockRestClient = new MockRestClient("/List/Show/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(MockRestClient.USER_KEY, listId, query: query);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_sortby()
		{
			// Arrange
			var listId = 12345L;
			var sortBy = ListMembersSortBy.EmailAddress;
			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "sort_by", Value = sortBy.GetEnumMemberValue() }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockRestClient = new MockRestClient("/List/Show/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(MockRestClient.USER_KEY, listId, sortBy: sortBy);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_sortdirection()
		{
			// Arrange
			var listId = 12345L;
			var sortDirection = SortDirection.Ascending;
			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "direction", Value = sortDirection.GetEnumMemberValue() }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockRestClient = new MockRestClient("/List/Show/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(MockRestClient.USER_KEY, listId, sortDirection: sortDirection);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_limit()
		{
			// Arrange
			var listId = 12345L;
			var limit = 5;
			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockRestClient = new MockRestClient("/List/Show/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(MockRestClient.USER_KEY, listId, limit: limit);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_offset()
		{
			// Arrange
			var listId = 12345L;
			var offset = 25;
			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockRestClient = new MockRestClient("/List/Show/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(MockRestClient.USER_KEY, listId, offset: offset);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aaa@aaa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2);
			var mockRestClient = new MockRestClient("/List/Show/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(MockRestClient.USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembersCount_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/Show/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersCountAsync(MockRestClient.USER_KEY, listId);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListMembersCount_with_status()
		{
			// Arrange
			var listId = 12345L;
			var status = ListMemberStatus.Active;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/Show/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersCountAsync(MockRestClient.USER_KEY, listId, status: status);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListMembersCount_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/Show/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersCountAsync(MockRestClient.USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task UpdateMember_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 456L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = memberId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/UpdateRecord/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateMemberAsync(MockRestClient.USER_KEY, listId, memberId);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
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
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = memberId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "data[fullname]", Value = "Bob Smith" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "data[birthday]", Value = "1973-01-01 00:00:00" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/UpdateRecord/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateMemberAsync(MockRestClient.USER_KEY, listId, memberId, customFields: customFields);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMember_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var memberId = 456L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "record_id", Value = memberId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/List/UpdateRecord/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateMemberAsync(MockRestClient.USER_KEY, listId, memberId, clientId: CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetListLogs_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(MockRestClient.USER_KEY, listId, uniques: false, totals: false);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_logtype()
		{
			// Arrange
			var listId = 12345L;
			var logType = LogType.Subscribe;
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "action", Value = logType.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0}]}}}}", subscribeLog);
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(MockRestClient.USER_KEY, listId, uniques: false, totals: false, logType: logType);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_startdate()
		{
			// Arrange
			var listId = 12345L;
			var start = new DateTime(2015, 1, 1);
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "start_time", Value = start.ToCakeMailString() }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(MockRestClient.USER_KEY, listId, uniques: false, totals: false, start: start);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_enddate()
		{
			// Arrange
			var listId = 12345L;
			var end = new DateTime(2015, 12, 31);
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "end_time", Value = end.ToCakeMailString() }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(MockRestClient.USER_KEY, listId, uniques: false, totals: false, end: end);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_limit()
		{
			// Arrange
			var listId = 12345L;
			var limit = 5;
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(MockRestClient.USER_KEY, listId, uniques: false, totals: false, limit: limit);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_offset()
		{
			// Arrange
			var listId = 12345L;
			var offset = 25;
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(MockRestClient.USER_KEY, listId, uniques: false, totals: false, offset: offset);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(MockRestClient.USER_KEY, listId, uniques: false, totals: false, clientId: CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_uniques_true()
		{
			// Arrange
			var listId = 12345L;
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(MockRestClient.USER_KEY, listId, uniques: true, totals: false);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_totals_true()
		{
			// Arrange
			var listId = 12345L;
			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog);
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(MockRestClient.USER_KEY, listId, uniques: false, totals: true);

			// Assert
			mockRestClient.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogsCount_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsCountAsync(MockRestClient.USER_KEY, listId);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListLogsCount_with_logtype()
		{
			// Arrange
			var listId = 12345L;
			var logType = LogType.Click;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "action", Value = logType.GetEnumMemberValue() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsCountAsync(MockRestClient.USER_KEY, listId, logType: logType);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListLogsCount_with_startdate()
		{
			// Arrange
			var listId = 12345L;
			var start = new DateTime(2015, 1, 1);
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "start_time", Value = start.ToCakeMailString() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsCountAsync(MockRestClient.USER_KEY, listId, uniques: false, totals: false, start: start);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListLogsCount_with_enddate()
		{
			// Arrange
			var listId = 12345L;
			var end = new DateTime(2015, 12, 31);
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "end_time", Value = end.ToCakeMailString() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsCountAsync(MockRestClient.USER_KEY, listId, uniques: false, totals: false, end: end);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListLogsCount_with_clientid()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsCountAsync(MockRestClient.USER_KEY, listId, uniques: false, totals: false, clientId: CLIENT_ID);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListLogsCount_with_uniques_true()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsCountAsync(MockRestClient.USER_KEY, listId, uniques: true, totals: false);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListLogsCount_with_totals_true()
		{
			// Arrange
			var listId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "list_id", Value = listId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "uniques", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "totals", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/List/GetLog/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(MockRestClient.API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsCountAsync(MockRestClient.USER_KEY, listId, uniques: false, totals: true);

			// Assert
			mockRestClient.Verify();
			Assert.AreEqual(2, result);
		}
	}
}
