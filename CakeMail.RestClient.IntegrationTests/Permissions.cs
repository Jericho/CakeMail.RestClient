using CakeMail.RestClient.Models;
using System;
using System.Linq;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class PermissionsTests
	{
		public static void ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing PERMISSIONS methods...");

			var users = api.Users.GetUsers(userKey, UserStatus.Active, null, null, clientId);
			Console.WriteLine("All users retrieved");

			var user = users.First();
			Console.WriteLine("For testing purposes, we selected {0} {1}", user.FirstName, user.LastName);

			var originalUserPermissions = api.Permissions.GetUserPermissions(userKey, user.Id, clientId);
			Console.WriteLine("Current user permissions: {0}", string.Join(", ", originalUserPermissions));

			var updated = api.Permissions.SetUserPermissions(userKey, user.Id, new[] { "admin_settings" }, clientId);
			Console.WriteLine("Permissions updated: {0}", updated ? "success" : "failed");

			var newUserPermissions = api.Permissions.GetUserPermissions(userKey, user.Id, clientId);
			Console.WriteLine("New user permissions: {0}", string.Join(", ", newUserPermissions));

			updated = api.Permissions.SetUserPermissions(userKey, user.Id, originalUserPermissions, clientId);
			Console.WriteLine("Permissions reset to original values: {0}", updated ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}