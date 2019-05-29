using System;
using System.IO;
using System.Net;

namespace CakeMail.RestClient.Exceptions
{
	/// <summary>
	/// This class represents an HTTP transport error. This is not an error returned
	/// by the web service itself. As such, it is a IOException.
	/// </summary>
	public class HttpException : IOException
	{
		/// <summary>
		/// Gets the HTTP status code returned by the web service.
		/// </summary>
		public HttpStatusCode HttpStatus { get; private set; }

		/// <summary>
		/// Gets the URI queried by the web service.
		/// </summary>
		public Uri Uri { get; private set; }

		/// <summary>
		/// Gets the human readable representation of the request/response.
		/// </summary>
		public string DiagnosticLog { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpException"/> class.
		/// </summary>
		/// <param name="message">A message describing the reason why the exception was thrown.</param>
		/// <param name="httpStatus">The HTTP status of the response that caused the exception.</param>
		/// <param name="uri">The URL queried.</param>
		/// <param name="diagnosticLog">The human readable representation of the request/response.</param>
		public HttpException(string message, HttpStatusCode httpStatus, Uri uri, string diagnosticLog)
			: base(message)
		{
			HttpStatus = httpStatus;
			Uri = uri;
			DiagnosticLog = diagnosticLog;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpException"/> class.
		/// </summary>
		/// <param name="message">A message describing the reason why the exception was thrown.</param>
		/// <param name="httpStatus">The HTTP status of the response that caused the exception.</param>
		/// <param name="uri">The URL queried.</param>
		/// <param name="diagnosticLog">The human readable representation of the request/response.</param>
		/// <param name="innerException">The underlying exception that caused this one.</param>
		public HttpException(string message, HttpStatusCode httpStatus, Uri uri, string diagnosticLog, Exception innerException)
			: base(message, innerException)
		{
			HttpStatus = httpStatus;
			Uri = uri;
			DiagnosticLog = diagnosticLog;
		}
	}
}
