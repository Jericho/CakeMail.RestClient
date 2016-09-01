using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class PermissionsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[TestMethod]
		public async Task SetPermissions_with_minimal_parameters()
		{
			// Arrange
			var userId = 1234L;
			var permissions = new string[] { "FirstPermission", "SecondPermission", "ThirdPermission" };
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "permission[0]", Value = permissions[0] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "permission[1]", Value = permissions[1] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "permission[2]", Value = permissions[2] }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Permission/SetPermissions/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Permissions.SetUserPermissionsAsync(USER_KEY, userId, permissions, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SetPermissions_with_customerid()
		{
			// Arrange
			var userId = 1234L;
			var permissions = new string[] { "FirstPermission", "SecondPermission", "ThirdPermission" };
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "permission[0]", Value = permissions[0] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "permission[1]", Value = permissions[1] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "permission[2]", Value = permissions[2] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/Permission/SetPermissions/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Permissions.SetUserPermissionsAsync(USER_KEY, userId, permissions, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetPermissions_with_minimal_parameters()
		{
			// Arrange
			var userId = 1234L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"permissions\":[\"FirstPermission\",\"SecondPermission\",\"ThirdPermission\"],\"user_id\":\"{0}\"}}}}", userId);
			var mockRestClient = new MockRestClient("/Permission/GetPermissions/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Permissions.GetUserPermissionsAsync(USER_KEY, userId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public async Task GetPermissions_with_clientid()
		{
			// Arrange
			var userId = 1234L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_id", Value = userId },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"permissions\":[\"FirstPermission\",\"SecondPermission\",\"ThirdPermission\"],\"user_id\":\"{0}\"}}}}", userId);
			var mockRestClient = new MockRestClient("/Permission/GetPermissions/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Permissions.GetUserPermissionsAsync(USER_KEY, userId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}
	}
}
