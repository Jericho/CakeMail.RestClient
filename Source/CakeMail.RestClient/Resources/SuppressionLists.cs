using CakeMail.RestClient.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public class SuppressionLists
	{
		#region Fields

		private readonly CakeMailRestClient _cakeMailRestClient;

		#endregion

		#region Constructor

		public SuppressionLists(CakeMailRestClient cakeMailRestClient)
		{
			_cakeMailRestClient = cakeMailRestClient;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Add email addresses to the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="emailAddresses">The email addresses to add to the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="SuppressEmailResult">results</see>. Each item in this enumeration indicates the result of adding an email address to the suppression list.</returns>
		public Task<IEnumerable<SuppressEmailResult>> AddEmailAddressesAsync(string userKey, IEnumerable<string> emailAddresses, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/SuppressionList/ImportEmails/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (emailAddresses != null)
			{
				foreach (var item in emailAddresses.Select((email, i) => new { Index = i, Email = email }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("email[{0}]", item.Index), item.Email));
				}
			}

			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteArrayRequestAsync<SuppressEmailResult>(path, parameters, null, cancellationToken);
		}

		/// <summary>
		/// Add domains to the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="domains">The domains to add to the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="SuppressDomainResult">results</see>. Each item in this enumeration indicates the result of adding a domain to the suppression list.</returns>
		public Task<IEnumerable<SuppressDomainResult>> AddDomainsAsync(string userKey, IEnumerable<string> domains, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/SuppressionList/ImportDomains/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (domains != null)
			{
				foreach (var item in domains.Select((domain, i) => new { Index = i, Domain = domain }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("domain[{0}]", item.Index), item.Domain));
				}
			}

			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteArrayRequestAsync<SuppressDomainResult>(path, parameters, null, cancellationToken);
		}

		/// <summary>
		/// Add localparts to the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="localParts">The localparts to add to the suppression list</param>
		/// <param name="clientId">Client ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="SuppressLocalPartResult">results</see>. Each item in this enumeration indicates the result of adding a localpart to the suppression list.</returns>
		public Task<IEnumerable<SuppressLocalPartResult>> AddLocalPartsAsync(string userKey, IEnumerable<string> localParts, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/SuppressionList/ImportLocalparts/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (localParts != null)
			{
				foreach (var item in localParts.Select((localPart, i) => new { Index = i, LocalPart = localPart }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("localpart[{0}]", item.Index), item.LocalPart));
				}
			}

			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteArrayRequestAsync<SuppressLocalPartResult>(path, parameters, "localparts", cancellationToken);
		}

		/// <summary>
		/// Remove email addresses from the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="emailAddresses">The email addresses to remove from the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="SuppressEmailResult">results</see>. Each item in this enumeration indicates the result of removing an email address from the suppression list.</returns>
		public Task<IEnumerable<SuppressEmailResult>> RemoveEmailAddressesAsync(string userKey, IEnumerable<string> emailAddresses, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/SuppressionList/DeleteEmails/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (emailAddresses != null)
			{
				foreach (var item in emailAddresses.Select((email, i) => new { Index = i, Email = email }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("email[{0}]", item.Index), item.Email));
				}
			}

			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteArrayRequestAsync<SuppressEmailResult>(path, parameters, null, cancellationToken);
		}

		/// <summary>
		/// Remove domains from the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="domains">The domains to remove from the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="SuppressDomainResult">results</see>. Each item in this enumeration indicates the result of removing a domain from the suppression list.</returns>
		public Task<IEnumerable<SuppressDomainResult>> RemoveDomainsAsync(string userKey, IEnumerable<string> domains, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/SuppressionList/DeleteDomains/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (domains != null)
			{
				foreach (var item in domains.Select((domain, i) => new { Index = i, Domain = domain }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("domain[{0}]", item.Index), item.Domain));
				}
			}

			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteArrayRequestAsync<SuppressDomainResult>(path, parameters, null, cancellationToken);
		}

		/// <summary>
		/// Remove localparts from the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="localParts">The localparts to remove from the suppression list</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="SuppressLocalPartResult">results</see>. Each item in this enumeration indicates the result of removing a localpart from the suppression list.</returns>
		public Task<IEnumerable<SuppressLocalPartResult>> RemoveLocalPartsAsync(string userKey, IEnumerable<string> localParts, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/SuppressionList/DeleteLocalparts/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey)
			};
			if (localParts != null)
			{
				foreach (var item in localParts.Select((localPart, i) => new { Index = i, LocalPart = localPart }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("localpart[{0}]", item.Index), item.LocalPart));
				}
			}

			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteArrayRequestAsync<SuppressLocalPartResult>(path, parameters, "localparts", cancellationToken);
		}

		/// <summary>
		/// Retrieve the email addresses on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="limit">Limit the number of resulting email addresses.</param>
		/// <param name="offset">Offset the beginning of resulting email addresses.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="SuppressedEmail">addresses</see>. The result also indicates how each email address ended up on the suppression list.</returns>
		public Task<IEnumerable<SuppressedEmail>> GetEmailAddressesAsync(string userKey, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/SuppressionList/ExportEmails/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteArrayRequestAsync<SuppressedEmail>(path, parameters, "emails", cancellationToken);
		}

		/// <summary>
		/// Retrieve the domains on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="limit">Limit the number of resulting domains.</param>
		/// <param name="offset">Offset the beginning of resulting domains.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of domains.</returns>
		public async Task<IEnumerable<string>> GetDomainsAsync(string userKey, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/SuppressionList/ExportDomains/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			var result = await _cakeMailRestClient.ExecuteArrayRequestAsync<ExpandoObject>(path, parameters, "domains", cancellationToken).ConfigureAwait(false);

			var domains = (from r in result select r.Single(p => p.Key == "domain").Value.ToString()).ToArray();
			return domains;
		}

		/// <summary>
		/// Retrieve the localparts on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="limit">Limit the number of resulting localparts.</param>
		/// <param name="offset">Offset the beginning of resulting localparts.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of localparts.</returns>
		public async Task<IEnumerable<string>> GetLocalPartsAsync(string userKey, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/SuppressionList/ExportLocalparts/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			var result = await _cakeMailRestClient.ExecuteArrayRequestAsync<ExpandoObject>(path, parameters, "localparts", cancellationToken).ConfigureAwait(false);

			var localParts = (from r in result select r.Single(p => p.Key == "localpart").Value.ToString()).ToArray();
			return localParts;
		}

		/// <summary>
		/// Get a count of email addresses on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of email addresses on the suppresssion list</returns>
		public Task<long> GetEmailAddressesCountAsync(string userKey, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/SuppressionList/ExportEmails/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteCountRequestAsync(path, parameters, cancellationToken);
		}

		/// <summary>
		/// Get a count of domains on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of domains on the suppresssion list</returns>
		public Task<long> GetDomainsCountAsync(string userKey, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/SuppressionList/ExportDomains/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteCountRequestAsync(path, parameters, cancellationToken);
		}

		/// <summary>
		/// Get a count of localparts on the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of localparts on the suppresssion list</returns>
		public Task<long> GetLocalPartsCountAsync(string userKey, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/SuppressionList/ExportLocalparts/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteCountRequestAsync(path, parameters, cancellationToken);
		}

		#endregion
	}
}
