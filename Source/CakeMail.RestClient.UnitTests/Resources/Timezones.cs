using RichardSzalay.MockHttp;
using Shouldly;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Resources
{
	public class TimezonesTests
	{
		private const string API_KEY = "...dummy API key...";

		[Fact]
		public async Task GetTimezones()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"timezones\":[{\"id\":\"152\",\"name\":\"America/Montreal\"},{\"id\":\"532\",\"name\":\"US/Central\"},{\"id\":\"542\",\"name\":\"UTC\"}]}}";

			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("Client/GetTimezones")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
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
