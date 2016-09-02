using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class TimezonesTests
	{
		private const string API_KEY = "...dummy API key...";

		[TestMethod]
		public async Task GetTimezones()
		{
			// Arrange
			var parameters = new Parameter[]
			{
				// There are no parameters
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"timezones\":[{\"id\":\"152\",\"name\":\"America/Montreal\"},{\"id\":\"532\",\"name\":\"US/Central\"},{\"id\":\"542\",\"name\":\"UTC\"}]}}";
			var mockRestClient = new MockRestClient("/Client/GetTimezones/", parameters, jsonResponse, false);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Timezones.GetAllAsync();

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
			result.Any(tz => tz.Id == 152 && tz.Name.Contains("Montreal")).ShouldBeTrue();
			result.Any(tz => tz.Id == 532 && tz.Name.Contains("Central")).ShouldBeTrue();
			result.Any(tz => tz.Id == 542 && tz.Name.Contains("UTC")).ShouldBeTrue();
		}
	}
}
