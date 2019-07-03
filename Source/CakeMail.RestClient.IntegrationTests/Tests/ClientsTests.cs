using CakeMail.RestClient.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests.Tests
{
	public class ClientsTests : IIntegrationTest
	{
		private const int UTC_TIMEZONE_ID = 542;

		public async Task Execute(ICakeMailRestClient client, string userKey, long clientId, TextWriter log, CancellationToken cancellationToken)
		{
			await log.WriteLineAsync("\n***** CLIENT *****").ConfigureAwait(false);

			var clientsCount = await client.Clients.GetCountAsync(userKey, null, null, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Clients count = {clientsCount}").ConfigureAwait(false);

			var adminEmail = string.Format("admin{0:00}+{1:0000}@integrationtesting.com", clientsCount, (new Random()).Next(9999));
			var confirmation = await client.Clients.CreateAsync(clientId, "_Integration Testing", "123 1st Street", "Suite 123", "Atlanta", "GA", "12345", "us", "www.company.com", "1-888-myphone", "1-888myfax", adminEmail, "Admin", "Integration Testing", "Super Administrator", "1-888-AdminPhone", "1-888-AdminMobile", "en_US", UTC_TIMEZONE_ID, "adminpassword", true, cancellationToken: cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"New client created. Confirmation code: {confirmation}").ConfigureAwait(false);

			var unconfirmedClient = await client.Clients.GetAsync(userKey, confirmation, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Information about this unconfirmed client: Name = {unconfirmedClient.Name}").ConfigureAwait(false);

			var registrationInfo = await client.Clients.ConfirmAsync(confirmation, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Client has been confirmed. Id = {registrationInfo.ClientId}").ConfigureAwait(false);

			var clients = await client.Clients.GetListAsync(userKey, null, null, ClientsSortBy.CompanyName, SortDirection.Ascending, null, null, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"All clients retrieved. Count = {clients.Count()}").ConfigureAwait(false);

			var updated = await client.Clients.UpdateAsync(userKey, registrationInfo.ClientId, name: "Fictitious Company", cancellationToken: cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Client updated: {(updated ? "success" : "failed")}").ConfigureAwait(false);

			var myClient = await client.Clients.GetAsync(userKey, registrationInfo.ClientId, cancellationToken: cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Client retrieved: Name = {myClient.Name}").ConfigureAwait(false);

			var suspended = await client.Clients.SuspendAsync(userKey, myClient.Id, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Client suspended: {(suspended ? "success" : "failed")}").ConfigureAwait(false);

			var reactivated = await client.Clients.ActivateAsync(userKey, myClient.Id, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Client re-activated: {(reactivated ? "success" : "failed")}").ConfigureAwait(false);

			var deleted = await client.Clients.DeleteAsync(userKey, myClient.Id, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Client deleted: {(deleted ? "success" : "failed")}").ConfigureAwait(false);
		}
	}
}
