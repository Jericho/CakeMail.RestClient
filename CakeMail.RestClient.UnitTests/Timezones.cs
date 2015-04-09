using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class TimezonesTests
	{
		private const string API_KEY = "...dummy API key...";

		[TestMethod]
		public void GetTimezones()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Client/GetTimezones/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"timezones\":[{\"id\":\"152\",\"name\":\"America/Montreal\"},{\"id\":\"532\",\"name\":\"US/Central\"},{\"id\":\"542\",\"name\":\"UTC\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTimezones();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
			Assert.IsTrue(result.Any(tz => tz.Id == 152 && tz.Name.Contains("Montreal")));
			Assert.IsTrue(result.Any(tz => tz.Id == 532 && tz.Name.Contains("Central")));
			Assert.IsTrue(result.Any(tz => tz.Id == 542 && tz.Name.Contains("UTC")));
		}
	}
}
