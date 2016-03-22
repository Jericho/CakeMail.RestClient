using CakeMail.RestClient.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class PermissionsTests
	{
		public static async Task ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing PERMISSIONS methods...");

			var users = await api.Users.GetUsersAsync(userKey, UserStatus.Active, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("All users retrieved");

			var user = users.First();
			Console.WriteLine("For testing purposes, we selected {0} {1}", user.FirstName, user.LastName);

			var originalUserPermissions = await api.Permissions.GetUserPermissionsAsync(userKey, user.Id, clientId).ConfigureAwait(false);
			Console.WriteLine("Current user permissions: {0}", string.Join(", ", originalUserPermissions));

			var updated = await api.Permissions.SetUserPermissionsAsync(userKey, user.Id, new[] { "admin_settings" }, clientId).ConfigureAwait(false);
			Console.WriteLine("Permissions updated: {0}", updated ? "success" : "failed");

			var newUserPermissions = await api.Permissions.GetUserPermissionsAsync(userKey, user.Id, clientId).ConfigureAwait(false);
			Console.WriteLine("New user permissions: {0}", string.Join(", ", newUserPermissions));

			updated = await api.Permissions.SetUserPermissionsAsync(userKey, user.Id, originalUserPermissions, clientId).ConfigureAwait(false);
			Console.WriteLine("Permissions reset to original values: {0}", updated ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}
