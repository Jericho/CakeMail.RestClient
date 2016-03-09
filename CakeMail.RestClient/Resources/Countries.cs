using CakeMail.RestClient.Models;
using System.Collections.Generic;

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
		public IEnumerable<Country> GetList()
		{
			var path = "/Country/GetList/";

			return _cakeMailRestClient.ExecuteArrayRequest<Country>(path, null, "countries");
		}

		/// <summary>
		/// Get the list of state/provinces for a given country
		/// </summary>
		/// <param name="countryId">ID of the country.</param>
		/// <returns>An enumeration of <see cref="Province">privinces</see></returns>
		public IEnumerable<Province> GetProvinces(string countryId)
		{
			var path = "/Country/GetProvinces/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("country_id", countryId)
			};

			return _cakeMailRestClient.ExecuteArrayRequest<Province>(path, parameters, "provinces");
		}

		#endregion
	}
}
