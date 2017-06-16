using CakeMail.RestClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public interface ITimezones
	{
		Task<Timezone[]> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}
