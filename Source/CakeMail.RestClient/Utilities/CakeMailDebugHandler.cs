using Pathoschild.Http.Client;
using Pathoschild.Http.Client.Extensibility;
using System.Diagnostics;

namespace CakeMail.RestClient.Utilities
{
	/// <summary>
	/// Handler for requests dispatched to the CakeMail API
	/// </summary>
	/// <seealso cref="Pathoschild.Http.Client.Extensibility.IHttpFilter" />
	public class CakeMailDebugHandler : IHttpFilter
	{
		#region PUBLIC METHODS

		/// <summary>Method invoked just before the HTTP request is submitted. This method can modify the outgoing HTTP request.</summary>
		/// <param name="request">The HTTP request.</param>
		public void OnRequest(IRequest request)
		{
			var debugRequestMsg = $"Request sent to CakeMail: {request.Message.RequestUri}";
			var debugParametersMsg = $"Request parameters: {request.Message.Content.ReadAsStringAsync().Result}";
			Debug.WriteLine($"{new string('=', 25)}\r\n{debugRequestMsg}\r\n{debugParametersMsg}");
		}

		/// <summary>Method invoked just after the HTTP response is received. This method can modify the incoming HTTP response.</summary>
		/// <param name="response">The HTTP response.</param>
		/// <param name="httpErrorAsException">Whether HTTP error responses should be raised as exceptions.</param>
		public void OnResponse(IResponse response, bool httpErrorAsException)
		{
			var debugResponseMsg = string.Format("Response received: {0}", response.AsString().Result);
			Debug.WriteLine($"\r\n{debugResponseMsg}\r\n{new string('=', 25)}");
		}

		#endregion
	}
}
