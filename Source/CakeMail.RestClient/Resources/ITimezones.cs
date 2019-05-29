using CakeMail.RestClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage timezones.
	/// </summary>
	public interface ITimezones
	{
		/// <summary>
		/// Retrieve the list of all timezones known to the CakeMail system.
		/// </summary>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>An enumeration of all <see cref="Timezone">timezones</see>.</returns>
		Task<Timezone[]> GetAllAsync(CancellationToken cancellationToken = default);
	}
}
