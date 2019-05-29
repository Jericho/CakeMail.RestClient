using System;

namespace CakeMail.RestClient.Exceptions
{
	/// <summary>
	/// This class represents a generic CakeMail error. All other exceptions thrown by
	/// the CakeMail API subclass this exception.
	/// </summary>
	public class CakeMailException : ApplicationException
	{
		/// <summary>
		/// Gets the human readable representation of the request/response.
		/// </summary>
		public string DiagnosticLog { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailException"/> class.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="diagnosticLog">The human readable representation of the request/response.</param>
		public CakeMailException(string message, string diagnosticLog)
			: base(message)
		{
			DiagnosticLog = diagnosticLog;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailException"/> class.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="diagnosticLog">The human readable representation of the request/response.</param>
		/// <param name="innerException">The inner exception.</param>
		public CakeMailException(string message, string diagnosticLog, Exception innerException)
			: base(message, innerException)
		{
			DiagnosticLog = diagnosticLog;
		}
	}
}
