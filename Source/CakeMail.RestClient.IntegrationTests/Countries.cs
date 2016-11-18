using System;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class CountriesTests
	{
		public static async Task ExecuteAllMethods(CakeMailRestClient api)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing COUNTRIES methods...");

			var countries = await api.Countries.GetListAsync().ConfigureAwait(false);
			Console.WriteLine("Retrieved all countries. There are {0} countries.", countries.Count());

			var canada = countries.Single(country => country.EnglishName == "Canada");
			Console.WriteLine("Canada --> Id: {0}", canada.Id);

			var canadianProvinces = await api.Countries.GetProvincesAsync(canada.Id).ConfigureAwait(false);
			Console.WriteLine("There are {0} canadian provinces/territories/etc.", canadianProvinces.Count());

			var quebec = canadianProvinces.Single(province => province.EnglishName == "Quebec");
			Console.WriteLine("Quebec --> Id: {0}", quebec.Id);

			var usa = countries.Single(country => country.EnglishName == "United States");
			Console.WriteLine("USA --> Id: {0}", usa.Id);

			var americanStates = await api.Countries.GetProvincesAsync(usa.Id).ConfigureAwait(false);
			Console.WriteLine("There are {0} american states/territories/etc.", americanStates.Count());

			var georgia = americanStates.Single(province => province.EnglishName == "Georgia");
			Console.WriteLine("Georgia --> Id: {0}", georgia.Id);

			var florida = americanStates.Single(province => province.EnglishName == "Florida");
			Console.WriteLine("Florida --> Id: {0}", florida.Id);

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}
