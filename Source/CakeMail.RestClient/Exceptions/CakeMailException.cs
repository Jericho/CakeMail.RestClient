using System;

namespace CakeMail.RestClient.Exceptions
{
	/// <summary>
	/// This class represents a generic CakeMail error. All other exceptions thrown by
	/// the CakeMail API subclass this exception
	/// </summary>
#if NETSTANDARD
	public class CakeMailException : Exception
#else
	public class CakeMailException : ApplicationException
#endif
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailException"/> class.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public CakeMailException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailException"/> class.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="innerException">The inner exception.</param>
		public CakeMailException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
