using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using Pathoschild.Http.Client;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Alows you to manage the list of countries.
	/// </summary>
	/// <seealso cref="CakeMail.RestClient.Resources.ICountries" />
	public class Countries : ICountries
	{
		#region Fields

		private readonly IClient _client;

		#endregion

		#region Constructor

		internal Countries(IClient client)
		{
			_client = client;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Get the list of countries.
		/// </summary>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>An enumeration of <see cref="Country">countries</see>.</returns>
		public Task<Country[]> GetListAsync(CancellationToken cancellationToken = default)
		{
			return _client
				.PostAsync("Country/GetList")
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<Country[]>("countries");
		}

		/// <summary>
		/// Get the list of state/provinces for a given country.
		/// </summary>
		/// <param name="countryId">ID of the country.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>An enumeration of <see cref="Province">privinces</see>.</returns>
		public Task<Province[]> GetProvincesAsync(string countryId, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("country_id", countryId)
			};

			return _client
				.PostAsync("Country/GetProvinces")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<Province[]>("provinces");
		}

		#endregion
	}
}
