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

		public MockRestClient(string resource, IEnumerable<Parameter> parameters, string jsonResponse, bool includeUserKeyParam = true) : base(MockBehavior.Strict)
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
			), It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = jsonResponse
			}).Verifiable();
		}

		private bool ValidateParameters(IEnumerable<Parameter> expectedParameters, IEnumerable<Parameter> actualParameters)
		{
			if (actualParameters.Count() != expectedParameters.Count()) return false;

			var p1 = expectedParameters.Select(e => new { e.Name, e.Value, e.Type }).ToArray();
			var p2 = actualParameters.Select(e => new { e.Name, e.Value, e.Type }).ToArray();

			// We only care about Name, Value and Type
			foreach (var expectedParam in p1)
			{
				var actualParam = p2.SingleOrDefault(a => a.Name == expectedParam.Name);
				if (actualParam == null) return false;
				if (!actualParam.Equals(expectedParam)) return false;
			}

			return true;
		}
	}
}
