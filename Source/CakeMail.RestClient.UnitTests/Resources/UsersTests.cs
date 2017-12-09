using CakeMail.RestClient.Models;
using RichardSzalay.MockHttp;
using Shouldly;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Resources
{
	public class UsersTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[Fact]
		public async Task CreateUser_with_minimal_parameters()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\"}}}}", userId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password);

			// Assert
			result.ShouldBe(userId);
		}

		[Fact]
		public async Task CreateUser_with_firstname()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var firstName = "Bob";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, firstName: firstName);

			// Assert
			result.ShouldBe(userId);
		}

		[Fact]
		public async Task CreateUser_with_lastname()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var lastName = "Smith";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, lastName: lastName);

			// Assert
			result.ShouldBe(userId);
		}

		[Fact]
		public async Task CreateUser_with_title()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var title = "Marketing Director";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, title: title);

			// Assert
			result.ShouldBe(userId);
		}

		[Fact]
		public async Task CreateUser_with_officephone()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var officePhone = "555-555-1212";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, officePhone: officePhone);

			// Assert
			result.ShouldBe(userId);
		}

		[Fact]
		public async Task CreateUser_with_mobilephone()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var mobilePhone = "555-111-2222";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, mobilePhone: mobilePhone);

			// Assert
			result.ShouldBe(userId);
		}

		[Fact]
		public async Task CreateUser_with_language()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var language = "en-US";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, language: language);

			// Assert
			result.ShouldBe(userId);
		}

		[Fact]
		public async Task CreateUser_with_timezoneid()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var timezoneId = 542L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, timezoneId: timezoneId);

			// Assert
			result.ShouldBe(userId);
		}

		[Fact]
		public async Task CreateUser_with_clientid()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var password = "MyPassword";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"key\":\"...user key...\"}}}}", userId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/Create")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.CreateAsync(USER_KEY, email, password, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(userId);
		}

		[Fact]
		public async Task DeactivateUser_with_minimal_parameters()
		{
			// Arrange
			var userId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.DeactivateAsync(USER_KEY, userId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteUser_with_minimal_parameters()
		{
			// Arrange
			var userId = 12345L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.DeleteAsync(USER_KEY, userId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetUser_with_minimal_parameters()
		{
			// Arrange
			var userId = 12345L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"{0}\",\"user_key\":\"{1}\",\"email\":\"admin@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"John\",\"last_name\":\"Smith\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}}}}", userId, USER_KEY);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.GetAsync(USER_KEY, userId);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(userId);
		}

		[Fact]
		public async Task GetUser_with_clientid()
		{
			// Arrange
			var userId = 12345L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"{0}\",\"user_key\":\"{1}\",\"email\":\"admin@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"John\",\"last_name\":\"Smith\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}}}}", userId, USER_KEY);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/GetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.GetAsync(USER_KEY, userId, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(userId);
		}

		[Fact]
		public async Task GetUsers_with_minimal_parameters()
		{
			// Arrange
			var jsonUser1 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"123\",\"user_key\":\"... user key #1...\",\"email\":\"admin1@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"John\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonUser2 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"456\",\"user_key\":\"... user key #2...\",\"email\":\"admin2@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"Jane\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"users\":[{0},{1}]}}}}", jsonUser1, jsonUser2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.GetUsersAsync(USER_KEY);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetUsers_with_status()
		{
			// Arrange
			var status = UserStatus.Active;
			var jsonUser1 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"123\",\"user_key\":\"... user key #1...\",\"email\":\"admin1@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"John\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonUser2 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"456\",\"user_key\":\"... user key #2...\",\"email\":\"admin2@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"Jane\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"users\":[{0},{1}]}}}}", jsonUser1, jsonUser2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.GetUsersAsync(USER_KEY, status: status);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetUsers_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonUser1 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"123\",\"user_key\":\"... user key #1...\",\"email\":\"admin1@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"John\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonUser2 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"456\",\"user_key\":\"... user key #2...\",\"email\":\"admin2@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"Jane\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"users\":[{0},{1}]}}}}", jsonUser1, jsonUser2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.GetUsersAsync(USER_KEY, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetUsers_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonUser1 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"123\",\"user_key\":\"... user key #1...\",\"email\":\"admin1@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"John\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonUser2 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"456\",\"user_key\":\"... user key #2...\",\"email\":\"admin2@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"Jane\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"users\":[{0},{1}]}}}}", jsonUser1, jsonUser2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.GetUsersAsync(USER_KEY, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetUsers_with_clientid()
		{
			// Arrange
			var jsonUser1 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"123\",\"user_key\":\"... user key #1...\",\"email\":\"admin1@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"John\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonUser2 = "{\"permissions\":[\"admin_settings\",\"admin_marketplace\",\"admin_campaigns\",\"admin_lists\"],\"id\":\"456\",\"user_key\":\"... user key #2...\",\"email\":\"admin2@fictitiouscompany.com\",\"status\":\"active\",\"first_name\":\"Jane\",\"last_name\":\"Doe\",\"openid\":null,\"last_activity\":\"2015-04-15 19:05:46\",\"timezone_id\":\"542\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"users\":[{0},{1}]}}}}", jsonUser1, jsonUser2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.GetUsersAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(123);
			result.ToArray()[1].Id.ShouldBe(456);
		}

		[Fact]
		public async Task GetUsersCount_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.GetCountAsync(USER_KEY);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetUsersCount_with_status()
		{
			// Arrange
			var status = UserStatus.Active;
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.GetCountAsync(USER_KEY, status: status);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetUsersCount_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.GetCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task UpdateUser_with_minimal_parameters()
		{
			// Arrange
			var userId = 123L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateUser_with_email()
		{
			// Arrange
			var userId = 123L;
			var email = "bob@fictitiouscompany.com";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, email: email);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateUser_with_password()
		{
			// Arrange
			var userId = 123L;
			var password = "AbCdEfGhIjKlMnOpQrStUvWxYz";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, password: password);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateUser_with_firstname()
		{
			// Arrange
			var userId = 123L;
			var firstName = "Bob";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, firstName: firstName);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateUser_with_lastname()
		{
			// Arrange
			var userId = 123L;
			var lastName = "Smith";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, lastName: lastName);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateUser_with_title()
		{
			// Arrange
			var userId = 123L;
			var title = "Marketing Director";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, title: title);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateUser_with_officephone()
		{
			// Arrange
			var userId = 123L;
			var officePhone = "555-555-1212";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, officePhone: officePhone);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateUser_with_mobilephone()
		{
			// Arrange
			var userId = 123L;
			var mobilePhone = "555-111-2222";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, mobilePhone: mobilePhone);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateUser_with_language()
		{
			// Arrange
			var userId = 123L;
			var language = "en-US";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, language: language);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateUser_with_timezoneid()
		{
			// Arrange
			var userId = 123L;
			var timezoneId = 542L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, timezoneId: timezoneId);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateUser_with_status()
		{
			// Arrange
			var userId = 123L;
			var status = UserStatus.Active;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, status: status);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateUser_with_clientid()
		{
			// Arrange
			var userId = 123L;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/SetInfo")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.UpdateAsync(USER_KEY, userId, clientId: CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task Login_with_minimal_parameters()
		{
			// Arrange
			var email = "admin@fictitiouscompany.com";
			var password = "abc123";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"client_id\":\"12345\",\"client_key\":\"... client key ...\",\"user_id\":\"111\",\"user_key\":\"{0}\",\"first_name\":\"Bob\",\"last_name\":\"Smith\",\"language\":\"en_US\",\"timezone\":\"UTC\",\"last_activity\":\"2015-04-15 18:53:32\",\"client_lineage\":\"1-12345\"}}}}", USER_KEY);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/Login")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.LoginAsync(email, password);

			// Assert
			result.ShouldNotBeNull();
			result.UserKey.ShouldBe(USER_KEY);
		}

		[Fact]
		public async Task Login_with_clientid()
		{
			// Arrange
			var email = "admin@fictitiouscompany.com";
			var password = "abc123";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"client_id\":\"12345\",\"client_key\":\"... client key ...\",\"user_id\":\"111\",\"user_key\":\"{0}\",\"first_name\":\"Bob\",\"last_name\":\"Smith\",\"language\":\"en_US\",\"timezone\":\"UTC\",\"last_activity\":\"2015-04-15 18:53:32\",\"client_lineage\":\"1-12345\"}}}}", USER_KEY);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("User/Login")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Users.LoginAsync(email, password, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.UserKey.ShouldBe(USER_KEY);
		}
	}
}
