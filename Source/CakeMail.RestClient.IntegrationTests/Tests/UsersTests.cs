using CakeMail.RestClient.Models;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests.Tests
{
	public class UsersTests : IIntegrationTest
	{
		public async Task Execute(ICakeMailRestClient client, string userKey, long clientId, TextWriter log, CancellationToken cancellationToken)
		{
			await log.WriteLineAsync("\n***** USERS *****").ConfigureAwait(false);

			var users = await client.Users.GetUsersAsync(userKey, UserStatus.Active, null, null, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"All users retrieved. Count = {users.Count()}").ConfigureAwait(false);

			var usersCount = await client.Users.GetCountAsync(userKey, UserStatus.Active, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Users count = {usersCount}").ConfigureAwait(false);

			var userId = await client.Users.CreateAsync(userKey, string.Format("bogus{0:00}@dummy.com", usersCount), "Integration", "Testing", "Test", "4045555555", "7701234567", "en_US", "dummy_password", 542, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"New user created. Id: {userId}").ConfigureAwait(false);

			var user = await client.Users.GetAsync(userKey, userId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"User retrieved: Name = {user.FirstName} {user.LastName}").ConfigureAwait(false);

			var deactivated = await client.Users.DeactivateAsync(userKey, userId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"User deactivate: {(deactivated ? "success" : "failed")}").ConfigureAwait(false);

			var deleted = await client.Users.DeleteAsync(userKey, userId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"User deleted: {(deleted ? "success" : "failed")}").ConfigureAwait(false);
		}
	}
}
