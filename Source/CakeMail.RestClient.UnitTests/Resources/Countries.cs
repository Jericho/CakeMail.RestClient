using RichardSzalay.MockHttp;
using Shouldly;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CakeMail.RestClient.UnitTests
{
	public class CountriesTests
	{
		private const string API_KEY = "...dummy API key...";

		[Fact]
		public async Task GetCountries()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"countries\":[{\"id\":\"f1\",\"en_name\":\"Fictitious Country 1\",\"fr_name\":\"Pays fictif 1\"},{\"id\":\"f2\",\"en_name\":\"Fictitious Country 2\",\"fr_name\":\"Pays fictif 2\"}]}}";

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Country/GetList")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Countries.GetListAsync();

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.Any(tz => tz.Id == "f1" && tz.EnglishName.Equals("Fictitious Country 1")).ShouldBeTrue();
			result.Any(tz => tz.Id == "f2" && tz.EnglishName.Equals("Fictitious Country 2")).ShouldBeTrue();
		}

		[Fact]
		public async Task GetProvinces()
		{
			// Arrange
			var countryId = "f1";
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"provinces\":[{\"id\":\"p1\",\"en_name\":\"Fictitious Province 1\",\"fr_name\":\"Province fictive 1\"},{\"id\":\"p2\",\"en_name\":\"Fictitious Province 2\",\"fr_name\":\"Province fictive 2\"}]}}";

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Country/GetProvinces")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Countries.GetProvincesAsync(countryId);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.Any(tz => tz.Id == "p1" && tz.EnglishName.Equals("Fictitious Province 1")).ShouldBeTrue();
			result.Any(tz => tz.Id == "p2" && tz.EnglishName.Equals("Fictitious Province 2")).ShouldBeTrue();
		}
	}
}
