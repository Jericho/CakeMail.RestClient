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
	public class UsersTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[TestMethod]
		public async Task CreateUser_with_minimal_parameters()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var timezoneId = 542L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password_confirmation", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "timezone_id", Value = timezoneId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", userId);
			var mockRestClient = new MockRestClient("/User/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password);

			// Assert
			Assert.AreEqual(userId, result);
		}

		[TestMethod]
		public async Task CreateUser_with_firstname()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var firstName = "Bob";
			var timezoneId = 542L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password_confirmation", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "first_name", Value = firstName },
				new Parameter { Type = ParameterType.GetOrPost, Name = "timezone_id", Value = timezoneId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockRestClient = new MockRestClient("/User/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, firstName: firstName);

			// Assert
			Assert.AreEqual(userId, result);
		}

		[TestMethod]
		public async Task CreateUser_with_lastname()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var lastName = "Smith";
			var timezoneId = 542L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password_confirmation", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "last_name", Value = lastName },
				new Parameter { Type = ParameterType.GetOrPost, Name = "timezone_id", Value = timezoneId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockRestClient = new MockRestClient("/User/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, lastName: lastName);

			// Assert
			Assert.AreEqual(userId, result);
		}

		[TestMethod]
		public async Task CreateUser_with_title()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var title = "Marketing Director";
			var timezoneId = 542L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password_confirmation", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "title", Value = title },
				new Parameter { Type = ParameterType.GetOrPost, Name = "timezone_id", Value = timezoneId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockRestClient = new MockRestClient("/User/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, title: title);

			// Assert
			Assert.AreEqual(userId, result);
		}

		[TestMethod]
		public async Task CreateUser_with_officephone()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var officePhone = "555-555-1212";
			var timezoneId = 542L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password_confirmation", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "office_phone", Value = officePhone },
				new Parameter { Type = ParameterType.GetOrPost, Name = "timezone_id", Value = timezoneId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockRestClient = new MockRestClient("/User/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, officePhone: officePhone);

			// Assert
			Assert.AreEqual(userId, result);
		}

		[TestMethod]
		public async Task CreateUser_with_mobilephone()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var mobilePhone = "555-111-2222";
			var timezoneId = 542L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password_confirmation", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "mobile_phone", Value = mobilePhone },
				new Parameter { Type = ParameterType.GetOrPost, Name = "timezone_id", Value = timezoneId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockRestClient = new MockRestClient("/User/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, mobilePhone: mobilePhone);

			// Assert
			Assert.AreEqual(userId, result);
		}

		[TestMethod]
		public async Task CreateUser_with_language()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var language = "en-US";
			var timezoneId = 542L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password_confirmation", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "language", Value = language },
				new Parameter { Type = ParameterType.GetOrPost, Name = "timezone_id", Value = timezoneId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockRestClient = new MockRestClient("/User/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, language: language);

			// Assert
			Assert.AreEqual(userId, result);
		}

		[TestMethod]
		public async Task CreateUser_with_timezoneid()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var timezoneId = 542L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password_confirmation", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "timezone_id", Value = timezoneId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockRestClient = new MockRestClient("/User/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, timezoneId: timezoneId);

			// Assert
			Assert.AreEqual(userId, result);
		}

		[TestMethod]
		public async Task CreateUser_with_clientid()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var timezoneId = 542L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password_confirmation", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "timezone_id", Value = timezoneId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockRestClient = new MockRestClient("/User/Create/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(userId, result);
		}

		[TestMethod]
		public async Task DeactivateUser_with_minimal_parameters()
		{
			// Arrange
			var userId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = "suspended" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/User/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.DeactivateAsync(USER_KEY, userId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteUser_with_minimal_parameters()
		{
			// Arrange
			var userId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = "deleted" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/User/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.DeleteAsync(USER_KEY, userId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetUser_with_minimal_parameters()
		{
			// Arrange
			var userId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"{0}\",\"user_key\":\"{1}\",\"email\":\"admin@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"John\",\"last_name\":\"Smith\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}}}}", userId, USER_KEY);
			var mockRestClient = new MockRestClient("/User/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.GetAsync(USER_KEY, userId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(userId, result.Id);
		}

		[TestMethod]
		public async Task GetUser_with_clientid()
		{
			// Arrange
			var userId = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"{0}\",\"user_key\":\"{1}\",\"email\":\"admin@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"John\",\"last_name\":\"Smith\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}}}}", userId, USER_KEY);
			var mockRestClient = new MockRestClient("/User/GetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.GetAsync(USER_KEY, userId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(userId, result.Id);
		}

		[TestMethod]
		public async Task GetUsers_with_minimal_parameters()
		{
			// Arrange
			var jsonUser1 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"123\",\"user_key\":\"... user key #1...\",\"email\":\"admin1@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"John\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonUser2 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"456\",\"user_key\":\"... user key #2...\",\"email\":\"admin2@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"Jane\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"users\":[{0},{1}]}}}}", jsonUser1, jsonUser2);
			var mockRestClient = new MockRestClient("/User/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.GetUsersAsync(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetUsers_with_status()
		{
			// Arrange
			var status = UserStatus.Active;
			var jsonUser1 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"123\",\"user_key\":\"... user key #1...\",\"email\":\"admin1@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"John\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonUser2 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"456\",\"user_key\":\"... user key #2...\",\"email\":\"admin2@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"Jane\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"users\":[{0},{1}]}}}}", jsonUser1, jsonUser2);
			var mockRestClient = new MockRestClient("/User/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.GetUsersAsync(USER_KEY, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetUsers_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonUser1 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"123\",\"user_key\":\"... user key #1...\",\"email\":\"admin1@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"John\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonUser2 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"456\",\"user_key\":\"... user key #2...\",\"email\":\"admin2@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"Jane\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"users\":[{0},{1}]}}}}", jsonUser1, jsonUser2);
			var mockRestClient = new MockRestClient("/User/GetList/", parameters, jsonResponse);
			
			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.GetUsersAsync(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetUsers_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonUser1 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"123\",\"user_key\":\"... user key #1...\",\"email\":\"admin1@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"John\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonUser2 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"456\",\"user_key\":\"... user key #2...\",\"email\":\"admin2@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"Jane\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"users\":[{0},{1}]}}}}", jsonUser1, jsonUser2);
			var mockRestClient = new MockRestClient("/User/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.GetUsersAsync(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetUsers_with_clientid()
		{
			// Arrange
			var jsonUser1 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"123\",\"user_key\":\"... user key #1...\",\"email\":\"admin1@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"John\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonUser2 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"456\",\"user_key\":\"... user key #2...\",\"email\":\"admin2@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"Jane\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"users\":[{0},{1}]}}}}", jsonUser1, jsonUser2);
			var mockRestClient = new MockRestClient("/User/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.GetUsersAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetUsersCount_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/User/GetList/", parameters, jsonResponse);
			
			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.GetCountAsync(USER_KEY);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetUsersCount_with_status()
		{
			// Arrange
			var status = UserStatus.Active;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/User/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.GetCountAsync(USER_KEY, status: status);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetUsersCount_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/User/GetList/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.GetCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task UpdateUser_with_minimal_parameters()
		{
			// Arrange
			var userId = 123L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/User/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateUser_with_email()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/User/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, email: email);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateUser_with_password()
		{
			// Arrange
			var userId = 123L;
			var password = "AbCdEfGhIjKlMnOpQrStUvWxYz";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password_confirmation", Value = password }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/User/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, password: password);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateUser_with_firstname()
		{
			// Arrange
			var userId = 123L;
			var firstName = "Bob";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "first_name", Value = firstName }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/User/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, firstName: firstName);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateUser_with_lastname()
		{
			// Arrange
			var userId = 123L;
			var lastName = "Smith";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "last_name", Value = lastName }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/User/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, lastName: lastName);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateUser_with_title()
		{
			// Arrange
			var userId = 123L;
			var title = "Marketing Director";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "title", Value = title }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/User/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, title: title);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateUser_with_officephone()
		{
			// Arrange
			var userId = 123L;
			var officePhone = "555-555-1212";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "office_phone", Value = officePhone }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/User/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, officePhone: officePhone);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateUser_with_mobilephone()
		{
			// Arrange
			var userId = 123L;
			var mobilePhone = "555-111-2222";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "mobile_phone", Value = mobilePhone }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/User/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, mobilePhone: mobilePhone);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateUser_with_language()
		{
			// Arrange
			var userId = 123L;
			var language = "en-US";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "language", Value = language }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/User/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, language: language);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateUser_with_timezoneid()
		{
			// Arrange
			var userId = 123L;
			var timezoneId = 542L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "timezone_id", Value = timezoneId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/User/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, timezoneId: timezoneId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateUser_with_status()
		{
			// Arrange
			var userId = 123L;
			var status = UserStatus.Active;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "status", Value = status.GetEnumMemberValue() }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/User/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, status: status);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateUser_with_clientid()
		{
			// Arrange
			var userId = 123L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/User/SetInfo/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task Login_with_minimal_parameters()
		{
			// Arrange
			var email = "admin@fictitiouscompany.com";
			var password = "abc123";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password", Value = password }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"client_id\":\"12345\",\"client_key\":\"... client key ...\",\"user_id\":\"111\",\"user_key\":\"{0}\",\"first_name\":\"Bob\",\"last_name\":\"Smith\",\"language\":\"en_US\",\"timezone\":\"UTC\",\"last_activity\":\"2015-04-15 18:53:32\",\"client_lineage\":\"1-12345\"}}}}", USER_KEY);
			var mockRestClient = new MockRestClient("/User/Login/", parameters, jsonResponse, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.LoginAsync(email, password);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(USER_KEY, result.UserKey);
		}

		[TestMethod]
		public async Task Login_with_clientid()
		{
			// Arrange
			var email = "admin@fictitiouscompany.com";
			var password = "abc123";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email", Value = email },
				new Parameter { Type = ParameterType.GetOrPost, Name = "password", Value = password },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"client_id\":\"12345\",\"client_key\":\"... client key ...\",\"user_id\":\"111\",\"user_key\":\"{0}\",\"first_name\":\"Bob\",\"last_name\":\"Smith\",\"language\":\"en_US\",\"timezone\":\"UTC\",\"last_activity\":\"2015-04-15 18:53:32\",\"client_lineage\":\"1-12345\"}}}}", USER_KEY);
			var mockRestClient = new MockRestClient("/User/Login/", parameters, jsonResponse, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Users.LoginAsync(email, password, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(USER_KEY, result.UserKey);
		}
	}
}
