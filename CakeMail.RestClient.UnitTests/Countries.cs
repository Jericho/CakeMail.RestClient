using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class CountriesTests
	{
		private const string API_KEY = "...dummy API key...";

		[TestMethod]
		public void GetCountries()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Country/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"countries\":[{\"id\":\"f1\",\"en_name\":\"Fictitious Country 1\",\"fr_name\":\"Pays fictif 1\"},{\"id\":\"f2\",\"en_name\":\"Fictitious Country 2\",\"fr_name\":\"Pays fictif 2\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Countries.GetList();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.IsTrue(result.Any(tz => tz.Id == "f1" && tz.EnglishName.Equals("Fictitious Country 1")));
			Assert.IsTrue(result.Any(tz => tz.Id == "f2" && tz.EnglishName.Equals("Fictitious Country 2")));
		}

		[TestMethod]
		public void GetProvinces()
		{
			// Arrange
			var countryId = "f1";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/Country/GetProvinces/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "country_id" && (string)p.Value == countryId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"provinces\":[{\"id\":\"p1\",\"en_name\":\"Fictitious Province 1\",\"fr_name\":\"Province fictive 1\"},{\"id\":\"p2\",\"en_name\":\"Fictitious Province 2\",\"fr_name\":\"Province fictive 2\"}]}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.Countries.GetProvinces(countryId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.IsTrue(result.Any(tz => tz.Id == "p1" && tz.EnglishName.Equals("Fictitious Province 1")));
			Assert.IsTrue(result.Any(tz => tz.Id == "p2" && tz.EnglishName.Equals("Fictitious Province 2")));
		}
	}
}
