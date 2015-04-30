using CakeMail.RestClient;
using CakeMail.RestClient.Models;
using System;
using System.Linq;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class ClientsTests
	{
		private const int UTC_TIMEZONE_ID = 542;

		public static void ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing CLIENTS methods...");

			var confirmation = api.CreateClient(clientId, "_Integration Testing", "123 1st Street", "Suite 123", "Atlanta", "GA", "12345", "us", "www.company.com", "1-888-myphone", "1-888myfax", "admin3@integrationtesting.com", "Admin", "Integration Testing", "Super Administrator", "1-888-AdminPhone", "1-888-AdminMobile", "en_US", UTC_TIMEZONE_ID, "adminpassword", true);
			Console.WriteLine("New client created. Confirmation code: {0}", confirmation);

			var unconfirmedClient = api.GetClient(userKey, confirmation);
			Console.WriteLine("Information about this unconfirmed client: Name = {0}", unconfirmedClient.Name);

			var registrationInfo = api.ConfirmClient(confirmation);
			Console.WriteLine("Client has been confirmed. Id = {0}", registrationInfo.ClientId);

			var clients = api.GetClients(userKey, null, null, ClientsSortBy.CompanyName, SortDirection.Ascending, null, null, clientId);
			Console.WriteLine("All clients retrieved. Count = {0}", clients.Count());

			var clientsCount = api.GetClientsCount(userKey, null, null, clientId);
			Console.WriteLine("Clients count = {0}", clientsCount);

			var updated = api.UpdateClient(userKey, registrationInfo.ClientId, name: "Fictitious Company");
			Console.WriteLine("Client updated: {0}", updated ? "success" : "failed");

			var client = api.GetClient(userKey, registrationInfo.ClientId);
			Console.WriteLine("Client retrieved: Name = {0}", client.Name);

			var suspended = api.SuspendClient(userKey, client.Id);
			Console.WriteLine("Client suspended: {0}", suspended ? "success" : "failed");

			var reactivated = api.ActivateClient(userKey, client.Id);
			Console.WriteLine("Client re-activated: {0}", reactivated ? "success" : "failed");

			var deleted = api.DeleteClient(userKey, client.Id);
			Console.WriteLine("Client deleted: {0}", deleted ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}