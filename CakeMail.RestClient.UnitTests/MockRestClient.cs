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

		public MockRestClient(string resource, IEnumerable<Parameter> parameters, string jsonResponse) : base(MockBehavior.Strict)
		{
			var standardParams = new[]
			{
				new Parameter { Type = ParameterType.HttpHeader, Name = "apikey", Value = API_KEY },
				new Parameter { Type = ParameterType.GetOrPost, Name = "user_key", Value = USER_KEY }
			};
			var expectedParameters = parameters.Union(standardParams);

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

			// We only care about Name, Value and Type. Note: ordering is important for SequenceEqual
			var p1 = expectedParameters.Select(e => new { e.Name, e.Value, e.Type }).OrderBy(p => p.Name).ToArray();
			var p2 = actualParameters.Select(e => new { e.Name, e.Value, e.Type }).OrderBy(p => p.Name).ToArray();
			if (p1.SequenceEqual(p2)) return true;

			// The following is mostly for debugging 
			// It helps determine why the sequences are not equal
			foreach (var expectedParam in expectedParameters)
			{
				var actualParam = actualParameters.Where(a => a.Name == expectedParam.Name);
				if (!actualParameters.Any()) return false;
				if (actualParam.Single() != expectedParam) return false;
			}

			return true;
		}
	}
}
