using CakeMail.RestClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public interface ICountries
	{
		Task<Country[]> GetListAsync(CancellationToken cancellationToken = default(CancellationToken));
		Task<Province[]> GetProvincesAsync(string countryId, CancellationToken cancellationToken = default(CancellationToken));
	}
}
