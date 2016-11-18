using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace CakeMail.RestClient.UnitTests
{
	public class MockRestClient : Mock<IRestClient>
	{
		public const string API_KEY = "...dummy API key...";
		public const string USER_KEY = "...dummy USER key...";

		public MockRestClient(string resource, IEnumerable<Parameter> parameters, RestResponse response, bool includeUserKeyParam = true) : base(MockBehavior.Strict)
		{
			ConfigureMock(resource, parameters, response, includeUserKeyParam);
		}

		public MockRestClient(string resource, IEnumerable<Parameter> parameters, ResponseStatus responseStatus, bool includeUserKeyParam = true) : base(MockBehavior.Strict)
		{
			var response = new RestResponse
			{
				ResponseStatus = responseStatus
			};
			ConfigureMock(resource, parameters, response, includeUserKeyParam);
		}

		public MockRestClient(string resource, IEnumerable<Parameter> parameters, string jsonResponse, bool includeUserKeyParam = true) : base(MockBehavior.Strict)
		{
			var response = new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = jsonResponse
			};
			ConfigureMock(resource, parameters, response, includeUserKeyParam);
		}

		private void ConfigureMock(string resource, IEnumerable<Parameter> parameters, RestResponse response, bool includeUserKeyParam = true)
		{
			var standardParameters = new List<Parameter>();
			standardParameters.Add(new Parameter { Type = ParameterType.HttpHeader, Name = "apikey", Value = API_KEY });
			if (includeUserKeyParam) standardParameters.Add(new Parameter { Type = ParameterType.GetOrPost, Name = "user_key", Value = USER_KEY });

			var expectedParameters = parameters.Union(standardParameters.ToArray()).ToArray();

			Setup(m => m.BaseUrl).Returns(new Uri("http://localhost")).Verifiable();
			Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == resource &&
				ValidateParameters(expectedParameters, r.Parameters)
			), It.IsAny<CancellationToken>())).ReturnsAsync(response).Verifiable();
		}

		private bool ValidateParameters(IEnumerable<Parameter> expectedParameters, IEnumerable<Parameter> actualParameters)
		{
			if (actualParameters.Count() != expectedParameters.Count()) return false;

			// We only care about Name, Value and Type
			var expected = expectedParameters.Select(e => new { e.Name, e.Value, e.Type }).ToArray();
			var actual = actualParameters.Select(e => new { e.Name, e.Value, e.Type }).ToArray();

			foreach (var expectedParam in expected)
			{
				var actualParam = actual.SingleOrDefault(a => a.Name == expectedParam.Name);
				if (actualParam == null) return false;
				if (!actualParam.Equals(expectedParam)) return false;
			}

			return true;
		}
	}
}
