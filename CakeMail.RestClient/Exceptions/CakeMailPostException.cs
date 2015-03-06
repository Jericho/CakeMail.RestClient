using System;

namespace CakeMail.RestClient.Exceptions
{
	/// <summary>
	/// This class represents an exception thrown by the CakeMail API when the posted data is determined to be invalid.
	/// </summary>
	public class CakeMailPostException : CakeMailException
	{
		public string PostData { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailPostException"/> class.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public CakeMailPostException(string message, string postData)
			: base(message) 
		{
			this.PostData = postData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailPostException"/> class.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="innerException">The inner exception.</param>
		public CakeMailPostException(string message, string postData, Exception innerException)
			: base(message, innerException)
		{
			this.PostData = postData;
		}
	}
}