using CakeMail.RestClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public interface IUsers
	{
		Task<long> CreateAsync(string userKey, string email, string password, string firstName = null, string lastName = null, string title = null, string officePhone = null, string mobilePhone = null, string language = null, long timezoneId = 542, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> DeactivateAsync(string userKey, long userId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> DeleteAsync(string userKey, long userId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<User> GetAsync(string userKey, long userId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetCountAsync(string userKey, UserStatus? status = default(UserStatus?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<User[]> GetUsersAsync(string userKey, UserStatus? status = default(UserStatus?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<LoginInfo> LoginAsync(string email, string password, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> UpdateAsync(string userKey, long userId, string email = null, string password = null, string firstName = null, string lastName = null, string title = null, string officePhone = null, string mobilePhone = null, string language = null, long? timezoneId = default(long?), UserStatus? status = default(UserStatus?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
	}
}
