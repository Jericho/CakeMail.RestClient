using CakeMail.RestClient.Models;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests.Tests
{
	public static class PermissionsTests
	{
		public static async Task ExecuteAllMethods(ICakeMailRestClient client, string userKey, long clientId, TextWriter log, CancellationToken cancellationToken)
		{
			await log.WriteLineAsync("\n***** PERMISSIONS *****").ConfigureAwait(false);

			var users = await client.Users.GetUsersAsync(userKey, UserStatus.Active, null, null, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync("All users retrieved").ConfigureAwait(false);

			var user = users.First();
			await log.WriteLineAsync($"For testing purposes, we selected {user.FirstName} {user.LastName}").ConfigureAwait(false);

			var originalUserPermissions = await client.Permissions.GetUserPermissionsAsync(userKey, user.Id, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Current user permissions: {string.Join(", ", originalUserPermissions)}").ConfigureAwait(false);

			var updated = await client.Permissions.SetUserPermissionsAsync(userKey, user.Id, new[] { "admin_settings" }, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Permissions updated: {(updated ? "success" : "failed")}").ConfigureAwait(false);

			var newUserPermissions = await client.Permissions.GetUserPermissionsAsync(userKey, user.Id, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"New user permissions: {string.Join(", ", newUserPermissions)}").ConfigureAwait(false);

			updated = await client.Permissions.SetUserPermissionsAsync(userKey, user.Id, originalUserPermissions, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Permissions reset to original values: {(updated ? "success" : "failed")}").ConfigureAwait(false);
		}
	}
}
