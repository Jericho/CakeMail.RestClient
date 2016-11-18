using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class CountriesTests
	{
		private const string API_KEY = "...dummy API key...";

		[TestMethod]
		public async Task GetCountries()
		{
			// Arrange
			var parameters = new Parameter[]
			{
				// There are no parameters
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"countries\":[{\"id\":\"f1\",\"en_name\":\"Fictitious Country 1\",\"fr_name\":\"Pays fictif 1\"},{\"id\":\"f2\",\"en_name\":\"Fictitious Country 2\",\"fr_name\":\"Pays fictif 2\"}]}}";
			var mockRestClient = new MockRestClient("/Country/GetList/", parameters, jsonResponse, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Countries.GetListAsync();

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.Any(tz => tz.Id == "f1" && tz.EnglishName.Equals("Fictitious Country 1")).ShouldBeTrue();
			result.Any(tz => tz.Id == "f2" && tz.EnglishName.Equals("Fictitious Country 2")).ShouldBeTrue();
		}

		[TestMethod]
		public async Task GetProvinces()
		{
			// Arrange
			var countryId = "f1";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "country_id", Value = countryId }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"provinces\":[{\"id\":\"p1\",\"en_name\":\"Fictitious Province 1\",\"fr_name\":\"Province fictive 1\"},{\"id\":\"p2\",\"en_name\":\"Fictitious Province 2\",\"fr_name\":\"Province fictive 2\"}]}}";
			var mockRestClient = new MockRestClient("/Country/GetProvinces/", parameters, jsonResponse, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Countries.GetProvincesAsync(countryId);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.Any(tz => tz.Id == "p1" && tz.EnglishName.Equals("Fictitious Province 1")).ShouldBeTrue();
			result.Any(tz => tz.Id == "p2" && tz.EnglishName.Equals("Fictitious Province 2")).ShouldBeTrue();
		}
	}
}
