using CakeMail.RestClient.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class UsersTests
	{
		public static async Task ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing USERS methods...");

			var users = await api.Users.GetUsersAsync(userKey, UserStatus.Active, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("All users retrieved. Count = {0}", users.Count());

			var usersCount = await api.Users.GetCountAsync(userKey, UserStatus.Active, clientId).ConfigureAwait(false);
			Console.WriteLine("Users count = {0}", usersCount);

			var userId = await api.Users.CreateAsync(userKey, string.Format("bogus{0:00}@dummy.com", usersCount), "Integration", "Testing", "Test", "4045555555", "7701234567", "en_US", "dummy_password", 542, clientId).ConfigureAwait(false);
			Console.WriteLine("New user created. Id: {0}", userId);

			var user = await api.Users.GetAsync(userKey, userId, clientId).ConfigureAwait(false);
			Console.WriteLine("User retrieved: Name = {0} {1}", user.FirstName, user.LastName);

			var deactivated = await api.Users.DeactivateAsync(userKey, userId, clientId).ConfigureAwait(false);
			Console.WriteLine("User deactivate: {0}", deactivated ? "success" : "failed");

			var deleted = await api.Users.DeleteAsync(userKey, userId, clientId).ConfigureAwait(false);
			Console.WriteLine("User deleted: {0}", deleted ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}
