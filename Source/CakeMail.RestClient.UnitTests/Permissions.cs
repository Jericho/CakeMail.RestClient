using RichardSzalay.MockHttp;
using Shouldly;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CakeMail.RestClient.UnitTests
{
	public class PermissionsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[Fact]
		public async Task SetPermissions_with_minimal_parameters()
		{
			// Arrange
			var userId = 1234L;
			var permissions = new string[] { "FirstPermission", "SecondPermission", "ThirdPermission" };
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("/Permission/SetPermissions/")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Permissions.SetUserPermissionsAsync(USER_KEY, userId, permissions, null);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task SetPermissions_with_customerid()
		{
			// Arrange
			var userId = 1234L;
			var permissions = new string[] { "FirstPermission", "SecondPermission", "ThirdPermission" };
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("/Permission/SetPermissions/")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Permissions.SetUserPermissionsAsync(USER_KEY, userId, permissions, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetPermissions_with_minimal_parameters()
		{
			// Arrange
			var userId = 1234L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"permissions\":[\"FirstPermission\",\"SecondPermission\",\"ThirdPermission\"],\"user_id\":\"{0}\"}}}}", userId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("/Permission/GetPermissions/")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Permissions.GetUserPermissionsAsync(USER_KEY, userId);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[Fact]
		public async Task GetPermissions_with_clientid()
		{
			// Arrange
			var userId = 1234L;
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"permissions\":[\"FirstPermission\",\"SecondPermission\",\"ThirdPermission\"],\"user_id\":\"{0}\"}}}}", userId);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("/Permission/GetPermissions/")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Permissions.GetUserPermissionsAsync(USER_KEY, userId, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}
	}
}
