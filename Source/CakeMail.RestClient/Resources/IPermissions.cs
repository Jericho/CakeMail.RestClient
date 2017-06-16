using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public interface IPermissions
	{
		Task<string[]> GetUserPermissionsAsync(string userKey, long userId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> SetUserPermissionsAsync(string userKey, long userId, IEnumerable<string> permissions, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
	}
}