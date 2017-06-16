using CakeMail.RestClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public interface ISegments
	{
		Task<long> CreateAsync(string userKey, long listId, string name, string query = null, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> DeleteAsync(string userKey, long segmentId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Segment> GetAsync(string userKey, long segmentId, bool includeStatistics = true, bool calculateEngagement = false, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetCountAsync(string userKey, long listId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Segment[]> GetSegmentsAsync(string userKey, long listId, int? limit = 0, int? offset = 0, bool includeDetails = true, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> UpdateAsync(string userKey, long segmentId, long listId, string name = null, string query = null, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
	}
}
