using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class ListsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const int CLIENT_ID = 999;

		[TestMethod]
		public void CreateList_with_all_parameters()
		{
			// Arrange
			var name = "My new list";
			var defaultSenderName = "Bob Smith";
			var defaultSenderAddress = "bobsmith@fictitiouscompany.com";
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_name" && (string)p.Value == defaultSenderName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_email" && (string)p.Value == defaultSenderAddress && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "policy" && (string)p.Value == "accepted" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateList(USER_KEY, name, defaultSenderName, defaultSenderAddress, true, CLIENT_ID);

			// Assert
			Assert.AreEqual(listId, result);
		}

		[TestMethod]
		public void CreateList_without_policy()
		{
			// Arrange
			var name = "My new list";
			var defaultSenderName = "Bob Smith";
			var defaultSenderAddress = "bobsmith@fictitiouscompany.com";
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_name" && (string)p.Value == defaultSenderName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_email" && (string)p.Value == defaultSenderAddress && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateList(USER_KEY, name, defaultSenderName, defaultSenderAddress, false, CLIENT_ID);

			// Assert
			Assert.AreEqual(listId, result);
		}

		[TestMethod]
		public void CreateList_without_clientid()
		{
			// Arrange
			var name = "My new list";
			var defaultSenderName = "Bob Smith";
			var defaultSenderAddress = "bobsmith@fictitiouscompany.com";
			var policy = "accepted";
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_name" && (string)p.Value == defaultSenderName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_email" && (string)p.Value == defaultSenderAddress && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "policy" && (string)p.Value == "accepted" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateList(USER_KEY, name, defaultSenderName, defaultSenderAddress, true, null);

			// Assert
			Assert.AreEqual(listId, result);
		}

		[TestMethod]
		public void DeleteList_with_all_parameters()
		{
			// Arrange
			var listId = 12345;

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
			var result = apiClient.DeleteList(USER_KEY, listId, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void DeleteList_without_clientid()
		{
			// Arrange
			var listId = 12345;

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
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteList(USER_KEY, listId, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetList_with_all_parameters()
		{
			// Arrange
			var listId = 12345;
			var subListId = 6789;
			var includeDetails = true;
			var calculateEngagement = true;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == subListId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "no_details" && (string)p.Value == (includeDetails ? "false" : "true") && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "with_engagement" && (string)p.Value == (calculateEngagement ? "true" : "false") && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetList(USER_KEY, listId, subListId, includeDetails, calculateEngagement, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public void GetList_without_sublist()
		{
			// Arrange
			var listId = 12345;
			var subListId = (int?)null;
			var includeDetails = true;
			var calculateEngagement = true;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "no_details" && (string)p.Value == (includeDetails ? "false" : "true") && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "with_engagement" && (string)p.Value == (calculateEngagement ? "true" : "false") && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetList(USER_KEY, listId, subListId, includeDetails, calculateEngagement, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public void GetList_without_clientid()
		{
			// Arrange
			var listId = 12345;
			var subListId = 6789;
			var includeDetails = true;
			var calculateEngagement = true;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == subListId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "no_details" && (string)p.Value == (includeDetails ? "false" : "true") && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "with_engagement" && (string)p.Value == (calculateEngagement ? "true" : "false") && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetList(USER_KEY, listId, subListId, includeDetails, calculateEngagement, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public void GetList_no_details()
		{
			// Arrange
			var listId = 12345;
			var subListId = 6789;
			var includeDetails = false;
			var calculateEngagement = false;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == subListId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "no_details" && (string)p.Value == (includeDetails ? "false" : "true") && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "with_engagement" && (string)p.Value == (calculateEngagement ? "true" : "false") && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (int)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetList(USER_KEY, listId, subListId, includeDetails, calculateEngagement, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public void GetLists_with_status()
		{
			// Arrange
			var status = "active";

			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";

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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetLists(USER_KEY, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetLists_with_name()
		{
			// Arrange
			var name = "Dummy List";

			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";

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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetLists(USER_KEY, name: name);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetLists_with_sortby()
		{
			// Arrange
			var sortBy = "name";

			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";

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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetLists(USER_KEY, sortBy: sortBy);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetLists_with_sortdirection()
		{
			// Arrange
			var sortDirection = "asc";

			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";

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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetLists(USER_KEY, sortDirection: sortDirection);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetLists_with_limit()
		{
			// Arrange
			var limit = 5;

			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";

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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetLists(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetLists_with_offset()
		{
			// Arrange
			var offset = 25;

			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";

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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetLists(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetLists_with_clientid()
		{
			// Arrange
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";

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
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetLists(USER_KEY, clientId: CLIENT_ID);

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
		public void UpdateList_policy()
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
				r.Parameters.Count(p => p.Name == "policy" && (string)p.Value == "accepted" && p.Type == ParameterType.GetOrPost) == 1
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
		public void UpdateSegment_name()
		{
			// Arrange
			var listId = 123;
			var segmentId = 456;
			var name = "My list";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == segmentId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.UpdateSegment(USER_KEY, segmentId, listId, name: name);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateSegment_language()
		{
			// Arrange
			var listId = 123;
			var segmentId = 456;
			var language = "fr-FR";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == segmentId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.UpdateSegment(USER_KEY, segmentId, listId, language: language);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateSegment_policy()
		{
			// Arrange
			var listId = 123;
			var segmentId = 456;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == segmentId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "policy" && (string)p.Value == "accepted" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateSegment(USER_KEY, segmentId, listId, spamPolicyAccepted: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateSegment_status()
		{
			// Arrange
			var listId = 123;
			var segmentId = 456;
			var status = "archived";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == segmentId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.UpdateSegment(USER_KEY, segmentId, listId, status: status);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateSegment_sendername()
		{
			// Arrange
			var listId = 123;
			var segmentId = 456;
			var senderName = "Bob Smith";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == segmentId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.UpdateSegment(USER_KEY, segmentId, listId, senderName: senderName);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateSegment_senderemail()
		{
			// Arrange
			var listId = 123;
			var segmentId = 456;
			var senderEmail = "bobsmith@fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == segmentId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.UpdateSegment(USER_KEY, segmentId, listId, senderEmail: senderEmail);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateSegment_gotooi()
		{
			// Arrange
			var listId = 123;
			var segmentId = 456;
			var goto_oi = "???";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == segmentId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.UpdateSegment(USER_KEY, segmentId, listId, goto_oi: goto_oi);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateSegment_gotodi()
		{
			// Arrange
			var listId = 123;
			var segmentId = 456;
			var goto_di = "???";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == segmentId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.UpdateSegment(USER_KEY, segmentId, listId, goto_di: goto_di);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateSegment_gotooo()
		{
			// Arrange
			var listId = 123;
			var segmentId = 456;
			var goto_oo = "???";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == segmentId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.UpdateSegment(USER_KEY, segmentId, listId, goto_oo: goto_oo);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateSegment_webhook()
		{
			// Arrange
			var listId = 123;
			var segmentId = 456;
			var webhook = "???";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == segmentId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.UpdateSegment(USER_KEY, segmentId, listId, webhook: webhook);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateSegment_query()
		{
			// Arrange
			var listId = 123;
			var segmentId = 456;
			var query = "???";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == segmentId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (int)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "query" && (string)p.Value == query && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateSegment(USER_KEY, segmentId, listId, query: query);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateSegment_clientid()
		{
			// Arrange
			var listId = 123;
			var segmentId = 456;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sublist_id" && (int)p.Value == segmentId && p.Type == ParameterType.GetOrPost) == 1 &&
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
			var result = apiClient.UpdateSegment(USER_KEY, segmentId, listId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}
	}
}
