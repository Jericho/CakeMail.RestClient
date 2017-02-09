using CakeMail.RestClient.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public class Countries
	{
		#region Fields

		private readonly CakeMailRestClient _cakeMailRestClient;

		#endregion

		#region Constructor

		public Countries(CakeMailRestClient cakeMailRestClient)
		{
			_cakeMailRestClient = cakeMailRestClient;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Get the list of countries
		/// </summary>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="Country">countries</see></returns>
		public Task<IEnumerable<Country>> GetListAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/Country/GetList";

			return _cakeMailRestClient.ExecuteArrayRequestAsync<Country>(path, null, "countries", cancellationToken);
		}

		/// <summary>
		/// Get the list of state/provinces for a given country
		/// </summary>
		/// <param name="countryId">ID of the country.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="Province">privinces</see></returns>
		public Task<IEnumerable<Province>> GetProvincesAsync(string countryId, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/Country/GetProvinces";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("country_id", countryId)
			};

			return _cakeMailRestClient.ExecuteArrayRequestAsync<Province>(path, parameters, "provinces", cancellationToken);
		}

		#endregion
	}
}
