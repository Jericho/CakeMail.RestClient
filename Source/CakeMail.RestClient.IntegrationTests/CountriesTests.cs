using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class CountriesTests
	{
		public static async Task ExecuteAllMethods(ICakeMailRestClient client, string userKey, long clientId, TextWriter log, CancellationToken cancellationToken)
		{
			await log.WriteLineAsync("\n***** COUNTRIES *****").ConfigureAwait(false);

			var countries = await client.Countries.GetListAsync().ConfigureAwait(false);
			await log.WriteLineAsync($"Retrieved all countries. There are {countries.Count()} countries.").ConfigureAwait(false);

			var canada = countries.Single(country => country.EnglishName == "Canada");
			await log.WriteLineAsync($"Canada --> Id: {canada.Id}").ConfigureAwait(false);

			var canadianProvinces = await client.Countries.GetProvincesAsync(canada.Id).ConfigureAwait(false);
			await log.WriteLineAsync($"There are {canadianProvinces.Count()} canadian provinces/territories/etc.").ConfigureAwait(false);

			var quebec = canadianProvinces.Single(province => province.EnglishName == "Quebec");
			await log.WriteLineAsync($"Quebec --> Id: {quebec.Id}").ConfigureAwait(false);

			var usa = countries.Single(country => country.EnglishName == "United States");
			await log.WriteLineAsync($"USA --> Id: {usa.Id}").ConfigureAwait(false);

			var americanStates = await client.Countries.GetProvincesAsync(usa.Id).ConfigureAwait(false);
			await log.WriteLineAsync($"There are {americanStates.Count()} american states/territories/etc.").ConfigureAwait(false);

			var georgia = americanStates.Single(province => province.EnglishName == "Georgia");
			await log.WriteLineAsync($"Georgia --> Id: {georgia.Id}").ConfigureAwait(false);

			var florida = americanStates.Single(province => province.EnglishName == "Florida");
			await log.WriteLineAsync($"Florida --> Id: {florida.Id}").ConfigureAwait(false);
		}
	}
}
