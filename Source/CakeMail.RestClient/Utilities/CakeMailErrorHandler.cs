using CakeMail.RestClient.Exceptions;
using Newtonsoft.Json.Linq;
using Pathoschild.Http.Client;
using Pathoschild.Http.Client.Extensibility;

namespace CakeMail.RestClient.Utilities
{
	/// <summary>
	/// Error handler for requests dispatched to the CakeMail API
	/// </summary>
	/// <seealso cref="Pathoschild.Http.Client.Extensibility.IHttpFilter" />
	public class CakeMailErrorHandler : IHttpFilter
	{
		#region PUBLIC METHODS

		/// <summary>Method invoked just before the HTTP request is submitted. This method can modify the outgoing HTTP request.</summary>
		/// <param name="request">The HTTP request.</param>
		public void OnRequest(IRequest request) { }

		/// <summary>Method invoked just after the HTTP response is received. This method can modify the incoming HTTP response.</summary>
		/// <param name="response">The HTTP response.</param>
		/// <param name="httpErrorAsException">Whether HTTP error responses should be raised as exceptions.</param>
		public void OnResponse(IResponse response, bool httpErrorAsException)
		{
			if (!response.Message.IsSuccessStatusCode)
			{
				throw new HttpException(
					$"{(int)response.Message.StatusCode}: {response.Message.ReasonPhrase}",
					response.Message.StatusCode,
					response.Message.RequestMessage.RequestUri);
			}

			/* A typical response from the CakeMail API looks like this:
			 *	{
			 *		"status" : "success",
			 *		"data" : { ... data for the API call ... }
			 *	}
			 *
			 * In case of an error, the response looks like this:
			 *	{
			 *		"status" : "failed",
			 *		"data" : "An error has occured",
			 *		"post" : "... additional information ..."	<-- This information is not always present in the response
			 *	}
			 */
			var responseContent = response.Message.Content.ReadAsStringAsync().Result;
			var cakeResponse = JObject.Parse(responseContent);
			var status = cakeResponse["status"].ToString();
			var data = cakeResponse["data"];
			var postData = cakeResponse["post"];

			if (status != "success")
			{
				if (postData != null) throw new CakeMailPostException(data.ToString(), postData.ToString());
				else throw new CakeMailException(data.ToString());
			}
		}

		#endregion
	}
}
