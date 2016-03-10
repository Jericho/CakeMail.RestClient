using CakeMail.RestClient.Models;
using System;
using System.Linq;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class UsersTests
	{
		public static void ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing USERS methods...");

			var users = api.Users.GetUsers(userKey, UserStatus.Active, null, null, clientId);
			Console.WriteLine("All users retrieved. Count = {0}", users.Count());

			var usersCount = api.Users.GetCount(userKey, UserStatus.Active, clientId);
			Console.WriteLine("Users count = {0}", usersCount);

			var userId = api.Users.Create(userKey, string.Format("bogus{0:00}@dummy.com", usersCount), "Integration", "Testing", "Test", "4045555555", "7701234567", "en_US", "dummy_password", 542, clientId);
			Console.WriteLine("New user created. Id: {0}", userId);

			var user = api.Users.Get(userKey, userId, clientId);
			Console.WriteLine("User retrieved: Name = {0} {1}", user.FirstName, user.LastName);

			var deactivated = api.Users.Deactivate(userKey, userId, clientId);
			Console.WriteLine("User deactivate: {0}", deactivated ? "success" : "failed");

			var deleted = api.Users.Delete(userKey, userId, clientId);
			Console.WriteLine("User deleted: {0}", deleted ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}