using CakeMail.RestClient.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public interface ISuppressionLists
	{
		Task<SuppressDomainResult[]> AddDomainsAsync(string userKey, IEnumerable<string> domains, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<SuppressEmailResult[]> AddEmailAddressesAsync(string userKey, IEnumerable<string> emailAddresses, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<SuppressLocalPartResult[]> AddLocalPartsAsync(string userKey, IEnumerable<string> localParts, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<string[]> GetDomainsAsync(string userKey, int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetDomainsCountAsync(string userKey, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<SuppressedEmail[]> GetEmailAddressesAsync(string userKey, int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetEmailAddressesCountAsync(string userKey, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<string[]> GetLocalPartsAsync(string userKey, int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetLocalPartsCountAsync(string userKey, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<SuppressDomainResult[]> RemoveDomainsAsync(string userKey, IEnumerable<string> domains, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<SuppressEmailResult[]> RemoveEmailAddressesAsync(string userKey, IEnumerable<string> emailAddresses, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<SuppressLocalPartResult[]> RemoveLocalPartsAsync(string userKey, IEnumerable<string> localParts, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
	}
}
