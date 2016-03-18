using CakeMail.RestClient.Models;
using System.Collections.Generic;
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
		/// <returns>An enumeration of <see cref="Country">countries</see></returns>
		public async Task<IEnumerable<Country>> GetListAsync()
		{
			var path = "/Country/GetList/";

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<Country>(path, null, "countries").ConfigureAwait(false);
		}

		/// <summary>
		/// Get the list of state/provinces for a given country
		/// </summary>
		/// <param name="countryId">ID of the country.</param>
		/// <returns>An enumeration of <see cref="Province">privinces</see></returns>
		public async Task<IEnumerable<Province>> GetProvincesAsync(string countryId)
		{
			var path = "/Country/GetProvinces/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("country_id", countryId)
			};

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<Province>(path, parameters, "provinces").ConfigureAwait(false);
		}

		#endregion
	}
}
