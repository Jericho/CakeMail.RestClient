using CakeMail.RestClient.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public class Timezones
	{
		#region Fields

		private readonly CakeMailRestClient _cakeMailRestClient;

		#endregion

		#region Constructor

		public Timezones(CakeMailRestClient cakeMailRestClient)
		{
			_cakeMailRestClient = cakeMailRestClient;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Retrieve the list of all timezones known to the CakeMail system
		/// </summary>
		/// <returns>An enumeration of all <see cref="Timezone">timezones</see>.</returns>
		public Task<IEnumerable<Timezone>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "Client/GetTimezones";

			return _cakeMailRestClient.ExecuteArrayRequestAsync<Timezone>(path, null, "timezones", cancellationToken);
		}

		#endregion
	}
}
