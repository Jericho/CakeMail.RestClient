using CakeMail.RestClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Alows you to manage the list of countries
	/// </summary>
	public interface ICountries
	{
		/// <summary>
		/// Get the list of countries
		/// </summary>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="Country">countries</see></returns>
		Task<Country[]> GetListAsync(CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Get the list of state/provinces for a given country
		/// </summary>
		/// <param name="countryId">ID of the country.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="Province">privinces</see></returns>
		Task<Province[]> GetProvincesAsync(string countryId, CancellationToken cancellationToken = default(CancellationToken));
	}
}
