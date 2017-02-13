using CakeMail.RestClient.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class ClientsTests
	{
		private const int UTC_TIMEZONE_ID = 542;

		public static async Task ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing CLIENTS methods...");

			var clientsCount = await api.Clients.GetCountAsync(userKey, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("Clients count = {0}", clientsCount);

			var adminEmail = string.Format("admin{0:00}+{1:0000}@integrationtesting.com", clientsCount, (new Random()).Next(9999));
			var confirmation = await api.Clients.CreateAsync(clientId, "_Integration Testing", "123 1st Street", "Suite 123", "Atlanta", "GA", "12345", "us", "www.company.com", "1-888-myphone", "1-888myfax", adminEmail, "Admin", "Integration Testing", "Super Administrator", "1-888-AdminPhone", "1-888-AdminMobile", "en_US", UTC_TIMEZONE_ID, "adminpassword", true).ConfigureAwait(false);
			Console.WriteLine("New client created. Confirmation code: {0}", confirmation);

			var unconfirmedClient = await api.Clients.GetAsync(userKey, confirmation).ConfigureAwait(false);
			Console.WriteLine("Information about this unconfirmed client: Name = {0}", unconfirmedClient.Name);

			var registrationInfo = await api.Clients.ConfirmAsync(confirmation).ConfigureAwait(false);
			Console.WriteLine("Client has been confirmed. Id = {0}", registrationInfo.ClientId);

			var clients = await api.Clients.GetListAsync(userKey, null, null, ClientsSortBy.CompanyName, SortDirection.Ascending, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("All clients retrieved. Count = {0}", clients.Count());

			var updated = await api.Clients.UpdateAsync(userKey, registrationInfo.ClientId, name: "Fictitious Company").ConfigureAwait(false);
			Console.WriteLine("Client updated: {0}", updated ? "success" : "failed");

			var client = await api.Clients.GetAsync(userKey, registrationInfo.ClientId).ConfigureAwait(false);
			Console.WriteLine("Client retrieved: Name = {0}", client.Name);

			var suspended = await api.Clients.SuspendAsync(userKey, client.Id).ConfigureAwait(false);
			Console.WriteLine("Client suspended: {0}", suspended ? "success" : "failed");

			var reactivated = await api.Clients.ActivateAsync(userKey, client.Id).ConfigureAwait(false);
			Console.WriteLine("Client re-activated: {0}", reactivated ? "success" : "failed");

			var deleted = await api.Clients.DeleteAsync(userKey, client.Id).ConfigureAwait(false);
			Console.WriteLine("Client deleted: {0}", deleted ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}
