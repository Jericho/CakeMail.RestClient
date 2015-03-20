using CakeMail.RestClient;
using CakeMail.RestClient.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace CakeMail.RestCLient.UnitTests
{
	[TestClass]
	public class CakeMailRestClientTests
	{
		private const string API_KEY = "...dummy API key...";

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_responsestatus_is_error()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Error
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_responsestatus_is_timeout()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.TimedOut
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_request_is_successful_but_response_content_is_empty()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = HttpStatusCode.OK,
				Content = ""
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(CakeMailException))]
		public void RestClient_Throws_exception_when_request_is_successful_but_response_contenttype_is_not_specified()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = HttpStatusCode.OK,
				Content = "dummy content"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_responsescode_is_between_400_and_499_and_content_is_empty()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = HttpStatusCode.Forbidden,
				Content = ""
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_responsescode_is_between_400_and_499_and_content_is_not_empty()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = HttpStatusCode.Forbidden,
				Content = "dummy content"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_responsescode_is_between_500_and_599()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = HttpStatusCode.InternalServerError
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(HttpException))]
		public void RestClient_Throws_exception_when_responsescode_is_greater_than_599()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				ResponseStatus = RestSharp.ResponseStatus.Completed,
				StatusCode = (HttpStatusCode)600
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}

		[TestMethod]
		[ExpectedException(typeof(CakeMailException))]
		public void RestClient_Throws_exception_when_cakemail_api_returns_failure()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Parameters.Any(p => p.Name == "apikey" && p.Value.ToString() == API_KEY) &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 0
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"failure\",\"data\":\"An error has occured\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetCountries();
		}
	}
}
